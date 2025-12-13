// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.ChannelCards.cs
// 通知チャネルカードの描画と URL バリデーションを担当します
// Discord 設定を共通処理へ集約し、保存前に入力チェックを行うため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SettingsLayout.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiInputTextFlags = Dalamud.Bindings.ImGui.ImGuiInputTextFlags;
using ImGuiStyleVar = Dalamud.Bindings.ImGui.ImGuiStyleVar;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiTableRowFlags = Dalamud.Bindings.ImGui.ImGuiTableRowFlags;

public sealed partial class NotificationMonitorWindowRenderer
{
    private enum NotificationChannel
    {
        Discord,
    }

    private readonly struct ChannelCardResult
    {
        public ChannelCardResult(bool changed, bool enabled, bool isValid, string? errorMessage)
        {
            this.Changed = changed;
            this.Enabled = enabled;
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
        }

        public bool Changed { get; }
        public bool Enabled { get; }
        public bool IsValid { get; }
        public string? ErrorMessage { get; }
    }

    private bool RenderChannelCards(in NotificationLayoutMetrics metrics, ref bool discordEnabled)
    {
        var changed = false;
        const ImGuiTableFlags flags = ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.PadOuterX | ImGuiTableFlags.NoSavedSettings;
        var columns = Math.Max(1, metrics.ColumnCount);
        if (!ImGui.BeginTable("channel_cards", columns, flags))
        {
            return false;
        }

        var totalChannels = 1;
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
                var result = this.RenderChannelCard(NotificationChannel.Discord, ref discordEnabled, this.discordUrlBuffer, value => this.editingSettings.DiscordWebhookUrl = value, metrics.CardHeight);
                changed |= result.Changed;
                discordEnabled = result.Enabled;
                this.discordUrlValid = result.IsValid;
                this.discordUrlError = result.ErrorMessage;
            }
        }

        ImGui.EndTable();
        return changed;
    }

    private ChannelCardResult RenderChannelCard(NotificationChannel channel, ref bool enabled, byte[] buffer, Action<string> setter, float preferredHeight)
    {
        var changed = false;
        var id = "discord";
        var title = "Discord";

        ImGui.PushID(id);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(8f, 6f));
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(6f, 4f));
        var childFlags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.AlwaysUseWindowPadding;
        var cardHeight = MathF.Max(preferredHeight, ChannelCardMinHeight);
        bool isValid;
        string? errorMessage;
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

            var urlText = ExtractString(buffer);
            var validation = NotificationWebhookValidator.ValidateDiscord(enabled, urlText);
            isValid = validation.IsValid;
            errorMessage = validation.ErrorMessage;
            if (!isValid)
            {
                ImGui.TextColored(UiTheme.ErrorText, errorMessage ?? "入力を確認してください。");
            }
        }
        else
        {
            var urlText = ExtractString(buffer);
            var validation = NotificationWebhookValidator.ValidateDiscord(enabled, urlText);
            isValid = validation.IsValid;
            errorMessage = validation.ErrorMessage;
        }

        ImGui.EndChild();
        ImGui.PopStyleVar(2);
        ImGui.PopID();
        return new ChannelCardResult(changed, enabled, isValid, errorMessage);
    }

    private bool RenderUrlInput(string label, byte[] buffer, Action<string> setValue)
    {
        var modified = ImGui.InputText(label, buffer, ImGuiInputTextFlags.None);
        if (modified)
        {
            var text = ExtractString(buffer);
            setValue(text);
        }

        return modified;
    }

    private void RevalidateChannelUrls()
    {
        var discord = ExtractString(this.discordUrlBuffer);
        var discordValidation = NotificationWebhookValidator.ValidateDiscord(this.editingSettings.EnableDiscord, discord);
        this.discordUrlValid = discordValidation.IsValid;
        this.discordUrlError = discordValidation.ErrorMessage;
    }
}

internal readonly struct NotificationChannelValidationResult
{
    public NotificationChannelValidationResult(bool isValid, string? errorMessage)
    {
        this.IsValid = isValid;
        this.ErrorMessage = errorMessage;
    }

    public bool IsValid { get; }

    public string? ErrorMessage { get; }
}

internal static class NotificationWebhookValidator
{
    public static NotificationChannelValidationResult ValidateDiscord(bool enabled, string url)
        => Validate(enabled, url, requireDiscordDomain: true);

    private static NotificationChannelValidationResult Validate(bool enabled, string url, bool requireDiscordDomain)
    {
        if (!enabled)
        {
            return new NotificationChannelValidationResult(true, null);
        }

        if (string.IsNullOrWhiteSpace(url))
        {
            return new NotificationChannelValidationResult(false, "URL を入力してください。");
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || uri.Scheme != Uri.UriSchemeHttps)
        {
            return new NotificationChannelValidationResult(false, "https:// から始まる URL を入力してください。");
        }

        if (requireDiscordDomain && !url.Contains("discord", StringComparison.OrdinalIgnoreCase))
        {
            return new NotificationChannelValidationResult(false, "Discord Webhook の URL を入力してください。");
        }

        return new NotificationChannelValidationResult(true, null);
    }
}