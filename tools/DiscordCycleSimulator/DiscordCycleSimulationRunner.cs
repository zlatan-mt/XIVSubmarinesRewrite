// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/DiscordCycleSimulationRunner.cs
// DiscordCycleNotificationAggregator を用いたシミュレーション実行ロジックをまとめます
// CLI から固定シナリオを再生し、各サイクルごとの計測値を取得するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationOptions.cs, apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationRecords.cs

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

internal sealed class DiscordCycleSimulationRunner
{
    private readonly SimulationOptions options;
    private readonly SimulationFileLogSink log;
    private readonly VoyageNotificationFormatter formatter = new ();
    private readonly JsonSerializerOptions serializerOptions = new ()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public DiscordCycleSimulationRunner(SimulationOptions options, SimulationFileLogSink log)
    {
        this.options = options;
        this.log = log;
    }

    public Task<SimulationRun> RunAsync()
    {
        Directory.CreateDirectory(this.options.DiscordFolder);
        Directory.CreateDirectory(this.options.NotionFolder);
        Directory.CreateDirectory(this.options.MetadataFolder);

        var aggregator = new DiscordCycleNotificationAggregator(this.formatter, this.log);
        var cycleResults = new List<SimulationCycleResult>(this.options.CycleCount);

        var baseDeparture = this.options.BaseDepartureUtc;
        for (var index = 0; index < this.options.CycleCount; index++)
        {
            var referenceUtc = baseDeparture.AddHours(index * this.options.CycleSpacing.TotalHours);
            var result = this.ExecuteCycle(aggregator, index + 1, referenceUtc);
            cycleResults.Add(result);
        }

        return Task.FromResult(new SimulationRun(cycleResults));
    }

    private SimulationCycleResult ExecuteCycle(DiscordCycleNotificationAggregator aggregator, int cycleNumber, DateTime departureAnchorUtc)
    {
        var completions = new List<VoyageNotification>(4);
        var underways = new List<VoyageNotification>(4);

        for (byte slot = 0; slot < 4; slot++)
        {
            var completion = this.CreateNotification(slot, VoyageStatus.Completed, departureAnchorUtc.AddHours(slot * 0.3));
            completions.Add(completion);
            var decision = aggregator.Process(completion);
            this.log.Log(LogLevel.Debug, $"[Simulator] Completion slot={slot} suppressed={decision.IsSuppressed}");
        }

        DiscordCycleNotificationAggregator.Decision? finalDecision = null;
        for (byte slot = 0; slot < 4; slot++)
        {
            var underway = this.CreateNotification(slot, VoyageStatus.Underway, departureAnchorUtc.AddHours(6 + slot * 0.2));
            underways.Add(underway);
            var decision = aggregator.Process(underway);
            this.log.Log(LogLevel.Debug, $"[Simulator] Underway slot={slot} suppressed={decision.IsSuppressed}");
            finalDecision = decision;
        }

        if (finalDecision is null)
        {
            throw new InvalidOperationException("Discord aggregator did not flush after a full cycle simulation.");
        }

        var aggregatedNotification = finalDecision.Value.Aggregate;
        if (aggregatedNotification is null)
        {
            throw new InvalidOperationException("Discord aggregator returned null aggregate payload.");
        }

        var aggregate = aggregatedNotification;
        var discordJson = JsonSerializer.Serialize(aggregate.Payload, this.serializerOptions);
        var notionPayloads = underways.Select(this.formatter.CreateNotionPayload).ToList();
        var notionJson = JsonSerializer.Serialize(notionPayloads, this.serializerOptions);

        var discordPath = Path.Combine(this.options.DiscordFolder, $"cycle-{cycleNumber:D2}.json");
        var notionPath = Path.Combine(this.options.NotionFolder, $"cycle-{cycleNumber:D2}.json");
        File.WriteAllText(discordPath, discordJson);
        File.WriteAllText(notionPath, notionJson);

        var screenshotPath = Path.Combine(this.options.DiscordFolder, $"cycle-{cycleNumber:D2}.placeholder.png");
        File.WriteAllBytes(screenshotPath, PlaceholderPng.Value);

        this.log.Log(LogLevel.Information, $"[Simulator] Cycle {cycleNumber} flushed to {discordPath}");

        return new SimulationCycleResult(
            CycleNumber: cycleNumber,
            CompletionCount: completions.Count,
            UnderwayCount: underways.Count,
            NotionPayloadCount: notionPayloads.Count,
            DiscordFieldCount: aggregate.Payload.Fields.Count,
            Aggregated: true,
            CharacterLabel: aggregate.CharacterLabel,
            AggregateTimestampUtc: aggregate.TimestampUtc,
            DiscordPayloadPath: discordPath,
            NotionPayloadPath: notionPath,
            ScreenshotPath: screenshotPath);
    }

    private VoyageNotification CreateNotification(byte slot, VoyageStatus status, DateTime arrivalUtc)
    {
        var submarineId = new SubmarineId(this.options.CharacterId, slot);
        var routeId = "R-CLISIM";
        var departureUtc = status == VoyageStatus.Completed
            ? arrivalUtc.AddHours(-6)
            : arrivalUtc.AddHours(-42);

        var label = $"Slot {slot + 1}";

        return new VoyageNotification(
            CharacterId: this.options.CharacterId,
            CharacterLabel: $"Character-{this.options.CharacterId:X}",
            CharacterName: $"Character-{this.options.CharacterId:X}",
            WorldName: "Simulator",
            SubmarineId: submarineId,
            SubmarineLabel: label,
            SubmarineName: label,
            RouteId: routeId,
            RouteDisplay: routeId,
            VoyageId: VoyageId.Create(submarineId, Guid.NewGuid()),
            DepartureUtc: departureUtc,
            ArrivalUtc: arrivalUtc,
            ArrivalLocal: arrivalUtc.ToLocalTime(),
            Duration: status == VoyageStatus.Completed ? arrivalUtc - departureUtc : null,
            Status: status,
            Confidence: SnapshotConfidence.Merged,
            HashKey: $"sim-{status}-{slot}-{arrivalUtc:yyyyMMddHHmmss}",
            HashKeyShort: $"sim{slot}{(int)status}");
    }
}
