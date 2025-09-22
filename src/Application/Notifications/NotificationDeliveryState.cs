namespace XIVSubmarinesRewrite.Application.Notifications;

/// <summary>Represents the lifecycle state of a queued notification.</summary>
public enum NotificationDeliveryState
{
    Pending = 0,
    Delivering = 1,
    Delivered = 2,
    DeadLetter = 3,
}
