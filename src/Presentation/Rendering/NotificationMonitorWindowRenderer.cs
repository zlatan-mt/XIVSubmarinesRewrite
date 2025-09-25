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

        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.PanelBg);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 6f);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(10f, 8f));
        var changed = false;
        if (!ImGui.BeginChild("##notification_settings_panel", Vector2.Zero, true))
        {
            ImGui.EndChild();
            ImGui.PopStyleVar(2);
            ImGui.PopStyleColor();
            return;
        }

        var enableDiscord = this.editingSettings.EnableDiscord;
        if (ImGui.Checkbox("Discord 通知を有効化", ref enableDiscord))
        {
            this.editingSettings.EnableDiscord = enableDiscord;
            changed = true;
        }
        changed |= this.RenderUrlInput("Discord Webhook URL", this.discordUrlBuffer, value => this.editingSettings.DiscordWebhookUrl = value);
        var discordStatusText = this.editingSettings.EnableDiscord ? "Discord 通知は有効です" : "Discord 通知は無効です";
        var discordStatusColor = this.editingSettings.EnableDiscord ? UiTheme.SuccessText : UiTheme.ErrorText;
        ImGui.TextColored(discordStatusColor, discordStatusText);

        ImGui.Separator();
        var enableNotion = this.editingSettings.EnableNotion;
        if (ImGui.Checkbox("Notion 通知を有効化", ref enableNotion))
        {
            this.editingSettings.EnableNotion = enableNotion;
            changed = true;
        }
        changed |= this.RenderUrlInput("Notion Webhook URL", this.notionUrlBuffer, value => this.editingSettings.NotionWebhookUrl = value);
        var notionStatusText = this.editingSettings.EnableNotion ? "Notion 通知は有効です" : "Notion 通知は無効です";
        var notionStatusColor = this.editingSettings.EnableNotion ? UiTheme.SuccessText : UiTheme.ErrorText;
        ImGui.TextColored(notionStatusColor, notionStatusText);

        ImGui.Separator();
        var retention = this.editingSettings.DeadLetterRetentionLimit;
        if (ImGui.SliderInt("デッドレター保持数", ref retention, 8, 256))
        {
            this.editingSettings.DeadLetterRetentionLimit = retention;
            changed = true;
        }

        var notifyCompleted = this.editingSettings.NotifyVoyageCompleted;
        if (ImGui.Checkbox("航海完了の通知を送信", ref notifyCompleted))
        {
            this.editingSettings.NotifyVoyageCompleted = notifyCompleted;
            changed = true;
        }

        var notifyUnderway = this.editingSettings.NotifyVoyageUnderway;
        if (ImGui.Checkbox("出航直後の通知を送信", ref notifyUnderway))
        {
            this.editingSettings.NotifyVoyageUnderway = notifyUnderway;
            changed = true;
        }

        var batchWindowSeconds = this.editingDiscordBatchWindowSeconds;
        if (ImGui.SliderFloat("Discord バッチ間隔 (秒)", ref batchWindowSeconds, 0.5f, 15f, "%.1f"))
        {
            this.editingDiscordBatchWindowSeconds = batchWindowSeconds;
            this.editingSettings.DiscordBatchWindowSeconds = Math.Round(batchWindowSeconds, 1);
            changed = true;
        }

        this.RenderIdentityRecoveryControls();

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

        ImGui.EndChild();
        ImGui.PopStyleVar(2);
        ImGui.PopStyleColor();
    }

    private void RenderIdentityRecoveryControls()
    {
        ImGui.Separator();
        ImGui.TextColored(UiTheme.MutedText, "キャラクター名の復元");

        if (ImGui.Button("キャラクター名を再取得"))
        {
            var refreshed = this.RecoverCharacterIdentities();
            this.ShowIdentityToast(refreshed);
        }

        ImGui.SameLine();
        ImGui.TextColored(UiTheme.MutedText, "設定ファイルが欠損した際に使用してください。");
        this.RenderIdentityToast();
        ImGui.Spacing();
    }

    private int RecoverCharacterIdentities()
    {
        var snapshots = this.snapshotCache.GetAll();
        var refreshed = 0;

        foreach (var kvp in snapshots)
        {
            var characterId = kvp.Key;
            var snapshot = kvp.Value;
            var before = this.characterRegistry.GetIdentity(characterId);
            var beforeMissing = before is null
                || string.IsNullOrWhiteSpace(before.Name)
                || string.IsNullOrWhiteSpace(before.World);
            this.characterRegistry.RegisterSnapshot(snapshot);
            var after = this.characterRegistry.GetIdentity(characterId);
            var afterMissing = after is null
                || string.IsNullOrWhiteSpace(after.Name)
                || string.IsNullOrWhiteSpace(after.World);
            if (beforeMissing && !afterMissing)
            {
                refreshed++;
            }
        }

        return refreshed;
    }

    // 即席のトーストとしてステータスを短時間だけ表示します。
    private void ShowIdentityToast(int refreshedCount)
    {
        this.identityToastMessage = refreshedCount > 0
            ? $"{refreshedCount} 件のキャラクター名を更新しました。"
            : "更新可能なキャラクター名はありません。";
        this.identityToastExpiryUtc = DateTime.UtcNow.AddSeconds(4);
    }

    private void RenderIdentityToast()
    {
        if (this.identityToastMessage is null)
        {
            return;
        }

        if (DateTime.UtcNow > this.identityToastExpiryUtc)
        {
            this.identityToastMessage = null;
            return;
        }

        ImGui.SameLine();
        ImGui.TextColored(UiTheme.AccentPrimary, this.identityToastMessage);
    }


}
