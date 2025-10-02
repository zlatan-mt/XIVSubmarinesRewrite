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

        var references = ThemeReferenceLoader.Load();
        var swatches = ThemeSwatchFactory.Create(references);
        WriteJson(options.JsonPath, options.RunName, swatches);
        WriteHtml(options.HtmlPath, options.RunName, swatches);
        WriteSummary(Path.Combine(options.RunRoot, "color-summary.json"), swatches);

        Console.WriteLine($"[RendererPreview] Generated {swatches.Count} swatches under {options.RunRoot}.");
        return 0;
    }

    private static void WriteJson(string path, string runName, IReadOnlyList<ThemeSwatch> swatches)
    {
        var artifact = new RendererPreviewArtifact(runName, DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture), swatches);
        var json = JsonSerializer.Serialize(artifact, JsonOptions);
        File.WriteAllText(path, json, Utf8NoBom);
    }

    private static void WriteSummary(string path, IReadOnlyList<ThemeSwatch> swatches)
    {
        var summary = new
        {
            GeneratedAt = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture),
            TotalColors = swatches.Count,
            BackgroundColors = swatches.Count(s => s.IsBackground),
            TextColors = swatches.Count(s => !s.IsBackground),
            DevColors = swatches.Count(s => s.Token.StartsWith("Dev", StringComparison.Ordinal)),
            Colors = swatches.Select(s => new
            {
                Token = s.Token,
                Hex = s.Hex,
                Rgba = s.Rgba,
                IsBackground = s.IsBackground,
                Description = s.Description,
                Role = s.Role,
            }).ToArray(),
        };

        var json = JsonSerializer.Serialize(summary, JsonOptions);
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
            var figmaHexAttr = string.IsNullOrWhiteSpace(swatch.ReferenceHex) ? string.Empty : $" data-figma-hex=\"{swatch.ReferenceHex}\"";
            var figmaRgbaAttr = string.IsNullOrWhiteSpace(swatch.ReferenceRgba) ? string.Empty : $" data-figma-rgba=\"{swatch.ReferenceRgba}\"";
            var role = swatch.Role ?? (swatch.IsBackground ? "background" : "text");
            builder.AppendLine($"    <div class=\"swatch\" data-token=\"{swatch.Token}\" data-expected=\"{swatch.Rgba}\" data-role=\"{role}\"{figmaHexAttr}{figmaRgbaAttr} style=\"{BuildStyle(swatch)}\">");
            builder.AppendLine($"      <div class=\"token\">{swatch.Token}</div>");
            builder.AppendLine($"      <div class=\"preview\">Sample Text</div>");
            var referenceLine = string.IsNullOrWhiteSpace(swatch.ReferenceHex)
                ? string.Empty
                : $"<br>FIGMA: {swatch.ReferenceHex}";
            builder.AppendLine($"      <div class=\"meta\">{swatch.Description}<br>HEX: {swatch.Hex}<br>RGBA: {swatch.Rgba}{referenceLine}</div>");
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
    string Description,
    string? ReferenceHex,
    string? ReferenceRgba,
    string? Role);

internal static class ThemeReferenceLoader
{
    public static IReadOnlyDictionary<string, ThemeReference> Load()
    {
        try
        {
            var repoRoot = Environment.CurrentDirectory;
            var path = Path.Combine(repoRoot, "docs", "ui", "theme-final.jsonc");
            if (!File.Exists(path))
            {
                return new Dictionary<string, ThemeReference>(StringComparer.OrdinalIgnoreCase);
            }

            using var stream = File.OpenRead(path);
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip,
            };

            using var document = JsonDocument.Parse(stream, options);
            if (!document.RootElement.TryGetProperty("palette", out var paletteElement))
            {
                return new Dictionary<string, ThemeReference>(StringComparer.OrdinalIgnoreCase);
            }

            var map = new Dictionary<string, ThemeReference>(StringComparer.OrdinalIgnoreCase);
            foreach (var element in paletteElement.EnumerateArray())
            {
                if (!element.TryGetProperty("token", out var tokenElement))
                {
                    continue;
                }

                var token = tokenElement.GetString();
                if (string.IsNullOrWhiteSpace(token))
                {
                    continue;
                }

                var role = element.TryGetProperty("role", out var roleElement) ? roleElement.GetString() : null;
                var hex = element.TryGetProperty("figmaHex", out var hexElement) ? hexElement.GetString() : null;
                var rgba = element.TryGetProperty("figmaRgba", out var rgbaElement) ? rgbaElement.GetString() : null;
                var description = element.TryGetProperty("description", out var descriptionElement) ? descriptionElement.GetString() : null;
                map[token] = new ThemeReference(token, role, hex, rgba, description);
            }

            return map;
        }
        catch
        {
            return new Dictionary<string, ThemeReference>(StringComparer.OrdinalIgnoreCase);
        }
    }
}

internal sealed record ThemeReference(
    string Token,
    string? Role,
    string? FigmaHex,
    string? FigmaRgba,
    string? Description);

internal static class ThemeSwatchFactory
{
    public static IReadOnlyList<ThemeSwatch> Create(IReadOnlyDictionary<string, ThemeReference> references)
    {
        var list = new List<ThemeSwatch>(UiTheme.Palette.Count);
        foreach (var entry in UiTheme.Palette)
        {
            references.TryGetValue(entry.Token, out var reference);
            var description = reference?.Description ?? entry.Description;
            var role = reference?.Role ?? (entry.IsBackground ? "background" : "text");
            list.Add(new ThemeSwatch(
                entry.Token,
                entry.Hex,
                entry.Rgba,
                entry.IsBackground,
                description,
                reference?.FigmaHex,
                reference?.FigmaRgba,
                role));
        }

        return list;
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
