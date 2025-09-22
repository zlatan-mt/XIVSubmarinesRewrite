namespace XIVSubmarinesRewrite.Application.Notifications;

using System;

/// <summary>Configurable parameters for notification queue retry behaviour.</summary>
public sealed class NotificationQueueOptions
{
    public static readonly TimeSpan[] DefaultRetrySchedule =
    {
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(30),
        TimeSpan.FromMinutes(2),
        TimeSpan.FromMinutes(10),
        TimeSpan.FromMinutes(30),
    };

    public TimeSpan[] RetrySchedule { get; set; } = DefaultRetrySchedule;

    public int MaxAttempts { get; set; } = 5;

    public TimeSpan DeliveryRecordTtl { get; set; } = TimeSpan.FromHours(24);

    public int DeadLetterCapacity { get; set; } = 64;
}
