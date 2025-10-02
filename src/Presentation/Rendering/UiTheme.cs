// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs
// ImGui 表示で使用する共通カラーと軽量スタイルユーティリティを定義します
// UI 全体のトーンを揃え、黒・白・ディープブルー基調のテーマを保つため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Numerics;

/// <summary>Shared color palette for renderer components.</summary>
public static class UiTheme
{
    // Core surfaces keep background contrast predictable for dark UI.
    public static readonly Vector4 WindowBg = new (0.05f, 0.05f, 0.06f, 0.98f);
    public static readonly Vector4 ToolbarBg = new (0.08f, 0.08f, 0.11f, 1f);
    public static readonly Vector4 ToolbarBorder = new (0.23f, 0.23f, 0.27f, 1f);
    public static readonly Vector4 SurfaceBorder = new (0.18f, 0.18f, 0.22f, 0.92f);
    public static readonly Vector4 PanelBg = new (0.12f, 0.12f, 0.15f, 0.90f);

    // Text colors align with black / white / deep blue direction.
    public static readonly Vector4 PrimaryText = new (0.94f, 0.94f, 0.96f, 1f);
    public static readonly Vector4 ToolbarText = new (0.94f, 0.94f, 0.96f, 1f);
    public static readonly Vector4 ToolbarMuted = new (0.60f, 0.60f, 0.64f, 1f);
    public static readonly Vector4 MutedText = new (0.58f, 0.58f, 0.62f, 1f);
    public static readonly Vector4 AccentPrimary = new (0.12f, 0.36f, 0.82f, 1f);
    public static readonly Vector4 WarningText = new (0.95f, 0.72f, 0.28f, 1f);
    public static readonly Vector4 SuccessText = new (0.18f, 0.58f, 0.38f, 1f);
    public static readonly Vector4 ErrorText = new (0.92f, 0.30f, 0.28f, 1f);

    // DEV-specific colors for developer tools and debugging UI.
    public static readonly Vector4 DevAccent = new (0.85f, 0.45f, 0.12f, 1f);
    public static readonly Vector4 DevBannerBg = new (0.22f, 0.12f, 0.05f, 0.88f);
    public static readonly Vector4 DevBannerText = new (0.95f, 0.78f, 0.45f, 1f);
    public static readonly Vector4 DevDangerBg = new (0.32f, 0.08f, 0.08f, 0.92f);
    public static readonly Vector4 DevDangerText = new (0.98f, 0.55f, 0.52f, 1f);

    private static readonly IReadOnlyList<UiThemeColor> PaletteValues = Array.AsReadOnly(new[]
    {
        new UiThemeColor("WindowBg", WindowBg, true, "Main window background"),
        new UiThemeColor("ToolbarBg", ToolbarBg, true, "Toolbar background"),
        new UiThemeColor("ToolbarBorder", ToolbarBorder, true, "Toolbar border"),
        new UiThemeColor("SurfaceBorder", SurfaceBorder, true, "Surface outlines"),
        new UiThemeColor("PanelBg", PanelBg, true, "Panel background"),
        new UiThemeColor("ToolbarText", ToolbarText, false, "Toolbar text"),
        new UiThemeColor("ToolbarMuted", ToolbarMuted, false, "Toolbar meta text"),
        new UiThemeColor("PrimaryText", PrimaryText, false, "Primary text"),
        new UiThemeColor("MutedText", MutedText, false, "Muted body text"),
        new UiThemeColor("AccentPrimary", AccentPrimary, false, "Accent actions"),
        new UiThemeColor("WarningText", WarningText, false, "Warning states"),
        new UiThemeColor("SuccessText", SuccessText, false, "Success states"),
        new UiThemeColor("ErrorText", ErrorText, false, "Error highlight"),
        new UiThemeColor("DevAccent", DevAccent, false, "DEV accent color"),
        new UiThemeColor("DevBannerBg", DevBannerBg, true, "DEV banner background"),
        new UiThemeColor("DevBannerText", DevBannerText, false, "DEV banner text"),
        new UiThemeColor("DevDangerBg", DevDangerBg, true, "DEV danger background"),
        new UiThemeColor("DevDangerText", DevDangerText, false, "DEV danger text"),
    });

    /// <summary>Tokenized palette that keeps RendererPreview と Playwright テストを同期します。</summary>
    public static IReadOnlyList<UiThemeColor> Palette => PaletteValues;

    /// <summary>Computes WCAG 2.1 contrast ratio between a foreground and background color.</summary>
    public static double ContrastRatio(Vector4 foreground, Vector4 background)
    {
        var lumA = Luminance(foreground);
        var lumB = Luminance(background);
        var lighter = Math.Max(lumA, lumB);
        var darker = Math.Min(lumA, lumB);
        return (lighter + 0.05) / (darker + 0.05);
    }

    /// <summary>Converts a theme vector to a hex string (without alpha).</summary>
    public static string ToHex(Vector4 color)
    {
        var (r, g, b, _) = ToRgbaComponents(color);
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    /// <summary>Converts a theme vector to an rgba() string.</summary>
    public static string ToRgba(Vector4 color)
    {
        var (r, g, b, a) = ToRgbaComponents(color);
        var alpha = a / 255f;
        var alphaText = Math.Abs(alpha - 1f) < 0.0005f
            ? "1"
            : alpha.ToString("0.##", CultureInfo.InvariantCulture);
        return $"rgba({r}, {g}, {b}, {alphaText})";
    }

    private static (int r, int g, int b, int a) ToRgbaComponents(Vector4 color)
    {
        static int ToByte(float component)
            => (int)Math.Round(Math.Clamp(component, 0f, 1f) * 255f, MidpointRounding.AwayFromZero);

        return (ToByte(color.X), ToByte(color.Y), ToByte(color.Z), ToByte(color.W));
    }

    private static double Luminance(Vector4 color)
    {
        static double Channel(float value)
        {
            var v = Math.Clamp(value, 0f, 1f);
            return v <= 0.03928f ? v / 12.92 : Math.Pow((v + 0.055) / 1.055, 2.4);
        }

        var r = Channel(color.X);
        var g = Channel(color.Y);
        var b = Channel(color.Z);
        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }

    /// <summary>Palette entry structure shared with tests and tooling.</summary>
    public readonly struct UiThemeColor
    {
        public UiThemeColor(string token, Vector4 value, bool isBackground, string description)
        {
            this.Token = token;
            this.Value = value;
            this.IsBackground = isBackground;
            this.Description = description;
            this.Hex = ToHex(value);
            this.Rgba = ToRgba(value);
        }

        public string Token { get; }

        public Vector4 Value { get; }

        public bool IsBackground { get; }

        public string Description { get; }

        public string Hex { get; }

        public string Rgba { get; }

        public double Alpha => Math.Clamp(this.Value.W, 0f, 1f);
    }
}
