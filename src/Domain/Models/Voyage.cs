namespace XIVSubmarinesRewrite.Domain.Models;

using System;

/// <summary>Tracks a single voyage lifecycle for scheduling and alarms.</summary>
public sealed record Voyage(VoyageId Id, string RouteId, DateTime Departure, DateTime? Arrival, VoyageStatus Status);

public enum VoyageStatus
{
    Unknown = 0,
    Scheduled = 1,
    Underway = 2,
    Completed = 3,
    Failed = 4
}
