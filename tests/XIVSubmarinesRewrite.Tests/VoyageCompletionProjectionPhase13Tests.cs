namespace XIVSubmarinesRewrite.Tests;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Application.Services;

/// <summary>
/// VoyageCompletionProjection のPhase 13変更に関する単体テスト
/// Completed通知がフィルタリングされることを検証
/// </summary>
public class VoyageCompletionProjectionPhase13Tests
{
    [Fact]
    public void HandleCompletedVoyage_DoesNotEnqueueNotification()
    {
        // Arrange
        var settings = new NotificationSettings
        {
            NotifyVoyageCompleted = true, // 設定はtrueでも...
            NotifyVoyageUnderway = true
        };
        var queue = new TestNotificationQueue();
        var cache = new SnapshotCache();
        var characterRegistry = new TestCharacterRegistry();
        var log = new NullLogSink();
        
        var projection = new VoyageCompletionProjection(
            cache,
            queue,
            settings,
            characterRegistry,
            log
        );

        // 初期スナップショット（Underway状態）
        var initialSnapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(12)
        );
        cache.Update(initialSnapshot, 1);
        queue.Clear(); // ベースライン更新によるUnderway通知はこのテストの対象外

        // Act: Completed状態に変更
        var completedSnapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Completed,
            arrival: DateTime.UtcNow
        );
        cache.Update(completedSnapshot, 1);

        // Assert: Completed通知は送信されない
        Assert.Empty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void HandleUnderwayVoyage_EnqueuesNotification()
    {
        // Arrange
        var settings = new NotificationSettings
        {
            NotifyVoyageUnderway = true
        };
        var queue = new TestNotificationQueue();
        var cache = new SnapshotCache();
        var characterRegistry = new TestCharacterRegistry();
        var log = new NullLogSink();
        
        var projection = new VoyageCompletionProjection(
            cache,
            queue,
            settings,
            characterRegistry,
            log
        );

        // Act: Underway状態のスナップショット
        var underwaySnapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Underway,
            arrival: DateTime.UtcNow.AddHours(12)
        );
        cache.Update(underwaySnapshot, 1);

        // Assert: Underway通知は送信される
        Assert.NotEmpty(queue.EnqueuedNotifications);
    }

    [Fact]
    public void CompletedThenUnderway_OnlyUnderwayNotificationSent()
    {
        // Arrange
        var settings = new NotificationSettings
        {
            NotifyVoyageCompleted = true,
            NotifyVoyageUnderway = true
        };
        var queue = new TestNotificationQueue();
        var cache = new SnapshotCache();
        var characterRegistry = new TestCharacterRegistry();
        var log = new NullLogSink();

        var projection = new VoyageCompletionProjection(
            cache,
            queue,
            settings,
            characterRegistry,
            log
        );

        // 初期: Underway
        var initialSnapshot = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(initialSnapshot, 1);
        queue.Clear();

        // Act 1: Completed状態に変更
        var completedSnapshot = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Completed, DateTime.UtcNow);
        cache.Update(completedSnapshot, 1);

        // Assert 1: Completed通知は送信されない
        Assert.Empty(queue.EnqueuedNotifications);

        // Act 2: 次のUnderway状態
        var nextUnderwaySnapshot = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.Update(nextUnderwaySnapshot, 1);

        // Assert 2: Underway通知のみ送信される
        Assert.Single(queue.EnqueuedNotifications);
    }

    // ヘルパー
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
            status
        );

        var submarine = new Submarine(
            submarineId,
            $"Sub-{submarineId.Slot}",
            "profile-1",
            new[] { voyage }
        );

        return new AcquisitionSnapshot(
            DateTime.UtcNow,
            new[] { submarine },
            AcquisitionSourceKind.Composite,
            characterId,
            $"Character-{characterId}",
            $"World-{characterId}",
            SnapshotConfidence.Merged
        );
    }

    // テスト用キュー実装
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

    // テスト用キャラクターレジストリ
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

