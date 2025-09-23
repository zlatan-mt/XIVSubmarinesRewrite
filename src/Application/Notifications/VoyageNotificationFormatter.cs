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
    private const string UnderwayColor = "#3498DB";

    public DiscordNotificationPayload CreateDiscordPayload(VoyageNotification notification)
    {
        var color = notification.Status switch
        {
            Domain.Models.VoyageStatus.Completed => CompletedColor,
            Domain.Models.VoyageStatus.Underway => UnderwayColor,
            Domain.Models.VoyageStatus.Failed => "#E74C3C",
            _ => "#95A5A6",
        };

        var description = notification.Status switch
        {
            Domain.Models.VoyageStatus.Completed => $"{notification.SubmarineLabel} が航海を完了しました。",
            Domain.Models.VoyageStatus.Underway => $"{notification.SubmarineLabel} が航海を開始しました。次の帰港は {FormatLocalTimestamp(notification.ArrivalLocal)} です。",
            _ => $"{notification.SubmarineLabel} の状態が更新されました。",
        };

        var fields = BuildDiscordFields(notification);

        var title = notification.Status switch
        {
            Domain.Models.VoyageStatus.Completed => $"{notification.SubmarineLabel} 帰港",
            Domain.Models.VoyageStatus.Underway => $"{notification.SubmarineLabel} 出航",
            Domain.Models.VoyageStatus.Failed => $"{notification.SubmarineLabel} 航海失敗",
            _ => $"{notification.SubmarineLabel} 状態更新",
        };

        return new DiscordNotificationPayload(
            Title: title,
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
            ["Remaining"] = FormatRemaining(notification),
        };

        var routeDisplay = NormalizeRouteDisplay(notification.RouteDisplay, notification.RouteId);
        if (!string.IsNullOrEmpty(routeDisplay))
        {
            properties["RouteDisplay"] = routeDisplay;
        }

        properties["ArrivalLocal"] = notification.ArrivalLocal.ToString("O", CultureInfo.InvariantCulture);

        return new NotionNotificationPayload(properties);
    }

    public DiscordNotificationPayload CreateDiscordBatchPayload(Domain.Models.VoyageStatus status, string characterLabel, IReadOnlyList<VoyageNotification> notifications)
    {
        if (notifications.Count == 0)
        {
            throw new ArgumentException("At least one notification is required for batch payloads.", nameof(notifications));
        }

        var description = status switch
        {
            Domain.Models.VoyageStatus.Completed => $"{characterLabel} の潜水艦 {notifications.Count} 隻が帰港しました。",
            Domain.Models.VoyageStatus.Underway => $"{characterLabel} の潜水艦 {notifications.Count} 隻が出航しました。次の帰港予定を確認してください。",
            Domain.Models.VoyageStatus.Failed => $"{characterLabel} の潜水艦 {notifications.Count} 隻で異常が発生しました。",
            _ => $"{characterLabel} の潜水艦 {notifications.Count} 隻の状態が更新されました。",
        };
        var fields = new List<DiscordNotificationField>(notifications.Count);

        foreach (var notification in notifications)
        {
            var parts = BuildBatchLines(status, notification);
            fields.Add(new DiscordNotificationField(notification.SubmarineLabel, string.Join("\n", parts), false));
        }

        var color = status switch
        {
            Domain.Models.VoyageStatus.Completed => CompletedColor,
            Domain.Models.VoyageStatus.Underway => UnderwayColor,
            Domain.Models.VoyageStatus.Failed => "#E74C3C",
            _ => "#95A5A6",
        };

        var title = status switch
        {
            Domain.Models.VoyageStatus.Completed => $"{notifications.Count} 隻が帰港",
            Domain.Models.VoyageStatus.Underway => $"{notifications.Count} 隻が出航",
            Domain.Models.VoyageStatus.Failed => $"{notifications.Count} 隻でエラー",
            _ => $"{notifications.Count} 隻の状態更新",
        };

        return new DiscordNotificationPayload(
            Title: title,
            Description: description,
            Color: color,
            Fields: fields,
            Footer: string.Empty);
    }

    private static List<DiscordNotificationField> BuildDiscordFields(VoyageNotification notification)
    {
        var fields = new List<DiscordNotificationField>
        {
            new ("キャラクター", notification.CharacterLabel, true),
            new ("航路", notification.RouteDisplay ?? notification.RouteId ?? "不明", true),
        };

        if (notification.DepartureUtc.HasValue)
        {
            fields.Add(new ("出航", FormatLocalTimestamp(notification.DepartureUtc), true));
        }

        if (notification.Status == Domain.Models.VoyageStatus.Underway)
        {
            fields.Add(new ("次の帰港", FormatLocalTimestamp(notification.ArrivalLocal), true));
            fields.Add(new ("残り時間", FormatRemaining(notification), true));
        }
        else
        {
            fields.Add(new ("帰港", FormatLocalTimestamp(notification.ArrivalLocal), true));
            fields.Add(new ("経過", FormatRemaining(notification), true));
        }

        fields.Add(new ("信頼度", notification.Confidence.ToString(), true));

        return fields;
    }

    private static IEnumerable<string> BuildBatchLines(Domain.Models.VoyageStatus status, VoyageNotification notification)
    {
        var route = $"航路: {notification.RouteDisplay ?? notification.RouteId ?? "不明"}";
        var arrival = status == Domain.Models.VoyageStatus.Underway
            ? $"次の帰港: {FormatLocalTimestamp(notification.ArrivalLocal)}"
            : $"帰港: {FormatLocalTimestamp(notification.ArrivalLocal)}";
        var remainingLabel = status == Domain.Models.VoyageStatus.Underway ? "残り" : "経過";
        yield return route;
        yield return arrival;
        yield return $"{remainingLabel}: {FormatRemaining(notification)}";
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
        var nowUtc = DateTime.UtcNow;
        var remaining = notification.ArrivalUtc - nowUtc;

        if (notification.Status == Domain.Models.VoyageStatus.Completed)
        {
            var elapsed = nowUtc - notification.ArrivalUtc;
            if (elapsed <= TimeSpan.FromMinutes(1))
            {
                return "完了";
            }

            var elapsedText = FormatDuration(elapsed);
            return elapsedText == "不明"
                ? "完了"
                : $"完了 ({elapsedText}前)";
        }

        if (remaining <= TimeSpan.Zero)
        {
            return "到着済み";
        }

        return FormatDuration(remaining);
    }

    // Notion 側で空欄になるのを避けるため、表示用の航路文字列を整えます。
    private static string? NormalizeRouteDisplay(string? routeDisplay, string? routeId)
    {
        if (string.IsNullOrWhiteSpace(routeDisplay))
        {
            return string.IsNullOrWhiteSpace(routeId) ? null : routeId;
        }

        var sanitized = routeDisplay.Trim();
        return sanitized.Length == 0 && !string.IsNullOrWhiteSpace(routeId)
            ? routeId
            : sanitized;
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
