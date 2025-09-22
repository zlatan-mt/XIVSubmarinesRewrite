namespace XIVSubmarinesRewrite.Application.Notifications;

using System;

/// <summary>Immutable projection of a notification work item for diagnostics/UI.</summary>
public sealed record NotificationWorkItemSnapshot(
    NotificationEnvelope Envelope,
    NotificationDeliveryState State,
    int AttemptCount,
    DateTime NextAttemptAtUtc,
    DateTime? LastAttemptAtUtc,
    string? LastError);
