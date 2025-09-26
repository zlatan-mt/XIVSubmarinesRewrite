// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationReportWriter.cs
// シミュレーション実行結果から検証サマリと HTML レポートを生成します
// 集約ロジックの健全性を即時に確認し、ドキュメントへ貼り付けやすくするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/Program.cs, apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationRecords.cs

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using XIVSubmarinesRewrite.Infrastructure.Logging;

internal sealed class SimulationReportWriter
{
    private static readonly JsonSerializerOptions SummaryJsonOptions = new ()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

    private readonly SimulationOptions options;
    private readonly SimulationManifest manifest;
    private readonly ILogSink log;

    public SimulationReportWriter(SimulationOptions options, SimulationManifest manifest, ILogSink log)
    {
        this.options = options;
        this.manifest = manifest;
        this.log = log;
    }

    public SimulationSummary BuildSummary()
    {
        var cycles = new List<SimulationSummaryCycle>(this.manifest.CycleCount);
        var overallNotes = new List<string>
        {
            $"Manifest: {this.options.ManifestPath}",
            $"Summary JSON: {this.options.SummaryPath}",
            $"HTML Report: {this.options.HtmlReportPath}",
        };

        var allValid = true;
        foreach (var cycle in this.manifest.Cycles)
        {
            var issues = new List<string>();

            if (!cycle.Aggregated)
            {
                issues.Add("Aggregator did not flush a Discord payload.");
            }

            if (cycle.CompletionCount != 4)
            {
                issues.Add($"Expected 4 completion events but observed {cycle.CompletionCount}.");
            }

            if (cycle.UnderwayCount != 4)
            {
                issues.Add($"Expected 4 underway events but observed {cycle.UnderwayCount}.");
            }

            if (cycle.NotionPayloadCount != cycle.UnderwayCount)
            {
                issues.Add($"Notion payload count mismatch (expected {cycle.UnderwayCount}, actual {cycle.NotionPayloadCount}).");
            }

            if (cycle.DiscordFieldCount != cycle.UnderwayCount)
            {
                issues.Add($"Discord field count mismatch (expected {cycle.UnderwayCount}, actual {cycle.DiscordFieldCount}).");
            }

            ValidateFile(cycle.DiscordPayloadPath, "Discord payload", issues);
            ValidateFile(cycle.NotionPayloadPath, "Notion payload", issues);
            ValidateFile(cycle.ScreenshotPath, "Screenshot placeholder", issues);

            var isValid = issues.Count == 0;
            if (!isValid)
            {
                allValid = false;
            }

            cycles.Add(new SimulationSummaryCycle(
                cycle.Cycle,
                cycle.CharacterLabel,
                cycle.AggregateTimestampUtc,
                cycle.CompletionCount,
                cycle.UnderwayCount,
                cycle.DiscordFieldCount,
                cycle.NotionPayloadCount,
                cycle.DiscordPayloadPath,
                cycle.NotionPayloadPath,
                cycle.ScreenshotPath,
                isValid,
                issues));
        }

        return new SimulationSummary(
            this.manifest.Run,
            this.manifest.GeneratedAtUtc,
            allValid,
            cycles,
            overallNotes);
    }

    public void WriteSummary(SimulationSummary summary)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(this.options.SummaryPath)!);

        var json = JsonSerializer.Serialize(summary, SummaryJsonOptions);
        File.WriteAllText(this.options.SummaryPath, json, Utf8NoBom);
        this.log.Log(LogLevel.Information, $"[Simulator] Wrote summary to {this.options.SummaryPath}");

        var html = BuildHtml(summary);
        File.WriteAllText(this.options.HtmlReportPath, html, Utf8NoBom);
        this.log.Log(LogLevel.Information, $"[Simulator] Wrote report to {this.options.HtmlReportPath}");
    }

    private static string BuildHtml(SimulationSummary summary)
    {
        var builder = new StringBuilder();
        builder.AppendLine("<!DOCTYPE html>");
        builder.AppendLine("<html lang=\"ja\">");
        builder.AppendLine("<head>");
        builder.AppendLine("  <meta charset=\"utf-8\">");
        builder.AppendLine("  <title>Discord Cycle Simulation Report</title>");
        builder.AppendLine("  <style>");
        builder.AppendLine("    body { font-family: 'Segoe UI', sans-serif; background: #111; color: #eee; margin: 24px; }");
        builder.AppendLine("    table { border-collapse: collapse; width: 100%; margin-top: 16px; }");
        builder.AppendLine("    th, td { border: 1px solid #333; padding: 8px 12px; text-align: left; }");
        builder.AppendLine("    th { background: #1f1f28; }");
        builder.AppendLine("    tr.pass { background: rgba(33, 73, 40, 0.6); }");
        builder.AppendLine("    tr.fail { background: rgba(92, 33, 33, 0.6); }");
        builder.AppendLine("    ul { margin: 8px 0 0 16px; }");
        builder.AppendLine("    .notes { margin-top: 24px; }");
        builder.AppendLine("  </style>");
        builder.AppendLine("</head>");
        builder.AppendLine("<body>");
        builder.AppendLine($"  <h1>Discord Cycle Simulation Report — {summary.Run}</h1>");
        builder.AppendLine($"  <p>Generated at {summary.GeneratedAtUtc} (UTC). All cycles valid: {summary.AllCyclesValid}</p>");
        builder.AppendLine("  <table>");
        builder.AppendLine("    <thead><tr><th>Cycle</th><th>Character</th><th>Timestamp (UTC)</th><th>Completions</th><th>Underways</th><th>Discord Fields</th><th>Notion Payloads</th><th>Discord JSON</th><th>Notion JSON</th><th>Screenshot</th><th>Issues</th></tr></thead>");
        builder.AppendLine("    <tbody>");

        foreach (var cycle in summary.Cycles)
        {
            var statusClass = cycle.IsValid ? "pass" : "fail";
            builder.AppendLine($"      <tr class=\"{statusClass}\">");
            builder.AppendLine($"        <td>{cycle.Cycle}</td>");
            builder.AppendLine($"        <td>{cycle.CharacterLabel}</td>");
            builder.AppendLine($"        <td>{cycle.AggregateTimestampUtc}</td>");
            builder.AppendLine($"        <td>{cycle.CompletionCount}</td>");
            builder.AppendLine($"        <td>{cycle.UnderwayCount}</td>");
            builder.AppendLine($"        <td>{cycle.DiscordFieldCount}</td>");
            builder.AppendLine($"        <td>{cycle.NotionPayloadCount}</td>");
            builder.AppendLine($"        <td>{cycle.DiscordPayloadPath}</td>");
            builder.AppendLine($"        <td>{cycle.NotionPayloadPath}</td>");
            builder.AppendLine($"        <td>{cycle.ScreenshotPath}</td>");

            if (cycle.Issues.Count == 0)
            {
                builder.AppendLine("        <td>None</td>");
            }
            else
            {
                builder.AppendLine("        <td><ul>");
                foreach (var issue in cycle.Issues)
                {
                    builder.AppendLine($"          <li>{issue}</li>");
                }
                builder.AppendLine("        </ul></td>");
            }

            builder.AppendLine("      </tr>");
        }

        builder.AppendLine("    </tbody>");
        builder.AppendLine("  </table>");

        builder.AppendLine("  <div class=\"notes\">");
        builder.AppendLine("    <h2>Notes</h2>");
        builder.AppendLine("    <ul>");
        foreach (var note in summary.Notes)
        {
            builder.AppendLine($"      <li>{note}</li>");
        }
        builder.AppendLine("    </ul>");
        builder.AppendLine("  </div>");

        builder.AppendLine("</body>");
        builder.AppendLine("</html>");
        return builder.ToString();
    }

    private static void ValidateFile(string path, string label, List<string> issues)
    {
        var fullPath = Path.IsPathRooted(path) ? path : Path.GetFullPath(path);
        if (!File.Exists(fullPath))
        {
            issues.Add($"Missing {label} file at {path}.");
        }
    }
}

internal sealed record SimulationSummary(
    string Run,
    string GeneratedAtUtc,
    bool AllCyclesValid,
    IReadOnlyList<SimulationSummaryCycle> Cycles,
    IReadOnlyList<string> Notes);

internal sealed record SimulationSummaryCycle(
    int Cycle,
    string CharacterLabel,
    string AggregateTimestampUtc,
    int CompletionCount,
    int UnderwayCount,
    int DiscordFieldCount,
    int NotionPayloadCount,
    string DiscordPayloadPath,
    string NotionPayloadPath,
    string ScreenshotPath,
    bool IsValid,
    IReadOnlyList<string> Issues);
