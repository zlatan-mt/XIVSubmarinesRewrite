// apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs
// 航海通知データを各チャネル向けのペイロードへ整形します
// 表示内容を統一し、通知先ごとの見やすさを確保するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs, apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>Builds channel-specific payloads from voyage notifications.</summary>
public sealed class VoyageNotificationFormatter
{
    private const string CompletedColor = "#1ABC9C";
    private const string UnderwayColor = "#3498DB";

    private readonly NotificationSettings settings;

    public VoyageNotificationFormatter(NotificationSettings settings)
    {
        this.settings = settings;
    }

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

        // オプション: リマインダーコマンド（Task 3.2）
        if (this.settings.EnableReminderCommand)
        {
            var reminderCommand = FormatReminderCommand(
                this.settings.ReminderChannelName,
                notification.ArrivalLocal,
                $"{notification.SubmarineLabel}が帰還"
            );
            var reminderField = new DiscordNotificationField(
                "リマインダー設定",
                $"`{reminderCommand}`",
                false
            );
            fields.Add(reminderField);
        }

        return new DiscordNotificationPayload(
            Title: title,
            Description: description,
            Color: color,
            Fields: fields,
            Footer: null);
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

        // 色: Underway用
        var color = UnderwayColor;

        // 最も遅く帰還する潜水艦を特定（メイン表示用）
        var sortedNotifications = notifications.OrderBy(n => n.ArrivalUtc).ToList();
        var latestNotification = sortedNotifications[^1]; // 最後（最も遅い）

        // 説明: 最も遅い帰還時刻のみを表示
        // 太字で強調し、視覚的に目立たせる（Discordのembed descriptionでは色の直接指定は不可）
        var latestArrivalTime = FormatLocalTimestamp(latestNotification.ArrivalLocal);
        var description = $"**🟠 帰還時間: {latestArrivalTime}**";

        // 各潜水艦を1行ずつ縦に並べて表示
        // フォーマット: "潜水艦名 帰還時間(曜日)"
        // 潜水艦名の長さを統一して、帰還日時を揃える
        // Discordのコードブロックを使用して等幅フォントで表示し、確実に整列させる
        var maxLabelLength = sortedNotifications.Max(n => n.SubmarineLabel?.Length ?? 0);
        var submarineLines = sortedNotifications.Select(n =>
        {
            var arrivalTime = FormatLocalTimestamp(n.ArrivalLocal);
            // 潜水艦名を固定幅にパディング（半角スペースで揃える）
            // コードブロック内では等幅フォントが使われるため、半角スペースで十分
            var paddedLabel = (n.SubmarineLabel ?? string.Empty).PadRight(maxLabelLength, ' '); // 半角スペース
            return $"{paddedLabel} {arrivalTime}";
        });

        // 説明フィールドに全情報を追加（縦並び表示）
        // Discordのコードブロックで囲むことで、等幅フォントで表示し、アライメントを保証
        description = $"{description}\n\n```\n{string.Join("\n", submarineLines)}\n```";

        // フィールドは使用しない（説明フィールドに全情報を表示）
        var fields = new List<DiscordNotificationField>();

        // オプション: バッチリマインダーコマンド（Task 3.3）
        if (this.settings.EnableReminderCommand && notifications.Count > 0)
        {
            // 最も早い帰還時刻を使用
            var firstArrival = notifications.Min(n => n.ArrivalLocal);
            var reminderCommand = FormatReminderCommand(
                this.settings.ReminderChannelName,
                firstArrival,
                $"{notifications.Count}隻帰還開始"
            );
            var reminderField = new DiscordNotificationField(
                "リマインダー一括設定",
                $"`{reminderCommand}`",
                false
            );
            fields.Add(reminderField);
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

    /// <summary>
    /// Discord Reminder Bot用のコマンド文字列を生成
    /// Phase 13: Discord Reminder Bot統合
    /// </summary>
    /// <param name="channelName">リマインダーを送信するチャンネル名（#付き推奨）</param>
    /// <param name="arrivalTime">帰還時刻（ローカル時刻）</param>
    /// <param name="message">リマインダーメッセージ</param>
    /// <returns>Discord Reminder Botのコマンド文字列</returns>
    private static string FormatReminderCommand(string channelName, DateTime arrivalTime, string message)
    {
        // チャンネル名のバリデーション
        var sanitizedChannel = SanitizeChannelName(channelName);

        // Reminder Botの時刻フォーマット: "M/d HH:mm" 形式
        // 例: "10/26 14:30"
        var timeStr = arrivalTime.ToString("M/d HH:mm", CultureInfo.InvariantCulture);

        // メッセージをサニタイズ
        var sanitizedMessage = SanitizeReminderMessage(message);

        return $"/remind {sanitizedChannel} {timeStr} {sanitizedMessage}";
    }

    /// <summary>
    /// チャンネル名をサニタイズ（#プレフィックス確認、危険文字削除）
    /// </summary>
    private static string SanitizeChannelName(string channelName)
    {
        if (string.IsNullOrWhiteSpace(channelName))
        {
            return "#submarine"; // デフォルト
        }

        var trimmed = channelName.Trim();

        // #プレフィックスがない場合は追加
        if (!trimmed.StartsWith("#"))
        {
            trimmed = "#" + trimmed;
        }

        // 英数字、ハイフン、アンダースコア、#のみ許可
        var sanitized = new string(trimmed
            .Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '#')
            .ToArray());

        // 有効なチャンネル名でない場合はデフォルト
        if (string.IsNullOrEmpty(sanitized) || sanitized == "#")
        {
            return "#submarine";
        }

        return sanitized;
    }

    /// <summary>
    /// リマインダーメッセージをサニタイズ（改行削除、長さ制限）
    /// </summary>
    private static string SanitizeReminderMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return "帰還";
        }

        // 改行を空白に変換
        var sanitized = message.Replace("\n", " ").Replace("\r", " ");

        // 連続する空白を1つに
        while (sanitized.Contains("  "))
        {
            sanitized = sanitized.Replace("  ", " ");
        }

        // 最大100文字に制限
        if (sanitized.Length > 100)
        {
            sanitized = sanitized.Substring(0, 97) + "...";
        }

        return sanitized.Trim();
    }

    // 表示用の航路文字列を整えます。
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