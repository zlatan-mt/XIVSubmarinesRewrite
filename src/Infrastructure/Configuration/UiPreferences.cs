// apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/UiPreferences.cs
// UI 表示状態とユーザー選択を永続化する設定クラスを定義します
// 起動時のウィンドウ表示やキャラクター選択を復元できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/DalamudJsonSettingsProvider.cs

namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>User interface preferences persisted across sessions.</summary>
public sealed class UiPreferences
{
    public ulong? LastSelectedCharacterId { get; set; }

    public bool OverviewWindowVisible { get; set; }

    public bool MainWindowVisible { get; set; } = true;

    public float MainWindowWidth { get; set; } = 780f;

    public float MainWindowHeight { get; set; } = 520f;

    public bool ShowDeveloperTools { get; set; } = false;
}
