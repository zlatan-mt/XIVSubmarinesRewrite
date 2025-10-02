// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.Layout.cs
// MainWindowRenderer のツールバーとDEVサマリ描画を担当します
// 部分クラスとして分離し、300行制限を守るため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiCol = Dalamud.Bindings.ImGui.ImGuiCol;
using ImGuiColorEditFlags = Dalamud.Bindings.ImGui.ImGuiColorEditFlags;
using ImGuiStyleVar = Dalamud.Bindings.ImGui.ImGuiStyleVar;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiWindowFlags = Dalamud.Bindings.ImGui.ImGuiWindowFlags;
using XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>Toolbar and DEV summary rendering methods.</summary>
public sealed partial class MainWindowRenderer
{
    private void DrawToolbar()
    {
        const float toolbarHeight = 72f; // Increased height for better spacing
        var uiScale = ImGui.GetIO().FontGlobalScale;
        var scaledPadding = 14f * uiScale;
        var scaledItemSpacing = 16f * uiScale;
        
        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.ToolbarBg);
        ImGui.PushStyleColor(ImGuiCol.Border, UiTheme.ToolbarBorder);
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(scaledPadding, 12f * uiScale));
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(scaledItemSpacing, 8f * uiScale));

        if (ImGui.BeginChild("##main_toolbar", new Vector2(0f, toolbarHeight), true, ImGuiWindowFlags.AlwaysUseWindowPadding | ImGuiWindowFlags.NoScrollbar))
        {
            const ImGuiTableFlags flags = ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.PadOuterX | ImGuiTableFlags.NoSavedSettings | ImGuiTableFlags.NoBordersInBody;
            if (ImGui.BeginTable("toolbar_table", 3, flags))
            {
                ImGui.TableSetupColumn("brand", ImGuiTableColumnFlags.WidthStretch, 1.4f);
                ImGui.TableSetupColumn("metrics", ImGuiTableColumnFlags.WidthStretch, 1.0f);
                ImGui.TableSetupColumn("actions", ImGuiTableColumnFlags.WidthStretch, 0.8f);
                ImGui.TableNextRow();

                // Brand section with improved layout
                ImGui.TableSetColumnIndex(0);
                this.DrawBrandSection();

                // Metrics section with better organization
                ImGui.TableSetColumnIndex(1);
                this.DrawMetricsSection();

                // Actions section
                ImGui.TableSetColumnIndex(2);
                this.DrawActionsSection();

                ImGui.EndTable();
            }
        }

        ImGui.EndChild();
        ImGui.PopStyleVar(2);
        ImGui.PopStyleColor(2);
    }

    private void DrawDeveloperSummary()
    {
        if (!this.showDeveloperTools)
        {
            return;
        }

        var history = this.preferences.DevHistory;
        if (history is null)
        {
            return;
        }

        var hasForce = history.LastForceNotifyToggleUtc.HasValue;
        var hasManual = history.LastManualTriggerUtc.HasValue;
        if (!hasForce && !hasManual)
        {
            return;
        }

        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.PanelBg);
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12f, 8f));
        if (ImGui.BeginChild("dev_summary", new Vector2(0f, 58f), false, ImGuiWindowFlags.NoScrollbar))
        {
            var forceTime = history.LastForceNotifyToggleUtc?.ToString("HH:mm:ss") ?? "未記録";
            var forceLabel = hasForce
                ? $"ForceNotify: {(history.ForceNotifyEnabled ? "ON" : "OFF")} / {forceTime}"
                : "ForceNotify: 未記録";
            var summary = history.LastManualTriggerSummary ?? "手動送信ログはまだありません。";
            var manualTime = history.LastManualTriggerUtc?.ToString("HH:mm:ss") ?? "未記録";
            var manualLabel = history.LastManualTriggerUtc.HasValue
                ? $"最終送信 {manualTime}"
                : "最終送信: 未記録";

            ImGui.TextColored(UiTheme.AccentPrimary, manualLabel);
            ImGui.TextColored(UiTheme.MutedText, summary);
            ImGui.TextColored(UiTheme.MutedText, forceLabel);
        }
        ImGui.EndChild();
        ImGui.PopStyleVar();
        ImGui.PopStyleColor();
    }

    private void DrawBrandSection()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(12f, 0f));
        ImGui.AlignTextToFramePadding();
        
        // Icon with proper alignment
        ImGui.TextColored(UiTheme.AccentPrimary, "⛵");
        ImGui.SameLine();
        
        // Product name
        ImGui.TextColored(UiTheme.ToolbarText, "XIV Submarines");
        ImGui.SameLine();
        
        // Version with truncation if needed
        var versionText = this.TruncateForWidth(this.versionLabel, 120f);
        if (versionText != this.versionLabel)
        {
            ImGui.TextColored(UiTheme.ToolbarMuted, versionText);
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip($"Version: {this.versionLabel}");
            }
        }
        else
        {
            ImGui.TextColored(UiTheme.ToolbarMuted, versionText);
        }
        
        ImGui.PopStyleVar();
    }

    private void DrawMetricsSection()
    {
        // Move metrics to second row to avoid crowding
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(8f, 4f));
        
        var toolbarContrast = UiTheme.ContrastRatio(UiTheme.ToolbarText, UiTheme.ToolbarBg);
        var windowContrast = UiTheme.ContrastRatio(UiTheme.PrimaryText, UiTheme.WindowBg);
        
        // Contrast ratios on first line
        ImGui.TextColored(UiTheme.ToolbarMuted, $"T:{toolbarContrast:F1}");
        ImGui.SameLine();
        ImGui.TextColored(UiTheme.ToolbarMuted, $"W:{windowContrast:F1}");
        
        // Color swatches on second line
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4f, 4f));
        var swatchFlags = ImGuiColorEditFlags.NoTooltip | ImGuiColorEditFlags.NoDragDrop | ImGuiColorEditFlags.NoPicker;
        var swatchSize = new Vector2(16f, 16f);
        
        ImGui.ColorButton("##toolbar_bg_swatch", UiTheme.ToolbarBg, swatchFlags, swatchSize);
        ImGui.SameLine();
        ImGui.ColorButton("##toolbar_text_swatch", UiTheme.ToolbarText, swatchFlags, swatchSize);
        ImGui.SameLine();
        ImGui.ColorButton("##window_bg_swatch", UiTheme.WindowBg, swatchFlags, swatchSize);
        ImGui.SameLine();
        ImGui.ColorButton("##primary_text_swatch", UiTheme.PrimaryText, swatchFlags, swatchSize);
        
        ImGui.PopStyleVar(2);
    }

    private void DrawActionsSection()
    {
        var buttonLabel = this.showDeveloperTools ? "DEV • ON" : "DEV • OFF";
        var buttonWidth = 100f;
        var cursorX = ImGui.GetCursorPosX();
        var available = ImGui.GetContentRegionAvail().X;
        ImGui.SetCursorPosX(cursorX + MathF.Max(0f, available - buttonWidth));
        
        var idleColor = this.showDeveloperTools ? UiTheme.AccentPrimary : UiTheme.ToolbarBg;
        ImGui.PushStyleColor(ImGuiCol.Button, idleColor);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, UiTheme.AccentPrimary);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, UiTheme.AccentPrimary);
        
        if (ImGui.Button(buttonLabel, new Vector2(buttonWidth, 0f)))
        {
            this.showDeveloperTools = !this.showDeveloperTools;
            this.preferences.ShowDeveloperTools = this.showDeveloperTools;
            var history = this.preferences.DevHistory ??= new UiPreferences.DevPanelHistory();
            history.LastDeveloperTabToggleUtc = DateTime.UtcNow;
            history.DeveloperToolsVisible = this.showDeveloperTools;
            this.SavePreferences();
        }

        ImGui.PopStyleColor(3);
        
        // Status text below button
        ImGui.SameLine();
        ImGui.TextColored(UiTheme.ToolbarMuted, this.showDeveloperTools ? "開発中" : "一般");
    }

    private string TruncateForWidth(string text, float maxWidth)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var textSize = ImGui.CalcTextSize(text);
        if (textSize.X <= maxWidth)
        {
            return text;
        }

        // Binary search for optimal truncation point
        var left = 0;
        var right = text.Length;
        var bestLength = 0;
        
        while (left <= right)
        {
            var mid = (left + right) / 2;
            var truncated = text.Substring(0, mid) + "...";
            var size = ImGui.CalcTextSize(truncated);
            
            if (size.X <= maxWidth)
            {
                bestLength = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return bestLength > 0 ? text.Substring(0, bestLength) + "..." : "...";
    }
}

