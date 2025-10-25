namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Collections.Generic;
using System.Numerics;
using XIVSubmarinesRewrite.Application.Notifications;

/// <summary>
/// リトライツールチップのフォーマッター
/// 通知のリトライ情報をツールチップ用に整形する
/// </summary>
public static class RetryTooltipFormatter
{
    /// <summary>
    /// ツールチップ内容を生成
    /// </summary>
    /// <param name="workItem">通知ワークアイテムのスナップショット</param>
    /// <param name="now">現在時刻</param>
    /// <returns>ツールチップ内容</returns>
    public static TooltipContent Format(
        NotificationWorkItemSnapshot workItem,
        DateTime now)
    {
        var lines = new List<TooltipLine>(capacity: 8);

        // ヘッダー
        AddHeader(lines, workItem);

        // 区切り線
        lines.Add(TooltipLine.Separator);

        // 試行回数
        AddAttemptCount(lines, workItem);

        // 最終試行時刻
        if (workItem.LastAttemptAtUtc.HasValue)
        {
            AddLastAttemptTime(lines, workItem.LastAttemptAtUtc.Value);
        }

        // 次回試行
        if (workItem.NextAttemptAtUtc < DateTime.MaxValue && workItem.State != NotificationDeliveryState.DeadLetter)
        {
            AddNextAttempt(lines, workItem.NextAttemptAtUtc, now);
        }

        // エラー情報（デッドレターの場合）
        if (workItem.State == NotificationDeliveryState.DeadLetter && !string.IsNullOrEmpty(workItem.LastError))
        {
            lines.Add(TooltipLine.Separator);
            AddErrorInfo(lines, workItem.LastError);
        }

        return new TooltipContent { Lines = lines };
    }

    /// <summary>
    /// ヘッダー行を追加
    /// </summary>
    private static void AddHeader(List<TooltipLine> lines, NotificationWorkItemSnapshot workItem)
    {
        var headerText = workItem.State == NotificationDeliveryState.DeadLetter
            ? "リトライ情報 (デッドレター)"
            : "リトライ情報";

        var headerColor = workItem.State == NotificationDeliveryState.DeadLetter
            ? UiTheme.ErrorText
            : (Vector4?)null;

        lines.Add(new TooltipLine
        {
            Text = headerText,
            Color = headerColor
        });
    }

    /// <summary>
    /// 試行回数行を追加
    /// </summary>
    private static void AddAttemptCount(List<TooltipLine> lines, NotificationWorkItemSnapshot workItem)
    {
        var attemptText = FormatAttemptCount(workItem);
        lines.Add(new TooltipLine { Text = attemptText });
    }

    /// <summary>
    /// 最終試行時刻行を追加
    /// </summary>
    private static void AddLastAttemptTime(List<TooltipLine> lines, DateTime lastAttemptTime)
    {
        var lastAttemptText = $"最終試行: {lastAttemptTime:yyyy-MM-dd HH:mm:ss}";
        lines.Add(new TooltipLine { Text = lastAttemptText });
    }

    /// <summary>
    /// 次回試行行を追加
    /// </summary>
    private static void AddNextAttempt(List<TooltipLine> lines, DateTime nextAttempt, DateTime now)
    {
        var nextAttemptText = FormatNextAttempt(nextAttempt, now);
        lines.Add(new TooltipLine { Text = nextAttemptText });
    }

    /// <summary>
    /// エラー情報行を追加
    /// </summary>
    private static void AddErrorInfo(List<TooltipLine> lines, string error)
    {
        // エラーメッセージは最大200文字に制限
        var truncatedError = error.Length > 200
            ? error.Substring(0, 197) + "..."
            : error;

        lines.Add(new TooltipLine
        {
            Text = $"最終エラー: {truncatedError}",
            Color = UiTheme.ErrorText
        });
    }

    /// <summary>
    /// 試行回数のフォーマット
    /// </summary>
    private static string FormatAttemptCount(NotificationWorkItemSnapshot workItem)
    {
        if (workItem.AttemptCount == 0)
        {
            return "試行履歴なし";
        }

        if (workItem.State == NotificationDeliveryState.DeadLetter)
        {
            return $"試行回数: {workItem.AttemptCount}回 (上限到達)";
        }

        return $"試行回数: {workItem.AttemptCount}回";
    }

    /// <summary>
    /// 次回試行時刻のフォーマット
    /// </summary>
    private static string FormatNextAttempt(DateTime nextAttempt, DateTime now)
    {
        var timeSpan = nextAttempt - now;
        var remainingSeconds = (int)Math.Max(0, timeSpan.TotalSeconds);

        return $"次回試行: {nextAttempt:yyyy-MM-dd HH:mm:ss} ({remainingSeconds}秒後)";
    }
}

/// <summary>
/// ツールチップの内容
/// </summary>
public record TooltipContent
{
    /// <summary>
    /// ツールチップの行リスト
    /// </summary>
    public required List<TooltipLine> Lines { get; init; }
}

/// <summary>
/// ツールチップの1行
/// </summary>
public record TooltipLine
{
    /// <summary>
    /// 表示テキスト
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// テキスト色（nullの場合はデフォルト色）
    /// </summary>
    public Vector4? Color { get; init; }

    /// <summary>
    /// 区切り線かどうか
    /// </summary>
    public bool IsSeparator { get; init; }

    /// <summary>
    /// 区切り線インスタンス
    /// </summary>
    public static readonly TooltipLine Separator = new() { IsSeparator = true };
}

