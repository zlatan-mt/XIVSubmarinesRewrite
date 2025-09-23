// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/VoyageCompletionProjectionTests.cs
// VoyageCompletionProjection の通知バッファ挙動を検証するユニットテストです
// ForceNotify を使わない通常フローで重複通知が発生しないことを確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

public sealed class VoyageCompletionProjectionTests
{
    private static readonly SubmarineId[] SubmarineIds =
    {
        new SubmarineId(12345UL, 1),
        new SubmarineId(12345UL, 2),
        new SubmarineId(12345UL, 3),
        new SubmarineId(12345UL, 4),
    };

    [Fact]
    public void FlushesBufferedNotificationsOncePerSubmarine()
    {
        var cache = new SnapshotCache();
        var queue = new TestNotificationQueue();
        var settings = new NotificationSettings();
        var registry = new TestCharacterRegistry();
        var log = new TestLogSink();
        var characterId = 12345UL;

        cache.Update(CreateSnapshot(characterId, VoyageStatus.Underway, utcOffsetHours: 6), characterId);

        using var projection = new VoyageCompletionProjection(cache, queue, settings, registry, log);

        cache.Update(CreateSnapshot(characterId, VoyageStatus.Completed, utcOffsetHours: -1), characterId);
        Assert.Empty(queue.Envelopes);

        cache.Update(CreateSnapshot(characterId, VoyageStatus.Underway, utcOffsetHours: 10), characterId);

        Assert.Equal(SubmarineIds.Length, queue.Envelopes.Count);
        Assert.Equal(SubmarineIds.Length, queue.Envelopes.Select(e => e.SubmarineId).Distinct().Count());
    }

    [Fact]
    public void SkipsCompletedNotificationWhenDisabled()
    {
        var cache = new SnapshotCache();
        var queue = new TestNotificationQueue();
        var settings = new NotificationSettings
        {
            NotifyVoyageCompleted = false,
        };
        var registry = new TestCharacterRegistry();
        var log = new TestLogSink();
        var characterId = 12345UL;

        cache.Update(CreateSnapshot(characterId, VoyageStatus.Underway, utcOffsetHours: 4), characterId);

        using var projection = new VoyageCompletionProjection(cache, queue, settings, registry, log);

        cache.Update(CreateSnapshot(characterId, VoyageStatus.Completed, utcOffsetHours: 0), characterId);

        Assert.Empty(queue.Envelopes);

        settings.NotifyVoyageCompleted = true;
        cache.Update(CreateSnapshot(characterId, VoyageStatus.Underway, utcOffsetHours: 6), characterId);
        cache.Update(CreateSnapshot(characterId, VoyageStatus.Completed, utcOffsetHours: 1), characterId);

        Assert.Equal(SubmarineIds.Length, queue.Envelopes.Count);
    }

    private static AcquisitionSnapshot CreateSnapshot(ulong characterId, VoyageStatus status, int utcOffsetHours)
    {
        var now = DateTime.UtcNow.AddHours(utcOffsetHours);
        var list = new List<Submarine>();
        foreach (var id in SubmarineIds)
        {
            var departure = now.AddHours(-12);
            var arrival = status == VoyageStatus.Underway ? now.AddHours(12) : now;
            var voyage = new Voyage(VoyageId.Create(id, Guid.NewGuid()), "O-J", departure, arrival, status);
            list.Add(new Submarine(id, "Sub-" + id.Slot, string.Empty, new[] { voyage }));
        }

        return new AcquisitionSnapshot(DateTime.UtcNow, list, AcquisitionSourceKind.Memory, characterId, "Tester", "World", SnapshotConfidence.Direct);
    }

    private sealed class TestNotificationQueue : INotificationQueue
    {
        public List<NotificationEnvelope> Envelopes { get; } = new ();

        public IReadOnlyCollection<NotificationWorkItemSnapshot> GetDeadLetters() => Array.Empty<NotificationWorkItemSnapshot>();
        public IReadOnlyCollection<NotificationWorkItemSnapshot> GetPending() => Array.Empty<NotificationWorkItemSnapshot>();
        public ValueTask<NotificationWorkItem?> DequeueAsync(CancellationToken cancellationToken) => ValueTask.FromResult<NotificationWorkItem?>(null);
        public NotificationDeliveryState ReportFailure(NotificationWorkItem item, Exception error, TimeSpan? overrideDelay = null) => NotificationDeliveryState.DeadLetter;
        public void ReportSuccess(NotificationWorkItem item) { }
        public bool TryEnqueue(NotificationEnvelope envelope, bool forceDuplicate = false)
        {
            this.Envelopes.Add(envelope);
            return true;
        }
        public bool TryRequeueDeadLetter(string hashKey) => false;
    }

    private sealed class TestCharacterRegistry : ICharacterRegistry
    {
        public event EventHandler<CharacterChangedEventArgs>? ActiveCharacterChanged { add { } remove { } }
        public event EventHandler? CharacterListChanged { add { } remove { } }
        public ulong ActiveCharacterId => 0;
        public IReadOnlyList<CharacterDescriptor> Characters => Array.Empty<CharacterDescriptor>();
        public void RegisterSnapshot(AcquisitionSnapshot snapshot) { }
        public void SelectCharacter(ulong characterId) { }
        public CharacterIdentity? GetIdentity(ulong characterId) => new CharacterIdentity(characterId, "Tester", "World");
        public DateTime? GetLastUpdatedUtc(ulong characterId) => DateTime.UtcNow;
    }

    private sealed class TestLogSink : ILogSink
    {
        public List<(LogLevel Level, string Message)> Entries { get; } = new ();
        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            Entries.Add((level, message));
        }
    }
}
