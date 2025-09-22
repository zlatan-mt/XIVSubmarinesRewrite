// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Diagnostics.cs
// ForceNotifyUnderway の診断表示を `NotificationMonitorWindowRenderer` から分離します
// 通知ウィンドウにクールダウン情報を提示し、利用者が挙動を把握できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/IForceNotifyDiagnostics.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiTreeNodeFlags = Dalamud.Bindings.ImGui.ImGuiTreeNodeFlags;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>ForceNotifyUnderway diagnostic rendering helpers.</summary>
public sealed partial class NotificationMonitorWindowRenderer
{
    private void RenderForceNotifyDiagnosticsSection()
    {
        if (!ImGui.CollapsingHeader("ForceNotifyUnderway 状態", ImGuiTreeNodeFlags.DefaultOpen))
        {
            return;
        }

        var enabled = this.forceNotifyDiagnostics.IsForceNotifyEnabled;
        var snapshots = this.forceNotifyDiagnostics.GetForceNotifySnapshot();

        if (!enabled)
        {
            ImGui.TextDisabled("ForceNotifyUnderway は無効です。");
            if (snapshots.Count > 0)
            {
                ImGui.TextColored(new Vector4(0.95f, 0.76f, 0.3f, 1f), "無効化後もクールダウン情報が残っています");
            }
            return;
        }

        if (snapshots.Count == 0)
        {
            ImGui.TextDisabled("アクティブな ForceNotify 状態はありません。");
            ImGui.TextDisabled("dalamud.log の ForceNotifyUnderway Trace を併せて確認してください。");
            return;
        }

        const ImGuiTableFlags flags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp;
        if (!ImGui.BeginTable("##force_notify_states", 5, flags))
        {
            return;
        }

        ImGui.TableSetupColumn("艦", ImGuiTableColumnFlags.WidthStretch, 1.2f);
        ImGui.TableSetupColumn("残り", ImGuiTableColumnFlags.WidthStretch, 0.8f);
        ImGui.TableSetupColumn("クールダウン終了", ImGuiTableColumnFlags.WidthStretch, 1.3f);
        ImGui.TableSetupColumn("最終到着", ImGuiTableColumnFlags.WidthStretch, 1.2f);
        ImGui.TableSetupColumn("理由", ImGuiTableColumnFlags.WidthStretch, 1.6f);
        ImGui.TableHeadersRow();

        foreach (var entry in snapshots.OrderBy(s => s.CooldownUntilUtc))
        {
            ImGui.TableNextRow();

            ImGui.TableSetColumnIndex(0);
            ImGui.TextUnformatted(this.FormatSubmarineLabel(entry.SubmarineId));

            ImGui.TableSetColumnIndex(1);
            ImGui.TextUnformatted(FormatRemaining(entry.Remaining));

            ImGui.TableSetColumnIndex(2);
            ImGui.TextUnformatted(this.FormatLocal(entry.CooldownUntilUtc));

            ImGui.TableSetColumnIndex(3);
            ImGui.TextUnformatted(entry.LastArrivalUtc.HasValue ? this.FormatLocal(entry.LastArrivalUtc.Value) : "--");

            ImGui.TableSetColumnIndex(4);
            ImGui.TextWrapped(string.IsNullOrWhiteSpace(entry.Reason) ? "--" : entry.Reason);
        }

        ImGui.EndTable();
    }

    private string FormatSubmarineLabel(SubmarineId id)
    {
        var snapshot = this.snapshotCache.GetSnapshot(id.CharacterId);
        if (snapshot is not null)
        {
            var submarine = snapshot.Submarines.FirstOrDefault(s => s.Id.Equals(id));
            if (submarine is not null && !string.IsNullOrWhiteSpace(submarine.Name))
            {
                return submarine.Name;
            }
        }

        return id.IsPending ? "Pending" : $"Slot {id.Slot}";
    }

    private string FormatLocal(DateTime timestampUtc)
    {
        var local = TimeZoneInfo.ConvertTimeFromUtc(timestampUtc, TimeZoneInfo.Local);
        return local.ToString("M/d(ddd) HH:mm:ss", CultureInfo.CurrentCulture);
    }

    private static string FormatRemaining(TimeSpan span)
    {
        if (span <= TimeSpan.Zero)
        {
            return "0分";
        }

        if (span.TotalHours >= 1)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}時間 {1}分", (int)span.TotalHours, span.Minutes);
        }

        return string.Format(CultureInfo.InvariantCulture, "{0}分", Math.Max(1, span.Minutes));
    }
}

