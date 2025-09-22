namespace XIVSubmarinesRewrite.Application.Notifications;

using System;

/// <summary>Raised when a remote notification endpoint signals rate limiting.</summary>
public sealed class NotificationRateLimitException : Exception
{
    public NotificationRateLimitException(TimeSpan retryAfter, string? message = null)
        : base(message ?? "Notification rate limited")
    {
        this.RetryAfter = retryAfter;
    }

    public TimeSpan RetryAfter { get; }
}
