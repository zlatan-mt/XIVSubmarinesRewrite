// apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs
// ImGui 表示で使用する共通カラーと軽量スタイルユーティリティを定義します
// UI 全体のトーンを揃え、黒・白・ディープブルー基調のテーマを保つため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

namespace XIVSubmarinesRewrite.Presentation.Rendering;

using System.Numerics;

/// <summary>Shared color palette for renderer components.</summary>
public static class UiTheme
{
    public static readonly Vector4 PrimaryText = new (0.92f, 0.92f, 0.95f, 1f);
    public static readonly Vector4 MutedText = new (0.62f, 0.62f, 0.66f, 1f);
    public static readonly Vector4 AccentPrimary = new (0.18f, 0.36f, 0.75f, 1f);
    public static readonly Vector4 WarningText = new (0.95f, 0.72f, 0.28f, 1f);
    public static readonly Vector4 SuccessText = new (0.18f, 0.58f, 0.38f, 1f);
}

