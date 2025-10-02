// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/UiThemePaletteTests.cs
// UiTheme の定義と Figma 参照 JSON の値を比較するテストです
// ドキュメントと実装のカラーパレットが乖離しないよう監視するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs, apps/XIVSubmarinesRewrite/docs/ui/theme-final.jsonc

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using XIVSubmarinesRewrite.Presentation.Rendering;
using Xunit;

public sealed class UiThemePaletteTests
{
    [Fact]
    public void Palette_MatchesFigmaReference()
    {
        var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
        var referencePath = Path.Combine(repoRoot, "docs", "ui", "theme-final.jsonc");
        Assert.True(File.Exists(referencePath), $"Expected reference file at {referencePath}");

        using var stream = File.OpenRead(referencePath);
        using var document = JsonDocument.Parse(stream, new JsonDocumentOptions
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        });

        var paletteElement = document.RootElement.GetProperty("palette");
        var referenceMap = new Dictionary<string, (string Hex, string? Rgba)>(StringComparer.OrdinalIgnoreCase);
        foreach (var element in paletteElement.EnumerateArray())
        {
            var token = element.GetProperty("token").GetString() ?? string.Empty;
            var hex = element.GetProperty("figmaHex").GetString() ?? string.Empty;
            var rgba = element.TryGetProperty("figmaRgba", out var rgbaElement) ? rgbaElement.GetString() : null;
            referenceMap[token] = (hex.ToUpperInvariant(), rgba);
        }

        foreach (var entry in UiTheme.Palette)
        {
            Assert.True(referenceMap.TryGetValue(entry.Token, out var reference), $"Missing reference for token {entry.Token}");
            Assert.Equal(reference.Hex, entry.Hex.ToUpperInvariant());

            if (!string.IsNullOrWhiteSpace(reference.Rgba))
            {
                Assert.Equal(reference.Rgba, UiTheme.ToRgba(entry.Value));
            }

            referenceMap.Remove(entry.Token);
        }

        Assert.Empty(referenceMap); // 参照に余分なトークンが残っていないこと
    }
}
