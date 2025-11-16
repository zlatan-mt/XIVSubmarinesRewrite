// src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs
// UI 行データを潜水艦スナップショットに変換する主要ロジックを保持します
// サブマリン生成の流れを集中管理し、補助的な解析処理は別パーシャルへ委譲するために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.Helpers.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>
/// Partial class slice that creates <see cref="Submarine"/> instances from collected row texts.
/// </summary>
public sealed unsafe partial class DalamudUiSubmarineSnapshotSource
{
    private const int AgentSelectedPointsOffset = 0x50;
    private const int ActivePointerIndex = 4;
    private const int MaxRoutePoints = 5;

    private readonly Dictionary<SubmarineId, string> cachedRoutes = new ();

    private Submarine? BuildSubmarineFromRow(RowInfo row)
    {
        if (row.Texts.Count == 0)
        {
            return null;
        }

        var name = this.ExtractName(row.Texts) ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Submarine-" + row.NodeId.ToString(CultureInfo.InvariantCulture);
        }

        // 名前を使用して実際のSlot番号を解決
        var submarineId = this.CreateSubmarineId(row.NodeId, name);
        var route = FindRouteHint(row.Texts)
            ?? this.TryReadRouteFromAgent(submarineId)
            ?? this.TryReadRouteFromMemory(submarineId)
            ?? string.Empty;
        var statusText = FindStatusHint(row.Texts);
        var etaText = FindEtaHint(row.Texts) ?? statusText;
        var status = ParseStatus(statusText);
        var arrival = ParseEta(etaText);

        if (arrival.HasValue && status == VoyageStatus.Unknown)
        {
            status = arrival.Value <= DateTime.UtcNow ? VoyageStatus.Completed : VoyageStatus.Underway;
        }

        if (!arrival.HasValue && status == VoyageStatus.Completed)
        {
            arrival = DateTime.UtcNow;
        }

        var voyageGuid = ComputeVoyageGuid(submarineId, arrival);
        var departure = arrival ?? DateTime.UtcNow;
        var voyage = new Voyage(VoyageId.Create(submarineId, voyageGuid), route, departure, arrival, status);

        return new Submarine(submarineId, name, string.Empty, new[] { voyage });
    }

    private SubmarineId CreateSubmarineId(uint nodeId, string? submarineName = null)
    {
        var cid = this.clientState.LocalContentId;
        byte slot = SubmarineId.PendingSlot;

        // まず名前から実際のSlot番号を解決を試みる
        if (!string.IsNullOrWhiteSpace(submarineName))
        {
            var actualSlot = this.TryResolveActualSlotFromName(submarineName);
            if (actualSlot.HasValue)
            {
                slot = actualSlot.Value;
            }
        }

        // 解決できない場合はNodeIdをフォールバック（既存の動作）
        if (slot == SubmarineId.PendingSlot)
        {
            slot = nodeId <= byte.MaxValue ? (byte)nodeId : SubmarineId.PendingSlot;
            if (slot >= 4)
            {
                slot = SubmarineId.PendingSlot;
            }
        }

        if (cid == 0)
        {
            return new SubmarineId(0, slot);
        }

        return new SubmarineId(cid, slot);
    }

    private unsafe byte? TryResolveActualSlotFromName(string submarineName)
    {
        var manager = HousingManager.Instance();
        if (manager == null)
        {
            return null;
        }

        var territory = manager->WorkshopTerritory;
        if (territory == null)
        {
            return null;
        }

        var dataPointers = territory->Submersible.DataPointers;
        byte? foundSlot = null;

        for (byte slot = 0; slot < 4 && slot < dataPointers.Length; slot++)
        {
            var subData = dataPointers[slot].Value;
            if (subData == null)
            {
                continue;
            }

            var name = this.ReadSubmarineName(subData);
            if (string.Equals(name, submarineName, StringComparison.OrdinalIgnoreCase))
            {
                if (foundSlot.HasValue)
                {
                    this.log.Log(LogLevel.Warning,
                        $"[UI Inspector] Duplicate submarine name '{submarineName}' found at slots {foundSlot.Value} and {slot}. Using first occurrence (Slot {foundSlot.Value}).");
                    // 重複が検出された場合、最初に見つかったSlotを使用
                    return foundSlot.Value;
                }

                foundSlot = slot;
            }
        }

        if (foundSlot.HasValue)
        {
            this.log.Log(LogLevel.Debug, $"[UI Inspector] Resolved submarine '{submarineName}' to actual Slot {foundSlot.Value}");
        }
        else
        {
            this.log.Log(LogLevel.Debug, $"[UI Inspector] Could not resolve submarine '{submarineName}' to actual Slot (not found in memory)");
        }

        return foundSlot;
    }

    private unsafe string ReadSubmarineName(HousingWorkshopSubmersibleSubData* vessel)
    {
        if (vessel == null)
        {
            return string.Empty;
        }

        const int NameOffset = 0x22;
        var span = new ReadOnlySpan<byte>((byte*)vessel + NameOffset, 20);
        var terminator = span.IndexOf((byte)0);
        if (terminator >= 0)
        {
            span = span[..terminator];
        }

        return span.IsEmpty ? string.Empty : Encoding.UTF8.GetString(span);
    }

    private unsafe string? TryReadRouteFromMemory(SubmarineId submarineId)
    {
        if (submarineId.Slot == SubmarineId.PendingSlot)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var manager = HousingManager.Instance();
        if (manager == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var territory = manager->WorkshopTerritory;
        if (territory == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var dataPointers = territory->Submersible.DataPointers;
        var index = submarineId.Slot;
        if (index >= dataPointers.Length)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var subData = dataPointers[index].Value;
        if (subData == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var segments = new List<string>();
        var points = subData->CurrentExplorationPoints.ToArray();
        foreach (var point in points)
        {
            if (point != 0)
            {
                segments.Add(point.ToString(CultureInfo.InvariantCulture));
            }
        }

        var gathered = subData->GatheredData;
        for (var i = 0; i < gathered.Length; i++)
        {
            var point = gathered[i].Point;
            if (point != 0)
            {
                segments.Add(point.ToString(CultureInfo.InvariantCulture));
            }
        }

        if (segments.Count == 0)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var routeId = string.Join('-', segments);
        this.cachedRoutes[submarineId] = routeId;
        return routeId;
    }

    private unsafe string? TryReadRouteFromAgent(SubmarineId submarineId)
    {
        if (submarineId.CharacterId == 0)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var module = AgentModule.Instance();
        if (module == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var agent = module->GetAgentByInternalId(AgentId.SubmersibleExploration);
        if (agent == null || !agent->IsAgentActive())
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var manager = HousingManager.Instance();
        if (manager == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var territory = manager->WorkshopTerritory;
        if (territory == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var dataPointers = territory->Submersible.DataPointers;
        if (dataPointers.Length <= ActivePointerIndex)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var activePointer = dataPointers[ActivePointerIndex].Value;
        if (activePointer == null)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        byte activeSlot = SubmarineId.PendingSlot;
        for (byte slot = 0; slot < 4; slot++)
        {
            if (dataPointers[slot].Value == activePointer)
            {
                activeSlot = slot;
                break;
            }
        }

        if (activeSlot == SubmarineId.PendingSlot || activeSlot != submarineId.Slot)
        {
            return this.TryGetCachedRoute(new SubmarineId(submarineId.CharacterId, activeSlot));
        }

        var exploration = (AgentSubmersibleExploration*)agent;
        var count = Math.Min(exploration->SelectedPointsCount, (byte)MaxRoutePoints);
        if (count == 0)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var segments = new List<string>(count);
        var pointsPtr = (byte*)exploration + AgentSelectedPointsOffset;
        for (var i = 0; i < count; i++)
        {
            var point = pointsPtr[i];
            if (point != 0)
            {
                segments.Add(point.ToString(CultureInfo.InvariantCulture));
            }
        }

        if (segments.Count == 0)
        {
            return this.TryGetCachedRoute(submarineId);
        }

        var routeId = string.Join('-', segments);
        this.cachedRoutes[submarineId] = routeId;
        this.log.Log(LogLevel.Debug, "[UI Inspector] Agent route slot=" + submarineId.Slot + " => " + routeId);
        return routeId;
    }

    private string? TryGetCachedRoute(SubmarineId submarineId)
        => this.cachedRoutes.TryGetValue(submarineId, out var cached) ? cached : null;

    private string? ExtractName(IReadOnlyList<string> texts)
    {
        foreach (var text in texts)
        {
            var candidate = ExtractNameCandidate(text);
            if (!string.IsNullOrWhiteSpace(candidate))
            {
                return candidate;
            }
        }

        return null;
    }

    private static string? ExtractNameCandidate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var trimmed = text.Trim();
        if (LooksLikeRoute(trimmed) || LooksLikeEta(trimmed) || LooksLikeRank(trimmed))
        {
            return null;
        }

        // ステータステキストが単語境界で区切られて存在する場合、ステータス部分を除去して名前を抽出
        // StripStatusSuffixは単語境界を考慮してステータスを除去する。
        // - 除去できた場合（non-null）: "Name 探索完了" → "Name" のように名前を返す
        // - 除去できなかった場合（null）: "MyReturn" や "探索完了" のようなケース
        //   → IsOnlyStatusTextで完全一致を確認し、純粋なステータスのみを除外
        var stripped = StripStatusSuffix(trimmed);
        if (stripped is not null)
        {
            // ステータスが除去できた場合は、その結果を使用
            trimmed = stripped;
        }
        else if (IsOnlyStatusText(trimmed))
        {
            // テキスト全体が純粋にステータスキーワードのみの場合は除外
            return null;
        }

        var bracketIndex = trimmed.IndexOf('[');
        if (bracketIndex > 0)
        {
            trimmed = trimmed[..bracketIndex].TrimEnd();
        }

        if (trimmed.Length == 0)
        {
            return null;
        }

        return trimmed;
    }

    private static string? StripStatusSuffix(string text)
    {
        // 全ステータスキーワードを結合
        var allStatusKeywords = StatusCompletedKeywords
            .Concat(StatusUnderwayKeywords)
            .Concat(StatusScheduledKeywords);

        var earliestIndex = -1;

        foreach (var keyword in allStatusKeywords)
        {
            // 単語境界を考慮：キーワードの前に空白があるか、文字列の先頭である必要がある
            var index = 0;
            while ((index = text.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                // キーワードの前が空白または先頭である場合のみ有効とみなす
                if (index == 0 || char.IsWhiteSpace(text[index - 1]))
                {
                    if (index > 0)  // 前に文字がある場合
                    {
                        if (earliestIndex < 0 || index < earliestIndex)
                        {
                            earliestIndex = index;
                        }
                    }
                    break;
                }
                index++;
            }
        }

        if (earliestIndex > 0)
        {
            return text[..earliestIndex].Trim();
        }

        return null;
    }

    private static bool IsOnlyStatusText(string text)
    {
        // テキストが純粋にステータスキーワードのみかどうかを確認
        // 空白を除去した後、いずれかのステータスキーワードと完全一致するかチェック
        var trimmed = text.Trim();
        var allStatusKeywords = StatusCompletedKeywords
            .Concat(StatusUnderwayKeywords)
            .Concat(StatusScheduledKeywords);

        foreach (var keyword in allStatusKeywords)
        {
            if (trimmed.Equals(keyword, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// テスト用のpublicラッパー。ExtractNameCandidate をテストから呼び出すために使用します。
    /// </summary>
    public static string? ExtractNameCandidateForTest(string text)
    {
        return ExtractNameCandidate(text);
    }

    private static string? FindRouteHint(IReadOnlyList<string> texts)
    {
        foreach (var text in texts)
        {
            if (LooksLikeRoute(text))
            {
                return text;
            }
        }

        return null;
    }

    private static string? FindStatusHint(IReadOnlyList<string> texts)
    {
        foreach (var text in texts)
        {
            if (IsStatusText(text))
            {
                return text;
            }
        }

        return null;
    }

    private static string? FindEtaHint(IReadOnlyList<string> texts)
    {
        foreach (var text in texts)
        {
            if (ParseEta(text) is not null)
            {
                return text;
            }
        }

        return null;
    }
}
