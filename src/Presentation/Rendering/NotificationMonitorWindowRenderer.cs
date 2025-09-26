// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs
// 通知モニタウィンドウのメイン描画を束ねるクラスです
// 設定編集とキューの可視化を 1 画面に集約し、運用調整を簡単にするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Diagnostics.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Queue.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiCol = Dalamud.Bindings.ImGui.ImGuiCol;
using ImGuiStyleVar = Dalamud.Bindings.ImGui.ImGuiStyleVar;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiWindowFlags = Dalamud.Bindings.ImGui.ImGuiWindowFlags;
using ImGuiTreeNodeFlags = Dalamud.Bindings.ImGui.ImGuiTreeNodeFlags;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Presentation.ViewModels;

/// <summary>Dedicated window for monitoring notification queue state and editing notification settings.</summary>
public sealed partial class NotificationMonitorWindowRenderer : IViewRenderer
{
    private readonly NotificationQueueViewModel queueViewModel;
    private readonly NotificationSettings settings;
    private readonly NotificationQueueOptions queueOptions;
    private readonly ISettingsProvider settingsProvider;
    private readonly INotificationQueue queue;
    private readonly SnapshotCache snapshotCache;
    private readonly ICharacterRegistry characterRegistry;
    private readonly DiscordNotificationBatcher discordBatcher;
    private readonly IForceNotifyDiagnostics forceNotifyDiagnostics;
    private NotificationSettings editingSettings;
    private bool settingsDirty;
    private readonly byte[] discordUrlBuffer = new byte[512];
    private readonly byte[] notionUrlBuffer = new byte[512];
    private float editingDiscordBatchWindowSeconds;
    private bool isVisible;
    private string? identityToastMessage;
    private DateTime identityToastExpiryUtc;

    public NotificationMonitorWindowRenderer(
        NotificationQueueViewModel queueViewModel,
        NotificationSettings settings,
        NotificationQueueOptions queueOptions,
        ISettingsProvider settingsProvider,
        INotificationQueue queue,
        SnapshotCache snapshotCache,
        ICharacterRegistry characterRegistry,
        DiscordNotificationBatcher discordBatcher,
        IForceNotifyDiagnostics forceNotifyDiagnostics)
    {
        this.queueViewModel = queueViewModel;
        this.settings = settings;
        this.queueOptions = queueOptions;
        this.settingsProvider = settingsProvider;
        this.queue = queue;
        this.snapshotCache = snapshotCache;
        this.characterRegistry = characterRegistry;
        this.discordBatcher = discordBatcher;
        this.forceNotifyDiagnostics = forceNotifyDiagnostics;
        this.editingSettings = this.settings.Clone();
        this.LoadBuffersFromSettings();
    }

    public bool IsVisible
    {
        get => this.isVisible;
        set => this.isVisible = value;
    }

    public void Render()
    {
        if (!this.isVisible)
        {
            return;
        }

        var title = "XIV Submarines — Notifications";
        ImGui.PushStyleVar(ImGuiStyleVar.WindowMinSize, new Vector2(520f, 380f));
        ImGui.SetNextWindowSize(new Vector2(760f, 540f), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(new Vector2(520f, 360f), new Vector2(1400f, 980f));

        if (!ImGui.Begin(title, ref this.isVisible))
        {
            ImGui.End();
            ImGui.PopStyleVar();
            return;
        }

        this.RenderContent(includeDeveloperOptions: true, includeDiagnostics: true);
        this.RenderResizeHandle();

        ImGui.End();
        ImGui.PopStyleVar();
    }

    public void RenderInline()
    {
        ImGui.PushID("xsr_notify_inline");
        this.RenderContent(includeDeveloperOptions: true, includeDiagnostics: true);
        ImGui.PopID();
    }

    public void RenderNotificationTab()
    {
        ImGui.PushID("xsr_notify_tab");
        this.RenderContent(includeDeveloperOptions: false, includeDiagnostics: false);
        ImGui.PopID();
    }

    public void RenderDeveloperTab()
    {
        ImGui.PushID("xsr_notify_dev_tab");
        var changed = this.RenderDeveloperOptions();

        ImGui.Separator();
        this.RenderForceNotifyDiagnosticsSection();
        ImGui.Separator();
        this.RenderSettingsLayoutDebugPanel();

        if (changed)
        {
            this.settingsDirty = true;
        }
        ImGui.PopID();
    }

    private void RenderContent(bool includeDeveloperOptions, bool includeDiagnostics)
    {
        this.RenderSettingsSection(includeDeveloperOptions);
        ImGui.Separator();
        this.RenderQueueSection();
        if (includeDiagnostics)
        {
            ImGui.Separator();
            this.RenderForceNotifyDiagnosticsSection();
            this.RenderSettingsLayoutDebugPanel();
        }
    }

    private void RenderSettingsSection(bool includeDeveloperOptions)
    {
        if (!ImGui.CollapsingHeader("通知設定", ImGuiTreeNodeFlags.DefaultOpen))
        {
            return;
        }

        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.PanelBg);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 6f);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(10f, 5f));

        var windowHeight = ImGui.GetWindowHeight();
        var panelHeight = windowHeight > 0f ? MathF.Min(windowHeight * 0.5f, windowHeight - 160f) : 320f;
        panelHeight = MathF.Max(220f, MathF.Min(panelHeight, 400f));

        if (ImGui.BeginChild("##notification_settings_panel", new Vector2(0f, panelHeight), true, ImGuiWindowFlags.AlwaysUseWindowPadding))
        {
            var changed = false;
            var spacing = ImGui.GetStyle().ItemSpacing.X;
            var width = ImGui.GetContentRegionAvail().X;
            var metrics = SettingsLayoutMetrics.Create(width, panelHeight, spacing, 620f);
            this.settingsLayoutDebug = SettingsLayoutDebugSnapshot.Create(metrics);

            var discordEnabled = this.editingSettings.EnableDiscord;
            var notionEnabled = this.editingSettings.EnableNotion;
            var twoColumn = metrics.UsesTwoColumn;
            var cardWidth = metrics.CardWidth;

            if (twoColumn)
            {
                ImGui.BeginGroup();
                changed |= this.RenderChannelCard("discord", "Discord", ref discordEnabled, this.discordUrlBuffer, value => this.editingSettings.DiscordWebhookUrl = value, cardWidth, metrics.CardHeight);
                ImGui.SameLine(0f, spacing);
                changed |= this.RenderChannelCard("notion", "Notion", ref notionEnabled, this.notionUrlBuffer, value => this.editingSettings.NotionWebhookUrl = value, cardWidth, metrics.CardHeight);
                ImGui.EndGroup();
            }
            else
            {
                changed |= this.RenderChannelCard("discord", "Discord", ref discordEnabled, this.discordUrlBuffer, value => this.editingSettings.DiscordWebhookUrl = value, cardWidth, metrics.CardHeight);
                ImGui.Dummy(new Vector2(0f, metrics.StackSpacingY));
                changed |= this.RenderChannelCard("notion", "Notion", ref notionEnabled, this.notionUrlBuffer, value => this.editingSettings.NotionWebhookUrl = value, cardWidth, metrics.CardHeight);
            }

            this.editingSettings.EnableDiscord = discordEnabled;
            this.editingSettings.EnableNotion = notionEnabled;

            ImGui.Separator();
            changed |= this.RenderDeliveryOptions();

            ImGui.Separator();
            this.RenderIdentityRecoveryControls();

            if (includeDeveloperOptions)
            {
                ImGui.Separator();
                changed |= this.RenderDeveloperOptions();
            }

            if (changed)
            {
                this.settingsDirty = true;
            }

            ImGui.BeginDisabled(!this.settingsDirty);
            if (ImGui.Button("通知設定を保存", new Vector2(180f, 0f)))
            {
                this.ApplySettings();
            }
            ImGui.EndDisabled();
            ImGui.SameLine();
            ImGui.TextColored(UiTheme.MutedText, "保存すると即座に Dalamud へ反映されます。");

            ImGui.EndChild();
        }

        ImGui.PopStyleVar(2);
        ImGui.PopStyleColor();
    }

}
