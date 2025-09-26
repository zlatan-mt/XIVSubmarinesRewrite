// apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationInfrastructure.cs
// CLI ログ出力とプレースホルダー画像など共通ユーティリティを提供します
// シミュレーション中の情報を記録し、スクリーンショット代替を出力するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/Program.cs, apps/XIVSubmarinesRewrite/tools/DiscordCycleSimulator/SimulationReportWriter.cs

namespace XIVSubmarinesRewrite.Tools.DiscordCycleSimulator;

using System;
using System.IO;
using XIVSubmarinesRewrite.Infrastructure.Logging;

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
