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

        lock (this.gate)
        {
            if (!this.pendingNotifications.TryGetValue(envelope.CharacterId, out var bucket))
            {
                bucket = new Dictionary<string, NotificationEnvelope>(StringComparer.Ordinal);
                this.pendingNotifications[envelope.CharacterId] = bucket;
            }

            if (bucket.TryAdd(envelope.HashKey, envelope))
            {
                this.log.Log(LogLevel.Debug, $"[Notifications] Buffered voyage notification {envelope.HashKey} status={voyage.Status} arrival={voyage.Arrival}.");
            }
            else
            {
                this.log.Log(LogLevel.Trace, $"[Notifications] Pending buffer already contains {envelope.HashKey}; skipping duplicate.");
            }
        }
    }

    private void TryFlushPendingNotifications(AcquisitionSnapshot snapshot)
    {
        Dictionary<string, NotificationEnvelope>? buffered;
        lock (this.gate)
        {
            if (!this.pendingNotifications.TryGetValue(snapshot.CharacterId, out buffered) || buffered.Count == 0)
            {
                return;
            }

            if (!AllSubmarinesUnderway(snapshot.Submarines))
            {
                return;
            }

            this.pendingNotifications.Remove(snapshot.CharacterId);
            buffered = new Dictionary<string, NotificationEnvelope>(buffered, StringComparer.Ordinal);
        }

        this.log.Log(LogLevel.Debug, $"[Notifications] Flushing {buffered.Count} buffered voyage notification(s) for character {snapshot.CharacterId}.");
        foreach (var envelope in buffered.Values.OrderBy(e => e.Arrival))
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
