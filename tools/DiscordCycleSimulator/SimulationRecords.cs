// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationRecords.cs
// DiscordCycleSimulator が扱う実行結果とマニフェストのデータ構造を定義します
// 実行と検証の結果を一貫した形で保存し、レポート生成に使うため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationOptions.cs, apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationReportWriter.cs

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

internal sealed record SimulationRun(IReadOnlyList<SimulationCycleResult> Cycles);

internal sealed record SimulationCycleResult(
    int CycleNumber,
    int CompletionCount,
    int UnderwayCount,
    int NotionPayloadCount,
    int DiscordFieldCount,
    bool Aggregated,
    string CharacterLabel,
    DateTime AggregateTimestampUtc,
    string DiscordPayloadPath,
    string NotionPayloadPath,
    string ScreenshotPath);

internal sealed record SimulationManifest(
    string Run,
    string GeneratedAtUtc,
    int CycleCount,
    IReadOnlyList<SimulationManifestCycle> Cycles)
{
    public static SimulationManifest Create(SimulationOptions options, SimulationRun run)
    {
        var generatedAt = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture);
        var cycles = run.Cycles
            .Select(cycle => new SimulationManifestCycle(
                cycle.CycleNumber,
                cycle.CompletionCount,
                cycle.UnderwayCount,
                cycle.NotionPayloadCount,
                cycle.DiscordFieldCount,
                cycle.Aggregated,
                cycle.CharacterLabel,
                cycle.AggregateTimestampUtc.ToString("O", CultureInfo.InvariantCulture),
                cycle.DiscordPayloadPath,
                cycle.NotionPayloadPath,
                cycle.ScreenshotPath))
            .ToList();

        return new SimulationManifest(options.RunName, generatedAt, cycles.Count, cycles);
    }
}

internal sealed record SimulationManifestCycle(
    int Cycle,
    int CompletionCount,
    int UnderwayCount,
    int NotionPayloadCount,
    int DiscordFieldCount,
    bool Aggregated,
    string CharacterLabel,
    string AggregateTimestampUtc,
    string DiscordPayloadPath,
    string NotionPayloadPath,
    string ScreenshotPath);
