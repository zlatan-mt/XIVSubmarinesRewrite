// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SettingsLayout.cs
// 通知設定タブの UI ヘルパー群をまとめた部分クラスです
// レイアウト補助メソッドを切り出し、メイン描画コードを読みやすく保つため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Queue.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiCol = Dalamud.Bindings.ImGui.ImGuiCol;
using ImGuiStyleVar = Dalamud.Bindings.ImGui.ImGuiStyleVar;
using ImGuiWindowFlags = Dalamud.Bindings.ImGui.ImGuiWindowFlags;
using ImGuiTreeNodeFlags = Dalamud.Bindings.ImGui.ImGuiTreeNodeFlags;
using ImGuiMouseCursor = Dalamud.Bindings.ImGui.ImGuiMouseCursor;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiTableRowFlags = Dalamud.Bindings.ImGui.ImGuiTableRowFlags;
using ImGuiSliderFlags = Dalamud.Bindings.ImGui.ImGuiSliderFlags;

public sealed partial class NotificationMonitorWindowRenderer
{
    private bool RenderChannelCard(string id, string title, ref bool enabled, byte[] buffer, Action<string> setter, float preferredHeight)
    {
        var changed = false;
        ImGui.PushID(id);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(8f, 6f));
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(6f, 4f));
        var childFlags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.AlwaysUseWindowPadding;
        var cardHeight = MathF.Max(preferredHeight, ChannelCardMinHeight);
        if (ImGui.BeginChild("channel_card", new Vector2(-1f, cardHeight), true, childFlags))
        {
            var stateText = enabled ? "ACTIVE" : "OFF";
            var stateColor = enabled ? UiTheme.SuccessText : UiTheme.MutedText;
            ImGui.TextColored(UiTheme.PrimaryText, title);
            ImGui.SameLine();
            ImGui.TextColored(stateColor, stateText);
            ImGui.SameLine();
            if (ImGui.Checkbox("通知を送信", ref enabled))
            {
                changed = true;
            }

            ImGui.PushItemWidth(-1f);
            changed |= this.RenderUrlInput("Webhook URL", buffer, setter);
            ImGui.PopItemWidth();
        }

        ImGui.EndChild();
        ImGui.PopStyleVar(2);
        ImGui.PopID();
        return changed;
    }

    private bool RenderChannelCards(in SettingsLayoutMetrics metrics, ref bool discordEnabled, ref bool notionEnabled)
    {
        var changed = false;
        const ImGuiTableFlags flags = ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.PadOuterX | ImGuiTableFlags.NoSavedSettings;
        var columns = Math.Max(1, metrics.ColumnCount);
        if (!ImGui.BeginTable("channel_cards", columns, flags))
        {
            return false;
        }

        var totalChannels = 2;
        for (var index = 0; index < totalChannels; index++)
        {
            if (index % columns == 0)
            {
                var rowHeight = metrics.CardHeight;
                if (index >= columns)
                {
                    rowHeight += metrics.StackSpacingY;
                }

                ImGui.TableNextRow(ImGuiTableRowFlags.None, rowHeight);
            }

            ImGui.TableSetColumnIndex(index % columns);
            if (index == 0)
            {
                changed |= this.RenderChannelCard("discord", "Discord", ref discordEnabled, this.discordUrlBuffer, value => this.editingSettings.DiscordWebhookUrl = value, metrics.CardHeight);
            }
            else
            {
                changed |= this.RenderChannelCard("notion", "Notion", ref notionEnabled, this.notionUrlBuffer, value => this.editingSettings.NotionWebhookUrl = value, metrics.CardHeight);
            }
        }

        ImGui.EndTable();
        return changed;
    }

    private bool RenderDeliveryOptions()
    {
        var changed = false;
        const ImGuiTableFlags tableFlags = ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.PadOuterX | ImGuiTableFlags.NoSavedSettings;
        if (ImGui.BeginTable("delivery_options", 2, tableFlags))
        {
            ImGui.TableSetupColumn("label", ImGuiTableColumnFlags.WidthFixed, 210f);
            ImGui.TableSetupColumn("value");

            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextColored(UiTheme.MutedText, "航海完了の通知 / 出港直後の通知");
            ImGui.TableNextColumn();
            var notifyCompleted = this.editingSettings.NotifyVoyageCompleted;
            var notifyUnderway = this.editingSettings.NotifyVoyageUnderway;
            var columnWidth = ImGui.GetContentRegionAvail().X;
            var halfWidth = MathF.Max(0f, columnWidth * 0.5f - ImGui.GetStyle().ItemInnerSpacing.X);
            ImGui.SetNextItemWidth(halfWidth);
            if (ImGui.Checkbox("航海完了を通知", ref notifyCompleted))
            {
                this.editingSettings.NotifyVoyageCompleted = notifyCompleted;
                changed = true;
            }
            ImGui.SameLine();
            ImGui.SetNextItemWidth(-1f);
            if (ImGui.Checkbox("出港直後を通知", ref notifyUnderway))
            {
                this.editingSettings.NotifyVoyageUnderway = notifyUnderway;
                changed = true;
            }

            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextColored(UiTheme.MutedText, "Discord バッチ間隔 (秒)");
            ImGui.TableNextColumn();
            var windowSeconds = this.editingDiscordBatchWindowSeconds;
            ImGui.SetNextItemWidth(200f);
            if (ImGui.SliderFloat("##discord_batch_window", ref windowSeconds, 0.5f, 15f, "%.1fs", ImGuiSliderFlags.AlwaysClamp))
            {
                this.editingDiscordBatchWindowSeconds = windowSeconds;
                this.editingSettings.DiscordBatchWindowSeconds = Math.Clamp(windowSeconds, 0.5f, 15f);
                changed = true;
            }
            ImGui.SameLine();
            ImGui.TextColored(UiTheme.MutedText, "推奨 1.5s");

            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TextColored(UiTheme.MutedText, "デッドレター保持数");
            ImGui.TableNextColumn();
            var deadLetters = this.editingSettings.DeadLetterRetentionLimit;
            ImGui.SetNextItemWidth(140f);
            if (ImGui.DragInt("##dead_letter_capacity", ref deadLetters, 1f, 1, 64, "%d"))
            {
                this.editingSettings.DeadLetterRetentionLimit = Math.Clamp(deadLetters, 1, 64);
                changed = true;
            }

            ImGui.EndTable();
        }

        return changed;
    }

    private bool RenderDeveloperOptions()
    {
        var changed = false;
        if (!ImGui.TreeNodeEx("開発オプション", ImGuiTreeNodeFlags.DefaultOpen))
        {
            return false;
        }

        ImGui.TextColored(UiTheme.MutedText, "テスト用の一時的な動作変更です。通常運用では無効のままにしてください。");
        var forceNotify = this.editingSettings.ForceNotifyUnderway;
        if (ImGui.Checkbox("出航中でも通知を送信 (開発用)", ref forceNotify))
        {
            this.editingSettings.ForceNotifyUnderway = forceNotify;
            changed = true;
        }

        if (ImGui.Button("選択キャラクターの通知を即時送信"))
        {
            this.TriggerManualNotification();
        }

        ImGui.TreePop();
        return changed;
    }

    private void RenderIdentityRecoveryControls()
    {
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

    private void RenderResizeHandle()
    {
        var handleSize = new Vector2(28f, 28f);
        var padding = new Vector2(10f, 10f);
        var windowPos = ImGui.GetWindowPos();
        var windowSize = ImGui.GetWindowSize();
        var handlePos = windowPos + windowSize - handleSize - padding;

        ImGui.SetCursorScreenPos(handlePos);
        ImGui.InvisibleButton("##notify_resize", handleSize);
        ImGui.SetItemAllowOverlap();
        var hovered = ImGui.IsItemHovered();
        var active = ImGui.IsItemActive();

        var drawList = ImGui.GetWindowDrawList();
        var color = hovered ? UiTheme.AccentPrimary : UiTheme.MutedText;
        var offset = 6f;
        for (var i = 0; i < 3; i++)
        {
            var start = handlePos + new Vector2(handleSize.X - offset * (i + 1), handleSize.Y);
            var end = handlePos + new Vector2(handleSize.X, handleSize.Y - offset * (i + 1));
            drawList.AddLine(start, end, ImGui.GetColorU32(color), 2f);
        }

        if (hovered)
        {
            ImGui.SetMouseCursor(ImGuiMouseCursor.ResizeAll);
        }

        if (active)
        {
            var io = ImGui.GetIO();
            var delta = io.MouseDelta;
            var newSize = windowSize + delta;
            newSize.X = MathF.Max(newSize.X, 520f);
            newSize.Y = MathF.Max(newSize.Y, 360f);
            newSize.X = MathF.Min(newSize.X, 1400f);
            newSize.Y = MathF.Min(newSize.Y, 980f);
            ImGui.SetWindowSize(newSize);
        }
    }

}
