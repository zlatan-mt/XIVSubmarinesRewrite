// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/Program.cs
// DiscordCycleNotificationAggregator を CLI から再生するエントリーポイントです
// 集約判定を自動化し、検証用ログと JSON を出力するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordCycleNotificationAggregator.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var options = SimulationOptions.Parse(args);
        using var log = new SimulationFileLogSink(options.LogFilePath);
        var runner = new DiscordCycleSimulationRunner(options, log);
        await runner.RunAsync().ConfigureAwait(false);
        return 0;
    }
}

internal sealed record SimulationOptions(
    string OutputRoot,
    string RunName,
    int CycleCount,
    TimeSpan CycleSpacing,
    ulong CharacterId,
    DateTime BaseDepartureUtc)
{
    public static SimulationOptions Parse(string[] args)
    {
        var nowUtc = DateTime.UtcNow;
        var outputRoot = Path.Combine("logs", nowUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), "notification-cycle");
        var runName = "cli-" + nowUtc.ToString("HHmmss", CultureInfo.InvariantCulture);
        var cycleCount = 2;
        var cycleSpacing = TimeSpan.FromMinutes(12);
        ulong characterId = 0xA11CEUL;

        foreach (var arg in args)
        {
            if (arg.StartsWith("--output=", StringComparison.Ordinal))
            {
                outputRoot = arg[9..];
            }
            else if (arg.StartsWith("--run=", StringComparison.Ordinal))
            {
                runName = arg[6..];
            }
            else if (arg.StartsWith("--cycles=", StringComparison.Ordinal) && int.TryParse(arg[9..], out var parsedCycles) && parsedCycles > 0)
            {
                cycleCount = parsedCycles;
            }
            else if (arg.StartsWith("--spacing-min=", StringComparison.Ordinal) && double.TryParse(arg[14..], NumberStyles.Float, CultureInfo.InvariantCulture, out var minutes) && minutes > 0)
            {
                cycleSpacing = TimeSpan.FromMinutes(minutes);
            }
            else if (arg.StartsWith("--character=", StringComparison.Ordinal) && ulong.TryParse(arg[12..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var parsedCharacter))
            {
                characterId = parsedCharacter;
            }
        }

        var runRoot = Path.Combine(outputRoot, runName);
        Directory.CreateDirectory(runRoot);
        return new SimulationOptions(outputRoot, runName, cycleCount, cycleSpacing, characterId, nowUtc.AddHours(-42));
    }

    public string RunRoot => Path.Combine(this.OutputRoot, this.RunName);

    public string DiscordFolder => Path.Combine(this.RunRoot, "discord");

    public string NotionFolder => Path.Combine(this.RunRoot, "notion");

    public string MetadataFolder => Path.Combine(this.RunRoot, "meta");

    public string LogFilePath => Path.Combine(this.RunRoot, "simulation.log");
}

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

    public Task RunAsync()
    {
        Directory.CreateDirectory(this.options.DiscordFolder);
        Directory.CreateDirectory(this.options.NotionFolder);
        Directory.CreateDirectory(this.options.MetadataFolder);

        var aggregator = new DiscordCycleNotificationAggregator(this.formatter, this.log);
        var manifest = new SimulationManifest(
            this.options.RunName,
            DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture),
            new List<SimulationCycleSummary>());

        var baseDeparture = this.options.BaseDepartureUtc;
        for (var index = 0; index < this.options.CycleCount; index++)
        {
            var referenceUtc = baseDeparture.AddHours(index * this.options.CycleSpacing.TotalHours);
            var result = this.ExecuteCycle(aggregator, index + 1, referenceUtc);
            manifest.Cycles.Add(new SimulationCycleSummary(
                Cycle: result.CycleNumber,
                DiscordPayloadPath: result.DiscordPayloadPath,
                NotionPayloadPath: result.NotionPayloadPath,
                ScreenshotPath: result.ScreenshotPath));
        }

        var manifestPath = Path.Combine(this.options.MetadataFolder, "manifest.json");
        var manifestJson = JsonSerializer.Serialize(manifest, this.serializerOptions);
        File.WriteAllText(manifestPath, manifestJson);
        this.log.Log(LogLevel.Information, $"[Simulator] Wrote manifest to {manifestPath}");
        return Task.CompletedTask;
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

        if (finalDecision is null || finalDecision.Value.Aggregate is null)
        {
            throw new InvalidOperationException("Discord aggregator did not flush after a full cycle simulation.");
        }

        var aggregate = finalDecision.Value.Aggregate;
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

        var label = status == VoyageStatus.Completed
            ? $"Slot {slot + 1}"
            : $"Slot {slot + 1}";

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

internal sealed record SimulationCycleResult(
    int CycleNumber,
    string DiscordPayloadPath,
    string NotionPayloadPath,
    string ScreenshotPath);

internal sealed record SimulationManifest(
    string Run,
    string GeneratedAtUtc,
    List<SimulationCycleSummary> Cycles);

internal sealed record SimulationCycleSummary(
    int Cycle,
    string DiscordPayloadPath,
    string NotionPayloadPath,
    string ScreenshotPath);

internal sealed class SimulationFileLogSink : ILogSink, IDisposable
{
    private readonly StreamWriter writer;
    private readonly object gate = new ();

    public SimulationFileLogSink(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        this.writer = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read))
        {
            AutoFlush = true,
        };
    }

    public void Log(LogLevel level, string message, Exception? exception = null)
    {
        var line = $"{DateTime.UtcNow:O}\t{level}\t{message}";
        if (exception is not null)
        {
            line += "\t" + exception;
        }

        lock (this.gate)
        {
            this.writer.WriteLine(line);
        }

        Console.WriteLine(line);
    }

    public void Dispose()
    {
        this.writer.Dispose();
    }
}

internal static class PlaceholderPng
{
    private static readonly byte[] Content =
    {
        0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
        0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
        0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
        0x08, 0x02, 0x00, 0x00, 0x00, 0x90, 0x77, 0x53,
        0xDE, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41,
        0x54, 0x78, 0x9C, 0x63, 0x60, 0x00, 0x00, 0x00,
        0x02, 0x00, 0x01, 0xE2, 0x21, 0xBC, 0x33, 0x00,
        0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE,
        0x42, 0x60, 0x82,
    };

    public static ReadOnlySpan<byte> Value => Content;
}
