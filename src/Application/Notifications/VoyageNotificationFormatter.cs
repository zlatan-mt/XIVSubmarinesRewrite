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
        // Phase 13: Underway通知のみサポート（Completedは送信されない）
        if (notification.Status != Domain.Models.VoyageStatus.Underway)
        {
            throw new ArgumentException(
                $"Only Underway notifications are supported. Got: {notification.Status}. " +
                "Completed notifications have been disabled in Phase 13.",
                nameof(notification));
        }

        // タイトル: [潜水艦名] 出航
        var title = $"{notification.SubmarineLabel} 出航";

        // 説明: 航路名のみ
        var description = notification.RouteDisplay ?? notification.RouteId ?? "航路不明";

        // 色: Underway用
        var color = UnderwayColor;

        // フィールド: 帰還予定のみ
        var arrivalTime = FormatLocalTimestamp(notification.ArrivalLocal);
        var remaining = FormatRemainingConcise(notification.Duration);
        var arrivalField = new DiscordNotificationField(
            "帰還予定",
            $"{arrivalTime} ({remaining})",
            false // inline
        );

        var fields = new List<DiscordNotificationField> { arrivalField };

        return new DiscordNotificationPayload(
            Title: title,
            Description: description,
            Color: color,
            Fields: fields,
            Footer: null);
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

        // Phase 13: Underway通知のみサポート
        if (status != Domain.Models.VoyageStatus.Underway)
        {
            throw new ArgumentException(
                $"Only Underway batch notifications are supported. Got: {status}. " +
                "Completed notifications have been disabled in Phase 13.",
                nameof(status));
        }

        // タイトル: [キャラクター名] - [N]隻出航
        var title = $"{characterLabel} - {notifications.Count}隻出航";

        // 説明: シンプルに（空欄でもOK）
        var description = string.Empty;

        // 色: Underway用
        var color = UnderwayColor;

        // 各潜水艦を1行で表示: "潜水艦名: 日時 (残り時間)"
        var fields = new List<DiscordNotificationField>(notifications.Count);
        foreach (var notification in notifications)
        {
            var arrivalTime = FormatLocalTimestamp(notification.ArrivalLocal);
            var remaining = FormatRemainingConcise(notification.Duration);
            var value = $"{arrivalTime} ({remaining})";
            
            fields.Add(new DiscordNotificationField(
                notification.SubmarineLabel,
                value,
                true // inline = true で横並び可能に
            ));
        }

        return new DiscordNotificationPayload(
            Title: title,
            Description: description,
            Color: color,
            Fields: fields,
            Footer: null);
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

    /// <summary>
    /// 残り時間を簡潔にフォーマット（12h, 30m, 12.5h）
    /// Phase 13: Discord通知最適化用
    /// </summary>
    /// <param name="duration">残り時間</param>
    /// <returns>簡潔な時間表記（例: "12h", "30m", "12.5h"）</returns>
    private static string FormatRemainingConcise(TimeSpan? duration)
    {
        if (duration is null || duration.Value <= TimeSpan.Zero)
        {
            return "0m";
        }

        var span = duration.Value;

        // 異常値チェック（14日超過）
        if (span > TimeSpan.FromDays(14))
        {
            return "14d+";
        }

        // 1時間未満: 分単位
        if (span.TotalHours < 1)
        {
            var minutes = (int)Math.Ceiling(span.TotalMinutes);
            return $"{minutes}m";
        }

        // 1時間以上: 時間単位（0.5刻みで丸める）
        var hours = span.TotalHours;
        var roundedHours = Math.Round(hours * 2) / 2; // 0.5刻み

        // 整数時間の場合
        if (Math.Abs(roundedHours - Math.Floor(roundedHours)) < 0.01)
        {
            return $"{(int)roundedHours}h";
        }

        // 小数点表示（.5のみ）
        return $"{roundedHours:F1}h";
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
