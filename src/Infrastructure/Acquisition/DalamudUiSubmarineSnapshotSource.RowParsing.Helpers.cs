// src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.Helpers.cs
// ステータス判定や GUID 生成など、行解析の補助処理を分離します
// 解析ヘルパーをまとめ、他のパーシャルから再利用しやすくするために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>
/// Partial class helpers for interpreting SelectString row hints and computing derived values.
/// </summary>
public sealed unsafe partial class DalamudUiSubmarineSnapshotSource
{
    private static readonly string[] StatusUnderwayKeywords =
    {
        "return",
        "underway",
        "航行",
        "出航",
        "航海中",
        "探索中",
    };

    private static readonly string[] StatusCompletedKeywords =
    {
        "complete",
        "completed",
        "ready",
        "完了",
        "帰港",
        "到着",
    };

    private static readonly string[] StatusScheduledKeywords =
    {
        "deploy",
        "scheduled",
        "準備中",
    };

    private static readonly Regex EtaSpanPattern = new("(?:(?<h>\\d+)\\s*(?:h|時間|時))?\\s*(?<m>\\d+)\\s*(?:m|分)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex EtaHourOnlyPattern = new("(?<h>\\d+)\\s*(?:h|時間|時)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex EtaClockPattern = new("(?<h>\\d{1,2})[:：](?<m>\\d{2})", RegexOptions.Compiled);
    private static readonly Regex RankPattern = new("^(?:Rank|ランク)\\s*\\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static VoyageStatus ParseStatus(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return VoyageStatus.Unknown;
        }

        if (ContainsAny(text, StatusCompletedKeywords, StringComparison.OrdinalIgnoreCase))
        {
            return VoyageStatus.Completed;
        }

        if (ContainsAny(text, StatusUnderwayKeywords, StringComparison.OrdinalIgnoreCase))
        {
            return VoyageStatus.Underway;
        }

        if (ContainsAny(text, StatusScheduledKeywords, StringComparison.OrdinalIgnoreCase))
        {
            return VoyageStatus.Scheduled;
        }

        return VoyageStatus.Unknown;
    }

    private static DateTime? ParseEta(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out var dt) ||
            DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt))
        {
            return dt.ToUniversalTime();
        }

        var spanMatch = EtaSpanPattern.Match(text);
        if (spanMatch.Success)
        {
            var hours = spanMatch.Groups["h"].Success ? int.Parse(spanMatch.Groups["h"].Value, CultureInfo.InvariantCulture) : 0;
            var minutes = spanMatch.Groups["m"].Success ? int.Parse(spanMatch.Groups["m"].Value, CultureInfo.InvariantCulture) : 0;
            return DateTime.UtcNow.AddHours(hours).AddMinutes(minutes);
        }

        var hourOnlyMatch = EtaHourOnlyPattern.Match(text);
        if (hourOnlyMatch.Success)
        {
            var hours = int.Parse(hourOnlyMatch.Groups["h"].Value, CultureInfo.InvariantCulture);
            return DateTime.UtcNow.AddHours(hours);
        }

        var clockMatch = EtaClockPattern.Match(text);
        if (clockMatch.Success)
        {
            var hours = int.Parse(clockMatch.Groups["h"].Value, CultureInfo.InvariantCulture);
            var minutes = clockMatch.Groups["m"].Success ? int.Parse(clockMatch.Groups["m"].Value, CultureInfo.InvariantCulture) : 0;
            return DateTime.UtcNow.AddHours(hours).AddMinutes(minutes);
        }

        return null;
    }

    private static Guid ComputeVoyageGuid(SubmarineId submarineId, DateTime? arrival)
    {
        var timestamp = arrival?.ToUniversalTime().Ticks ?? DateTime.UtcNow.Ticks;
        var nameBytes = Encoding.UTF8.GetBytes(submarineId.ToString());
        var length = Math.Min(nameBytes.Length, 32);
        Span<byte> buffer = stackalloc byte[sizeof(long) + length];
        BitConverter.TryWriteBytes(buffer[..sizeof(long)], timestamp);
        if (length > 0)
        {
            nameBytes.AsSpan(0, length).CopyTo(buffer[sizeof(long)..]);
        }

        var hash = MD5.HashData(buffer);
        return new Guid(hash);
    }

    private static bool LooksLikeRoute(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        return text.Contains("->", StringComparison.OrdinalIgnoreCase)
            || text.Contains("→", StringComparison.Ordinal)
            || text.Contains("航路", StringComparison.Ordinal)
            || text.Contains("route", StringComparison.OrdinalIgnoreCase);
    }

    private static bool LooksLikeEta(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        return EtaSpanPattern.IsMatch(text) || EtaHourOnlyPattern.IsMatch(text) || EtaClockPattern.IsMatch(text);
    }

    private static bool LooksLikeRank(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        return RankPattern.IsMatch(text);
    }

    private static bool IsStatusText(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        return ContainsAny(text, StatusUnderwayKeywords, StringComparison.OrdinalIgnoreCase)
            || ContainsAny(text, StatusCompletedKeywords, StringComparison.OrdinalIgnoreCase)
            || ContainsAny(text, StatusScheduledKeywords, StringComparison.OrdinalIgnoreCase);
    }

    private static bool ContainsAny(string text, IReadOnlyList<string> keywords, StringComparison comparison)
    {
        foreach (var keyword in keywords)
        {
            if (text.IndexOf(keyword, comparison) >= 0)
            {
                return true;
            }
        }

        return false;
    }
}
