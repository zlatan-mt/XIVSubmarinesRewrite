# 技術スタック - XIV Submarines Rewrite

## プラットフォーム

### Dalamud プラグイン
- **Dalamud API Level**: 13
- **ターゲット OS**: Windows
- **ランタイム**: .NET 9.0-windows
- **アーキテクチャ**: x64

## 言語とフレームワーク

### C# (.NET 9.0)
- **言語バージョン**: C# 12
- **Nullable Reference Types**: 有効化
- **Implicit Usings**: 有効化
- **Unsafe Code**: 許可（Dalamud FFI用）

### 主要ライブラリ
- **Dalamud.dll**: Dalamud API
- **Dalamud.Bindings.ImGui.dll**: UI フレームワーク
- **FFXIVClientStructs.dll**: ゲームメモリアクセス
- **Lumina.dll / Lumina.Excel.dll**: ゲームデータアクセス

## アーキテクチャパターン

### レイヤー構成
```
Presentation (UI)
  ↓
Application (Services, Commands, Queries)
  ↓
Domain (Models, Business Logic)
  ↓
Infrastructure (External Services, Storage)
```

### 主要コンポーネント

#### Presentation Layer
- `MainWindowRenderer`: メインウィンドウ描画（3部分クラス）
  - `MainWindowRenderer.Toolbar.cs`: ツールバー
  - `MainWindowRenderer.Tabs.cs`: タブ切り替え
  - `MainWindowRenderer.Content.cs`: コンテンツ描画
- `NotificationMonitorWindowRenderer`: 通知モニター
- `OverviewRowFormatter`: Overview行フォーマット

#### Application Layer
- `SnapshotOrchestrator`: データ取得の調整
- `NotificationCoordinator`: 通知制御
- `CharacterRegistry`: キャラクター管理
- `LowImpactModeController`: 低負荷モード制御

#### Domain Layer
- `Submarine`: 潜水艦エンティティ
- `Voyage`: 航海エンティティ
- `Profile`: キャラクタープロファイル
- `Route`: 航路データ

#### Infrastructure Layer
- `DalamudUiSubmarineSnapshotSource`: UI データソース
- `DalamudMemorySubmarineSnapshotSource`: メモリデータソース
- `SnapshotStorageService`: ローカルストレージ
- `DiscordNotificationBatcher`: Discord 通知送信
- `NotionWebhookClient`: Notion 通知送信

## データフロー

### データ取得フロー
```
ゲームUI / メモリ
  ↓ (DalamudUiSubmarineSnapshotSource / DalamudMemorySubmarineSnapshotSource)
SnapshotOrchestrator
  ↓ (CharacterSnapshotAggregator)
SnapshotPersister
  ↓
SnapshotStorageService (JSON)
```

### 通知フロー
```
VoyageCompletionProjection
  ↓
NotificationCoordinator
  ↓
DiscordCycleNotificationAggregator (4隻集約)
  ↓
NotificationWorker (バッチング)
  ↓
DiscordNotificationBatcher / NotionWebhookClient
```

## UI フレームワーク

### ImGui（Dalamud.Bindings.ImGui）
- **描画周期**: 毎フレーム（60fps目標）
- **ウィンドウサイズ**: 640x420 ～ 1100x860 px
- **レスポンシブ**: 幅820px閾値で2列/1列切り替え
- **テーマ**: UiTheme 18色パレット（WCAG 2.1準拠）

### UI 更新戦略
- 初期更新: 2秒周期
- 安定後: 10秒周期
- 強制更新: ユーザー操作時

## データ永続化

### ローカルストレージ
- **形式**: JSON
- **場所**: Dalamud プラグイン設定ディレクトリ
- **スキーマバージョン**: 1.0
- **マイグレーション**: SnapshotStorageMigrator

### 保存対象
- キャラクタースナップショット（潜水艦操作履歴あり）
- UI 設定（UiPreferences）
- 通知設定（NotificationSettings）
- 開発者履歴（DevHistory）

## 通知システム

### Discord Webhook
- **集約方式**: 4隻分をバッチ送信
- **リトライ**: 指数バックオフ（最大3回）
- **タイムアウト**: 10秒
- **デッドレター**: 失敗通知を記録・再送可能

### Notion Webhook
- **送信方式**: 個別送信
- **リトライ**: 同上
- **タイムアウト**: 10秒

## テスト戦略

### 単体テスト（xUnit）
- **フレームワーク**: xUnit
- **モック**: Moq
- **カバレッジ**: 90%以上必須
- **命名**: `MethodName_Scenario_ExpectedBehavior`

### E2Eテスト（Playwright）
- **フレームワーク**: Playwright（TypeScript）
- **テストタグ**:
  - `@theme`: カラーテーマ
  - `@notification`: 通知フォーム
  - `@overview`: Overview表示
  - `@dev`: 開発者ツール
  - `@release`: リリース候補検証

### テストファイル構成
```
tests/
├── XIVSubmarinesRewrite.Tests/  (.NET 単体テスト)
│   ├── DevNotificationPanelTests.cs
│   ├── DiscordCycleNotificationAggregatorTests.cs
│   ├── NotificationCoordinatorForceImmediateTests.cs
│   └── UiThemeContrastTests.cs
└── Playwright/  (E2E UIテスト)
    ├── dev-tools.spec.ts
    ├── main-window.spec.ts
    ├── notification-layout.spec.ts
    ├── overview-responsive.spec.ts
    └── release-smoke.spec.ts
```

## CI/CD

### GitHub Actions
- **ワークフロー**:
  - `verify.yml`: ビルド＋単体テスト＋Playwright
  - `ui-tests.yml`: UIテスト専用
  - `release.yml`: タグプッシュ時の自動リリース

- **ブランチ戦略**:
  - `develop`: 開発用（全ファイル、自動CI）
  - `release`: 公開用（クリーン構成、自動CI）
  - `feature/*`: 機能ブランチ（プッシュ時CI）

- **アーティファクト**:
  - ビルドDLL（7日保存）
  - Playwrightレポート（14日保存）
  - リリース候補ZIP（30日保存）

## パフォーマンス要件

### UI
- **フレームレート**: 60fps維持
- **描画時間**: < 16.7ms / frame
- **ウィンドウリサイズ**: 遅延なし

### データ取得
- **UI スナップショット**: < 100ms
- **メモリスナップショット**: < 50ms
- **差分検出**: < 10ms

### 通知
- **バッチング遅延**: 最大5秒
- **送信タイムアウト**: 10秒
- **リトライ間隔**: 1秒、2秒、4秒（指数バックオフ）

## セキュリティ

### 認証情報管理
- Webhook URL は暗号化せず平文保存（ローカルのみ）
- 環境変数での上書き非対応（プラグイン特性上）

### 入力検証
- Webhook URL: 形式検証のみ（HTTP/HTTPS）
- ユーザー入力: ImGui側で制限

## 既知の技術的制約

### Dalamud プラグイン制約
- **メインスレッド**: UI操作は必ずメインスレッドで実行
- **リソース管理**: Dispose パターン必須
- **プラグインライフサイクル**: 初期化→有効化→無効化→破棄

### .NET 制約
- Windows専用（.NET 9.0-windows）
- x64アーキテクチャのみ
- Unsafe コード使用（FFI用）

## 開発ツール

### ビルドツール
- **DalamudPackager**: 13.1.0
- **dotnet**: 9.0.x

### 外部ツール
- **DalamudRestore**: Dalamud DLL自動取得スクリプト
- **RendererPreview**: カラーパレット検証ツール

## 技術的負債

### 現在の負債
1. 航路名が内部IDの簡易整形（正式名称未対応）
2. ForceNotifyUnderway は開発用（本番非推奨）

### 将来の改善候補
1. メモリデータソースの完全実装
2. 航路マスタデータの取得・表示
3. 通知テンプレートのカスタマイズ機能

