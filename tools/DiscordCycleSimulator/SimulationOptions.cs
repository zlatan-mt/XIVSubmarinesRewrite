// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationOptions.cs
// DiscordCycleSimulator の CLI オプション解析と共通パス計算を行います
// 実行モードや出力先を統一し、追加機能でも設定が簡潔になるように存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/Program.cs, apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationReportWriter.cs

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

using System;
using System.Globalization;
using System.IO;

internal sealed record SimulationOptions(
    string OutputRoot,
    string RunName,
    int CycleCount,
    TimeSpan CycleSpacing,
    ulong CharacterId,
    DateTime BaseDepartureUtc,
    bool Validate)
{
    public static SimulationOptions Parse(string[] args)
    {
        var nowUtc = DateTime.UtcNow;
        var outputRoot = Path.Combine("logs", nowUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), "notification-cycle");
        var runName = "cli-" + nowUtc.ToString("HHmmss", CultureInfo.InvariantCulture);
        var cycleCount = 2;
        var cycleSpacing = TimeSpan.FromMinutes(12);
        ulong characterId = 0xA11CEUL;
        var baseDepartureUtc = nowUtc.AddHours(-42);
        var validate = false;

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
            else if (arg.StartsWith("--cycles=", StringComparison.Ordinal))
            {
                if (int.TryParse(arg[9..], NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedCycles) && parsedCycles > 0)
                {
                    cycleCount = parsedCycles;
                }
            }
            else if (arg.StartsWith("--spacing-min=", StringComparison.Ordinal))
            {
                if (double.TryParse(arg[14..], NumberStyles.Float, CultureInfo.InvariantCulture, out var minutes) && minutes > 0)
                {
                    cycleSpacing = TimeSpan.FromMinutes(minutes);
                }
            }
            else if (arg.StartsWith("--character=", StringComparison.Ordinal))
            {
                if (ulong.TryParse(arg[12..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var parsedCharacter))
                {
                    characterId = parsedCharacter;
                }
            }
            else if (arg.StartsWith("--base-offset-hours=", StringComparison.Ordinal))
            {
                if (double.TryParse(arg[20..], NumberStyles.Float, CultureInfo.InvariantCulture, out var hours))
                {
                    baseDepartureUtc = nowUtc.AddHours(-Math.Abs(hours));
                }
            }
            else if (string.Equals(arg, "--validate", StringComparison.OrdinalIgnoreCase))
            {
                validate = true;
            }
        }

        var runRoot = Path.Combine(outputRoot, runName);
        Directory.CreateDirectory(runRoot);
        return new SimulationOptions(outputRoot, runName, cycleCount, cycleSpacing, characterId, baseDepartureUtc, validate);
    }

    public string RunRoot => Path.Combine(this.OutputRoot, this.RunName);

    public string DiscordFolder => Path.Combine(this.RunRoot, "discord");

    public string NotionFolder => Path.Combine(this.RunRoot, "notion");

    public string MetadataFolder => Path.Combine(this.RunRoot, "meta");

    public string LogFilePath => Path.Combine(this.RunRoot, "simulation.log");

    public string ManifestPath => Path.Combine(this.MetadataFolder, "manifest.json");

    public string SummaryPath => Path.Combine(this.MetadataFolder, "summary.json");

    public string HtmlReportPath => Path.Combine(this.MetadataFolder, "report.html");
}
