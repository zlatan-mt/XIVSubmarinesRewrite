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
public sealed partial class OverviewWindowRenderer : IViewRenderer
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
        this.isVisible = this.preferences.OverviewWindowVisible ?? false;
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
