// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs
// 概要・通知・開発タブを単一ウィンドウで描画する統合レンダラです
// 複数ウィンドウに分かれていた UI をまとめ、手動リサイズやタブ切替を簡潔に扱うため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.Layout.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiCol = Dalamud.Bindings.ImGui.ImGuiCol;
using ImGuiCond = Dalamud.Bindings.ImGui.ImGuiCond;
using ImGuiWindowFlags = Dalamud.Bindings.ImGui.ImGuiWindowFlags;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Diagnostics;

/// <summary>Tabs rendered in the consolidated main window.</summary>
public enum MainWindowTab
{
    Overview,
    Notifications,
    Developer,
}

/// <summary>Renders the consolidated main window containing overview and notification tabs.</summary>
public sealed partial class MainWindowRenderer : IViewRenderer
{
    private readonly OverviewWindowRenderer overviewRenderer;
    private readonly NotificationMonitorWindowRenderer notificationRenderer;
    private readonly UiPreferences preferences;
    private readonly ISettingsProvider settingsProvider;
    private readonly string versionLabel;

    private bool isVisible;
    private MainWindowTab activeTab;
    private bool showDeveloperTools;
    private MainWindowTab? pendingSelection;
    private DateTime nextPersistAtUtc = DateTime.MinValue;

    public MainWindowRenderer(
        OverviewWindowRenderer overviewRenderer,
        NotificationMonitorWindowRenderer notificationRenderer,
        UiPreferences preferences,
        ISettingsProvider settingsProvider)
    {
        this.overviewRenderer = overviewRenderer;
        this.notificationRenderer = notificationRenderer;
        this.preferences = preferences;
        this.settingsProvider = settingsProvider;
        this.versionLabel = BuildMetadata.DisplayVersion;

        this.isVisible = preferences.MainWindowVisible ?? true;
        this.showDeveloperTools = preferences.ShowDeveloperTools ?? false;
        this.activeTab = MainWindowTab.Overview;
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
            this.preferences.MainWindowVisible = value;
            this.SavePreferences();
        }
    }

    public void Open(MainWindowTab tab)
    {
        this.pendingSelection = tab;
        this.activeTab = tab;
        this.IsVisible = true;
    }

    public void Toggle(MainWindowTab tab)
    {
        if (!this.IsVisible)
        {
            this.pendingSelection = tab;
            this.activeTab = tab;
            this.IsVisible = true;
            return;
        }

        if (this.activeTab != tab)
        {
            this.pendingSelection = tab;
            this.activeTab = tab;
            return;
        }

        this.pendingSelection = null;
        this.IsVisible = false;
    }

    public void Render()
    {
        if (!this.IsVisible)
        {
            return;
        }

        var initialSize = new Vector2(
            MathF.Max(640f, this.preferences.MainWindowWidth ?? 780f),
            MathF.Max(420f, this.preferences.MainWindowHeight ?? 520f));
        ImGui.SetNextWindowSize(initialSize, ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(new Vector2(640f, 420f), new Vector2(1100f, 860f));
        ImGui.PushStyleColor(ImGuiCol.WindowBg, UiTheme.WindowBg);
        ImGui.PushStyleColor(ImGuiCol.Border, UiTheme.SurfaceBorder);

        var openFlag = this.isVisible;
        if (!ImGui.Begin("XIV Submarines — Control", ref openFlag, ImGuiWindowFlags.None))
        {
            ImGui.End();
            ImGui.PopStyleColor(2);
            this.SetVisibility(openFlag);
            return;
        }

        this.DrawToolbar();
        this.DrawDeveloperSummary();
        ImGui.Separator();
        this.DrawTabBar();
        this.PersistSizeIfNeeded();

        ImGui.End();
        ImGui.PopStyleColor(2);
        this.SetVisibility(openFlag);
    }

    private void PersistSizeIfNeeded()
    {
        if (DateTime.UtcNow < this.nextPersistAtUtc)
        {
            return;
        }

        var size = ImGui.GetWindowSize();
        if (Math.Abs(size.X - (this.preferences.MainWindowWidth ?? 780f)) < 0.5f
            && Math.Abs(size.Y - (this.preferences.MainWindowHeight ?? 520f)) < 0.5f)
        {
            return;
        }

        this.preferences.MainWindowWidth = size.X;
        this.preferences.MainWindowHeight = size.Y;
        this.SavePreferences();
        this.nextPersistAtUtc = DateTime.UtcNow.AddSeconds(1);
    }

    private void SetVisibility(bool value)
    {
        if (this.isVisible == value)
        {
            return;
        }

        this.isVisible = value;
        this.preferences.MainWindowVisible = value;
        this.SavePreferences();
    }

    private void SavePreferences()
    {
        try
        {
            this.settingsProvider.SaveAsync(this.preferences).GetAwaiter().GetResult();
        }
        catch
        {
            // 無視: Dalamud 側が破棄中でも UI を落とさない
        }
    }
}
