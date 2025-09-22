// apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs
// 航海通知データを各チャネル向けのペイロードへ整形します
// 表示内容を統一し、通知先ごとの見やすさを確保するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs, apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

/// <summary>Builds channel-specific payloads from voyage notifications.</summary>
public sealed class VoyageNotificationFormatter
{
    private const string CompletedColor = "#1ABC9C";

    public DiscordNotificationPayload CreateDiscordPayload(VoyageNotification notification)
    {
        var color = notification.Status switch
        {
            Domain.Models.VoyageStatus.Completed => CompletedColor,
            Domain.Models.VoyageStatus.Failed => "#E74C3C",
            _ => "#95A5A6",
        };

        var description = $"{notification.SubmarineLabel} が航海を完了しました。";

        var fields = new List<DiscordNotificationField>
        {
            new ("キャラクター", notification.CharacterLabel, true),
            new ("航路", notification.RouteId ?? "不明", true),
            new ("出航", FormatLocalTimestamp(notification.DepartureUtc), true),
            new ("帰港", FormatLocalTimestamp(notification.ArrivalLocal), true),
            new ("残り", FormatRemaining(notification), true),
            new ("信頼度", notification.Confidence.ToString(), true),
        };

        return new DiscordNotificationPayload(
            Title: $"{notification.SubmarineLabel} 帰港",
            Description: description,
            Color: color,
            Fields: fields,
            Footer: string.Empty);
    }

    public NotionNotificationPayload CreateNotionPayload(VoyageNotification notification)
    {
        var properties = new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            ["Character"] = notification.CharacterLabel,
            ["Submarine"] = notification.SubmarineLabel,
            ["Route"] = notification.RouteId,
            ["DepartureUtc"] = FormatIso(notification.DepartureUtc),
            ["ArrivalUtc"] = notification.ArrivalUtc.ToString("O", CultureInfo.InvariantCulture),
            ["Duration"] = FormatDuration(notification.Duration),
            ["Status"] = notification.Status.ToString(),
            ["Confidence"] = notification.Confidence.ToString(),
            ["Hash"] = notification.HashKey,
        };

        return new NotionNotificationPayload(properties);
    }

    public DiscordNotificationPayload CreateDiscordBatchPayload(string characterLabel, IReadOnlyList<VoyageNotification> notifications)
    {
        if (notifications.Count == 0)
        {
            throw new ArgumentException("At least one notification is required for batch payloads.", nameof(notifications));
        }

        var description = $"{characterLabel} の潜水艦 {notifications.Count} 隻が帰港しました。";
        var fields = new List<DiscordNotificationField>(notifications.Count);

        foreach (var notification in notifications)
        {
            var parts = new List<string>(3)
            {
                $"航路: {notification.RouteId ?? "不明"}",
                $"帰港: {FormatLocalTimestamp(notification.ArrivalLocal)}",
                $"残り: {FormatRemaining(notification)}",
            };

            fields.Add(new DiscordNotificationField(notification.SubmarineLabel, string.Join("\n", parts), false));
        }

        return new DiscordNotificationPayload(
            Title: $"{notifications.Count} 隻が帰港",
            Description: description,
            Color: CompletedColor,
            Fields: fields,
            Footer: string.Empty);
    }

    private static string FormatLocalTimestamp(DateTime? timestamp)
    {
        if (timestamp is null)
        {
            return "N/A";
        }

        var value = timestamp.Value;
        if (value.Kind == DateTimeKind.Utc)
        {
            value = value.ToLocalTime();
        }

        return value.ToString("M/d(ddd) HH:mm", CultureInfo.CurrentCulture);
    }

    private static string? FormatIso(DateTime? timestampUtc)
    {
        return timestampUtc?.ToString("O", CultureInfo.InvariantCulture);
    }

    private static string FormatDuration(TimeSpan? duration)
    {
        if (duration is null)
        {
            return "不明";
        }

        var span = duration.Value;
        if (span < TimeSpan.Zero)
        {
            span = TimeSpan.Zero;
        }

        if (span > TimeSpan.FromDays(14))
        {
            return "不明";
        }

        if (span.TotalHours >= 1)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}時間 {1}分", (int)span.TotalHours, span.Minutes);
        }

        return string.Format(CultureInfo.InvariantCulture, "{0}分", (int)Math.Round(span.TotalMinutes));
    }

    private static string FormatRemaining(VoyageNotification notification)
    {
        var remaining = notification.ArrivalUtc - DateTime.UtcNow;
        if (remaining < TimeSpan.Zero)
        {
            remaining = TimeSpan.Zero;
        }

        return FormatDuration(remaining);
    }
}

public sealed record DiscordNotificationPayload(
    string Title,
    string Description,
    string Color,
    IReadOnlyList<DiscordNotificationField> Fields,
    string? Footer);

public sealed record DiscordNotificationField(string Name, string Value, bool Inline);

public sealed record NotionNotificationPayload(IReadOnlyDictionary<string, string?> Properties);
