// src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.cs
// Dalamud UI から SelectString の行を抽出する低レベル処理をまとめます
// 行抽出とテキスト解析を分離し、責務を明確にするために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using InteropGenerator.Runtime;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>
/// Partial class slice focused on gathering row texts from the SelectString popup menu.
/// </summary>
public sealed unsafe partial class DalamudUiSubmarineSnapshotSource
{
    private unsafe List<RowInfo> ExtractRowsFromPopupMenu(PopupMenu* menu)
    {
        var rows = new List<RowInfo>();
        if (menu == null || menu->EntryCount <= 0 || menu->EntryNames == null)
        {
            return this.TryExtractFromRenderers(menu);
        }

        this.log.Log(LogLevel.Debug, "[UI Inspector] PopupMenu entries=" + menu->EntryCount);

        for (var i = 0; i < menu->EntryCount; i++)
        {
            ref readonly CStringPointer entry = ref menu->EntryNames[i];
            var rowTexts = new List<string>();
            var text = DecodeEntryText(entry);
            if (!string.IsNullOrWhiteSpace(text))
            {
                rowTexts.Add(text);
                this.log.Log(LogLevel.Debug, "[UI Inspector] Entry[" + i + "] '" + text + "'");
            }

            var supplemental = this.TryExtractRendererRow(menu, i);
            if (supplemental.Count > 0)
            {
                rowTexts.AddRange(supplemental);
            }

            if (rowTexts.Count == 0)
            {
                this.log.Log(LogLevel.Debug, "[UI Inspector] Entry[" + i + "] <empty>");
                continue;
            }

            var normalized = NormalizeRowTexts(rowTexts);
            if (i == 0)
            {
                var now = DateTime.UtcNow;
                if (now - this.lastNormalizedRowLogAtUtc >= TimeSpan.FromSeconds(10))
                {
                    this.log.Log(LogLevel.Debug, "[UI Inspector] Normalized row[" + i + "] => " + string.Join(" | ", normalized));
                    this.lastNormalizedRowLogAtUtc = now;
                }
            }

            rows.Add(new RowInfo((uint)i, normalized));
        }

        return rows;
    }

    private unsafe List<RowInfo> TryExtractFromRenderers(PopupMenu* menu)
    {
        var rows = new List<RowInfo>();
        if (menu == null || menu->List == null)
        {
            return rows;
        }

        var list = menu->List;
        for (var i = 0; i < list->ListLength; i++)
        {
            var texts = this.TryExtractRendererRow(menu, i);
            if (texts.Count == 0)
            {
                continue;
            }

            rows.Add(new RowInfo((uint)i, NormalizeRowTexts(texts)));
        }

        return rows;
    }

    private unsafe List<string> TryExtractRendererRow(PopupMenu* menu, int index)
    {
        var result = new List<string>();
        if (menu == null || menu->List == null)
        {
            return result;
        }

        try
        {
            var renderer = menu->List->GetItemRenderer(index);
            if (renderer == null)
            {
                return result;
            }

            var uld = renderer->UldManager;
            var count = uld.NodeListCount;
            for (var j = 0; j < count; j++)
            {
                var node = uld.NodeList[j];
                if (node == null)
                {
                    continue;
                }

                if (node->Type == NodeType.Text)
                {
                    var text = ((AtkTextNode*)node)->NodeText.ToString();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        result.Add(text.Trim());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Debug, "[UI Inspector] Failed to extract renderer row.", ex);
        }

        return result;
    }
}
