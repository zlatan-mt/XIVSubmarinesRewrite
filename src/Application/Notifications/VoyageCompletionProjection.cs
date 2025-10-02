// apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs
// スナップショット更新から航海完了通知を検出し、通知キューへ橋渡しします
// 同一航海の重複通知を抑えつつ、ForceNotify の制御も統合するために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.Buffering.cs, apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs

namespace XIVSubmarinesRewrite.Application.Notifications;

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Observes snapshot updates and enqueues voyage completion notifications.</summary>
public sealed partial class VoyageCompletionProjection : IDisposable, IForceNotifyDiagnostics
{
    private static readonly TimeSpan CompletedArrivalDuplicateTolerance = TimeSpan.FromSeconds(90);
    private readonly SnapshotCache cache;
    private readonly INotificationQueue queue;
    private readonly NotificationSettings notificationSettings;
    private readonly ICharacterRegistry characterRegistry;
    private readonly ILogSink log;
    private readonly TimeProvider timeProvider;
    private readonly Dictionary<ulong, AcquisitionSnapshot> snapshots = new ();
    // ForceNotifyUnderway 用のクールダウン間隔。
    private static readonly TimeSpan ForceNotifyCooldownWindow = TimeSpan.FromMinutes(30);
    private readonly Dictionary<SubmarineId, ForceNotifyState> forceNotifyStates = new ();
    private readonly PendingVoyageNotificationStore pendingNotifications = new (CompletedArrivalDuplicateTolerance);
    private readonly Dictionary<SubmarineId, DateTime> lastCompletedArrivals = new ();
    private readonly Queue<ForceNotifyManualTrigger> manualTriggerLog = new ();
    private readonly object gate = new ();
    private bool disposed;

    private bool ShouldNotifyUnderway => this.notificationSettings.NotifyVoyageUnderway || this.notificationSettings.ForceNotifyUnderway;

    private const int ManualTriggerLogLimit = 10;

    public VoyageCompletionProjection(
        SnapshotCache cache,
        INotificationQueue queue,
        NotificationSettings notificationSettings,
        ICharacterRegistry characterRegistry,
        ILogSink log,
        TimeProvider? timeProvider = null)
    {
        this.cache = cache;
        this.queue = queue;
        this.notificationSettings = notificationSettings;
        this.characterRegistry = characterRegistry;
        this.log = log;
        this.timeProvider = timeProvider ?? TimeProvider.System;

        foreach (var kvp in cache.GetAll())
        {
            this.snapshots[kvp.Key] = kvp.Value;
        }

        this.cache.SnapshotUpdated += this.OnSnapshotUpdated;
    }

    public void Dispose()
    {
        if (this.disposed)
        {
            return;
        }

        this.disposed = true;
        this.cache.SnapshotUpdated -= this.OnSnapshotUpdated;
    }

    private void OnSnapshotUpdated(object? sender, SnapshotUpdatedEventArgs args)
    {
        AcquisitionSnapshot? previous;
        lock (this.gate)
        {
            this.snapshots.TryGetValue(args.CharacterId, out previous);
            this.snapshots[args.CharacterId] = args.Snapshot;
        }

        if (previous is null)
        {
            return;
        }

        var previousVoyages = ExtractVoyages(previous);
        var currentVoyages = ExtractVoyages(args.Snapshot);

        foreach (var (submarineId, voyage) in currentVoyages)
        {
            previousVoyages.TryGetValue(submarineId, out var priorVoyage);
            this.ProcessVoyage(args.Snapshot, submarineId, voyage, priorVoyage);
        }

        this.CleanupForceNotifyState(currentVoyages.Values);
        this.TryFlushPendingNotifications(args.Snapshot);
    }

    private void ProcessVoyage(AcquisitionSnapshot snapshot, SubmarineId submarineId, Voyage voyage, Voyage? priorVoyage)
    {
        if (voyage.Status == VoyageStatus.Completed && voyage.Arrival is not null)
        {
            this.HandleCompletedVoyage(snapshot, voyage, priorVoyage);
            return;
        }

        if (voyage.Status != VoyageStatus.Underway)
        {
            return;
        }

        if (!this.ShouldNotifyUnderway)
        {
            this.log.Log(LogLevel.Trace, $"[Notifications] Underway voyage {voyage.Id} suppressed; NotifyVoyageUnderway={this.notificationSettings.NotifyVoyageUnderway} ForceNotify={this.notificationSettings.ForceNotifyUnderway}.");
            return;
        }

        this.HandleForceNotify(snapshot, voyage);
    }

    private void HandleCompletedVoyage(AcquisitionSnapshot snapshot, Voyage voyage, Voyage? priorVoyage)
    {
        var voyageId = voyage.Id;
        var submarineId = voyage.Id.SubmarineId;
        var arrivalUtc = voyage.Arrival!.Value.ToUniversalTime();

        if (priorVoyage is not null && priorVoyage.Status == VoyageStatus.Completed && priorVoyage.Arrival.HasValue)
        {
            var priorArrivalUtc = priorVoyage.Arrival.Value.ToUniversalTime();
            if (AreArrivalsClose(arrivalUtc, priorArrivalUtc))
            {
                this.log.Log(LogLevel.Trace, $"[Notifications] Completed voyage {voyageId} already handled (arrival={voyage.Arrival}).");
                return;
            }
        }

        this.forceNotifyStates.Remove(submarineId);
        if (this.lastCompletedArrivals.TryGetValue(submarineId, out var recordedArrivalUtc))
        {
            if (AreArrivalsClose(arrivalUtc, recordedArrivalUtc))
            {
                if (arrivalUtc < recordedArrivalUtc)
                {
                    this.lastCompletedArrivals[submarineId] = arrivalUtc;
                }

                this.log.Log(LogLevel.Trace, $"[Notifications] Completed voyage {voyageId} arrival within tolerance ({CompletedArrivalDuplicateTolerance.TotalSeconds:F0}s); refreshing buffer.");
            }
            else
            {
                this.lastCompletedArrivals[submarineId] = arrivalUtc;
            }
        }
        else
        {
            this.lastCompletedArrivals[submarineId] = arrivalUtc;
        }

        if (!this.notificationSettings.NotifyVoyageCompleted)
        {
            this.log.Log(LogLevel.Trace, $"[Notifications] Completed voyage {voyageId} suppressed; NotifyVoyageCompleted=false arrival={voyage.Arrival}.");
            return;
        }

        this.BufferNotification(snapshot, voyage);
    }

    private void HandleForceNotify(AcquisitionSnapshot snapshot, Voyage voyage)
    {
        var submarineId = voyage.Id.SubmarineId;
        var arrivalUtc = voyage.Arrival?.ToUniversalTime();

        if (!this.forceNotifyStates.TryGetValue(submarineId, out var state))
        {
            this.EmitForceNotify(snapshot, voyage, submarineId, "first-detect");
            return;
        }

        var now = this.timeProvider.GetUtcNow().UtcDateTime;
        var arrivalChanged = arrivalUtc.HasValue && (!state.LastArrivalUtc.HasValue || state.LastArrivalUtc.Value != arrivalUtc.Value);
        if (arrivalChanged)
        {
            this.EmitForceNotify(snapshot, voyage, submarineId, "arrival-changed");
            return;
        }

        if (now >= state.CooldownUntilUtc)
        {
            this.EmitForceNotify(snapshot, voyage, submarineId, "cooldown-expired");
            return;
        }

        var remaining = state.CooldownUntilUtc - now;
        if (remaining <= TimeSpan.Zero)
        {
            return;
        }

        var remainingWholeMinutes = Math.Max(0, (int)Math.Ceiling(remaining.TotalMinutes));
        if (!state.LastLoggedWholeMinutes.HasValue || remainingWholeMinutes < state.LastLoggedWholeMinutes.Value)
        {
            this.log.Log(LogLevel.Trace, $"[Notifications] ForceNotifyUnderway skipping submarine {submarineId}; cooldown {remainingWholeMinutes}m remaining.");
            this.forceNotifyStates[submarineId] = state with { LastLoggedWholeMinutes = remainingWholeMinutes };
        }
    }

    private static Dictionary<SubmarineId, Voyage> ExtractVoyages(AcquisitionSnapshot snapshot)
    {
        var result = new Dictionary<SubmarineId, Voyage>();
        foreach (var submarine in snapshot.Submarines)
        {
            var voyage = submarine.Voyages.LastOrDefault();
            if (voyage is null)
            {
                continue;
            }

            result[submarine.Id] = voyage;
        }

        return result;
    }
}

public sealed partial class VoyageCompletionProjection
{
    private static bool AreArrivalsClose(DateTime firstUtc, DateTime secondUtc)
        => (firstUtc - secondUtc).Duration() <= CompletedArrivalDuplicateTolerance;
}
