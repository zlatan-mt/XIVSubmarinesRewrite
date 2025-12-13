# プロジェクト構造 - XIV Submarines Rewrite

## ディレクトリ構成

```
XIVSubmarinesRewrite/
├── src/                           # ソースコード
│   ├── Acquisition/               # データ取得層
│   ├── Application/               # アプリケーション層
│   ├── Domain/                    # ドメイン層
│   ├── Infrastructure/            # インフラ層
│   ├── Integrations/              # 外部連携
│   ├── Presentation/              # UI層
│   └── Plugin.cs                  # エントリポイント
├── tests/                         # テスト
│   ├── XIVSubmarinesRewrite.Tests/  # .NET単体テスト
│   └── Playwright/                # E2E UIテスト
├── plans/                         # 開発計画（developのみ）
│   ├── phase01_*.md               # 既存Phase計画
│   └── specs/                     # cc-sdd仕様書（新規）
│       ├── requirements/          # 要件定義
│       ├── design/                # 設計書
│       ├── tasks/                 # タスク分解
│       ├── steering/              # プロジェクトメモリ
│       └── settings/              # cc-sdd設定
├── .cursor/commands/kiro/         # Cursor用コマンド
├── .claude/commands/kiro/         # Claude Code用コマンド
├── .codex/prompts/                # Codex用プロンプト
├── .github/workflows/             # CI/CD
├── tools/                         # 開発ツール
│   ├── DalamudRestore/            # Dalamud DLL取得
│   └── RendererPreview/           # UI検証ツール
├── vendor/                        # 依存DLL
├── Properties/                    # プロジェクト設定
├── manifest.json                  # Dalamud マニフェスト
├── plugin.json                    # プラグインメタデータ
├── repo.json                      # 配布用リポジトリ定義
├── XIVSubmarinesRewrite.csproj    # プロジェクトファイル
├── AGENTS.md                      # Cursor/Codex用ガイド
├── CLAUDE.md                      # Claude Code用ガイド
├── README.md                      # プロジェクト概要
└── CHANGELOG.md                   # 変更履歴
```

## ソースコード構成

### src/Acquisition/ - データ取得層
```
Acquisition/
├── DataAcquisitionGateway.cs      # データ取得ゲートウェイ
├── SnapshotOrchestrator.cs        # スナップショット調整
├── CharacterSnapshotAggregator.cs # キャラクター集約
├── SnapshotPersister.cs           # 永続化
├── SnapshotCache.cs               # キャッシュ
├── SnapshotDiffer.cs              # 差分検出
├── IDataSource.cs                 # データソースインターフェース
├── MemoryDataSource.cs            # メモリデータソース
└── UiDataSource.cs                # UIデータソース
```

### src/Application/ - アプリケーション層
```
Application/
├── Commands/                      # コマンドパターン
│   └── ICommand.cs
├── Queries/                       # クエリパターン
│   └── IQuery.cs
├── Services/                      # アプリケーションサービス
│   ├── NotificationCoordinator.cs
│   ├── CharacterRegistry.cs
│   └── LowImpactModeController.cs
├── Notifications/                 # 通知システム
│   ├── VoyageCompletionProjection.cs
│   ├── DiscordCycleNotificationAggregator.cs
│   ├── DiscordNotificationBatcher.cs
│   ├── NotificationWorker.cs
│   ├── NotificationEnvelope.cs
│   └── NotificationDeliveryRecord.cs
└── Messaging/
    └── IEventBus.cs
```

### src/Domain/ - ドメイン層
```
Domain/
├── Models/                        # ドメインモデル
│   ├── Submarine.cs               # 潜水艦
│   ├── SubmarineId.cs             # 潜水艦ID
│   ├── Voyage.cs                  # 航海
│   ├── VoyageId.cs                # 航海ID
│   ├── Route.cs                   # 航路
│   ├── Profile.cs                 # プロファイル
│   └── Alarm.cs                   # アラーム
└── Repositories/                  # リポジトリインターフェース
    ├── ISubmarineRepository.cs
    ├── IVoyageRepository.cs
    ├── IRouteRepository.cs
    └── IAlarmRepository.cs
```

### src/Infrastructure/ - インフラ層
```
Infrastructure/
├── Acquisition/                   # データ取得実装
│   ├── DalamudUiSubmarineSnapshotSource.cs
│   ├── DalamudUiSubmarineSnapshotSource.*.cs (部分クラス)
│   └── DalamudMemorySubmarineSnapshotSource.cs
├── Configuration/                 # 設定管理
│   ├── DalamudJsonSettingsProvider.cs
│   ├── InMemorySettingsProvider.cs
│   ├── NotificationSettings.cs
│   └── UiPreferences.cs
├── Persistence/                   # 永続化
│   ├── SnapshotStorageService.cs
│   ├── SnapshotStorageModel.cs
│   └── SnapshotStorageMigrator.cs
├── Repositories/                  # リポジトリ実装
│   ├── InMemorySubmarineRepository.cs
│   ├── InMemoryVoyageRepository.cs
│   ├── InMemoryRouteRepository.cs
│   └── InMemoryAlarmRepository.cs
├── Diagnostics/                   # 診断・監視
│   ├── DefaultAcquisitionTelemetry.cs
│   └── IPerformanceMonitor.cs
├── Logging/                       # ログ
│   ├── DalamudPluginLogSink.cs
│   └── ILogSink.cs
├── Messaging/                     # メッセージング
│   └── InMemoryEventBus.cs
├── Notifications/                 # 通知実装
│   ├── DalamudMainThreadDispatcher.cs
│   └── InMemoryNotificationQueue.cs
├── Routes/                        # 航路データ
│   └── RouteCatalog.cs
└── Composition/                   # DI構成
    ├── PluginBootstrapper.cs
    └── ServiceRegistry.cs
```

### src/Integrations/ - 外部連携
```
Integrations/
└── Notifications/
    ├── DiscordNotificationBatcher.cs
    ├── DiscordWebhookContract.cs
    └── NotificationWebhookValidator.cs
```

### src/Presentation/ - UI層
```
Presentation/
├── Rendering/                     # レンダリング
│   ├── MainWindowRenderer.cs      # メインウィンドウ
│   ├── MainWindowRenderer.Toolbar.cs
│   ├── MainWindowRenderer.Tabs.cs
│   ├── MainWindowRenderer.Content.cs
│   ├── NotificationMonitorWindowRenderer.cs
│   ├── NotificationFormRenderer.cs
│   ├── OverviewRenderer.cs
│   ├── OverviewRowFormatter.cs
│   ├── DevNotificationPanelRenderer.cs
│   ├── UiTheme.cs                 # カラーテーマ
│   └── UiThemeContrast.cs         # コントラスト計算
├── ViewModels/                    # ビューモデル
│   ├── MainWindowViewModel.cs
│   ├── NotificationViewModel.cs
│   ├── OverviewViewModel.cs
│   ├── DevPanelViewModel.cs
│   └── UiPreferences.cs
└── UIStateObserver.cs             # UI状態監視
```

## テスト構成

### tests/XIVSubmarinesRewrite.Tests/ - .NET単体テスト
```
XIVSubmarinesRewrite.Tests/
├── DevNotificationPanelTests.cs
├── DiscordCycleNotificationAggregatorTests.cs
├── NotificationCoordinatorForceImmediateTests.cs
├── NotificationWebhookValidatorTests.cs
├── OverviewRowFormatterTests.cs
├── UiThemeContrastTests.cs
└── UiThemePaletteTests.cs
```

### tests/Playwright/ - E2E UIテスト
```
Playwright/
├── dev-tools.spec.ts              # @dev
├── main-window.spec.ts            # @main-window
├── notification-layout.spec.ts    # @notification
├── overview-responsive.spec.ts    # @overview
├── release-smoke.spec.ts          # @release
├── ui-theme.spec.ts               # @theme
├── fixtures/
│   └── main-window-fixture.ts
├── utils/
│   └── renderer-preview.ts
├── playwright.config.ts
└── package.json
```

## 設定ファイル

### ビルド・プロジェクト設定
- `XIVSubmarinesRewrite.csproj`: MSBuildプロジェクトファイル
- `DalamudPackager.targets`: Dalamudパッケージング
- `Local.props.example`: ローカル開発環境設定例

### Dalamud配布設定
- `manifest.json`: Dalamud マニフェスト（埋め込みリソース）
- `plugin.json`: プラグインメタデータ
- `repo.json`: 配布リポジトリ定義

### AI開発支援
- `.cursor/commands/kiro/*.md`: Cursor用コマンド（11個）
- `.claude/commands/kiro/*.md`: Claude Code用コマンド（11個）
- `.codex/prompts/*.md`: Codex用プロンプト（11個）
- `plans/specs/settings/`: cc-sdd設定・テンプレート

### CI/CD
- `.github/workflows/verify.yml`: ビルド＋テスト
- `.github/workflows/ui-tests.yml`: UIテスト専用
- `.github/workflows/release.yml`: リリース自動化

## 命名規則

### C# コード
- **名前空間**: `XIVSubmarinesRewrite.<Layer>.<Feature>`
- **クラス**: PascalCase（例: `NotificationCoordinator`）
- **インターフェース**: `I` + PascalCase（例: `IDataSource`）
- **メソッド**: PascalCase（例: `RegisterSnapshot`）
- **プライベートフィールド**: `_camelCase`（例: `_logger`）
- **定数**: SCREAMING_SNAKE_CASE（例: `MAX_RETRY_COUNT`）

### ファイル
- **C#ファイル**: クラス名.cs（例: `Plugin.cs`）
- **部分クラス**: クラス名.部分名.cs（例: `MainWindowRenderer.Toolbar.cs`）
- **テスト**: クラス名Tests.cs（例: `UiThemeContrastTests.cs`）

### ディレクトリ
- **レイヤー**: PascalCase（例: `Application/`）
- **機能**: PascalCase（例: `Notifications/`）
- **cc-sdd**: kebab-case（例: `plans/specs/`）

## ブランチ戦略

### ブランチ構成
- **`main`**: 開発兼公開用（タグでリリース）
- **`feature/*`**: 機能ブランチ

### マージフロー
```
feature/* → main (Squash Merge)
main → タグ → GitHub Release
```

## ファイルサイズ制約

### コード品質ルール
- **1ファイル**: 300行以下を維持
- **過大ファイル**: 部分クラス (`partial class`) で分割
- **責務分離**: 単一責任原則（SRP）を遵守

### 例外
- 自動生成ファイル: 制約なし
- テストデータ: 制約なし
- 外部ライブラリ: 制約なし

## 依存関係管理

### NuGet パッケージ
- `DalamudPackager`: 13.1.0

### ローカル参照（vendor/）
- Dalamud.dll
- Dalamud.Bindings.ImGui.dll
- FFXIVClientStructs.dll
- InteropGenerator.Runtime.dll
- Lumina.dll
- Lumina.Excel.dll

### npm パッケージ（テスト用）
- `@playwright/test`: ^1.45.0

## ドキュメント構成

### ユーザー向け（releaseブランチに含める）
- `README.md`: プロジェクト概要、インストール手順、使用方法
- `CHANGELOG.md`: バージョン履歴（Keep a Changelog形式）
- `RELEASE_NOTES_v*.md`: リリースノート（最新版のみ）

### 開発者向け（developブランチのみ）
- `docs/ai-development/`: AI開発支援ドキュメント
  - `AGENTS.md`: Cursor/Codex統合ガイド
  - `CLAUDE.md`: Claude Code統合ガイド
  - `QUICKSTART.md`: クイックスタートガイド
- `docs/release/`: リリース作業ドキュメント
  - `github-release.md`: GitHub Release作成手順
  - `release-checklist.md`: リリースチェックリスト
- `plans/`: Phase計画とcc-sdd仕様書
  - `phase*.md`: Phase計画
  - `*completion*.md`: Phase完了レポート
  - `*installation*.md`: インストール計画
  - `v*.md`: リリース準備サマリー
  - `specs/`: cc-sdd仕様書（requirements, design, tasks, steering）

### ドキュメント分類ルール
- **ユーザー向け**: ルートディレクトリに配置、releaseブランチに含める
- **開発者向け**: `docs/`配下に配置、developブランチのみ
- **開発計画**: `plans/`配下に配置、developブランチのみ
- **一時的ドキュメント**: Phase完了レポート等は`plans/`配下に配置

### リリース向け（補足）
- `build-info.txt`: ビルド情報（CI生成、release-package/に含まれる）