// src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs
// 行抽出で利用するテキスト正規化とユーティリティを分離します
// 補助関数をまとめ、他のパーシャルから再利用しやすくするために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using InteropGenerator.Runtime;
using ValueType = FFXIVClientStructs.FFXIV.Component.GUI.ValueType;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>
/// Feature toggles for UI fallback filtering behaviour.
/// </summary>
internal readonly record struct UiFilterToggles(bool StrictFilter, bool TerritoryGate, int MinimumScore)
{
    public static UiFilterToggles Load()
    {
        static bool ReadBoolean(string key, bool fallback)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
            {
                return fallback;
            }

            return value.Equals("1", StringComparison.OrdinalIgnoreCase)
                || value.Equals("true", StringComparison.OrdinalIgnoreCase)
                || value.Equals("on", StringComparison.OrdinalIgnoreCase);
        }

        static int ReadInt32(string key, int fallback)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
            {
                return fallback;
            }

            return parsed;
        }

        var strict = ReadBoolean("XIVSR_UI_STRICT_FILTER", true);
        var territory = ReadBoolean("XIVSR_UI_TERRITORY_GATE", true);
        var minimumScore = ReadInt32("XIVSR_UI_MIN_SCORE", 3);
        return new UiFilterToggles(strict, territory, minimumScore);
    }
}
/// <summary>
/// Partial class helpers that normalise row texts and extract header information.
/// </summary>
public sealed unsafe partial class DalamudUiSubmarineSnapshotSource
{
    private const int MaxCandidateCount = 4;
    private static readonly UiFilterToggles FilterToggles = UiFilterToggles.Load();

    private static readonly string[] NoisePrefixes =
    {
        "潜水艦を選択してください",
        "探索機体数",
        "保有燃料数",
        "Cancel",
    };

    private static readonly Regex[] NoisePatterns = Array.Empty<Regex>();

    private static readonly string[] NegativeRowKeywords =
    {
        "retainer",
        "リテイナー",
        "ベンチャー",
        "収集品",
        "返却",
        "return",
        "venture",
        "delivery",
        "supply",
        "squadron",
    };

    private static readonly string[] HeaderPositiveKeywords =
    {
        "submersible",
        "潜水艦",
        "sous-marin",
        "untersee",
        "submarino",
    };

    private static readonly string[] RemainingTimeKeywords =
    {
        "remaining",
        "残り時間",
        "剩余时间",
    };

    private static List<string> NormalizeRowTexts(List<string> texts)
    {
        var normalized = new List<string>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var text in texts)
        {
            foreach (var segment in SplitSegments(text))
            {
                var value = FilterNoise(segment);
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

        if (FilterToggles.StrictFilter && ContainsNegativeKeyword(value))
        {
            continue;
        }

                if (seen.Add(value))
                {
                    normalized.Add(value);
                }
            }
        }

        return normalized;
    }

    private static IEnumerable<string> SplitSegments(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            yield break;
        }

        var collapsed = text.Replace('\n', ' ').Trim();
        if (collapsed.Length == 0)
        {
            yield break;
        }

        var baseText = collapsed;
        while (true)
        {
            var bracketIndex = baseText.IndexOf('[');
            if (bracketIndex < 0)
            {
                break;
            }

            var bracketEnd = baseText.IndexOf(']', bracketIndex + 1);
            if (bracketEnd > bracketIndex)
            {
                var bracketContent = baseText[(bracketIndex + 1)..bracketEnd].Trim();
                if (!string.IsNullOrEmpty(bracketContent))
                {
                    yield return bracketContent;
                }

                var before = baseText[..bracketIndex];
                var after = bracketEnd + 1 < baseText.Length ? baseText[(bracketEnd + 1)..] : string.Empty;
                baseText = string.Concat(before, " ", after).Trim();
                continue;
            }

            break;
        }

        if (string.IsNullOrEmpty(baseText))
        {
            yield break;
        }

        var separators = new[] { '|', '/' };
        var tokens = baseText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        var emitted = false;
        foreach (var token in tokens)
        {
            var trimmed = token.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                emitted = true;
                yield return trimmed;
            }
        }

        if (!emitted)
        {
            yield return baseText;
        }
    }

    private static unsafe string DecodeHeader(AddonSelectString* addon)
    {
        try
        {
            var values = addon->AtkValuesSpan;
            if (values.Length > 2)
            {
                return DecodeValueAsText(values[2]);
            }
        }
        catch
        {
            // Header decoding failures are non-fatal.
        }

        return string.Empty;
    }

    private static string DecodeValueAsText(AtkValue value)
    {
        var type = value.Type & ValueType.TypeMask;
        return type switch
        {
            ValueType.String => value.String.ToString(),
            ValueType.ManagedString => value.String.ToString(),
            ValueType.String8 => value.String.ToString(),
            _ => string.Empty,
        };
    }

    private static string DecodeEntryText(CStringPointer entry)
    {
        if (!entry.HasValue)
        {
            return string.Empty;
        }

        var span = entry.AsSpan();
        if (span.IsEmpty)
        {
            return string.Empty;
        }

        try
        {
            var bytes = new byte[span.Length];
            span.CopyTo(bytes);
            var seString = SeString.Parse(bytes);
            var builder = new StringBuilder();
            foreach (var payload in seString.Payloads)
            {
                if (payload is TextPayload textPayload)
                {
                    builder.Append(textPayload.Text);
                }
            }

            var text = builder.Length > 0 ? builder.ToString() : Encoding.UTF8.GetString(bytes);
            return text.Replace('\n', ' ').Trim();
        }
        catch
        {
            var fallback = Encoding.UTF8.GetString(entry.AsSpan());
            return fallback.Replace('\n', ' ').Trim();
        }
    }

    private List<RowInfo> FilterRowCandidates(string header, PopupMenu* menu, List<RowInfo> rows)
    {
        if (!FilterToggles.StrictFilter)
        {
            return Truncate(rows, MaxCandidateCount);
        }

        if (FilterToggles.TerritoryGate && !IsWorkshopTerritoryActive())
        {
            var now = DateTime.UtcNow;
            if (now - this.lastTerritoryGateLogAtUtc >= TimeSpan.FromSeconds(30))
            {
                this.log.Log(LogLevel.Trace, "[UI Inspector] Workshop territory gate prevented UI fallback snapshot.");
                this.lastTerritoryGateLogAtUtc = now;
            }

            return new List<RowInfo>();
        }

        if (!MatchesStructuralPattern(menu))
        {
            this.log.Log(LogLevel.Trace, "[UI Inspector] PopupMenu structure mismatched submarine expectations.");
            return new List<RowInfo>();
        }

        if (!string.IsNullOrWhiteSpace(header) && !HeaderContainsKeyword(header))
        {
            this.log.Log(LogLevel.Trace, "[UI Inspector] Header keyword check failed.");
            return new List<RowInfo>();
        }

        var accepted = new List<RowCandidate>();
        var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var row in rows)
        {
            var candidate = this.TryCreateCandidate(row);
            if (candidate is null)
            {
                continue;
            }

            var value = candidate.Value;
            if (!seenKeys.Add(value.Key))
            {
                this.log.Log(LogLevel.Trace, "[UI Inspector] Dropped duplicate submarine candidate '" + value.Key + "'.");
                continue;
            }

            accepted.Add(value);
        }

        if (accepted.Count == 0)
        {
            this.log.Log(LogLevel.Trace, "[UI Inspector] No UI candidates passed scoring thresholds.");
            return new List<RowInfo>();
        }

        accepted.Sort(static (left, right) =>
        {
            var comparison = right.Score.CompareTo(left.Score);
            if (comparison != 0)
            {
                return comparison;
            }

            comparison = right.HasRoute.CompareTo(left.HasRoute);
            if (comparison != 0)
            {
                return comparison;
            }

            comparison = right.HasEtaOrStatus.CompareTo(left.HasEtaOrStatus);
            if (comparison != 0)
            {
                return comparison;
            }

            return left.Row.NodeId.CompareTo(right.Row.NodeId);
        });

        if (accepted.Count > MaxCandidateCount)
        {
            this.log.Log(LogLevel.Trace, "[UI Inspector] Truncating UI candidates from " + accepted.Count + " to " + MaxCandidateCount + ".");
            accepted.RemoveRange(MaxCandidateCount, accepted.Count - MaxCandidateCount);
        }

        var result = new List<RowInfo>(accepted.Count);
        foreach (var candidate in accepted)
        {
            result.Add(candidate.Row);
        }

        return result;
    }

    private RowCandidate? TryCreateCandidate(RowInfo row)
    {
        if (row.Texts.Count == 0)
        {
            return null;
        }

        var score = ComputeRowConfidence(row);
        if (FilterToggles.StrictFilter && score < FilterToggles.MinimumScore)
        {
            this.log.Log(LogLevel.Trace, "[UI Inspector] Row score below threshold score=" + score + " threshold=" + FilterToggles.MinimumScore + " texts=" + string.Join(" | ", row.Texts));
            return null;
        }

        var routeHint = FindRouteHint(row.Texts);
        var etaHint = FindEtaHint(row.Texts);
        var statusHint = FindStatusHint(row.Texts);

        var hasRoute = routeHint is not null;
        var hasEta = etaHint is not null || statusHint is not null;
        var name = this.ExtractName(row.Texts) ?? string.Empty;
        var route = routeHint ?? string.Empty;
        var eta = etaHint ?? string.Empty;
        var key = (name + "|" + route + "|" + eta).ToLowerInvariant();

        return new RowCandidate(row, score, hasRoute, hasEta, key);
    }

    private static List<RowInfo> Truncate(List<RowInfo> rows, int max)
    {
        if (rows.Count <= max)
        {
            return rows;
        }

        return new List<RowInfo>(rows.GetRange(0, max));
    }

    private static bool MatchesStructuralPattern(PopupMenu* menu)
    {
        if (menu == null)
        {
            return false;
        }

        if (menu->EntryCount <= 0 || menu->EntryCount > 10)
        {
            return false;
        }

        if (menu->List == null || menu->List->ListLength <= 0)
        {
            return false;
        }

        return menu->EntryNames != null;
    }

    private static bool HeaderContainsKeyword(string header)
    {
        foreach (var keyword in HeaderPositiveKeywords)
        {
            if (header.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsWorkshopTerritoryActive()
    {
        try
        {
            var manager = HousingManager.Instance();
            return manager != null && manager->WorkshopTerritory != null;
        }
        catch
        {
            return false;
        }
    }

    private static int ComputeRowConfidence(RowInfo row)
    {
        if (row.Texts.Count == 0)
        {
            return 0;
        }

        var score = 0;
        var hasRoute = false;
        var hasEta = false;
        var hasStatus = false;
        var hasRank = false;
        var hasNegative = false;
        var hasRemaining = false;

        foreach (var text in row.Texts)
        {
            if (!hasRoute && LooksLikeRoute(text))
            {
                hasRoute = true;
                score += 2;
            }

            if (!hasEta && LooksLikeEta(text))
            {
                hasEta = true;
                score += 2;
            }

            if (!hasRemaining && ContainsAnyKeyword(text, RemainingTimeKeywords))
            {
                hasEta = true;
                hasRemaining = true;
                score += 2;
            }

            if (!hasStatus && IsStatusText(text))
            {
                hasStatus = true;
                score += 2;
            }

            if (!hasRank && LooksLikeRank(text))
            {
                hasRank = true;
                score += 1;
            }

            if (!hasNegative && ContainsNegativeKeyword(text))
            {
                hasNegative = true;
                score -= 3;
            }
        }

        // Check if we can extract a valid submarine name
        bool hasValidName = false;
        foreach (var text in row.Texts)
        {
            if (ExtractNameCandidate(text) is { Length: > 0 })
            {
                hasValidName = true;
                break;
            }
        }

        if (hasValidName)
        {
            score += 1;
        }

        if (!hasRoute && !(hasStatus && !hasEta))
        {
            score -= 1;
        }

        if (!hasEta && !hasStatus)
        {
            score -= 1;
        }

        return score;
    }

    public static int ComputeConfidenceScoreForTest(IEnumerable<string> texts)
    {
        var list = new List<string>();
        foreach (var text in texts)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                list.Add(text);
            }
        }

        var row = new RowInfo(0, list);
        return ComputeRowConfidence(row);
    }

    private readonly record struct RowCandidate(RowInfo Row, int Score, bool HasRoute, bool HasEtaOrStatus, string Key);

    private static string FilterNoise(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var trimmed = text.Trim();

        foreach (var noise in NoisePrefixes)
        {
            if (trimmed.StartsWith(noise, StringComparison.Ordinal))
            {
                return string.Empty;
            }
        }

        foreach (var pattern in NoisePatterns)
        {
            if (pattern.IsMatch(trimmed))
            {
                return string.Empty;
            }
        }

        return trimmed.Replace('\n', ' ');
    }

    private static bool ContainsNegativeKeyword(string text)
    {
        foreach (var keyword in NegativeRowKeywords)
        {
            if (text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsAnyKeyword(string text, IReadOnlyList<string> keywords)
    {
        foreach (var keyword in keywords)
        {
            if (text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    private readonly record struct RowInfo(uint NodeId, List<string> Texts);
}
