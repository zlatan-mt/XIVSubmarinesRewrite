namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Globalization;
using System.Text;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
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
        if (!ImGui.Begin(title, ref this.isVisible, ImGuiWindowFlags.AlwaysAutoResize))
        {
            ImGui.End();
            return;
        }

        this.RenderContent();

        ImGui.End();
    }

    public void RenderInline()
    {
        ImGui.PushID("xsr_notify_inline");
        this.RenderContent();
        ImGui.PopID();
    }

    private void RenderContent()
    {
        this.RenderSettingsSection();
        ImGui.Separator();
        this.RenderQueueSection();
    }

    private void RenderSettingsSection()
    {
        if (!ImGui.CollapsingHeader("通知設定", ImGuiTreeNodeFlags.DefaultOpen))
        {
            return;
        }

        var changed = false;

        var enableDiscord = this.editingSettings.EnableDiscord;
        if (ImGui.Checkbox("Discord 通知を有効化", ref enableDiscord))
        {
            this.editingSettings.EnableDiscord = enableDiscord;
            changed = true;
        }
        changed |= this.RenderUrlInput("Discord Webhook URL", this.discordUrlBuffer, value => this.editingSettings.DiscordWebhookUrl = value);

        ImGui.Separator();
        var enableNotion = this.editingSettings.EnableNotion;
        if (ImGui.Checkbox("Notion 通知を有効化", ref enableNotion))
        {
            this.editingSettings.EnableNotion = enableNotion;
            changed = true;
        }
        changed |= this.RenderUrlInput("Notion Webhook URL", this.notionUrlBuffer, value => this.editingSettings.NotionWebhookUrl = value);

        ImGui.Separator();
        var retention = this.editingSettings.DeadLetterRetentionLimit;
        if (ImGui.SliderInt("デッドレター保持数", ref retention, 8, 256))
        {
            this.editingSettings.DeadLetterRetentionLimit = retention;
            changed = true;
        }

        var batchWindowSeconds = this.editingDiscordBatchWindowSeconds;
        if (ImGui.SliderFloat("Discord バッチ間隔 (秒)", ref batchWindowSeconds, 0.5f, 15f, "%.1f"))
        {
            this.editingDiscordBatchWindowSeconds = batchWindowSeconds;
            this.editingSettings.DiscordBatchWindowSeconds = Math.Round(batchWindowSeconds, 1);
            changed = true;
        }

        if (ImGui.CollapsingHeader("開発オプション", ImGuiTreeNodeFlags.DefaultOpen))
        {
            ImGui.TextDisabled("テスト用の一時的な動作変更です。通常運用では無効のままにしてください。");
            var forceNotify = this.editingSettings.ForceNotifyUnderway;
            if (ImGui.Checkbox("出航中でも通知を送信する (開発用)", ref forceNotify))
            {
                this.editingSettings.ForceNotifyUnderway = forceNotify;
                changed = true;
            }

            if (ImGui.Button("選択キャラクターの通知を即時送信"))
            {
                this.TriggerManualNotification();
            }
        }

        if (changed)
        {
            this.settingsDirty = true;
        }

        ImGui.BeginDisabled(!this.settingsDirty);
        if (ImGui.Button("通知設定を保存"))
        {
            this.ApplySettings();
        }
        ImGui.EndDisabled();

        this.RenderForceNotifyDiagnosticsSection();
    }


}
