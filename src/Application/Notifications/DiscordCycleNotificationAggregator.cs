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
                state.Underway[notification.SubmarineId] = notification;
                this.log.Log(LogLevel.Trace, $"[Notifications] Discord aggregator recorded underway submarine={notification.SubmarineLabel} count={state.Underway.Count}.");

                if (!state.CycleReady)
                {
                    this.log.Log(LogLevel.Trace, $"[Notifications] Discord aggregator cycle not ready, suppressing notification. cycleReady={state.CycleReady} completed={state.Completed.Count}");
                    return Decision.Suppress();
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

        public void Reset()
        {
            this.Completed.Clear();
            this.Underway.Clear();
            this.CycleReady = false;
        }
    }

    private bool TryCreateAggregate(CycleState state, out VoyageNotification[] aggregate)
    {
        aggregate = state.Underway.Values
            .GroupBy(n => n.SubmarineId)
            .Select(g => g.OrderByDescending(x => x.ArrivalUtc).First())
            .OrderBy(n => n.ArrivalUtc)
            .ToArray();

        return state.CycleReady && aggregate.Length >= CycleSize;
    }

    private Decision BuildAggregateDecision(CycleState state, VoyageNotification[] aggregate, string reason, bool forceImmediate = false)
    {
        var payload = this.formatter.CreateDiscordBatchPayload(VoyageStatus.Underway, aggregate[0].CharacterLabel, aggregate);
        var latestArrival = aggregate[^1].ArrivalUtc;
        this.log.Log(LogLevel.Information,
            $"[Notifications] Discord aggregator flushing cycle reason={reason} character={aggregate[0].CharacterLabel} submarines={aggregate.Length} latestArrival={latestArrival:O}.");
        
        // ForceImmediate の場合は状態をリセットしない（通常運用の CycleReady を維持）
        if (!forceImmediate)
        {
            state.Reset();
        }
        else
        {
            this.log.Log(LogLevel.Debug, "[Notifications] Discord aggregator preserving cycle state for ForceImmediate.");
        }
        
        return Decision.Aggregated(aggregate[0].CharacterLabel, payload, latestArrival);
    }
}
