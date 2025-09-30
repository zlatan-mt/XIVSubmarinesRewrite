// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs
// ImGui 表示で使用する共通カラーと軽量スタイルユーティリティを定義します
// UI 全体のトーンを揃え、黒・白・ディープブルー基調のテーマを保つため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System;
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

    /// <summary>Computes WCAG 2.1 contrast ratio between a foreground and background color.</summary>
    public static double ContrastRatio(Vector4 foreground, Vector4 background)
    {
        var lumA = Luminance(foreground);
        var lumB = Luminance(background);
        var lighter = Math.Max(lumA, lumB);
        var darker = Math.Min(lumA, lumB);
        return (lighter + 0.05) / (darker + 0.05);
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
}
