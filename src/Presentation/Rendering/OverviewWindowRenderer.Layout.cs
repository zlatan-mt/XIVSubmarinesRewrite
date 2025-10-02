// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.Layout.cs
// Overview ウィンドウのレイアウト計算と描画補助をまとめた部分クラスです
// 列幅のプリセット管理とコンパクト表示を分離し、メインクラスの行数を抑えるため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewRowFormatter.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Presentation.ViewModels;

public sealed partial class OverviewWindowRenderer
{
    private const float CompactWidthThreshold = 720f;

    public void RenderTabContent()
    {
        this.RenderOverviewContent(includeNotificationTab: false);
    }

    private void RenderOverviewContent(bool includeNotificationTab)
    {
        ImGui.TextDisabled(this.versionLabel);
        ImGui.Separator();

        var descriptors = this.characterRegistry.Characters;
        if (descriptors.Count == 0)
        {
            ImGui.Text("キャラクターのデータがまだありません。");
            ImGui.TextDisabled("工房 UI を開き、スナップショットを取得してください。");
            return;
        }

        if (!this.TryEnsureSelection(descriptors))
        {
            return;
        }

        var displayName = descriptors.First(d => d.CharacterId == this.selectedCharacterId).DisplayName;
        if (ImGui.BeginCombo("キャラクター", displayName))
        {
            foreach (var descriptor in descriptors)
            {
                var isSelected = descriptor.CharacterId == this.selectedCharacterId;
                if (ImGui.Selectable(descriptor.DisplayName, isSelected))
                {
                    this.selectedCharacterId = descriptor.CharacterId;
                    this.characterRegistry.SelectCharacter(this.selectedCharacterId);
                    this.preferences.LastSelectedCharacterId = this.selectedCharacterId;
                    this.settings.SaveAsync(this.preferences).GetAwaiter().GetResult();
                }

                if (isSelected)
                {
                    ImGui.SetItemDefaultFocus();
                }
            }

            ImGui.EndCombo();
        }

        ImGui.Separator();

        if (includeNotificationTab)
        {
            if (ImGui.BeginTabBar("xsr_overview_tabs"))
            {
                if (ImGui.BeginTabItem("潜水艦"))
                {
                    this.RenderSubmarineTab(includeNotificationShortcut: true);
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("通知"))
                {
                    this.notificationWindow.RenderInline();
                    ImGui.EndTabItem();
                }

                ImGui.EndTabBar();
            }
        }
        else
        {
            this.RenderSubmarineTab(includeNotificationShortcut: false);
        }
    }

    private void RenderSubmarineTab(bool includeNotificationShortcut)
    {
        var submarines = this.viewModel.Submarines;
        if (submarines.Count == 0)
        {
            ImGui.Text("潜水艦データがまだありません。");
            ImGui.TextDisabled("工房 UI を開くか、メモリ取得が完了すると一覧が更新されます。");
            return;
        }

        var regionWidth = ImGui.GetContentRegionAvail().X;
        var preset = OverviewColumnPreset.ForWidth(regionWidth);
        var useCompact = regionWidth < CompactWidthThreshold;

        if (!includeNotificationShortcut)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.TextColored(UiTheme.MutedText, "通知フォームを開く");
            ImGui.SameLine();
            if (ImGui.SmallButton("通知設定ウィンドウを開く"))
            {
                this.notificationWindow.IsVisible = true;
            }
        }

        const ImGuiTableFlags flags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp;
        this.RenderSummaryRow(submarines, useCompact);
        if (ImGui.BeginTable("##xsr_overview_table", 5, flags))
        {
            ImGui.TableSetupColumn("艦名", ImGuiTableColumnFlags.WidthStretch, preset.NameWeight);
            ImGui.TableSetupColumn("状態", ImGuiTableColumnFlags.WidthStretch, preset.StatusWeight);
            ImGui.TableSetupColumn("残り時間", ImGuiTableColumnFlags.WidthStretch, preset.RemainingWeight);
            ImGui.TableSetupColumn("帰港予定", ImGuiTableColumnFlags.WidthStretch, preset.ArrivalWeight);
            ImGui.TableSetupColumn("航路", ImGuiTableColumnFlags.WidthStretch, preset.RouteWeight);
            ImGui.TableHeadersRow();

            foreach (var submarine in submarines)
            {
                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                ImGui.TextUnformatted(OverviewRowFormatter.GetDisplayName(submarine));

                ImGui.TableSetColumnIndex(1);
                ImGui.TextUnformatted(OverviewRowFormatter.FormatStatus(submarine.Status));

                ImGui.TableSetColumnIndex(2);
                var remainingText = useCompact
                    ? OverviewRowFormatter.FormatRemainingCompact(submarine.Remaining)
                    : OverviewRowFormatter.FormatRemaining(submarine.Remaining);
                ImGui.TextUnformatted(remainingText);

                ImGui.TableSetColumnIndex(3);
                var arrivalText = useCompact
                    ? OverviewRowFormatter.FormatArrivalCompact(submarine.Arrival)
                    : OverviewRowFormatter.FormatArrival(submarine.Arrival);
                ImGui.TextUnformatted(arrivalText);

                ImGui.TableSetColumnIndex(4);
                ImGui.PushID(submarine.SubmarineId.ToString());
                var rawRoute = string.IsNullOrWhiteSpace(submarine.RouteId)
                    ? "--"
                    : this.routeCatalog.FormatRoute(submarine.RouteId);
                if (string.IsNullOrWhiteSpace(rawRoute) || rawRoute == "--")
                {
                    ImGui.TextDisabled("--");
                }
                else
                {
                    var wrapped = OverviewRowFormatter.BuildWrappedRouteLabel(rawRoute);
                    ImGui.PushTextWrapPos(ImGui.GetCursorPosX() + ImGui.GetColumnWidth());
                    ImGui.TextUnformatted(wrapped);
                    ImGui.PopTextWrapPos();
                    if (ImGui.SmallButton("コピー"))
                    {
                        var copyPayload = OverviewRowFormatter.BuildCopyLine(submarine, rawRoute);
                        ImGui.SetClipboardText(copyPayload);
                    }
                }
                ImGui.PopID();
            }

            ImGui.EndTable();
        }
    }

    private void RenderSummaryRow(IReadOnlyList<SubmarineOverviewEntry> submarines, bool useCompact)
    {
        var nowUtc = DateTime.UtcNow;
        var nextArrivalEntry = submarines
            .Where(s => s.Arrival.HasValue && s.Arrival.Value > nowUtc)
            .OrderBy(s => s.Arrival)
            .FirstOrDefault();

        var nextArrivalLabel = "--";
        if (nextArrivalEntry?.Arrival is DateTime arrivalUtc)
        {
            nextArrivalLabel = useCompact
                ? arrivalUtc.ToLocalTime().ToString("M/d(ddd) HH:mm", CultureInfo.InvariantCulture)
                : arrivalUtc.ToLocalTime().ToString("M/d(ddd) HH:mm", CultureInfo.InvariantCulture);
        }

        var underway = submarines.Count(s => s.Status == VoyageStatus.Underway);
        var completed = submarines.Count(s => s.Status == VoyageStatus.Completed);

        var lastUpdatedUtc = this.viewModel.LastUpdatedUtc;
        var lastUpdatedLabel = lastUpdatedUtc.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(lastUpdatedUtc.Value, TimeZoneInfo.Local).ToString(useCompact ? "HH:mm:ss" : "M/d(ddd) HH:mm:ss", CultureInfo.InvariantCulture)
            : "--";

        var nextArrivalText = useCompact ? "次の帰港" : "次の帰港: " + nextArrivalLabel;
        if (useCompact)
        {
            ImGui.TextColored(UiTheme.AccentPrimary, $"次の帰港 {nextArrivalLabel}");
        }
        else if (nextArrivalEntry is not null)
        {
            ImGui.TextColored(UiTheme.AccentPrimary, nextArrivalText);
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, nextArrivalText);
        }

        ImGui.SameLine();
        ImGui.TextColored(UiTheme.MutedText, $"航行中 {underway}/{submarines.Count}");
        ImGui.SameLine();
        ImGui.TextColored(UiTheme.MutedText, "最終更新: " + lastUpdatedLabel);
        ImGui.Spacing();
    }

    private bool TryEnsureSelection(IReadOnlyList<CharacterDescriptor> descriptors)
    {
        if (this.selectedCharacterId != 0 && descriptors.Any(d => d.CharacterId == this.selectedCharacterId))
        {
            return true;
        }

        if (this.characterRegistry.ActiveCharacterId != 0 && descriptors.Any(d => d.CharacterId == this.characterRegistry.ActiveCharacterId))
        {
            this.selectedCharacterId = this.characterRegistry.ActiveCharacterId;
            return true;
        }

        if (descriptors.Count > 0)
        {
            this.selectedCharacterId = descriptors[0].CharacterId;
            this.characterRegistry.SelectCharacter(this.selectedCharacterId);
            return true;
        }

        return false;
    }

    private readonly struct OverviewColumnPreset
    {
        public OverviewColumnPreset(float name, float status, float remaining, float arrival, float route)
        {
            this.NameWeight = name;
            this.StatusWeight = status;
            this.RemainingWeight = remaining;
            this.ArrivalWeight = arrival;
            this.RouteWeight = route;
        }

        public float NameWeight { get; }
        public float StatusWeight { get; }
        public float RemainingWeight { get; }
        public float ArrivalWeight { get; }
        public float RouteWeight { get; }

        public static OverviewColumnPreset ForWidth(float width)
        {
            if (width >= 960f)
            {
                return new OverviewColumnPreset(2.0f, 1.0f, 1.0f, 1.2f, 1.8f);
            }

            if (width >= 780f)
            {
                return new OverviewColumnPreset(1.8f, 0.9f, 0.9f, 1.1f, 1.5f);
            }

            return new OverviewColumnPreset(1.6f, 0.8f, 0.8f, 0.9f, 1.4f);
        }
    }
}
