// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs
// 概要ウィンドウの描画と表示状態の永続化を担うレンダラです
// ユーザーが求める情報を必要なタイミングでシンプルに確認できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/UiPreferences.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Routes/RouteCatalog.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
using ImGuiWindowFlags = Dalamud.Bindings.ImGui.ImGuiWindowFlags;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Diagnostics;
using XIVSubmarinesRewrite.Infrastructure.Routes;
using XIVSubmarinesRewrite.Presentation.ViewModels;

/// <summary>Renders the main overview window that end users interact with.</summary>
public sealed class OverviewWindowRenderer : IViewRenderer
{
    private readonly IOverviewViewModel viewModel;
    private readonly ICharacterRegistry characterRegistry;
    private readonly NotificationMonitorWindowRenderer notificationWindow;
    private readonly UiPreferences preferences;
    private readonly ISettingsProvider settings;
    private readonly RouteCatalog routeCatalog;
    private readonly string versionLabel;
    private ulong selectedCharacterId;
    private bool isVisible;

    public OverviewWindowRenderer(
        IOverviewViewModel viewModel,
        ICharacterRegistry characterRegistry,
        NotificationMonitorWindowRenderer notificationWindow,
        ISettingsProvider settings,
        RouteCatalog routeCatalog)
    {
        this.viewModel = viewModel;
        this.characterRegistry = characterRegistry;
        this.notificationWindow = notificationWindow;
        this.settings = settings;
        this.routeCatalog = routeCatalog;
        this.preferences = settings.Get<UiPreferences>();
        this.versionLabel = BuildMetadata.DisplayVersion;
        this.isVisible = this.preferences.OverviewWindowVisible;
        this.selectedCharacterId = this.preferences.LastSelectedCharacterId ?? this.characterRegistry.ActiveCharacterId;
        this.characterRegistry.ActiveCharacterChanged += this.OnActiveCharacterChanged;
        this.characterRegistry.CharacterListChanged += this.OnCharacterListChanged;
    }

    public bool IsVisible
    {
        get => this.isVisible;
        set
        {
            if (this.isVisible == value)
            {
                return;
            }

            this.isVisible = value;
            this.PersistVisibility();
        }
    }

    public void Render()
    {
        if (!this.isVisible)
        {
            return;
        }

        var title = "XIV Submarines — Overview";
        var wasVisible = this.isVisible;
        if (!ImGui.Begin(title, ref this.isVisible, ImGuiWindowFlags.None))
        {
            ImGui.End();
            if (wasVisible != this.isVisible)
            {
                this.PersistVisibility();
            }
            return;
        }

        this.RenderOverviewContent(includeNotificationTab: true);

        ImGui.End();

        if (wasVisible != this.isVisible)
        {
            this.PersistVisibility();
        }
    }

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
                    this.RenderSubmarineTab();
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
            this.RenderSubmarineTab();
        }
    }

    private void RenderSubmarineTab()
    {
        var submarines = this.viewModel.Submarines;
        if (submarines.Count == 0)
        {
            ImGui.Text("潜水艦データがまだありません。");
            ImGui.TextDisabled("工房 UI を開くか、メモリ取得が完了すると一覧が更新されます。");
            return;
        }

        const ImGuiTableFlags flags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp;
        this.RenderSummaryRow(submarines);
        if (ImGui.BeginTable("##xsr_overview_table", 5, flags))
        {
            ImGui.TableSetupColumn("艦名", ImGuiTableColumnFlags.WidthStretch, 2.0f);
            ImGui.TableSetupColumn("状態", ImGuiTableColumnFlags.WidthStretch, 1.0f);
            ImGui.TableSetupColumn("残り時間", ImGuiTableColumnFlags.WidthStretch, 1.0f);
            ImGui.TableSetupColumn("帰港予定", ImGuiTableColumnFlags.WidthStretch, 1.2f);
            ImGui.TableSetupColumn("航路", ImGuiTableColumnFlags.WidthStretch, 1.8f);
            ImGui.TableHeadersRow();

            foreach (var submarine in submarines)
            {
                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                ImGui.TextUnformatted(OverviewRowFormatter.GetDisplayName(submarine));

                ImGui.TableSetColumnIndex(1);
                ImGui.TextUnformatted(OverviewRowFormatter.FormatStatus(submarine.Status));

                ImGui.TableSetColumnIndex(2);
                ImGui.TextUnformatted(OverviewRowFormatter.FormatRemaining(submarine.Remaining));

                ImGui.TableSetColumnIndex(3);
                ImGui.TextUnformatted(OverviewRowFormatter.FormatArrival(submarine.Arrival));

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

    public void Toggle()
    {
        this.isVisible = !this.isVisible;
        this.PersistVisibility();
    }

    public void Show()
    {
        this.isVisible = true;
        this.PersistVisibility();
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

    private void OnActiveCharacterChanged(object? sender, CharacterChangedEventArgs e)
    {
        if (e.CharacterId != 0)
        {
            this.selectedCharacterId = e.CharacterId;
        }
    }

    private void OnCharacterListChanged(object? sender, EventArgs e)
    {
        var descriptors = this.characterRegistry.Characters;
        if (!descriptors.Any(d => d.CharacterId == this.selectedCharacterId) && descriptors.Count > 0)
        {
            this.selectedCharacterId = descriptors[0].CharacterId;
        }
    }

    private void PersistVisibility()
    {
        this.preferences.OverviewWindowVisible = this.isVisible;
        this.settings.SaveAsync(this.preferences).GetAwaiter().GetResult();
    }

    private void RenderSummaryRow(IReadOnlyList<SubmarineOverviewEntry> submarines)
    {
        var nowUtc = DateTime.UtcNow;
        var nextArrivalEntry = submarines
            .Where(s => s.Arrival.HasValue && s.Arrival.Value > nowUtc)
            .OrderBy(s => s.Arrival)
            .FirstOrDefault();

        var nextArrivalLabel = "--";
        if (nextArrivalEntry?.Arrival is DateTime arrivalUtc)
        {
            nextArrivalLabel = arrivalUtc.ToLocalTime().ToString("M/d(ddd) HH:mm", CultureInfo.CurrentCulture);
        }

        var underway = submarines.Count(s => s.Status == VoyageStatus.Underway);
        var completed = submarines.Count(s => s.Status == VoyageStatus.Completed);

        var lastUpdatedUtc = this.viewModel.LastUpdatedUtc;
        var lastUpdatedLabel = lastUpdatedUtc.HasValue
            ? TimeZoneInfo.ConvertTimeFromUtc(lastUpdatedUtc.Value, TimeZoneInfo.Local).ToString("M/d(ddd) HH:mm:ss", CultureInfo.CurrentCulture)
            : "--";

        var nextArrivalText = "次の帰港: " + nextArrivalLabel;
        if (nextArrivalEntry is not null)
        {
            ImGui.TextColored(UiTheme.AccentPrimary, nextArrivalText);
        }
        else
        {
            ImGui.TextColored(UiTheme.MutedText, nextArrivalText);
        }

        ImGui.SameLine();
        ImGui.TextColored(UiTheme.MutedText, $"航行中 {underway}/{submarines.Count}");
        ImGui.TextColored(UiTheme.MutedText, "最終更新: " + lastUpdatedLabel);
        ImGui.Spacing();
    }
}
