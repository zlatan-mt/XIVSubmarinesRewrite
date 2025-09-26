// apps/XIVSubmarinesRewrite/tools/RendererPreview/Program.cs
// UiTheme のカラーパレットを HTML と JSON へ書き出す CLI です
// 色の自動検証を Playwright で行えるようアーティファクトを生成するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs, apps/XIVSubmarinesRewrite/tests/Playwright/ui-theme.spec.ts

namespace XIVSubmarinesRewrite.Tools.RendererPreview;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using XIVSubmarinesRewrite.Presentation.Rendering;

internal static class Program
{
    private static readonly JsonSerializerOptions JsonOptions = new ()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

    public static int Main(string[] args)
    {
        var options = RendererPreviewOptions.Parse(args);
        Directory.CreateDirectory(options.RunRoot);

        var swatches = ThemeSwatchFactory.Create();
        WriteJson(options.JsonPath, options.RunName, swatches);
        WriteHtml(options.HtmlPath, options.RunName, swatches);

        Console.WriteLine($"[RendererPreview] Generated {swatches.Count} swatches under {options.RunRoot}.");
        return 0;
    }

    private static void WriteJson(string path, string runName, IReadOnlyList<ThemeSwatch> swatches)
    {
        var artifact = new RendererPreviewArtifact(runName, DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture), swatches);
        var json = JsonSerializer.Serialize(artifact, JsonOptions);
        File.WriteAllText(path, json, Utf8NoBom);
    }

    private static void WriteHtml(string path, string runName, IReadOnlyList<ThemeSwatch> swatches)
    {
        var builder = new StringBuilder();
        builder.AppendLine("<!DOCTYPE html>");
        builder.AppendLine("<html lang=\"ja\">");
        builder.AppendLine("<head>");
        builder.AppendLine("  <meta charset=\"utf-8\">");
        builder.AppendLine($"  <title>UiTheme Preview — {runName}</title>");
        builder.AppendLine("  <style>");
        builder.AppendLine("    body { background: #070708; color: #f5f5f5; font-family: 'Segoe UI', sans-serif; margin: 24px; }");
        builder.AppendLine("    .grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(240px, 1fr)); gap: 16px; }");
        builder.AppendLine("    .swatch { border-radius: 12px; padding: 16px; box-shadow: 0 6px 18px rgba(0,0,0,0.4); position: relative; }");
        builder.AppendLine("    .swatch[data-role=background] { color: #fff; }");
        builder.AppendLine("    .swatch[data-role=text] { background: #121217; color: inherit; }");
        builder.AppendLine("    .token { font-weight: 600; letter-spacing: 0.04em; text-transform: uppercase; }");
        builder.AppendLine("    .preview { margin-top: 12px; font-size: 20px; }");
        builder.AppendLine("    .meta { font-size: 12px; margin-top: 12px; opacity: 0.8; }");
        builder.AppendLine("  </style>");
        builder.AppendLine("</head>");
        builder.AppendLine("<body>");
        builder.AppendLine($"  <h1>UiTheme Preview — {runName}</h1>");
        builder.AppendLine("  <p>data-expected 属性に rgba 値をセットしているので、自動テストは computed style を比較できます。</p>");
        builder.AppendLine("  <div class=\"grid\">");

        foreach (var swatch in swatches)
        {
            builder.AppendLine($"    <div class=\"swatch\" data-token=\"{swatch.Token}\" data-expected=\"{swatch.Rgba}\" data-role=\"{(swatch.IsBackground ? "background" : "text")}\" style=\"{BuildStyle(swatch)}\">");
            builder.AppendLine($"      <div class=\"token\">{swatch.Token}</div>");
            builder.AppendLine($"      <div class=\"preview\">Sample Text</div>");
            builder.AppendLine($"      <div class=\"meta\">{swatch.Description}<br>HEX: {swatch.Hex}<br>RGBA: {swatch.Rgba}</div>");
            builder.AppendLine("    </div>");
        }

        builder.AppendLine("  </div>");
        builder.AppendLine("</body>");
        builder.AppendLine("</html>");

        File.WriteAllText(path, builder.ToString(), Utf8NoBom);
    }

    private static string BuildStyle(ThemeSwatch swatch)
    {
        if (swatch.IsBackground)
        {
            return $"background-color: {swatch.Rgba}; color: #f5f5f5;";
        }

        return $"color: {swatch.Rgba};";
    }
}

internal sealed record RendererPreviewArtifact(
    string Run,
    string GeneratedAtUtc,
    IReadOnlyList<ThemeSwatch> Swatches);

internal sealed record ThemeSwatch(
    string Token,
    string Hex,
    string Rgba,
    bool IsBackground,
    string Description);

internal static class ThemeSwatchFactory
{
    public static IReadOnlyList<ThemeSwatch> Create()
    {
        return new List<ThemeSwatch>
        {
            Build("PrimaryText", UiTheme.PrimaryText, false, "通常テキスト"),
            Build("MutedText", UiTheme.MutedText, false, "サブラベル・補足"),
            Build("AccentPrimary", UiTheme.AccentPrimary, false, "アクション強調"),
            Build("WarningText", UiTheme.WarningText, false, "警告表示"),
            Build("SuccessText", UiTheme.SuccessText, false, "成功状態"),
            Build("ErrorText", UiTheme.ErrorText, false, "エラー強調"),
            Build("PanelBg", UiTheme.PanelBg, true, "設定パネル背景"),
        };
    }

    private static ThemeSwatch Build(string token, Vector4 color, bool isBackground, string description)
    {
        var (r, g, b, a) = Convert(color);
        var hex = $"#{r:X2}{g:X2}{b:X2}";
        var alpha = a / 255.0;
        var alphaText = alpha >= 0.999 ? "1" : alpha.ToString("0.##", CultureInfo.InvariantCulture);
        var rgba = $"rgba({r}, {g}, {b}, {alphaText})";
        return new ThemeSwatch(token, hex, rgba, isBackground, description);
    }

    private static (int r, int g, int b, int a) Convert(Vector4 color)
    {
        int ToByte(float component) => (int)Math.Round(Math.Clamp(component, 0f, 1f) * 255f, MidpointRounding.AwayFromZero);
        return (ToByte(color.X), ToByte(color.Y), ToByte(color.Z), ToByte(color.W));
    }
}

internal sealed record RendererPreviewOptions(
    string OutputRoot,
    string RunName)
{
    public static RendererPreviewOptions Parse(string[] args)
    {
        var nowUtc = DateTime.UtcNow;
        var outputRoot = Path.Combine("logs", nowUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), "renderer-preview");
        var runName = "cli-" + nowUtc.ToString("HHmmss", CultureInfo.InvariantCulture);

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
        }

        var runRoot = Path.Combine(outputRoot, runName);
        Directory.CreateDirectory(runRoot);
        return new RendererPreviewOptions(outputRoot, runName);
    }

    public string RunRoot => Path.Combine(this.OutputRoot, this.RunName);

    public string HtmlPath => Path.Combine(this.RunRoot, "report.html");

    public string JsonPath => Path.Combine(this.RunRoot, "swatches.json");
}
