// apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.Buffering.cs
// VoyageCompletionProjection の通知バッファ操作を切り出した部分クラスです
// 完了通知の遅延送信と一括フラッシュを整理するために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Partial class handling voyage notification buffering.</summary>
public sealed partial class VoyageCompletionProjection
{
    private void BufferNotification(AcquisitionSnapshot snapshot, Voyage voyage)
        => this.EnqueueNotification(snapshot, voyage, forceImmediate: false);

    private void EnqueueNotification(AcquisitionSnapshot snapshot, Voyage voyage, bool forceImmediate)
    {
        if (voyage.Arrival is null)
        {
            return;
        }

        var submarine = snapshot.Submarines.FirstOrDefault(s => s.Id.Equals(voyage.Id.SubmarineId));
        var identity = this.characterRegistry.GetIdentity(snapshot.CharacterId);
        var envelope = NotificationEnvelope.Create(
            snapshot.CharacterId,
            snapshot.CharacterName ?? identity?.Name,
            snapshot.WorldName ?? identity?.World,
            voyage.Id.SubmarineId,
            submarine?.Name,
            voyage.RouteId,
            voyage.Id,
            voyage.Departure,
            voyage.Arrival.Value,
            voyage.Status,
            snapshot.Confidence);

        if (forceImmediate)
        {
            if (this.queue.TryEnqueue(envelope))
            {
                this.log.Log(LogLevel.Debug, $"[Notifications] Enqueued voyage notification {envelope.HashKey} status={voyage.Status} arrival={voyage.Arrival}.");
            }
            return;
        }

        PendingNotificationAddResult result;
        lock (this.gate)
        {
            result = this.pendingNotifications.Add(envelope);
        }

        switch (result)
        {
            case PendingNotificationAddResult.Added:
                this.log.Log(LogLevel.Debug, $"[Notifications] Buffered voyage notification {envelope.HashKey} status={voyage.Status} arrival={voyage.Arrival}.");
                break;
            case PendingNotificationAddResult.Replaced:
                this.log.Log(LogLevel.Debug, $"[Notifications] Replaced buffered voyage notification with {envelope.HashKey} (submarine={envelope.SubmarineId}).");
                break;
            case PendingNotificationAddResult.Ignored:
                this.log.Log(LogLevel.Trace, $"[Notifications] Suppressed duplicate voyage notification {envelope.HashKey} (submarine={envelope.SubmarineId}).");
                break;
        }
    }

    private void TryFlushPendingNotifications(AcquisitionSnapshot snapshot)
    {
        IReadOnlyList<NotificationEnvelope>? buffered;
        lock (this.gate)
        {
            if (!AllSubmarinesUnderway(snapshot.Submarines))
            {
                return;
            }

            buffered = this.pendingNotifications.TakeAll(snapshot.CharacterId);
        }

        if (buffered is null)
        {
            return;
        }

        this.log.Log(LogLevel.Debug, $"[Notifications] Flushing {buffered.Count} buffered voyage notification(s) for character {snapshot.CharacterId}.");
        foreach (var envelope in buffered.OrderBy(e => e.Arrival))
        {
            if (this.queue.TryEnqueue(envelope))
            {
                this.log.Log(LogLevel.Debug, $"[Notifications] Enqueued voyage notification {envelope.HashKey} arrival={envelope.Arrival}.");
            }
            else
            {
                this.log.Log(LogLevel.Trace, $"[Notifications] Skipped buffered notification {envelope.HashKey}; queue rejected (duplicate or delivered).");
            }
        }
    }

    private static bool AllSubmarinesUnderway(IReadOnlyList<Submarine> submarines)
    {
        if (submarines.Count == 0)
        {
            return false;
        }

        foreach (var submarine in submarines)
        {
            var voyage = submarine.Voyages.LastOrDefault();
            if (voyage is null)
            {
                return false;
            }

            if (voyage.Status != VoyageStatus.Underway && voyage.Status != VoyageStatus.Scheduled)
            {
                return false;
            }
        }

        return true;
    }
}
