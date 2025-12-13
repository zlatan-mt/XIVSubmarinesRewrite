# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

---

# XIV Submarines Rewrite - プロジェクトガイド

**プロジェクト**: Final Fantasy XIV Dalamud プラグイン
**目的**: 潜水艦探索管理の自動化とDiscord通知連携
**バージョン**: 1.3.0
**技術スタック**: .NET 9.0-windows, C# 12, Dalamud API Level 13

## コマンド早見表

### ビルド
```bash
# Release ビルド
dotnet build XIVSubmarinesRewrite.csproj -c Release

# Debug ビルド（devPluginsに自動コピー）
dotnet build XIVSubmarinesRewrite.csproj -c Debug

# クリーンビルド
dotnet clean && dotnet build -c Release
```

### テスト
```bash
# .NET 単体テスト（xUnit）
dotnet test tests/XIVSubmarinesRewrite.Tests/XIVSubmarinesRewrite.Tests.csproj

# 特定テストクラス実行
dotnet test --filter "FullyQualifiedName~DiscordCycleNotificationAggregatorTests"

# Playwright E2Eテスト（全タグ）
npm test --prefix tests/Playwright

# 特定タグのみ実行
npm test --prefix tests/Playwright -- --grep @theme
npm test --prefix tests/Playwright -- --grep @release

# UIモードでテスト
npm run test:ui --prefix tests/Playwright
```

### リリース
```bash
# リリースZIPファイル作成
dotnet build XIVSubmarinesRewrite.csproj -c Release
cd bin/Release/net9.0-windows
powershell.exe -Command "Compress-Archive -Path XIVSubmarinesRewrite.dll,XIVSubmarinesRewrite.pdb,manifest.json,icon.png -DestinationPath ../../../../XIVSubmarinesRewrite-v{VERSION}.zip -Force"
```

### Git運用
```bash
# ブランチ確認
git branch -a
# -> main: 開発兼公開用（タグでリリース）
# -> feature/*: 機能ブランチ

# 通常の開発フロー
git checkout -b feature/my-feature
# ... 開発作業 ...
git add .
git commit -m "feat: 機能説明"
git push origin feature/my-feature
# PR作成 -> Squash Merge -> main

# リリースフロー（詳細は後述）
```

## アーキテクチャ概要

### レイヤー構成（DDD風）

```
Presentation (UI)
    ↓
Application (Services)
    ↓
Domain (Models, Logic)
    ↓
Infrastructure (Dalamud API, Storage)
```

### 主要ディレクトリ

- **`src/Acquisition/`** - データ取得の抽象化と調整
- **`src/Application/`** - ビジネスロジック、通知システム
- **`src/Domain/`** - ドメインモデル（Submarine, Voyage等）
- **`src/Infrastructure/`** - Dalamud API統合、永続化、DI構成
- **`src/Integrations/`** - 外部サービス連携（Discord）
- **`src/Presentation/`** - ImGui UI レンダリング、ViewModel

### エントリーポイント

**`src/Plugin.cs`** - Dalamud プラグインライフサイクル
- コンストラクタ: DI構成、イベントハンドラ登録
- Dispose: クリーンアップ

**主要コンポーネント**:
- `PluginBootstrapper` (DI) - `src/Infrastructure/Composition/`
- `SnapshotOrchestrator` - `src/Application/Services/`
- `MainWindowRenderer` - `src/Presentation/Rendering/`

### チャットコマンド
- `/xsr` - メインウィンドウ表示/非表示
- `/xsr notify` - 通知タブを開く
- `/xsr dev` - 開発者タブを開く

## 重要なアーキテクチャパターン

### 1. 部分クラス分割（300行制約）

複雑なクラスは部分クラスで分割されています：

**例**: `MainWindowRenderer`
- `MainWindowRenderer.cs` - コアロジック
- `MainWindowRenderer.Layout.cs` - レイアウト処理
- `MainWindowRenderer.Tabs.cs` - タブ切り替え

**例**: `DalamudUiSubmarineSnapshotSource`
- `DalamudUiSubmarineSnapshotSource.cs` - メイン
- `DalamudUiSubmarineSnapshotSource.RowParsing.cs` - 行解析
- `DalamudUiSubmarineSnapshotSource.RowParsing.Helpers.cs` - ヘルパー
- `DalamudUiSubmarineSnapshotSource.RowExtraction.cs` - 行抽出

部分クラスを編集する際は、関連するすべてのファイルを確認してください。

### 2. Discord通知の4隻バッチング

**実装**: `src/Application/Notifications/DiscordCycleNotificationAggregator.cs`

出航通知を4隻まで集約して一括送信します（1.5秒以内の出航をグループ化）。これによりDiscord通知スパムを防ぎ、可読性を向上させています。

### 3. Dalamud制約への対応

#### unsafeコード
ゲームメモリアクセスには`unsafe`キーワードとポインタ操作が必要です：
```csharp
public sealed unsafe partial class DalamudMemorySubmarineSnapshotSource
{
    var manager = HousingManager.Instance();
    var subData = dataPointers[slot].Value;
    // ...
}
```

#### メインスレッド同期
ImGui UI操作は必ずメインスレッドで実行：
```csharp
// DalamudMainThreadDispatcher を使用
this.dispatcher.Enqueue(() => {
    // UI更新処理
});
```

### 4. リトライとデッドレター

**実装**: `src/Integrations/Notifications/NotificationRetryHelper.cs`

通知送信失敗時は指数バックオフ（1秒、2秒、4秒）でリトライ。3回失敗でデッドレター登録し、UIから手動再送可能。

### 5. 低負荷モード制御

**実装**: `src/Application/Services/LowImpactModeController.cs`

- 初期: 2秒周期でデータ取得（高頻度）
- 安定後: 10秒周期に自動移行（低負荷）

## データフロー

### データ取得
```
ゲームUI/メモリ
    ↓
IDataSource (UiDataSource / MemoryDataSource)
    ↓
SnapshotOrchestrator (調整・差分検出)
    ↓
CharacterSnapshotAggregator
    ↓
SnapshotPersister
    ↓
SnapshotStorageService (JSON永続化)
```

### 通知
```
VoyageCompletionProjection (航海完了検出)
    ↓
NotificationCoordinator (通知調整)
    ↓
DiscordCycleNotificationAggregator (4隻集約)
    ↓
NotificationWorker (バッチング)
    ↓
DiscordWebhookClient
```

## ブランチ戦略

- **`main`** - 開発兼公開用（タグでリリース）
- **`feature/*`** - 機能ブランチ（プッシュ時CI）

### リリースフロー

```bash
# 1. feature ブランチで作業
git checkout -b feature/release-vX.Y.Z
# バージョン更新、CHANGELOG更新など
git commit -am "release: prepare vX.Y.Z"
git push origin feature/release-vX.Y.Z

# 2. PR 作成 & マージ
gh pr create --base main
# (Review & Merge)

# 3. タグ作成 & リリース
git checkout main
git pull
git tag -a vX.Y.Z -m "Release vX.Y.Z"
git push origin vX.Y.Z
gh release create vX.Y.Z --generate-notes
```

## 依存関係

### Dalamud関連（ローカル参照）
Dalamud DLLは`vendor/Dalamud/`に配置し、`Local.props`でパスを指定：
```xml
<DalamudLibPath>$(MSBuildThisFileDirectory)vendor\Dalamud\</DalamudLibPath>
```

**自動取得スクリプト**: `tools/DalamudRestore/restore.ps1`
```bash
pwsh tools/DalamudRestore/restore.ps1
```

### プロジェクト参照
- `Dalamud.dll`
- `FFXIVClientStructs.dll`
- `Lumina.dll`, `Lumina.Excel.dll`
- `Dalamud.Bindings.ImGui.dll`

## テストアプローチ

### 単体テスト（xUnit）
- 場所: `tests/XIVSubmarinesRewrite.Tests/`
- カバレッジ目標: 90%以上
- 命名規則: `MethodName_Scenario_ExpectedBehavior`

### E2Eテスト（Playwright）
- 場所: `tests/Playwright/`
- タグ: `@theme`, `@notification`, `@overview`, `@dev`, `@release`
- RendererPreview: UI検証用の独立実行環境

## 設定と永続化

### 設定ファイル
**場所**: `%APPDATA%\XIVLauncher\pluginConfigs\XIVSubmarinesRewrite\`

- `NotificationSettings.json` - 通知設定
- `UiPreferences.json` - UI設定
- `snapshots/` - キャラクタースナップショット

**実装**: `src/Infrastructure/Configuration/DalamudJsonSettingsProvider.cs`

## 既知の制約

1. **航路名は内部ID** - 正式名称未対応（RouteCatalogで部分マッピング）
2. **ForceNotifyUnderwayは開発用** - 本番環境では非推奨
3. **メモリデータソースは部分実装** - UIデータソースが主力

## トラブルシューティング

### ビルドエラー
- Dalamud DLL参照エラー → `tools/DalamudRestore/restore.ps1` 実行
- NuGetパッケージエラー → `dotnet restore` 実行

### 実行時エラー
- プラグインが読み込まれない → `manifest.json`のDalamudApiLevelを確認
- UI描画エラー → ImGuiのメインスレッド制約を確認
- 通知送信失敗 → NotificationMonitorウィンドウのデッドレターを確認

### デバッグ
1. `%APPDATA%\XIVLauncher\dalamud.log` を確認
2. LogLevelをDebugに変更して詳細ログを取得
3. Visual Studio Debugger でアタッチ（プロセス: ffxiv_dx11.exe）

## 開発時の注意点

### ImGui UI開発
- ウィンドウサイズ制約: 640x420 ～ 1100x860px
- カラーテーマ: `UiTheme` の18色パレット（WCAG 2.1準拠）
- レスポンシブデザイン: OverviewウィンドウはWide/Medium/Compactの3段階

### 通知システム
- 出航通知のみ（航海完了通知はv1.1.5で廃止）
- 4隻バッチングを維持
- Discord Reminder Bot連携をサポート

### コーディング規約
- 1ファイル300行以下（部分クラスで分割）
- コンベンショナルコミット（feat:, fix:, docs:, etc.）
- 日本語コミットメッセージ
- 「🤖 Generated with [Claude Code]」フッター

## 参考リンク

- **プロジェクトメモリ**: `plans/specs/steering/` (main ブランチ)
- **AI開発ガイド**: `docs/ai-development/` (main ブランチ)
- **リリース手順**: `docs/release/` (main ブランチ)
- **Dalamud Docs**: https://goatcorp.github.io/Dalamud/

---

**このファイルはmainブランチで管理されています。**