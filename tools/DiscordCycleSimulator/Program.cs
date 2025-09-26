// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/Program.cs
// DiscordCycleNotificationAggregator を CLI から再生するエントリーポイントです
// 集約判定を自動化し、検証用ログと JSON を出力するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordCycleNotificationAggregator.cs, apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationReportWriter.cs

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using XIVSubmarinesRewrite.Infrastructure.Logging;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var options = SimulationOptions.Parse(args);
        using var log = new SimulationFileLogSink(options.LogFilePath);
        try
        {
            log.Log(LogLevel.Information, $"[Simulator] Run start mode={(options.Validate ? "validate" : "generate")} run={options.RunName}");

            var runner = new DiscordCycleSimulationRunner(options, log);
            var run = await runner.RunAsync().ConfigureAwait(false);

            var manifest = SimulationManifest.Create(options, run);
            WriteManifest(options.ManifestPath, manifest, log);

            var reportWriter = new SimulationReportWriter(options, manifest, log);
            var summary = reportWriter.BuildSummary();
            reportWriter.WriteSummary(summary);

            if (options.Validate)
            {
                if (!summary.AllCyclesValid)
                {
                    log.Log(LogLevel.Error, "[Simulator] Validation failed. See summary.json for details.");
                    return 2;
                }

                log.Log(LogLevel.Information, "[Simulator] Validation succeeded.");
            }

            return 0;
        }
        catch (Exception ex)
        {
            log.Log(LogLevel.Error, "[Simulator] Unhandled exception", ex);
            return 1;
        }
    }

    private static void WriteManifest(string manifestPath, SimulationManifest manifest, ILogSink log)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(manifestPath)!);
        var serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        var json = JsonSerializer.Serialize(manifest, serializerOptions);
        File.WriteAllText(manifestPath, json);
        log.Log(LogLevel.Information, $"[Simulator] Wrote manifest to {manifestPath}");
    }
}
