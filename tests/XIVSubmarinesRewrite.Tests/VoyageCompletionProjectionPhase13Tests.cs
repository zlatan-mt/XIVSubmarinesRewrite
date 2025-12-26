// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/VoyageCompletionProjectionPhase13Tests.cs
// VoyageCompletionProjection の通知抑止と通常通知の振る舞いを検証します
// 起動時抑止と ForceNotify 状態管理の変更を安全に保つために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs, apps/XIVSubmarinesRewrite/src/Acquisition/SnapshotCache.cs

namespace XIVSubmarinesRewrite.Tests;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>
/// VoyageCompletionProjection のPhase 13変更に関する単体テスト
/// Completed通知がフィルタリングされることと起動時抑止を検証
/// </summary>
public class VoyageCompletionProjectionPhase13Tests
{
    [Fact]
    public void HandleCompletedVoyage_DoesNotEnqueueNotification()
    {
        _ = CreateProjection(out var queue, out var cache);

        var initialSnapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(12));
        cache.Update(initialSnapshot, 1);
        queue.Clear();

        var completedSnapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Completed,
            arrival: DateTime.UtcNow);
        cache.Update(completedSnapshot, 1);

        Assert.Empty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void Startup_Underway_DoesNotNotify()
    {
        _ = CreateProjection(out var queue, out var cache);

        var snapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(12));
        cache.Update(snapshot, 1);

        Assert.Empty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void Startup_CachedUnderway_DoesNotNotify()
    {
        var cache = new SnapshotCache();
        var cached = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(10));
        cache.Update(cached, 1);

        _ = CreateProjection(cache, out var queue);

        var snapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(12));
        cache.Update(snapshot, 1);

        Assert.Empty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void Startup_CachedCompleted_ToUnderway_Suppressed()
    {
        var cache = new SnapshotCache();
        var cached = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Completed,
            arrival: DateTime.UtcNow.AddHours(-1));
        cache.Update(cached, 1);

        _ = CreateProjection(cache, out var queue);

        var snapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(8));
        cache.Update(snapshot, 1);

        Assert.Empty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void Startup_SecondCharacter_Suppressed()
    {
        _ = CreateProjection(out var queue, out var cache);

        var snapshotA = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(snapshotA, 1);

        var snapshotB = CreateSnapshot(2, new SubmarineId(2, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(snapshotB, 2);

        Assert.Empty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void Normal_NewSubmarine_Notifies()
    {
        _ = CreateProjection(out var queue, out var cache);

        var baseline = CreateSnapshot(1, Array.Empty<Submarine>());
        cache.Update(baseline, 1);
        queue.Clear();

        var snapshot = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(snapshot, 1);

        Assert.Single(queue.EnqueuedNotifications);
    }

    [Fact]
    public void Normal_Transition_CompletedToUnderway_Notifies()
    {
        _ = CreateProjection(out var queue, out var cache);

        var baseline = CreateSnapshot(1, Array.Empty<Submarine>());
        cache.Update(baseline, 1);
        queue.Clear();

        var completed = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Completed, DateTime.UtcNow.AddHours(-1));
        cache.Update(completed, 1);
        queue.Clear();

        var underway = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(8));
        cache.Update(underway, 1);

        Assert.Single(queue.EnqueuedNotifications);
    }

    [Fact]
    public void SilentInit_Then_ArrivalChange_Notifies()
    {
        _ = CreateProjection(out var queue, out var cache);

        var initial = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(initial, 1);
        queue.Clear();

        var changed = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(10));
        cache.Update(changed, 1);

        Assert.Single(queue.EnqueuedNotifications);
    }

    [Fact]
    public void SwitchCharacter_Then_Return_Suppressed()
    {
        _ = CreateProjection(out var queue, out var cache);

        var snapshotA = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(snapshotA, 1);

        var snapshotB = CreateSnapshot(2, new SubmarineId(2, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(snapshotB, 2);

        var snapshotAReturn = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(snapshotAReturn, 1);

        Assert.Empty(queue.EnqueuedNotifications);
    }

    private static VoyageCompletionProjection CreateProjection(out TestNotificationQueue queue, out SnapshotCache cache)
    {
        cache = new SnapshotCache();
        return CreateProjection(cache, out queue);
    }

    private static VoyageCompletionProjection CreateProjection(SnapshotCache cache, out TestNotificationQueue queue)
    {
        var settings = new NotificationSettings
        {
            NotifyVoyageCompleted = true,
            NotifyVoyageUnderway = true,
            ForceNotifyUnderway = true
        };
        queue = new TestNotificationQueue();
        var characterRegistry = new TestCharacterRegistry();
        var log = new NullLogSink();

        return new VoyageCompletionProjection(
            cache,
            queue,
            settings,
            characterRegistry,
            log);
    }

    private static AcquisitionSnapshot CreateSnapshot(ulong characterId, IReadOnlyList<Submarine> submarines)
    {
        return new AcquisitionSnapshot(
            DateTime.UtcNow,
            submarines,
            AcquisitionSourceKind.Composite,
            characterId,
            $"Character-{characterId}",
            $"World-{characterId}",
            SnapshotConfidence.Merged);
    }

    private static AcquisitionSnapshot CreateSnapshot(
        ulong characterId,
        SubmarineId submarineId,
        VoyageStatus status,
        DateTime arrival)
    {
        var departure = DateTime.UtcNow.AddHours(-12);
        var voyage = new Voyage(
            VoyageId.Create(submarineId, Guid.NewGuid()),
            "Route-1",
            departure,
            arrival,
            status);

        var submarine = new Submarine(
            submarineId,
            $"Sub-{submarineId.Slot}",
            "profile-1",
            new[] { voyage });

        return CreateSnapshot(characterId, new[] { submarine });
    }

    private class TestNotificationQueue : INotificationQueue
    {
        public List<NotificationEnvelope> EnqueuedNotifications { get; } = new();

        public bool TryEnqueue(NotificationEnvelope envelope, bool forceDuplicate = false)
        {
            EnqueuedNotifications.Add(envelope);
            return true;
        }

        public void Clear() => EnqueuedNotifications.Clear();

        public ValueTask<NotificationWorkItem?> DequeueAsync(CancellationToken cancellationToken) =>
            new((NotificationWorkItem?)null);

        public void ReportSuccess(NotificationWorkItem item) { }

        public NotificationDeliveryState ReportFailure(NotificationWorkItem item, Exception error, TimeSpan? overrideDelay = null) =>
            NotificationDeliveryState.DeadLetter;

        public IReadOnlyCollection<NotificationWorkItemSnapshot> GetPending() =>
            Array.Empty<NotificationWorkItemSnapshot>();

        public IReadOnlyCollection<NotificationWorkItemSnapshot> GetDeadLetters() =>
            Array.Empty<NotificationWorkItemSnapshot>();

        public bool TryRequeueDeadLetter(string hashKey) => false;
    }

    private class TestCharacterRegistry : ICharacterRegistry
    {
        public event EventHandler<CharacterChangedEventArgs>? ActiveCharacterChanged;
        public event EventHandler? CharacterListChanged;

        public ulong ActiveCharacterId => 1;
        public IReadOnlyList<CharacterDescriptor> Characters => Array.Empty<CharacterDescriptor>();

        public CharacterIdentity? GetIdentity(ulong characterId) =>
            new CharacterIdentity(characterId, $"Character-{characterId}", $"World-{characterId}");

        public void RegisterSnapshot(AcquisitionSnapshot snapshot) { }

        public void SelectCharacter(ulong characterId) { }

        public DateTime? GetLastUpdatedUtc(ulong characterId) => null;

        public IReadOnlyList<CharacterDescriptor> GetCharactersWithSubmarineOperations() =>
            Array.Empty<CharacterDescriptor>();

        public void CleanupCharactersWithoutSubmarineOperations() { }
    }
}
