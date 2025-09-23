// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/ForceNotifyUnderwayTests.cs
// VoyageCompletionProjection の ForceNotifyUnderway 挙動を集中的に検証するユニットテストです
// クールダウンや到着時刻変更時の再通知が正しく働くことを確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs

using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

public sealed class ForceNotifyUnderwayTests
{
    private static readonly SubmarineId DefaultSubmarineId = new (12345UL, 1);

    [Fact]
    public void FirstUnderwaySnapshotEnqueuesNotification()
    {
        using var context = this.CreateProjection();
        var clock = context.Clock;
        var cache = context.Cache;
        var queue = context.Queue;

        var arrival = clock.GetUtcNow().UtcDateTime.AddHours(6);
        clock.Advance(TimeSpan.FromMinutes(1));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrival), DefaultSubmarineId.CharacterId);

        Assert.Single(queue.Envelopes);
    }

    [Fact]
    public void CooldownPreventsImmediateDuplicate()
    {
        using var context = this.CreateProjection();
        var clock = context.Clock;
        var cache = context.Cache;
        var queue = context.Queue;

        var arrival = clock.GetUtcNow().UtcDateTime.AddHours(6);
        clock.Advance(TimeSpan.FromMinutes(1));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrival), DefaultSubmarineId.CharacterId);
        Assert.Single(queue.Envelopes);

        clock.Advance(TimeSpan.FromMinutes(5));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrival), DefaultSubmarineId.CharacterId);

        Assert.Single(queue.Envelopes);
    }

    [Fact]
    public void ArrivalChangeBypassesCooldown()
    {
        using var context = this.CreateProjection();
        var clock = context.Clock;
        var cache = context.Cache;
        var queue = context.Queue;

        var arrival = clock.GetUtcNow().UtcDateTime.AddHours(4);
        clock.Advance(TimeSpan.FromMinutes(1));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrival), DefaultSubmarineId.CharacterId);
        Assert.Single(queue.Envelopes);

        var arrivalChanged = arrival.AddHours(-2);
        clock.Advance(TimeSpan.FromMinutes(2));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrivalChanged), DefaultSubmarineId.CharacterId);

        Assert.Collection(queue.Envelopes,
            _ => { },
            _ => { });
    }

    [Fact]
    public void CooldownExpiryAllowsResend()
    {
        using var context = this.CreateProjection();
        var clock = context.Clock;
        var cache = context.Cache;
        var queue = context.Queue;

        var arrival = clock.GetUtcNow().UtcDateTime.AddHours(3);
        clock.Advance(TimeSpan.FromMinutes(1));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrival), DefaultSubmarineId.CharacterId);
        Assert.Single(queue.Envelopes);

        clock.Advance(TimeSpan.FromMinutes(31));
        cache.Update(this.CreateUnderwaySnapshot(clock, arrival), DefaultSubmarineId.CharacterId);

        Assert.Collection(queue.Envelopes,
            _ => { },
            _ => { });
    }

    private ProjectionContext CreateProjection()
    {
        var clock = new ManualTimeProvider(DateTimeOffset.Parse("2025-09-23T00:00:00Z"));
        var cache = new SnapshotCache();
        var queue = new RecordingQueue();
        var settings = new NotificationSettings
        {
            ForceNotifyUnderway = true,
        };
        var registry = new TestCharacterRegistry();
        var log = new TestLogSink();
        var characterId = DefaultSubmarineId.CharacterId;

        var initialArrival = clock.GetUtcNow().UtcDateTime.AddHours(8);
        cache.Update(this.CreateUnderwaySnapshot(clock, initialArrival), characterId);
        var projection = new VoyageCompletionProjection(cache, queue, settings, registry, log, clock);
        return new ProjectionContext(projection, cache, queue, clock);
    }

    private AcquisitionSnapshot CreateUnderwaySnapshot(ManualTimeProvider clock, DateTime arrivalUtc)
    {
        var nowUtc = clock.GetUtcNow().UtcDateTime;
        var departureUtc = arrivalUtc.AddHours(-12);
        var voyage = new Voyage(VoyageId.Create(DefaultSubmarineId, Guid.NewGuid()), "1-2-3", departureUtc, arrivalUtc, VoyageStatus.Underway);
        var submarine = new Submarine(DefaultSubmarineId, "Alpha", string.Empty, new[] { voyage });
        return new AcquisitionSnapshot(nowUtc, new[] { submarine }, AcquisitionSourceKind.Memory, DefaultSubmarineId.CharacterId, "Tester", "Chocobo", SnapshotConfidence.Direct);
    }

    private sealed class RecordingQueue : INotificationQueue
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

    private sealed class ManualTimeProvider : TimeProvider
    {
        private DateTimeOffset current;

        public ManualTimeProvider(DateTimeOffset start)
        {
            this.current = start;
        }

        public override DateTimeOffset GetUtcNow() => this.current;

        public void Advance(TimeSpan delta) => this.current += delta;
    }

    private sealed class TestCharacterRegistry : ICharacterRegistry
    {
        public event EventHandler<CharacterChangedEventArgs>? ActiveCharacterChanged { add { } remove { } }
        public event EventHandler? CharacterListChanged { add { } remove { } }
        public ulong ActiveCharacterId => 0;
        public IReadOnlyList<CharacterDescriptor> Characters => Array.Empty<CharacterDescriptor>();
        public void RegisterSnapshot(AcquisitionSnapshot snapshot) { }
        public void SelectCharacter(ulong characterId) { }
        public CharacterIdentity? GetIdentity(ulong characterId) => new CharacterIdentity(characterId, "Tester", "Chocobo");
        public DateTime? GetLastUpdatedUtc(ulong characterId) => DateTime.UtcNow;
    }

    private sealed class TestLogSink : ILogSink
    {
        public void Log(LogLevel level, string message, Exception? exception = null)
        {
        }
    }

    private sealed record ProjectionContext(
        VoyageCompletionProjection Projection,
        SnapshotCache Cache,
        RecordingQueue Queue,
        ManualTimeProvider Clock) : IDisposable
    {
        public void Dispose()
        {
            this.Projection.Dispose();
        }
    }
}
