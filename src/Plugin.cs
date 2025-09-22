// apps/XIVSubmarinesRewrite/src/Plugin.cs
// Dalamud エントリポイントとして初期化と UI ハンドラを担当します
// プラグインの各機能を結線し、ユーザー操作から呼び出せるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Composition/PluginBootstrapper.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

namespace XIVSubmarinesRewrite;

using System;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Infrastructure.Acquisition;
using XIVSubmarinesRewrite.Infrastructure.Composition;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Infrastructure.Routes;
using XIVSubmarinesRewrite.Presentation.Rendering;
using XIVSubmarinesRewrite.Presentation.ViewModels;

/// <summary>Dalamud entry point that wires Dalamud services into the rewrite bootstrapper.</summary>
public sealed class Plugin : IDalamudPlugin
{
    private readonly PluginBootstrapper bootstrapper;
    private readonly SnapshotOrchestrator orchestrator;
    private readonly AcquisitionOptions options;
    private readonly LowImpactModeController modeController;
    private readonly OverviewWindowRenderer overviewWindow;
    private readonly NotificationMonitorWindowRenderer notificationWindow;
    private const string CommandRoot = "/xsr";
    private DateTime nextRefreshAt = DateTime.MinValue;
    private bool initialUiRequestHandled;

    [PluginService] private static IFramework Framework { get; set; } = null!;
    [PluginService] private static IClientState ClientState { get; set; } = null!;
    [PluginService] private static IGameGui GameGui { get; set; } = null!;
    [PluginService] private static IAddonLifecycle AddonLifecycle { get; set; } = null!;
    [PluginService] private static IPluginLog PluginLog { get; set; } = null!;
    [PluginService] private static IDalamudPluginInterface PluginInterface { get; set; } = null!;
    [PluginService] private static ICommandManager CommandManager { get; set; } = null!;
    [PluginService] private static IDataManager DataManager { get; set; } = null!;

    public string Name => "XIV Submarines Rewrite";

    public Plugin()
    {
        var log = new DalamudPluginLogSink(PluginLog);
        log.Log(LogLevel.Information, "[Plugin] AddonLifecycle type=" + (AddonLifecycle?.GetType().FullName ?? "null"));
        var memorySource = new DalamudMemorySubmarineSnapshotSource(ClientState, log);
        var uiSource = new DalamudUiSubmarineSnapshotSource(AddonLifecycle, GameGui, ClientState, log);
        this.bootstrapper = new PluginBootstrapper(ClientState, memorySource, uiSource, log, Framework, PluginInterface, DataManager);
        this.orchestrator = this.bootstrapper.Services.Resolve<SnapshotOrchestrator>();
        this.options = this.bootstrapper.Services.Resolve<AcquisitionOptions>();
        this.modeController = this.bootstrapper.Services.Resolve<LowImpactModeController>();

        var overview = this.bootstrapper.Services.Resolve<IOverviewViewModel>();
        var characterRegistry = this.bootstrapper.Services.Resolve<ICharacterRegistry>();
        this.notificationWindow = this.bootstrapper.Services.Resolve<NotificationMonitorWindowRenderer>();
        var settingsProvider = this.bootstrapper.Services.Resolve<ISettingsProvider>();
        var routeCatalog = this.bootstrapper.Services.Resolve<RouteCatalog>();
        var initialCid = characterRegistry.ActiveCharacterId != 0 ? characterRegistry.ActiveCharacterId : ClientState.LocalContentId;
        if (initialCid != 0)
        {
            characterRegistry.SelectCharacter(initialCid);
            _ = overview.RefreshAsync(initialCid);
        }

        this.overviewWindow = new OverviewWindowRenderer(overview, characterRegistry, this.notificationWindow, settingsProvider, routeCatalog);

        Framework.Update += this.OnFrameworkUpdate;
        ClientState.Login += this.OnClientLogin;
        PluginInterface.UiBuilder.Draw += this.DrawUi;
        PluginInterface.UiBuilder.Draw += this.DrawNotificationWindow;
        PluginInterface.UiBuilder.OpenMainUi += this.ShowUi;
        PluginInterface.UiBuilder.OpenConfigUi += this.ShowUi;
        CommandManager.AddHandler(CommandRoot, new CommandInfo(this.OnCommand)
        {
            HelpMessage = "XIV Submarines Rewrite のウィンドウを表示/非表示します。",
        });
        CommandManager.AddHandler(CommandRoot + " notify", new CommandInfo(this.OnNotificationCommand)
        {
            HelpMessage = "通知モニタを表示/非表示します。",
        });
    }

    private void OnFrameworkUpdate(IFramework framework)
    {
        _ = framework;
        var now = DateTime.UtcNow;
        if (now < this.nextRefreshAt)
        {
            return;
        }

        var mode = this.modeController.Evaluate();
        var interval = mode == LowImpactModeState.LowImpact ? this.options.StableSamplingInterval : this.options.InitialSamplingInterval;
        this.nextRefreshAt = now + interval;
        _ = this.orchestrator.ExecuteAsync();
    }

    private void DrawUi()
    {
        this.overviewWindow.Render();
    }

    private void DrawNotificationWindow()
    {
        this.notificationWindow.Render();
    }

    private void ShowUi()
    {
        if (!this.initialUiRequestHandled)
        {
            this.initialUiRequestHandled = true;
            if (!this.overviewWindow.IsVisible)
            {
                return;
            }
        }

        this.overviewWindow.Show();
    }

    private void OnCommand(string command, string args)
    {
        _ = command;
        _ = args;
        this.overviewWindow.Toggle();
    }

    private void OnNotificationCommand(string command, string args)
    {
        _ = command;
        _ = args;
        this.notificationWindow.IsVisible = !this.notificationWindow.IsVisible;
    }

    private void OnClientLogin()
    {
        this.initialUiRequestHandled = false;
        this.overviewWindow.IsVisible = false;
    }

    public void Dispose()
    {
        Framework.Update -= this.OnFrameworkUpdate;
        ClientState.Login -= this.OnClientLogin;
        PluginInterface.UiBuilder.Draw -= this.DrawUi;
        PluginInterface.UiBuilder.Draw -= this.DrawNotificationWindow;
        PluginInterface.UiBuilder.OpenMainUi -= this.ShowUi;
        PluginInterface.UiBuilder.OpenConfigUi -= this.ShowUi;
        CommandManager.RemoveHandler(CommandRoot);
        CommandManager.RemoveHandler(CommandRoot + " notify");
        this.bootstrapper.Dispose();
    }
}
