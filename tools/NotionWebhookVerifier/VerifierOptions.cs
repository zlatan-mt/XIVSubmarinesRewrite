// apps/XIVSubmarinesRewrite/tools/NotionWebhookVerifier/VerifierOptions.cs
// NotionWebhookVerifier の CLI オプションを解析し、入出力パスを提供します
// 必須パラメータを検証しつつ、ログやリクエスト保存先を統一するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tools/NotionWebhookVerifier/Program.cs, apps/XIVSubmarinesRewrite/tools/NotionWebhookVerifier/NotionWebhookVerifier.csproj

namespace XIVSubmarinesRewrite.Tools.NotionWebhookVerifier;

using System;
using System.Globalization;
using System.IO;

internal readonly record struct VerifierOptions(
    string OutputRoot,
    string RunName,
    string WebhookUrl,
    ulong CharacterId)
{
    public static bool TryParse(string[] args, out VerifierOptions options, out string error)
    {
        var nowUtc = DateTime.UtcNow;
        var outputRoot = Path.Combine("logs", nowUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), "notion-verifier");
        var runName = "cli-" + nowUtc.ToString("HHmmss", CultureInfo.InvariantCulture);
        string? webhook = null;
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
            else if (arg.StartsWith("--webhook=", StringComparison.Ordinal))
            {
                webhook = arg[10..];
            }
            else if (arg.StartsWith("--character=", StringComparison.Ordinal))
            {
                if (ulong.TryParse(arg[12..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var parsed))
                {
                    characterId = parsed;
                }
            }
        }

        if (string.IsNullOrWhiteSpace(webhook))
        {
            error = "Missing required --webhook=<url> argument.";
            options = default;
            return false;
        }

        var runRoot = Path.Combine(outputRoot, runName);
        Directory.CreateDirectory(runRoot);

        options = new VerifierOptions(outputRoot, runName, webhook, characterId);
        error = string.Empty;
        return true;
    }

    public string RunRoot => Path.Combine(this.OutputRoot, this.RunName);

    public string LogFilePath => Path.Combine(this.RunRoot, "verifier.log");

    public string RequestPath => Path.Combine(this.RunRoot, "request.json");

    public string ResponsePath => Path.Combine(this.RunRoot, "response.json");

    public string SummaryPath => Path.Combine(this.RunRoot, "summary.json");

    public TimeSpan Timeout => TimeSpan.FromSeconds(15);
}
