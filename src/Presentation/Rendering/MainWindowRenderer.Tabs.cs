// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.Tabs.cs
// MainWindowRenderer のタブ描画とタブ切替を担当します
// 部分クラスとして分離し、300行制限を守るため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using Dalamud.Bindings.ImGui;
using ImGui = Dalamud.Bindings.ImGui.ImGui;
using ImGuiTabItemFlags = Dalamud.Bindings.ImGui.ImGuiTabItemFlags;

/// <summary>Tab bar and tab switching methods.</summary>
public sealed partial class MainWindowRenderer
{
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
}

