namespace XIVSubmarinesRewrite.Tests;

using System;
using System.Collections.Generic;
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
        cache.UpdateSnapshot(1, initialSnapshot);

        // Act: Completed状態に変更
        var completedSnapshot = CreateSnapshot(
            characterId: 1,
            submarineId: new SubmarineId(1, 1),
            status: VoyageStatus.Completed,
            arrival: DateTime.UtcNow
        );
        cache.UpdateSnapshot(1, completedSnapshot);

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
        cache.UpdateSnapshot(1, underwaySnapshot);

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
        cache.UpdateSnapshot(1, initialSnapshot);
        queue.Clear();

        // Act 1: Completed状態に変更
        var completedSnapshot = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Completed, DateTime.UtcNow);
        cache.UpdateSnapshot(1, completedSnapshot);

        // Assert 1: Completed通知は送信されない
        Assert.Empty(queue.EnqueuedNotifications);

        // Act 2: 次のUnderway状態
        var nextUnderwaySnapshot = CreateSnapshot(1, new SubmarineId(1, 1), VoyageStatus.Underway, DateTime.UtcNow.AddHours(12));
        cache.UpdateSnapshot(1, nextUnderwaySnapshot);

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
        var voyage = new Voyage(
            new VoyageId(submarineId, 1),
            status,
            DateTime.UtcNow.AddHours(-12),
            arrival,
            "Route-1"
        );

        var submarine = new Submarine(
            submarineId,
            $"Sub-{submarineId.SlotIndex}",
            new[] { voyage }
        );

        return new AcquisitionSnapshot(
            characterId,
            $"Character-{characterId}",
            $"World-{characterId}",
            new[] { submarine },
            AcquisitionConfidence.High,
            DateTime.UtcNow
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

        public IReadOnlyList<NotificationWorkItemSnapshot> GetPendingSnapshots() => 
            Array.Empty<NotificationWorkItemSnapshot>();

        public IReadOnlyList<NotificationWorkItemSnapshot> GetDeadLetterSnapshots() => 
            Array.Empty<NotificationWorkItemSnapshot>();

        public bool TryRequeue(string hashKey) => false;
    }

    // テスト用キャラクターレジストリ
    private class TestCharacterRegistry : ICharacterRegistry
    {
        public ulong ActiveCharacterId => 1;

        public CharacterIdentity? GetIdentity(ulong characterId) => 
            new CharacterIdentity(characterId, $"Character-{characterId}", $"World-{characterId}");

        public void RegisterSnapshot(AcquisitionSnapshot snapshot) { }

        public IReadOnlyList<CharacterIdentity> GetAllIdentities() => 
            Array.Empty<CharacterIdentity>();

        public IReadOnlyList<ulong> GetCharactersWithSubmarineOperations() => 
            Array.Empty<ulong>();

        public void CleanupCharactersWithoutSubmarineOperations() { }
    }
}

