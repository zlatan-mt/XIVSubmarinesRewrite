// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs
// 概要・通知・開発タブを単一ウィンドウで描画する統合レンダラです
// 複数ウィンドウに分かれていた UI をまとめ、手動リサイズやタブ切替を簡潔に扱うため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiCol = Dalamud.Bindings.ImGui.ImGuiCol;
using ImGuiColorEditFlags = Dalamud.Bindings.ImGui.ImGuiColorEditFlags;
using ImGuiCond = Dalamud.Bindings.ImGui.ImGuiCond;
using ImGuiStyleVar = Dalamud.Bindings.ImGui.ImGuiStyleVar;
using ImGuiTableColumnFlags = Dalamud.Bindings.ImGui.ImGuiTableColumnFlags;
using ImGuiTableFlags = Dalamud.Bindings.ImGui.ImGuiTableFlags;
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
public sealed class MainWindowRenderer : IViewRenderer
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

        this.isVisible = preferences.MainWindowVisible;
        this.showDeveloperTools = preferences.ShowDeveloperTools;
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
            MathF.Max(640f, this.preferences.MainWindowWidth),
            MathF.Max(420f, this.preferences.MainWindowHeight));
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
        ImGui.Separator();
        this.DrawTabBar();
        this.PersistSizeIfNeeded();

        ImGui.End();
        ImGui.PopStyleColor(2);
        this.SetVisibility(openFlag);
    }

    private void DrawToolbar()
    {
        const float toolbarHeight = 60f;
        ImGui.PushStyleColor(ImGuiCol.ChildBg, UiTheme.ToolbarBg);
        ImGui.PushStyleColor(ImGuiCol.Border, UiTheme.ToolbarBorder);
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(14f, 10f));
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(16f, 8f));

        if (ImGui.BeginChild("##main_toolbar", new Vector2(0f, toolbarHeight), true, ImGuiWindowFlags.AlwaysUseWindowPadding | ImGuiWindowFlags.NoScrollbar))
        {
            const ImGuiTableFlags flags = ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.PadOuterX | ImGuiTableFlags.NoSavedSettings | ImGuiTableFlags.NoBordersInBody;
            if (ImGui.BeginTable("toolbar_table", 3, flags))
            {
                ImGui.TableSetupColumn("brand", ImGuiTableColumnFlags.WidthStretch, 1.2f);
                ImGui.TableSetupColumn("metrics", ImGuiTableColumnFlags.WidthStretch, 0.9f);
                ImGui.TableSetupColumn("actions", ImGuiTableColumnFlags.WidthStretch, 0.6f);
                ImGui.TableNextRow();

                ImGui.TableSetColumnIndex(0);
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(10f, 0f));
                ImGui.AlignTextToFramePadding();
                ImGui.TextColored(UiTheme.AccentPrimary, "⛵");
                ImGui.SameLine();
                ImGui.TextColored(UiTheme.ToolbarText, "XIV Submarines");
                ImGui.SameLine();
                ImGui.TextColored(UiTheme.ToolbarMuted, this.versionLabel);
                ImGui.PopStyleVar();

                ImGui.TableSetColumnIndex(1);
                var toolbarContrast = UiTheme.ContrastRatio(UiTheme.ToolbarText, UiTheme.ToolbarBg);
                var windowContrast = UiTheme.ContrastRatio(UiTheme.PrimaryText, UiTheme.WindowBg);
                ImGui.TextColored(UiTheme.ToolbarMuted, $"Toolbar 1:{toolbarContrast:F2}");
                ImGui.TextColored(UiTheme.ToolbarMuted, $"Window 1:{windowContrast:F2}");
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(6f, 6f));
                var swatchFlags = ImGuiColorEditFlags.NoTooltip | ImGuiColorEditFlags.NoDragDrop | ImGuiColorEditFlags.NoPicker;
                var swatchSize = new Vector2(18f, 18f);
                ImGui.ColorButton("##toolbar_bg_swatch", UiTheme.ToolbarBg, swatchFlags, swatchSize);
                ImGui.SameLine();
                ImGui.ColorButton("##toolbar_text_swatch", UiTheme.ToolbarText, swatchFlags, swatchSize);
                ImGui.SameLine();
                ImGui.ColorButton("##window_bg_swatch", UiTheme.WindowBg, swatchFlags, swatchSize);
                ImGui.SameLine();
                ImGui.ColorButton("##primary_text_swatch", UiTheme.PrimaryText, swatchFlags, swatchSize);
                ImGui.PopStyleVar();

                ImGui.TableSetColumnIndex(2);
                var buttonLabel = this.showDeveloperTools ? "DEV • ON" : "DEV • OFF";
                var buttonWidth = 120f;
                var cursorX = ImGui.GetCursorPosX();
                var available = ImGui.GetContentRegionAvail().X;
                ImGui.SetCursorPosX(cursorX + MathF.Max(0f, available - buttonWidth));
                var idleColor = this.showDeveloperTools ? UiTheme.AccentPrimary : UiTheme.ToolbarBg;
                ImGui.PushStyleColor(ImGuiCol.Button, idleColor);
                ImGui.PushStyleColor(ImGuiCol.ButtonHovered, UiTheme.AccentPrimary);
                ImGui.PushStyleColor(ImGuiCol.ButtonActive, UiTheme.AccentPrimary);
                if (ImGui.Button(buttonLabel, new Vector2(buttonWidth, 0f)))
                {
                    this.showDeveloperTools = !this.showDeveloperTools;
                    this.preferences.ShowDeveloperTools = this.showDeveloperTools;
                    this.SavePreferences();
                }

                ImGui.PopStyleColor(3);
                ImGui.SameLine();
                ImGui.TextColored(UiTheme.ToolbarMuted, this.showDeveloperTools ? "開発タブ表示中" : "一般表示");

                ImGui.EndTable();
            }
        }

        ImGui.EndChild();
        ImGui.PopStyleVar(2);
        ImGui.PopStyleColor(2);
    }

    private void DrawTabBar()
    {
        if (!ImGui.BeginTabBar("xsr_main_tabs"))
        {
            return;
        }

        if (!this.showDeveloperTools && this.activeTab == MainWindowTab.Developer)
        {
            this.activeTab = MainWindowTab.Overview;
        }

        var requested = this.pendingSelection;
        if (!this.showDeveloperTools && requested == MainWindowTab.Developer)
        {
            requested = MainWindowTab.Overview;
            this.pendingSelection = MainWindowTab.Overview;
            this.activeTab = MainWindowTab.Overview;
        }

        var overviewFlags = requested == MainWindowTab.Overview ? ImGuiTabItemFlags.SetSelected : ImGuiTabItemFlags.None;
        if (ImGui.BeginTabItem("概要", overviewFlags))
        {
            this.activeTab = MainWindowTab.Overview;
            this.overviewRenderer.RenderTabContent();
            ImGui.EndTabItem();
        }

        var notifyFlags = requested == MainWindowTab.Notifications ? ImGuiTabItemFlags.SetSelected : ImGuiTabItemFlags.None;
        if (ImGui.BeginTabItem("通知", notifyFlags))
        {
            this.activeTab = MainWindowTab.Notifications;
            this.notificationRenderer.RenderNotificationTab();
            ImGui.EndTabItem();
        }

        var developerFlags = requested == MainWindowTab.Developer ? ImGuiTabItemFlags.SetSelected : ImGuiTabItemFlags.None;
        if (this.showDeveloperTools && ImGui.BeginTabItem("開発", developerFlags))
        {
            this.activeTab = MainWindowTab.Developer;
            this.notificationRenderer.RenderDeveloperTab();
            ImGui.EndTabItem();
        }

        ImGui.EndTabBar();
        this.pendingSelection = null;
    }

    private void PersistSizeIfNeeded()
    {
        if (DateTime.UtcNow < this.nextPersistAtUtc)
        {
            return;
        }

        var size = ImGui.GetWindowSize();
        if (Math.Abs(size.X - this.preferences.MainWindowWidth) < 0.5f
            && Math.Abs(size.Y - this.preferences.MainWindowHeight) < 0.5f)
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
