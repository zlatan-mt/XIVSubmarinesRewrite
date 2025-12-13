// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SavePanel.cs
// 通知設定の保存ボタンとバリデーションメッセージを管理します
// 入力誤りをユーザーへ明示し、正しい状態でのみ設定を反映するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.ChannelCards.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;

public sealed partial class NotificationMonitorWindowRenderer
{
    private void RenderSavePanel(bool formValid)
    {
        var canSave = this.settingsDirty && formValid;
        ImGui.BeginDisabled(!canSave);
        if (ImGui.Button("通知設定を保存", new Vector2(180f, 0f)))
        {
            this.ApplySettings();
        }
        ImGui.EndDisabled();
        ImGui.SameLine();

        if (!formValid)
        {
            var message = this.discordUrlError ?? "URL を確認してください。";
            ImGui.TextColored(UiTheme.ErrorText, message);
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, "保存すると即座に Dalamud へ反映されます。");
        }
    }

    private bool IsNotificationFormValid()
    {
        var discordOk = !this.editingSettings.EnableDiscord || this.discordUrlValid;
        return discordOk;
    }
}