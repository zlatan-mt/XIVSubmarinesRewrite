// apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs
// ForceNotifyUnderway 関連の処理を VoyageCompletionProjection から切り出した部分クラスです
// ForceNotify の評価と状態取得を明確にし、メインロジックを読みやすくするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.Buffering.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Partial class handling ForceNotifyUnderway bookkeeping.</summary>
public sealed partial class VoyageCompletionProjection
{
    private void EmitForceNotify(AcquisitionSnapshot snapshot, Voyage voyage, SubmarineId submarineId, string reason)
    {
        this.log.Log(LogLevel.Trace, $"[Notifications] ForceNotifyUnderway enqueuing voyage {voyage.Id} reason={reason} arrival={voyage.Arrival}.");
        this.EnqueueNotification(snapshot, voyage, forceImmediate: true);
        var sentAt = this.timeProvider.GetUtcNow().UtcDateTime;
        var arrivalUtc = voyage.Arrival?.ToUniversalTime();
        this.forceNotifyStates[submarineId] = new ForceNotifyState(sentAt + ForceNotifyCooldownWindow, arrivalUtc, null, reason, sentAt);
    }

    private void CleanupForceNotifyState(IEnumerable<Voyage> activeVoyages)
    {
        var activeSet = new HashSet<SubmarineId>();
        foreach (var voyage in activeVoyages)
        {
            activeSet.Add(voyage.Id.SubmarineId);
        }

        foreach (var key in new List<SubmarineId>(this.forceNotifyStates.Keys))
        {
            if (!activeSet.Contains(key))
            {
                this.forceNotifyStates.Remove(key);
            }
        }
    }

    public bool IsForceNotifyEnabled => this.notificationSettings.ForceNotifyUnderway;

    public IReadOnlyList<ForceNotifyStateSnapshot> GetForceNotifySnapshot()
    {
        lock (this.gate)
        {
            var now = this.timeProvider.GetUtcNow().UtcDateTime;
            var list = new List<ForceNotifyStateSnapshot>(this.forceNotifyStates.Count);
            foreach (var (submarineId, state) in this.forceNotifyStates)
            {
                var remaining = state.CooldownUntilUtc > now ? state.CooldownUntilUtc - now : TimeSpan.Zero;
                list.Add(new ForceNotifyStateSnapshot(submarineId, state.LastTriggerUtc, state.CooldownUntilUtc, state.LastArrivalUtc, remaining, state.Reason));
            }

            return list;
        }
    }

    public void RecordManualTrigger(ForceNotifyManualTrigger entry)
    {
        lock (this.gate)
        {
            this.manualTriggerLog.Enqueue(entry);
            while (this.manualTriggerLog.Count > ManualTriggerLogLimit)
            {
                this.manualTriggerLog.Dequeue();
            }
        }

        var characterLabel = entry.CharacterId == 0
            ? "unknown"
            : $"{entry.CharacterName ?? "?"}@{entry.World ?? "?"}";
        this.log.Log(LogLevel.Trace, $"[Notifications] Manual ForceNotify trigger recorded for {characterLabel} count={entry.NotificationsEnqueued} includeUnderway={entry.IncludeUnderway}.");
    }

    public IReadOnlyList<ForceNotifyManualTrigger> GetManualTriggerLog()
    {
        lock (this.gate)
        {
            return this.manualTriggerLog.ToArray();
        }
    }

    private readonly record struct ForceNotifyState(DateTime CooldownUntilUtc, DateTime? LastArrivalUtc, int? LastLoggedWholeMinutes, string Reason, DateTime LastTriggerUtc);
}
