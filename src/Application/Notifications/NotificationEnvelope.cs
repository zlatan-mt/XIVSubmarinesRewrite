namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Represents a deduplicated notification payload for voyage-related events.</summary>
public sealed record NotificationEnvelope(
    ulong CharacterId,
    string? CharacterName,
    string? WorldName,
    SubmarineId SubmarineId,
    string? SubmarineName,
    string? RouteId,
    VoyageId VoyageId,
    DateTime? Departure,
    DateTime Arrival,
    VoyageStatus Status,
    SnapshotConfidence Confidence,
    string HashKey)
{
    public static NotificationEnvelope Create(
        ulong characterId,
        string? characterName,
        string? worldName,
        SubmarineId submarineId,
        string? submarineName,
        string? routeId,
        VoyageId voyageId,
        DateTime? departure,
        DateTime arrival,
        VoyageStatus status,
        SnapshotConfidence confidence)
    {
        var normalizedArrival = NormalizeTimestamp(arrival);
        DateTime? normalizedDeparture = departure.HasValue ? NormalizeTimestamp(departure.Value) : null;
        var hashKey = ComputeHashKey(characterId, voyageId, normalizedArrival);
        return new NotificationEnvelope(
            characterId,
            characterName,
            worldName,
            submarineId,
            submarineName,
            routeId,
            voyageId,
            normalizedDeparture,
            normalizedArrival,
            status,
            confidence,
            hashKey);
    }

    public static string ComputeHashKey(ulong characterId, VoyageId voyageId, DateTime arrivalUtc)
    {
        var cidHex = characterId.ToString("X16", CultureInfo.InvariantCulture);
        var timestamp = arrivalUtc.ToString("O", CultureInfo.InvariantCulture);
        var payload = $"{cidHex}:{voyageId}:{timestamp}";
        var bytes = Encoding.UTF8.GetBytes(payload);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }

    private static DateTime NormalizeTimestamp(DateTime timestamp)
    {
        if (timestamp == DateTime.MinValue)
        {
            return timestamp;
        }

        if (timestamp.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(timestamp, DateTimeKind.Utc);
        }

        return timestamp.ToUniversalTime();
    }
}
