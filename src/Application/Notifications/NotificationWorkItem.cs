namespace XIVSubmarinesRewrite.Application.Notifications;

using System;

/// <summary>Wraps a notification envelope with retry bookkeeping.</summary>
public sealed class NotificationWorkItem
{
    public NotificationWorkItem(NotificationEnvelope envelope, DateTime createdAtUtc)
    {
        this.Envelope = envelope;
        this.CreatedAtUtc = createdAtUtc;
        this.NextAttemptAtUtc = createdAtUtc;
    }

    public NotificationEnvelope Envelope { get; }

    public int AttemptCount { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public DateTime? LastAttemptAtUtc { get; private set; }

    public DateTime NextAttemptAtUtc { get; private set; }

    public string? LastError { get; private set; }

    public NotificationDeliveryState State { get; private set; } = NotificationDeliveryState.Pending;

    internal void MarkDelivering(DateTime timestampUtc)
    {
        this.State = NotificationDeliveryState.Delivering;
        this.LastAttemptAtUtc = timestampUtc;
    }

    internal void MarkDelivered()
    {
        this.State = NotificationDeliveryState.Delivered;
        this.LastError = null;
    }

    internal void ScheduleRetry(DateTime timestampUtc, TimeSpan delay, string error)
    {
        this.State = NotificationDeliveryState.Pending;
        this.AttemptCount++;
        this.LastError = error;
        this.LastAttemptAtUtc = timestampUtc;
        this.NextAttemptAtUtc = timestampUtc + delay;
    }

    internal void MarkDeadLetter(DateTime timestampUtc, string error)
    {
        this.State = NotificationDeliveryState.DeadLetter;
        this.LastError = error;
        this.LastAttemptAtUtc = timestampUtc;
        this.NextAttemptAtUtc = DateTime.MaxValue;
    }
}
