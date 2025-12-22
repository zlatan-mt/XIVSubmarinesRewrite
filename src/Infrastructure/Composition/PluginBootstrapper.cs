// apps/XIVSubmarinesRewrite/src/Infrastructure/Composition/PluginBootstrapper.cs
// プラグイン全体の依存解決と初期化を束ねるブートストラップを提供します
// 小規模チームでも安全に構成要素を差し替えられるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Plugin.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Routes/RouteCatalog.cs

namespace XIVSubmarinesRewrite.Infrastructure.Composition;

using System;
using System.Collections.Generic;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Messaging;
using XIVSubmarinesRewrite.Application.Notifications;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;
using XIVSubmarinesRewrite.Infrastructure.Acquisition;
using XIVSubmarinesRewrite.Integrations.Notifications;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Diagnostics;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Infrastructure.Notifications;
using XIVSubmarinesRewrite.Infrastructure.Messaging;
using XIVSubmarinesRewrite.Infrastructure.Persistence;
using XIVSubmarinesRewrite.Infrastructure.Repositories;
using XIVSubmarinesRewrite.Infrastructure.Routes;
using XIVSubmarinesRewrite.Presentation;
using XIVSubmarinesRewrite.Presentation.Rendering;
using XIVSubmarinesRewrite.Presentation.ViewModels;
using System.Net;
using System.Net.Http;

/// <summary>Registers core services used by the rewrite during early development.</summary>
public sealed class PluginBootstrapper : IDisposable
{
    private readonly IMemorySubmarineSnapshotSource? overrideMemorySource;
    private readonly IUiSubmarineSnapshotSource? overrideUiSource;
    private readonly ILogSink logSink;
    private readonly IClientState clientState;
    private readonly IPlayerState? playerState;
    private readonly IObjectTable? objectTable;
    private readonly IFramework? framework;
    private readonly IDalamudPluginInterface? pluginInterface;
    private readonly IDataManager? dataManager;

    public ServiceRegistry Services { get; }

    public PluginBootstrapper(
        IClientState clientState,
        IMemorySubmarineSnapshotSource? memorySource = null,
        IUiSubmarineSnapshotSource? uiSource = null,
        ILogSink? logSink = null,
        IFramework? framework = null,
        IDalamudPluginInterface? pluginInterface = null,
        IDataManager? dataManager = null,
        IPlayerState? playerState = null,
        IObjectTable? objectTable = null)
    {
        this.clientState = clientState;
        this.playerState = playerState;
        this.objectTable = objectTable;
        this.overrideMemorySource = memorySource;
        this.overrideUiSource = uiSource;
        this.logSink = logSink ?? new NullLogSink();
        this.framework = framework;
        this.pluginInterface = pluginInterface;
        this.dataManager = dataManager;
        this.Services = ServiceRegistry.CreateDefault();
        this.RegisterDefaults();
    }

    private void RegisterDefaults()
    {
        ISettingsProvider settings = this.pluginInterface is not null
            ? new DalamudJsonSettingsProvider(this.pluginInterface)
            : new InMemorySettingsProvider();
        this.Services.RegisterSingleton<ISettingsProvider>(settings);
        this.Services.RegisterSingleton(settings);

        var secrets = new InMemorySecretStore();
        this.Services.RegisterSingleton<ISecretStore>(secrets);
        this.Services.RegisterSingleton(secrets);

        this.Services.RegisterSingleton<ILogSink>(this.logSink);
        this.Services.RegisterSingleton(this.logSink);

        var performanceMonitor = new NullPerformanceMonitor();
        this.Services.RegisterSingleton<IPerformanceMonitor>(performanceMonitor);
        this.Services.RegisterSingleton(performanceMonitor);

        var telemetry = new DefaultAcquisitionTelemetry();
        this.Services.RegisterSingleton<IAcquisitionTelemetry>(telemetry);
        this.Services.RegisterSingleton(telemetry);

        var options = new AcquisitionOptions();
        this.Services.RegisterSingleton(options);

        var routeCatalog = new RouteCatalog(this.dataManager, this.logSink);
        this.Services.RegisterSingleton(routeCatalog);

        var migrator = new SnapshotStorageMigrator(settings, this.logSink);
        this.Services.RegisterSingleton(migrator);

        var storageService = new SnapshotStorageService(settings, migrator);
        this.Services.RegisterSingleton(storageService);

        var characterRegistry = new CharacterRegistry(this.clientState, this.playerState!, this.objectTable!, this.logSink, settings);
        this.Services.RegisterSingleton<ICharacterRegistry>(characterRegistry);
        this.Services.RegisterSingleton(characterRegistry);

        var cache = new SnapshotCache();
        this.Services.RegisterSingleton(cache);

        var notificationSettings = settings.Get<NotificationSettings>();
        this.Services.RegisterSingleton(notificationSettings);

        var notificationQueueOptions = new NotificationQueueOptions
        {
            DeadLetterCapacity = Math.Max(1, notificationSettings.DeadLetterRetentionLimit),
        };
        this.Services.RegisterSingleton(notificationQueueOptions);

        var notificationQueue = new InMemoryNotificationQueue(notificationQueueOptions);
        this.Services.RegisterSingleton<INotificationQueue>(notificationQueue);
        this.Services.RegisterSingleton(notificationQueue);

        var httpClientHandler = new SocketsHttpHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        };
        var httpClient = new HttpClient(httpClientHandler)
        {
            Timeout = TimeSpan.FromSeconds(10),
        };
        this.Services.RegisterSingleton(httpClient);

        var discordClient = new DiscordWebhookClient(httpClient, notificationSettings, this.logSink);
        this.Services.RegisterSingleton<IDiscordClient>(discordClient);
        this.Services.RegisterSingleton(discordClient);

        IMainThreadDispatcher mainThreadDispatcher = this.framework is not null
            ? new DalamudMainThreadDispatcher(this.framework)
            : new ImmediateMainThreadDispatcher();
        this.Services.RegisterSingleton<IMainThreadDispatcher>(mainThreadDispatcher);
        this.Services.RegisterSingleton(mainThreadDispatcher);

        var notificationFormatter = new VoyageNotificationFormatter(notificationSettings);
        this.Services.RegisterSingleton(notificationFormatter);

        var initialBatchWindow = TimeSpan.FromSeconds(Math.Clamp(notificationSettings.DiscordBatchWindowSeconds, 0.5, 15.0));
        var discordBatcher = new DiscordNotificationBatcher(discordClient, notificationFormatter, this.logSink, initialBatchWindow);
        this.Services.RegisterSingleton(discordBatcher);

        var notificationCoordinator = new NotificationCoordinator(discordClient, notificationFormatter, routeCatalog, discordBatcher, this.logSink);
        this.Services.RegisterSingleton(notificationCoordinator);

        var notificationDispatcher = new NotificationCoordinatorDispatcher(notificationCoordinator);
        this.Services.RegisterSingleton<IVoyageNotificationDispatcher>(notificationDispatcher);
        this.Services.RegisterSingleton(notificationDispatcher);

        var notificationWorker = new NotificationWorker(notificationQueue, notificationDispatcher, mainThreadDispatcher, this.logSink);
        this.Services.RegisterSingleton(notificationWorker);

        var voyageProjection = new VoyageCompletionProjection(cache, notificationQueue, notificationSettings, characterRegistry, this.logSink, TimeProvider.System);
        this.Services.RegisterSingleton(voyageProjection);

        var eventBus = new InMemoryEventBus();
        this.Services.RegisterSingleton<IEventBus>(eventBus);
        this.Services.RegisterSingleton(eventBus);

        var submarineRepository = new InMemorySubmarineRepository();
        this.Services.RegisterSingleton<ISubmarineRepository>(submarineRepository);
        this.Services.RegisterSingleton(submarineRepository);

        var voyageRepository = new InMemoryVoyageRepository();
        this.Services.RegisterSingleton<IVoyageRepository>(voyageRepository);
        this.Services.RegisterSingleton(voyageRepository);

        storageService.HydrateAsync(submarineRepository, voyageRepository).GetAwaiter().GetResult();

        var routeRepository = new InMemoryRouteRepository();
        this.Services.RegisterSingleton<IRouteRepository>(routeRepository);
        this.Services.RegisterSingleton(routeRepository);

        var alarmRepository = new InMemoryAlarmRepository();
        this.Services.RegisterSingleton<IAlarmRepository>(alarmRepository);
        this.Services.RegisterSingleton(alarmRepository);

        var persister = new SnapshotPersister(submarineRepository, voyageRepository, storageService);
        this.Services.RegisterSingleton(persister);

        var memorySource = this.overrideMemorySource ?? new NullMemorySubmarineSnapshotSource();
        this.Services.RegisterSingleton<IMemorySubmarineSnapshotSource>(memorySource);
        this.Services.RegisterSingleton(memorySource);

        var uiSource = this.overrideUiSource ?? new NullUiSubmarineSnapshotSource();
        this.Services.RegisterSingleton<IUiSubmarineSnapshotSource>(uiSource);
        this.Services.RegisterSingleton(uiSource);

        var aggregator = new CharacterSnapshotAggregator(this.logSink);
        this.Services.RegisterSingleton(aggregator);

        var differ = new SnapshotDiffer();
        this.Services.RegisterSingleton(differ);

        var dataSources = new List<IDataSource>
        {
            new MemoryDataSource(memorySource),
            new UiDataSource(uiSource),
        };

        var gateway = new DataAcquisitionGateway(dataSources, cache, telemetry, persister, aggregator, differ, characterRegistry, this.logSink);
        this.Services.RegisterSingleton(gateway);

        foreach (var storedSnapshot in storageService.LoadSnapshots())
        {
            // ゴーストデータをフィルタリング：Pending スロットは保持しつつ、実スロットは 0-3 のみ許可
            static bool ShouldKeep(Submarine submarine)
                => submarine.Id.IsPending || (!submarine.Id.IsPending && submarine.Id.Slot <= 3);

            var validSubmarines = storedSnapshot.Submarines
                .Where(ShouldKeep)
                .ToList();

            var removed = storedSnapshot.Submarines.Count - validSubmarines.Count;
            if (removed > 0)
            {
                this.logSink.Log(LogLevel.Information,
                    $"[Plugin] Filtered {removed} ghost submarines (slot>3) from character {storedSnapshot.CharacterId}. Valid slots: 0-3.");
            }

            var cleanedSnapshot = storedSnapshot with { Submarines = validSubmarines };
            var snapshot = aggregator.Integrate(cleanedSnapshot);
            cache.Update(snapshot, snapshot.CharacterId);
            characterRegistry.RegisterSnapshot(snapshot);
            storageService.UpdateFromSnapshot(snapshot);
        }

        var orchestrator = new SnapshotOrchestrator(gateway, eventBus);
        this.Services.RegisterSingleton(orchestrator);

        var lowImpact = new LowImpactModeController(options, telemetry);
        this.Services.RegisterSingleton(lowImpact);

        var overviewViewModel = new OverviewViewModel(submarineRepository, characterRegistry);
        this.Services.RegisterSingleton<IOverviewViewModel>(overviewViewModel);
        this.Services.RegisterSingleton(overviewViewModel);

        var notificationQueueViewModel = new NotificationQueueViewModel(notificationQueue);
        this.Services.RegisterSingleton(notificationQueueViewModel);

        var notificationWindow = new NotificationMonitorWindowRenderer(notificationQueueViewModel, notificationSettings, notificationQueueOptions, settings, notificationQueue, cache, characterRegistry, discordBatcher, voyageProjection);
        this.Services.RegisterSingleton(notificationWindow);

        var alarmViewModel = new NullAlarmViewModel();
        this.Services.RegisterSingleton<IAlarmViewModel>(alarmViewModel);
        this.Services.RegisterSingleton(alarmViewModel);

        var uiObserver = new UIStateObserver(cache, overviewViewModel, alarmViewModel, characterRegistry);
        this.Services.RegisterSingleton(uiObserver);
    }

    public void Dispose()
    {
        this.Services.Dispose();
    }
}