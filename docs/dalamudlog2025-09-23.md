<!-- apps/XIVSubmarinesRewrite/docs/dalamudlog2025-09-23.md -->
<!-- Dalamud セッションログのサンプルです -->
<!-- 通知まわりのデバッグ記録を共有するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/docs/notifications, apps/XIVSubmarinesRewrite/logs -->

2025-09-22 20:44:47.771 +09:00 [VRB] [VFS] Initializing VFS database at C:\Users\MonaT\AppData\Roaming\XIVLauncher\dalamudVfs.db
2025-09-22 20:44:47.959 +09:00 [INF] Waiting for 5ms before starting a session.
2025-09-22 20:44:47.975 +09:00 [INF] --------------------------------------------------------------------------------
2025-09-22 20:44:47.975 +09:00 [INF] Initializing a session..
2025-09-22 20:44:48.024 +09:00 [VRB] Module base: 0x2001CB0C040
2025-09-22 20:44:48.024 +09:00 [VRB] Module size: 0x1FD1D92
2025-09-22 20:44:48.032 +09:00 [DBG] [SVC] Service<ServiceContainer>: Static ctor called
2025-09-22 20:44:48.032 +09:00 [DBG] [SVC] Service<ServiceContainer>: Provided
2025-09-22 20:44:48.032 +09:00 [DBG] [SVC] Service<Dalamud>: Static ctor called
2025-09-22 20:44:48.032 +09:00 [DBG] [SVC] Service<Dalamud>: Provided
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<ReliableFileStorage>: Static ctor called
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<ReliableFileStorage>: Provided
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<DalamudConfiguration>: Static ctor called
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<DalamudConfiguration>: Provided
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<TargetSigScanner>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<TargetSigScanner>: Provided
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<Localization>: Static ctor called
2025-09-22 20:44:48.033 +09:00 [DBG] [SVC] Service<Localization>: Provided
2025-09-22 20:44:48.156 +09:00 [DBG] [SVC] Service<DalamudReleases>: Static ctor called
2025-09-22 20:44:48.157 +09:00 [VRB] [SVC] Service<DalamudReleases>: Getting dependencies
2025-09-22 20:44:48.157 +09:00 [VRB] [SVC] Service<DalamudReleases>: => Dependency: HappyHttpClient
2025-09-22 20:44:48.157 +09:00 [VRB] [SVC] Service<DalamudReleases>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.158 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Storage.Assets.IDalamudAssetManager provides for Dalamud.Storage.Assets.DalamudAssetManager
2025-09-22 20:44:48.158 +09:00 [DBG] [SVC] Service<DalamudAssetManager>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<DalamudAssetManager>: Getting dependencies
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<DalamudAssetManager>: => Dependency: Dalamud
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<DalamudAssetManager>: => Dependency: HappyHttpClient
2025-09-22 20:44:48.158 +09:00 [DBG] [SVC] Service<CallGate>: Static ctor called
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<CallGate>: Getting dependencies
2025-09-22 20:44:48.158 +09:00 [DBG] [SVC] Service<DataShare>: Static ctor called
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<DataShare>: Getting dependencies
2025-09-22 20:44:48.158 +09:00 [DBG] [SVC] Service<PluginManager>: Static ctor called
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<PluginManager>: Getting dependencies
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<PluginManager>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<PluginManager>: => Dependency: Dalamud
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<PluginManager>: => Dependency: ProfileManager
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<PluginManager>: => Dependency: HappyHttpClient
2025-09-22 20:44:48.158 +09:00 [DBG] [SVC] Service<PluginManagementCommandHandler>: Static ctor called
2025-09-22 20:44:48.158 +09:00 [VRB] [SVC] Service<PluginManagementCommandHandler>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<PluginManagementCommandHandler>: => Dependency: CommandManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<PluginManagementCommandHandler>: => Dependency: ProfileManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<PluginManagementCommandHandler>: => Dependency: PluginManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<PluginManagementCommandHandler>: => Dependency: ChatGui
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<PluginManagementCommandHandler>: => Dependency: Framework
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<ProfileManager>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<ProfileManager>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<ProfileManager>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<AutoUpdateManager>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<AutoUpdateManager>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<AutoUpdateManager>: => Dependency: ConsoleManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<AutoUpdateManager>: => Dependency: PluginManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<AutoUpdateManager>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<AutoUpdateManager>: => Dependency: NotificationManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<AutoUpdateManager>: => Dependency: DalamudInterface
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<HappyHttpClient>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<HappyHttpClient>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IPluginLog provides for Dalamud.Logging.ScopedPluginLogService
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<TaskTracker>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TaskTracker>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TaskTracker>: => Dependency: Framework
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<TitleScreenMenu>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TitleScreenMenu>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ITitleScreenMenu provides for Dalamud.Interface.TitleScreenMenuPluginScoped
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<WindowSystemPersistence>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<WindowSystemPersistence>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<WindowSystemPersistence>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<DevTextureSaveMenu>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DevTextureSaveMenu>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DevTextureSaveMenu>: => Dependency: InterfaceManager
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<TextureManager>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: => Dependency: InterfaceManagerWithScene
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: => Dependency: Dalamud
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: => Dependency: DataManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: => Dependency: Framework
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<TextureManager>: => Dependency: InterfaceManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ITextureProvider provides for Dalamud.Interface.Textures.Internal.TextureManagerPluginScoped
2025-09-22 20:44:48.159 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ITextureSubstitutionProvider provides for Dalamud.Interface.Textures.Internal.TextureManagerPluginScoped
2025-09-22 20:44:48.159 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ITextureReadbackProvider provides for Dalamud.Interface.Textures.Internal.TextureManagerPluginScoped
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<FontAtlasFactory>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<FontAtlasFactory>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<FontAtlasFactory>: => Dependency: DataManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<FontAtlasFactory>: => Dependency: Framework
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<FontAtlasFactory>: => Dependency: InterfaceManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<FontAtlasFactory>: => Dependency: DalamudAssetManager
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<DalamudCommands>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudCommands>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudCommands>: => Dependency: CommandManager
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<DalamudIme>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudIme>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudIme>: => Dependency: InterfaceManagerWithScene
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudIme>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudIme>: => Dependency: WndProcHookManager
2025-09-22 20:44:48.159 +09:00 [DBG] [SVC] Service<DalamudInterface>: Static ctor called
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: Getting dependencies
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: Dalamud
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: FontAtlasFactory
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: InterfaceManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: PluginImageCache
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: DalamudAssetManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: Framework
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: ClientState
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: TitleScreenMenu
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: GameGui
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: ConsoleManager
2025-09-22 20:44:48.159 +09:00 [VRB] [SVC] Service<DalamudInterface>: => Dependency: AddonLifecycle
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<ImGuiClipboardFunctionProvider>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<ImGuiClipboardFunctionProvider>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<ImGuiClipboardFunctionProvider>: => Dependency: InterfaceManagerWithScene
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<ImGuiClipboardFunctionProvider>: => Dependency: ToastGui
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<InterfaceManager>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<InterfaceManager>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<InterfaceManager>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<InterfaceManager>: => Dependency: Framework
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<InterfaceManager>: => Dependency: HookManager
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<InterfaceManager>: => Dependency: WndProcHookManager
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<InterfaceManager>: => Dependency: WindowSystemPersistence
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<StaThreadService>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<StaThreadService>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<PluginImageCache>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<PluginImageCache>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<PluginImageCache>: => Dependency: Dalamud
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<PluginImageCache>: => Dependency: DalamudAssetManager
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<PluginImageCache>: => Dependency: HappyHttpClient
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<SeStringRenderer>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<SeStringRenderer>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<SeStringRenderer>: => Dependency: DataManager
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<SeStringRenderer>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<NotificationManager>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NotificationManager>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NotificationManager>: => Dependency: FontAtlasFactory
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NotificationManager>: => Dependency: GameGui
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NotificationManager>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.INotificationManager provides for Dalamud.Interface.ImGuiNotification.Internal.NotificationManagerPluginScoped
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Interface.DragDrop.IDragDropManager provides for Dalamud.Interface.DragDrop.DragDropManager
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<DragDropManager>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<DragDropManager>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<WndProcHookManager>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<WndProcHookManager>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IGameInteropProvider provides for Dalamud.Hooking.Internal.GameInteropProviderPluginScoped
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<HookManager>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<HookManager>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<ChatHandlers>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<ChatHandlers>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<ChatHandlers>: => Dependency: ChatGui
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<ChatHandlers>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<Framework>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<Framework>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<Framework>: => Dependency: GameLifecycle
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<Framework>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IFramework provides for Dalamud.Game.FrameworkPluginScoped
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IGameLifecycle provides for Dalamud.Game.GameLifecycle
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<GameLifecycle>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<GameLifecycle>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Game.ISigScanner provides for Dalamud.Game.TargetSigScanner
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<NounProcessor>: Static ctor called
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NounProcessor>: Getting dependencies
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NounProcessor>: => Dependency: DataManager
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<NounProcessor>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.160 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ISeStringEvaluator provides for Dalamud.Game.Text.Evaluator.SeStringEvaluator
2025-09-22 20:44:48.160 +09:00 [DBG] [SVC] Service<SeStringEvaluator>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.160 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: => Dependency: ClientState
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: => Dependency: DataManager
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: => Dependency: GameConfig
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: => Dependency: NounProcessor
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SeStringEvaluator>: => Dependency: SheetRedirectResolver
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<SheetRedirectResolver>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SheetRedirectResolver>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<SheetRedirectResolver>: => Dependency: DataManager
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<GameNetwork>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameNetwork>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameNetwork>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameNetwork>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<NetworkHandlers>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NetworkHandlers>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NetworkHandlers>: => Dependency: GameNetwork
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NetworkHandlers>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NetworkHandlers>: => Dependency: HappyHttpClient
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NetworkHandlers>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<WinSockHandlers>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<WinSockHandlers>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<MarketBoard>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<MarketBoard>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<MarketBoard>: => Dependency: NetworkHandlers
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IMarketBoard provides for Dalamud.Game.MarketBoard.MarketBoardPluginScoped
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<GameInventory>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameInventory>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameInventory>: => Dependency: Framework
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IGameInventory provides for Dalamud.Game.Inventory.GameInventoryPluginScoped
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<DalamudAtkTweaks>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<DalamudAtkTweaks>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<DalamudAtkTweaks>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<DalamudAtkTweaks>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<DalamudCompletion>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<DalamudCompletion>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<DalamudCompletion>: => Dependency: CommandManager
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<DalamudCompletion>: => Dependency: Framework
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<ChatGui>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<ChatGui>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<ChatGui>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IChatGui provides for Dalamud.Game.Gui.ChatGuiPluginScoped
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<GameGui>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameGui>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<GameGui>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IGameGui provides for Dalamud.Game.Gui.GameGuiPluginScoped
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<ToastGui>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<ToastGui>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IToastGui provides for Dalamud.Game.Gui.Toast.ToastGuiPluginScoped
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<PartyFinderGui>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<PartyFinderGui>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<PartyFinderGui>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IPartyFinderGui provides for Dalamud.Game.Gui.PartyFinder.PartyFinderGuiPluginScoped
2025-09-22 20:44:48.161 +09:00 [DBG] [SVC] Service<NamePlateGui>: Static ctor called
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NamePlateGui>: Getting dependencies
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NamePlateGui>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NamePlateGui>: => Dependency: GameGui
2025-09-22 20:44:48.161 +09:00 [VRB] [SVC] Service<NamePlateGui>: => Dependency: ObjectTable
2025-09-22 20:44:48.161 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.INamePlateGui provides for Dalamud.Game.Gui.NamePlate.NamePlateGuiPluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<FlyTextGui>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<FlyTextGui>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<FlyTextGui>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IFlyTextGui provides for Dalamud.Game.Gui.FlyText.FlyTextGuiPluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<DtrBar>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DtrBar>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DtrBar>: => Dependency: Framework
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DtrBar>: => Dependency: GameGui
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DtrBar>: => Dependency: DalamudConfiguration
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DtrBar>: => Dependency: AddonEventManager
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DtrBar>: => Dependency: AddonLifecycle
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IDtrBar provides for Dalamud.Game.Gui.Dtr.DtrBarPluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<ContextMenu>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ContextMenu>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IContextMenu provides for Dalamud.Game.Gui.ContextMenu.ContextMenuPluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<DutyState>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DutyState>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DutyState>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DutyState>: => Dependency: Condition
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DutyState>: => Dependency: Framework
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<DutyState>: => Dependency: ClientState
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IDutyState provides for Dalamud.Game.DutyState.DutyStatePluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<GameConfig>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<GameConfig>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<GameConfig>: => Dependency: Framework
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<GameConfig>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IGameConfig provides for Dalamud.Game.Config.GameConfigPluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<CommandManager>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<CommandManager>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<CommandManager>: => Dependency: Dalamud
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<CommandManager>: => Dependency: ConsoleManager
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ICommandManager provides for Dalamud.Game.Command.CommandManagerPluginScoped
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<ClientState>: Static ctor called
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ClientState>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ClientState>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ClientState>: => Dependency: Dalamud
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ClientState>: => Dependency: GameLifecycle
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ClientState>: => Dependency: Framework
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ClientState>: => Dependency: NetworkHandlers
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IClientState provides for Dalamud.Game.ClientState.ClientStatePluginScoped
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IPartyList provides for Dalamud.Game.ClientState.Party.PartyList
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<PartyList>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<PartyList>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<PartyList>: => Dependency: ClientState
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IObjectTable provides for Dalamud.Game.ClientState.Objects.ObjectTable
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<ObjectTable>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ObjectTable>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<ObjectTable>: => Dependency: ClientState
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Game.ClientState.Objects.ITargetManager provides for Dalamud.Game.ClientState.Objects.TargetManager
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<TargetManager>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<TargetManager>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<TargetManager>: => Dependency: ObjectTable
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IKeyState provides for Dalamud.Game.ClientState.Keys.KeyState
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<KeyState>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<KeyState>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<KeyState>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<KeyState>: => Dependency: ClientState
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IJobGauges provides for Dalamud.Game.ClientState.JobGauge.JobGauges
2025-09-22 20:44:48.162 +09:00 [DBG] [SVC] Service<JobGauges>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.162 +09:00 [VRB] [SVC] Service<JobGauges>: Getting dependencies
2025-09-22 20:44:48.162 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IGamepadState provides for Dalamud.Game.ClientState.GamePad.GamepadState
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<GamepadState>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<GamepadState>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<GamepadState>: => Dependency: ClientState
2025-09-22 20:44:48.163 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IFateTable provides for Dalamud.Game.ClientState.Fates.FateTable
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<FateTable>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<FateTable>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<Condition>: Static ctor called
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<Condition>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<Condition>: => Dependency: Framework
2025-09-22 20:44:48.163 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.ICondition provides for Dalamud.Game.ClientState.Conditions.ConditionPluginScoped
2025-09-22 20:44:48.163 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IBuddyList provides for Dalamud.Game.ClientState.Buddy.BuddyList
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<BuddyList>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<BuddyList>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<BuddyList>: => Dependency: ClientState
2025-09-22 20:44:48.163 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IAetheryteList provides for Dalamud.Game.ClientState.Aetherytes.AetheryteList
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<AetheryteList>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AetheryteList>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AetheryteList>: => Dependency: ClientState
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<AddonLifecyclePooledArgs>: Static ctor called
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AddonLifecyclePooledArgs>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [DBG] [SVC] Service<AddonLifecycle>: Static ctor called
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AddonLifecycle>: Getting dependencies
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AddonLifecycle>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AddonLifecycle>: => Dependency: Framework
2025-09-22 20:44:48.163 +09:00 [VRB] [SVC] Service<AddonLifecycle>: => Dependency: AddonLifecyclePooledArgs
2025-09-22 20:44:48.163 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IAddonLifecycle provides for Dalamud.Game.Addon.Lifecycle.AddonLifecyclePluginScoped
2025-09-22 20:44:48.164 +09:00 [DBG] [SVC] Service<AddonEventManager>: Static ctor called
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<AddonEventManager>: Getting dependencies
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<AddonEventManager>: => Dependency: TargetSigScanner
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<AddonEventManager>: => Dependency: AddonLifecycle
2025-09-22 20:44:48.164 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IAddonEventManager provides for Dalamud.Game.Addon.Events.AddonEventManagerPluginScoped
2025-09-22 20:44:48.164 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IDataManager provides for Dalamud.Data.DataManager
2025-09-22 20:44:48.164 +09:00 [DBG] [SVC] Service<DataManager>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<DataManager>: Getting dependencies
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<DataManager>: => Dependency: Dalamud
2025-09-22 20:44:48.164 +09:00 [DBG] [SVC] Service<ConsoleManager>: Static ctor called
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<ConsoleManager>: Getting dependencies
2025-09-22 20:44:48.164 +09:00 [VRB] [SERVICECONTAINER] => Dalamud.Plugin.Services.IConsole provides for Dalamud.Console.ConsoleManagerPluginScoped
2025-09-22 20:44:48.164 +09:00 [DBG] [SVC] Service<Renderer>: Static ctor called
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<Renderer>: Getting dependencies
2025-09-22 20:44:48.164 +09:00 [VRB] [SVC] Service<Renderer>: => Dependency: InterfaceManagerWithScene
2025-09-22 20:44:48.164 +09:00 [DBG] [SVC] Service<InterfaceManagerWithScene>: Static ctor called
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<DataShare>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<DragDropManager>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<TitleScreenMenu>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<WindowSystemPersistence>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<WndProcHookManager>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<CallGate>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<StaThreadService>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<HookManager>: Begin construction
2025-09-22 20:44:48.166 +09:00 [DBG] [SVC] Service<GameLifecycle>: Begin construction
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<GameNetwork>: Begin construction
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<WinSockHandlers>: Begin construction
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<DalamudAtkTweaks>: Begin construction
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<ChatGui>: Begin construction
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<WindowSystemPersistence>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<GameGui>: Begin construction
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<CallGate>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<TitleScreenMenu>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<HookManager>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<GameLifecycle>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<DragDropManager>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<DataShare>: Construction complete
2025-09-22 20:44:48.167 +09:00 [DBG] [SVC] Service<ToastGui>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<PartyFinderGui>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<FlyTextGui>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<ContextMenu>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<FateTable>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<JobGauges>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<FateTable>: Construction complete
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<AddonLifecyclePooledArgs>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<DataManager>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<ProfileManager>: Begin construction
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<AddonLifecyclePooledArgs>: Construction complete
2025-09-22 20:44:48.168 +09:00 [DBG] [SVC] Service<HappyHttpClient>: Begin construction
2025-09-22 20:44:48.169 +09:00 [DBG] [SVC] Service<JobGauges>: Construction complete
2025-09-22 20:44:48.169 +09:00 [DBG] [SVC] Service<ConsoleManager>: Begin construction
2025-09-22 20:44:48.170 +09:00 [DBG] [SVC] Service<ConsoleManager>: Construction complete
2025-09-22 20:44:48.169 +09:00 [VRB] [PROFILE] Init profile "00000000-0000-0000-0000-000000000000" (DEFAULT) enabled:true policy:"RememberState" plugins:3 will be enabled:true
2025-09-22 20:44:48.170 +09:00 [DBG] [SVC] Service<CommandManager>: Begin construction
2025-09-22 20:44:48.171 +09:00 [DBG] Set ExceptionFilter to "0", old: "140719935935968"
2025-09-22 20:44:48.171 +09:00 [VRB] [GameGui] ===== G A M E G U I =====
2025-09-22 20:44:48.171 +09:00 [VRB] [PROFILE] "3237f9ef-f9a2-4298-a250-4d06bf2158b9" set enabled because remember
2025-09-22 20:44:48.171 +09:00 [DBG] Set ExceptionFilter to "140719935935968", old: "0"
2025-09-22 20:44:48.171 +09:00 [VRB] [PROFILE] Init profile "3237f9ef-f9a2-4298-a250-4d06bf2158b9" (all) enabled:true policy:"RememberState" plugins:15 will be enabled:true
2025-09-22 20:44:48.172 +09:00 [DBG] [SVC] Service<Framework>: Begin construction
2025-09-22 20:44:48.172 +09:00 [DBG] SE default exception filter at 7FFBE9CB55E0
2025-09-22 20:44:48.172 +09:00 [DBG] SE debug exception filter at 7FF73F9F6B90
2025-09-22 20:44:48.172 +09:00 [DBG] [SVC] Service<ProfileManager>: Construction complete
2025-09-22 20:44:48.172 +09:00 [VRB] ===== G A M E N E T W O R K =====
2025-09-22 20:44:48.172 +09:00 [INF] This is Dalamud - Core: 13.0.0.4, CS: 3ae5a65f [6580]
2025-09-22 20:44:48.173 +09:00 [DBG] [SVC] Service<HappyHttpClient>: Construction complete
2025-09-22 20:44:48.173 +09:00 [DBG] [SVC] Service<DalamudReleases>: Begin construction
2025-09-22 20:44:48.173 +09:00 [DBG] [SVC] Service<DalamudAssetManager>: Begin construction
2025-09-22 20:44:48.173 +09:00 [DBG] [SVC] Service<PluginManager>: Begin construction
2025-09-22 20:44:48.173 +09:00 [DBG] [SVC] Service<DalamudReleases>: Construction complete
2025-09-22 20:44:48.176 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:48.177 +09:00 [DBG] [SVC] Service<DalamudAssetManager>: Construction complete
2025-09-22 20:44:48.177 +09:00 [DBG] [SVC] Service<PluginImageCache>: Begin construction
2025-09-22 20:44:48.178 +09:00 [DBG] [SVC] Service<PluginImageCache>: Construction complete
2025-09-22 20:44:48.179 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Storage.Assets.DalamudAssetManager
2025-09-22 20:44:48.179 +09:00 [DBG] [SVC] Service<PluginErrorHandler>: Static ctor called
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<PluginErrorHandler>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<PluginErrorHandler>: => Dependency: NotificationManager
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<PluginErrorHandler>: => Dependency: DalamudInterface
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Plugin.Internal.PluginErrorHandler (2)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Interface.ImGuiNotification.Internal.NotificationManager via Dalamud.Plugin.Internal.PluginErrorHandler
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Interface.Internal.DalamudInterface via Dalamud.Plugin.Internal.PluginErrorHandler
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<ScopedPluginLogService>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<ScopedPluginLogService>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Logging.ScopedPluginLogService (0)
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<TitleScreenMenuPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<TitleScreenMenuPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<TitleScreenMenuPluginScoped>: => Dependency: TitleScreenMenu
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Interface.TitleScreenMenuPluginScoped (1)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Interface.TitleScreenMenu via Dalamud.Interface.TitleScreenMenuPluginScoped
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<TextureManagerPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<TextureManagerPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] Optional assets load state: System.Threading.Tasks.Task+WhenAllPromise
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<TextureManagerPluginScoped>: => Dependency: TextureManager
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Interface.Textures.Internal.TextureManagerPluginScoped (1)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Interface.Textures.Internal.TextureManager via Dalamud.Interface.Textures.Internal.TextureManagerPluginScoped
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<NotificationManagerPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<NotificationManagerPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<NotificationManagerPluginScoped>: => Dependency: NotificationManager
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Interface.ImGuiNotification.Internal.NotificationManagerPluginScoped (1)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Interface.ImGuiNotification.Internal.NotificationManager via Dalamud.Interface.ImGuiNotification.Internal.NotificationManagerPluginScoped
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Interface.DragDrop.DragDropManager
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<GameInteropProviderPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<GameInteropProviderPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Hooking.Internal.GameInteropProviderPluginScoped (0)
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<FrameworkPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<FrameworkPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<FrameworkPluginScoped>: => Dependency: Framework
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.FrameworkPluginScoped (1)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Framework via Dalamud.Game.FrameworkPluginScoped
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.GameLifecycle
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.TargetSigScanner
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Text.Evaluator.SeStringEvaluator
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<MarketBoardPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<MarketBoardPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<MarketBoardPluginScoped>: => Dependency: MarketBoard
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.MarketBoard.MarketBoardPluginScoped (1)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.MarketBoard.MarketBoard via Dalamud.Game.MarketBoard.MarketBoardPluginScoped
2025-09-22 20:44:48.180 +09:00 [DBG] [SVC] Service<GameInventoryPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<GameInventoryPluginScoped>: Getting dependencies
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Service<GameInventoryPluginScoped>: => Dependency: GameInventory
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Inventory.GameInventoryPluginScoped (1)
2025-09-22 20:44:48.180 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Inventory.GameInventory via Dalamud.Game.Inventory.GameInventoryPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<ChatGuiPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.180 +09:00 [INF] [PluginManager] Now reloading all PluginMasters...
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<ChatGuiPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<ChatGuiPluginScoped>: => Dependency: ChatGui
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.ChatGuiPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.ChatGui via Dalamud.Game.Gui.ChatGuiPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<GameGuiPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<GameGuiPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<GameGuiPluginScoped>: => Dependency: GameGui
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.GameGuiPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.GameGui via Dalamud.Game.Gui.GameGuiPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<ToastGuiPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<ToastGuiPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<ToastGuiPluginScoped>: => Dependency: ToastGui
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.Toast.ToastGuiPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.Toast.ToastGui via Dalamud.Game.Gui.Toast.ToastGuiPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<PartyFinderGuiPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<PartyFinderGuiPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<PartyFinderGuiPluginScoped>: => Dependency: PartyFinderGui
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.PartyFinder.PartyFinderGuiPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.PartyFinder.PartyFinderGui via Dalamud.Game.Gui.PartyFinder.PartyFinderGuiPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<NamePlateGuiPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<NamePlateGuiPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<NamePlateGuiPluginScoped>: => Dependency: NamePlateGui
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.NamePlate.NamePlateGuiPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.NamePlate.NamePlateGui via Dalamud.Game.Gui.NamePlate.NamePlateGuiPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<FlyTextGuiPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<FlyTextGuiPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<FlyTextGuiPluginScoped>: => Dependency: FlyTextGui
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.FlyText.FlyTextGuiPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.FlyText.FlyTextGui via Dalamud.Game.Gui.FlyText.FlyTextGuiPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<DtrBarPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<DtrBarPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<DtrBarPluginScoped>: => Dependency: DtrBar
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.Dtr.DtrBarPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.Dtr.DtrBar via Dalamud.Game.Gui.Dtr.DtrBarPluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<ContextMenuPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<ContextMenuPluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<ContextMenuPluginScoped>: => Dependency: ContextMenu
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Gui.ContextMenu.ContextMenuPluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Gui.ContextMenu.ContextMenu via Dalamud.Game.Gui.ContextMenu.ContextMenuPluginScoped
2025-09-22 20:44:48.181 +09:00 [INF] [PLUGINR] Fetching repo: https://kamori.goats.dev/Plugin/PluginMaster
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<DutyStatePluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<DutyStatePluginScoped>: Getting dependencies
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<DutyStatePluginScoped>: => Dependency: DutyState
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.DutyState.DutyStatePluginScoped (1)
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.DutyState.DutyState via Dalamud.Game.DutyState.DutyStatePluginScoped
2025-09-22 20:44:48.181 +09:00 [DBG] [SVC] Service<GameConfigPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.181 +09:00 [VRB] [SVC] Service<GameConfigPluginScoped>: Getting dependencies
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<GameConfigPluginScoped>: => Dependency: GameConfig
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Config.GameConfigPluginScoped (1)
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Config.GameConfig via Dalamud.Game.Config.GameConfigPluginScoped
2025-09-22 20:44:48.182 +09:00 [DBG] [SVC] Service<CommandManagerPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<CommandManagerPluginScoped>: Getting dependencies
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<CommandManagerPluginScoped>: => Dependency: CommandManager
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Command.CommandManagerPluginScoped (1)
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Command.CommandManager via Dalamud.Game.Command.CommandManagerPluginScoped
2025-09-22 20:44:48.182 +09:00 [DBG] [SVC] Service<ClientStatePluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<ClientStatePluginScoped>: Getting dependencies
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<ClientStatePluginScoped>: => Dependency: ClientState
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.ClientState.ClientStatePluginScoped (1)
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.ClientState via Dalamud.Game.ClientState.ClientStatePluginScoped
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Party.PartyList
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Objects.ObjectTable
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Objects.TargetManager
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Keys.KeyState
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.JobGauge.JobGauges
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.GamePad.GamepadState
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Fates.FateTable
2025-09-22 20:44:48.182 +09:00 [DBG] [SVC] Service<ConditionPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<ConditionPluginScoped>: Getting dependencies
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Service<ConditionPluginScoped>: => Dependency: Condition
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.ClientState.Conditions.ConditionPluginScoped (1)
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Conditions.Condition via Dalamud.Game.ClientState.Conditions.ConditionPluginScoped
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Buddy.BuddyList
2025-09-22 20:44:48.182 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.ClientState.Aetherytes.AetheryteList
2025-09-22 20:44:48.183 +09:00 [DBG] [SVC] Service<AddonLifecyclePluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Service<AddonLifecyclePluginScoped>: Getting dependencies
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Service<AddonLifecyclePluginScoped>: => Dependency: AddonLifecycle
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Addon.Lifecycle.AddonLifecyclePluginScoped (1)
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Addon.Lifecycle.AddonLifecycle via Dalamud.Game.Addon.Lifecycle.AddonLifecyclePluginScoped
2025-09-22 20:44:48.183 +09:00 [DBG] [SVC] Service<AddonEventManagerPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Service<AddonEventManagerPluginScoped>: Getting dependencies
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Service<AddonEventManagerPluginScoped>: => Dependency: AddonEventManager
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Game.Addon.Events.AddonEventManagerPluginScoped (1)
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Game.Addon.Events.AddonEventManager via Dalamud.Game.Addon.Events.AddonEventManagerPluginScoped
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Data.DataManager
2025-09-22 20:44:48.183 +09:00 [DBG] [SVC] Service<ConsoleManagerPluginScoped>: Static ctor called; will be exposed to plugins
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Service<ConsoleManagerPluginScoped>: Getting dependencies
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Service<ConsoleManagerPluginScoped>: => Dependency: ConsoleManager
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] Found dependencies of scoped plugin service Dalamud.Console.ConsoleManagerPluginScoped (1)
2025-09-22 20:44:48.183 +09:00 [VRB] [SVC] PluginManager MUST depend on Dalamud.Console.ConsoleManager via Dalamud.Console.ConsoleManagerPluginScoped
2025-09-22 20:44:48.184 +09:00 [DBG] [SVC] Service<PluginManager>: Construction complete
2025-09-22 20:44:48.185 +09:00 [DBG] [SVC] Service<StaThreadService>: Construction complete
2025-09-22 20:44:48.193 +09:00 [VRB] OnReceivePacket address 0x7FF741164B80(ffxiv_dx11.exe+0x17C4B80(.text+0x17C3B80))
2025-09-22 20:44:48.193 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:48.194 +09:00 [VRB] [GameGui] HandleImm address 0x7FF73FA6CCC0(ffxiv_dx11.exe+0xCCCC0(.text+0xCBCC0))
2025-09-22 20:44:48.194 +09:00 [VRB] [HM] Registering hook at 0x7FF741973D38(ffxiv_dx11.exe+0x1FD3D38(.rdata+0xD38)) (minBytes=0x8, maxBytes=0x8)
2025-09-22 20:44:48.197 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:48.207 +09:00 [INF] [PluginManager] Repos loaded!
2025-09-22 20:44:48.207 +09:00 [VRB] ProcessZonePacketUp address 0x7FF7415DFEA0(ffxiv_dx11.exe+0x1C3FEA0(.text+0x1C3EEA0))
2025-09-22 20:44:48.209 +09:00 [VRB] [HM] Registering hook at 0x7FF741973958(ffxiv_dx11.exe+0x1FD3958(.rdata+0x958)) (minBytes=0x8, maxBytes=0x8)
2025-09-22 20:44:48.210 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:48.210 +09:00 [DBG] [SVC] Service<WndProcHookManager>: Construction complete
2025-09-22 20:44:48.215 +09:00 [INF] [PluginManager] Plugin cleanup OK!
2025-09-22 20:44:48.215 +09:00 [INF] [PluginManager] Boot load started
2025-09-22 20:44:48.215 +09:00 [VRB] [SVC] =============== BLOCKINGSERVICES & TASKS INITIALIZED ===============
2025-09-22 20:44:48.216 +09:00 [VRB] =============== GAME THREAD KICKOFF ===============
2025-09-22 20:44:48.218 +09:00 [VRB] [HM] Registering hook at 0x7FF740117690(ffxiv_dx11.exe+0x777690(.text+0x776690)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.224 +09:00 [INF] [PluginManager] ============= LoadPluginsSync(AnytimeSync) START =============
2025-09-22 20:44:48.224 +09:00 [INF] [PluginManager] ============= LoadPluginsSync(AnytimeSync) END =============
2025-09-22 20:44:48.224 +09:00 [INF] [PluginManager] ============= LoadPluginsAsync(AnytimeAsync) START =============
2025-09-22 20:44:48.225 +09:00 [INF] [PluginManager] ============= LoadPluginsAsync(AnytimeAsync) END =============
2025-09-22 20:44:48.226 +09:00 [VRB] [PluginManager] Starting async boot load
2025-09-22 20:44:48.235 +09:00 [INF] TROUBLESHOOTING:eyJMb2FkZWRQbHVnaW5zIjpbXSwiUGx1Z2luU3RhdGVzIjp7fSwiRXZlclN0YXJ0ZWRMb2FkaW5nUGx1Z2lucyI6W10sIkRhbGFtdWRWZXJzaW9uIjoiMTMuMC4wLjQiLCJEYWxhbXVkR2l0SGFzaCI6ImZhNThkN2IzY2NlZjFjYWFkODljN2E1MWFjMmFiYjM1NTllNzgxYmIiLCJHYW1lVmVyc2lvbiI6IjIwMjUuMDkuMDQuMDAwMC4wMDAwIiwiTGFuZ3VhZ2UiOiJKYXBhbmVzZSIsIkRvRGFsYW11ZFRlc3QiOmZhbHNlLCJCZXRhS2V5IjpudWxsLCJEb1BsdWdpblRlc3QiOnRydWUsIkxvYWRBbGxBcGlMZXZlbHMiOmZhbHNlLCJJbnRlcmZhY2VMb2FkZWQiOmZhbHNlLCJGb3JjZWRNaW5Ib29rIjpmYWxzZSwiVGhpcmRSZXBvIjpbXSwiSGFzVGhpcmRSZXBvIjp0cnVlfQ==
2025-09-22 20:44:48.268 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:48.277 +09:00 [VRB] [HM] Registering hook at 0x7FF7402D8770(ffxiv_dx11.exe+0x938770(.text+0x937770)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.278 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA6BDC0(ffxiv_dx11.exe+0xCBDC0(.text+0xCADC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.278 +09:00 [DBG] [SVC] Service<PartyFinderGui>: Construction complete
2025-09-22 20:44:48.279 +09:00 [VRB] [HM] Registering hook at 0x7FF74007BFD0(ffxiv_dx11.exe+0x6DBFD0(.text+0x6DAFD0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.280 +09:00 [VRB] Starting data load...
2025-09-22 20:44:48.280 +09:00 [VRB] [HM] Registering hook at 0x7FF740D6B330(ffxiv_dx11.exe+0x13CB330(.text+0x13CA330)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.280 +09:00 [VRB] [HM] Registering hook at 0x7FF740017120(ffxiv_dx11.exe+0x677120(.text+0x676120)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.280 +09:00 [DBG] [SVC] Service<FlyTextGui>: Construction complete
2025-09-22 20:44:48.281 +09:00 [VRB] [HM] Registering hook at 0x7FF7407F0BB0(ffxiv_dx11.exe+0xE50BB0(.text+0xE4FBB0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.281 +09:00 [VRB] [HM] Registering hook at 0x7FF7400F4E60(ffxiv_dx11.exe+0x754E60(.text+0x753E60)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.281 +09:00 [VRB] [HM] Registering hook at 0x7FF740832650(ffxiv_dx11.exe+0xE92650(.text+0xE91650)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.282 +09:00 [VRB] [HM] Registering hook at 0x7FF7408325F0(ffxiv_dx11.exe+0xE925F0(.text+0xE915F0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.282 +09:00 [DBG] [SVC] Service<WinSockHandlers>: Construction complete
2025-09-22 20:44:48.282 +09:00 [VRB] [HM] Registering hook at 0x7FF7406E4250(ffxiv_dx11.exe+0xD44250(.text+0xD43250)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.282 +09:00 [DBG] [SVC] Service<CommandManager>: Construction complete
2025-09-22 20:44:48.282 +09:00 [VRB] [HM] Registering hook at 0x7FF74030C9D0(ffxiv_dx11.exe+0x96C9D0(.text+0x96B9D0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.282 +09:00 [DBG] [SVC] Service<DalamudCommands>: Begin construction
2025-09-22 20:44:48.283 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA6B4E0(ffxiv_dx11.exe+0xCB4E0(.text+0xCA4E0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.283 +09:00 [VRB] [HM] Registering hook at 0x7FF7409EDF00(ffxiv_dx11.exe+0x104DF00(.text+0x104CF00)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.283 +09:00 [DBG] [SVC] Service<Framework>: Construction complete
2025-09-22 20:44:48.283 +09:00 [DBG] [SVC] Service<TaskTracker>: Begin construction
2025-09-22 20:44:48.283 +09:00 [DBG] [SVC] Service<TaskTracker>: Construction complete
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<DalamudCommands>: Construction complete
2025-09-22 20:44:48.284 +09:00 [VRB] [HM] Registering hook at 0x7FF7400F4D50(ffxiv_dx11.exe+0x754D50(.text+0x753D50)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<Condition>: Begin construction
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<GameConfig>: Begin construction
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<ContextMenu>: Construction complete
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<InterfaceManager>: Begin construction
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<GameInventory>: Begin construction
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<DalamudCompletion>: Begin construction
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<AddonLifecycle>: Begin construction
2025-09-22 20:44:48.284 +09:00 [DBG] [SVC] Service<Condition>: Construction complete
2025-09-22 20:44:48.285 +09:00 [DBG] [SVC] Service<DalamudCompletion>: Construction complete
2025-09-22 20:44:48.285 +09:00 [VRB] [HM] Registering hook at 0x7FF7400F4F60(ffxiv_dx11.exe+0x754F60(.text+0x753F60)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.285 +09:00 [DBG] [SVC] Service<GameConfig>: Construction complete
2025-09-22 20:44:48.285 +09:00 [DBG] [SVC] Service<ToastGui>: Construction complete
2025-09-22 20:44:48.285 +09:00 [VRB] [HM] Registering hook at 0x7FF7406E4620(ffxiv_dx11.exe+0xD44620(.text+0xD43620)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.285 +09:00 [DBG] [SVC] Service<InterfaceManager>: Construction complete
2025-09-22 20:44:48.286 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA6CCC0(ffxiv_dx11.exe+0xCCCC0(.text+0xCBCC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.286 +09:00 [VRB] [HM] Registering hook at 0x7FF7400F54E0(ffxiv_dx11.exe+0x7554E0(.text+0x7544E0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.286 +09:00 [DBG] [SVC] Service<DevTextureSaveMenu>: Begin construction
2025-09-22 20:44:48.287 +09:00 [DBG] [SVC] Service<DevTextureSaveMenu>: Construction complete
2025-09-22 20:44:48.287 +09:00 [VRB] [HM] Registering hook at 0x7FF741164B80(ffxiv_dx11.exe+0x17C4B80(.text+0x17C3B80)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.287 +09:00 [VRB] [HM] Registering hook at 0x7FF7415DFEA0(ffxiv_dx11.exe+0x1C3FEA0(.text+0x1C3EEA0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.288 +09:00 [VRB] [HM] Registering hook at 0x7FF7401C85B0(ffxiv_dx11.exe+0x8285B0(.text+0x8275B0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.288 +09:00 [DBG] [SVC] Service<GameNetwork>: Construction complete
2025-09-22 20:44:48.288 +09:00 [DBG] [SVC] Service<NetworkHandlers>: Begin construction
2025-09-22 20:44:48.289 +09:00 [VRB] [HM] Registering hook at 0x7FF73FAB2BB0(ffxiv_dx11.exe+0x112BB0(.text+0x111BB0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.298 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:48.314 +09:00 [VRB] [HM] Registering hook at 0x7FF73FFF1DC0(ffxiv_dx11.exe+0x651DC0(.text+0x650DC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.315 +09:00 [VRB] [HM] Registering hook at 0x7FF73FFE86D0(ffxiv_dx11.exe+0x6486D0(.text+0x6476D0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.315 +09:00 [VRB] [HM] Registering hook at 0x7FF7419993C8(ffxiv_dx11.exe+0x1FF93C8(.rdata+0x263C8)) (minBytes=0x8, maxBytes=0x8)
2025-09-22 20:44:48.316 +09:00 [VRB] [HM] Registering hook at 0x7FF7409E2960(ffxiv_dx11.exe+0x1042960(.text+0x1041960)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.316 +09:00 [DBG] [SVC] Service<GameInventory>: Construction complete
2025-09-22 20:44:48.318 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA22DC0(ffxiv_dx11.exe+0x82DC0(.text+0x81DC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.318 +09:00 [DBG] [SVC] Service<ChatGui>: Construction complete
2025-09-22 20:44:48.318 +09:00 [DBG] [SVC] Service<PluginManagementCommandHandler>: Begin construction
2025-09-22 20:44:48.318 +09:00 [DBG] [SVC] Service<ChatHandlers>: Begin construction
2025-09-22 20:44:48.318 +09:00 [VRB] [HM] Registering hook at 0x7FF73FFECB60(ffxiv_dx11.exe+0x64CB60(.text+0x64BB60)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.319 +09:00 [DBG] [SVC] Service<GameGui>: Construction complete
2025-09-22 20:44:48.319 +09:00 [DBG] [SVC] Service<ChatHandlers>: Construction complete
2025-09-22 20:44:48.319 +09:00 [DBG] [SVC] Service<DalamudAtkTweaks>: Construction complete
2025-09-22 20:44:48.319 +09:00 [DBG] [SVC] Service<AddonLifecycle>: Construction complete
2025-09-22 20:44:48.320 +09:00 [DBG] [SVC] Service<AddonEventManager>: Begin construction
2025-09-22 20:44:48.320 +09:00 [DBG] [SVC] Service<PluginManagementCommandHandler>: Construction complete
2025-09-22 20:44:48.321 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA81490(ffxiv_dx11.exe+0xE1490(.text+0xE0490)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.321 +09:00 [DBG] [SVC] Service<AddonEventManager>: Construction complete
2025-09-22 20:44:48.322 +09:00 [DBG] [SVC] Service<DtrBar>: Begin construction
2025-09-22 20:44:48.322 +09:00 [VRB] [HM] Registering hook at 0x7FF74126A170(ffxiv_dx11.exe+0x18CA170(.text+0x18C9170)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.322 +09:00 [DBG] [SVC] Service<DtrBar>: Construction complete
2025-09-22 20:44:48.323 +09:00 [VRB] [HM] Registering hook at 0x7FF7402CA2A0(ffxiv_dx11.exe+0x92A2A0(.text+0x9292A0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.323 +09:00 [VRB] [HM] Registering hook at 0x7FF740410E00(ffxiv_dx11.exe+0xA70E00(.text+0xA6FE00)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.324 +09:00 [VRB] [HM] Registering hook at 0x7FF74126A030(ffxiv_dx11.exe+0x18CA030(.text+0x18C9030)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.324 +09:00 [VRB] [HM] Registering hook at 0x7FF7402C9D90(ffxiv_dx11.exe+0x929D90(.text+0x928D90)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.324 +09:00 [VRB] [HM] Registering hook at 0x7FF7402CACC0(ffxiv_dx11.exe+0x92ACC0(.text+0x929CC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.325 +09:00 [VRB] [HM] Registering hook at 0x7FF7412B4740(ffxiv_dx11.exe+0x1914740(.text+0x1913740)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.325 +09:00 [DBG] [SVC] Service<NetworkHandlers>: Construction complete
2025-09-22 20:44:48.326 +09:00 [DBG] [SVC] Service<MarketBoard>: Begin construction
2025-09-22 20:44:48.326 +09:00 [DBG] [SVC] Service<ClientState>: Begin construction
2025-09-22 20:44:48.327 +09:00 [VRB] [ClientState] ===== C L I E N T  S T A T E =====
2025-09-22 20:44:48.327 +09:00 [VRB] [ClientState] SetupTerritoryType address 0x7FF74050E1D0(ffxiv_dx11.exe+0xB6E1D0(.text+0xB6D1D0))
2025-09-22 20:44:48.327 +09:00 [VRB] [HM] Registering hook at 0x7FF74050E1D0(ffxiv_dx11.exe+0xB6E1D0(.text+0xB6D1D0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.328 +09:00 [DBG] [SVC] Service<MarketBoard>: Construction complete
2025-09-22 20:44:48.329 +09:00 [VRB] [HM] Registering hook at 0x7FF7400F2AF0(ffxiv_dx11.exe+0x752AF0(.text+0x751AF0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.330 +09:00 [DBG] [SVC] Service<ClientState>: Construction complete
2025-09-22 20:44:48.330 +09:00 [DBG] [SVC] Service<DutyState>: Begin construction
2025-09-22 20:44:48.330 +09:00 [DBG] [SVC] Service<PartyList>: Begin construction
2025-09-22 20:44:48.330 +09:00 [DBG] [SVC] Service<ObjectTable>: Begin construction
2025-09-22 20:44:48.330 +09:00 [DBG] [SVC] Service<KeyState>: Begin construction
2025-09-22 20:44:48.340 +09:00 [DBG] [SVC] Service<PartyList>: Construction complete
2025-09-22 20:44:48.340 +09:00 [DBG] [SVC] Service<GamepadState>: Begin construction
2025-09-22 20:44:48.341 +09:00 [DBG] [SVC] Service<BuddyList>: Begin construction
2025-09-22 20:44:48.341 +09:00 [DBG] [SVC] Service<AetheryteList>: Begin construction
2025-09-22 20:44:48.341 +09:00 [DBG] [SVC] Service<BuddyList>: Construction complete
2025-09-22 20:44:48.341 +09:00 [VRB] Keyboard state buffer address 0x7FF7422BC354(ffxiv_dx11.exe+0x291C354(.data+0x1FA354))
2025-09-22 20:44:48.341 +09:00 [VRB] [HM] Registering hook at 0x7FF7401A9BB0(ffxiv_dx11.exe+0x809BB0(.text+0x808BB0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.341 +09:00 [DBG] [SVC] Service<KeyState>: Construction complete
2025-09-22 20:44:48.341 +09:00 [VRB] Teleport address 0x7FF7422F09E0(ffxiv_dx11.exe+0x29509E0(.data+0x22E9E0))
2025-09-22 20:44:48.341 +09:00 [DBG] [SVC] Service<AetheryteList>: Construction complete
2025-09-22 20:44:48.341 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA5DBC0(ffxiv_dx11.exe+0xBDBC0(.text+0xBCBC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.341 +09:00 [DBG] [SVC] Service<ObjectTable>: Construction complete
2025-09-22 20:44:48.342 +09:00 [DBG] [SVC] Service<NamePlateGui>: Begin construction
2025-09-22 20:44:48.342 +09:00 [DBG] [SVC] Service<TargetManager>: Begin construction
2025-09-22 20:44:48.342 +09:00 [DBG] [SVC] Service<TargetManager>: Construction complete
2025-09-22 20:44:48.342 +09:00 [DBG] [SVC] Service<GamepadState>: Construction complete
2025-09-22 20:44:48.342 +09:00 [DBG] [SVC] Service<DutyState>: Construction complete
2025-09-22 20:44:48.342 +09:00 [VRB] [HM] Registering hook at 0x7FF740BF27C0(ffxiv_dx11.exe+0x12527C0(.text+0x12517C0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:48.342 +09:00 [DBG] [SVC] Service<NamePlateGui>: Construction complete
2025-09-22 20:44:48.351 +09:00 [VRB] [HTTP] Established connection to kamori.goats.dev at 104.21.85.184:443
2025-09-22 20:44:48.660 +09:00 [INF] Lumina is ready: D:\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\game\sqpack
2025-09-22 20:44:48.662 +09:00 [DBG] [SVC] Service<DataManager>: Construction complete
2025-09-22 20:44:48.662 +09:00 [DBG] [SVC] Service<FontAtlasFactory>: Begin construction
2025-09-22 20:44:48.662 +09:00 [DBG] [SVC] Service<SeStringRenderer>: Begin construction
2025-09-22 20:44:48.662 +09:00 [DBG] [SVC] Service<NounProcessor>: Begin construction
2025-09-22 20:44:48.662 +09:00 [DBG] [SVC] Service<SheetRedirectResolver>: Begin construction
2025-09-22 20:44:48.662 +09:00 [DBG] [SVC] Service<SheetRedirectResolver>: Construction complete
2025-09-22 20:44:48.663 +09:00 [DBG] [SVC] Service<NounProcessor>: Construction complete
2025-09-22 20:44:48.663 +09:00 [DBG] [SVC] Service<SeStringEvaluator>: Begin construction
2025-09-22 20:44:48.664 +09:00 [DBG] [SVC] Service<SeStringEvaluator>: Construction complete
2025-09-22 20:44:48.677 +09:00 [DBG] [SVC] Service<FontAtlasFactory>: Construction complete
2025-09-22 20:44:48.677 +09:00 [DBG] [SVC] Service<NotificationManager>: Begin construction
2025-09-22 20:44:48.678 +09:00 [DBG] [SVC] Service<DalamudInterface>: Begin construction
2025-09-22 20:44:48.681 +09:00 [DBG] [SVC] Service<NotificationManager>: Construction complete
2025-09-22 20:44:48.683 +09:00 [DBG] [SVC] Service<SeStringRenderer>: Construction complete
2025-09-22 20:44:48.974 +09:00 [DBG] [SVC] Service<DalamudInterface>: Construction complete
2025-09-22 20:44:48.974 +09:00 [DBG] [SVC] Service<AutoUpdateManager>: Begin construction
2025-09-22 20:44:48.975 +09:00 [DBG] [SVC] Service<AutoUpdateManager>: Construction complete
2025-09-22 20:44:49.402 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://kamori.goats.dev/Plugin/PluginMaster
2025-09-22 20:44:49.402 +09:00 [INF] [PLUGINR] Fetching repo: https://raw.githubusercontent.com/Eternita-S/MyDalamudPlugins/main/pluginmaster.json
2025-09-22 20:44:49.403 +09:00 [INF] [PLUGINR] Fetching repo: https://github.com/Aireil/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:44:49.404 +09:00 [INF] [PLUGINR] Fetching repo: https://github.com/daemitus/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:44:49.404 +09:00 [INF] [PLUGINR] Fetching repo: https://love.puni.sh/ment.json
2025-09-22 20:44:49.420 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:44:49.441 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://raw.githubusercontent.com/Eternita-S/MyDalamudPlugins/main/pluginmaster.json
2025-09-22 20:44:49.452 +09:00 [VRB] [HTTP] Established connection to github.com at 20.27.177.113:443
2025-09-22 20:44:49.457 +09:00 [VRB] [HTTP] Established connection to github.com at 20.27.177.113:443
2025-09-22 20:44:49.478 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://github.com/daemitus/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:44:49.600 +09:00 [VRB] [HTTP] Established connection to love.puni.sh at [2606:4700:3031::6815:3264]:443
2025-09-22 20:44:49.698 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://github.com/Aireil/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:44:50.270 +09:00 [VRB] [HTTP] Established connection to puni.sh at [2606:4700:3035::ac43:ccb6]:443
2025-09-22 20:44:50.738 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://love.puni.sh/ment.json
2025-09-22 20:44:50.738 +09:00 [INF] [PluginManager] PluginMasters reloaded, now refiltering...
2025-09-22 20:44:56.522 +09:00 [VRB] Config saved
2025-09-22 20:44:56.523 +09:00 [VRB] [GameConfig] Initializing
2025-09-22 20:44:56.524 +09:00 [VRB] [GameConfig] Initalizing System with 1020 entries.
2025-09-22 20:44:56.524 +09:00 [VRB] [GameConfig] Initalizing UiConfig with 1020 entries.
2025-09-22 20:44:56.525 +09:00 [VRB] [GameConfig] Initalizing UiControl with 1020 entries.
2025-09-22 20:44:56.525 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:44:56.542 +09:00 [VRB] [HM] Registering hook at 0x7FF73FA33CC0(ffxiv_dx11.exe+0x93CC0(.text+0x92CC0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:56.544 +09:00 [VRB] [HM] Registering hook at 0x7FF73FE605A0(ffxiv_dx11.exe+0x4C05A0(.text+0x4BF5A0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:56.545 +09:00 [INF] [PluginManager] ============= LoadPluginsSync(FrameworkTickSync) START =============
2025-09-22 20:44:56.545 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] Building from BuildFontsAsync.
2025-09-22 20:44:56.549 +09:00 [VRB] [FontAtlasFactory] [NotificationManager:RebuildFontsPrivateReal] 0x20113E76C00: PreBuild (at 3ms)
2025-09-22 20:44:56.545 +09:00 [INF] [PluginManager] ============= LoadPluginsSync(FrameworkTickSync) END =============
2025-09-22 20:44:56.553 +09:00 [VRB] [PluginManager] Loaded FrameworkTickSync plugins (LoadRequiredState == 1)
2025-09-22 20:44:56.553 +09:00 [INF] [PluginManager] ============= LoadPluginsAsync(FrameworkTickAsync) START =============
2025-09-22 20:44:56.553 +09:00 [INF] [PluginManager] ============= LoadPluginsAsync(FrameworkTickAsync) END =============
2025-09-22 20:44:56.553 +09:00 [VRB] [PluginManager] Kicked off FrameworkTickAsync plugins (LoadRequiredState == 1)
2025-09-22 20:44:56.556 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] 0x20113E76C00: AddFontFromImGuiHeapAllocatedMemory(0x1BF80ED0790, 0xFB39C, ...) from AddFontFromStream(Asset(FontAwesomeFreeSolid))
2025-09-22 20:44:56.577 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] 0x20113E76C00: AddFontFromImGuiHeapAllocatedMemory(0x2013D97B040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:44:56.634 +09:00 [VRB] [HM] Registering hook at 0x7FF73FFFA0E0(ffxiv_dx11.exe+0x65A0E0(.text+0x6590E0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:44:56.635 +09:00 [VRB] [HM] Registering hook at 0x7FF74122C010(ffxiv_dx11.exe+0x188C010(.text+0x188B010)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.382 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager] Building from BuildFontsAsync.
2025-09-22 20:45:00.382 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager:RebuildFontsPrivateReal] 0x2012D25E080: PreBuild (at 0ms)
2025-09-22 20:45:00.383 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:45:00.388 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager] 0x2012D25E080: AddFontFromImGuiHeapAllocatedMemory(0x201A07E0040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:00.392 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager] 0x2012D25E080: AddFontFromImGuiHeapAllocatedMemory(0x2019C38E530, 0xFB39C, ...) from AddFontFromStream(Asset(FontAwesomeFreeSolid))
2025-09-22 20:45:00.393 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager] 0x2012D25E080: AddFontFromImGuiHeapAllocatedMemory(0x2019C4898E0, 0xFB39C, ...) from AddFontFromStream(Asset(FontAwesomeFreeSolid))
2025-09-22 20:45:00.394 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager] 0x2012D25E080: AddFontFromImGuiHeapAllocatedMemory(0x2019C213250, 0x1A88C, ...) from AddFontFromStream(Asset(InconsolataRegular))
2025-09-22 20:45:00.400 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager] 0x2012D25E080: AddFontFromImGuiHeapAllocatedMemory(0x201A1880040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:00.401 +09:00 [VRB] [HM] Registering hook at 0x7FF741973AC8(ffxiv_dx11.exe+0x1FD3AC8(.rdata+0xAC8)) (minBytes=0x8, maxBytes=0x8)
2025-09-22 20:45:00.429 +09:00 [INF] [INTERFACE] ===== S W A P C H A I N =====
2025-09-22 20:45:00.429 +09:00 [INF] [INTERFACE] Hooking using bytecode...
2025-09-22 20:45:00.429 +09:00 [VRB] [HM] Registering hook at 0x7FFC550ADBE0(dxgi.dll+0x3DBE0(.text+0x3CBE0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.479 +09:00 [INF] [INTERFACE] Hooked IDXGISwapChain::ResizeBuffers using bytecode: 0x7FFC550ADBE0(dxgi.dll+0x3DBE0(.text+0x3CBE0))
2025-09-22 20:45:00.479 +09:00 [VRB] [HM] Registering hook at 0x7FFC55072A20(dxgi.dll+0x2A20(.text+0x1A20)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.479 +09:00 [INF] [INTERFACE] Hooked IDXGISwapChain::Present using bytecode: 0x7FFC55072A20(dxgi.dll+0x2A20(.text+0x1A20))
2025-09-22 20:45:00.480 +09:00 [WRN] [HITCH] Long FrameworkUpdate detected, 99.182ms > 50ms - check in the plugin stats window.
2025-09-22 20:45:00.540 +09:00 [INF] [INTERFACE] [IM] Scene & ImGui setup OK!
2025-09-22 20:45:00.540 +09:00 [DBG] [SVC] Service<InterfaceManagerWithScene>: Provided
2025-09-22 20:45:00.541 +09:00 [VRB] [PluginManager]  InterfaceManager is ready, starting to load DrawAvailableSync plugins
2025-09-22 20:45:00.541 +09:00 [DBG] [SVC] Service<Renderer>: Begin construction
2025-09-22 20:45:00.542 +09:00 [DBG] [SVC] Service<TextureManager>: Begin construction
2025-09-22 20:45:00.542 +09:00 [DBG] [SVC] Service<DalamudIme>: Begin construction
2025-09-22 20:45:00.542 +09:00 [DBG] [SVC] Service<ImGuiClipboardFunctionProvider>: Begin construction
2025-09-22 20:45:00.542 +09:00 [DBG] [SVC] Service<DalamudIme>: Construction complete
2025-09-22 20:45:00.543 +09:00 [INF] [DragDrop] Registered window 0x170E84 for external drag and drop operations. (0)
2025-09-22 20:45:00.543 +09:00 [DBG] [SVC] Service<ImGuiClipboardFunctionProvider>: Construction complete
2025-09-22 20:45:00.543 +09:00 [DBG] [SVC] Service<Renderer>: Construction complete
2025-09-22 20:45:00.548 +09:00 [DBG] [SVC] Service<TextureManager>: Construction complete
2025-09-22 20:45:00.557 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] Building from BuildFontsAsync.
2025-09-22 20:45:00.557 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x20113E76730: PreBuild (at 0ms)
2025-09-22 20:45:00.563 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] 0x20113E76730: AddFontFromImGuiHeapAllocatedMemory(0x201A292B040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:00.567 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x20113E76730: Complete (at 10ms)
2025-09-22 20:45:00.569 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] Building from BuildFontsAsync.
2025-09-22 20:45:00.569 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] Building from BuildFontsAsync.
2025-09-22 20:45:00.569 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x20113E76730: PreBuild (at 0ms)
2025-09-22 20:45:00.571 +09:00 [VRB] [FontAtlasFactory] [NotificationManager:RebuildFontsPrivateReal] 0x20113E76C00: Complete (at 4024ms)
2025-09-22 20:45:00.572 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] Building from BuildFontsAsync.
2025-09-22 20:45:00.572 +09:00 [VRB] [FontAtlasFactory] [NotificationManager:RebuildFontsPrivateReal] 0x2012D25CD40: PreBuild (at 0ms)
2025-09-22 20:45:00.572 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] 0x2012D25CD40: AddFontFromImGuiHeapAllocatedMemory(0x1BF80ED0790, 0xFB39C, ...) from AddFontFromStream(Asset(FontAwesomeFreeSolid))
2025-09-22 20:45:00.575 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] 0x20113E76730: AddFontFromImGuiHeapAllocatedMemory(0x201A2924040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:00.578 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] 0x2012D25CD40: AddFontFromImGuiHeapAllocatedMemory(0x2013D97D040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:00.579 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x20113E76730: Complete (at 9ms)
2025-09-22 20:45:00.591 +09:00 [INF] [PluginManager] ============= LoadPluginsSync(DrawAvailableSync) START =============
2025-09-22 20:45:00.591 +09:00 [INF] [PluginManager] ============= LoadPluginsSync(DrawAvailableSync) END =============
2025-09-22 20:45:00.591 +09:00 [VRB] [PluginManager] Loaded DrawAvailableSync plugins (LoadRequiredState == 0 or null)
2025-09-22 20:45:00.591 +09:00 [INF] [PluginManager] ============= LoadPluginsAsync(DrawAvailableAsync) START =============
2025-09-22 20:45:00.592 +09:00 [VRB] [PluginManager] Kicked off DrawAvailableAsync plugins (LoadRequiredState == 0 or null)
2025-09-22 20:45:00.592 +09:00 [VRB] [PluginManager] Now waiting for 3 async load tasks
2025-09-22 20:45:00.593 +09:00 [INF] [PluginManager] Loading plugin FPSPlugin
2025-09-22 20:45:00.594 +09:00 [VRB] [PluginManager] Starting to load plugin FPSPlugin at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\FPSPlugin\1.7.0.3\FPSPlugin.dll
2025-09-22 20:45:00.594 +09:00 [INF] [PluginManager] Loading plugin AntiAfkKick-Dalamud
2025-09-22 20:45:00.594 +09:00 [VRB] [PluginManager] Starting to load plugin AntiAfkKick-Dalamud at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\AntiAfkKick-Dalamud\2.1.0.9\AntiAfkKick-Dalamud.dll
2025-09-22 20:45:00.594 +09:00 [INF] [PluginManager] Loading dev plugin XIVSubmarinesRewrite
2025-09-22 20:45:00.595 +09:00 [INF] [PluginManager] FPSPlugin defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.595 +09:00 [INF] [PluginManager] AntiAfkKick-Dalamud defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.596 +09:00 [VRB] [PluginManager] Starting to load plugin XIVSubmarinesRewrite at C:\Users\MonaT\AppData\Roaming\XIVLauncher\devPlugins\XIVSubmarinesRewrite\XIVSubmarinesRewrite.dll
2025-09-22 20:45:00.596 +09:00 [INF] [PluginManager] Loading plugin AutoRetainer
2025-09-22 20:45:00.596 +09:00 [VRB] [PluginManager] DevPlugin XIVSubmarinesRewrite enabled and StartOnBoot => enable
2025-09-22 20:45:00.596 +09:00 [VRB] [PluginManager] Starting to load plugin AutoRetainer at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\AutoRetainer\4.5.2.1\AutoRetainer.dll
2025-09-22 20:45:00.596 +09:00 [INF] [PluginManager] Loading plugin SonarPlugin
2025-09-22 20:45:00.596 +09:00 [INF] [PluginManager] AutoRetainer defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.596 +09:00 [VRB] [PluginManager] Starting to load plugin SonarPlugin at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\SonarPlugin\0.7.3.4\SonarPlugin.dll
2025-09-22 20:45:00.596 +09:00 [INF] [PluginManager] SonarPlugin defaultState: true wantedByAnyProfile: false loadPlugin: true
2025-09-22 20:45:00.596 +09:00 [INF] [PROFILE] Adding plugin XIVSubmarinesRewrite("5cb9cb66-b279-4599-9243-e82bc6d87ee5") to profile "00000000-0000-0000-0000-000000000000" with state true
2025-09-22 20:45:00.596 +09:00 [INF] [PluginManager] Loading plugin EngageTimer
2025-09-22 20:45:00.596 +09:00 [VRB] [PluginManager] SonarPlugin not loaded, wantToLoad:false orphaned:false
2025-09-22 20:45:00.596 +09:00 [INF] [PluginManager] XIVSubmarinesRewrite defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.596 +09:00 [INF] [LOCALPLUGIN] Loading FPSPlugin.dll
2025-09-22 20:45:00.596 +09:00 [INF] [LOCALPLUGIN] Loading AutoRetainer.dll
2025-09-22 20:45:00.597 +09:00 [VRB] [PluginManager] Starting to load plugin EngageTimer at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\EngageTimer\2.4.4.0\EngageTimer.dll
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] Loading plugin FFLogsViewer
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] EngageTimer defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.597 +09:00 [INF] [LOCALPLUGIN] Loading XIVSubmarinesRewrite.dll
2025-09-22 20:45:00.597 +09:00 [INF] [LOCALPLUGIN] Loading AntiAfkKick-Dalamud.dll
2025-09-22 20:45:00.597 +09:00 [INF] [LOCALPLUGIN] Loading EngageTimer.dll
2025-09-22 20:45:00.597 +09:00 [VRB] [PluginManager] Starting to load plugin FFLogsViewer at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\FFLogsViewer\2.3.0.0\FFLogsViewer.dll
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] Loading plugin YesAlready
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] FFLogsViewer defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.597 +09:00 [VRB] [PluginManager] Starting to load plugin YesAlready at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\YesAlready\1.13.3\YesAlready.dll
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] Loading plugin TriadBuddy
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] YesAlready defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.597 +09:00 [INF] [LOCALPLUGIN] Loading FFLogsViewer.dll
2025-09-22 20:45:00.597 +09:00 [INF] [LOCALPLUGIN] Loading YesAlready.dll
2025-09-22 20:45:00.597 +09:00 [VRB] [PluginManager] Starting to load plugin TriadBuddy at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\TriadBuddy\1.13.0.0\TriadBuddy.dll
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] Loading plugin Dalamud.FullscreenCutscenes
2025-09-22 20:45:00.597 +09:00 [INF] [PluginManager] TriadBuddy defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.598 +09:00 [INF] [LOCALPLUGIN] Loading TriadBuddy.dll
2025-09-22 20:45:00.598 +09:00 [VRB] [PluginManager] Starting to load plugin Dalamud.FullscreenCutscenes at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\Dalamud.FullscreenCutscenes\1.0.0.6\Dalamud.FullscreenCutscenes.dll
2025-09-22 20:45:00.598 +09:00 [INF] [PluginManager] Loading plugin BetterPartyFinder
2025-09-22 20:45:00.598 +09:00 [INF] [PluginManager] Dalamud.FullscreenCutscenes defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.598 +09:00 [INF] [LOCALPLUGIN] Loading Dalamud.FullscreenCutscenes.dll
2025-09-22 20:45:00.598 +09:00 [VRB] [PluginManager] Starting to load plugin BetterPartyFinder at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\BetterPartyFinder\1.5.7.0\BetterPartyFinder.dll
2025-09-22 20:45:00.598 +09:00 [INF] [PluginManager] BetterPartyFinder defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.598 +09:00 [INF] [PluginManager] Loading plugin GatherBuddy
2025-09-22 20:45:00.599 +09:00 [INF] [LOCALPLUGIN] Loading BetterPartyFinder.dll
2025-09-22 20:45:00.599 +09:00 [INF] [PluginManager] Loading plugin Globetrotter
2025-09-22 20:45:00.599 +09:00 [VRB] [PluginManager] Starting to load plugin GatherBuddy at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\GatherBuddy\3.8.3.0\GatherBuddy.dll
2025-09-22 20:45:00.599 +09:00 [INF] [PluginManager] GatherBuddy defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.599 +09:00 [INF] [LOCALPLUGIN] Loading GatherBuddy.dll
2025-09-22 20:45:00.599 +09:00 [VRB] [PluginManager] Starting to load plugin Globetrotter at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\Globetrotter\1.2.15\Globetrotter.dll
2025-09-22 20:45:00.599 +09:00 [INF] [PluginManager] Globetrotter defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.599 +09:00 [INF] [PluginManager] Loading plugin HuntBuddy
2025-09-22 20:45:00.599 +09:00 [INF] [LOCALPLUGIN] Loading Globetrotter.dll
2025-09-22 20:45:00.600 +09:00 [VRB] [PluginManager] Starting to load plugin HuntBuddy at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\HuntBuddy\1.2.2.0\HuntBuddy.dll
2025-09-22 20:45:00.600 +09:00 [INF] [PluginManager] Loading plugin MarketBoardPlugin
2025-09-22 20:45:00.600 +09:00 [INF] [PluginManager] HuntBuddy defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.600 +09:00 [INF] [LOCALPLUGIN] Loading HuntBuddy.dll
2025-09-22 20:45:00.600 +09:00 [VRB] [PluginManager] Starting to load plugin MarketBoardPlugin at C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\MarketBoardPlugin\1.11.0.0\MarketBoardPlugin.dll
2025-09-22 20:45:00.600 +09:00 [INF] [PluginManager] MarketBoardPlugin defaultState: true wantedByAnyProfile: true loadPlugin: true
2025-09-22 20:45:00.600 +09:00 [INF] [LOCALPLUGIN] Loading MarketBoardPlugin.dll
2025-09-22 20:45:00.609 +09:00 [VRB] [FontAtlasFactory] [NotificationManager:RebuildFontsPrivateReal] 0x2012D25CD40: Complete (at 36ms)
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] Globetrotter ("e59c86df-adf8-4af3-a09c-f3a37410927f"): Have type
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] HuntBuddy ("16725adf-5ae2-4a0b-9181-7f948b9b5a47"): Have type
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] FPSPlugin ("6f5ee63a-b707-4f5b-b6d4-95a887270bce"): Have type
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] AntiAfkKick-Dalamud ("59765c0c-1b6d-4c68-a551-6afb258024f8"): Have type
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] Dalamud.FullscreenCutscenes ("22889e5e-e12b-4efb-936e-e893c2d082cc"): Have type
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] MarketBoardPlugin ("e1811414-668a-440d-855c-3327a15eaf2d"): Have type
2025-09-22 20:45:00.618 +09:00 [VRB] [LOCALPLUGIN] BetterPartyFinder ("6f7b4aa8-7f97-45a7-a9a2-a738b1885605"): Have type
2025-09-22 20:45:00.620 +09:00 [VRB] [LOCALPLUGIN] FFLogsViewer ("259aac68-8d14-4228-8d7a-daeee95bb8a8"): Have type
2025-09-22 20:45:00.620 +09:00 [VRB] [LOCALPLUGIN] TriadBuddy ("a1900d5c-8e2c-4949-ab77-b1eaea9b1fef"): Have type
2025-09-22 20:45:00.640 +09:00 [VRB] [LOCALPLUGIN] YesAlready ("1e0d2991-8598-4e7a-ad78-4a7f494547f5"): Have type
2025-09-22 20:45:00.640 +09:00 [VRB] [LOCALPLUGIN] EngageTimer ("065230ab-0235-480c-ab86-4e7ee1852054"): Have type
2025-09-22 20:45:00.643 +09:00 [VRB] [LOCALPLUGIN] XIVSubmarinesRewrite ("5cb9cb66-b279-4599-9243-e82bc6d87ee5"): Have type
2025-09-22 20:45:00.644 +09:00 [VRB] [LOCALPLUGIN] GatherBuddy ("4d84e550-0852-4eec-a812-8628d8537c1b"): Have type
2025-09-22 20:45:00.645 +09:00 [VRB] Config saved
2025-09-22 20:45:00.648 +09:00 [VRB] [LOCALPLUGIN] AutoRetainer ("2f5e0c48-21eb-400c-8cb5-0f63d20bb2f8"): Have type
2025-09-22 20:45:00.651 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:45:00.653 +09:00 [VRB] [HM] Detected hook trampoline at 7FF7401A9BB0, stopping jump resolution.
2025-09-22 20:45:00.657 +09:00 [INF] [XIVSubmarinesRewrite] [Plugin] AddonLifecycle type=Dalamud.Game.Addon.Lifecycle.AddonLifecyclePluginScoped
2025-09-22 20:45:00.659 +09:00 [INF] [LOCALPLUGIN] Finished loading FPSPlugin
2025-09-22 20:45:00.662 +09:00 [INF] [LOCALPLUGIN] Finished loading AntiAfkKick-Dalamud
2025-09-22 20:45:00.666 +09:00 [VRB] [AddonEventManager] Creating new PluginEventController for: 1e0d2991-8598-4e7a-ad78-4a7f494547f5
2025-09-22 20:45:00.667 +09:00 [INF] [XIVSubmarinesRewrite] [UI Inspector] Registered SelectString lifecycle listener for PostSetup.
2025-09-22 20:45:00.667 +09:00 [INF] [XIVSubmarinesRewrite] [UI Inspector] Registered SelectString lifecycle listener for PostRefresh.
2025-09-22 20:45:00.667 +09:00 [INF] [XIVSubmarinesRewrite] [UI Inspector] Registered SelectString lifecycle listener for PreFinalize.
2025-09-22 20:45:00.668 +09:00 [VRB] [AddonEventManager] Creating new PluginEventController for: 2f5e0c48-21eb-400c-8cb5-0f63d20bb2f8
2025-09-22 20:45:00.669 +09:00 [INF] [LOCALPLUGIN] Finished loading HuntBuddy
2025-09-22 20:45:00.671 +09:00 [INF] [YesAlready] This is ECommons v3.0.1.5 (release build) and YesAlready v1.13.3.0. Hello!
2025-09-22 20:45:00.673 +09:00 [INF] [AutoRetainer] This is ECommons v3.0.1.9 (release build) and AutoRetainer v4.5.2.1. Hello!
2025-09-22 20:45:00.675 +09:00 [INF] [AutoRetainer] Advanced Dalamud reflection module has been requested
2025-09-22 20:45:00.679 +09:00 [INF] [LOCALPLUGIN] Finished loading BetterPartyFinder
2025-09-22 20:45:00.680 +09:00 [DBG] [AutoRetainer] Path: C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\AutoRetainer\4.5.2.1\AutoRetainer.json
2025-09-22 20:45:00.682 +09:00 [INF] [LOCALPLUGIN] Finished loading TriadBuddy
2025-09-22 20:45:00.691 +09:00 [VRB] [HM] Registering hook at 0x7FF73FBC9C00(ffxiv_dx11.exe+0x229C00(.text+0x228C00)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.691 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:45:00.703 +09:00 [INF] [AutoRetainer] FFXIV instances: 1
2025-09-22 20:45:00.705 +09:00 [INF] [LOCALPLUGIN] Finished loading AutoRetainer
2025-09-22 20:45:00.709 +09:00 [DBG] [DtrBar] Adding entry: FPS Display; owner: FPSPlugin("6f5ee63a-b707-4f5b-b6d4-95a887270bce")
2025-09-22 20:45:00.710 +09:00 [INF] [LOCALPLUGIN] Finished loading FFLogsViewer
2025-09-22 20:45:00.716 +09:00 [INF] [AutoRetainer] Migrating conditions are not met, skipping...
2025-09-22 20:45:00.717 +09:00 [DBG] [YesAlready] Creating singleton instance of ECommons.Automation.NeoTaskManager.TaskManager
2025-09-22 20:45:00.719 +09:00 [DBG] [YesAlready] Creating singleton instance of YesAlready.IPC.BlockListHandler
2025-09-22 20:45:00.720 +09:00 [VRB] [DataShare] Created new data for [YesAlready.StopRequests] for creator YesAlready.
2025-09-22 20:45:00.720 +09:00 [DBG] [YesAlready] Creating singleton instance of YesAlready.IPC.YesAlreadyIPC
2025-09-22 20:45:00.727 +09:00 [DBG] [YesAlready] [EzIPC Provider] Attempting to register YesAlreadyIPC.IsPluginEnabled as IPC method (0)
2025-09-22 20:45:00.728 +09:00 [DBG] [YesAlready] [EzIPC Provider] Registering IPC method YesAlready.IsPluginEnabled with method YesAlready.IPC.YesAlreadyIPC.IsPluginEnabled
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Attempting to register YesAlreadyIPC.SetPluginEnabled as IPC method (1)
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Registering IPC method YesAlready.SetPluginEnabled with method YesAlready.IPC.YesAlreadyIPC.SetPluginEnabled
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Attempting to register YesAlreadyIPC.IsBotherEnabled as IPC method (1)
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Registering IPC method YesAlready.IsBotherEnabled with method YesAlready.IPC.YesAlreadyIPC.IsBotherEnabled
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Attempting to register YesAlreadyIPC.SetBotherEnabled as IPC method (2)
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Registering IPC method YesAlready.SetBotherEnabled with method YesAlready.IPC.YesAlreadyIPC.SetBotherEnabled
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Attempting to register YesAlreadyIPC.PausePlugin as IPC method (1)
2025-09-22 20:45:00.730 +09:00 [DBG] [YesAlready] [EzIPC Provider] Registering IPC method YesAlready.PausePlugin with method YesAlready.IPC.YesAlreadyIPC.PausePlugin
2025-09-22 20:45:00.731 +09:00 [DBG] [YesAlready] [EzIPC Provider] Attempting to register YesAlreadyIPC.PauseBother as IPC method (2)
2025-09-22 20:45:00.731 +09:00 [DBG] [YesAlready] [EzIPC Provider] Registering IPC method YesAlready.PauseBother with method YesAlready.IPC.YesAlreadyIPC.PauseBother
2025-09-22 20:45:00.731 +09:00 [DBG] [YesAlready] Creating singleton instance of YesAlready.Watcher
2025-09-22 20:45:00.732 +09:00 [VRB] [HM] Registering hook at 0x7FF7401A9BB0(ffxiv_dx11.exe+0x809BB0(.text+0x808BB0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.732 +09:00 [VRB] [HM] Registering hook at 0x7FF73FEF9410(ffxiv_dx11.exe+0x559410(.text+0x558410)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.733 +09:00 [VRB] [HM] Registering hook at 0x7FF7404BC640(ffxiv_dx11.exe+0xB1C640(.text+0xB1B640)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.733 +09:00 [INF] [LOCALPLUGIN] Finished loading Dalamud.FullscreenCutscenes
2025-09-22 20:45:00.733 +09:00 [DBG] [YesAlready] [EzHook] Configured YesAlready.Watcher+FireCallbackDelegate at 00007FF73FFE3EE0
2025-09-22 20:45:00.734 +09:00 [INF] [LOCALPLUGIN] Finished loading Globetrotter
2025-09-22 20:45:00.735 +09:00 [DBG] [YesAlready] [EzHook] Creating hook YesAlready.Watcher+FireCallbackDelegate at 00007FF73FFE3EE0
2025-09-22 20:45:00.735 +09:00 [VRB] [HM] Registering hook at 0x7FF73FFE3EE0(ffxiv_dx11.exe+0x643EE0(.text+0x642EE0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.739 +09:00 [DBG] [DtrBar] Adding entry: YesAlready; owner: YesAlready("1e0d2991-8598-4e7a-ad78-4a7f494547f5")
2025-09-22 20:45:00.751 +09:00 [DBG] [YesAlready] Enabling AirShipExplorationResult
2025-09-22 20:45:00.751 +09:00 [DBG] [YesAlready] Enabling BannerPreview
2025-09-22 20:45:00.751 +09:00 [DBG] [YesAlready] Enabling ContentsFinderConfirm
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling CustomAddonCallbacks
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling DifficultySelectYesNo
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling FashionCheck
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling FGSEnterDialog
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling FGSExitDialog
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling FrontlineRecord
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling GrandCompanySupplyReward
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling GuildLeveDifficulty
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] Enabling HWDLottery
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] [EzHook] Configured YesAlready.Features.InclusionShop+AgentReceiveEventDelegate at 00007FF7408281B0
2025-09-22 20:45:00.752 +09:00 [DBG] [YesAlready] [EzHook] Creating hook YesAlready.Features.InclusionShop+AgentReceiveEventDelegate at 00007FF7408281B0
2025-09-22 20:45:00.752 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:45:00.753 +09:00 [VRB] [GatherBuddy] Collected 203 different Weathers.
2025-09-22 20:45:00.780 +09:00 [VRB] [GatherBuddy] Collected 103 different territories with dynamic weather.
2025-09-22 20:45:00.785 +09:00 [VRB] [HM] Detected hook trampoline at 7FF74126A030, stopping jump resolution.
2025-09-22 20:45:00.787 +09:00 [INF] [LOCALPLUGIN] Finished loading XIVSubmarinesRewrite
2025-09-22 20:45:00.793 +09:00 [VRB] [HM] Registering hook at 0x7FF7408281B0(ffxiv_dx11.exe+0xE881B0(.text+0xE871B0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.793 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling InclusionShop
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling InputNumeric
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling ItemInspectionResult
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling JournalResult
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling LobbyDKTCheck
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling LotteryWeeklyInput
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling LovmResult
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling MateriaAttachDialog
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling MaterializeDialog
2025-09-22 20:45:00.793 +09:00 [DBG] [YesAlready] Enabling MateriaRetrieveDialog
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling MiragePrismExecute
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling MiragePrismRemove
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling MKSRecord
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling PurifyResult
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling RaceChocoboResult
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling RetainerItemTransferList
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling RetainerItemTransferProgress
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling RetainerTaskAsk
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling RetainerTaskResult
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling SalvageDialog
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling SalvageResult
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling SatisfactionSupply
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling SelectIconString
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling SelectOk
2025-09-22 20:45:00.794 +09:00 [DBG] [YesAlready] Enabling SelectString
2025-09-22 20:45:00.795 +09:00 [DBG] [YesAlready] Enabling SelectYesno
2025-09-22 20:45:00.795 +09:00 [DBG] [YesAlready] Enabling ShopCardDialog
2025-09-22 20:45:00.795 +09:00 [DBG] [YesAlready] Enabling ShopExchangeItemDialog
2025-09-22 20:45:00.795 +09:00 [DBG] [YesAlready] Enabling Talk
2025-09-22 20:45:00.795 +09:00 [DBG] [YesAlready] Enabling WKSReward
2025-09-22 20:45:00.795 +09:00 [INF] [LOCALPLUGIN] Finished loading YesAlready
2025-09-22 20:45:00.802 +09:00 [INF] [LOCALPLUGIN] Finished loading EngageTimer
2025-09-22 20:45:00.836 +09:00 [VRB] [HM] Registering hook at 0x7FF74126A030(ffxiv_dx11.exe+0x18CA030(.text+0x18C9030)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.838 +09:00 [VRB] [HM] Registering hook at 0x7FF7403BDCB0(ffxiv_dx11.exe+0xA1DCB0(.text+0xA1CCB0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.843 +09:00 [DBG] [AutoRetainer] [EzHook] Configured AutoRetainer.Internal.Memory+SellItemDelegate at 00007FF740FDDB50
2025-09-22 20:45:00.843 +09:00 [DBG] [AutoRetainer] [EzHook] Configured AutoRetainer.Internal.Memory+RetainerItemCommandDelegate at 00007FF7404794D0
2025-09-22 20:45:00.854 +09:00 [VRB] [HM] Registering hook at 0x7FF73FE11410(ffxiv_dx11.exe+0x471410(.text+0x470410)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.857 +09:00 [DBG] [AutoRetainer] [IPC] IPC init
2025-09-22 20:45:00.871 +09:00 [DBG] [AutoRetainer] Registered interactable ProperOnLogin event: System.Action
2025-09-22 20:45:00.871 +09:00 [DBG] [AutoRetainer] ProperOnLogin interactable master event registered
2025-09-22 20:45:00.875 +09:00 [INF] [AutoRetainer] AutoCutsceneSkipper requested
2025-09-22 20:45:00.876 +09:00 [VRB] [HM] Registering hook at 0x7FF7400EDA10(ffxiv_dx11.exe+0x74DA10(.text+0x74CA10)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:00.876 +09:00 [INF] [AutoRetainer] Found cutscene skip condition address at 0x00007FF7400EDB47
2025-09-22 20:45:00.876 +09:00 [INF] [AutoRetainer] AutoCutsceneSkipper initialized
2025-09-22 20:45:00.877 +09:00 [VRB] [DataShare] Created new data for [AutoRetainer.Started] for creator ECommons.
2025-09-22 20:45:00.879 +09:00 [VRB] [DataShare] Created new data for [ECommons.ContextMenuPrefixRemover.TakenContextMenuPrefixes] for creator ECommons.
2025-09-22 20:45:00.880 +09:00 [DBG] [AutoRetainer] Initialized prefix remover with prefix=57264, color=3155, iterations=1
2025-09-22 20:45:00.880 +09:00 [INF] [AutoRetainer] AutoRetainer v4.5.2.1 is ready.
2025-09-22 20:45:00.883 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.UI.NeoUI.NeoWindow
2025-09-22 20:45:00.910 +09:00 [VRB] [GatherBuddy] Collected 107 different aetherytes.
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MainSettings pat General
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.UserInterface pat User Interface
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.DeployablesTab pat Deployables
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.RetainersTab pat Retainers
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.Keybinds pat Keybinds
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.MultiModeCommon pat Multi Mode/Common Settings
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.MultiModeRetainers pat Multi Mode/Retainers
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.MultiModeDeployables pat Multi Mode/Deployables
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.MultiModeContingency pat Multi Mode/Contingency
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.CharaOrder pat Multi Mode/Exclusions and Order
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.MultiModeFPSLimiter pat Multi Mode/FPS Limiter
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MultiModeEntries.MultiModeLockout pat Multi Mode/Region Lock
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.EntrustManager pat Inventory Management/Entrust Manager
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.GeneralSettings pat Inventory Management/Inventory Cleanup/General Settings
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.HardList pat Inventory Management/Inventory Cleanup/Unconditional Sell List
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.SoftList pat Inventory Management/Inventory Cleanup/Quick Venture Sell List
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.GCDeliveryEntries.GeneralSettings pat Inventory Management/Grand Company Delivery/General Settings
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.DiscardList pat Inventory Management/Inventory Cleanup/Discard List
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.ProtectionList pat Inventory Management/Inventory Cleanup/Protection List
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.GCDeliveryEntries.ExchangeLists pat Inventory Management/Grand Company Delivery/Exchange Lists
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.FastAddition pat Inventory Management/Inventory Cleanup/Fast Addition and Removal
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.GCDeliveryEntries.GCCharacterConfiguration pat Inventory Management/Grand Company Delivery/Character Configuration
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries.CleanupCharacterConfiguration pat Inventory Management/Inventory Cleanup/Character Configuration
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.LoginOverlay pat Login Overlay
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.MiscTab pat Miscellaneous
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.Experiments.NightMode pat Experiments/Night Mode
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.Experiments.Notifications pat Experiments/Notifications
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.AdvancedEntries.LogTab pat Advanced/Log
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.AdvancedEntries.ExpertTab pat Advanced/Expert Settings
2025-09-22 20:45:00.942 +09:00 [VRB] [AutoRetainer] Item AutoRetainer.UI.NeoUI.AdvancedEntries.CharacterSync pat Advanced/Character Synchronization
2025-09-22 20:45:00.943 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Modules.EzIPCManagers.EzIPCManager
2025-09-22 20:45:00.944 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_GCContinuation.EnqueueInitiation as IPC method (0)
2025-09-22 20:45:00.944 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.GC.EnqueueInitiation with method AutoRetainer.Modules.EzIPCManagers.IPC_GCContinuation.EnqueueInitiation
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_GCContinuation.GetGCInfo as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.GC.GetGCInfo with method AutoRetainer.Modules.EzIPCManagers.IPC_GCContinuation.GetGCInfo
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.IsBusy as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.IsBusy with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.IsBusy
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.GetEnabledRetainers as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.GetEnabledRetainers with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.GetEnabledRetainers
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.AreAnyRetainersAvailableForCurrentChara as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.AreAnyRetainersAvailableForCurrentChara with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.AreAnyRetainersAvailableForCurrentChara
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.AbortAllTasks as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.AbortAllTasks with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.AbortAllTasks
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.DisableAllFunctions as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.DisableAllFunctions with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.DisableAllFunctions
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.GetMultiModeStatus as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.GetMultiModeStatus with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.GetMultiModeStatus
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.EnableMultiMode as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.EnableMultiMode with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.EnableMultiMode
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.GetInventoryFreeSlotCount as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.GetInventoryFreeSlotCount with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.GetInventoryFreeSlotCount
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.EnqueueHET as IPC method (1)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.EnqueueHET with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.EnqueueHET
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.CanAutoLogin as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.CanAutoLogin with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.CanAutoLogin
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.Relog as IPC method (1)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.Relog with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.Relog
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.GetOptionRetainerSense as IPC method (0)
2025-09-22 20:45:00.946 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.GetOptionRetainerSense with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.GetOptionRetainerSense
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.SetOptionRetainerSense as IPC method (1)
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.SetOptionRetainerSense with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.SetOptionRetainerSense
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.GetOptionRetainerSenseThreshold as IPC method (0)
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.GetOptionRetainerSenseThreshold with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.GetOptionRetainerSenseThreshold
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.SetOptionRetainerSenseThreshold as IPC method (1)
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.SetOptionRetainerSenseThreshold with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.SetOptionRetainerSenseThreshold
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.GetClosestRetainerVentureSecondsRemaining as IPC method (1)
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.GetClosestRetainerVentureSecondsRemaining with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.GetClosestRetainerVentureSecondsRemaining
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Attempting to register IPC_PluginState.IsItemProtected as IPC method (1)
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] [EzIPC Provider] Registering IPC method AutoRetainer.PluginState.IsItemProtected with method AutoRetainer.Modules.EzIPCManagers.IPC_PluginState.IsItemProtected
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Modules.FCPointsUpdater
2025-09-22 20:45:00.947 +09:00 [DBG] [AutoRetainer] Registered interactable ProperOnLogin event: System.Action
2025-09-22 20:45:00.948 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.UI.Statistics.FcDataManager
2025-09-22 20:45:00.948 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.UI.Statistics.GilDisplayManager
2025-09-22 20:45:00.948 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.UI.Statistics.VentureStatsManager
2025-09-22 20:45:00.948 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Services.Lifestream.LifestreamIPC
2025-09-22 20:45:00.949 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.Teleport with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.TeleportToHome with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.TeleportToFC with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.TeleportToApartment with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.IsBusy with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.GetResidentialTerritory with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.GetHousePathData with wrapper AnyException
2025-09-22 20:45:00.950 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.GetSharedHousePathData with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.GetPlotEntrance with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.EnqueuePropertyShortcut with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.GetCurrentPlotInfo with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.EnqueueInnShortcut with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.HasApartment with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.EnterApartment with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.HasPrivateHouse with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.HasSharedEstate with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.HasFreeCompanyHouse with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.CanMoveToWorkshop with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.MoveToWorkshop with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to assign IPC method to LifestreamIPC.ExecuteCommand with wrapper AnyException
2025-09-22 20:45:00.951 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Attempting to register LifestreamIPC.OnHouseEnterError as IPC event (0)
2025-09-22 20:45:00.952 +09:00 [DBG] [AutoRetainer] [EzIPC Subscriber] Registering IPC event Lifestream.OnHouseEnterError with method AutoRetainer.Services.Lifestream.LifestreamIPC.OnHouseEnterError
2025-09-22 20:45:00.952 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.UI.Overlays.AutoBuyFuelOverlay
2025-09-22 20:45:00.952 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Services.TitleScreenButton
2025-09-22 20:45:00.952 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Services.AddonWatcher
2025-09-22 20:45:00.952 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Services.DataMigrator
2025-09-22 20:45:00.952 +09:00 [DBG] [AutoRetainer] Creating singleton instance of AutoRetainer.Services.WorkstationMonitor
2025-09-22 20:45:00.955 +09:00 [INF] [LOCALPLUGIN] Finished loading MarketBoardPlugin
2025-09-22 20:45:01.009 +09:00 [VRB] [GatherBuddy] Collected 1490 different gatherable items.
2025-09-22 20:45:01.013 +09:00 [VRB] [FontAtlasFactory] [Market board] Building from BuildFontsAsync.
2025-09-22 20:45:01.013 +09:00 [VRB] [FontAtlasFactory] [Market board] Building from BuildFontsAsync.
2025-09-22 20:45:01.013 +09:00 [VRB] [FontAtlasFactory] [EngageTimer] Building from BuildFontsAsync.
2025-09-22 20:45:01.013 +09:00 [VRB] [FontAtlasFactory] [Market board:RebuildFontsPrivateReal] 0x20175B66A40: PreBuild (at 0ms)
2025-09-22 20:45:01.013 +09:00 [VRB] [FontAtlasFactory] [EngageTimer:RebuildFontsPrivateReal] 0x20113E76C00: PreBuild (at 0ms)
2025-09-22 20:45:01.021 +09:00 [VRB] [FontAtlasFactory] [EngageTimer] 0x20113E76C00: AddFontFromImGuiHeapAllocatedMemory(0x201A69AB040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.021 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20175B66A40: AddFontFromImGuiHeapAllocatedMemory(0x201A7A5F040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.024 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20175B66A40: AddFontFromImGuiHeapAllocatedMemory(0x2011B35E060, 0x500, ...) from AddFontFromStream(NNBSP)
2025-09-22 20:45:01.031 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20175B66A40: AddFontFromImGuiHeapAllocatedMemory(0x201A9BAA040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.031 +09:00 [VRB] [FontAtlasFactory] [EngageTimer] 0x20113E76C00: AddFontFromImGuiHeapAllocatedMemory(0x201A8B04040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.039 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20175B66A40: AddFontFromImGuiHeapAllocatedMemory(0x201AAC51040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.041 +09:00 [VRB] [GatherBuddy] Collected 573 different gathering nodes
2025-09-22 20:45:01.046 +09:00 [VRB] [GatherBuddy] Collected 97 different cosmic fishing missions.
2025-09-22 20:45:01.046 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20175B66A40: AddFontFromImGuiHeapAllocatedMemory(0x201ABCF6040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.054 +09:00 [VRB] [GatherBuddy] Collected 148 different types of bait.
2025-09-22 20:45:01.072 +09:00 [VRB] [GatherBuddy] Collected 2412 different types of fish.
2025-09-22 20:45:01.129 +09:00 [VRB] [FontAtlasFactory] [InterfaceManager:RebuildFontsPrivateReal] 0x2012D25E080: Complete (at 746ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [SettingsTabAbout] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [NotificationManager:RebuildFontsPrivateReal] 0x20113E78410: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [SettingsTabAbout:RebuildFontsPrivateReal] 0x201A6065D50: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x201A5F91AB0: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [AntiAfkKick] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Hunt Buddy] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [DalamudChangelogWindow] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Hunt Buddy:RebuildFontsPrivateReal] 0x201A5FA8E40: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [DalamudChangelogWindow:RebuildFontsPrivateReal] 0x201A5FA97E0: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Globetrotter] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [TriadBuddy] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Ultrawide Cutscenes] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Globetrotter:RebuildFontsPrivateReal] 0x2011A8D5890: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Better Party Finder] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [FPS Plugin] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Better Party Finder:RebuildFontsPrivateReal] 0x2017CC41C50: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [FF Logs Viewer] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [FPS Plugin:RebuildFontsPrivateReal] 0x2019BB675B0: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [AntiAfkKick:RebuildFontsPrivateReal] 0x2012FEF7890: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [TriadBuddy:RebuildFontsPrivateReal] 0x20113E74F20: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Ultrawide Cutscenes:RebuildFontsPrivateReal] 0x201A5F15910: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Yes Already] Building from BuildFontsAsync.
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [FF Logs Viewer:RebuildFontsPrivateReal] 0x20175B678B0: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [Yes Already:RebuildFontsPrivateReal] 0x20175ACA090: PreBuild (at 0ms)
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [XIV Submarines Rewrite] Building from BuildFontsAsync.
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [DalamudChangelogWindow] 0x201A5FA97E0: AddFontFromImGuiHeapAllocatedMemory(0x201B1B9A040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [GatherBuddy] Building from BuildFontsAsync.
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [AutoRetainer] Building from BuildFontsAsync.
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] 0x201A5F91AB0: AddFontFromImGuiHeapAllocatedMemory(0x201AE98D040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [AutoRetainer:RebuildFontsPrivateReal] 0x2019BB705B0: PreBuild (at 0ms)
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [Better Party Finder] 0x2017CC41C50: AddFontFromImGuiHeapAllocatedMemory(0x201B3CE7040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [AntiAfkKick] 0x2012FEF7890: AddFontFromImGuiHeapAllocatedMemory(0x201B5E39040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [SettingsTabAbout] 0x201A6065D50: AddFontFromImGuiHeapAllocatedMemory(0x201AFA3C040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [Hunt Buddy] 0x201A5FA8E40: AddFontFromImGuiHeapAllocatedMemory(0x201B0AE6040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [TriadBuddy] 0x20113E74F20: AddFontFromImGuiHeapAllocatedMemory(0x201B6EEF040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] 0x20113E78410: AddFontFromImGuiHeapAllocatedMemory(0x201AD1D5460, 0xFB39C, ...) from AddFontFromStream(Asset(FontAwesomeFreeSolid))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [FPS Plugin] 0x2019BB675B0: AddFontFromImGuiHeapAllocatedMemory(0x201B4D93040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.155 +09:00 [VRB] [FontAtlasFactory] [XIV Submarines Rewrite:RebuildFontsPrivateReal] 0x201A5FA9310: PreBuild (at 0ms)
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [Globetrotter] 0x2011A8D5890: AddFontFromImGuiHeapAllocatedMemory(0x201B2C44040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.170 +09:00 [VRB] [FontAtlasFactory] [GatherBuddy:RebuildFontsPrivateReal] 0x2011A8D4A20: PreBuild (at 0ms)
2025-09-22 20:45:01.182 +09:00 [VRB] [FontAtlasFactory] [Ultrawide Cutscenes] 0x201A5F15910: AddFontFromImGuiHeapAllocatedMemory(0x201B9038040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.182 +09:00 [VRB] [FontAtlasFactory] [Yes Already] 0x20175ACA090: AddFontFromImGuiHeapAllocatedMemory(0x201B7F92040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.183 +09:00 [VRB] [FontAtlasFactory] [NotificationManager] 0x20113E78410: AddFontFromImGuiHeapAllocatedMemory(0x201BD2E7040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.183 +09:00 [VRB] [FontAtlasFactory] [XIV Submarines Rewrite] 0x201A5FA9310: AddFontFromImGuiHeapAllocatedMemory(0x201BC240040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.183 +09:00 [VRB] [FontAtlasFactory] [AutoRetainer] 0x2019BB705B0: AddFontFromImGuiHeapAllocatedMemory(0x201BB19D040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.183 +09:00 [VRB] [FontAtlasFactory] [FF Logs Viewer] 0x20175B678B0: AddFontFromImGuiHeapAllocatedMemory(0x201BA0EB040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.183 +09:00 [INF] Monitors set up!
2025-09-22 20:45:01.183 +09:00 [INF] Monitor 0: <0, 0> <2560, 1440> <0, 0> <2560, 1392>
2025-09-22 20:45:01.183 +09:00 [INF] Monitor 1: <2560, 130> <1920, 1080> <2560, 130> <1920, 1032>
2025-09-22 20:45:01.184 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x201A5F91AB0: Complete (at 29ms)
2025-09-22 20:45:01.191 +09:00 [VRB] [FontAtlasFactory] [GatherBuddy] 0x2011A8D4A20: AddFontFromImGuiHeapAllocatedMemory(0x201BE393040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.193 +09:00 [VRB] [GatherBuddy] Collected 509 different fishing spots.
2025-09-22 20:45:01.193 +09:00 [VRB] [FontAtlasFactory] [DalamudChangelogWindow:RebuildFontsPrivateReal] 0x201A5FA97E0: Complete (at 38ms)
2025-09-22 20:45:01.193 +09:00 [VRB] [FontAtlasFactory] [FF Logs Viewer:RebuildFontsPrivateReal] 0x20175B678B0: Complete (at 38ms)
2025-09-22 20:45:01.193 +09:00 [VRB] [FontAtlasFactory] [Globetrotter:RebuildFontsPrivateReal] 0x2011A8D5890: Complete (at 38ms)
2025-09-22 20:45:01.193 +09:00 [VRB] [FontAtlasFactory] [Yes Already:RebuildFontsPrivateReal] 0x20175ACA090: Complete (at 38ms)
2025-09-22 20:45:01.193 +09:00 [VRB] [FontAtlasFactory] [TriadBuddy:RebuildFontsPrivateReal] 0x20113E74F20: Complete (at 38ms)
2025-09-22 20:45:01.193 +09:00 [VRB] [FontAtlasFactory] [Better Party Finder:RebuildFontsPrivateReal] 0x2017CC41C50: Complete (at 38ms)
2025-09-22 20:45:01.195 +09:00 [VRB] [FontAtlasFactory] [SettingsTabAbout:RebuildFontsPrivateReal] 0x201A6065D50: Complete (at 40ms)
2025-09-22 20:45:01.195 +09:00 [VRB] [FontAtlasFactory] [Ultrawide Cutscenes:RebuildFontsPrivateReal] 0x201A5F15910: Complete (at 39ms)
2025-09-22 20:45:01.195 +09:00 [VRB] [FontAtlasFactory] [AntiAfkKick:RebuildFontsPrivateReal] 0x2012FEF7890: Complete (at 39ms)
2025-09-22 20:45:01.195 +09:00 [VRB] [FontAtlasFactory] [FPS Plugin:RebuildFontsPrivateReal] 0x2019BB675B0: Complete (at 39ms)
2025-09-22 20:45:01.198 +09:00 [VRB] [FontAtlasFactory] [GatherBuddy:RebuildFontsPrivateReal] 0x2011A8D4A20: Complete (at 28ms)
2025-09-22 20:45:01.198 +09:00 [VRB] [FontAtlasFactory] [AutoRetainer:RebuildFontsPrivateReal] 0x2019BB705B0: Complete (at 28ms)
2025-09-22 20:45:01.198 +09:00 [VRB] [FontAtlasFactory] [XIV Submarines Rewrite:RebuildFontsPrivateReal] 0x201A5FA9310: Complete (at 42ms)
2025-09-22 20:45:01.198 +09:00 [VRB] [FontAtlasFactory] [Hunt Buddy:RebuildFontsPrivateReal] 0x201A5FA8E40: Complete (at 43ms)
2025-09-22 20:45:01.200 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] Building from BuildFontsAsync.
2025-09-22 20:45:01.200 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x2012D25D210: PreBuild (at 0ms)
2025-09-22 20:45:01.206 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] 0x2012D25D210: AddFontFromImGuiHeapAllocatedMemory(0x201A2927040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.214 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay] 0x2012D25D210: AddFontFromImGuiHeapAllocatedMemory(0x201C143B040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.217 +09:00 [VRB] [FontAtlasFactory] [NotificationManager:RebuildFontsPrivateReal] 0x20113E78410: Complete (at 62ms)
2025-09-22 20:45:01.219 +09:00 [VRB] [WindowSystem] Saved preset for TitleScreenMenuOverlay
2025-09-22 20:45:01.231 +09:00 [VRB] [INTERFACE] [IM] Disposing 32 textures
2025-09-22 20:45:01.234 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758541500000 36175710857142
2025-09-22 20:45:01.240 +09:00 [DBG] [GatherBuddy] CurrentBait address 0x00007FF7422F02F4, baseOffset 0x00000000029502F4.
2025-09-22 20:45:01.241 +09:00 [DBG] [GatherBuddy] CurrentWeather address 0x00007FF7420D8B40, baseOffset 0x0000000002738B40.
2025-09-22 20:45:01.241 +09:00 [DBG] [GatherBuddy] SeTugType address 0x00007FF74230A930, baseOffset 0x000000000296A930.
2025-09-22 20:45:01.242 +09:00 [DBG] [GatherBuddy] ProcessChatBox address 0x00007FF7400F64B0, baseOffset 0x00000000007564B0.
2025-09-22 20:45:01.282 +09:00 [VRB] Config saved
2025-09-22 20:45:01.295 +09:00 [DBG] [GatherBuddy] UpdateFishCatch address 0x00007FF7407322F0, baseOffset 0x0000000000D922F0.
2025-09-22 20:45:01.296 +09:00 [VRB] CurrentProcessModules: Fetching fresh copy of current process modules.
2025-09-22 20:45:01.311 +09:00 [INF] [TriadBuddy] Loaded game data for cards:451, npcs:129
2025-09-22 20:45:01.326 +09:00 [VRB] [HM] Registering hook at 0x7FF7407322F0(ffxiv_dx11.exe+0xD922F0(.text+0xD912F0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:01.326 +09:00 [DBG] [GatherBuddy] Hooked onto UpdateFishCatch at address 0x00007FF7407322F0.
2025-09-22 20:45:01.327 +09:00 [VRB] [HM] Registering hook at 0x7FF7402469D0(ffxiv_dx11.exe+0x8A69D0(.text+0x8A59D0)) (minBytes=0x0, maxBytes=0x32)
2025-09-22 20:45:01.382 +09:00 [INF] [LOCALPLUGIN] Finished loading GatherBuddy
2025-09-22 20:45:01.382 +09:00 [INF] [PluginManager] ============= LoadPluginsAsync(DrawAvailableAsync) END =============
2025-09-22 20:45:01.382 +09:00 [INF] [PluginManager] Loaded plugins on boot
2025-09-22 20:45:01.383 +09:00 [DBG] [PluginManager] Starting plugin update check...
2025-09-22 20:45:01.385 +09:00 [DBG] [PluginManager] Update check found 1 available updates.
2025-09-22 20:45:01.389 +09:00 [INF] Saved cache to C:\Users\MonaT\AppData\Roaming\XIVLauncher\addon\Hooks\13.0.0.4\cachedSigs\2025.09.04.0000.0000.json
2025-09-22 20:45:01.390 +09:00 [INF] TROUBLESHOOTING:eyJMb2FkZWRQbHVnaW5zIjpbeyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJodHRwczovL2xvdmUucHVuaS5zaC9tZW50Lmpzb24iLCJXb3JraW5nUGx1Z2luSWQiOiIxZTBkMjk5MS04NTk4LTRlN2EtYWQ3OC00YTdmNDk0NTQ3ZjUiLCJJc1RoaXJkUGFydHkiOnRydWUsIkVmZmVjdGl2ZVZlcnNpb24iOiIxLjEzLjMiLCJFZmZlY3RpdmVBcGlMZXZlbCI6MTMsIkF1dGhvciI6ImRhZW1pdHVzLCBrYXdhaWkiLCJOYW1lIjoiWWVzIEFscmVhZHkiLCJQdW5jaGxpbmUiOiJDbGlja3MgWWVzIGluIHZhcmlvdXMgZGlhbG9ncyB5b3UndmUgc3BlY2lmaWVkLiIsIkRlc2NyaXB0aW9uIjoiVGlyZWQgb2YgY2xpY2tpbmcgeWVzIGluIFByYWV0b3JpdW0/IENhbid0IHN0YW5kIHVzaW5nIGtleXMgaW4gZHVuZ2VvbnM/IFdlbGwgZG8gSSBoYXZlIHRoZSBwbHVnaW4gZm9yIHlvdSEgWWVzIEFscmVhZHkgaXMgc3BlY2lmaWNhbGx5IGRlc2lnbmVkIHRvIGNsaWNrIGFsbCB0aGUgYW5ub3lpbmcgZGlhbG9ncyB5b3UndmUgZ3Jvd24gYWNjdXN0b21lZCBmYWNlcm9sbGluZy4gSnVzdCBlbnRlciB0aGUgZGlhbG9nIHRleHQsIGFuZCB5b3UnbGwgbmV2ZXIgc2VlIGl0IGFnYWluISBBbHNvIHdvcmtzIHdpdGggL3JlZ2V4LyBhbmQgaGFzIHNldmVyYWwgYnVpbHQtaW4gbW9kdWxlcyBmb3IgZGlhbG9ncyB0aGF0IGFyZW4ndCBxdWl0ZSB5ZXMvbm8gcHJvbXB0cyBidXQgYWN0IGxpa2UgaXQgKGxpa2UgZGVzeW50aCkuIiwiQ2hhbmdlbG9nIjoiIiwiVGFncyI6W10sIkNhdGVnb3J5VGFncyI6bnVsbCwiSXNIaWRlIjpmYWxzZSwiSW50ZXJuYWxOYW1lIjoiWWVzQWxyZWFkeSIsIkFzc2VtYmx5VmVyc2lvbiI6IjEuMTMuMyIsIlRlc3RpbmdBc3NlbWJseVZlcnNpb24iOiIxLjEwLjQuMCIsIklzVGVzdGluZ0V4Y2x1c2l2ZSI6ZmFsc2UsIlJlcG9VcmwiOiJodHRwczovL2dpdGh1Yi5jb20vUHVuaXNoWElWL1llc0FscmVhZHkiLCJBcHBsaWNhYmxlVmVyc2lvbiI6ImFueSIsIk1pbmltdW1EYWxhbXVkVmVyc2lvbiI6bnVsbCwiRGFsYW11ZEFwaUxldmVsIjoxMywiVGVzdGluZ0RhbGFtdWRBcGlMZXZlbCI6MTIsIkRvd25sb2FkQ291bnQiOjg2NDk0NCwiTGFzdFVwZGF0ZSI6MTc1Nzg3MzczMTg0NCwiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8vcHVuaS5zaC9hcGkvcGx1Z2lucy9kb3dubG9hZC8yNi9ZZXNBbHJlYWR5L3ZlcnNpb25zL2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3B1bmkuc2gvYXBpL3BsdWdpbnMvZG93bmxvYWQvMjYvWWVzQWxyZWFkeS92ZXJzaW9ucy9sYXRlc3QuemlwIiwiRG93bmxvYWRMaW5rVGVzdGluZyI6Imh0dHBzOi8vcHVuaS5zaC9hcGkvcGx1Z2lucy9kb3dubG9hZC8yNi9ZZXNBbHJlYWR5L3ZlcnNpb25zL3Rlc3RpbmcuemlwIiwiTG9hZFJlcXVpcmVkU3RhdGUiOjAsIkxvYWRTeW5jIjpmYWxzZSwiTG9hZFByaW9yaXR5IjowLCJDYW5VbmxvYWRBc3luYyI6ZmFsc2UsIlN1cHBvcnRzUHJvZmlsZXMiOnRydWUsIkltYWdlVXJscyI6bnVsbCwiSWNvblVybCI6Imh0dHBzOi8vczMucHVuaS5zaC9tZWRpYS9wbHVnaW4vMjYvaWNvbi12MXB2b25rcjZyLnBuZyIsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjpudWxsfSx7IkRpc2FibGVkIjpmYWxzZSwiVGVzdGluZyI6ZmFsc2UsIlNjaGVkdWxlZEZvckRlbGV0aW9uIjpmYWxzZSwiSW5zdGFsbGVkRnJvbVVybCI6IiIsIldvcmtpbmdQbHVnaW5JZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzVGhpcmRQYXJ0eSI6ZmFsc2UsIkVmZmVjdGl2ZVZlcnNpb24iOiIwLjEuMCIsIkVmZmVjdGl2ZUFwaUxldmVsIjoxMywiQXV0aG9yIjoiVEJEIiwiTmFtZSI6IlhJViBTdWJtYXJpbmVzIFJld3JpdGUiLCJQdW5jaGxpbmUiOiJOZXh0LWdlbmVyYXRpb24gc3VibWFyaW5lIHdvcmtzaG9wIHRvb2xzIChXSVApIiwiRGVzY3JpcHRpb24iOiJQbGFjZWhvbGRlciBtZXRhZGF0YSBmb3IgdGhlIHJld3JpdGUgcHJvamVjdC4gVXBkYXRlIGFzIHRoZSBuZXcgaW1wbGVtZW50YXRpb24gdGFrZXMgc2hhcGUuXHJcbiIsIkNoYW5nZWxvZyI6bnVsbCwiVGFncyI6WyJyZXdyaXRlIiwid2lwIl0sIkNhdGVnb3J5VGFncyI6bnVsbCwiSXNIaWRlIjpmYWxzZSwiSW50ZXJuYWxOYW1lIjoiWElWU3VibWFyaW5lc1Jld3JpdGUiLCJBc3NlbWJseVZlcnNpb24iOiIwLjEuMCIsIlRlc3RpbmdBc3NlbWJseVZlcnNpb24iOm51bGwsIklzVGVzdGluZ0V4Y2x1c2l2ZSI6ZmFsc2UsIlJlcG9VcmwiOiJodHRwczovL2dpdGh1Yi5jb20vbW9uYS10eS9YSVZTdWJtYXJpbmVzUmV0dXJuIiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOm51bGwsIkRvd25sb2FkQ291bnQiOjAsIkxhc3RVcGRhdGUiOjAsIkRvd25sb2FkTGlua0luc3RhbGwiOm51bGwsIkRvd25sb2FkTGlua1VwZGF0ZSI6bnVsbCwiRG93bmxvYWRMaW5rVGVzdGluZyI6bnVsbCwiTG9hZFJlcXVpcmVkU3RhdGUiOjAsIkxvYWRTeW5jIjpmYWxzZSwiTG9hZFByaW9yaXR5IjowLCJDYW5VbmxvYWRBc3luYyI6ZmFsc2UsIlN1cHBvcnRzUHJvZmlsZXMiOnRydWUsIkltYWdlVXJscyI6WyJpY29uLnBuZyJdLCJJY29uVXJsIjoiaWNvbi5wbmciLCJBY2NlcHRzRmVlZGJhY2siOnRydWUsIkZlZWRiYWNrTWVzc2FnZSI6bnVsbCwiX0RpcDE3Q2hhbm5lbCI6bnVsbH0seyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJPRkZJQ0lBTCIsIldvcmtpbmdQbHVnaW5JZCI6ImExOTAwZDVjLThlMmMtNDk0OS1hYjc3LWIxZWFlYTliMWZlZiIsIklzVGhpcmRQYXJ0eSI6ZmFsc2UsIkVmZmVjdGl2ZVZlcnNpb24iOiIxLjEzLjAuMCIsIkVmZmVjdGl2ZUFwaUxldmVsIjoxMywiQXV0aG9yIjoiTWdBbDJPNCIsIk5hbWUiOiJUcmlhZEJ1ZGR5IiwiUHVuY2hsaW5lIjoiVHJpcGxlIHRyaWFkIHNvbHZlciIsIkRlc2NyaXB0aW9uIjoiVHJpcGxlIHRyaWFkIHNvbHZlciwgTlBDIG1hdGNoZXMgb25seS5cbi0gaGlnaGxpZ2h0cyBuZXh0IG1vdmUgZHVyaW5nIG1pbmkgZ2FtZVxuLSBldmFsdWF0ZXMgYWxsIGRlY2sgcHJlc2V0cyBiZWZvcmUgbWF0Y2hcbi0gZGVjayBvcHRpbWl6ZXIgZm9yIE5QQ1xuLSBhZGRzIG1vcmUgZGV0YWlscyB0byBjYXJkIGNvbGxlY3Rpb24gd2luZG93IiwiQ2hhbmdlbG9nIjoiYXBpMTMgLyA3LjMiLCJUYWdzIjpbInNvbHZlciIsInRyaWFkIiwidHJpcGxlIHRyaWFkIl0sIkNhdGVnb3J5VGFncyI6bnVsbCwiSXNIaWRlIjpmYWxzZSwiSW50ZXJuYWxOYW1lIjoiVHJpYWRCdWRkeSIsIkFzc2VtYmx5VmVyc2lvbiI6IjEuMTMuMC4wIiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6bnVsbCwiSXNUZXN0aW5nRXhjbHVzaXZlIjpmYWxzZSwiUmVwb1VybCI6bnVsbCwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjAsIkRvd25sb2FkQ291bnQiOjg0NTk3MiwiTGFzdFVwZGF0ZSI6MTc1NDg2MTk2NiwiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvVHJpYWRCdWRkeT9pc1VwZGF0ZT1GYWxzZSZpc1Rlc3Rpbmc9RmFsc2UmYnJhbmNoPWFwaTkmaXNEaXAxNz1UcnVlIiwiRG93bmxvYWRMaW5rVXBkYXRlIjoiaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2dvYXRjb3JwL0RhbGFtdWRQbHVnaW5zL2FwaTkvcGx1Z2lucy9UcmlhZEJ1ZGR5L2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtUZXN0aW5nIjoiaHR0cHM6Ly9rYW1vcmkuZ29hdHMuZGV2L1BsdWdpbi9Eb3dubG9hZC9UcmlhZEJ1ZGR5P2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1UcnVlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOm51bGwsIkljb25VcmwiOm51bGwsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjoic3RhYmxlIn0seyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJPRkZJQ0lBTCIsIldvcmtpbmdQbHVnaW5JZCI6IjZiMDM2MGZiLThhOGYtNGU4My1iMGEzLTJmMzg1ZmUzMmMxOCIsIklzVGhpcmRQYXJ0eSI6ZmFsc2UsIkVmZmVjdGl2ZVZlcnNpb24iOiIwLjcuMy40IiwiRWZmZWN0aXZlQXBpTGV2ZWwiOjEzLCJBdXRob3IiOiJTb25hciBUZWFtIiwiTmFtZSI6IlNvbmFyIiwiUHVuY2hsaW5lIjoiQXV0b21hdGljYWxseSBzZW5kIGFuZCByZWNlaXZlIGh1bnQgYW5kIGZhdGUgcmVsYXlzLiIsIkRlc2NyaXB0aW9uIjoiQXV0b21hdGljYWxseSBzZW5kIGFuZCByZWNlaXZlIGh1bnQgYW5kIGZhdGUgcmVsYXlzLiAvc29uYXIgdG8gb3BlbiwgL3NvbmFyY29uZmlnIHRvIGNvbmZpZ3VyZS4gU3VwcG9ydCBpcyBvbmx5IHByb3ZpZGVkIGluIHRoZSBTb25hciBTdXBwb3J0IGRpc2NvcmQuIiwiQ2hhbmdlbG9nIjoiQ2hlY2sgU29uYXIgTmV3cyBmb3IgbW9yZSBpbmZvcm1hdGlvbjogaHR0cHM6Ly9uZXdzLmZmeGl2c29uYXIuY29tIiwiVGFncyI6WyJTb25hciIsIkh1bnQiLCJIdW50cyIsIkZhdGUiLCJGYXRlcyIsIlJlbGF5IiwiUmVsYXlzIiwiQXV0b21hdGVkIiwiQXV0b21hdGljIl0sIkNhdGVnb3J5VGFncyI6WyJ1dGlsaXR5Il0sIklzSGlkZSI6ZmFsc2UsIkludGVybmFsTmFtZSI6IlNvbmFyUGx1Z2luIiwiQXNzZW1ibHlWZXJzaW9uIjoiMC43LjMuNCIsIlRlc3RpbmdBc3NlbWJseVZlcnNpb24iOm51bGwsIklzVGVzdGluZ0V4Y2x1c2l2ZSI6ZmFsc2UsIlJlcG9VcmwiOiJodHRwczovL2Rpc2NvcmQuZ2cvSzd5MjRSciIsIkFwcGxpY2FibGVWZXJzaW9uIjoiYW55IiwiTWluaW11bURhbGFtdWRWZXJzaW9uIjpudWxsLCJEYWxhbXVkQXBpTGV2ZWwiOjEzLCJUZXN0aW5nRGFsYW11ZEFwaUxldmVsIjowLCJEb3dubG9hZENvdW50IjoxNTYxMTEwLCJMYXN0VXBkYXRlIjoxNzU3MDc4Mjk0LCJEb3dubG9hZExpbmtJbnN0YWxsIjoiaHR0cHM6Ly9rYW1vcmkuZ29hdHMuZGV2L1BsdWdpbi9Eb3dubG9hZC9Tb25hclBsdWdpbj9pc1VwZGF0ZT1GYWxzZSZpc1Rlc3Rpbmc9RmFsc2UmYnJhbmNoPWFwaTkmaXNEaXAxNz1UcnVlIiwiRG93bmxvYWRMaW5rVXBkYXRlIjoiaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2dvYXRjb3JwL0RhbGFtdWRQbHVnaW5zL2FwaTkvcGx1Z2lucy9Tb25hclBsdWdpbi9sYXRlc3QuemlwIiwiRG93bmxvYWRMaW5rVGVzdGluZyI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvU29uYXJQbHVnaW4/aXNVcGRhdGU9RmFsc2UmaXNUZXN0aW5nPVRydWUmYnJhbmNoPWFwaTkmaXNEaXAxNz1UcnVlIiwiTG9hZFJlcXVpcmVkU3RhdGUiOjAsIkxvYWRTeW5jIjpmYWxzZSwiTG9hZFByaW9yaXR5IjowLCJDYW5VbmxvYWRBc3luYyI6ZmFsc2UsIlN1cHBvcnRzUHJvZmlsZXMiOnRydWUsIkltYWdlVXJscyI6WyJodHRwczovL2Fzc2V0cy5mZnhpdnNvbmFyLmNvbS9kYWxhbXVkL2ltYWdlMS5wbmciLCJodHRwczovL2Fzc2V0cy5mZnhpdnNvbmFyLmNvbS9kYWxhbXVkL2ltYWdlMi5wbmciLCJodHRwczovL2Fzc2V0cy5mZnhpdnNvbmFyLmNvbS9kYWxhbXVkL2ltYWdlMy5wbmciLCJodHRwczovL2Fzc2V0cy5mZnhpdnNvbmFyLmNvbS9kYWxhbXVkL2ltYWdlNC5wbmciLCJodHRwczovL2Fzc2V0cy5mZnhpdnNvbmFyLmNvbS9kYWxhbXVkL2ltYWdlNS5wbmciXSwiSWNvblVybCI6Imh0dHBzOi8vYXNzZXRzLmZmeGl2c29uYXIuY29tL2RhbGFtdWQvbG9nby5wbmciLCJBY2NlcHRzRmVlZGJhY2siOmZhbHNlLCJGZWVkYmFja01lc3NhZ2UiOm51bGwsIl9EaXAxN0NoYW5uZWwiOiJzdGFibGUifSx7IkRpc2FibGVkIjpmYWxzZSwiVGVzdGluZyI6ZmFsc2UsIlNjaGVkdWxlZEZvckRlbGV0aW9uIjpmYWxzZSwiSW5zdGFsbGVkRnJvbVVybCI6Ik9GRklDSUFMIiwiV29ya2luZ1BsdWdpbklkIjoiZTE4MTE0MTQtNjY4YS00NDBkLTg1NWMtMzMyN2ExNWVhZjJkIiwiSXNUaGlyZFBhcnR5IjpmYWxzZSwiRWZmZWN0aXZlVmVyc2lvbiI6IjEuMTEuMC4wIiwiRWZmZWN0aXZlQXBpTGV2ZWwiOjEzLCJBdXRob3IiOiJmbWF1TmVrbyIsIk5hbWUiOiJNYXJrZXQgYm9hcmQiLCJQdW5jaGxpbmUiOiJCcm93c2UgdGhlIG1hcmtldCBib2FyZCBmcm9tIGFueXdoZXJlLiIsIkRlc2NyaXB0aW9uIjoiQnJvd3NlIHRoZSBtYXJrZXQgYm9hcmQuIC9wbWIgdG8gb3Blbi4iLCJDaGFuZ2Vsb2ciOiItIFVwZGF0ZWQgZm9yIDcuMyAmIEFQSSAxM1xuLSBBZGRlZCB0aGUgYWJpbGl0eSB0byBvcmdhbmlzZSBwbGF0ZXMgaW50byBmb2xkZXJzXG5cbkZvciB0cm91Ymxlc2hvb3RpbmcsIHBsZWFzZSBlbmFibGUgVHJvdWJsZXNob290aW5nIG1vZGUgKFNldHRpbmdzIC0+IFRyb3VibGVzaG9vdGluZyBtb2RlKSwgcmVwcm9kdWNlIHRoZSBpc3N1ZSwgdGhlbiBwb3N0IGFueSBsb2cgbGluZSBmcm9tIGAveGxsb2dgIHN0YXJ0aW5nIHdpdGggYFtUcm91Ymxlc2hvb3RpbmddYC4gVGhhbmtzISIsIlRhZ3MiOlsibWFya2V0IiwiYm9hcmQiLCJtYXJrZXRib2FyZCIsIm1hcmtldCBib2FyZCJdLCJDYXRlZ29yeVRhZ3MiOm51bGwsIklzSGlkZSI6ZmFsc2UsIkludGVybmFsTmFtZSI6Ik1hcmtldEJvYXJkUGx1Z2luIiwiQXNzZW1ibHlWZXJzaW9uIjoiMS4xMS4wLjAiLCJUZXN0aW5nQXNzZW1ibHlWZXJzaW9uIjpudWxsLCJJc1Rlc3RpbmdFeGNsdXNpdmUiOmZhbHNlLCJSZXBvVXJsIjoiaHR0cHM6Ly9naXRodWIuY29tL2ZtYXVOZWtvL01hcmtldEJvYXJkUGx1Z2luIiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjAsIkRvd25sb2FkQ291bnQiOjIzMDcxNTEsIkxhc3RVcGRhdGUiOjE3NTQ2MTA2MDMsIkRvd25sb2FkTGlua0luc3RhbGwiOiJodHRwczovL2thbW9yaS5nb2F0cy5kZXYvUGx1Z2luL0Rvd25sb2FkL01hcmtldEJvYXJkUGx1Z2luP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1GYWxzZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vZ29hdGNvcnAvRGFsYW11ZFBsdWdpbnMvYXBpOS9wbHVnaW5zL01hcmtldEJvYXJkUGx1Z2luL2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtUZXN0aW5nIjoiaHR0cHM6Ly9rYW1vcmkuZ29hdHMuZGV2L1BsdWdpbi9Eb3dubG9hZC9NYXJrZXRCb2FyZFBsdWdpbj9pc1VwZGF0ZT1GYWxzZSZpc1Rlc3Rpbmc9VHJ1ZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJMb2FkUmVxdWlyZWRTdGF0ZSI6MCwiTG9hZFN5bmMiOmZhbHNlLCJMb2FkUHJpb3JpdHkiOjAsIkNhblVubG9hZEFzeW5jIjpmYWxzZSwiU3VwcG9ydHNQcm9maWxlcyI6dHJ1ZSwiSW1hZ2VVcmxzIjpudWxsLCJJY29uVXJsIjpudWxsLCJBY2NlcHRzRmVlZGJhY2siOnRydWUsIkZlZWRiYWNrTWVzc2FnZSI6bnVsbCwiX0RpcDE3Q2hhbm5lbCI6InN0YWJsZSJ9LHsiRGlzYWJsZWQiOmZhbHNlLCJUZXN0aW5nIjpmYWxzZSwiU2NoZWR1bGVkRm9yRGVsZXRpb24iOmZhbHNlLCJJbnN0YWxsZWRGcm9tVXJsIjoiT0ZGSUNJQUwiLCJXb3JraW5nUGx1Z2luSWQiOiIxNjcyNWFkZi01YWUyLTRhMGItOTE4MS03Zjk0OGI5YjVhNDciLCJJc1RoaXJkUGFydHkiOmZhbHNlLCJFZmZlY3RpdmVWZXJzaW9uIjoiMS4yLjIuMCIsIkVmZmVjdGl2ZUFwaUxldmVsIjoxMywiQXV0aG9yIjoiRHJhZ29uIiwiTmFtZSI6Ikh1bnQgQnVkZHkiLCJQdW5jaGxpbmUiOiJIZWxwcyB5b3UgdHJhY2sgeW91ciBkYWlseSBodW50IGJpbGxzIiwiRGVzY3JpcHRpb24iOiJBIGRhaWx5IGh1bnQgYmlsbCB0cmFja2VyIiwiQ2hhbmdlbG9nIjoiLSBVcGRhdGVkIHRvIEFQSSAxMyIsIlRhZ3MiOlsiSHVudCIsIkRhaWx5IiwiVXRpbGl0eSJdLCJDYXRlZ29yeVRhZ3MiOm51bGwsIklzSGlkZSI6ZmFsc2UsIkludGVybmFsTmFtZSI6Ikh1bnRCdWRkeSIsIkFzc2VtYmx5VmVyc2lvbiI6IjEuMi4yLjAiLCJUZXN0aW5nQXNzZW1ibHlWZXJzaW9uIjpudWxsLCJJc1Rlc3RpbmdFeGNsdXNpdmUiOmZhbHNlLCJSZXBvVXJsIjoiaHR0cHM6Ly9naXRodWIuY29tL1NoZWVwR29NZWgvSHVudEJ1ZGR5IiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjAsIkRvd25sb2FkQ291bnQiOjQ1MDQwNSwiTGFzdFVwZGF0ZSI6MTc1NDY5MTMxNCwiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvSHVudEJ1ZGR5P2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1GYWxzZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vZ29hdGNvcnAvRGFsYW11ZFBsdWdpbnMvYXBpOS9wbHVnaW5zL0h1bnRCdWRkeS9sYXRlc3QuemlwIiwiRG93bmxvYWRMaW5rVGVzdGluZyI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvSHVudEJ1ZGR5P2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1UcnVlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOm51bGwsIkljb25VcmwiOm51bGwsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjoic3RhYmxlIn0seyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJPRkZJQ0lBTCIsIldvcmtpbmdQbHVnaW5JZCI6ImU1OWM4NmRmLWFkZjgtNGFmMy1hMDljLWYzYTM3NDEwOTI3ZiIsIklzVGhpcmRQYXJ0eSI6ZmFsc2UsIkVmZmVjdGl2ZVZlcnNpb24iOiIxLjIuMTUiLCJFZmZlY3RpdmVBcGlMZXZlbCI6MTMsIkF1dGhvciI6IkFubmEsIE1haW50YWluZWQgYnkgY2hpcnAiLCJOYW1lIjoiR2xvYmV0cm90dGVyIiwiUHVuY2hsaW5lIjoiQXV0b21hdGljYWxseSBzaG93cyB3aGVyZSB0cmVhc3VyZSBtYXBzIGFyZSBsb2NhdGVkLiIsIkRlc2NyaXB0aW9uIjoiQXV0b21hdGljYWxseSBzaG93cyB3aGVyZSB0cmVhc3VyZSBtYXBzIGFyZSBsb2NhdGVkLlxuXG5JY29uOiBNYXAgYnkgQWRyaWVuIENvcXVldCBmcm9tIHRoZSBOb3VuIFByb2plY3QiLCJDaGFuZ2Vsb2ciOiJGaXggYmFkIGZ1bmN0aW9uIHNpZ25hdHVyZSIsIlRhZ3MiOm51bGwsIkNhdGVnb3J5VGFncyI6bnVsbCwiSXNIaWRlIjpmYWxzZSwiSW50ZXJuYWxOYW1lIjoiR2xvYmV0cm90dGVyIiwiQXNzZW1ibHlWZXJzaW9uIjoiMS4yLjE1IiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6bnVsbCwiSXNUZXN0aW5nRXhjbHVzaXZlIjpmYWxzZSwiUmVwb1VybCI6Imh0dHBzOi8vZ2l0aHViLmNvbS9jaGlycHhpdi9HbG9iZXRyb3R0ZXIiLCJBcHBsaWNhYmxlVmVyc2lvbiI6ImFueSIsIk1pbmltdW1EYWxhbXVkVmVyc2lvbiI6bnVsbCwiRGFsYW11ZEFwaUxldmVsIjoxMywiVGVzdGluZ0RhbGFtdWRBcGlMZXZlbCI6MCwiRG93bmxvYWRDb3VudCI6OTg5NzQwLCJMYXN0VXBkYXRlIjoxNzU0NzAwMjIxLCJEb3dubG9hZExpbmtJbnN0YWxsIjoiaHR0cHM6Ly9rYW1vcmkuZ29hdHMuZGV2L1BsdWdpbi9Eb3dubG9hZC9HbG9iZXRyb3R0ZXI/aXNVcGRhdGU9RmFsc2UmaXNUZXN0aW5nPUZhbHNlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkRvd25sb2FkTGlua1VwZGF0ZSI6Imh0dHBzOi8vcmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbS9nb2F0Y29ycC9EYWxhbXVkUGx1Z2lucy9hcGk5L3BsdWdpbnMvR2xvYmV0cm90dGVyL2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtUZXN0aW5nIjoiaHR0cHM6Ly9rYW1vcmkuZ29hdHMuZGV2L1BsdWdpbi9Eb3dubG9hZC9HbG9iZXRyb3R0ZXI/aXNVcGRhdGU9RmFsc2UmaXNUZXN0aW5nPVRydWUmYnJhbmNoPWFwaTkmaXNEaXAxNz1UcnVlIiwiTG9hZFJlcXVpcmVkU3RhdGUiOjAsIkxvYWRTeW5jIjpmYWxzZSwiTG9hZFByaW9yaXR5IjowLCJDYW5VbmxvYWRBc3luYyI6ZmFsc2UsIlN1cHBvcnRzUHJvZmlsZXMiOnRydWUsIkltYWdlVXJscyI6bnVsbCwiSWNvblVybCI6bnVsbCwiQWNjZXB0c0ZlZWRiYWNrIjp0cnVlLCJGZWVkYmFja01lc3NhZ2UiOm51bGwsIl9EaXAxN0NoYW5uZWwiOiJzdGFibGUifSx7IkRpc2FibGVkIjpmYWxzZSwiVGVzdGluZyI6ZmFsc2UsIlNjaGVkdWxlZEZvckRlbGV0aW9uIjpmYWxzZSwiSW5zdGFsbGVkRnJvbVVybCI6Ik9GRklDSUFMIiwiV29ya2luZ1BsdWdpbklkIjoiNGQ4NGU1NTAtMDg1Mi00ZWVjLWE4MTItODYyOGQ4NTM3YzFiIiwiSXNUaGlyZFBhcnR5IjpmYWxzZSwiRWZmZWN0aXZlVmVyc2lvbiI6IjMuOC4zLjAiLCJFZmZlY3RpdmVBcGlMZXZlbCI6MTMsIkF1dGhvciI6Ik90dGVybWFuZGlhcyIsIk5hbWUiOiJHYXRoZXJCdWRkeSIsIlB1bmNobGluZSI6IlNpbXBsaWZ5IEdhdGhlcmluZyBhbmQgRmlzaGluZy4iLCJEZXNjcmlwdGlvbiI6IkFkZHMgY29tbWFuZHMgdG8gc2ltcGxpZnkgZ2F0aGVyaW5nIGJ5IGZpbmRpbmcgbm9kZXMgYW5kIGZpc2ggYW5kIHRoZWlyIGxvY2F0aW9uc1xudmlhIGl0ZW0gbmFtZSBhbmQgYSBVSSB0byBrZWVwIHRyYWNrIG9mIHNwZWNpYWwgdXB0aW1lIGFuZCB3ZWF0aGVyIGNvbmRpdGlvbnMuIiwiQ2hhbmdlbG9nIjoiIiwiVGFncyI6WyJHYXRoZXJpbmciLCJGaXNoaW5nIiwiTWluZXIiLCJCb3RhbmlzdCIsIldlYXRoZXIiLCJBbGFybXMiLCJUaW1lciJdLCJDYXRlZ29yeVRhZ3MiOm51bGwsIklzSGlkZSI6ZmFsc2UsIkludGVybmFsTmFtZSI6IkdhdGhlckJ1ZGR5IiwiQXNzZW1ibHlWZXJzaW9uIjoiMy44LjMuMCIsIlRlc3RpbmdBc3NlbWJseVZlcnNpb24iOm51bGwsIklzVGVzdGluZ0V4Y2x1c2l2ZSI6ZmFsc2UsIlJlcG9VcmwiOiJodHRwczovL2dpdGh1Yi5jb20vT3R0ZXJtYW5kaWFzL0dhdGhlckJ1ZGR5IiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjAsIkRvd25sb2FkQ291bnQiOjMyNzA3ODQsIkxhc3RVcGRhdGUiOjE3NTY5MTUzMjIsIkRvd25sb2FkTGlua0luc3RhbGwiOiJodHRwczovL2thbW9yaS5nb2F0cy5kZXYvUGx1Z2luL0Rvd25sb2FkL0dhdGhlckJ1ZGR5P2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1GYWxzZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vZ29hdGNvcnAvRGFsYW11ZFBsdWdpbnMvYXBpOS9wbHVnaW5zL0dhdGhlckJ1ZGR5L2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtUZXN0aW5nIjoiaHR0cHM6Ly9rYW1vcmkuZ29hdHMuZGV2L1BsdWdpbi9Eb3dubG9hZC9HYXRoZXJCdWRkeT9pc1VwZGF0ZT1GYWxzZSZpc1Rlc3Rpbmc9VHJ1ZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJMb2FkUmVxdWlyZWRTdGF0ZSI6MCwiTG9hZFN5bmMiOmZhbHNlLCJMb2FkUHJpb3JpdHkiOjAsIkNhblVubG9hZEFzeW5jIjpmYWxzZSwiU3VwcG9ydHNQcm9maWxlcyI6dHJ1ZSwiSW1hZ2VVcmxzIjpudWxsLCJJY29uVXJsIjpudWxsLCJBY2NlcHRzRmVlZGJhY2siOnRydWUsIkZlZWRiYWNrTWVzc2FnZSI6bnVsbCwiX0RpcDE3Q2hhbm5lbCI6InN0YWJsZSJ9LHsiRGlzYWJsZWQiOmZhbHNlLCJUZXN0aW5nIjpmYWxzZSwiU2NoZWR1bGVkRm9yRGVsZXRpb24iOmZhbHNlLCJJbnN0YWxsZWRGcm9tVXJsIjoiT0ZGSUNJQUwiLCJXb3JraW5nUGx1Z2luSWQiOiI2ZjVlZTYzYS1iNzA3LTRmNWItYjZkNC05NWE4ODcyNzBiY2UiLCJJc1RoaXJkUGFydHkiOmZhbHNlLCJFZmZlY3RpdmVWZXJzaW9uIjoiMS43LjAuMyIsIkVmZmVjdGl2ZUFwaUxldmVsIjoxMywiQXV0aG9yIjoiQ2FyYXhpIiwiTmFtZSI6IkZQUyBQbHVnaW4iLCJQdW5jaGxpbmUiOiJEaXNwbGF5IGdhbWUgRlBTLiIsIkRlc2NyaXB0aW9uIjoiRGlzcGxheSBnYW1lIEZQUy4gSXQgcmVhbGx5IGlzIHRoYXQgc2ltcGxlLiIsIkNoYW5nZWxvZyI6IkFQSTEzIiwiVGFncyI6bnVsbCwiQ2F0ZWdvcnlUYWdzIjpudWxsLCJJc0hpZGUiOmZhbHNlLCJJbnRlcm5hbE5hbWUiOiJGUFNQbHVnaW4iLCJBc3NlbWJseVZlcnNpb24iOiIxLjcuMC4zIiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6bnVsbCwiSXNUZXN0aW5nRXhjbHVzaXZlIjpmYWxzZSwiUmVwb1VybCI6Imh0dHBzOi8vZ2l0aHViLmNvbS9DYXJheGkvRlBTUGx1Z2luIiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjAsIkRvd25sb2FkQ291bnQiOjU3NTc5NywiTGFzdFVwZGF0ZSI6MTc1NDYyMzkyNiwiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvRlBTUGx1Z2luP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1GYWxzZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vZ29hdGNvcnAvRGFsYW11ZFBsdWdpbnMvYXBpOS9wbHVnaW5zL0ZQU1BsdWdpbi9sYXRlc3QuemlwIiwiRG93bmxvYWRMaW5rVGVzdGluZyI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvRlBTUGx1Z2luP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1UcnVlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOm51bGwsIkljb25VcmwiOm51bGwsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjoic3RhYmxlIn0seyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJodHRwczovL2dpdGh1Yi5jb20vQWlyZWlsL015RGFsYW11ZFBsdWdpbnMvcmF3L21hc3Rlci9wbHVnaW5tYXN0ZXIuanNvbiIsIldvcmtpbmdQbHVnaW5JZCI6IjI1OWFhYzY4LThkMTQtNDIyOC04ZDdhLWRhZWVlOTViYjhhOCIsIklzVGhpcmRQYXJ0eSI6dHJ1ZSwiRWZmZWN0aXZlVmVyc2lvbiI6IjIuMy4wLjAiLCJFZmZlY3RpdmVBcGlMZXZlbCI6MTMsIkF1dGhvciI6IkFpcmVpbCIsIk5hbWUiOiJGRiBMb2dzIFZpZXdlciIsIlB1bmNobGluZSI6IlZpZXcgRkYgTG9ncyByYW5raW5nIHBlcmNlbnRpbGVzIGluLWdhbWUuIiwiRGVzY3JpcHRpb24iOiJWaWV3IEZGIExvZ3MgcmFua2luZyBwZXJjZW50aWxlcyBpbi1nYW1lLiBBZGQgYSBjb250ZXh0IG1lbnUgdG8gb3BlbiBzb21lb25lJ3MgbG9ncy4gTGF5b3V0LCBzdHlsZSwgc3RhdHMsIGRlZmF1bHQgbWV0cmljLCBhbmQgbW9yZSBhcmUgYWxsIGN1c3RvbWl6YWJsZSBpbiB0aGUgc2V0dGluZ3MuIiwiQ2hhbmdlbG9nIjoiLTIuMy4wLjBcbiAgICBEZWZhdWx0IGxheW91dCB1cGRhdGUgKyA3LjMgdXBkYXRlLiIsIlRhZ3MiOlsiRkZMb2dzIl0sIkNhdGVnb3J5VGFncyI6bnVsbCwiSXNIaWRlIjpmYWxzZSwiSW50ZXJuYWxOYW1lIjoiRkZMb2dzVmlld2VyIiwiQXNzZW1ibHlWZXJzaW9uIjoiMi4zLjAuMCIsIlRlc3RpbmdBc3NlbWJseVZlcnNpb24iOm51bGwsIklzVGVzdGluZ0V4Y2x1c2l2ZSI6ZmFsc2UsIlJlcG9VcmwiOiJodHRwczovL2dpdGh1Yi5jb20vQWlyZWlsL0ZGTG9nc1ZpZXdlciIsIkFwcGxpY2FibGVWZXJzaW9uIjoiYW55IiwiTWluaW11bURhbGFtdWRWZXJzaW9uIjpudWxsLCJEYWxhbXVkQXBpTGV2ZWwiOjEzLCJUZXN0aW5nRGFsYW11ZEFwaUxldmVsIjpudWxsLCJEb3dubG9hZENvdW50IjowLCJMYXN0VXBkYXRlIjoxNzU0NTA3Njc1LCJEb3dubG9hZExpbmtJbnN0YWxsIjoiaHR0cHM6Ly9naXRodWIuY29tL0FpcmVpbC9NeURhbGFtdWRQbHVnaW5zL3Jhdy9tYXN0ZXIvcGx1Z2lucy9GRkxvZ3NWaWV3ZXIvbGF0ZXN0LnppcCIsIkRvd25sb2FkTGlua1VwZGF0ZSI6Imh0dHBzOi8vZ2l0aHViLmNvbS9BaXJlaWwvTXlEYWxhbXVkUGx1Z2lucy9yYXcvbWFzdGVyL3BsdWdpbnMvRkZMb2dzVmlld2VyL2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtUZXN0aW5nIjoiaHR0cHM6Ly9naXRodWIuY29tL0FpcmVpbC9NeURhbGFtdWRQbHVnaW5zL3Jhdy9tYXN0ZXIvcGx1Z2lucy9GRkxvZ3NWaWV3ZXIvbGF0ZXN0LnppcCIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOlsiaHR0cHM6Ly9naXRodWIuY29tL0FpcmVpbC9GRkxvZ3NWaWV3ZXIvcmF3L21hc3Rlci9yZXMvaW1hZ2UxLnBuZyIsImh0dHBzOi8vZ2l0aHViLmNvbS9BaXJlaWwvRkZMb2dzVmlld2VyL3Jhdy9tYXN0ZXIvcmVzL2ltYWdlMi5wbmciXSwiSWNvblVybCI6Imh0dHBzOi8vZ2l0aHViLmNvbS9BaXJlaWwvRkZMb2dzVmlld2VyL3Jhdy9tYXN0ZXIvcmVzL2ljb24ucG5nIiwiQWNjZXB0c0ZlZWRiYWNrIjp0cnVlLCJGZWVkYmFja01lc3NhZ2UiOm51bGwsIl9EaXAxN0NoYW5uZWwiOm51bGx9LHsiRGlzYWJsZWQiOmZhbHNlLCJUZXN0aW5nIjpmYWxzZSwiU2NoZWR1bGVkRm9yRGVsZXRpb24iOmZhbHNlLCJJbnN0YWxsZWRGcm9tVXJsIjoiT0ZGSUNJQUwiLCJXb3JraW5nUGx1Z2luSWQiOiIwNjUyMzBhYi0wMjM1LTQ4MGMtYWI4Ni00ZTdlZTE4NTIwNTQiLCJJc1RoaXJkUGFydHkiOmZhbHNlLCJFZmZlY3RpdmVWZXJzaW9uIjoiMi40LjQuMCIsIkVmZmVjdGl2ZUFwaUxldmVsIjoxMywiQXV0aG9yIjoiWG9ydXMiLCJOYW1lIjoiRW5nYWdlVGltZXIiLCJQdW5jaGxpbmUiOiJDb21wbGV0ZSBjb21iYXQgY291bnRkb3duLCBwdWxsIHRpbWVyIGFuZCBPQlMgb3ZlcmxheSIsIkRlc2NyaXB0aW9uIjoiRmluYWxseSwgZGlzcGxheSBhbGwgdGhlIG51bWJlcnMgd2hlbiBzdGFydGluZyBhIGNvdW50ZG93biFcbkNvbWVzIHdpdGggYW4gYWNjdXJhdGUgcHVsbCB0aW1lciB0aGF0IHN0YXJ0cyB3aGVuIHlvdSBvciBhbnkgb2YgeW91ciBwYXJ0eSBtZW1iZXJzIGVudGVyIGNvbWJhdC5cblxuRmVhdHVyZXMgOlxuLSBCaWcgY291bnRkb3duIHRoYXQgZGlzcGxheXMgYWxsIHRoZSBudW1iZXJzXG4tIEN1c3RvbWl6YWJsZSBiaWctbnVtYmVyIHRleHR1cmVzIChkZWZhdWx0LCB5ZWxsb3csIFdvVyBzdHlsZSBvciBjdXN0b20gZmlsZXMpXG4tIERpc3BsYXkgY29tYmF0IHRpbWUgaW4gdGhlIFNlcnZlciBJbmZvIEJhciwgYW5kL29yIGEgZmxvYXRpbmcgd2luZG93XG4tIE9CUyBicm93c2VyLXNvdXJjZSBjb21wYXRpYmxlIHBhZ2Ugd2l0aCBjb3VudGRvd24gYW5kIGN1cnJlbnQgY29tYmF0IGR1cmF0aW9uXG5cbldoeSB0aGUgT0JTIHRoaW5nID8gSSBsaWtlIGhhdmluZyBhIGJpZyBzdHlsaXplZCBzdG9wd2F0Y2ggaW4gbXkgcHJpdmF0ZSByZWNvcmRpbmdzIGFuZCB0aGF0J3MgYWJvdXQgaXQuXG5TZWUgcmVwb3NpdG9yeSBmb3IgbW9yZSBpbmZvLlxuXG5GZWVsIGZyZWUgc28gc2VuZCBtZSBmZWVkYmFjaywgYnVncyBhbmQgcG90ZW50aWFsIGltcHJvdmVtZW50cyEgQ2hlY2sgdGhlIGFib3V0IHBhZ2UgaW4gdGhlIHBsdWdpbiBzZXR0aW5ncyBmb3IgbW9yZVxuaW5mby4gUGxlYXNlIHJlcG9ydCB1cmdlbnQgZmVlZGJhY2sgb24gR2l0SHViIG9yIFhJVkxhdW5jaGVyIERpc2NvcmQncyBzdXBwb3J0IHRocmVhZCBmb3IgdGhpcyBwbHVnaW4uIiwiQ2hhbmdlbG9nIjoiLSBVcGRhdGUgZm9yIEFQSTEzIiwiVGFncyI6WyJjb3VudGRvd24iLCJwdWxsIHRpbWVyIiwic3RvcHdhdGNoIiwiT0JTIiwib3ZlcmxheSIsInN0cmVhbWluZyIsImNvbWJhdCJdLCJDYXRlZ29yeVRhZ3MiOlsiam9icyJdLCJJc0hpZGUiOmZhbHNlLCJJbnRlcm5hbE5hbWUiOiJFbmdhZ2VUaW1lciIsIkFzc2VtYmx5VmVyc2lvbiI6IjIuNC40LjAiLCJUZXN0aW5nQXNzZW1ibHlWZXJzaW9uIjpudWxsLCJJc1Rlc3RpbmdFeGNsdXNpdmUiOmZhbHNlLCJSZXBvVXJsIjoiaHR0cHM6Ly9naXRodWIuY29tL3hvcnVzL0VuZ2FnZVRpbWVyIiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjAsIkRvd25sb2FkQ291bnQiOjkzMTk0NiwiTGFzdFVwZGF0ZSI6MTc1NDY2MjcxMywiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvRW5nYWdlVGltZXI/aXNVcGRhdGU9RmFsc2UmaXNUZXN0aW5nPUZhbHNlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkRvd25sb2FkTGlua1VwZGF0ZSI6Imh0dHBzOi8vcmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbS9nb2F0Y29ycC9EYWxhbXVkUGx1Z2lucy9hcGk5L3BsdWdpbnMvRW5nYWdlVGltZXIvbGF0ZXN0LnppcCIsIkRvd25sb2FkTGlua1Rlc3RpbmciOiJodHRwczovL2thbW9yaS5nb2F0cy5kZXYvUGx1Z2luL0Rvd25sb2FkL0VuZ2FnZVRpbWVyP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1UcnVlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOlsiaHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL3hvcnVzL0VuZ2FnZVRpbWVyL21haW4vaW1hZ2VzL2ltYWdlMS5wbmciLCJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20veG9ydXMvRW5nYWdlVGltZXIvbWFpbi9pbWFnZXMvaW1hZ2UyLnBuZyIsImh0dHBzOi8vcmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbS94b3J1cy9FbmdhZ2VUaW1lci9tYWluL2ltYWdlcy9pbWFnZTMucG5nIl0sIkljb25VcmwiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20veG9ydXMvRW5nYWdlVGltZXIvbWFpbi9pbWFnZXMvaWNvbi5wbmciLCJBY2NlcHRzRmVlZGJhY2siOnRydWUsIkZlZWRiYWNrTWVzc2FnZSI6bnVsbCwiX0RpcDE3Q2hhbm5lbCI6InN0YWJsZSJ9LHsiRGlzYWJsZWQiOmZhbHNlLCJUZXN0aW5nIjpmYWxzZSwiU2NoZWR1bGVkRm9yRGVsZXRpb24iOmZhbHNlLCJJbnN0YWxsZWRGcm9tVXJsIjoiT0ZGSUNJQUwiLCJXb3JraW5nUGx1Z2luSWQiOiIyMjg4OWU1ZS1lMTJiLTRlZmItOTM2ZS1lODkzYzJkMDgyY2MiLCJJc1RoaXJkUGFydHkiOmZhbHNlLCJFZmZlY3RpdmVWZXJzaW9uIjoiMS4wLjAuNiIsIkVmZmVjdGl2ZUFwaUxldmVsIjoxMywiQXV0aG9yIjoiZ29hdCIsIk5hbWUiOiJVbHRyYXdpZGUgQ3V0c2NlbmVzIiwiUHVuY2hsaW5lIjoiUmVtb3ZlIGxldHRlcmJveGluZyBmcm9tIGN1dHNjZW5lcyBvbiB1bHRyYXdpZGUgbW9uaXRvcnMuIiwiRGVzY3JpcHRpb24iOiJUaGlzIHBsdWdpbiByZW1vdmVzIHRoZSBcImxldHRlcmJveGluZ1wiIGJhcnMgd2hlbiB3YXRjaGluZyBjdXRzY2VuZXMgb24gdWx0cmF3aWRlIG1vbml0b3JzLlxuQmV3YXJlISBZb3UgbWF5IHNlZSB0aGluZ3MgdGhhdCB5b3UgYXJlIG5vdCBzdXBwb3NlZCB0byBiZSBzZWVpbmcsIHN1Y2ggYXMgdXBjb21pbmcgTlBDcyBwb3BwaW5nIGluIG9yIFQtcG9zaW5nLlxuXG5UaGFua3MgdG8gYWVycyBmb3IgZmluZGluZyB0aGlzLiIsIkNoYW5nZWxvZyI6Ii0gVXBkYXRlZCBmb3IgcGF0Y2ggNy4zIiwiVGFncyI6WyJ1bHRyYXdpZGUiLCJjdXRzY2VuZSIsImZ1bGxzY3JlZW4iXSwiQ2F0ZWdvcnlUYWdzIjpudWxsLCJJc0hpZGUiOmZhbHNlLCJJbnRlcm5hbE5hbWUiOiJEYWxhbXVkLkZ1bGxzY3JlZW5DdXRzY2VuZXMiLCJBc3NlbWJseVZlcnNpb24iOiIxLjAuMC42IiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6bnVsbCwiSXNUZXN0aW5nRXhjbHVzaXZlIjpmYWxzZSwiUmVwb1VybCI6Imh0dHBzOi8vZ2l0aHViLmNvbS9nb2FhYXRzL0RhbGFtdWQuRnVsbHNjcmVlbkN1dHNjZW5lcyIsIkFwcGxpY2FibGVWZXJzaW9uIjoiYW55IiwiTWluaW11bURhbGFtdWRWZXJzaW9uIjpudWxsLCJEYWxhbXVkQXBpTGV2ZWwiOjEzLCJUZXN0aW5nRGFsYW11ZEFwaUxldmVsIjowLCJEb3dubG9hZENvdW50Ijo0Njg0NSwiTGFzdFVwZGF0ZSI6MTc1NDcwNjM3NSwiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvRGFsYW11ZC5GdWxsc2NyZWVuQ3V0c2NlbmVzP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1GYWxzZSZicmFuY2g9YXBpOSZpc0RpcDE3PVRydWUiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vZ29hdGNvcnAvRGFsYW11ZFBsdWdpbnMvYXBpOS9wbHVnaW5zL0RhbGFtdWQuRnVsbHNjcmVlbkN1dHNjZW5lcy9sYXRlc3QuemlwIiwiRG93bmxvYWRMaW5rVGVzdGluZyI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvRGFsYW11ZC5GdWxsc2NyZWVuQ3V0c2NlbmVzP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1UcnVlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOm51bGwsIkljb25VcmwiOm51bGwsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjoic3RhYmxlIn0seyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJPRkZJQ0lBTCIsIldvcmtpbmdQbHVnaW5JZCI6IjZmN2I0YWE4LTdmOTctNDVhNy1hOWEyLWE3MzhiMTg4NTYwNSIsIklzVGhpcmRQYXJ0eSI6ZmFsc2UsIkVmZmVjdGl2ZVZlcnNpb24iOiIxLjUuNy4wIiwiRWZmZWN0aXZlQXBpTGV2ZWwiOjEzLCJBdXRob3IiOiJJbmZpLCBhc2gsIEFubmEiLCJOYW1lIjoiQmV0dGVyIFBhcnR5IEZpbmRlciIsIlB1bmNobGluZSI6IlVzZSBhZHZhbmNlZCBQYXJ0eSBGaW5kZXIgZmlsdGVyIHByZXNldHMuIiwiRGVzY3JpcHRpb24iOiJGaWx0ZXIgdGhlIHBhcnR5IGZpbmRlciBiZXR0ZXIuXG5cbkFsbCB2YW5pbGxhIGZpbHRlcmluZyBpcyBhdmFpbGFibGUsIHBsdXM6XG5cbi0gQ3JlYXRlIGZpbHRlciBwcmVzZXRzIHRvIGVhc2lseSBzd2l0Y2ggYmV0d2VlblxuLSBXaGl0ZWxpc3QvYmxhY2tsaXN0IHNwZWNpZmljIGR1dGllcyBhbmQgY2F0ZWdvcmllc1xuLSBBZGQgY2hhcmFjdGVyIG5hbWVzIHRvIGhpZGUgUEZzIGZyb21cbi0gUmVtb3ZlIFBGcyBhYm92ZSBtYXhpbXVtIGl0ZW0gbGV2ZWxcbi0gRmlsdGVyIG9uIGl0ZW0gbGV2ZWwgcmFuZ2Vcbi0gRmlsdGVyIG9uIG11bHRpcGxlIGpvYnMgYW5kIHNsb3RzIChleC4gTUNIICsgR05CIGF2YWlsYWJsZSkiLCJDaGFuZ2Vsb2ciOiItIEFQSSAxMyIsIlRhZ3MiOm51bGwsIkNhdGVnb3J5VGFncyI6bnVsbCwiSXNIaWRlIjpmYWxzZSwiSW50ZXJuYWxOYW1lIjoiQmV0dGVyUGFydHlGaW5kZXIiLCJBc3NlbWJseVZlcnNpb24iOiIxLjUuNy4wIiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6IjEuNC4wLjAiLCJJc1Rlc3RpbmdFeGNsdXNpdmUiOmZhbHNlLCJSZXBvVXJsIjoiaHR0cHM6Ly9naXRodWIuY29tL0luZml6aWVydDkwL0JldHRlclBhcnR5RmluZGVyIiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOjEwLCJEb3dubG9hZENvdW50Ijo5OTEyNCwiTGFzdFVwZGF0ZSI6MTc1NDYxMDgyOCwiRG93bmxvYWRMaW5rSW5zdGFsbCI6Imh0dHBzOi8va2Ftb3JpLmdvYXRzLmRldi9QbHVnaW4vRG93bmxvYWQvQmV0dGVyUGFydHlGaW5kZXI/aXNVcGRhdGU9RmFsc2UmaXNUZXN0aW5nPUZhbHNlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkRvd25sb2FkTGlua1VwZGF0ZSI6Imh0dHBzOi8vcmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbS9nb2F0Y29ycC9EYWxhbXVkUGx1Z2lucy9hcGk5L3BsdWdpbnMvQmV0dGVyUGFydHlGaW5kZXIvbGF0ZXN0LnppcCIsIkRvd25sb2FkTGlua1Rlc3RpbmciOiJodHRwczovL2thbW9yaS5nb2F0cy5kZXYvUGx1Z2luL0Rvd25sb2FkL0JldHRlclBhcnR5RmluZGVyP2lzVXBkYXRlPUZhbHNlJmlzVGVzdGluZz1UcnVlJmJyYW5jaD1hcGk5JmlzRGlwMTc9VHJ1ZSIsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOm51bGwsIkljb25VcmwiOm51bGwsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjoidGVzdGluZy1saXZlIn0seyJEaXNhYmxlZCI6ZmFsc2UsIlRlc3RpbmciOmZhbHNlLCJTY2hlZHVsZWRGb3JEZWxldGlvbiI6ZmFsc2UsIkluc3RhbGxlZEZyb21VcmwiOiJodHRwczovL2xvdmUucHVuaS5zaC9tZW50Lmpzb24iLCJXb3JraW5nUGx1Z2luSWQiOiIyZjVlMGM0OC0yMWViLTQwMGMtOGNiNS0wZjYzZDIwYmIyZjgiLCJJc1RoaXJkUGFydHkiOnRydWUsIkVmZmVjdGl2ZVZlcnNpb24iOiI0LjUuMi4xIiwiRWZmZWN0aXZlQXBpTGV2ZWwiOjEzLCJBdXRob3IiOiJrYXdhaWksIE5pZ2h0bWFyZVhJViIsIk5hbWUiOiJBdXRvUmV0YWluZXIiLCJQdW5jaGxpbmUiOiJDb2xsZWN0IGFuZCBhc3NpZ24gdmVudHVyZXMgdG8geW91ciByZXRhaW5lcnMgZnJvbSB0aGUgY29tZm9ydCBvZiB5b3VyIGJlZC4iLCJEZXNjcmlwdGlvbiI6IkNvbGxlY3QgYW5kIGFzc2lnbiB2ZW50dXJlcyB0byB5b3VyIHJldGFpbmVycyBmcm9tIHRoZSBjb21mb3J0IG9mIHlvdXIgYmVkLiIsIkNoYW5nZWxvZyI6Ii0gTXVsdGkgTW9kZSBleHBlcnQgZGVsaXZlcnk6XG4tIC0gRml4ZWQgYW4gaXNzdWUgd2hlcmUgXCJGaW5hbGl6ZSBieSBwdXJjaGFzaW5nXCIgb3B0aW9uIHdvdWxkIHRyaWdnZXIgMS1taW51dGUgbG9uZyBsb2Nrb3V0XG4tIC0gQWRkZWQgYSBwb3NzaWJpbGl0eSB0byB0cmlnZ2VyIGRlbGl2ZXJ5IHdoZW4gYW1vdW50IG9mIHZlbnR1cmVzIGRyb3BzIGJlbG93IGNlcnRhaW4gYW1vdW50XG4tIC0gQWRkZWQgYW4gb3B0aW9uIHRvIHVzZSBQcmlvcml0eSBTZWFsIEFsbG93YW5jZSBiZWZvcmUgZGVsaXZlcnlcbi0gQWRkZWQgcG9zc2liaWxpdHkgdG8gdXNlIFNoYXJlZCBFc3RhdGUgZm9yIE11bHRpIE1vZGUgdGVsZXBvcnRhdGlvblxuLSAtIFlvdSBtdXN0IGhhdmUgdGVzdGluZyB2ZXJzaW9uIG9mIExpZmVzdHJlYW0sIG9yIGl0IHdvbid0IHdvcmtcbi0gLSBEb24ndCBmb3JnZXQgdG8gcmVnaXN0ZXIgeW91ciBzaGFyZWQgZXN0YXRlIGluIExpZmVzdHJlYW1cbi0gLSBUaGlzIG9wdGlvbiBpcyBleHBlcmltZW50YWwgYW5kIHByb2JhYmx5IHRoZSBtb3N0IHVudGVzdGVkIG9wdGlvbiB0aGF0IGhhcyBldmVyIGJlZW4gaW4gQXV0b1JldGFpbmVyIHNvIGRvbid0IGxlYXZlIHVuYXR0ZW5kZWRcbi0gQWRkZWQgbmV3IHN1Ym1hcmluZSBzZWN0b3JzIChieSBZc0VtZWkpXG4iLCJUYWdzIjpbXSwiQ2F0ZWdvcnlUYWdzIjpudWxsLCJJc0hpZGUiOmZhbHNlLCJJbnRlcm5hbE5hbWUiOiJBdXRvUmV0YWluZXIiLCJBc3NlbWJseVZlcnNpb24iOiI0LjUuMi4xIiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6bnVsbCwiSXNUZXN0aW5nRXhjbHVzaXZlIjpmYWxzZSwiUmVwb1VybCI6Imh0dHBzOi8vZ2l0aHViLmNvbS9QdW5pc2hYSVYvQXV0b1JldGFpbmVyIiwiQXBwbGljYWJsZVZlcnNpb24iOiJhbnkiLCJNaW5pbXVtRGFsYW11ZFZlcnNpb24iOm51bGwsIkRhbGFtdWRBcGlMZXZlbCI6MTMsIlRlc3RpbmdEYWxhbXVkQXBpTGV2ZWwiOm51bGwsIkRvd25sb2FkQ291bnQiOjE1MDQ3MjksIkxhc3RVcGRhdGUiOjE3NTYxMTAwNTk2ODUsIkRvd25sb2FkTGlua0luc3RhbGwiOiJodHRwczovL3B1bmkuc2gvYXBpL3BsdWdpbnMvZG93bmxvYWQvMjQvQXV0b1JldGFpbmVyL3ZlcnNpb25zL2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL3B1bmkuc2gvYXBpL3BsdWdpbnMvZG93bmxvYWQvMjQvQXV0b1JldGFpbmVyL3ZlcnNpb25zL2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtUZXN0aW5nIjpudWxsLCJMb2FkUmVxdWlyZWRTdGF0ZSI6MCwiTG9hZFN5bmMiOmZhbHNlLCJMb2FkUHJpb3JpdHkiOjAsIkNhblVubG9hZEFzeW5jIjpmYWxzZSwiU3VwcG9ydHNQcm9maWxlcyI6dHJ1ZSwiSW1hZ2VVcmxzIjpudWxsLCJJY29uVXJsIjoiaHR0cHM6Ly9zMy5wdW5pLnNoL21lZGlhL3BsdWdpbi8yNC9pY29uLWRyYjZheXdrNW05LnBuZyIsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjpudWxsfSx7IkRpc2FibGVkIjpmYWxzZSwiVGVzdGluZyI6ZmFsc2UsIlNjaGVkdWxlZEZvckRlbGV0aW9uIjpmYWxzZSwiSW5zdGFsbGVkRnJvbVVybCI6Imh0dHBzOi8vcmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbS9FdGVybml0YS1TL015RGFsYW11ZFBsdWdpbnMvbWFpbi9wbHVnaW5tYXN0ZXIuanNvbiIsIldvcmtpbmdQbHVnaW5JZCI6IjU5NzY1YzBjLTFiNmQtNGM2OC1hNTUxLTZhZmIyNTgwMjRmOCIsIklzVGhpcmRQYXJ0eSI6dHJ1ZSwiRWZmZWN0aXZlVmVyc2lvbiI6IjIuMS4wLjkiLCJFZmZlY3RpdmVBcGlMZXZlbCI6MTMsIkF1dGhvciI6Ik5pZ2h0bWFyZVhJViIsIk5hbWUiOiJBbnRpQWZrS2ljayIsIlB1bmNobGluZSI6IkEgRGFsYW11ZCBwbHVnaW4gZm9yIHByZXZlbnRpbmcgYmVpbmcgYXV0by1raWNrZWQgZnJvbSBGRlhJViBkdWUgdG8gaW5hY3Rpdml0eS4gSW5zdGFsbCBhbmQgZm9yZ2V0LCBubyBjb25maWcgbmVlZGVkLiIsIkRlc2NyaXB0aW9uIjoiQSBEYWxhbXVkIHBsdWdpbiBmb3IgcHJldmVudGluZyBiZWluZyBhdXRvLWtpY2tlZCBmcm9tIEZGWElWIGR1ZSB0byBpbmFjdGl2aXR5LiBJbnN0YWxsIGFuZCBmb3JnZXQsIG5vIGNvbmZpZyBuZWVkZWQuIiwiQ2hhbmdlbG9nIjoiLSA3LjMiLCJUYWdzIjpudWxsLCJDYXRlZ29yeVRhZ3MiOm51bGwsIklzSGlkZSI6ZmFsc2UsIkludGVybmFsTmFtZSI6IkFudGlBZmtLaWNrLURhbGFtdWQiLCJBc3NlbWJseVZlcnNpb24iOiIyLjEuMC45IiwiVGVzdGluZ0Fzc2VtYmx5VmVyc2lvbiI6bnVsbCwiSXNUZXN0aW5nRXhjbHVzaXZlIjpmYWxzZSwiUmVwb1VybCI6Imh0dHBzOi8vZ2l0aHViLmNvbS9OaWdodG1hcmVYSVYvQW50aUFma0tpY2siLCJBcHBsaWNhYmxlVmVyc2lvbiI6ImFueSIsIk1pbmltdW1EYWxhbXVkVmVyc2lvbiI6bnVsbCwiRGFsYW11ZEFwaUxldmVsIjoxMywiVGVzdGluZ0RhbGFtdWRBcGlMZXZlbCI6bnVsbCwiRG93bmxvYWRDb3VudCI6MTkzMDcwLCJMYXN0VXBkYXRlIjoxNzU0NTMxOTA2LCJEb3dubG9hZExpbmtJbnN0YWxsIjoiaHR0cHM6Ly9naXRodWIuY29tL05pZ2h0bWFyZVhJVi9BbnRpQWZrS2ljay9yZWxlYXNlcy9kb3dubG9hZC8yLjEuMC45L2xhdGVzdC56aXAiLCJEb3dubG9hZExpbmtVcGRhdGUiOiJodHRwczovL2dpdGh1Yi5jb20vTmlnaHRtYXJlWElWL0FudGlBZmtLaWNrL3JlbGVhc2VzL2Rvd25sb2FkLzIuMS4wLjkvbGF0ZXN0LnppcCIsIkRvd25sb2FkTGlua1Rlc3RpbmciOm51bGwsIkxvYWRSZXF1aXJlZFN0YXRlIjowLCJMb2FkU3luYyI6ZmFsc2UsIkxvYWRQcmlvcml0eSI6MCwiQ2FuVW5sb2FkQXN5bmMiOmZhbHNlLCJTdXBwb3J0c1Byb2ZpbGVzIjp0cnVlLCJJbWFnZVVybHMiOm51bGwsIkljb25VcmwiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vTmlnaHRtYXJlWElWL0FudGlBZmtLaWNrL21hc3Rlci9BbnRpQWZrS2ljay1EYWxhbXVkL2ltYWdlcy9pY29uLnBuZyIsIkFjY2VwdHNGZWVkYmFjayI6dHJ1ZSwiRmVlZGJhY2tNZXNzYWdlIjpudWxsLCJfRGlwMTdDaGFubmVsIjpudWxsfV0sIlBsdWdpblN0YXRlcyI6eyJGUFNQbHVnaW4iOiJMb2FkZWQiLCJBbnRpQWZrS2ljay1EYWxhbXVkIjoiTG9hZGVkIiwiQXV0b1JldGFpbmVyIjoiTG9hZGVkIiwiU29uYXJQbHVnaW4iOiJVbmxvYWRlZCIsIkVuZ2FnZVRpbWVyIjoiTG9hZGVkIiwiRkZMb2dzVmlld2VyIjoiTG9hZGVkIiwiWWVzQWxyZWFkeSI6IkxvYWRlZCIsIlRyaWFkQnVkZHkiOiJMb2FkZWQiLCJEYWxhbXVkLkZ1bGxzY3JlZW5DdXRzY2VuZXMiOiJMb2FkZWQiLCJCZXR0ZXJQYXJ0eUZpbmRlciI6IkxvYWRlZCIsIkdhdGhlckJ1ZGR5IjoiTG9hZGVkIiwiR2xvYmV0cm90dGVyIjoiTG9hZGVkIiwiSHVudEJ1ZGR5IjoiTG9hZGVkIiwiTWFya2V0Qm9hcmRQbHVnaW4iOiJMb2FkZWQifSwiRXZlclN0YXJ0ZWRMb2FkaW5nUGx1Z2lucyI6WyJGUFNQbHVnaW4iLCJBbnRpQWZrS2ljay1EYWxhbXVkIiwiWElWU3VibWFyaW5lc1Jld3JpdGUiLCJBdXRvUmV0YWluZXIiLCJFbmdhZ2VUaW1lciIsIkZGTG9nc1ZpZXdlciIsIlllc0FscmVhZHkiLCJUcmlhZEJ1ZGR5IiwiRGFsYW11ZC5GdWxsc2NyZWVuQ3V0c2NlbmVzIiwiQmV0dGVyUGFydHlGaW5kZXIiLCJHYXRoZXJCdWRkeSIsIkdsb2JldHJvdHRlciIsIkh1bnRCdWRkeSIsIk1hcmtldEJvYXJkUGx1Z2luIl0sIkRhbGFtdWRWZXJzaW9uIjoiMTMuMC4wLjQiLCJEYWxhbXVkR2l0SGFzaCI6ImZhNThkN2IzY2NlZjFjYWFkODljN2E1MWFjMmFiYjM1NTllNzgxYmIiLCJHYW1lVmVyc2lvbiI6IjIwMjUuMDkuMDQuMDAwMC4wMDAwIiwiTGFuZ3VhZ2UiOiJKYXBhbmVzZSIsIkRvRGFsYW11ZFRlc3QiOmZhbHNlLCJCZXRhS2V5IjpudWxsLCJEb1BsdWdpblRlc3QiOnRydWUsIkxvYWRBbGxBcGlMZXZlbHMiOmZhbHNlLCJJbnRlcmZhY2VMb2FkZWQiOnRydWUsIkZvcmNlZE1pbkhvb2siOmZhbHNlLCJUaGlyZFJlcG8iOltdLCJIYXNUaGlyZFJlcG8iOnRydWV9
2025-09-22 20:45:01.670 +09:00 [VRB] [FontAtlasFactory] [EngageTimer:RebuildFontsPrivateReal] 0x20113E76C00: Complete (at 657ms)
2025-09-22 20:45:01.672 +09:00 [VRB] [FontAtlasFactory] [EngageTimer] Building from BuildFontsAsync.
2025-09-22 20:45:01.672 +09:00 [VRB] [FontAtlasFactory] [EngageTimer:RebuildFontsPrivateReal] 0x2019BB66C10: PreBuild (at 0ms)
2025-09-22 20:45:01.677 +09:00 [VRB] [FontAtlasFactory] [EngageTimer] 0x2019BB66C10: AddFontFromImGuiHeapAllocatedMemory(0x201A69AC040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.685 +09:00 [VRB] [FontAtlasFactory] [EngageTimer] 0x2019BB66C10: AddFontFromImGuiHeapAllocatedMemory(0x201C551E040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:01.715 +09:00 [VRB] [INTERFACE] [IM] Disposing 4 textures
2025-09-22 20:45:01.894 +09:00 [VRB] [FontAtlasFactory] [TitleScreenMenuOverlay:RebuildFontsPrivateReal] 0x2012D25D210: Complete (at 694ms)
2025-09-22 20:45:01.923 +09:00 [VRB] [INTERFACE] [IM] Disposing 1 textures
2025-09-22 20:45:02.317 +09:00 [VRB] [FontAtlasFactory] [EngageTimer:RebuildFontsPrivateReal] 0x2019BB66C10: Complete (at 645ms)
2025-09-22 20:45:02.405 +09:00 [VRB] [FontAtlasFactory] [Market board:RebuildFontsPrivateReal] 0x20175B66A40: Complete (at 1392ms)
2025-09-22 20:45:02.408 +09:00 [VRB] [FontAtlasFactory] [Market board] Building from BuildFontsAsync.
2025-09-22 20:45:02.408 +09:00 [VRB] [FontAtlasFactory] [Market board:RebuildFontsPrivateReal] 0x20180948510: PreBuild (at 0ms)
2025-09-22 20:45:02.414 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20180948510: AddFontFromImGuiHeapAllocatedMemory(0x201A7A52040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:02.416 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20180948510: AddFontFromImGuiHeapAllocatedMemory(0x201A5EBC4D0, 0x500, ...) from AddFontFromStream(NNBSP)
2025-09-22 20:45:02.421 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20180948510: AddFontFromImGuiHeapAllocatedMemory(0x201A9277040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:02.428 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20180948510: AddFontFromImGuiHeapAllocatedMemory(0x201AA32E040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:02.436 +09:00 [VRB] [FontAtlasFactory] [Market board] 0x20180948510: AddFontFromImGuiHeapAllocatedMemory(0x201AB3D0040, 0x109AE5C, ...) from AddFontFromStream(Asset(NotoSansJpMedium))
2025-09-22 20:45:02.445 +09:00 [VRB] [INTERFACE] [IM] Disposing 7 textures
2025-09-22 20:45:03.752 +09:00 [VRB] [FontAtlasFactory] [Market board:RebuildFontsPrivateReal] 0x20180948510: Complete (at 1343ms)
2025-09-22 20:45:19.479 +09:00 [DBG] [YesAlready] [SelectOk] text=お使いのプレイ環境で最後にログアウトしたキャラクターが、現在接続中のデータセンターでは見つかりませんでした。データセンター選択画面から他のデータセンターに接続し直してご確認ください。
2025-09-22 20:45:19.481 +09:00 [DBG] [YesAlready] [SelectOk] Not proceeding
2025-09-22 20:45:22.867 +09:00 [DBG] [YesAlready] [SelectYesno] text=「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。
2025-09-22 20:45:22.878 +09:00 [VRB] [YesAlready] [SelectYesno] No match on へ入りますか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on から出ますか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on ハイクオリティ品がトレードされようとしています。 本当によろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on を カンパニー製作設備に納品します。 よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 下記のアイテムを修理しますか？  (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on ダークマターG6×99を、11,880で購入します。 よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 青燐水バレル×99を9,900カンパニークレジットで交換します。よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on を5個受け取ります。 よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 以下のアイテムを受け取ります。よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on Kansha ShimasuMandragoraのパーティに参加します。よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on ハイクオリティ品がトレードされようとしています。 本当によろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 選択したマテリアを合成します。よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on にテレポします。 よろしいですか？  (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 「蜃気楼の島 クレセントアイル：南征編」の 開始地点に戻ります。よろしいですか？ (「Mona Tierra」でログインします。 よろしいですか？  ※キャラクターの再編集はサブコマンドから実行できます。)
2025-09-22 20:45:22.879 +09:00 [DBG] [YesAlready] [SelectYesno] Not proceeding
2025-09-22 20:45:24.865 +09:00 [DBG] [YesAlready] [SelectOk] text=現在サーバーが混み合っています。順次ログイン処理を行っていますので、そのまましばらくお待ちください。（現在22人待ちです）
2025-09-22 20:45:24.866 +09:00 [DBG] [YesAlready] [SelectOk] Not proceeding
2025-09-22 20:45:56.977 +09:00 [VRB] [AddonEventManager] Adding Event. ParamKey: 0, Addon: _DTR, Event: MouseOver, GUID: 4e51e0a8-9738-43bd-8d6b-a54d294d50f9
2025-09-22 20:45:56.977 +09:00 [VRB] [AddonEventManager] Adding Event. ParamKey: 1, Addon: _DTR, Event: MouseOut, GUID: cdfe8b83-4179-4558-a1a1-87f9fd733f9e
2025-09-22 20:45:56.977 +09:00 [VRB] [AddonEventManager] Adding Event. ParamKey: 2, Addon: _DTR, Event: MouseClick, GUID: 9822887e-9d3f-46a0-8bac-5b39eb8b8c4a
2025-09-22 20:45:56.977 +09:00 [DBG] [DtrBar] Found last sibling: 2012BCD9A80
2025-09-22 20:45:56.977 +09:00 [DBG] [DtrBar] Set last sibling of DTR and updated child count
2025-09-22 20:45:56.977 +09:00 [DBG] [DtrBar] Updated node draw list
2025-09-22 20:45:56.977 +09:00 [VRB] [AddonEventManager] Adding Event. ParamKey: 3, Addon: _DTR, Event: MouseOver, GUID: 44b788eb-83cb-4b1b-804e-904050e8490a
2025-09-22 20:45:56.977 +09:00 [VRB] [AddonEventManager] Adding Event. ParamKey: 4, Addon: _DTR, Event: MouseOut, GUID: 28cc7c61-7621-4e81-9f14-a345252a3f88
2025-09-22 20:45:56.977 +09:00 [VRB] [AddonEventManager] Adding Event. ParamKey: 5, Addon: _DTR, Event: MouseClick, GUID: 11804b53-b61b-4d32-a352-9561f10e75ae
2025-09-22 20:45:56.977 +09:00 [DBG] [DtrBar] Found last sibling: 1BF80703F20
2025-09-22 20:45:56.977 +09:00 [DBG] [DtrBar] Set last sibling of DTR and updated child count
2025-09-22 20:45:56.977 +09:00 [DBG] [DtrBar] Updated node draw list
2025-09-22 20:45:58.726 +09:00 [DBG] [ClientState] TerritoryType changed: 339
2025-09-22 20:45:58.729 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <EnqueueWriteWhenPlayerAvailable>b__0_0@<>c
2025-09-22 20:45:58.790 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c], timeout=60000
2025-09-22 20:45:59.064 +09:00 [DBG] [ClientState] Is login
2025-09-22 20:45:59.067 +09:00 [DBG] [AutoRetainer] Registering ProperOnLogin Interactable event's framework update
2025-09-22 20:45:59.080 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c] completed successfully 
2025-09-22 20:46:02.619 +09:00 [DBG] [AutoRetainer] Firing ProperOnLogin Interactable event and unregistering framework update
2025-09-22 20:46:16.589 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758541575000 36175712400000
2025-09-22 20:46:41.426 +09:00 [DBG] [YesAlready] [SelectYesno] text=「ハウス」へ入りますか？
2025-09-22 20:46:41.426 +09:00 [VRB] [YesAlready] [SelectYesno] Matched on text へ入りますか？ (「ハウス」へ入りますか？)
2025-09-22 20:46:41.426 +09:00 [DBG] [YesAlready] [SelectYesno] Proceeding
2025-09-22 20:46:43.896 +09:00 [DBG] [ClientState] TerritoryType changed: 1250
2025-09-22 20:46:43.896 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <EnqueueWriteWhenPlayerAvailable>b__0_0@<>c
2025-09-22 20:46:43.902 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c], timeout=60000
2025-09-22 20:46:43.997 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c] completed successfully 
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 11
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2204923637416
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:46:47.369 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=4
2025-09-22 20:46:47.370 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '地下工房に移動する'
2025-09-22 20:46:47.371 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => 地下工房に移動する
2025-09-22 20:46:47.371 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '自分の個室に移動する'
2025-09-22 20:46:47.371 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] '個室番号を指定して移動（ハウスアピール確認）'
2025-09-22 20:46:47.371 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'やめる'
2025-09-22 20:46:47.372 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Workshop territory gate prevented UI fallback snapshot.
2025-09-22 20:46:47.374 +09:00 [DBG] [YesAlready] [SelectString] text=地下工房に移動する, 自分の個室に移動する, 個室番号を指定して移動（ハウスアピール確認）, やめる
2025-09-22 20:46:47.376 +09:00 [VRB] [YesAlready] [SelectString] No match on ミュタミクス (別室へ移動する)
2025-09-22 20:46:47.376 +09:00 [DBG] [YesAlready] [SelectString] Target restriction not met: 別室へ移動する does not match ミュタミクス
2025-09-22 20:46:47.376 +09:00 [DBG] [YesAlready] [SelectString] Not proceeding
2025-09-22 20:46:48.088 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 0
2025-09-22 20:46:49.381 +09:00 [DBG] [ClientState] TerritoryType changed: 423
2025-09-22 20:46:49.381 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <EnqueueWriteWhenPlayerAvailable>b__0_0@<>c
2025-09-22 20:46:49.387 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c], timeout=60000
2025-09-22 20:46:49.733 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c] completed successfully 
2025-09-22 20:46:52.073 +09:00 [DBG] [AutoRetainer] Entered voyage panel
2025-09-22 20:46:52.074 +09:00 [DBG] [AutoRetainer] <!> Enabled voyage scheduler
2025-09-22 20:46:52.089 +09:00 [VRB] [AutoRetainer] Starting ThreadLoadImageHandler
2025-09-22 20:46:52.092 +09:00 [VRB] [AutoRetainer] Loading image C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\AutoRetainer\4.5.2.1\res\submarine.png
2025-09-22 20:46:52.093 +09:00 [VRB] [WindowSystem] Saved preset for AutoRetainer Alert
2025-09-22 20:46:52.093 +09:00 [VRB] Config saved
2025-09-22 20:46:52.095 +09:00 [VRB] [AutoRetainer] Stopping ThreadLoadImageHandler, ticks=100
2025-09-22 20:46:52.095 +09:00 [DBG] [AutoRetainer] YesAlready locked
2025-09-22 20:46:52.095 +09:00 [DBG] [AutoRetainer] TextAdvance locked
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 10
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2203108543912
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=3
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '飛空艇の管理'
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '潜水艦の管理'
2025-09-22 20:46:52.173 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'キャンセル'
2025-09-22 20:46:52.177 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=飛空艇の管理
2025-09-22 20:46:52.177 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=潜水艦の管理
2025-09-22 20:46:52.177 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=キャンセル
2025-09-22 20:46:52.177 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] No UI candidates passed scoring thresholds.
2025-09-22 20:46:52.184 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectSubManagement@VoyageScheduler
2025-09-22 20:46:52.187 +09:00 [VRB] [AutoRetainer] Starting ThreadLoadImageHandler
2025-09-22 20:46:52.188 +09:00 [VRB] [AutoRetainer] Loading image C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\AutoRetainer\4.5.2.1\res\processing.png
2025-09-22 20:46:52.188 +09:00 [VRB] [AutoRetainer] Stopping ThreadLoadImageHandler, ticks=100
2025-09-22 20:46:52.188 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectSubManagement@VoyageScheduler], timeout=20000
2025-09-22 20:46:52.196 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:52.198 +09:00 [INF] [AutoRetainer] Initialized Callback module, FireCallback = 0x00007FF73FFE3EE0
2025-09-22 20:46:52.198 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:46:52.198 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectSubManagement@VoyageScheduler] completed successfully 
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207304158104
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　202樽'
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:46:52.535 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]  探索完了'
2025-09-22 20:46:52.536 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]  探索完了'
2025-09-22 20:46:52.536 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]  探索完了'
2025-09-22 20:46:52.536 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]  探索完了'
2025-09-22 20:46:52.536 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:46:52.543 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Dropped duplicate submarine candidate '||'.
2025-09-22 20:46:52.543 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Dropped duplicate submarine candidate '||'.
2025-09-22 20:46:52.543 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Dropped duplicate submarine candidate '||'.
2025-09-22 20:46:52.543 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:46:52.546 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Submarine-0'
2025-09-22 20:46:52.553 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskFinalizeVessel name=Bonfire, type=Submersible, quit=False
2025-09-22 20:46:52.553 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskSelectVesselByName (Bonfire)
2025-09-22 20:46:52.553 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskSelectVesselByName: Bonfire@<>c__DisplayClass0_0
2025-09-22 20:46:52.553 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:46:52.553 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task FinalizeVessel@VoyageScheduler
2025-09-22 20:46:52.553 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:46:52.554 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Bonfire, type=Submersible
2025-09-22 20:46:52.554 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:46:52.554 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__7_0@<>c
2025-09-22 20:46:52.554 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__1@<>c__DisplayClass7_0
2025-09-22 20:46:52.560 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskSelectVesselByName: Bonfire@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:46:52.562 +09:00 [DBG] [AutoRetainer] Selecting vessel Bonfire/Submersible/Bonfire  [Rank131]  探索完了/0
2025-09-22 20:46:52.562 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:52.562 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 0
2025-09-22 20:46:52.562 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskSelectVesselByName: Bonfire@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:46:52.565 +09:00 [DBG] [AutoRetainer] FPS restrictions removed
2025-09-22 20:46:52.570 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:46:52.857 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:46:52.865 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [FinalizeVessel@VoyageScheduler], timeout=20000
2025-09-22 20:46:52.866 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:52.984 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [FinalizeVessel@VoyageScheduler] completed successfully 
2025-09-22 20:46:52.989 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:46:53.999 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification 3E0ED97CC53E26417677EF4463C34F131A80ABDF73E00CA8E6F7B936814CE0E0 status=Completed arrival=2025/09/22 5:00:57.
2025-09-22 20:46:53.999 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification FB33E45FC9D5DC350B7AE08B7695DC4047B74B461B2DC301DB5B28E40BCE46D7 status=Completed arrival=2025/09/22 5:00:18.
2025-09-22 20:46:53.999 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification EFC051879814DC34F0B4449BEB79AA6A957CD3A36C91DB8FC68B4728C750B7C5 status=Completed arrival=2025/09/22 5:00:26.
2025-09-22 20:46:54.000 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification B4D1ED835F3B5D0A0D805018B2E4D95D14595487ED75384B4AF57FD127E0CD80 status=Completed arrival=2025/09/22 5:00:08.
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 14
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2204375892328
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'Bonfire [Rank131]
　探索機体数：3/4機
　保有燃料数：青燐水バレル　202樽'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=7
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'ボイジャー出港'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '前回のボイジャー報告'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'パーツの変更'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'パーツの修理'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'パーツの染色'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] '設定'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'やめる'
2025-09-22 20:46:56.742 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:46:56.747 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:46:56.753 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:46:56.756 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:46:56.756 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRepairAll
2025-09-22 20:46:56.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task RepairAllSelectRepair@<>c
2025-09-22 20:46:56.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 0@<>c__DisplayClass3_1
2025-09-22 20:46:56.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 1@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 2@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 3@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task CloseRepair@VoyageScheduler
2025-09-22 20:46:56.757 +09:00 [DBG] [AutoRetainer] Inserting stack with 18 tasks
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task CloseRepair@VoyageScheduler
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 3@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 2@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 1@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 0@<>c__DisplayClass3_1
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task RepairAllSelectRepair@<>c
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 0, index: 1, id: 24363, cond: 0, index: 2, id: 24352, cond: 0, index: 3, id: 24349, cond: 0
2025-09-22 20:46:56.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:46:56.762 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [RepairAllSelectRepair@<>c], timeout=20000
2025-09-22 20:46:56.762 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:56.762 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 3
2025-09-22 20:46:56.762 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [RepairAllSelectRepair@<>c] completed successfully 
2025-09-22 20:46:56.767 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 0@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:56.872 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:46:56.872 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 0 
2025-09-22 20:46:56.879 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:46:56.879 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 0@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:56.880 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.886 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:46:56.886 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.892 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.899 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.906 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.913 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.921 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.926 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.933 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.940 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.949 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 251) addon 1 by predicate
2025-09-22 20:46:56.949 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:46:57.066 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:46:57.072 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:57.107 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:57.114 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:46:57.254 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:46:57.260 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 1@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:57.260 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:46:57.260 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 1 
2025-09-22 20:46:57.267 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:46:57.267 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 1@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:57.267 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.274 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:46:57.274 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.282 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.287 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.294 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.302 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.308 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.317 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.322 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.329 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.336 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 245) addon 1 by predicate
2025-09-22 20:46:57.336 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:46:57.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:46:57.461 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:57.774 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:57.781 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:46:57.921 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:46:57.926 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 2@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:57.926 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:46:57.926 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 2 
2025-09-22 20:46:57.933 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:46:57.934 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 2@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:57.934 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.941 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:46:57.941 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.949 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.954 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.961 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.968 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.975 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.983 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.989 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:57.996 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:58.003 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 239) addon 1 by predicate
2025-09-22 20:46:58.003 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:46:58.123 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:46:58.128 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:58.128 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:58.135 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:46:58.274 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:46:58.282 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 3@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:58.282 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:46:58.283 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 3 
2025-09-22 20:46:58.288 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:46:58.288 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 3@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:58.288 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.295 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:46:58.295 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.302 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.308 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.317 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.322 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.329 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.336 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.343 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.352 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.357 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 233) addon 1 by predicate
2025-09-22 20:46:58.357 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:46:58.475 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:46:58.483 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:46:58.483 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:46:58.489 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:46:58.628 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:46:58.635 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [CloseRepair@VoyageScheduler], timeout=20000
2025-09-22 20:46:58.636 +09:00 [DBG] [AutoRetainer] [Voyage] Closing repair window (CompanyCraftSupply)
2025-09-22 20:46:58.636 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:58.636 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [CloseRepair@VoyageScheduler] completed successfully 
2025-09-22 20:46:58.643 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__7_0@<>c], timeout=20000
2025-09-22 20:46:58.644 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__7_0@<>c] completed successfully 
2025-09-22 20:46:58.650 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__1@<>c__DisplayClass7_0], timeout=20000
2025-09-22 20:46:58.652 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployVessel name=Bonfire, type=Submersible
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Bonfire, type=Submersible
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployPreviousLog
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Bonfire, type=Submersible
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task RedeployVessel@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task DeployVessel@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForCutscene@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] Inserting stack with 11 tasks
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForCutscene@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task DeployVessel@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task RedeployVessel@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForSelectStringAddon@<>c
2025-09-22 20:46:58.653 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__1@<>c__DisplayClass7_0] completed successfully 
2025-09-22 20:46:58.657 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 14
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207585146200
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'Bonfire [Rank131]
　探索機体数：3/4機
　保有燃料数：青燐水バレル　202樽'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=7
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'ボイジャー出港'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => ボイジャー出港
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '前回のボイジャー報告'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'パーツの変更'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'パーツの修理'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'パーツの染色'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] '設定'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'やめる'
2025-09-22 20:46:58.659 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:46:58.664 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:46:58.670 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:46:58.670 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 30000, index: 1, id: 24363, cond: 30000, index: 2, id: 24352, cond: 30000, index: 3, id: 24349, cond: 30000
2025-09-22 20:46:58.670 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:46:58.678 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:46:58.678 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 30000, index: 1, id: 24363, cond: 30000, index: 2, id: 24352, cond: 30000, index: 3, id: 24349, cond: 30000
2025-09-22 20:46:58.678 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:46:58.683 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewPreviousLog@VoyageScheduler], timeout=20000
2025-09-22 20:46:58.711 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:58.712 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:46:58.712 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewPreviousLog@VoyageScheduler] completed successfully 
2025-09-22 20:46:58.720 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:46:58.739 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:46:58.746 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [RedeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:46:58.787 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:58.794 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [RedeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:46:58.803 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:46:58.871 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:46:58.879 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [CheckForFuel@TaskRedeployPreviousLog], timeout=20000
2025-09-22 20:46:58.880 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [CheckForFuel@TaskRedeployPreviousLog] completed successfully 
2025-09-22 20:46:58.885 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [DeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:46:58.885 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationDetail, valueCount = 1, updateStatte = True, values:

2025-09-22 20:46:58.885 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [DeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:46:58.892 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:46:59.219 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:46:59.227 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForNoCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:46:59.996 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:2:2fd6d5c9-34ce-5b2a-afc3-5edecb7016bb duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:46:59.997 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:3:301ba0c4-e980-44ae-b2f8-e0090c9a38ca duplicate arrival detected; skipping (arrival=2025/09/22 5:00:18).
2025-09-22 20:46:59.997 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:4:d55e4e7d-b1bc-0488-19ff-bf5d2699649e duplicate arrival detected; skipping (arrival=2025/09/22 5:00:26).
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 9
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2199574020392
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'このカットシーンをスキップしますか？'
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=2
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'はい'
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'いいえ'
2025-09-22 20:47:01.005 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:01.011 +09:00 [DBG] [AutoRetainer] Selecting cutscene skipping
2025-09-22 20:47:01.011 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:01.011 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 0
2025-09-22 20:47:01.215 +09:00 [VRB] [INTERFACE] [IM] Disposing 2 textures
2025-09-22 20:47:02.170 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForNoCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:02.170 +09:00 [DBG] [AutoRetainer] FPS restrictions restored
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207323631848
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　175樽'
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]  探索完了'
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]  探索完了'
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]  探索完了'
2025-09-22 20:47:02.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:47:02.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Dropped duplicate submarine candidate '||'.
2025-09-22 20:47:02.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Dropped duplicate submarine candidate '||'.
2025-09-22 20:47:02.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:47:02.201 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Bonfire'
2025-09-22 20:47:02.201 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Submarine-1'
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskFinalizeVessel name=Pilvi, type=Submersible, quit=False
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskSelectVesselByName (Pilvi)
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskSelectVesselByName: Pilvi@<>c__DisplayClass0_0
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task FinalizeVessel@VoyageScheduler
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Pilvi, type=Submersible
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__7_0@<>c
2025-09-22 20:47:02.240 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__1@<>c__DisplayClass7_0
2025-09-22 20:47:02.246 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskSelectVesselByName: Pilvi@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:02.246 +09:00 [DBG] [AutoRetainer] Selecting vessel Pilvi/Submersible/Pilvi  [Rank131]  探索完了/2
2025-09-22 20:47:02.246 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:02.246 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 2
2025-09-22 20:47:02.246 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskSelectVesselByName: Pilvi@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:02.246 +09:00 [DBG] [AutoRetainer] FPS restrictions removed
2025-09-22 20:47:02.253 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:47:03.220 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:47:03.225 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [FinalizeVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:03.225 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:03.343 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [FinalizeVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:03.352 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:47:04.004 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:2:2fd6d5c9-34ce-5b2a-afc3-5edecb7016bb duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:47:04.004 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification 7FAF018B6A4695FA9F2ADD5D9953CB459F1348820C2D1F5CF4BDE636B9EEB9EB status=Completed arrival=2025/09/22 5:00:18.
2025-09-22 20:47:04.004 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:4:d55e4e7d-b1bc-0488-19ff-bf5d2699649e duplicate arrival detected; skipping (arrival=2025/09/22 5:00:26).
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 14
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2206106617096
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'Pilvi [Rank131]
　探索機体数：3/4機
　保有燃料数：青燐水バレル　175樽'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=7
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'ボイジャー出港'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '前回のボイジャー報告'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'パーツの変更'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'パーツの修理'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'パーツの染色'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] '設定'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'やめる'
2025-09-22 20:47:07.012 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:07.017 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:47:07.024 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:07.024 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 18877, index: 1, id: 24363, cond: 18877, index: 2, id: 24352, cond: 18877, index: 3, id: 24349, cond: 18877
2025-09-22 20:47:07.024 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:07.032 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__7_0@<>c], timeout=20000
2025-09-22 20:47:07.032 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__7_0@<>c] completed successfully 
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__1@<>c__DisplayClass7_0], timeout=20000
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployVessel name=Pilvi, type=Submersible
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Pilvi, type=Submersible
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployPreviousLog
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Pilvi, type=Submersible
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task RedeployVessel@VoyageScheduler
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task DeployVessel@VoyageScheduler
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForCutscene@VoyageScheduler
2025-09-22 20:47:07.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] Inserting stack with 11 tasks
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForCutscene@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task DeployVessel@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task RedeployVessel@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForSelectStringAddon@<>c
2025-09-22 20:47:07.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__1@<>c__DisplayClass7_0] completed successfully 
2025-09-22 20:47:07.044 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:47:07.045 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:47:07.051 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:07.051 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 18877, index: 1, id: 24363, cond: 18877, index: 2, id: 24352, cond: 18877, index: 3, id: 24349, cond: 18877
2025-09-22 20:47:07.051 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:07.060 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:07.060 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 18877, index: 1, id: 24363, cond: 18877, index: 2, id: 24352, cond: 18877, index: 3, id: 24349, cond: 18877
2025-09-22 20:47:07.060 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:07.065 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewPreviousLog@VoyageScheduler], timeout=20000
2025-09-22 20:47:07.065 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:07.065 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:47:07.065 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewPreviousLog@VoyageScheduler] completed successfully 
2025-09-22 20:47:07.072 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:47:07.095 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:47:07.100 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [RedeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:07.141 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:07.149 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [RedeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:07.157 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:07.227 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:07.232 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [CheckForFuel@TaskRedeployPreviousLog], timeout=20000
2025-09-22 20:47:07.232 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [CheckForFuel@TaskRedeployPreviousLog] completed successfully 
2025-09-22 20:47:07.239 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [DeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:07.239 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationDetail, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:07.239 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [DeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:07.246 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:47:07.398 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:07.405 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForNoCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:47:08.011 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:2:2fd6d5c9-34ce-5b2a-afc3-5edecb7016bb duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:47:08.011 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:4:d55e4e7d-b1bc-0488-19ff-bf5d2699649e duplicate arrival detected; skipping (arrival=2025/09/22 5:00:26).
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 9
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207583895624
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'このカットシーンをスキップしますか？'
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=2
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'はい'
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => はい
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'いいえ'
2025-09-22 20:47:09.179 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:09.184 +09:00 [DBG] [AutoRetainer] Selecting cutscene skipping
2025-09-22 20:47:09.184 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:09.184 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 0
2025-09-22 20:47:10.385 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForNoCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:10.385 +09:00 [DBG] [AutoRetainer] FPS restrictions restored
2025-09-22 20:47:10.414 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:10.414 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:47:10.414 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205131051224
2025-09-22 20:47:10.414 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　148樽'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]  探索完了'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]  探索完了'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Dropped duplicate submarine candidate '||'.
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Bonfire'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Pilvi'
2025-09-22 20:47:10.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Submarine-1'
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskFinalizeVessel name=Kukka, type=Submersible, quit=False
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskSelectVesselByName (Kukka)
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskSelectVesselByName: Kukka@<>c__DisplayClass0_0
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task FinalizeVessel@VoyageScheduler
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Kukka, type=Submersible
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__7_0@<>c
2025-09-22 20:47:10.456 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__1@<>c__DisplayClass7_0
2025-09-22 20:47:10.461 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskSelectVesselByName: Kukka@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:10.461 +09:00 [DBG] [AutoRetainer] Selecting vessel Kukka/Submersible/Kukka  [Rank131]  探索完了/3
2025-09-22 20:47:10.461 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:10.461 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 3
2025-09-22 20:47:10.461 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskSelectVesselByName: Kukka@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:10.461 +09:00 [DBG] [AutoRetainer] FPS restrictions removed
2025-09-22 20:47:10.470 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:47:11.420 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:47:11.426 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [FinalizeVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:11.426 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:11.544 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [FinalizeVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:11.551 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:47:12.018 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:2:2fd6d5c9-34ce-5b2a-afc3-5edecb7016bb duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:47:12.018 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification 0941A7BCCB57AAE59445E9D35530499CDC2D29F638B899BD3C5B306E7F10AE86 status=Completed arrival=2025/09/22 5:00:26.
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 14
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205178332504
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'Kukka [Rank131]
　探索機体数：3/4機
　保有燃料数：青燐水バレル　148樽'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=7
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'ボイジャー出港'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '前回のボイジャー報告'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'パーツの変更'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'パーツの修理'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'パーツの染色'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] '設定'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'やめる'
2025-09-22 20:47:15.324 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:15.329 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRepairAll
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task RepairAllSelectRepair@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 0@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 1@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 2@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Repair 3@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task CloseRepair@VoyageScheduler
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] Inserting stack with 18 tasks
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task CloseRepair@VoyageScheduler
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 3@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 2@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 1@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (20 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilRepairComplete@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForYesNoDisappear@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Repair 0@<>c__DisplayClass3_1
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task RepairAllSelectRepair@<>c
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 0, index: 1, id: 24363, cond: 0, index: 2, id: 24352, cond: 0, index: 3, id: 24349, cond: 0
2025-09-22 20:47:15.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:15.343 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [RepairAllSelectRepair@<>c], timeout=20000
2025-09-22 20:47:15.343 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:15.343 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 3
2025-09-22 20:47:15.343 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [RepairAllSelectRepair@<>c] completed successfully 
2025-09-22 20:47:15.350 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 0@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:15.440 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:47:15.440 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 0 
2025-09-22 20:47:15.447 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:47:15.447 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 0@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:15.447 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.454 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:47:15.454 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.461 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.470 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.475 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.482 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.489 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.496 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.504 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.509 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.517 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦体
消費:魔導機械修理材×6(所持数 227) addon 1 by predicate
2025-09-22 20:47:15.517 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:47:15.636 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:47:15.641 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:15.648 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:15.656 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:15.795 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:15.803 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 1@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:15.803 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:47:15.803 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 1 
2025-09-22 20:47:15.808 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:47:15.808 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 1@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:15.808 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.815 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:47:15.815 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.822 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.829 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.838 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.843 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.850 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.857 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.865 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.871 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.878 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シーラカンス改級艦尾
消費:魔導機械修理材×6(所持数 221) addon 1 by predicate
2025-09-22 20:47:15.878 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:47:15.996 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:47:16.004 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:16.004 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:16.010 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:16.148 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:16.155 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 2@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:16.156 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:47:16.156 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 2 
2025-09-22 20:47:16.162 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:47:16.163 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 2@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:16.163 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.171 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:47:16.172 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.176 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.183 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.190 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.198 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.204 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.211 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.218 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.225 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.233 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
ウンキウ改級艦首
消費:魔導機械修理材×6(所持数 215) addon 1 by predicate
2025-09-22 20:47:16.233 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:47:16.350 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:47:16.357 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:16.357 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:16.365 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:16.504 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:16.509 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Repair 3@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:16.509 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 6, updateStatte = True, values:

2025-09-22 20:47:16.510 +09:00 [DBG] [AutoRetainer] [Voyage] Executing CompanyCraftSupply repair request on slot 3 
2025-09-22 20:47:16.516 +09:00 [DBG] [AutoRetainer] [Voyage] Found yesno, repair request success
2025-09-22 20:47:16.516 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Repair 3@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:16.516 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.524 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesNoDisappear@<>c], timeout=5000
2025-09-22 20:47:16.524 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.532 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.537 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.544 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.551 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.558 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.567 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.572 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.579 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.586 +09:00 [VRB] [AutoRetainer] SelectYesno 下記のアイテムを修理しますか？
シャーク改級艦橋
消費:魔導機械修理材×6(所持数 209) addon 1 by predicate
2025-09-22 20:47:16.586 +09:00 [DBG] [AutoRetainer] [Voyage] Confirming repair
2025-09-22 20:47:16.704 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesNoDisappear@<>c] completed successfully 
2025-09-22 20:47:16.711 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilRepairComplete@<>c__DisplayClass3_1], timeout=20000
2025-09-22 20:47:16.711 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilRepairComplete@<>c__DisplayClass3_1] completed successfully 
2025-09-22 20:47:16.718 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (20 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:16.857 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (20 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:16.865 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [CloseRepair@VoyageScheduler], timeout=20000
2025-09-22 20:47:16.865 +09:00 [DBG] [AutoRetainer] [Voyage] Closing repair window (CompanyCraftSupply)
2025-09-22 20:47:16.865 +09:00 [VRB] [AutoRetainer] Firing callback: CompanyCraftSupply, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:16.865 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [CloseRepair@VoyageScheduler] completed successfully 
2025-09-22 20:47:16.871 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__7_0@<>c], timeout=20000
2025-09-22 20:47:16.871 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__7_0@<>c] completed successfully 
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__1@<>c__DisplayClass7_0], timeout=20000
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployVessel name=Kukka, type=Submersible
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Kukka, type=Submersible
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployPreviousLog
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Kukka, type=Submersible
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task RedeployVessel@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task DeployVessel@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForCutscene@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] Inserting stack with 11 tasks
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForCutscene@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task DeployVessel@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task RedeployVessel@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:16.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForSelectStringAddon@<>c
2025-09-22 20:47:16.878 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__1@<>c__DisplayClass7_0] completed successfully 
2025-09-22 20:47:16.885 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 14
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205178343384
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'Kukka [Rank131]
　探索機体数：3/4機
　保有燃料数：青燐水バレル　148樽'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=7
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'ボイジャー出港'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '前回のボイジャー報告'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'パーツの変更'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'パーツの修理'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'パーツの染色'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] '設定'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'やめる'
2025-09-22 20:47:16.887 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:16.892 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:47:16.900 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:16.900 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 30000, index: 1, id: 24363, cond: 30000, index: 2, id: 24352, cond: 30000, index: 3, id: 24349, cond: 30000
2025-09-22 20:47:16.900 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:16.905 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:16.905 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 30000, index: 1, id: 24363, cond: 30000, index: 2, id: 24352, cond: 30000, index: 3, id: 24349, cond: 30000
2025-09-22 20:47:16.905 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:16.912 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewPreviousLog@VoyageScheduler], timeout=20000
2025-09-22 20:47:16.942 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:16.942 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:47:16.942 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewPreviousLog@VoyageScheduler] completed successfully 
2025-09-22 20:47:16.947 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:47:16.969 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:47:16.975 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [RedeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:17.017 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:17.024 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [RedeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:17.032 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:17.101 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:17.107 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [CheckForFuel@TaskRedeployPreviousLog], timeout=20000
2025-09-22 20:47:17.107 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [CheckForFuel@TaskRedeployPreviousLog] completed successfully 
2025-09-22 20:47:17.114 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [DeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:17.114 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationDetail, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:17.114 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [DeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:17.121 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:47:17.399 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:17.407 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForNoCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:47:18.038 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:2:2fd6d5c9-34ce-5b2a-afc3-5edecb7016bb duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 9
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207574407800
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'このカットシーンをスキップしますか？'
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=2
2025-09-22 20:47:19.185 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'はい'
2025-09-22 20:47:19.186 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => はい
2025-09-22 20:47:19.186 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'いいえ'
2025-09-22 20:47:19.186 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:19.192 +09:00 [DBG] [AutoRetainer] Selecting cutscene skipping
2025-09-22 20:47:19.192 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:19.192 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 0
2025-09-22 20:47:20.407 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForNoCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:20.407 +09:00 [DBG] [AutoRetainer] FPS restrictions restored
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207304241384
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　121樽'
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]  探索完了'
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:47:20.436 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:47:20.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Bonfire'
2025-09-22 20:47:20.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Pilvi'
2025-09-22 20:47:20.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Kukka'
2025-09-22 20:47:20.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Submarine-1'
2025-09-22 20:47:20.478 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskQuitMenu
2025-09-22 20:47:20.479 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuitVesselSelectorMenu@VoyageScheduler
2025-09-22 20:47:20.482 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuitVesselSelectorMenu@VoyageScheduler], timeout=20000
2025-09-22 20:47:20.483 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:20.483 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 4
2025-09-22 20:47:20.483 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuitVesselSelectorMenu@VoyageScheduler] completed successfully 
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 10
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205183499784
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=3
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '飛空艇の管理'
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '潜水艦の管理'
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'キャンセル'
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=飛空艇の管理
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=潜水艦の管理
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=キャンセル
2025-09-22 20:47:21.415 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] No UI candidates passed scoring thresholds.
2025-09-22 20:47:21.421 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectExitMainPanel@VoyageScheduler
2025-09-22 20:47:21.426 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectExitMainPanel@VoyageScheduler], timeout=20000
2025-09-22 20:47:21.427 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:21.427 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 2
2025-09-22 20:47:21.427 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectExitMainPanel@VoyageScheduler] completed successfully 
2025-09-22 20:47:21.732 +09:00 [DBG] [AutoRetainer] <!> Exited voyage panel, disabled voyage scheduler
2025-09-22 20:47:21.741 +09:00 [DBG] [AutoRetainer] YesAlready unlocked
2025-09-22 20:47:21.741 +09:00 [DBG] [AutoRetainer] TextAdvance unlocked
2025-09-22 20:47:22.038 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:2:2fd6d5c9-34ce-5b2a-afc3-5edecb7016bb duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:47:25.407 +09:00 [DBG] [AutoRetainer] Entered voyage panel
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 10
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205183489384
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=3
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '飛空艇の管理'
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '潜水艦の管理'
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'キャンセル'
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=飛空艇の管理
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=潜水艦の管理
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=キャンセル
2025-09-22 20:47:25.513 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] No UI candidates passed scoring thresholds.
2025-09-22 20:47:25.513 +09:00 [DBG] [YesAlready] [SelectString] text=飛空艇の管理, 潜水艦の管理, キャンセル
2025-09-22 20:47:25.513 +09:00 [VRB] [YesAlready] [SelectString] No match on ミュタミクス (管制卓)
2025-09-22 20:47:25.513 +09:00 [DBG] [YesAlready] [SelectString] Target restriction not met: 管制卓 does not match ミュタミクス
2025-09-22 20:47:25.513 +09:00 [DBG] [YesAlready] [SelectString] Not proceeding
2025-09-22 20:47:26.345 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2206702713624
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　121樽'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]  探索完了'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Bonfire'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Pilvi'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Kukka'
2025-09-22 20:47:26.451 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Submarine-1'
2025-09-22 20:47:26.452 +09:00 [DBG] [YesAlready] [SelectString] text=Bonfire  [Rank131]   [探索中:残り時間 1日16時間28分], Siipi  [Rank131]  探索完了, Pilvi  [Rank131]   [探索中:残り時間 1日16時間28分], Kukka  [Rank131]   [探索中:残り時間 1日16時間29分], やめる
2025-09-22 20:47:26.452 +09:00 [VRB] [YesAlready] [SelectString] No match on ミュタミクス (管制卓)
2025-09-22 20:47:26.452 +09:00 [DBG] [YesAlready] [SelectString] Target restriction not met: 管制卓 does not match ミュタミクス
2025-09-22 20:47:26.452 +09:00 [DBG] [YesAlready] [SelectString] Not proceeding
2025-09-22 20:47:28.434 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: -1
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 10
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205183495416
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=3
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '飛空艇の管理'
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '潜水艦の管理'
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'キャンセル'
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=飛空艇の管理
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=潜水艦の管理
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=キャンセル
2025-09-22 20:47:28.505 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] No UI candidates passed scoring thresholds.
2025-09-22 20:47:28.505 +09:00 [DBG] [YesAlready] [SelectString] text=飛空艇の管理, 潜水艦の管理, キャンセル
2025-09-22 20:47:28.505 +09:00 [VRB] [YesAlready] [SelectString] No match on ミュタミクス (管制卓)
2025-09-22 20:47:28.505 +09:00 [DBG] [YesAlready] [SelectString] Target restriction not met: 管制卓 does not match ミュタミクス
2025-09-22 20:47:28.505 +09:00 [DBG] [YesAlready] [SelectString] Not proceeding
2025-09-22 20:47:29.601 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: -1
2025-09-22 20:47:29.648 +09:00 [DBG] [AutoRetainer] <!> Exited voyage panel, disabled voyage scheduler
2025-09-22 20:47:32.156 +09:00 [INF] [PluginManager] Now reloading all PluginMasters...
2025-09-22 20:47:32.156 +09:00 [INF] [PLUGINR] Fetching repo: https://kamori.goats.dev/Plugin/PluginMaster
2025-09-22 20:47:32.157 +09:00 [VRB] [PluginManager] Scanning dev plugins at C:\Users\MonaT\AppData\Roaming\XIVLauncher\devPlugins\XIVSubmarinesRewrite\XIVSubmarinesRewrite.dll
2025-09-22 20:47:32.166 +09:00 [VRB] [WindowSystem] Saved preset for プラグインインストーラ (テスト中)###XlPluginInstaller
2025-09-22 20:47:32.166 +09:00 [VRB] WindowSystem "DalamudCore" Window "プラグインインストーラ (テスト中)###XlPluginInstaller" has focus now
2025-09-22 20:47:32.167 +09:00 [VRB] Config saved
2025-09-22 20:47:32.284 +09:00 [VRB] [HTTP] Established connection to kamori.goats.dev at 104.21.85.184:443
2025-09-22 20:47:33.408 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://kamori.goats.dev/Plugin/PluginMaster
2025-09-22 20:47:33.408 +09:00 [INF] [PLUGINR] Fetching repo: https://raw.githubusercontent.com/Eternita-S/MyDalamudPlugins/main/pluginmaster.json
2025-09-22 20:47:33.408 +09:00 [INF] [PLUGINR] Fetching repo: https://github.com/Aireil/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:47:33.408 +09:00 [INF] [PLUGINR] Fetching repo: https://github.com/daemitus/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:47:33.408 +09:00 [INF] [PLUGINR] Fetching repo: https://love.puni.sh/ment.json
2025-09-22 20:47:33.417 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:33.438 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://raw.githubusercontent.com/Eternita-S/MyDalamudPlugins/main/pluginmaster.json
2025-09-22 20:47:33.461 +09:00 [VRB] [HTTP] Established connection to github.com at 20.27.177.113:443
2025-09-22 20:47:33.524 +09:00 [VRB] [HTTP] Established connection to github.com at 20.27.177.113:443
2025-09-22 20:47:33.529 +09:00 [VRB] [HTTP] Established connection to love.puni.sh at [2606:4700:3031::6815:3264]:443
2025-09-22 20:47:33.650 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://github.com/daemitus/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:47:33.722 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://github.com/Aireil/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 20:47:34.126 +09:00 [VRB] [HTTP] Established connection to puni.sh at [2606:4700:3035::ac43:ccb6]:443
2025-09-22 20:47:34.572 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://love.puni.sh/ment.json
2025-09-22 20:47:34.572 +09:00 [INF] [PluginManager] PluginMasters reloaded, now refiltering...
2025-09-22 20:47:34.625 +09:00 [DBG] [PluginManager] Starting plugin update check...
2025-09-22 20:47:34.625 +09:00 [DBG] [PluginManager] Update check found 1 available updates.
2025-09-22 20:47:34.627 +09:00 [WRN] [HITCH] Long FrameworkUpdate detected, 54.4071ms > 50ms - check in the plugin stats window.
2025-09-22 20:47:34.636 +09:00 [VRB] Downloading icon for Accountant from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/Accountant/images/icon.png
2025-09-22 20:47:34.636 +09:00 [VRB] Downloading icon for ARealmRecorded from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/testing-live/ARealmRecorded/images/icon.png
2025-09-22 20:47:34.636 +09:00 [VRB] Downloading icon for AbilityAnts from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/AbilityAnts/images/icon.png
2025-09-22 20:47:34.636 +09:00 [VRB] Downloading icon for AcquisitionDate from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/testing-live/AcquisitionDate/images/icon.png
2025-09-22 20:47:34.636 +09:00 [VRB] Downloading icon for ActionTimeline from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/ActionTimeline/images/icon.png
2025-09-22 20:47:34.637 +09:00 [VRB] Downloading icon for AdaptiveHud from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/AdaptiveHud/images/icon.png
2025-09-22 20:47:34.637 +09:00 [VRB] Downloading icon for AdventurerInNeed from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/AdventurerInNeed/images/icon.png
2025-09-22 20:47:34.637 +09:00 [VRB] Downloading icon for AetherBreakout from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/testing-live/AetherBreakout/images/icon.png
2025-09-22 20:47:34.642 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.643 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.643 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.643 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.644 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.644 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.655 +09:00 [VRB] Plugin icon for AetherBreakout loaded
2025-09-22 20:47:34.668 +09:00 [VRB] Plugin icon for AdaptiveHud loaded
2025-09-22 20:47:34.668 +09:00 [VRB] Plugin icon for AbilityAnts loaded
2025-09-22 20:47:34.668 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8003::154]:443
2025-09-22 20:47:34.684 +09:00 [VRB] Plugin icon for ARealmRecorded loaded
2025-09-22 20:47:34.685 +09:00 [VRB] Plugin icon for AdventurerInNeed loaded
2025-09-22 20:47:34.685 +09:00 [VRB] Plugin icon for Accountant loaded
2025-09-22 20:47:34.687 +09:00 [VRB] Plugin icon for ActionTimeline loaded
2025-09-22 20:47:34.690 +09:00 [VRB] Plugin icon for AcquisitionDate loaded
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for AntiAfkKick-Dalamud from https://raw.githubusercontent.com/NightmareXIV/AntiAfkKick/master/AntiAfkKick-Dalamud/images/icon.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for AutoRetainer from https://s3.puni.sh/media/plugin/24/icon-drb6aywk5m9.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for BetterPartyFinder from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/testing-live/BetterPartyFinder/images/icon.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for EngageTimer from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/EngageTimer/images/icon.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for FFLogsViewer from https://github.com/Aireil/FFLogsViewer/raw/master/res/icon.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for FPSPlugin from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/FPSPlugin/images/icon.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for GatherBuddy from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/GatherBuddy/images/icon.png
2025-09-22 20:47:35.127 +09:00 [VRB] Downloading icon for Globetrotter from https://raw.githubusercontent.com/goatcorp/PluginDistD17/main/stable/Globetrotter/images/icon.png
2025-09-22 20:47:35.135 +09:00 [VRB] Plugin icon for EngageTimer loaded
2025-09-22 20:47:35.138 +09:00 [VRB] Plugin icon for Globetrotter loaded
2025-09-22 20:47:35.139 +09:00 [VRB] Plugin icon for AntiAfkKick-Dalamud loaded
2025-09-22 20:47:35.255 +09:00 [VRB] [HTTP] Established connection to s3.puni.sh at [2606:4700:3035::ac43:ccb6]:443
2025-09-22 20:47:35.307 +09:00 [VRB] Plugin icon for GatherBuddy loaded
2025-09-22 20:47:35.325 +09:00 [VRB] Plugin icon for BetterPartyFinder loaded
2025-09-22 20:47:35.362 +09:00 [VRB] Plugin icon for FPSPlugin loaded
2025-09-22 20:47:35.376 +09:00 [VRB] Plugin icon for FFLogsViewer loaded
2025-09-22 20:47:36.203 +09:00 [VRB] Images for AutoRetainer are not available
2025-09-22 20:47:36.299 +09:00 [VRB] Plugin icon for AutoRetainer loaded
2025-09-22 20:47:38.434 +09:00 [VRB] [DataShare] Created new data for [ECommonsPatreonBannerRandomColor] for creator AutoRetainer.
2025-09-22 20:47:38.472 +09:00 [VRB] [AutoRetainer] Starting ThreadLoadImageHandler
2025-09-22 20:47:38.473 +09:00 [VRB] [AutoRetainer] Loading icon 62123, hq=True
2025-09-22 20:47:38.473 +09:00 [VRB] [AutoRetainer] Stopping ThreadLoadImageHandler, ticks=100
2025-09-22 20:47:38.480 +09:00 [VRB] [AutoRetainer] Starting ThreadLoadImageHandler
2025-09-22 20:47:38.481 +09:00 [VRB] [AutoRetainer] Loading icon 62116, hq=True
2025-09-22 20:47:38.481 +09:00 [VRB] [AutoRetainer] Loading icon 62117, hq=True
2025-09-22 20:47:38.482 +09:00 [VRB] [AutoRetainer] Stopping ThreadLoadImageHandler, ticks=100
2025-09-22 20:47:38.489 +09:00 [VRB] [WindowSystem] Saved preset for AutoRetainer 4.5.2.1 | Session expires in 2 days 23 hours###AutoRetainer
2025-09-22 20:47:38.489 +09:00 [VRB] WindowSystem "" Window "AutoRetainer 4.5.2.1 | Session expires in 2 days 23 hours###AutoRetainer" has focus now
2025-09-22 20:47:38.490 +09:00 [VRB] Config saved
2025-09-22 20:47:38.494 +09:00 [VRB] WindowSystem "DalamudCore" Window "プラグインインストーラ (テスト中)###XlPluginInstaller" lost focus
2025-09-22 20:47:42.333 +09:00 [VRB] [INTERFACE] [IM] Disposing 3 textures
2025-09-22 20:47:43.197 +09:00 [VRB] WindowSystem "" Window "AutoRetainer 4.5.2.1 | Session expires in 2 days 23 hours###AutoRetainer" lost focus
2025-09-22 20:47:43.288 +09:00 [DBG] [AutoRetainer] Entered voyage panel
2025-09-22 20:47:43.288 +09:00 [DBG] [AutoRetainer] <!> Enabled voyage scheduler
2025-09-22 20:47:43.296 +09:00 [DBG] [AutoRetainer] YesAlready locked
2025-09-22 20:47:43.296 +09:00 [DBG] [AutoRetainer] TextAdvance locked
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 10
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207588829640
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=3
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '飛空艇の管理'
2025-09-22 20:47:43.437 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => 飛空艇の管理
2025-09-22 20:47:43.438 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '潜水艦の管理'
2025-09-22 20:47:43.438 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'キャンセル'
2025-09-22 20:47:43.438 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=飛空艇の管理
2025-09-22 20:47:43.438 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=潜水艦の管理
2025-09-22 20:47:43.438 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=キャンセル
2025-09-22 20:47:43.438 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] No UI candidates passed scoring thresholds.
2025-09-22 20:47:43.440 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectSubManagement@VoyageScheduler
2025-09-22 20:47:43.447 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectSubManagement@VoyageScheduler], timeout=20000
2025-09-22 20:47:43.447 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:43.447 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:47:43.447 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectSubManagement@VoyageScheduler] completed successfully 
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207323626568
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　121樽'
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:43.753 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]  探索完了'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Bonfire'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Pilvi'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Kukka'
2025-09-22 20:47:43.754 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Submarine-1'
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskFinalizeVessel name=Siipi, type=Submersible, quit=False
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskSelectVesselByName (Siipi)
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskSelectVesselByName: Siipi@<>c__DisplayClass0_0
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task FinalizeVessel@VoyageScheduler
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Siipi, type=Submersible
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__7_0@<>c
2025-09-22 20:47:43.756 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <ScheduleResend>b__1@<>c__DisplayClass7_0
2025-09-22 20:47:43.762 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskSelectVesselByName: Siipi@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:43.762 +09:00 [DBG] [AutoRetainer] Selecting vessel Siipi/Submersible/Siipi  [Rank131]  探索完了/1
2025-09-22 20:47:43.762 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:43.762 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:47:43.762 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskSelectVesselByName: Siipi@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:43.762 +09:00 [DBG] [AutoRetainer] FPS restrictions removed
2025-09-22 20:47:43.769 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:47:44.068 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:47:44.074 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Buffered voyage notification 19FCBCA737A8F58FA346ABAF4A847853A3E33BCCC4622DC884D307CC506D9474 status=Completed arrival=2025/09/22 5:00:57.
2025-09-22 20:47:44.074 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [FinalizeVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:44.074 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:44.191 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [FinalizeVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:44.198 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:47:44.548 +09:00 [VRB] WindowSystem "" Window "AutoRetainer 4.5.2.1 | Session expires in 2 days 23 hours###AutoRetainer" has focus now
2025-09-22 20:47:44.665 +09:00 [VRB] WindowSystem "" Window "AutoRetainer 4.5.2.1 | Session expires in 2 days 23 hours###AutoRetainer" lost focus
2025-09-22 20:47:44.731 +09:00 [VRB] [INTERFACE] [IM] Disposing 1 textures
2025-09-22 20:47:44.735 +09:00 [VRB] WindowSystem "DalamudCore" Window "プラグインインストーラ (テスト中)###XlPluginInstaller" has focus now
2025-09-22 20:47:46.074 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Completed voyage 0x40000001EB8997:5:6a82bdc9-f7a0-7636-0c01-00858f3ef13a duplicate arrival detected; skipping (arrival=2025/09/22 5:00:57).
2025-09-22 20:47:47.089 +09:00 [VRB] WindowSystem "DalamudCore" Window "プラグインインストーラ (テスト中)###XlPluginInstaller" lost focus
2025-09-22 20:47:47.093 +09:00 [VRB] Config saved
2025-09-22 20:47:47.158 +09:00 [VRB] [INTERFACE] [IM] Disposing 1 textures
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 14
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2206106619272
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'Siipi [Rank131]
　探索機体数：3/4機
　保有燃料数：青燐水バレル　121樽'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=7
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'ボイジャー出港'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '前回のボイジャー報告'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'パーツの変更'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'パーツの修理'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'パーツの染色'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] '設定'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'やめる'
2025-09-22 20:47:47.908 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:47.913 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:47:47.920 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:47.920 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 7754, index: 1, id: 24363, cond: 7754, index: 2, id: 24352, cond: 7754, index: 3, id: 24349, cond: 7754
2025-09-22 20:47:47.920 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:47.927 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__7_0@<>c], timeout=20000
2025-09-22 20:47:47.927 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__7_0@<>c] completed successfully 
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<ScheduleResend>b__1@<>c__DisplayClass7_0], timeout=20000
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployVessel name=Siipi, type=Submersible
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForSelectStringAddon@<>c
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Siipi, type=Submersible
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskRedeployPreviousLog
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskIntelligentRepair, name=Siipi, type=Submersible
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task RedeployVessel@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task DeployVessel@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForCutscene@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] (stack) Enqueued task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] Inserting stack with 11 tasks
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForNoCutscene@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForCutscene@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task DeployVessel@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task CheckForFuel@TaskRedeployPreviousLog
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task RedeployVessel@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitUntilFinalizeDeployAddonExists@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task SelectViewPreviousLog@VoyageScheduler
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task IntelligentRepairTask@<>c__DisplayClass0_0
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Inserted task WaitForSelectStringAddon@<>c
2025-09-22 20:47:47.935 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<ScheduleResend>b__1@<>c__DisplayClass7_0] completed successfully 
2025-09-22 20:47:47.940 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForSelectStringAddon@<>c], timeout=20000
2025-09-22 20:47:47.940 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForSelectStringAddon@<>c] completed successfully 
2025-09-22 20:47:47.947 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:47.947 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 7754, index: 1, id: 24363, cond: 7754, index: 2, id: 24352, cond: 7754, index: 3, id: 24349, cond: 7754
2025-09-22 20:47:47.947 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:47.954 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [IntelligentRepairTask@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:47:47.954 +09:00 [DBG] [AutoRetainer] Repair check log: index: 0, id: 24350, cond: 7754, index: 1, id: 24363, cond: 7754, index: 2, id: 24352, cond: 7754, index: 3, id: 24349, cond: 7754
2025-09-22 20:47:47.954 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [IntelligentRepairTask@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:47:47.961 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewPreviousLog@VoyageScheduler], timeout=20000
2025-09-22 20:47:47.961 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:47.961 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 1
2025-09-22 20:47:47.961 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewPreviousLog@VoyageScheduler] completed successfully 
2025-09-22 20:47:47.970 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler], timeout=20000
2025-09-22 20:47:47.989 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitUntilFinalizeDeployAddonExists@VoyageScheduler] completed successfully 
2025-09-22 20:47:47.996 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [RedeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:48.037 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationResult, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:48.046 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [RedeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:48.051 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:47:48.121 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:47:48.128 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [CheckForFuel@TaskRedeployPreviousLog], timeout=20000
2025-09-22 20:47:48.128 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [CheckForFuel@TaskRedeployPreviousLog] completed successfully 
2025-09-22 20:47:48.136 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [DeployVessel@VoyageScheduler], timeout=20000
2025-09-22 20:47:48.136 +09:00 [VRB] [AutoRetainer] Firing callback: AirShipExplorationDetail, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:48.136 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [DeployVessel@VoyageScheduler] completed successfully 
2025-09-22 20:47:48.142 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:47:48.294 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:48.302 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForNoCutscene@VoyageScheduler], timeout=20000
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 9
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2199574022696
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header 'このカットシーンをスキップしますか？'
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=2
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'はい'
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'いいえ'
2025-09-22 20:47:50.060 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header keyword check failed.
2025-09-22 20:47:50.065 +09:00 [DBG] [AutoRetainer] Selecting cutscene skipping
2025-09-22 20:47:50.065 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:50.065 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 0
2025-09-22 20:47:50.081 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Flushing 7 buffered voyage notification(s) for character 18014398541695383.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification B4D1ED835F3B5D0A0D805018B2E4D95D14595487ED75384B4AF57FD127E0CD80 arrival=2025/09/22 5:00:08.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification FB33E45FC9D5DC350B7AE08B7695DC4047B74B461B2DC301DB5B28E40BCE46D7 arrival=2025/09/22 5:00:18.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification 7FAF018B6A4695FA9F2ADD5D9953CB459F1348820C2D1F5CF4BDE636B9EEB9EB arrival=2025/09/22 5:00:18.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification EFC051879814DC34F0B4449BEB79AA6A957CD3A36C91DB8FC68B4728C750B7C5 arrival=2025/09/22 5:00:26.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification 0941A7BCCB57AAE59445E9D35530499CDC2D29F638B899BD3C5B306E7F10AE86 arrival=2025/09/22 5:00:26.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification 3E0ED97CC53E26417677EF4463C34F131A80ABDF73E00CA8E6F7B936814CE0E0 arrival=2025/09/22 5:00:57.
2025-09-22 20:47:50.083 +09:00 [DBG] [XIVSubmarinesRewrite] [Notifications] Enqueued voyage notification 19FCBCA737A8F58FA346ABAF4A847853A3E33BCCC4622DC884D307CC506D9474 arrival=2025/09/22 5:00:57.
2025-09-22 20:47:51.225 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForNoCutscene@VoyageScheduler] completed successfully 
2025-09-22 20:47:51.225 +09:00 [DBG] [AutoRetainer] FPS restrictions restored
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 12
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205943048376
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '潜水艦を選択してください。
　探索機体数：4/4機
　保有燃料数：青燐水バレル　94樽'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=5
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'Bonfire  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'Siipi  [Rank131]   [探索中:残り時間 1日16時間29分]'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'Pilvi  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'Kukka  [Rank131]   [探索中:残り時間 1日16時間28分]'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] 'やめる'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=やめる
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Bonfire'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Siipi'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Pilvi'
2025-09-22 20:47:51.255 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row => 'Kukka'
2025-09-22 20:47:51.295 +09:00 [DBG] [AutoRetainer] [Voyage] Task enqueued: TaskQuitMenu
2025-09-22 20:47:51.295 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuitVesselSelectorMenu@VoyageScheduler
2025-09-22 20:47:51.302 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuitVesselSelectorMenu@VoyageScheduler], timeout=20000
2025-09-22 20:47:51.302 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:51.302 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 4
2025-09-22 20:47:51.302 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuitVesselSelectorMenu@VoyageScheduler] completed successfully 
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 10
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205626573400
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=3
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] '飛空艇の管理'
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] '潜水艦の管理'
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'キャンセル'
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=飛空艇の管理
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=潜水艦の管理
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Row score below threshold score=-1 threshold=3 texts=キャンセル
2025-09-22 20:47:52.283 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] No UI candidates passed scoring thresholds.
2025-09-22 20:47:52.288 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectExitMainPanel@VoyageScheduler
2025-09-22 20:47:52.294 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectExitMainPanel@VoyageScheduler], timeout=20000
2025-09-22 20:47:52.295 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:47:52.295 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 2
2025-09-22 20:47:52.295 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectExitMainPanel@VoyageScheduler] completed successfully 
2025-09-22 20:47:52.572 +09:00 [DBG] [AutoRetainer] <!> Exited voyage panel, disabled voyage scheduler
2025-09-22 20:47:52.581 +09:00 [DBG] [AutoRetainer] YesAlready unlocked
2025-09-22 20:47:52.581 +09:00 [DBG] [AutoRetainer] TextAdvance unlocked
2025-09-22 20:47:54.284 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:47:54.311 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:47:54.331 +09:00 [VRB] [GameGui] HoveredItem changed: 5069
2025-09-22 20:47:54.352 +09:00 [VRB] [GameGui] HoveredItem changed: 22505
2025-09-22 20:47:54.382 +09:00 [VRB] [GameGui] HoveredItem changed: 5722
2025-09-22 20:47:54.568 +09:00 [VRB] [GameGui] HoveredItem changed: 22505
2025-09-22 20:47:54.638 +09:00 [VRB] [GameGui] HoveredItem changed: 1005067
2025-09-22 20:47:54.680 +09:00 [VRB] [GameGui] HoveredItem changed: 1005187
2025-09-22 20:47:54.701 +09:00 [VRB] [GameGui] HoveredItem changed: 1005192
2025-09-22 20:47:54.811 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:47:55.359 +09:00 [VRB] [GameGui] HoveredItem changed: 22502
2025-09-22 20:47:55.457 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:47:55.519 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:47:55.555 +09:00 [VRB] [GameGui] HoveredItem changed: 5069
2025-09-22 20:47:55.645 +09:00 [VRB] [GameGui] HoveredItem changed: 1009360
2025-09-22 20:47:55.686 +09:00 [VRB] [GameGui] HoveredItem changed: 22501
2025-09-22 20:47:55.998 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:47:56.700 +09:00 [DBG] [YesAlready] [InputNumeric] text=入れる個数を指定してください。
2025-09-22 20:47:56.700 +09:00 [VRB] [YesAlready] [InputNumeric] No match on 合成に出す個数を指定してください。 (入れる個数を指定してください。)
2025-09-22 20:47:56.700 +09:00 [DBG] [YesAlready] [InputNumeric] Not proceeding
2025-09-22 20:47:57.527 +09:00 [VRB] [GameGui] HoveredItem changed: 22506
2025-09-22 20:47:57.653 +09:00 [VRB] [GameGui] HoveredItem changed: 1005192
2025-09-22 20:47:57.681 +09:00 [VRB] [GameGui] HoveredItem changed: 1005187
2025-09-22 20:47:57.713 +09:00 [VRB] [GameGui] HoveredItem changed: 1005067
2025-09-22 20:47:57.735 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:47:57.825 +09:00 [VRB] [GameGui] HoveredItem changed: 5069
2025-09-22 20:47:57.930 +09:00 [VRB] [GameGui] HoveredItem changed: 1009360
2025-09-22 20:47:58.041 +09:00 [VRB] [GameGui] HoveredItem changed: 1036060
2025-09-22 20:47:58.145 +09:00 [VRB] [GameGui] HoveredItem changed: 14952
2025-09-22 20:47:58.331 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:47:58.652 +09:00 [VRB] [GameGui] HoveredItem changed: 5192
2025-09-22 20:47:58.748 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:47:59.089 +09:00 [VRB] [GameGui] HoveredItem changed: 1005067
2025-09-22 20:47:59.137 +09:00 [VRB] [GameGui] HoveredItem changed: 22505
2025-09-22 20:47:59.671 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:00.279 +09:00 [DBG] [YesAlready] [InputNumeric] text=入れる個数を指定してください。
2025-09-22 20:48:00.279 +09:00 [VRB] [YesAlready] [InputNumeric] No match on 合成に出す個数を指定してください。 (入れる個数を指定してください。)
2025-09-22 20:48:00.279 +09:00 [DBG] [YesAlready] [InputNumeric] Not proceeding
2025-09-22 20:48:01.192 +09:00 [VRB] [GameGui] HoveredItem changed: 22506
2025-09-22 20:48:01.290 +09:00 [VRB] [GameGui] HoveredItem changed: 7569
2025-09-22 20:48:01.304 +09:00 [VRB] [GameGui] HoveredItem changed: 7671
2025-09-22 20:48:01.311 +09:00 [VRB] [GameGui] HoveredItem changed: 33916
2025-09-22 20:48:01.320 +09:00 [VRB] [GameGui] HoveredItem changed: 7621
2025-09-22 20:48:01.332 +09:00 [VRB] [GameGui] HoveredItem changed: 21800
2025-09-22 20:48:01.347 +09:00 [VRB] [GameGui] HoveredItem changed: 44549
2025-09-22 20:48:01.361 +09:00 [VRB] [GameGui] HoveredItem changed: 46978
2025-09-22 20:48:01.486 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:01.513 +09:00 [VRB] [GameGui] HoveredItem changed: 1005189
2025-09-22 20:48:01.575 +09:00 [VRB] [GameGui] HoveredItem changed: 22504
2025-09-22 20:48:02.214 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:03.193 +09:00 [DBG] [YesAlready] [InputNumeric] text=入れる個数を指定してください。
2025-09-22 20:48:03.193 +09:00 [VRB] [YesAlready] [InputNumeric] No match on 合成に出す個数を指定してください。 (入れる個数を指定してください。)
2025-09-22 20:48:03.193 +09:00 [DBG] [YesAlready] [InputNumeric] Not proceeding
2025-09-22 20:48:04.229 +09:00 [VRB] [GameGui] HoveredItem changed: 22506
2025-09-22 20:48:04.242 +09:00 [VRB] [GameGui] HoveredItem changed: 22505
2025-09-22 20:48:04.360 +09:00 [VRB] [GameGui] HoveredItem changed: 22504
2025-09-22 20:48:04.651 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:04.915 +09:00 [VRB] [GameGui] HoveredItem changed: 22504
2025-09-22 20:48:05.125 +09:00 [VRB] [GameGui] HoveredItem changed: 22505
2025-09-22 20:48:05.652 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:05.839 +09:00 [VRB] [GameGui] HoveredItem changed: 12585
2025-09-22 20:48:05.887 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:06.609 +09:00 [VRB] [GameGui] HoveredItem changed: 1005187
2025-09-22 20:48:06.734 +09:00 [VRB] [GameGui] HoveredItem changed: 1005192
2025-09-22 20:48:06.791 +09:00 [VRB] [GameGui] HoveredItem changed: 47998
2025-09-22 20:48:07.060 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:08.730 +09:00 [VRB] [GameGui] HoveredItem changed: 47998
2025-09-22 20:48:08.991 +09:00 [VRB] [GameGui] HoveredItem changed: 12228
2025-09-22 20:48:09.033 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:09.220 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:48:09.887 +09:00 [VRB] [GameGui] HoveredItem changed: 1005067
2025-09-22 20:48:10.324 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:10.735 +09:00 [VRB] [GameGui] HoveredItem changed: 12545
2025-09-22 20:48:10.805 +09:00 [VRB] [GameGui] HoveredItem changed: 1012544
2025-09-22 20:48:10.866 +09:00 [VRB] [GameGui] HoveredItem changed: 1005189
2025-09-22 20:48:11.062 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:11.332 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:11.729 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:12.047 +09:00 [VRB] [GameGui] HoveredItem changed: 1005189
2025-09-22 20:48:12.067 +09:00 [VRB] [GameGui] HoveredItem changed: 38949
2025-09-22 20:48:12.125 +09:00 [VRB] [GameGui] HoveredItem changed: 46974
2025-09-22 20:48:12.137 +09:00 [VRB] [GameGui] HoveredItem changed: 46982
2025-09-22 20:48:12.172 +09:00 [VRB] [GameGui] HoveredItem changed: 46720
2025-09-22 20:48:12.206 +09:00 [VRB] [GameGui] HoveredItem changed: 43540
2025-09-22 20:48:12.229 +09:00 [VRB] [GameGui] HoveredItem changed: 43942
2025-09-22 20:48:12.298 +09:00 [VRB] [GameGui] HoveredItem changed: 5118
2025-09-22 20:48:12.458 +09:00 [VRB] [GameGui] HoveredItem changed: 22502
2025-09-22 20:48:12.754 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:13.749 +09:00 [DBG] [YesAlready] [InputNumeric] text=入れる個数を指定してください。
2025-09-22 20:48:13.749 +09:00 [VRB] [YesAlready] [InputNumeric] No match on 合成に出す個数を指定してください。 (入れる個数を指定してください。)
2025-09-22 20:48:13.749 +09:00 [DBG] [YesAlready] [InputNumeric] Not proceeding
2025-09-22 20:48:14.515 +09:00 [VRB] [GameGui] HoveredItem changed: 22507
2025-09-22 20:48:14.583 +09:00 [VRB] [GameGui] HoveredItem changed: 12922
2025-09-22 20:48:14.609 +09:00 [VRB] [GameGui] HoveredItem changed: 12928
2025-09-22 20:48:14.727 +09:00 [VRB] [GameGui] HoveredItem changed: 22500
2025-09-22 20:48:15.025 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:15.749 +09:00 [DBG] [YesAlready] [InputNumeric] text=入れる個数を指定してください。
2025-09-22 20:48:15.749 +09:00 [VRB] [YesAlready] [InputNumeric] No match on 合成に出す個数を指定してください。 (入れる個数を指定してください。)
2025-09-22 20:48:15.749 +09:00 [DBG] [YesAlready] [InputNumeric] Not proceeding
2025-09-22 20:48:16.833 +09:00 [VRB] [GameGui] HoveredItem changed: 22507
2025-09-22 20:48:17.394 +09:00 [VRB] [GameGui] HoveredItem changed: 22500
2025-09-22 20:48:17.402 +09:00 [VRB] [GameGui] HoveredItem changed: 22507
2025-09-22 20:48:17.574 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:17.602 +09:00 [DBG] [AutoRetainer] ConditionWasEnabled = false;
2025-09-22 20:48:17.965 +09:00 [DBG] [YesAlready] [SelectYesno] text=「地下工房」から出ますか？
2025-09-22 20:48:17.965 +09:00 [VRB] [YesAlready] [SelectYesno] No match on へ入りますか？ (「地下工房」から出ますか？)
2025-09-22 20:48:17.965 +09:00 [VRB] [YesAlready] [SelectYesno] Matched on text から出ますか？ (「地下工房」から出ますか？)
2025-09-22 20:48:17.965 +09:00 [DBG] [YesAlready] [SelectYesno] Proceeding
2025-09-22 20:48:19.435 +09:00 [DBG] [ClientState] TerritoryType changed: 1250
2025-09-22 20:48:19.435 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <EnqueueWriteWhenPlayerAvailable>b__0_0@<>c
2025-09-22 20:48:19.442 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c], timeout=60000
2025-09-22 20:48:19.629 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c] completed successfully 
2025-09-22 20:48:22.414 +09:00 [DBG] [YesAlready] [SelectIconString] text=アイテムの購入(畑・染料), アイテムの購入(触媒), キャンセル
2025-09-22 20:48:22.415 +09:00 [VRB] [YesAlready] [SelectIconString] No match on ミュタミクス (よろずシステム)
2025-09-22 20:48:22.415 +09:00 [DBG] [YesAlready] [SelectIconString] Target restriction not met: よろずシステム does not match ミュタミクス
2025-09-22 20:48:22.415 +09:00 [DBG] [YesAlready] [SelectIconString] Not proceeding
2025-09-22 20:48:23.365 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectIconString with values: 0
2025-09-22 20:48:24.082 +09:00 [VRB] [GameGui] HoveredItem changed: 44549
2025-09-22 20:48:24.179 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:24.716 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:24.834 +09:00 [VRB] [GameGui] HoveredItem changed: 8214
2025-09-22 20:48:24.992 +09:00 [VRB] [GameGui] HoveredItem changed: 1036060
2025-09-22 20:48:25.089 +09:00 [VRB] [GameGui] HoveredItem changed: 1009360
2025-09-22 20:48:25.152 +09:00 [VRB] [GameGui] HoveredItem changed: 5069
2025-09-22 20:48:25.200 +09:00 [VRB] [GameGui] HoveredItem changed: 5192
2025-09-22 20:48:25.235 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:25.325 +09:00 [VRB] [GameGui] HoveredItem changed: 12228
2025-09-22 20:48:25.562 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:25.657 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:25.784 +09:00 [VRB] [GameGui] HoveredItem changed: 12228
2025-09-22 20:48:25.805 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:25.825 +09:00 [VRB] [GameGui] HoveredItem changed: 17019
2025-09-22 20:48:25.941 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:26.048 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:26.172 +09:00 [VRB] [GameGui] HoveredItem changed: 17019
2025-09-22 20:48:26.336 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:26.442 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:26.583 +09:00 [VRB] [GameGui] HoveredItem changed: 17019
2025-09-22 20:48:26.775 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:26.882 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:27.020 +09:00 [VRB] [GameGui] HoveredItem changed: 17019
2025-09-22 20:48:27.118 +09:00 [VRB] [GameGui] HoveredItem changed: 5192
2025-09-22 20:48:27.211 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:27.317 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:27.471 +09:00 [VRB] [GameGui] HoveredItem changed: 5192
2025-09-22 20:48:27.533 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:27.635 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:27.742 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:27.887 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:28.066 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:28.171 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:28.319 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:28.367 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:28.422 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:28.531 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:28.637 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:28.784 +09:00 [VRB] [GameGui] HoveredItem changed: 23875
2025-09-22 20:48:28.813 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:28.853 +09:00 [VRB] [GameGui] HoveredItem changed: 12925
2025-09-22 20:48:28.962 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:29.068 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:29.234 +09:00 [VRB] [GameGui] HoveredItem changed: 12925
2025-09-22 20:48:29.304 +09:00 [VRB] [GameGui] HoveredItem changed: 12924
2025-09-22 20:48:29.408 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:29.513 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:29.652 +09:00 [VRB] [GameGui] HoveredItem changed: 12924
2025-09-22 20:48:29.738 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:48:29.857 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:29.963 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:30.067 +09:00 [VRB] [GameGui] HoveredItem changed: 1012223
2025-09-22 20:48:30.138 +09:00 [VRB] [GameGui] HoveredItem changed: 5069
2025-09-22 20:48:30.281 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:30.386 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:30.513 +09:00 [VRB] [GameGui] HoveredItem changed: 5069
2025-09-22 20:48:30.561 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:30.569 +09:00 [VRB] [GameGui] HoveredItem changed: 1009360
2025-09-22 20:48:30.691 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:30.796 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:30.945 +09:00 [VRB] [GameGui] HoveredItem changed: 1009360
2025-09-22 20:48:31.005 +09:00 [VRB] [GameGui] HoveredItem changed: 5722
2025-09-22 20:48:31.115 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:31.220 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:31.360 +09:00 [VRB] [GameGui] HoveredItem changed: 5722
2025-09-22 20:48:31.402 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:31.860 +09:00 [VRB] [GameGui] HoveredItem changed: 1005067
2025-09-22 20:48:32.025 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:32.129 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:32.249 +09:00 [VRB] [GameGui] HoveredItem changed: 1005067
2025-09-22 20:48:32.305 +09:00 [VRB] [GameGui] HoveredItem changed: 1005187
2025-09-22 20:48:32.414 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:32.518 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:32.672 +09:00 [VRB] [GameGui] HoveredItem changed: 1005187
2025-09-22 20:48:32.770 +09:00 [VRB] [GameGui] HoveredItem changed: 1005192
2025-09-22 20:48:32.858 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:32.963 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:33.138 +09:00 [VRB] [GameGui] HoveredItem changed: 1005192
2025-09-22 20:48:33.193 +09:00 [VRB] [GameGui] HoveredItem changed: 12585
2025-09-22 20:48:33.303 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 20:48:33.407 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:33.547 +09:00 [VRB] [GameGui] HoveredItem changed: 12585
2025-09-22 20:48:33.575 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:33.706 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:33.810 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:33.936 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:33.964 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:33.979 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:34.088 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:34.192 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:34.321 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:34.496 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:34.602 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:34.749 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:34.948 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:35.054 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:35.199 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:35.393 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:35.498 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:35.623 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:35.658 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:35.693 +09:00 [VRB] [GameGui] HoveredItem changed: 12545
2025-09-22 20:48:35.802 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:35.908 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:36.040 +09:00 [VRB] [GameGui] HoveredItem changed: 12545
2025-09-22 20:48:36.068 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:36.130 +09:00 [VRB] [GameGui] HoveredItem changed: 1012544
2025-09-22 20:48:36.220 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:36.324 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:36.451 +09:00 [VRB] [GameGui] HoveredItem changed: 1012544
2025-09-22 20:48:36.506 +09:00 [VRB] [GameGui] HoveredItem changed: 1005189
2025-09-22 20:48:36.635 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:36.741 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:36.880 +09:00 [VRB] [GameGui] HoveredItem changed: 1005189
2025-09-22 20:48:36.923 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:37.045 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:37.150 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:37.290 +09:00 [VRB] [GameGui] HoveredItem changed: 9733
2025-09-22 20:48:37.361 +09:00 [VRB] [GameGui] HoveredItem changed: 38949
2025-09-22 20:48:37.374 +09:00 [VRB] [GameGui] HoveredItem changed: 46974
2025-09-22 20:48:37.401 +09:00 [VRB] [GameGui] HoveredItem changed: 46720
2025-09-22 20:48:37.429 +09:00 [VRB] [GameGui] HoveredItem changed: 43942
2025-09-22 20:48:37.574 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:37.610 +09:00 [VRB] [GameGui] HoveredItem changed: 5118
2025-09-22 20:48:37.739 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 20:48:37.846 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:38.013 +09:00 [VRB] [GameGui] HoveredItem changed: 5118
2025-09-22 20:48:38.036 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:38.118 +09:00 [VRB] [GameGui] HoveredItem changed: 5187
2025-09-22 20:48:38.255 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 20:48:38.360 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:38.512 +09:00 [VRB] [GameGui] HoveredItem changed: 5187
2025-09-22 20:48:38.637 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 20:48:42.304 +09:00 [DBG] [AutoRetainer] Plugin is enabled, reason: Access
2025-09-22 20:48:42.307 +09:00 [DBG] [AutoRetainer] ConditionWasEnabled = true
2025-09-22 20:48:42.313 +09:00 [VRB] [AutoRetainer] Starting ThreadLoadImageHandler
2025-09-22 20:48:42.313 +09:00 [VRB] [AutoRetainer] Loading image C:\Users\MonaT\AppData\Roaming\XIVLauncher\installedPlugins\AutoRetainer\4.5.2.1\res\bell.png
2025-09-22 20:48:42.314 +09:00 [VRB] [AutoRetainer] Stopping ThreadLoadImageHandler, ticks=100
2025-09-22 20:48:42.955 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:48:42.956 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Rizbety
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] YesAlready locked
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] FPS restrictions removed
2025-09-22 20:48:42.957 +09:00 [DBG] [AutoRetainer] TextAdvance locked
2025-09-22 20:48:42.962 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:48:42.989 +09:00 [VRB] [WindowSystem] Saved preset for AutoRetainer retainerlist overlay
2025-09-22 20:48:42.990 +09:00 [VRB] Config saved
2025-09-22 20:48:43.032 +09:00 [DBG] [AutoRetainer] Selecting retainer Rizbety with index 0
2025-09-22 20:48:43.034 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:48:43.034 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:48:43.039 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:48:43.039 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:48:43.044 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:48:43.824 +09:00 [DBG] [AutoRetainer] Retainer Rizbety current venture=1051
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207338166792
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13873枚]
（Rizbety）'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：155枠]'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => 預託中：155枠 | アイテムの受け渡し
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：874,217]'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[吟遊詩人：Lv100 ]'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:43.980 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Workshop territory gate prevented UI fallback snapshot.
2025-09-22 20:48:44.051 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:44.051 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:48:44.051 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:48:44.058 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:48:44.059 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:48:44.066 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:48:44.319 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541722
2025-09-22 20:48:44.320 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:44.336 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:44.412 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:48:44.412 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:48:44.419 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:48:44.623 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C37, 0000001C1211B3D8
2025-09-22 20:48:44.627 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:48:44.635 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:48:44.704 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:48:44.711 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:48:44.712 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:48:44.712 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:48:44.719 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:44.719 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Rizbety
2025-09-22 20:48:44.720 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:44.724 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:44.725 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:48:44.726 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:48:44.726 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:48:44.726 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:44.733 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:48:44.994 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541723
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207338166216
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13871枚]
（Rizbety）'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：155枠]'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：874,217]'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:48]'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[吟遊詩人：Lv100 ]'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:45.679 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:45.752 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:45.752 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:48:45.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:48:45.761 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:48:46.440 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Bangbang
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:48:46.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:48:46.761 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:48:46.830 +09:00 [DBG] [AutoRetainer] Selecting retainer Bangbang with index 1
2025-09-22 20:48:46.830 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:48:46.830 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:48:46.835 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:48:46.836 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:48:46.842 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:48:47.454 +09:00 [DBG] [AutoRetainer] Retainer Bangbang current venture=1051
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2202141261304
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13871枚]
（Bangbang）'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：152枠]'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：151,750]'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[吟遊詩人：Lv100 ]'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:47.602 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:47.676 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:47.676 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:48:47.676 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:48:47.683 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:48:47.683 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:48:47.690 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:48:47.942 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541726
2025-09-22 20:48:47.942 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:47.954 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:48.031 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:48:48.031 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:48:48.037 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:48:48.294 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C38, 0000001C1211B3D8
2025-09-22 20:48:48.302 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:48:48.307 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:48:48.377 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:48:48.384 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:48:48.384 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:48:48.384 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:48:48.392 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:48.392 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Bangbang
2025-09-22 20:48:48.392 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:48.398 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:48.398 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:48:48.398 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:48:48.398 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:48:48.398 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:48.405 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:48:48.616 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541726
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2202141261304
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13869枚]
（Bangbang）'
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:49.310 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：152枠]'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：151,750]'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:48]'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[吟遊詩人：Lv100 ]'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:49.311 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:49.385 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:49.385 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:48:49.385 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:48:49.393 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:48:50.078 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Gizmel
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:48:50.338 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:48:50.343 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:48:50.413 +09:00 [DBG] [AutoRetainer] Selecting retainer Gizmel with index 2
2025-09-22 20:48:50.413 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:48:50.413 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:48:50.419 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:48:50.419 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:48:50.426 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:48:51.086 +09:00 [DBG] [AutoRetainer] Retainer Gizmel current venture=1054
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2202141263032
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13869枚]
（Gizmel）'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：123枠]'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[採掘師：Lv100 ]'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:51.326 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:51.400 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:51.400 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:48:51.400 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:48:51.405 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:48:51.405 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:48:51.412 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:48:51.616 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541729
2025-09-22 20:48:51.616 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:51.637 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:51.710 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:48:51.711 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:48:51.718 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:48:51.934 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C39, 0000001C1211B3D8
2025-09-22 20:48:51.940 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:48:51.947 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:48:52.017 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:48:52.023 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:48:52.024 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:48:52.024 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:48:52.032 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:52.032 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Gizmel
2025-09-22 20:48:52.032 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:52.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:52.038 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:48:52.038 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:48:52.038 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:48:52.038 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:52.044 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:48:52.291 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541730
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2202141263032
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13867枚]
（Gizmel）'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：123枠]'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:48]'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[採掘師：Lv100 ]'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:52.985 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:53.058 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:53.058 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:48:53.058 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:48:53.066 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:48:53.745 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Mermaidgirl
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:48:54.003 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:48:54.010 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:48:54.079 +09:00 [DBG] [AutoRetainer] Selecting retainer Mermaidgirl with index 3
2025-09-22 20:48:54.079 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:48:54.079 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:48:54.086 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:48:54.086 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:48:54.093 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:48:54.718 +09:00 [DBG] [AutoRetainer] Retainer Mermaidgirl current venture=1054
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2207338166792
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13867枚]
（Mermaidgirl）'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：148枠]'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => 預託中：148枠 | アイテムの受け渡し
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[採掘師：Lv100 ]'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:54.867 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:54.942 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:54.942 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:48:54.942 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:48:54.947 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:48:54.947 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:48:54.954 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:48:55.166 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541733
2025-09-22 20:48:55.166 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:55.180 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:55.259 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:48:55.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:48:55.267 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:48:55.525 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C3A, 0000001C1211B3D8
2025-09-22 20:48:55.530 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:48:55.538 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:48:55.608 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:48:55.613 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:48:55.613 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:48:55.613 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:48:55.620 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:55.620 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Mermaidgirl
2025-09-22 20:48:55.620 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:55.629 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:55.629 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:48:55.629 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:48:55.629 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:48:55.629 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:55.634 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:48:55.838 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541734
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2202141263032
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13865枚]
（Mermaidgirl）'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：148枠]'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:48]'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[採掘師：Lv100 ]'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:56.526 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:56.599 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:56.599 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:48:56.599 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:48:56.608 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:48:57.279 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Pokemonmaster
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:48:57.592 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:48:57.599 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:48:57.670 +09:00 [DBG] [AutoRetainer] Selecting retainer Pokemonmaster with index 4
2025-09-22 20:48:57.671 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:48:57.671 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:48:57.675 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:48:57.675 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:48:57.682 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:48:58.363 +09:00 [DBG] [AutoRetainer] Retainer Pokemonmaster current venture=1054
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13865枚]
（Pokemonmaster）'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：158枠]'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[採掘師：Lv100 ]'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:48:58.541 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:48:58.613 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:48:58.613 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:48:58.613 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:48:58.620 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:48:58.620 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:48:58.627 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:48:58.838 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541737
2025-09-22 20:48:58.838 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:58.850 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:48:58.925 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:48:58.925 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:48:58.932 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:48:59.191 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C3B, 0000001C1211B3D8
2025-09-22 20:48:59.196 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:48:59.204 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:48:59.273 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:48:59.281 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:48:59.281 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:48:59.281 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:48:59.286 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:59.287 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Pokemonmaster
2025-09-22 20:48:59.287 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:59.295 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:48:59.295 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:48:59.295 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:48:59.295 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:48:59.295 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:48:59.300 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:48:59.505 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541737
2025-09-22 20:49:00.199 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13863枚]
（Pokemonmaster）'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：158枠]'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:48]'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[採掘師：Lv100 ]'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:00.200 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:00.274 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:00.274 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:49:00.274 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:49:00.279 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:49:00.960 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Raam
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:49:01.259 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:49:01.266 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:49:01.335 +09:00 [DBG] [AutoRetainer] Selecting retainer Raam with index 5
2025-09-22 20:49:01.335 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:49:01.335 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:49:01.344 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:49:01.344 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:49:01.349 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:49:01.995 +09:00 [DBG] [AutoRetainer] Retainer Raam current venture=1057
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13863枚]
（Raam）'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：173枠]'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：2,619]'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[園芸師：Lv100 ]'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:02.408 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:02.481 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:02.481 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:49:02.481 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:49:02.488 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:49:02.488 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:49:02.496 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:49:02.692 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541740
2025-09-22 20:49:02.693 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:49:02.706 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:49:02.780 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:49:02.780 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:49:02.788 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:49:03.045 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C3C, 0000001C1211B3D8
2025-09-22 20:49:03.050 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:49:03.058 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:49:03.127 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:49:03.134 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:49:03.134 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:49:03.134 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:49:03.141 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:49:03.141 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Raam
2025-09-22 20:49:03.141 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:49:03.149 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:49:03.150 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:49:03.150 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:49:03.150 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:49:03.150 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:49:03.154 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:49:03.366 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541741
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13861枚]
（Raam）'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：173枠]'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：2,619]'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:49]'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[園芸師：Lv100 ]'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:04.053 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:04.127 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:04.127 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:49:04.127 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:49:04.134 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:49:04.781 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Vivilia
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:49:05.071 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:49:05.078 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:49:05.148 +09:00 [DBG] [AutoRetainer] Selecting retainer Vivilia with index 6
2025-09-22 20:49:05.148 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:49:05.148 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:49:05.156 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:49:05.156 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:49:05.161 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:49:05.669 +09:00 [DBG] [AutoRetainer] Retainer Vivilia current venture=1057
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932357704
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13861枚]
（Vivilia）'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：172枠]'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Normalized row[0] => 預託中：172枠 | アイテムの受け渡し
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[園芸師：Lv100 ]'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:06.033 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:06.106 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:06.106 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:49:06.106 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:49:06.115 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:49:06.115 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:49:06.120 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:49:06.373 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541744
2025-09-22 20:49:06.373 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:49:06.385 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:49:06.460 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:49:06.460 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:49:06.467 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:49:06.684 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C3D, 0000001C1211B3D8
2025-09-22 20:49:06.689 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:49:06.696 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:49:06.766 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:49:06.773 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:49:06.773 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:49:06.773 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:49:06.781 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:49:06.781 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Vivilia
2025-09-22 20:49:06.781 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:49:06.786 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:49:06.787 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:49:06.787 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:49:06.787 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:49:06.787 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:49:06.793 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:49:07.046 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541745
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13859枚]
（Vivilia）'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：172枠]'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：0]'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:49]'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[園芸師：Lv100 ]'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:07.734 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:07.808 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:07.808 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:49:07.808 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:49:07.815 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:49:08.509 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Tick>b__2@<>c__DisplayClass17_0
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireSendRetainerToVentureEvent for Magonote
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForYesAlreadyDisabledTask@NewYesAlreadyManager
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectViewVentureReport@RetainerHandlers
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <Enqueue>b__0_0@<>c
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickResultReassign@RetainerHandlers
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task WaitForVentureListUpdate@RetainerHandlers
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task Delay (10 frames)@<>c__DisplayClass2_0
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ClickAskAssign@RetainerHandlers
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task SelectQuit@RetainerHandlers
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task ConfirmCantBuyback@RetainerHandlers
2025-09-22 20:49:08.752 +09:00 [DBG] [AutoRetainer] Retainer Vivilia current venture=1057
2025-09-22 20:49:08.758 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Tick>b__2@<>c__DisplayClass17_0], timeout=20000
2025-09-22 20:49:08.828 +09:00 [DBG] [AutoRetainer] Selecting retainer Magonote with index 7
2025-09-22 20:49:08.829 +09:00 [VRB] [AutoRetainer] Firing callback: RetainerList, valueCount = 4, updateStatte = True, values:

2025-09-22 20:49:08.829 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Tick>b__2@<>c__DisplayClass17_0] completed successfully 
2025-09-22 20:49:08.835 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager], timeout=20000
2025-09-22 20:49:08.835 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForYesAlreadyDisabledTask@NewYesAlreadyManager] completed successfully 
2025-09-22 20:49:08.843 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectViewVentureReport@RetainerHandlers], timeout=20000
2025-09-22 20:49:08.843 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:49:09.649 +09:00 [DBG] [AutoRetainer] Retainer Magonote current venture=1057
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13859枚]
（Magonote）'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：100枠]'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：197,869]'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[完了]'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[園芸師：Lv100 ]'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:09.796 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:09.870 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:09.870 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 5
2025-09-22 20:49:09.870 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectViewVentureReport@RetainerHandlers] completed successfully 
2025-09-22 20:49:09.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<Enqueue>b__0_0@<>c], timeout=20000
2025-09-22 20:49:09.877 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<Enqueue>b__0_0@<>c] completed successfully 
2025-09-22 20:49:09.885 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickResultReassign@RetainerHandlers], timeout=20000
2025-09-22 20:49:10.087 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541748
2025-09-22 20:49:10.087 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:49:10.100 +09:00 [DBG] [AutoRetainer] Single item RawText - Text: Mona Tierraは
2025-09-22 20:49:10.175 +09:00 [DBG] [AutoRetainer] Clicked reassign
2025-09-22 20:49:10.175 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickResultReassign@RetainerHandlers] completed successfully 
2025-09-22 20:49:10.184 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [WaitForVentureListUpdate@RetainerHandlers], timeout=20000
2025-09-22 20:49:10.449 +09:00 [DBG] [AutoRetainer] 00007FF7422F0740, 40036C3E, 0000001C1211B3D8
2025-09-22 20:49:10.453 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [WaitForVentureListUpdate@RetainerHandlers] completed successfully 
2025-09-22 20:49:10.460 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [Delay (10 frames)@<>c__DisplayClass2_0], timeout=20000
2025-09-22 20:49:10.530 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [Delay (10 frames)@<>c__DisplayClass2_0] completed successfully 
2025-09-22 20:49:10.536 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ClickAskAssign@RetainerHandlers], timeout=20000
2025-09-22 20:49:10.537 +09:00 [DBG] [AutoRetainer] Clicked assign
2025-09-22 20:49:10.537 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ClickAskAssign@RetainerHandlers] completed successfully 
2025-09-22 20:49:10.543 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:49:10.544 +09:00 [DBG] [AutoRetainer] [IPC] Firing FireRetainerPostprocessTaskRequestEvent for Magonote
2025-09-22 20:49:10.544 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessIPCEnqueue@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:49:10.552 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0], timeout=20000
2025-09-22 20:49:10.552 +09:00 [DBG] [AutoRetainer] Stack mode begins
2025-09-22 20:49:10.552 +09:00 [DBG] [AutoRetainer] SchedulerMain.RetainerPostprocess contains: , pluginToProcess = 
2025-09-22 20:49:10.553 +09:00 [DBG] [AutoRetainer] Inserting stack with 0 tasks
2025-09-22 20:49:10.553 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [TaskRetainerPostprocessProcessEntries@<>c__DisplayClass0_0] completed successfully 
2025-09-22 20:49:10.557 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [SelectQuit@RetainerHandlers], timeout=20000
2025-09-22 20:49:10.761 +09:00 [DBG] [AutoRetainer] Recorded venture start time = 1758541748
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Type (Dalamud.Game.Addon.Lifecycle.AddonArgsType) => Dalamud.Game.Addon.Lifecycle.AddonArgsType: Setup
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueCount (System.UInt32) => System.UInt32: 33
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValues (System.IntPtr) => System.IntPtr: 2205932358280
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AtkValueSpan (System.Span`1[[FFXIVClientStructs.FFXIV.Component.GUI.AtkValue, FFXIVClientStructs, Version=0.0.6580.0, Culture=neutral, PublicKeyToken=null]]) => <null>: <null>
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.AddonName (System.String) => System.String: SelectString
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] AddonArgs.Addon (Dalamud.Game.NativeWrapper.AtkUnitBasePtr) => Dalamud.Game.NativeWrapper.AtkUnitBasePtr: Dalamud.Game.NativeWrapper.AtkUnitBasePtr
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Header '何をする？　[所持ベンチャースクリップ：13857枚]
（Magonote）'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] PopupMenu entries=13
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[0] 'アイテムの受け渡し　[預託中：100枠]'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[1] 'ギルの受け渡し　 [預託中：197,869]'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[2] 'マーケット出品（プレイヤー所持品から）'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[3] 'マーケット出品（リテイナー所持品から）'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[4] '販売履歴を見る'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[5] 'リテイナーベンチャーの確認　[～2025/9/23 14:49]'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[6] 'リテイナーベンチャーの依頼　[依頼中]'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[7] 'リテイナーの装備設定　[メインアーム装備中]'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[8] 'リテイナーのクラス設定　[園芸師：Lv100 ]'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[9] 'リテイナーの武具投影'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[10] 'リテイナーのアイテム染色'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[11] 'リテイナーのメモ編集'
2025-09-22 20:49:11.456 +09:00 [DBG] [XIVSubmarinesRewrite] [UI Inspector] Entry[12] 'リテイナーを帰す'
2025-09-22 20:49:11.530 +09:00 [VRB] [AutoRetainer] Firing callback: SelectString, valueCount = 1, updateStatte = True, values:

2025-09-22 20:49:11.530 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectString with values: 12
2025-09-22 20:49:11.530 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [SelectQuit@RetainerHandlers] completed successfully 
2025-09-22 20:49:11.538 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [ConfirmCantBuyback@RetainerHandlers], timeout=20000
2025-09-22 20:49:11.932 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758541750000 36175716000000
2025-09-22 20:49:12.219 +09:00 [DBG] [AutoRetainer] Last venture ID reset
2025-09-22 20:49:12.467 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [ConfirmCantBuyback@RetainerHandlers] completed successfully 
2025-09-22 20:49:12.469 +09:00 [DBG] [AutoRetainer] YesAlready unlocked
2025-09-22 20:49:12.469 +09:00 [DBG] [AutoRetainer] FPS restrictions restored
2025-09-22 20:49:12.469 +09:00 [DBG] [AutoRetainer] TextAdvance unlocked
2025-09-22 20:52:07.296 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758541925000 36175719600000
2025-09-22 20:55:02.315 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758542100000 36175723200000
2025-09-22 20:57:57.371 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758542275000 36175726800000
2025-09-22 21:00:52.283 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758542450000 36175730400000
2025-09-22 21:03:47.342 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758542625000 36175734000000
2025-09-22 21:06:41.612 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758542800000 36175737600000
2025-09-22 21:09:37.090 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758542975000 36175741200000
2025-09-22 21:12:31.496 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758543150000 36175744800000
2025-09-22 21:15:26.902 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758543325000 36175748400000
2025-09-22 21:17:04.223 +09:00 [DBG] [AutoRetainer] ConditionWasEnabled = false;
2025-09-22 21:17:04.223 +09:00 [DBG] [AutoRetainer] Disabling plugin because AutoDisable is on
2025-09-22 21:17:04.223 +09:00 [DBG] [AutoRetainer] Plugin disabled
2025-09-22 21:17:06.231 +09:00 [VRB] [INTERFACE] [IM] Disposing 1 textures
2025-09-22 21:18:21.539 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758543500000 36175752000000
2025-09-22 21:21:16.501 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758543675000 36175755600000
2025-09-22 21:24:11.691 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758543850000 36175759200000
2025-09-22 21:27:06.827 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758544025000 36175762800000
2025-09-22 21:30:02.072 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758544200000 36175766400000
2025-09-22 21:32:57.075 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758544375000 36175770000000
2025-09-22 21:35:52.270 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758544550000 36175773600000
2025-09-22 21:38:45.839 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758544725000 36175777200000
2025-09-22 21:41:40.892 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758544900000 36175780800000
2025-09-22 21:44:35.969 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758545075000 36175784400000
2025-09-22 21:47:30.818 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758545250000 36175788000000
2025-09-22 21:50:25.950 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758545425000 36175791600000
2025-09-22 21:53:20.875 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758545600000 36175795200000
2025-09-22 21:56:15.390 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758545775000 36175798800000
2025-09-22 21:59:10.625 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758545950000 36175802400000
2025-09-22 22:02:05.816 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758546125000 36175806000000
2025-09-22 22:05:00.059 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758546300000 36175809600000
2025-09-22 22:07:55.615 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758546475000 36175813200000
2025-09-22 22:10:50.266 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758546650000 36175816800000
2025-09-22 22:13:45.883 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758546825000 36175820400000
2025-09-22 22:16:40.488 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758547000000 36175824000000
2025-09-22 22:19:35.765 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758547175000 36175827600000
2025-09-22 22:22:30.454 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758547350000 36175831200000
2025-09-22 22:25:25.942 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758547525000 36175834800000
2025-09-22 22:28:20.814 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758547700000 36175838400000
2025-09-22 22:28:24.844 +09:00 [DBG] [YesAlready] [SelectIconString] text=アイテムの購入(畑・染料), アイテムの購入(触媒), キャンセル
2025-09-22 22:28:24.845 +09:00 [VRB] [YesAlready] [SelectIconString] No match on ミュタミクス (よろずシステム)
2025-09-22 22:28:24.845 +09:00 [DBG] [YesAlready] [SelectIconString] Target restriction not met: よろずシステム does not match ミュタミクス
2025-09-22 22:28:24.845 +09:00 [DBG] [YesAlready] [SelectIconString] Not proceeding
2025-09-22 22:28:25.787 +09:00 [DBG] [YesAlready] [Watcher] Callback triggered on SelectIconString with values: 0
2025-09-22 22:28:26.524 +09:00 [VRB] [GameGui] HoveredItem changed: 28724
2025-09-22 22:28:26.789 +09:00 [VRB] [GameGui] HoveredItem changed: 7569
2025-09-22 22:28:26.989 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:27.059 +09:00 [VRB] [GameGui] HoveredItem changed: 43984
2025-09-22 22:28:27.094 +09:00 [VRB] [GameGui] HoveredItem changed: 43985
2025-09-22 22:28:27.122 +09:00 [VRB] [GameGui] HoveredItem changed: 5825
2025-09-22 22:28:27.266 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 22:28:27.371 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:27.504 +09:00 [VRB] [GameGui] HoveredItem changed: 5825
2025-09-22 22:28:27.553 +09:00 [VRB] [GameGui] HoveredItem changed: 44056
2025-09-22 22:28:27.676 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 1 custom items.
2025-09-22 22:28:27.781 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:27.906 +09:00 [VRB] [GameGui] HoveredItem changed: 44056
2025-09-22 22:28:27.935 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:27.948 +09:00 [VRB] [GameGui] HoveredItem changed: 43996
2025-09-22 22:28:28.072 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 22:28:28.176 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:28.288 +09:00 [VRB] [GameGui] HoveredItem changed: 43996
2025-09-22 22:28:28.338 +09:00 [VRB] [GameGui] HoveredItem changed: 44005
2025-09-22 22:28:28.474 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 22:28:28.579 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:28.725 +09:00 [VRB] [GameGui] HoveredItem changed: 44005
2025-09-22 22:28:28.782 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:28.893 +09:00 [VRB] [GameGui] HoveredItem changed: 44028
2025-09-22 22:28:29.024 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 22:28:29.128 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:29.253 +09:00 [VRB] [GameGui] HoveredItem changed: 44028
2025-09-22 22:28:29.310 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:29.323 +09:00 [VRB] [GameGui] HoveredItem changed: 43985
2025-09-22 22:28:29.453 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 22:28:29.558 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:29.684 +09:00 [VRB] [GameGui] HoveredItem changed: 43985
2025-09-22 22:28:29.698 +09:00 [VRB] [GameGui] HoveredItem changed: 43984
2025-09-22 22:28:29.828 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 22:28:29.933 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:30.712 +09:00 [VRB] [GameGui] HoveredItem changed: 43984
2025-09-22 22:28:31.002 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
2025-09-22 22:28:31.107 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:31.232 +09:00 [VRB] [GameGui] HoveredItem changed: 43984
2025-09-22 22:28:31.371 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:33.817 +09:00 [VRB] [GameGui] HoveredItem changed: 46691
2025-09-22 22:28:33.920 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 22:28:37.371 +09:00 [VRB] [GameGui] HoverActionId: Action/34685 this:20138E5F1F0
2025-09-22 22:28:37.384 +09:00 [VRB] [GameGui] HoverActionId: Action/35347 this:20138E5F1F0
2025-09-22 22:28:37.406 +09:00 [VRB] [GameGui] HoverActionId: Action/35348 this:20138E5F1F0
2025-09-22 22:28:37.412 +09:00 [VRB] [GameGui] HoverActionId: Action/34653 this:20138E5F1F0
2025-09-22 22:28:37.557 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/20 this:20138E5F1F0
2025-09-22 22:28:37.634 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/14 this:20138E5F1F0
2025-09-22 22:31:15.789 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758547875000 36175842000000
2025-09-22 22:34:10.856 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758548050000 36175845600000
2025-09-22 22:37:05.820 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758548225000 36175849200000
2025-09-22 22:40:00.873 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758548400000 36175852800000
2025-09-22 22:42:55.364 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758548575000 36175856400000
2025-09-22 22:45:50.860 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758548750000 36175860000000
2025-09-22 22:46:19.105 +09:00 [VRB] [AUTOUPDATE] Starting periodic update check
2025-09-22 22:46:19.106 +09:00 [INF] [PluginManager] Now reloading all PluginMasters...
2025-09-22 22:46:19.106 +09:00 [INF] [PLUGINR] Fetching repo: https://kamori.goats.dev/Plugin/PluginMaster
2025-09-22 22:46:19.239 +09:00 [VRB] [HTTP] Established connection to kamori.goats.dev at 172.67.208.228:443
2025-09-22 22:46:20.344 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://kamori.goats.dev/Plugin/PluginMaster
2025-09-22 22:46:20.344 +09:00 [INF] [PLUGINR] Fetching repo: https://raw.githubusercontent.com/Eternita-S/MyDalamudPlugins/main/pluginmaster.json
2025-09-22 22:46:20.344 +09:00 [INF] [PLUGINR] Fetching repo: https://github.com/Aireil/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 22:46:20.344 +09:00 [INF] [PLUGINR] Fetching repo: https://github.com/daemitus/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 22:46:20.345 +09:00 [INF] [PLUGINR] Fetching repo: https://love.puni.sh/ment.json
2025-09-22 22:46:20.403 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8000::154]:443
2025-09-22 22:46:20.404 +09:00 [VRB] [HTTP] Established connection to github.com at 20.27.177.113:443
2025-09-22 22:46:20.417 +09:00 [VRB] [HTTP] Established connection to github.com at 20.27.177.113:443
2025-09-22 22:46:20.470 +09:00 [VRB] [HTTP] Established connection to raw.githubusercontent.com at [2606:50c0:8000::154]:443
2025-09-22 22:46:20.510 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://github.com/daemitus/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 22:46:20.525 +09:00 [VRB] [HTTP] Established connection to love.puni.sh at [2606:4700:3031::6815:3264]:443
2025-09-22 22:46:20.596 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://raw.githubusercontent.com/Eternita-S/MyDalamudPlugins/main/pluginmaster.json
2025-09-22 22:46:20.692 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://github.com/Aireil/MyDalamudPlugins/raw/master/pluginmaster.json
2025-09-22 22:46:21.305 +09:00 [VRB] [HTTP] Established connection to puni.sh at [2606:4700:3035::ac43:ccb6]:443
2025-09-22 22:46:21.811 +09:00 [INF] [PLUGINR] Successfully fetched repo: https://love.puni.sh/ment.json
2025-09-22 22:46:21.812 +09:00 [INF] [PluginManager] PluginMasters reloaded, now refiltering...
2025-09-22 22:46:21.850 +09:00 [DBG] [PluginManager] Starting plugin update check...
2025-09-22 22:46:21.850 +09:00 [DBG] [PluginManager] Update check found 1 available updates.
2025-09-22 22:46:21.852 +09:00 [VRB] [AUTOUPDATE] Available Updates: SonarPlugin
2025-09-22 22:46:21.852 +09:00 [VRB] [AUTOUPDATE] Auto update found nothing to do, next update at "2025-09-23T00:46:21.8520443+09:00"
2025-09-22 22:48:45.129 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758548925000 36175863600000
2025-09-22 22:51:40.258 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758549100000 36175867200000
2025-09-22 22:54:35.513 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758549275000 36175870800000
2025-09-22 22:57:30.871 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758549450000 36175874400000
2025-09-22 23:00:25.328 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758549625000 36175878000000
2025-09-22 23:03:20.851 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758549800000 36175881600000
2025-09-22 23:06:15.633 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758549975000 36175885200000
2025-09-22 23:09:10.496 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758550150000 36175888800000
2025-09-22 23:12:05.748 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758550325000 36175892400000
2025-09-22 23:15:00.794 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758550500000 36175896000000
2025-09-22 23:17:55.877 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758550675000 36175899600000
2025-09-22 23:20:50.782 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758550850000 36175903200000
2025-09-22 23:23:45.891 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758551025000 36175906800000
2025-09-22 23:26:40.878 +09:00 [VRB] [GatherBuddy] Eorzea Hour and Weather Change triggered. 1758551200000 36175910400000
2025-09-22 23:29:35.961 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758551375000 36175914000000
2025-09-22 23:32:30.084 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758551550000 36175917600000
2025-09-22 23:35:25.735 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758551725000 36175921200000
2025-09-22 23:38:20.108 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758551900000 36175924800000
2025-09-22 23:41:15.822 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758552075000 36175928400000
2025-09-22 23:44:10.604 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758552250000 36175932000000
2025-09-22 23:47:05.645 +09:00 [VRB] [GatherBuddy] Eorzea Hour Change triggered. 1758552425000 36175935600000
2025-09-22 23:48:22.133 +09:00 [VRB] [GameGui] HoverActionId: Action/34676 this:20138E5F1F0
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Enter CreateFlyText detour!
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Called with addonFlyText(201AF20E170) kind(12) val1(0) val2(0) damageTypeIcon(0) text1(201ED864DA0, "+トライアングレート") text2(201439C2FF0, "") color(FF005D2A) icon(211051) yOffset(0)
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Calling flytext events!
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Calling flytext with original args.
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Enter CreateFlyText detour!
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Called with addonFlyText(201AF20E170) kind(12) val1(0) val2(0) damageTypeIcon(0) text1(201C9E10D60, "+トゥルー・オブ・フォレスト") text2(201439C2A10, "") color(FF005D2A) icon(211057) yOffset(0)
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Calling flytext events!
2025-09-22 23:48:23.583 +09:00 [VRB] [FlyText] Calling flytext with original args.
2025-09-22 23:48:23.862 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/8 this:20138E5F1F0
2025-09-22 23:48:23.959 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/7 this:20138E5F1F0
2025-09-22 23:48:24.306 +09:00 [VRB] [GameGui] HoverActionId: Action/273 this:20138E5F1F0
2025-09-22 23:48:32.852 +09:00 [DBG] [ClientState] TerritoryType changed: 1192
2025-09-22 23:48:32.852 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <EnqueueWriteWhenPlayerAvailable>b__0_0@<>c
2025-09-22 23:48:32.915 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c], timeout=60000
2025-09-22 23:48:33.427 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c] completed successfully 
2025-09-22 23:48:59.225 +09:00 [VRB] [GameGui] HoverActionId: Action/815 this:20138E5F1F0
2025-09-22 23:48:59.309 +09:00 [VRB] [GameGui] HoverActionId: Action/4590 this:20138E5F1F0
2025-09-22 23:48:59.326 +09:00 [VRB] [GameGui] HoverActionId: Action/4095 this:20138E5F1F0
2025-09-22 23:48:59.548 +09:00 [VRB] [GameGui] HoverActionId: Action/4095 this:20138E5F1F0
2025-09-22 23:48:59.617 +09:00 [VRB] [GameGui] HoverActionId: Action/4590 this:20138E5F1F0
2025-09-22 23:49:00.719 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:00.733 +09:00 [VRB] [GameGui] HoveredItem changed: 44039
2025-09-22 23:49:00.770 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:00.782 +09:00 [VRB] [GameGui] HoveredItem changed: 8
2025-09-22 23:49:00.856 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:00.859 +09:00 [VRB] [GameGui] HoveredItem changed: 44039
2025-09-22 23:49:00.921 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:00.933 +09:00 [VRB] [GameGui] HoveredItem changed: 46185
2025-09-22 23:49:01.268 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:03.789 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/7 this:20138E5F1F0
2025-09-22 23:49:03.875 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/7 this:20138E5F1F0
2025-09-22 23:49:03.964 +09:00 [VRB] [GameGui] HoverActionId: GeneralAction/7 this:20138E5F1F0
2025-09-22 23:49:07.085 +09:00 [VRB] [GameGui] HoveredItem changed: 47998
2025-09-22 23:49:07.932 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:12.145 +09:00 [DBG] [ClientState] TerritoryType changed: 339
2025-09-22 23:49:12.146 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] Enqueued task <EnqueueWriteWhenPlayerAvailable>b__0_0@<>c
2025-09-22 23:49:12.146 +09:00 [WRN] [HITCH] Long GameNetworkDown detected, 33.4609ms > 30ms - check in the plugin stats window.
2025-09-22 23:49:12.150 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →Starting to execute task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c], timeout=60000
2025-09-22 23:49:12.550 +09:00 [DBG] [AutoRetainer] [NeoTaskManager] →→Task [<EnqueueWriteWhenPlayerAvailable>b__0_0@<>c] completed successfully 
2025-09-22 23:49:19.113 +09:00 [DBG] [YesAlready] [SelectYesno] text=このレターを削除します。 よろしいですか？
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on へ入りますか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on から出ますか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on ハイクオリティ品がトレードされようとしています。 本当によろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on を カンパニー製作設備に納品します。 よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 下記のアイテムを修理しますか？  (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on ダークマターG6×99を、11,880で購入します。 よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 青燐水バレル×99を9,900カンパニークレジットで交換します。よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on を5個受け取ります。 よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 以下のアイテムを受け取ります。よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on Kansha ShimasuMandragoraのパーティに参加します。よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on ハイクオリティ品がトレードされようとしています。 本当によろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 選択したマテリアを合成します。よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on にテレポします。 よろしいですか？  (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [VRB] [YesAlready] [SelectYesno] No match on 「蜃気楼の島 クレセントアイル：南征編」の 開始地点に戻ります。よろしいですか？ (このレターを削除します。 よろしいですか？)
2025-09-22 23:49:19.113 +09:00 [DBG] [YesAlready] [SelectYesno] Not proceeding
2025-09-22 23:49:21.844 +09:00 [VRB] [GameGui] HoveredItem changed: 8214
2025-09-22 23:49:21.933 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:21.981 +09:00 [VRB] [GameGui] HoveredItem changed: 1036060
2025-09-22 23:49:22.073 +09:00 [VRB] [GameGui] HoveredItem changed: 0
2025-09-22 23:49:22.189 +09:00 [VRB] [GameGui] HoveredItem changed: 46185
2025-09-22 23:49:22.314 +09:00 [VRB] [ContextMenu] Opening Inventory context menu with 2 custom items.
