// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.LayoutDebug.cs
// 通知設定ウィンドウのレイアウト診断とメトリクス収集を担当します
// Phase9-A でカード幅や閾値を計測し、レイアウト調整の根拠を残すため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SettingsLayout.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiTreeNodeFlags = Dalamud.Bindings.ImGui.ImGuiTreeNodeFlags;

public sealed partial class NotificationMonitorWindowRenderer
{
    // Shared layout constants keep card rendering and debug metrics aligned.
    private const float ChannelCardMinWidth = 280f;
    private const float ChannelCardHeight = 132f;

    // Latest layout snapshot helps Phase9-A collect concrete measurements.
    private SettingsLayoutDebugSnapshot settingsLayoutDebug = SettingsLayoutDebugSnapshot.Empty;

    private void RenderSettingsLayoutDebugPanel()
    {
        if (!this.settingsLayoutDebug.HasRuntime)
        {
            return;
        }

        ImGui.Spacing();
        if (!ImGui.TreeNodeEx("レイアウト診断", ImGuiTreeNodeFlags.DefaultOpen))
        {
            return;
        }

        var snapshot = this.settingsLayoutDebug;

        ImGui.TextColored(UiTheme.MutedText, "計測パラメータ");
        ImGui.TextUnformatted($"PanelHeight: {snapshot.Runtime.PanelHeight:F1}px");
        ImGui.TextUnformatted($"ItemSpacingX: {snapshot.Runtime.SpacingX:F1}px");

        const ImGuiTableFlags tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp;
        if (ImGui.BeginTable("settings_layout_metrics", 3, tableFlags))
        {
            ImGui.TableSetupColumn("ケース", ImGuiTableColumnFlags.WidthStretch, 1.1f);
            ImGui.TableSetupColumn("利用幅", ImGuiTableColumnFlags.WidthStretch, 1f);
            ImGui.TableSetupColumn("カード幅", ImGuiTableColumnFlags.WidthStretch, 1f);
            ImGui.TableHeadersRow();

            RenderSettingsLayoutMetricsRow("現在", snapshot.Runtime);
            RenderSettingsLayoutMetricsRow("幅640px", snapshot.Width640);
            RenderSettingsLayoutMetricsRow("幅780px", snapshot.Width780);

            ImGui.EndTable();
        }

        ImGui.TreePop();
    }

    private static void RenderSettingsLayoutMetricsRow(string label, SettingsLayoutMetrics metrics)
    {
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        var layoutLabel = metrics.UsesTwoColumn ? "2列" : "1列";
        ImGui.TextUnformatted($"{label} ({layoutLabel})");

        ImGui.TableSetColumnIndex(1);
        ImGui.TextUnformatted($"{metrics.AvailableWidth:F1}px");

        ImGui.TableSetColumnIndex(2);
        ImGui.TextUnformatted($"{metrics.CardWidth:F1}px");
    }

    private readonly struct SettingsLayoutDebugSnapshot
    {
        public static readonly SettingsLayoutDebugSnapshot Empty = new SettingsLayoutDebugSnapshot(default, default, default);

        public SettingsLayoutDebugSnapshot(SettingsLayoutMetrics runtime, SettingsLayoutMetrics width640, SettingsLayoutMetrics width780)
        {
            this.Runtime = runtime;
            this.Width640 = width640;
            this.Width780 = width780;
        }

        public SettingsLayoutMetrics Runtime { get; }
        public SettingsLayoutMetrics Width640 { get; }
        public SettingsLayoutMetrics Width780 { get; }
        public bool HasRuntime => this.Runtime.AvailableWidth > 0.1f;

        public static SettingsLayoutDebugSnapshot Create(SettingsLayoutMetrics runtime)
        {
            if (runtime.AvailableWidth <= 0f)
            {
                return Empty;
            }

            var width640 = SettingsLayoutMetrics.Create(640f, runtime.PanelHeight, runtime.SpacingX, runtime.TwoColumnThreshold);
            var width780 = SettingsLayoutMetrics.Create(780f, runtime.PanelHeight, runtime.SpacingX, runtime.TwoColumnThreshold);
            return new SettingsLayoutDebugSnapshot(runtime, width640, width780);
        }
    }

    private readonly struct SettingsLayoutMetrics
    {
        public SettingsLayoutMetrics(float availableWidth, float panelHeight, float spacingX, float twoColumnThreshold, bool usesTwoColumn, float cardWidth, float cardHeight, float stackSpacingY)
        {
            this.AvailableWidth = availableWidth;
            this.PanelHeight = panelHeight;
            this.SpacingX = spacingX;
            this.TwoColumnThreshold = twoColumnThreshold;
            this.UsesTwoColumn = usesTwoColumn;
            this.CardWidth = cardWidth;
            this.CardHeight = cardHeight;
            this.StackSpacingY = stackSpacingY;
        }

        public float AvailableWidth { get; }
        public float PanelHeight { get; }
        public float SpacingX { get; }
        public float TwoColumnThreshold { get; }
        public bool UsesTwoColumn { get; }
        public float CardWidth { get; }
        public float CardHeight { get; }
        public float StackSpacingY { get; }

        public static SettingsLayoutMetrics Create(float availableWidth, float panelHeight, float spacingX, float twoColumnThreshold)
        {
            var usesTwoColumn = availableWidth >= twoColumnThreshold;
            var twoColumnWidth = MathF.Max((availableWidth - spacingX) * 0.5f, ChannelCardMinWidth);
            var singleColumnWidth = MathF.Max(availableWidth, ChannelCardMinWidth);
            var cardWidth = usesTwoColumn ? twoColumnWidth : singleColumnWidth;

            var baseHeight = ChannelCardHeight;

if (!usesTwoColumn)
{
    baseHeight -= 10f; // compact vertical layout to trim excess space under the URL field
}

var stackSpacingY = MathF.Max(4f, spacingX * 0.6f); // maintain a narrow but visible gutter between stacked cards
return new SettingsLayoutMetrics(availableWidth, panelHeight, spacingX, twoColumnThreshold, usesTwoColumn, cardWidth, baseHeight, stackSpacingY);
        }
    }
}
