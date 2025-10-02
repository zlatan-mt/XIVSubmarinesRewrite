// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/DevNotificationPanel.cs
// 通知ウィンドウの開発者向け操作を分離し、履歴とログ記録を担当します
// DEV トグルや ForceNotify 手動発火の履歴を保存し、UI から簡潔に参照できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/IForceNotifyDiagnostics.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/UiPreferences.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Linq;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>Renders the developer-only notification controls and keeps lightweight history.</summary>
public sealed class DevNotificationPanel
{
    private readonly UiPreferences preferences;
    private readonly ISettingsProvider settingsProvider;
    private readonly IForceNotifyDiagnostics diagnostics;
    private readonly Func<int> triggerManualNotification;
    private readonly Action<ForceNotifyManualTrigger> logWriter;
    private DateTime? lastToastShownAt;
    private string lastToastMessage = string.Empty;
    private const double ToastVisibilitySeconds = 3.0;

    public DevNotificationPanel(
        UiPreferences preferences,
        ISettingsProvider settingsProvider,
        IForceNotifyDiagnostics diagnostics,
        Func<int> triggerManualNotification,
        Action<ForceNotifyManualTrigger> logWriter)
    {
        this.preferences = preferences;
        this.settingsProvider = settingsProvider;
        this.diagnostics = diagnostics;
        this.triggerManualNotification = triggerManualNotification;
        this.logWriter = logWriter;
    }

    public bool Render(ref NotificationSettings editingSettings, bool formValid, bool allowUnsafeActions, Func<DevContext> contextProvider)
    {
        var history = this.EnsureHistory();
        var changed = false;

        // DEV バナーを表示
        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.DevBannerBg);
        if (ImGui.BeginChild("##dev_banner", new System.Numerics.Vector2(0, 32), true, ImGuiWindowFlags.AlwaysUseWindowPadding))
        {
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new System.Numerics.Vector2(8, 4));
            ImGui.TextColored(UiTheme.DevBannerText, "⚠ 開発者ツール");
            ImGui.SameLine();
            ImGui.TextColored(UiTheme.MutedText, "テスト目的の設定です。通常運用では無効のままにしてください。");
            ImGui.PopStyleVar();
        }
        ImGui.EndChild();
        ImGui.PopStyleColor();

        ImGui.Spacing();

        // トースト表示
        this.RenderToast();

        var forceNotify = editingSettings.ForceNotifyUnderway;
        if (ImGui.Checkbox("出航中でも通知を送信 (開発用)", ref forceNotify))
        {
            editingSettings.ForceNotifyUnderway = forceNotify;
            history.LastForceNotifyToggleUtc = DateTime.UtcNow;
            history.ForceNotifyEnabled = forceNotify;
            this.PersistPreferences();
            changed = true;
        }

        var disableManual = !allowUnsafeActions && !formValid;
        if (disableManual)
        {
            ImGui.BeginDisabled();
        }

        if (ImGui.Button("選択キャラクターの通知を即時送信"))
        {
            var enqueued = this.triggerManualNotification();
            var context = contextProvider();
            this.RecordManualTrigger(history, context, enqueued, editingSettings.ForceNotifyUnderway);
            
            // トーストを表示
            this.lastToastShownAt = DateTime.UtcNow;
            this.lastToastMessage = enqueued > 0
                ? $"✓ {enqueued} 件の通知をキューに追加しました"
                : "再送対象がありませんでした";
        }

        if (disableManual)
        {
            ImGui.EndDisabled();
        }

        this.RenderHistory(history);
        return changed;
    }

    private void RecordManualTrigger(UiPreferences.DevPanelHistory history, DevContext context, int enqueued, bool includeUnderway)
    {
        var timestamp = DateTime.UtcNow;
        history.LastManualTriggerUtc = timestamp;
        history.LastManualTriggerSummary = enqueued > 0
            ? $"{enqueued} 件の通知を再送しました。"
            : "再送対象はありませんでした。";
        this.PersistPreferences();

        var entry = new ForceNotifyManualTrigger(
            timestamp,
            context.CharacterId,
            context.CharacterName,
            context.World,
            includeUnderway,
            enqueued);
        this.diagnostics.RecordManualTrigger(entry);
        this.logWriter(entry);
    }

    private void RenderToast()
    {
        if (!this.lastToastShownAt.HasValue)
        {
            return;
        }

        var elapsed = (DateTime.UtcNow - this.lastToastShownAt.Value).TotalSeconds;
        if (elapsed > ToastVisibilitySeconds)
        {
            this.lastToastShownAt = null;
            return;
        }

        // トーストの背景と表示
        var alpha = (float)(1.0 - (elapsed / ToastVisibilitySeconds));
        var toastBg = new System.Numerics.Vector4(
            UiTheme.DevAccent.X,
            UiTheme.DevAccent.Y,
            UiTheme.DevAccent.Z,
            0.9f * alpha);
        var toastText = new System.Numerics.Vector4(
            UiTheme.PrimaryText.X,
            UiTheme.PrimaryText.Y,
            UiTheme.PrimaryText.Z,
            alpha);

        ImGui.PushStyleColor(ImGuiCol.ChildBg, toastBg);
        if (ImGui.BeginChild("##dev_toast", new System.Numerics.Vector2(0, 28), true, ImGuiWindowFlags.AlwaysUseWindowPadding))
        {
            ImGui.TextColored(toastText, this.lastToastMessage);
        }
        ImGui.EndChild();
        ImGui.PopStyleColor();
        ImGui.Spacing();
    }

    private void RenderHistory(UiPreferences.DevPanelHistory history)
    {
        if (history.LastForceNotifyToggleUtc.HasValue)
        {
            var label = history.ForceNotifyEnabled ? "ON" : "OFF";
            ImGui.TextColored(UiTheme.MutedText, $"ForceNotify 切替: {history.LastForceNotifyToggleUtc:yyyy-MM-dd HH:mm:ss} ({label})");
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, "ForceNotify 切替: 未記録");
        }

        if (history.LastDeveloperTabToggleUtc.HasValue)
        {
            var devLabel = history.DeveloperToolsVisible ? "表示中" : "非表示";
            ImGui.TextColored(UiTheme.MutedText, $"DEV トグル: {history.LastDeveloperTabToggleUtc:yyyy-MM-dd HH:mm:ss} ({devLabel})");
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, "DEV トグル: 未記録");
        }

        if (history.LastManualTriggerUtc.HasValue)
        {
            ImGui.TextColored(UiTheme.MutedText, $"最終送信: {history.LastManualTriggerUtc:yyyy-MM-dd HH:mm:ss}");
            ImGui.TextColored(UiTheme.AccentPrimary, history.LastManualTriggerSummary ?? "ログがありません。");
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, "最終送信: 未記録");
        }

        var logEntries = this.diagnostics.GetManualTriggerLog();
        if (logEntries.Count > 0)
        {
            ImGui.Separator();
            ImGui.TextColored(UiTheme.MutedText, "手動送信ログ (最新 10 件)");
            ImGui.Spacing();

            if (ImGui.BeginTable("##dev_log_table", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp))
            {
                ImGui.TableSetupColumn("時刻", ImGuiTableColumnFlags.WidthFixed, 80);
                ImGui.TableSetupColumn("キャラクター", ImGuiTableColumnFlags.WidthStretch);
                ImGui.TableSetupColumn("通知数", ImGuiTableColumnFlags.WidthFixed, 60);
                ImGui.TableSetupColumn("モード", ImGuiTableColumnFlags.WidthFixed, 80);
                ImGui.TableHeadersRow();

                var start = Math.Max(0, logEntries.Count - 10);
                for (var index = logEntries.Count - 1; index >= start; index--)
                {
                    var entry = logEntries[index];
                    ImGui.TableNextRow();

                    ImGui.TableNextColumn();
                    ImGui.TextColored(UiTheme.MutedText, entry.TriggeredAtUtc.ToLocalTime().ToString("HH:mm:ss"));

                    ImGui.TableNextColumn();
                    var charDisplay = !string.IsNullOrEmpty(entry.CharacterName) && !string.IsNullOrEmpty(entry.World)
                        ? $"{entry.CharacterName} @ {entry.World}"
                        : $"CID:{entry.CharacterId}";
                    ImGui.TextColored(UiTheme.PrimaryText, charDisplay);

                    ImGui.TableNextColumn();
                    var countColor = entry.NotificationsEnqueued > 0 ? UiTheme.SuccessText : UiTheme.MutedText;
                    ImGui.TextColored(countColor, $"{entry.NotificationsEnqueued}");

                    ImGui.TableNextColumn();
                    var mode = entry.IncludeUnderway ? "Underway" : "Completed";
                    var modeColor = entry.IncludeUnderway ? UiTheme.DevDangerText : UiTheme.AccentPrimary;
                    ImGui.TextColored(modeColor, mode);
                }

                ImGui.EndTable();
            }
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, "手動送信ログ: 未記録");
        }
    }

    private UiPreferences.DevPanelHistory EnsureHistory()
    {
        if (this.preferences.DevHistory == null)
        {
            this.preferences.DevHistory = new UiPreferences.DevPanelHistory();
        }

        return this.preferences.DevHistory;
    }

    private void PersistPreferences()
    {
        this.settingsProvider.SaveAsync(this.preferences).GetAwaiter().GetResult();
    }

    public readonly record struct DevContext(ulong CharacterId, string? CharacterName, string? World, bool IncludeUnderway);
}
