// src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.cs
// Dalamud UI から潜水艦データを取得するエントリポイントの本体を定義します
// 分割したパーシャルを束ね、安全に UI スナップショットを管理するために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.Lifecycle.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.Interop;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>
/// Listens to Dalamud UI events and extracts submarine information as a snapshot fallback.
/// </summary>
public sealed unsafe partial class DalamudUiSubmarineSnapshotSource : IUiSubmarineSnapshotSource, IDisposable
{
    private readonly IAddonLifecycle? addonLifecycle;
    private readonly IGameGui gameGui;
    private readonly ILogSink log;
    private readonly IPlayerState playerState;
    private readonly IAddonLifecycle.AddonEventDelegate lifecycleHandler;
    private readonly List<IDisposable> lifecycleSubscriptions = new ();
    private readonly List<(AddonEvent EventType, string AddonName)> lifecycleRegistrations = new ();
    private readonly object sync = new ();
    private List<Submarine>? pendingSubmarines;
    private DateTime lastTerritoryGateLogAtUtc;
    private DateTime lastNormalizedRowLogAtUtc;

    public DalamudUiSubmarineSnapshotSource(IAddonLifecycle? addonLifecycle, IGameGui gameGui, IPlayerState playerState, ILogSink log)
    {
        this.addonLifecycle = addonLifecycle;
        this.gameGui = gameGui;
        this.log = log;
        this.playerState = playerState;
        this.lifecycleHandler = this.OnAddonLifecycle;
        this.RegisterLifecycle();
    }

    public ValueTask<IReadOnlyList<Submarine>?> TryReadAsync(CancellationToken cancellationToken = default)
    {
        if (this.playerState.ContentId == 0)
        {
            return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
        }

        cancellationToken.ThrowIfCancellationRequested();
        lock (this.sync)
        {
            if (this.pendingSubmarines is null || this.pendingSubmarines.Count == 0)
            {
                if (this.addonLifecycle is null && this.TryCaptureActiveAddon(out var fallback))
                {
                    this.pendingSubmarines = fallback;
                }
                else
                {
                    return ValueTask.FromResult<IReadOnlyList<Submarine>?>(null);
                }
            }

            var snapshot = this.pendingSubmarines;
            this.pendingSubmarines = null;
            return ValueTask.FromResult<IReadOnlyList<Submarine>?>(snapshot);
        }
    }

    private void ProcessLifecycleEvent(AddonEvent eventType, AddonArgs? addonArgs, object? rawArgs)
    {
        if (Array.IndexOf(ObservedEvents, eventType) < 0)
        {
            return;
        }

        if (eventType == AddonEvent.PreFinalize)
        {
            lock (this.sync)
            {
                this.pendingSubmarines = null;
            }

            return;
        }

        try
        {
            if (rawArgs != null)
            {
                this.DumpAddonArgs(rawArgs);
            }

            var addonPtr = addonArgs is not null ? (nint)addonArgs.Addon : ExtractAddonPointer(rawArgs);
            if (addonPtr == nint.Zero)
            {
                this.log.Log(LogLevel.Debug, "[UI Inspector] Lifecycle payload missing addon pointer.");
                return;
            }

            var addon = (AddonSelectString*)addonPtr;
            var header = DecodeHeader(addon);
            if (!string.IsNullOrWhiteSpace(header))
            {
                this.log.Log(LogLevel.Debug, "[UI Inspector] Header '" + header + "'");
            }

            var rows = this.ExtractRowsFromPopupMenu(&addon->PopupMenu.PopupMenu);
            var filteredRows = this.FilterRowCandidates(header, &addon->PopupMenu.PopupMenu, rows);
            if (filteredRows.Count == 0)
            {
                lock (this.sync)
                {
                    this.pendingSubmarines = null;
                }

                return;
            }

            var submarines = new List<Submarine>();
            foreach (var row in filteredRows)
            {
                var submarine = this.BuildSubmarineFromRow(row);
                if (submarine != null)
                {
                    submarines.Add(submarine);
                    this.log.Log(LogLevel.Debug, "[UI Inspector] Row => '" + submarine.Name + "'");
                }
            }

            lock (this.sync)
            {
                this.pendingSubmarines = submarines;
            }
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Error, "Failed to read submarine UI fallback.", ex);
        }
    }

    private bool TryCaptureActiveAddon(out List<Submarine> submarines)
    {
        submarines = new List<Submarine>();
        foreach (var name in new[] { "SelectString", "SelectIconString" })
        {
            nint addonPtr = this.gameGui.GetAddonByName(name, 1);
            if (addonPtr == nint.Zero)
            {
                continue;
            }

            PopupMenu* menu;
            if (name.Equals("SelectIconString", StringComparison.OrdinalIgnoreCase))
            {
                menu = &((AddonSelectIconString*)addonPtr)->PopupMenu.PopupMenu;
            }
            else
            {
                menu = &((AddonSelectString*)addonPtr)->PopupMenu.PopupMenu;
            }

            var rows = this.ExtractRowsFromPopupMenu(menu);
            var filtered = this.FilterRowCandidates(string.Empty, menu, rows);
            foreach (var row in filtered)
            {
                var submarine = this.BuildSubmarineFromRow(row);
                if (submarine != null)
                {
                    submarines.Add(submarine);
                }
            }

            if (submarines.Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    public void Dispose()
    {
        foreach (var registration in this.lifecycleRegistrations)
        {
            try
            {
                this.addonLifecycle?.UnregisterListener(registration.EventType, registration.AddonName, this.lifecycleHandler);
            }
            catch (Exception ex)
            {
                this.log.Log(LogLevel.Warning, "[UI Inspector] Failed to unregister lifecycle listener for " + registration.AddonName + " (" + registration.EventType + ").", ex);
            }
        }

        this.lifecycleRegistrations.Clear();

        foreach (var subscription in this.lifecycleSubscriptions)
        {
            try
            {
                subscription.Dispose();
            }
            catch (Exception ex)
            {
                this.log.Log(LogLevel.Warning, "[UI Inspector] Failed to unregister lifecycle listener.", ex);
            }
        }

        this.lifecycleSubscriptions.Clear();
    }
}
