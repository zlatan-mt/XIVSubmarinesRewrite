namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Normalized payload used when dispatching voyage completion notifications.</summary>
public sealed record VoyageNotification(
    ulong CharacterId,
    string CharacterLabel,
    string? CharacterName,
    string? WorldName,
    SubmarineId SubmarineId,
    string SubmarineLabel,
    string? SubmarineName,
    string? RouteId,
    string? RouteDisplay,
    VoyageId VoyageId,
    DateTime? DepartureUtc,
    DateTime ArrivalUtc,
    DateTime ArrivalLocal,
    TimeSpan? Duration,
    VoyageStatus Status,
    SnapshotConfidence Confidence,
    string HashKey,
    string HashKeyShort);
