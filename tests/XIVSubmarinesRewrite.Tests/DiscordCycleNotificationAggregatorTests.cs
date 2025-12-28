// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/DiscordCycleNotificationAggregatorTests.cs
// DiscordCycleNotificationAggregator の振る舞いを検証するユニットテストです
// Discord 通知集約の回帰を防ぎ、実装の安全確認を自動化するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordCycleNotificationAggregator.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs

using System;
using System.Collections.Generic;
using System.Linq;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using Xunit;

public sealed class DiscordCycleNotificationAggregatorTests
{
    [Fact]
    public void Process_FlushesSingleDiscordBatchAfterCompleteCycle()
    {
        var log = new FakeLogSink();
        var formatter = new VoyageNotificationFormatter(new NotificationSettings());
        var aggregator = new DiscordCycleNotificationAggregator(formatter, log);
        var characterId = 0xDEADBEEF;
        var baseArrival = DateTime.UtcNow;

        for (byte slot = 0; slot < 4; slot++)
        {
            var completed = CreateNotification(characterId, slot, VoyageStatus.Completed, baseArrival.AddMinutes(slot * -5));
            var decision = aggregator.Process(completed);
            Assert.True(decision.IsSuppressed);
            Assert.Null(decision.Aggregate);
        }

        DiscordCycleNotificationAggregator.Decision? finalDecision = null;
        for (byte slot = 0; slot < 4; slot++)
        {
            var underway = CreateNotification(characterId, slot, VoyageStatus.Underway, baseArrival.AddMinutes(slot * 10));
            var decision = aggregator.Process(underway);
            if (slot < 3)
            {
                Assert.True(decision.IsSuppressed);
                Assert.Null(decision.Aggregate);
            }
            else
            {
                finalDecision = decision;
            }
        }

        Assert.NotNull(finalDecision);
        Assert.False(finalDecision.Value.IsSuppressed);
        Assert.NotNull(finalDecision.Value.Aggregate);
        var aggregate = finalDecision.Value.Aggregate!;
        Assert.Equal("Character-DEADBEEF", aggregate.CharacterLabel);
        // Phase 13: リマインダー無効時はFieldsは空、Descriptionにバッチ一覧
        Assert.Empty(aggregate.Payload.Fields);
        Assert.NotEmpty(aggregate.Payload.Description);

        var debugLogs = log.Entries.Where(e => e.Level == LogLevel.Debug).ToList();
        Assert.Contains(debugLogs, entry => entry.Message.Contains("flushing cycle", StringComparison.Ordinal));
    }

    [Fact]
    public void Process_AllowsSecondCycleAfterReset()
    {
        var log = new FakeLogSink();
        var formatter = new VoyageNotificationFormatter(new NotificationSettings());
        var aggregator = new DiscordCycleNotificationAggregator(formatter, log);
        var characterId = 0xC0FFEEUL;
        var firstCycleDecision = RunFullCycle(aggregator, characterId, DateTime.UtcNow);

        Assert.False(firstCycleDecision.IsSuppressed);
        Assert.NotNull(firstCycleDecision.Aggregate);

        var secondCycleDecision = RunFullCycle(aggregator, characterId, DateTime.UtcNow.AddHours(6));

        Assert.False(secondCycleDecision.IsSuppressed);
        Assert.NotNull(secondCycleDecision.Aggregate);

        var flushLogs = log.Entries.Count(entry => entry.Level == LogLevel.Debug && entry.Message.Contains("flushing cycle", StringComparison.Ordinal));
        Assert.Equal(2, flushLogs);
    }

    private static DiscordCycleNotificationAggregator.Decision RunFullCycle(DiscordCycleNotificationAggregator aggregator, ulong characterId, DateTime baseArrival)
    {
        for (byte slot = 0; slot < 4; slot++)
        {
            var completed = CreateNotification(characterId, slot, VoyageStatus.Completed, baseArrival.AddMinutes(slot * -3));
            aggregator.Process(completed);
        }

        DiscordCycleNotificationAggregator.Decision? decision = null;
        for (byte slot = 0; slot < 4; slot++)
        {
            var underway = CreateNotification(characterId, slot, VoyageStatus.Underway, baseArrival.AddMinutes(slot * 15));
            decision = aggregator.Process(underway);
        }

        return decision!.Value;
    }

    private static VoyageNotification CreateNotification(ulong characterId, byte slot, VoyageStatus status, DateTime arrivalUtc)
    {
        var submarineId = new SubmarineId(characterId, slot);
        var hash = $"hash-{characterId}-{slot}-{status}";
        var departureUtc = status == VoyageStatus.Completed ? arrivalUtc.AddHours(-6) : arrivalUtc.AddHours(-42);

        return new VoyageNotification(
            CharacterId: characterId,
            CharacterLabel: $"Character-{characterId:X}",
            CharacterName: $"Character-{characterId:X}",
            WorldName: "World",
            SubmarineId: submarineId,
            SubmarineLabel: $"Sub-{slot}",
            SubmarineName: $"Sub-{slot}",
            RouteId: "R-123",
            RouteDisplay: "R-123",
            VoyageId: VoyageId.Create(submarineId, Guid.NewGuid()),
            DepartureUtc: departureUtc,
            ArrivalUtc: arrivalUtc,
            ArrivalLocal: arrivalUtc.ToLocalTime(),
            Duration: status == VoyageStatus.Completed ? arrivalUtc - departureUtc : null,
            Status: status,
            Confidence: SnapshotConfidence.Merged,
            HashKey: hash,
            HashKeyShort: hash.Length > 8 ? hash[..8] : hash);
    }

    private sealed class FakeLogSink : ILogSink
    {
        public List<LogEntry> Entries { get; } = new ();

        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            this.Entries.Add(new LogEntry(level, message, exception));
        }

        public readonly record struct LogEntry(LogLevel Level, string Message, Exception? Exception);
    }
}
