// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.LayoutDebug.cs
// 通知設定ウィンドウのレイアウト診断とメトリクス収集を担当します
// Phase9-A でカード幅や閾値を計測し、レイアウト調整の根拠を残すため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SettingsLayout.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiTreeNodeFlags = Dalamud.Bindings.ImGui.ImGuiTreeNodeFlags;
using XIVSubmarinesRewrite.Application.Notifications;

public sealed partial class NotificationMonitorWindowRenderer
{
    // Shared layout constants keep card rendering and debug metrics aligned.
    private const float ChannelCardMinWidth = 280f;
    private const float ChannelCardBaseHeight = 148f;
    private const float ChannelCardMinHeight = 120f;

    // Latest layout snapshot helps Phase9-A collect concrete measurements.
    private NotificationLayoutDebugSnapshot settingsLayoutDebug = NotificationLayoutDebugSnapshot.Empty;
    private string? layoutTelemetryMessage;
    private DateTime layoutTelemetryExpiryUtc;

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

        if (ImGui.Button("メトリクスJSONを保存"))
        {
            this.SaveLayoutTelemetry(snapshot.Runtime);
        }
        ImGui.SameLine();
        this.RenderLayoutTelemetryStatus();

        ImGui.TreePop();
    }

    private static void RenderSettingsLayoutMetricsRow(string label, NotificationLayoutMetrics metrics)
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

    private readonly struct NotificationLayoutDebugSnapshot
    {
        public static readonly NotificationLayoutDebugSnapshot Empty = new NotificationLayoutDebugSnapshot(default, default, default);

        public NotificationLayoutDebugSnapshot(NotificationLayoutMetrics runtime, NotificationLayoutMetrics width640, NotificationLayoutMetrics width780)
        {
            this.Runtime = runtime;
            this.Width640 = width640;
            this.Width780 = width780;
        }

        public NotificationLayoutMetrics Runtime { get; }
        public NotificationLayoutMetrics Width640 { get; }
        public NotificationLayoutMetrics Width780 { get; }
        public bool HasRuntime => this.Runtime.AvailableWidth > 0.1f;

        public static NotificationLayoutDebugSnapshot Create(NotificationLayoutMetrics runtime)
        {
            if (runtime.AvailableWidth <= 0f)
            {
                return Empty;
            }

            var width640 = NotificationLayoutMetrics.Create(640f, runtime.PanelHeight, runtime.SpacingX, runtime.TwoColumnThreshold);
            var width780 = NotificationLayoutMetrics.Create(780f, runtime.PanelHeight, runtime.SpacingX, runtime.TwoColumnThreshold);
            return new NotificationLayoutDebugSnapshot(runtime, width640, width780);
        }
    }

    private readonly struct NotificationLayoutMetrics
    {
        public NotificationLayoutMetrics(float availableWidth, float panelHeight, float spacingX, float twoColumnThreshold, bool usesTwoColumn, float cardWidth, float cardHeight, float stackSpacingY)
        {
            this.AvailableWidth = availableWidth;
            this.PanelHeight = panelHeight;
            this.SpacingX = spacingX;
            this.TwoColumnThreshold = twoColumnThreshold;
            this.UsesTwoColumn = usesTwoColumn;
            this.CardWidth = cardWidth;
            this.CardHeight = cardHeight;
            this.StackSpacingY = stackSpacingY;
            this.breakpointList = new[] { twoColumnThreshold };
        }

        public float AvailableWidth { get; }
        public float PanelHeight { get; }
        public float SpacingX { get; }
        public float TwoColumnThreshold { get; }
        public bool UsesTwoColumn { get; }
        public float CardWidth { get; }
        public float CardHeight { get; }
        public float StackSpacingY { get; }
        public int ColumnCount => this.UsesTwoColumn ? 2 : 1;
        public IReadOnlyList<float> Breakpoints => this.breakpointList;

        private readonly IReadOnlyList<float> breakpointList;

        public static NotificationLayoutMetrics Create(float availableWidth, float panelHeight, float spacingX, float twoColumnThreshold)
        {
            var usesTwoColumn = availableWidth >= twoColumnThreshold;
            var twoColumnWidth = MathF.Max((availableWidth - spacingX) * 0.5f, ChannelCardMinWidth);
            var singleColumnWidth = MathF.Max(availableWidth, ChannelCardMinWidth);
            var cardWidth = usesTwoColumn ? twoColumnWidth : singleColumnWidth;

            var baseHeight = ChannelCardBaseHeight;
            if (!usesTwoColumn)
            {
                baseHeight -= 12f;
            }

            var cardHeight = MathF.Max(ChannelCardMinHeight, baseHeight);
            var stackSpacingY = MathF.Max(6f, spacingX * 0.5f);
            return new NotificationLayoutMetrics(availableWidth, panelHeight, spacingX, twoColumnThreshold, usesTwoColumn, cardWidth, cardHeight, stackSpacingY);
        }
    }

    private void SaveLayoutTelemetry(NotificationLayoutMetrics metrics)
    {
        try
        {
            var payload = LayoutTelemetryRecord.Create(metrics);
            var nowUtc = DateTime.UtcNow;
            var dateSegment = nowUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var timestamp = nowUtc.ToString("HH-mm-ss-fff", CultureInfo.InvariantCulture);
            var directory = Path.Combine(AppContext.BaseDirectory, "logs", dateSegment, "notification-layout");
            Directory.CreateDirectory(directory);
            var filePath = Path.Combine(directory, $"metrics-{timestamp}.json");
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            this.layoutTelemetryMessage = $"{timestamp} に保存: metrics-{timestamp}.json";
            this.layoutTelemetryExpiryUtc = nowUtc.AddSeconds(5);
        }
        catch (Exception error)
        {
            this.layoutTelemetryMessage = $"保存失敗: {error.Message}";
            this.layoutTelemetryExpiryUtc = DateTime.UtcNow.AddSeconds(5);
        }
    }

    private void RenderLayoutTelemetryStatus()
    {
        if (this.layoutTelemetryMessage is null)
        {
            ImGui.TextColored(UiTheme.MutedText, "クリックで JSON を出力します。");
            return;
        }

        if (DateTime.UtcNow > this.layoutTelemetryExpiryUtc)
        {
            this.layoutTelemetryMessage = null;
            ImGui.TextColored(UiTheme.MutedText, "クリックで JSON を出力します。");
            return;
        }

        ImGui.TextColored(UiTheme.AccentPrimary, this.layoutTelemetryMessage);
    }

    private void SaveDevManualLog(ForceNotifyManualTrigger entry)
    {
        try
        {
            var nowUtc = entry.TriggeredAtUtc.ToUniversalTime();
            var dateSegment = nowUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var timestamp = nowUtc.ToString("HH-mm-ss-fff", CultureInfo.InvariantCulture);
            var directory = Path.Combine(AppContext.BaseDirectory, "logs", dateSegment, "dev-panel");
            Directory.CreateDirectory(directory);
            var filePath = Path.Combine(directory, $"manual-{timestamp}.json");
            var payload = new
            {
                entry.CharacterId,
                entry.CharacterName,
                entry.World,
                entry.IncludeUnderway,
                entry.NotificationsEnqueued,
                TriggeredAtUtc = nowUtc,
            };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            this.layoutTelemetryMessage = $"DEV ログ保存: manual-{timestamp}.json";
            this.layoutTelemetryExpiryUtc = DateTime.UtcNow.AddSeconds(5);
        }
        catch (Exception error)
        {
            this.layoutTelemetryMessage = $"DEV ログ保存失敗: {error.Message}";
            this.layoutTelemetryExpiryUtc = DateTime.UtcNow.AddSeconds(5);
        }
    }


    private readonly struct LayoutTelemetryRecord
    {
        private LayoutTelemetryRecord(
            float availableWidth,
            float panelHeight,
            float spacingX,
            float cardWidth,
            float cardHeight,
            float twoColumnThreshold,
            bool usesTwoColumn,
            float stackSpacing,
            IReadOnlyList<float> breakpoints)
        {
            this.AvailableWidth = availableWidth;
            this.PanelHeight = panelHeight;
            this.SpacingX = spacingX;
            this.CardWidth = cardWidth;
            this.CardHeight = cardHeight;
            this.TwoColumnThreshold = twoColumnThreshold;
            this.UsesTwoColumn = usesTwoColumn;
            this.StackSpacing = stackSpacing;
            this.Breakpoints = breakpoints;
        }

        public float AvailableWidth { get; }
        public float PanelHeight { get; }
        public float SpacingX { get; }
        public float CardWidth { get; }
        public float CardHeight { get; }
        public float TwoColumnThreshold { get; }
        public bool UsesTwoColumn { get; }
        public float StackSpacing { get; }
        public IReadOnlyList<float> Breakpoints { get; }

        public static LayoutTelemetryRecord Create(NotificationLayoutMetrics metrics)
        {
            return new LayoutTelemetryRecord(
                metrics.AvailableWidth,
                metrics.PanelHeight,
                metrics.SpacingX,
                metrics.CardWidth,
                metrics.CardHeight,
                metrics.TwoColumnThreshold,
                metrics.UsesTwoColumn,
                metrics.StackSpacingY,
                metrics.Breakpoints);
        }
    }
}
