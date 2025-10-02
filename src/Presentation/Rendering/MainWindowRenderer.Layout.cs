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
        const float toolbarHeight = 60f;
        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.ToolbarBg);
        ImGui.PushStyleColor(ImGuiCol.Border, UiTheme.ToolbarBorder);
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(14f, 10f));
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(16f, 8f));

        if (ImGui.BeginChild("##main_toolbar", new Vector2(0f, toolbarHeight), true, ImGuiWindowFlags.AlwaysUseWindowPadding | ImGuiWindowFlags.NoScrollbar))
        {
            const ImGuiTableFlags flags = ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.PadOuterX | ImGuiTableFlags.NoSavedSettings | ImGuiTableFlags.NoBordersInBody;
            if (ImGui.BeginTable("toolbar_table", 3, flags))
            {
                ImGui.TableSetupColumn("brand", ImGuiTableColumnFlags.WidthStretch, 1.2f);
                ImGui.TableSetupColumn("metrics", ImGuiTableColumnFlags.WidthStretch, 0.9f);
                ImGui.TableSetupColumn("actions", ImGuiTableColumnFlags.WidthStretch, 0.6f);
                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(10f, 0f));
                ImGui.AlignTextToFramePadding();
                ImGui.TextColored(UiTheme.AccentPrimary, "⛵");
                ImGui.SameLine();
                ImGui.TextColored(UiTheme.ToolbarText, "XIV Submarines");
                ImGui.SameLine();
                ImGui.TextColored(UiTheme.ToolbarMuted, this.versionLabel);
                ImGui.PopStyleVar();

                ImGui.TableSetColumnIndex(1);
                var toolbarContrast = UiTheme.ContrastRatio(UiTheme.ToolbarText, UiTheme.ToolbarBg);
                var windowContrast = UiTheme.ContrastRatio(UiTheme.PrimaryText, UiTheme.WindowBg);
                ImGui.TextColored(UiTheme.ToolbarMuted, $"Toolbar 1:{toolbarContrast:F2}");
                ImGui.TextColored(UiTheme.ToolbarMuted, $"Window 1:{windowContrast:F2}");
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(6f, 6f));
                var swatchFlags = ImGuiColorEditFlags.NoTooltip | ImGuiColorEditFlags.NoDragDrop | ImGuiColorEditFlags.NoPicker;
                var swatchSize = new Vector2(18f, 18f);
                ImGui.ColorButton("##toolbar_bg_swatch", UiTheme.ToolbarBg, swatchFlags, swatchSize);
                ImGui.SameLine();
                ImGui.ColorButton("##toolbar_text_swatch", UiTheme.ToolbarText, swatchFlags, swatchSize);
                ImGui.SameLine();
                ImGui.ColorButton("##window_bg_swatch", UiTheme.WindowBg, swatchFlags, swatchSize);
                ImGui.SameLine();
                ImGui.ColorButton("##primary_text_swatch", UiTheme.PrimaryText, swatchFlags, swatchSize);
                ImGui.PopStyleVar();

                ImGui.TableSetColumnIndex(2);
                var buttonLabel = this.showDeveloperTools ? "DEV • ON" : "DEV • OFF";
                var buttonWidth = 120f;
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
                ImGui.SameLine();
                ImGui.TextColored(UiTheme.ToolbarMuted, this.showDeveloperTools ? "開発タブ表示中" : "一般表示");

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
}

