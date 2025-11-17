// apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordCycleNotificationAggregator.cs
// Discord 出航通知を 4 隻まとめて送信するための集約器です
// 4 隻が帰港・再出航したタイミングだけで Discord 通知を送りスパムを防ぐため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs, apps/XIVSubmarinesRewrite/src/Domain/Models/SubmarineId.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Aggregates voyage notifications so that a single Discord message covers all four submarines.</summary>
public sealed class DiscordCycleNotificationAggregator
{
    private const int CycleSize = 4;
    private readonly VoyageNotificationFormatter formatter;
    private readonly ILogSink log;
    private readonly Dictionary<ulong, CycleState> states = new ();

    public DiscordCycleNotificationAggregator(VoyageNotificationFormatter formatter, ILogSink log)
    {
        this.formatter = formatter;
        this.log = log;
    }

    public Decision Process(VoyageNotification notification, bool forceImmediate = false)
    {
        var state = this.GetState(notification.CharacterId);
        
        // デバッグログ：現在の状態を明示的に記録
        this.log.Log(LogLevel.Trace, $"[Notifications] Discord aggregator processing character={notification.CharacterLabel} status={notification.Status} forceImmediate={forceImmediate} cycleReady={state.CycleReady} completed={state.Completed.Count} underway={state.Underway.Count}");
        
        if (forceImmediate)
        {
            this.log.Log(LogLevel.Debug, $"[Notifications] Discord aggregator ForceImmediate mode: state will be updated but not reset on flush");
        }
        
        switch (notification.Status)
        {
            case VoyageStatus.Completed:
                state.Completed[notification.SubmarineId] = notification;
                this.log.Log(LogLevel.Trace, $"[Notifications] Discord aggregator recorded completion submarine={notification.SubmarineLabel} total={state.Completed.Count}.");

                if (!state.CycleReady && state.Completed.Count >= CycleSize)
                {
                    state.CycleReady = true;
                    this.log.Log(LogLevel.Debug, "[Notifications] Discord aggregator cycle ready (all submarines completed).");
                }

                if (state.CycleReady && this.TryCreateAggregate(state, out var aggregateFromCompletion))
                {
                    return this.BuildAggregateDecision(state, aggregateFromCompletion, "completion", forceImmediate);
                }

                return Decision.Suppress();

            case VoyageStatus.Underway:
                // デバッグログ: SubmarineId、Slot、HashKeyを記録
                var existingNotification = state.Underway.TryGetValue(notification.SubmarineId, out var existing) ? existing : null;
                state.Underway[notification.SubmarineId] = notification;
                this.log.Log(LogLevel.Debug, 
                    $"[Notifications] Discord aggregator recorded underway: SubmarineId={notification.SubmarineId} " +
                    $"Slot={notification.SubmarineId.Slot} Label={notification.SubmarineLabel} " +
                    $"HashKey={notification.HashKey} ArrivalUtc={notification.ArrivalUtc:O} " +
                    $"count={state.Underway.Count}" +
                    (existingNotification != null ? $" (replaced existing: HashKey={existingNotification.HashKey})" : ""));

                if (!state.CycleReady)
                {
                    if (forceImmediate && state.Underway.Count >= CycleSize)
                    {
                        state.CycleReady = true;
                        state.ForceImmediatePrimed = true;
                        this.log.Log(LogLevel.Debug,
                            "[Notifications] Discord aggregator primed cycle via ForceImmediate underway notifications (count reached 4).");
                    }
                    else
                    {
                        this.log.Log(LogLevel.Trace,
                            $"[Notifications] Discord aggregator cycle not ready, suppressing notification. cycleReady={state.CycleReady} completed={state.Completed.Count}");
                        return Decision.Suppress();
                    }
                }

                if (!this.TryCreateAggregate(state, out var aggregateFromUnderway))
                {
                    this.log.Log(LogLevel.Trace, $"[Notifications] Discord aggregator could not create aggregate, suppressing notification. underway={state.Underway.Count}");
                    return Decision.Suppress();
                }

                return this.BuildAggregateDecision(state, aggregateFromUnderway, "underway", forceImmediate);

            default:
                return Decision.Forward();
        }
    }


    /// <summary>Resets the cycle state for a specific character to allow new ForceNotify requests.</summary>
    public void ResetCycle(ulong characterId)
    {
        if (this.states.TryGetValue(characterId, out var state))
        {
            state.Reset();
            this.log.Log(LogLevel.Debug, $"[Notifications] Discord aggregator cycle reset for character={characterId}.");
        }
    }

    private CycleState GetState(ulong characterId)
    {
        if (!this.states.TryGetValue(characterId, out var state))
        {
            state = new CycleState();
            this.states[characterId] = state;
        }

        return state;
    }

    public readonly record struct Decision(bool IsSuppressed, AggregatedNotification? Aggregate)
    {
        public static Decision Suppress() => new (true, null);

        public static Decision Forward() => new (false, null);

        public static Decision Aggregated(string characterLabel, DiscordNotificationPayload payload, DateTime timestampUtc)
            => new (false, new AggregatedNotification(characterLabel, payload, timestampUtc));
    }

    public sealed record AggregatedNotification(string CharacterLabel, DiscordNotificationPayload Payload, DateTime TimestampUtc);

    private sealed class CycleState
    {
        public Dictionary<SubmarineId, VoyageNotification> Completed { get; } = new ();

        public Dictionary<SubmarineId, VoyageNotification> Underway { get; } = new ();

        public bool CycleReady { get; set; }

        public bool ForceImmediatePrimed { get; set; }

        public void Reset()
        {
            this.Completed.Clear();
            this.Underway.Clear();
            this.CycleReady = false;
            this.ForceImmediatePrimed = false;
        }
    }

    private bool TryCreateAggregate(CycleState state, out VoyageNotification[] aggregate)
    {
        aggregate = state.Underway.Values
            .GroupBy(n => n.SubmarineId)
            .Select(g => g.OrderByDescending(x => x.ArrivalUtc).First())
            .OrderBy(n => n.ArrivalUtc)
            .ToArray();

        // 重複検出ロジック: 同じSubmarineIdが複数ある場合を検出
        var duplicateGroups = aggregate
            .GroupBy(n => n.SubmarineId)
            .Where(g => g.Count() > 1)
            .ToList();
        
        if (duplicateGroups.Any())
        {
            foreach (var group in duplicateGroups)
            {
                this.log.Log(LogLevel.Warning,
                    $"[Notifications] Duplicate SubmarineId detected in aggregate: SubmarineId={group.Key} " +
                    $"Slot={group.Key.Slot} Count={group.Count()} " +
                    $"HashKeys=[{string.Join(", ", group.Select(n => n.HashKey))}]");
            }
        }

        // Slot番号の検証: 0-3の範囲に正規化されているか確認
        var invalidSlots = aggregate.Where(n => n.SubmarineId.Slot >= 4 && !n.SubmarineId.IsPending).ToList();
        if (invalidSlots.Any())
        {
            this.log.Log(LogLevel.Warning,
                $"[Notifications] Invalid Slot numbers detected in aggregate: " +
                string.Join(", ", invalidSlots.Select(n => $"SubmarineId={n.SubmarineId} Slot={n.SubmarineId.Slot}")));
        }

        // 集約結果の詳細ログ
        if (aggregate.Length > 0)
        {
            var slotSummary = string.Join(", ", aggregate.Select(n => $"Slot{n.SubmarineId.Slot}:{n.SubmarineLabel}"));
            this.log.Log(LogLevel.Debug,
                $"[Notifications] Discord aggregator created aggregate: count={aggregate.Length} slots=[{slotSummary}]");
        }

        return state.CycleReady && aggregate.Length >= CycleSize;
    }

    private Decision BuildAggregateDecision(CycleState state, VoyageNotification[] aggregate, string reason, bool forceImmediate = false)
    {
        // 集約結果の詳細ログ（重複チェック）
        var uniqueSubmarineIds = aggregate.Select(n => n.SubmarineId).Distinct().Count();
        if (uniqueSubmarineIds < aggregate.Length)
        {
            this.log.Log(LogLevel.Warning,
                $"[Notifications] Discord aggregator detected duplicate SubmarineIds in final aggregate: " +
                $"total={aggregate.Length} unique={uniqueSubmarineIds} " +
                $"SubmarineIds=[{string.Join(", ", aggregate.Select(n => $"{n.SubmarineId}(Slot{n.SubmarineId.Slot})"))}]");
        }

        var payload = this.formatter.CreateDiscordBatchPayload(VoyageStatus.Underway, aggregate[0].CharacterLabel, aggregate);
        var latestArrival = aggregate[^1].ArrivalUtc;
        this.log.Log(LogLevel.Debug,
            $"[Notifications] Discord aggregator flushing cycle reason={reason} character={aggregate[0].CharacterLabel} " +
            $"submarines={aggregate.Length} uniqueSubmarineIds={uniqueSubmarineIds} latestArrival={latestArrival:O}.");
        
        // ForceImmediate で強制的に cycle を整えた場合は送信後にリセットする
        if (!forceImmediate || state.ForceImmediatePrimed)
        {
            if (forceImmediate && state.ForceImmediatePrimed)
            {
                this.log.Log(LogLevel.Debug, "[Notifications] Discord aggregator resetting forced cycle after ForceImmediate delivery.");
            }

            state.Reset();
        }
        else
        {
            this.log.Log(LogLevel.Debug, "[Notifications] Discord aggregator preserving cycle state for ForceImmediate.");
        }

        return Decision.Aggregated(aggregate[0].CharacterLabel, payload, latestArrival);
    }
}
