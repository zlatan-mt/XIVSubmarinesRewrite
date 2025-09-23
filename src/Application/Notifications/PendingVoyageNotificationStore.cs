// apps/XIVSubmarinesRewrite/src/Application/Notifications/PendingVoyageNotificationStore.cs
// VoyageCompletionProjection の保留通知を管理する補助クラスです
// バッファ操作を単一責任に切り出し、重複排除とフラッシュ処理を簡潔にするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.Buffering.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotification.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Stores pending voyage notifications per character with duplicate suppression.</summary>
internal sealed class PendingVoyageNotificationStore
{
    private readonly TimeSpan arrivalTolerance;
    private readonly Dictionary<ulong, CharacterPendingNotifications> buffers = new ();

    internal PendingVoyageNotificationStore(TimeSpan arrivalTolerance)
    {
        this.arrivalTolerance = arrivalTolerance;
    }

    public PendingNotificationAddResult Add(NotificationEnvelope envelope)
    {
        if (!this.buffers.TryGetValue(envelope.CharacterId, out var buffer))
        {
            buffer = new CharacterPendingNotifications(this.arrivalTolerance);
            this.buffers[envelope.CharacterId] = buffer;
        }

        return buffer.Add(envelope);
    }

    public IReadOnlyList<NotificationEnvelope>? TakeAll(ulong characterId)
    {
        if (!this.buffers.TryGetValue(characterId, out var buffer))
        {
            return null;
        }

        var items = buffer.Collect();
        this.buffers.Remove(characterId);
        return items.Count == 0 ? null : items;
    }

    private sealed class CharacterPendingNotifications
    {
        private readonly TimeSpan arrivalTolerance;
        private readonly Dictionary<string, NotificationEnvelope> entries = new (StringComparer.Ordinal);

        internal CharacterPendingNotifications(TimeSpan arrivalTolerance)
        {
            this.arrivalTolerance = arrivalTolerance;
        }

        internal PendingNotificationAddResult Add(NotificationEnvelope envelope)
        {
            if (this.TryFindDuplicate(envelope, out var existingKey, out var existing))
            {
                if (ShouldReplace(existing, envelope))
                {
                    this.entries.Remove(existingKey!);
                    this.entries[envelope.HashKey] = envelope;
                    return PendingNotificationAddResult.Replaced;
                }

                return PendingNotificationAddResult.Ignored;
            }

            this.entries[envelope.HashKey] = envelope;
            return PendingNotificationAddResult.Added;
        }

        internal IReadOnlyList<NotificationEnvelope> Collect()
        {
            return new List<NotificationEnvelope>(this.entries.Values);
        }

        private bool TryFindDuplicate(NotificationEnvelope candidate, out string? existingKey, out NotificationEnvelope existing)
        {
            existingKey = null;
            existing = default!;

            foreach (var kvp in this.entries)
            {
                var envelope = kvp.Value;
                if (!envelope.SubmarineId.Equals(candidate.SubmarineId))
                {
                    continue;
                }

                if (AreArrivalsClose(envelope.Arrival, candidate.Arrival, this.arrivalTolerance))
                {
                    existingKey = kvp.Key;
                    existing = envelope;
                    return true;
                }
            }

            return false;
        }

        private static bool ShouldReplace(NotificationEnvelope existing, NotificationEnvelope candidate)
        {
            if (candidate.Arrival < existing.Arrival)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(existing.RouteId) && !string.IsNullOrWhiteSpace(candidate.RouteId))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(existing.SubmarineName) && !string.IsNullOrWhiteSpace(candidate.SubmarineName))
            {
                return true;
            }

            return candidate.Confidence.CompareTo(existing.Confidence) > 0;
        }
    }

    private static bool AreArrivalsClose(DateTime first, DateTime second, TimeSpan tolerance)
        => (first - second).Duration() <= tolerance;
}

internal enum PendingNotificationAddResult
{
    Added,
    Replaced,
    Ignored,
}
