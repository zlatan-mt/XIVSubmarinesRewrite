namespace XIVSubmarinesRewrite.Application.Notifications;

using System;

/// <summary>Tracks the last known delivery outcome for a notification hash.</summary>
public sealed class NotificationDeliveryRecord
{
    public NotificationDeliveryRecord(string hashKey, NotificationDeliveryState state, DateTime expiresAtUtc)
    {
        this.HashKey = hashKey;
        this.State = state;
        this.ExpiresAtUtc = expiresAtUtc;
    }

    public string HashKey { get; }

    public NotificationDeliveryState State { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? DeliveredAtUtc { get; set; }

    public DateTime? DeadLetteredAtUtc { get; set; }

    public string? LastError { get; set; }

    public bool IsExpired(DateTime timestampUtc) => timestampUtc >= this.ExpiresAtUtc;
}
