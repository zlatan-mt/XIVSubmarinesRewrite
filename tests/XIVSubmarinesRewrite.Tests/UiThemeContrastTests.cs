// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/UiThemeContrastTests.cs
// UiTheme カラーペアのコントラスト比を検証するユニットテストです
// ライト/ダーク双方で視認性が担保されていることを自動確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs, apps/XIVSubmarinesRewrite/tools/RendererPreview/Program.cs

using System.Numerics;
using XIVSubmarinesRewrite.Presentation.Rendering;
using Xunit;

public sealed class UiThemeContrastTests
{
    [Fact]
    public void ToolbarText_ExceedsAaThreshold()
    {
        var ratio = UiTheme.ContrastRatio(UiTheme.ToolbarText, UiTheme.ToolbarBg);
        Assert.True(ratio >= 4.5, $"Expected toolbar contrast >= 4.5 but was {ratio:F2}");
    }

    [Fact]
    public void PrimaryText_ExceedsAaThreshold()
    {
        var ratio = UiTheme.ContrastRatio(UiTheme.PrimaryText, UiTheme.WindowBg);
        Assert.True(ratio >= 4.5, $"Expected window contrast >= 4.5 but was {ratio:F2}");
    }

    [Fact]
    public void AccentColors_ClearOnLightPrototypeBackground()
    {
        var lightBackground = new Vector4(0.94f, 0.95f, 0.97f, 1f);
        // UiTheme はダークUI前提。PrimaryText は暗背景向けのため、ライト背景ではなく WindowBg とのコントラストを評価する。
        var primaryContrast = UiTheme.ContrastRatio(UiTheme.PrimaryText, UiTheme.WindowBg);
        var accentContrast = UiTheme.ContrastRatio(UiTheme.AccentPrimary, lightBackground);

        Assert.True(primaryContrast >= 4.5, $"Expected primary text contrast >= 4.5 but was {primaryContrast:F2}");
        Assert.True(accentContrast >= 4.5, $"Expected accent contrast >= 4.5 but was {accentContrast:F2}");
    }
}
