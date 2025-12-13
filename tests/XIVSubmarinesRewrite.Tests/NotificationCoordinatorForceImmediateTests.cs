// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/NotificationCoordinatorForceImmediateTests.cs
// NotificationCoordinator の ForceImmediate 動作を検証するユニットテストです
// ForceNotify 時に Discord が単体通知と一括通知を重複送信しないことを保証するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs

// NOTE: These tests require Dalamud runtime (RouteCatalog depends on IDataManager).
// Enable by defining DALAMUD_TEST when running in a Dalamud environment.
#if DALAMUD_TEST

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Infrastructure.Routes;
using XIVSubmarinesRewrite.Integrations.Notifications;
using Xunit;

public sealed class NotificationCoordinatorForceImmediateTests
{
    [Fact]
    public async Task ForceImmediateUnderway_SendsOnlyAggregatedDiscordMessage()
    {
        var log = new TestLogSink();
        var discord = new RecordingDiscordClient();
        var formatter = new VoyageNotificationFormatter(new Infrastructure.Configuration.NotificationSettings());
        using var batcher = new DiscordNotificationBatcher(discord, formatter, log, TimeSpan.FromSeconds(60));
        var routeCatalog = new RouteCatalog(null, log);
        var coordinator = new NotificationCoordinator(discord, formatter, routeCatalog, batcher, log);

        var characterId = 0xBEEFUL;
        var baseArrival = DateTime.UtcNow;

        // まず 4 隻分の Completed を処理してサイクルを有効化
        for (byte slot = 0; slot < 4; slot++)
        {
            var completed = BuildEnvelope(characterId, slot, VoyageStatus.Completed, baseArrival.AddMinutes(slot * -5), baseArrival.AddMinutes(slot), forceImmediate: false);
            await coordinator.PublishVoyageCompletionAsync(completed);
        }

        discord.Clear();

        // ForceNotifyUnderway 相当: ForceImmediate で 4 隻ぶんの Underway を送る
        for (byte slot = 0; slot < 4; slot++)
        {
            var underway = BuildEnvelope(characterId, slot, VoyageStatus.Underway, baseArrival, baseArrival.AddMinutes(30 + slot), forceImmediate: true);
            await coordinator.PublishVoyageCompletionAsync(underway);
        }

        Assert.Empty(discord.Singles);
        var batch = Assert.Single(discord.Batches);
    }

    [Fact]
    public async Task ForceImmediateUnderway_PrimesAggregateWithoutCompletedCycle()
    {
        var log = new TestLogSink();
        var discord = new RecordingDiscordClient();
        var formatter = new VoyageNotificationFormatter(new Infrastructure.Configuration.NotificationSettings());
        using var batcher = new DiscordNotificationBatcher(discord, formatter, log, TimeSpan.FromSeconds(60));
        var routeCatalog = new RouteCatalog(null, log);
        var coordinator = new NotificationCoordinator(discord, formatter, routeCatalog, batcher, log);

        var characterId = 0xCAFE_BABEUL;
        var baseArrival = DateTime.UtcNow;

        for (byte slot = 0; slot < 4; slot++)
        {
            var underway = BuildEnvelope(characterId, slot, VoyageStatus.Underway, baseArrival, baseArrival.AddMinutes(slot), forceImmediate: true);
            await coordinator.PublishVoyageCompletionAsync(underway);
        }

        Assert.Empty(discord.Singles);
        var batch = Assert.Single(discord.Batches);
    }

    private static NotificationEnvelope BuildEnvelope(ulong characterId, byte slot, VoyageStatus status, DateTime departureUtc, DateTime arrivalUtc, bool forceImmediate)
    {
        var submarineId = new SubmarineId(characterId, slot);
        var voyageId = VoyageId.Create(submarineId, Guid.NewGuid());
        var departure = status == VoyageStatus.Underway ? departureUtc : arrivalUtc.AddHours(-6);

        return NotificationEnvelope.Create(
            characterId,
            $"Character-{characterId:X}",
            "World",
            submarineId,
            $"Sub-{slot}",
            "R-123",
            voyageId,
            departure,
            arrivalUtc,
            status,
            SnapshotConfidence.Merged,
            forceImmediate);
    }

    private sealed class RecordingDiscordClient : IDiscordClient
    {
        public List<SinglePayload> Singles { get; } = new ();

        public List<BatchPayload> Batches { get; } = new ();

        public ValueTask SendAlarmAsync(Alarm alarm, CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;

        public ValueTask SendVoyageCompletionAsync(VoyageNotification notification, DiscordNotificationPayload payload, CancellationToken cancellationToken = default)
        {
            this.Singles.Add(new SinglePayload(notification, payload));
            return ValueTask.CompletedTask;
        }

        public ValueTask SendVoyageBatchAsync(string username, DiscordNotificationPayload payload, DateTime timestampUtc, CancellationToken cancellationToken = default)
        {
            this.Batches.Add(new BatchPayload(username, payload, timestampUtc));
            return ValueTask.CompletedTask;
        }

        public void Clear()
        {
            this.Singles.Clear();
            this.Batches.Clear();
        }

        public readonly record struct SinglePayload(VoyageNotification Notification, DiscordNotificationPayload Payload);

        public readonly record struct BatchPayload(string Username, DiscordNotificationPayload Payload, DateTime TimestampUtc);
    }

    private sealed class TestLogSink : ILogSink
    {
        public List<LogEntry> Entries { get; } = new ();

        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            this.Entries.Add(new LogEntry(level, message, exception));
        }

        public readonly record struct LogEntry(LogLevel Level, string Message, Exception? Exception);
    }
}

#endif
