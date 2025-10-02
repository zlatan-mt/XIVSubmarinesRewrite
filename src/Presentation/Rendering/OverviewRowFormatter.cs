// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewRowFormatter.cs
// Overview タブに表示するテキスト表現をまとめたユーティリティです
// UI とクリップボード出力の書式を統一し、テストしやすくするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/OverviewRowFormatterTests.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Globalization;
using System.Text;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Presentation.ViewModels;

/// <summary>Provides render-ready and copy-friendly text for overview rows.</summary>
internal static class OverviewRowFormatter
{
    public static string GetDisplayName(SubmarineOverviewEntry submarine)
        => string.IsNullOrWhiteSpace(submarine.Name) ? submarine.SubmarineId.ToString() : submarine.Name;

    public static string FormatStatus(VoyageStatus status)
        => status switch
        {
            VoyageStatus.Completed => "完了",
            VoyageStatus.Underway => "航行中",
            VoyageStatus.Scheduled => "出航予定",
            _ => "不明",
        };

    public static string FormatRemaining(TimeSpan? remaining)
    {
        if (remaining is null)
        {
            return "--";
        }

        if (remaining.Value <= TimeSpan.Zero)
        {
            return "帰港済み";
        }

        var span = remaining.Value;
        if (span.TotalHours >= 1)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:D2}h {1:D2}m", (int)span.TotalHours, span.Minutes);
        }

        return string.Format(CultureInfo.InvariantCulture, "{0:D2}m", Math.Max(0, span.Minutes));
    }

    public static string FormatArrival(DateTime? arrival)
    {
        if (arrival is null)
        {
            return "--";
        }

        return arrival.Value.ToLocalTime().ToString("M/d(ddd) HH:mm", CultureInfo.InvariantCulture);
    }

    public static string FormatRemainingCompact(TimeSpan? remaining)
    {
        if (remaining is null)
        {
            return "--";
        }

        if (remaining.Value <= TimeSpan.Zero)
        {
            return "帰港";
        }

        var span = remaining.Value;
        if (span.TotalHours >= 1)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}h{1:D2}", (int)span.TotalHours, span.Minutes);
        }

        return string.Format(CultureInfo.InvariantCulture, "{0}m", Math.Max(1, span.Minutes));
    }

    public static string FormatArrivalCompact(DateTime? arrival)
    {
        if (arrival is null)
        {
            return "--";
        }

        return arrival.Value.ToLocalTime().ToString("M/d(ddd) HH:mm", CultureInfo.InvariantCulture);
    }

    public static string BuildWrappedRouteLabel(string routeLabel)
    {
        if (string.IsNullOrWhiteSpace(routeLabel))
        {
            return routeLabel;
        }

        var builder = new StringBuilder(routeLabel.Length + 8);
        foreach (var ch in routeLabel)
        {
            builder.Append(ch);
            if (ch is '>' or '/' or '\u2192')
            {
                builder.Append('\u200B');
            }
        }

        return builder.ToString();
    }

    public static string BuildCopyLine(SubmarineOverviewEntry submarine, string routeLabel)
    {
        var safeRoute = string.IsNullOrWhiteSpace(routeLabel) ? "--" : routeLabel;
        safeRoute = StripZeroWidth(safeRoute);

        return string.Join(" | ", new[]
        {
            GetDisplayName(submarine),
            FormatStatus(submarine.Status),
            FormatRemaining(submarine.Remaining),
            FormatArrival(submarine.Arrival),
            safeRoute,
        });
    }

    public static string StripZeroWidth(string value)
        => string.IsNullOrEmpty(value) ? value : value.Replace("\u200B", string.Empty, StringComparison.Ordinal);
}
