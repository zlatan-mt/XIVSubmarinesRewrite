// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Queue.cs
// 通知キュー描画と設定適用処理を本体ファイルから分離します
// メインファイルを簡潔に保ち、各責務を追いやすくするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Diagnostics.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiTreeNodeFlags = Dalamud.Bindings.ImGui.ImGuiTreeNodeFlags;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Queue rendering helpers for the notification monitor window.</summary>
public sealed partial class NotificationMonitorWindowRenderer
{
    private void RenderQueueSection()
    {
        if (!ImGui.CollapsingHeader("通知キュー", ImGuiTreeNodeFlags.DefaultOpen))
        {
            return;
        }

        this.queueViewModel.Refresh();
        ImGui.Text("保留中: " + this.queueViewModel.Pending.Count.ToString(CultureInfo.InvariantCulture));
        ImGui.Text("デッドレター: " + this.queueViewModel.DeadLetters.Count.ToString(CultureInfo.InvariantCulture));

        ImGui.Separator();
        this.RenderPendingTable();
        ImGui.Spacing();
        this.RenderDeadLetterTable();
    }

    private void RenderPendingTable()
    {
        var pending = this.queueViewModel.Pending;
        if (pending.Count == 0)
        {
            ImGui.TextDisabled("保留中の通知はありません。");
            return;
        }

        if (ImGui.BeginTable("##notify_pending", 5, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp))
        {
            ImGui.TableSetupColumn("ハッシュ", ImGuiTableColumnFlags.WidthStretch, 1.2f);
            ImGui.TableSetupColumn("状態", ImGuiTableColumnFlags.WidthStretch, 0.9f);
            ImGui.TableSetupColumn("試行", ImGuiTableColumnFlags.WidthStretch, 0.6f);
            ImGui.TableSetupColumn("次回試行", ImGuiTableColumnFlags.WidthStretch, 1.3f);
            ImGui.TableSetupColumn("最終エラー", ImGuiTableColumnFlags.WidthStretch, 1.8f);
            ImGui.TableHeadersRow();

            foreach (var item in pending)
            {
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.TextUnformatted(GetShortHash(item.Envelope.HashKey));

                ImGui.TableSetColumnIndex(1);
                ImGui.TextUnformatted(item.State.ToString());

                ImGui.TableSetColumnIndex(2);
                ImGui.TextUnformatted(item.AttemptCount.ToString(CultureInfo.InvariantCulture));

                ImGui.TableSetColumnIndex(3);
                ImGui.TextUnformatted(item.NextAttemptAtUtc.ToLocalTime().ToString("MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

                ImGui.TableSetColumnIndex(4);
                if (string.IsNullOrEmpty(item.LastError))
                {
                    ImGui.TextDisabled("--");
                }
                else
                {
                    ImGui.TextWrapped(item.LastError);
                }
            }

            ImGui.EndTable();
        }
    }

    private void RenderDeadLetterTable()
    {
        var deadLetters = this.queueViewModel.DeadLetters;
        if (deadLetters.Count == 0)
        {
            ImGui.TextDisabled("デッドレターはありません。");
            return;
        }

        if (ImGui.BeginTable("##notify_dead", 5, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp))
        {
            ImGui.TableSetupColumn("ハッシュ", ImGuiTableColumnFlags.WidthStretch, 1.2f);
            ImGui.TableSetupColumn("最終試行", ImGuiTableColumnFlags.WidthStretch, 1.3f);
            ImGui.TableSetupColumn("試行回数", ImGuiTableColumnFlags.WidthStretch, 0.7f);
            ImGui.TableSetupColumn("最終エラー", ImGuiTableColumnFlags.WidthStretch, 2.0f);
            ImGui.TableSetupColumn("再送", ImGuiTableColumnFlags.WidthFixed, 90f);
            ImGui.TableHeadersRow();

            foreach (var item in deadLetters)
            {
                ImGui.TableNextRow();
                ImGui.PushID(item.Envelope.HashKey);

                ImGui.TableSetColumnIndex(0);
                ImGui.TextUnformatted(GetShortHash(item.Envelope.HashKey));

                ImGui.TableSetColumnIndex(1);
                var lastAttempt = item.LastAttemptAtUtc?.ToLocalTime().ToString("MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? "--";
                ImGui.TextUnformatted(lastAttempt);

                ImGui.TableSetColumnIndex(2);
                ImGui.TextUnformatted(item.AttemptCount.ToString(CultureInfo.InvariantCulture));

                ImGui.TableSetColumnIndex(3);
                if (string.IsNullOrEmpty(item.LastError))
                {
                    ImGui.TextDisabled("--");
                }
                else
                {
                    ImGui.TextWrapped(item.LastError);
                }

                ImGui.TableSetColumnIndex(4);
                if (DrawRetryButtonWithTooltip(item))
                {
                    this.queueViewModel.TryRequeue(item.Envelope.HashKey);
                }

                ImGui.PopID();
            }

            ImGui.EndTable();
        }
    }

    private void ApplySettings()
    {
        this.settings.EnableDiscord = this.editingSettings.EnableDiscord;
        this.settings.DiscordWebhookUrl = this.editingSettings.DiscordWebhookUrl;
        this.settings.EnableNotion = this.editingSettings.EnableNotion;
        this.settings.NotionWebhookUrl = this.editingSettings.NotionWebhookUrl;
        this.settings.DeadLetterRetentionLimit = this.editingSettings.DeadLetterRetentionLimit;
        this.settings.ForceNotifyUnderway = this.editingSettings.ForceNotifyUnderway;
        // Note: NotifyVoyageCompleted は Phase 13 で廃止（設定から削除）
        this.settings.NotifyVoyageUnderway = this.editingSettings.NotifyVoyageUnderway;
        this.settings.DiscordBatchWindowSeconds = Math.Clamp(this.editingSettings.DiscordBatchWindowSeconds, 0.5, 15.0);

        this.queueOptions.DeadLetterCapacity = Math.Max(1, this.editingSettings.DeadLetterRetentionLimit);
        this.settingsProvider.SaveAsync(this.settings).GetAwaiter().GetResult();
        this.discordBatcher.UpdateWindow(TimeSpan.FromSeconds(this.settings.DiscordBatchWindowSeconds));
        this.settingsDirty = false;
        this.editingSettings = this.settings.Clone();
        this.LoadBuffersFromSettings();
    }

    private void LoadBuffersFromSettings()
    {
        WriteStringToBuffer(this.settings.DiscordWebhookUrl, this.discordUrlBuffer);
        WriteStringToBuffer(this.settings.NotionWebhookUrl, this.notionUrlBuffer);
        this.editingDiscordBatchWindowSeconds = (float)Math.Clamp(this.settings.DiscordBatchWindowSeconds, 0.5, 15.0);
        this.RevalidateChannelUrls();
    }

    private int TriggerManualNotification()
    {
        var characterId = this.characterRegistry.ActiveCharacterId;
        if (characterId == 0)
        {
            this.queueViewModel.Refresh();
            return 0;
        }

        var snapshot = this.snapshotCache.GetSnapshot(characterId);
        if (snapshot is null)
        {
            this.queueViewModel.Refresh();
            return 0;
        }

        var includeUnderway = this.editingSettings.ForceNotifyUnderway;
        var enqueued = 0;

        foreach (var submarine in snapshot.Submarines)
        {
            foreach (var voyage in submarine.Voyages)
            {
                if (voyage.Status != VoyageStatus.Completed && (!includeUnderway || voyage.Status != VoyageStatus.Underway))
                {
                    continue;
                }

                var arrival = voyage.Arrival ?? DateTime.UtcNow;
                var identity = this.characterRegistry.GetIdentity(snapshot.CharacterId);
                var forceImmediate = includeUnderway; // ensure ForceNotify bypasses aggregator suppression when testing underway cycles
                var envelope = NotificationEnvelope.Create(
                    snapshot.CharacterId,
                    snapshot.CharacterName ?? identity?.Name,
                    snapshot.WorldName ?? identity?.World,
                    submarine.Id,
                    submarine.Name,
                    voyage.RouteId,
                    voyage.Id,
                    voyage.Departure,
                    arrival,
                    voyage.Status,
                    snapshot.Confidence,
                    forceImmediate);

                if (this.queue.TryEnqueue(envelope, forceDuplicate: true))
                {
                    enqueued++;
                }
            }
        }

        if (enqueued > 0)
        {
            this.queueViewModel.Refresh();
        }

        return enqueued;
    }

    private static void WriteStringToBuffer(string? value, byte[] buffer)
    {
        Array.Clear(buffer);
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        var bytes = Encoding.UTF8.GetBytes(value);
        Array.Copy(bytes, buffer, Math.Min(bytes.Length, buffer.Length - 1));
    }

    private static string ExtractString(byte[] buffer)
    {
        var text = Encoding.UTF8.GetString(buffer);
        var nullIndex = text.IndexOf('\0');
        if (nullIndex >= 0)
        {
            text = text[..nullIndex];
        }

        return text.Trim();
    }

    private static string GetShortHash(string hash)
        => hash.Length <= 8 ? hash : hash[..8];
}
