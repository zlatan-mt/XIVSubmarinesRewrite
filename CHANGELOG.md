<!-- apps/XIVSubmarinesRewrite/CHANGELOG.md -->
<!-- XIV Submarines Rewrite のバージョン履歴を記録します -->
<!-- リリースノートと変更内容を時系列で追跡するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/manifest.json, apps/XIVSubmarinesRewrite/README.md, apps/XIVSubmarinesRewrite/plugin.json -->

# Changelog

All notable changes to XIV Submarines Rewrite will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

_No unreleased changes._

## [1.1.0] - 2025-10-02

### Fixed

- **Dalamud 配信互換性**: manifest / plugin / repo メタデータのアイコン参照を絶対 URL 化し、Dalamud プラグインインストーラで発生していた `Invalid request URI` 例外を修正。

### Changed

- **リリースメタデータ更新**: Dalamud 向け配布 URL を v1.1.0 用に更新し、`repo.json` から最新版 ZIP を直接取得できるよう調整。
- **プロジェクトバージョン更新**: アセンブリ / ファイル / InformationalVersion を 1.1.0 系列へ引き上げ。
- **リポジトリ公開方針整備**: 利用者向けのクリーンな構成と README 改善を反映。

## [1.0.0] - 2025-10-02

### Added

#### Phase7: UI 磨き込みとテスト基盤
- **統合メインウィンドウ**: Overview、Notification、Developer タブを単一ウィンドウで管理
- **UiTheme カラーパレット**: 黒・白・ディープブルー基調の18色パレット（DEV用5色含む）
- **配色コントラスト検証**: WCAG 2.1 基準の自動計算とツールバー表示
- **通知フォームUI改善**:
  - Discord/Notion URL のリアルタイムバリデーション
  - 2列/1列レスポンシブレイアウト（幅820px閾値）
  - バッチ間隔とデッドレター保持数のスライダー操作
  - 保存パネル分離と無効入力時の保存ブロック
- **Overview レスポンシブ対応**:
  - 3段階の列幅プリセット（Wide/Medium/Compact）
  - コンパクト表示モード（残り時間と到着時刻の短縮表示）
  - 航路名の自動折り返し（ゼロ幅スペース挿入）
  - コピー時のゼロ幅スペース除去
- **Playwright E2E テスト**:
  - 17ケースの自動テスト（@theme, @notification, @overview, @main-window, @dev）
  - RendererPreview による配色スウォッチ生成と検証
  - GitHub Actions CI 統合

#### Phase8: 開発者ツールと CI ハードニング
- **DEV タブ完全実装**:
  - ForceNotifyUnderway トグル（出航中通知の開発モード）
  - 手動送信ボタンと通知キュー追加
  - トースト通知（3秒間フェードアウト）
  - 手動送信ログの ImGui Table 表示（最新10件）
  - DEV サマリーバナー（メインウィンドウ上部に最終操作概要）
  - オレンジ色の警告バナーとアクセントカラー
- **DalamudRestore 自動化**:
  - 公式ディストリビューション URL から最新版を自動ダウンロード
  - SHA256 ハッシュ検証（改ざん検出）
  - vendor ディレクトリへのフォールバック
  - PowerShell/Bash 両対応
- **GitHub Actions 最適化**:
  - Dalamud DLL キャッシュ（2回目以降のビルド高速化）
  - アーティファクト保存（DLL, Playwright レポート）
  - 統合レポート構造（test-results, playwright-report, .artifacts）
  - 保存期間設定（DLL: 7日、レポート: 14日）
- **コード品質改善**:
  - MainWindowRenderer を3つの部分クラスに分割（300行制限遵守）
  - NotificationMonitorWindowRenderer の部分クラス化
  - OverviewRowFormatter ユーティリティの新設

#### Phase9: リリース準備と QA
- **リリースパッケージング**:
  - verify.yml にリリース候補 ZIP 生成機能を追加
  - ビルド情報（日時・コミット・ブランチ）を build-info.txt に記録
  - GitHub Actions Artifacts で30日間保管
- **配色サマリーエクスポート**:
  - RendererPreview に color-summary.json 出力機能を追加
  - 色の統計情報（総数・背景色・テキスト色・DEV色）をJSON化
  - CI でアーティファクト化し、デザインレビューに活用
- **@release E2Eテスト新設**:
  - リリース候補検証用の7ケーステスト追加
  - タブナビゲーション・機能アクセス・設定保存などを簡易検証
  - npm run test:ui:release で実行可能

#### Phase10: UI リファイン & キャラクタ永続化調整
- **タイトルバー UI 改善**:
  - ツールバー高さを72pxに拡張し、UI スケーリング対応を追加
  - アイコン・テキスト・バージョン表示のレイアウトを分離（DrawBrandSection）
  - バージョンラベルの長い場合の省略表示とツールチップ機能を実装
  - カラーコントラスト情報を2行レイアウトに整理（T:4.5 W:4.2形式）
  - 文字クリップ防止のため FramePadding と ItemSpacing をスケール対応
- **キャラクタ永続化の最適化**:
  - RegisterSnapshot() に潜水艦操作履歴のガード条件を追加
  - 潜水艦操作したキャラクターのみを永続化対象とする仕様に変更
  - 起動時のクリーンアップ機能（CleanupCharactersWithoutSubmarineOperations）
  - 潜水艦操作キャラクター一覧取得 API（GetCharactersWithSubmarineOperations）
  - 不要なキャラクター表示を防止し、データベースサイズを最適化

### Changed
- **メインウィンドウサイズ制約**: 640x420 ～ 1100x860 ピクセル
- **自動更新間隔**: 初期2秒 → 安定後10秒
- **通知ログ保存**: `logs/<date>/dev-panel` に最大10件の JSON
- **ファイル構成**: 全ファイル300行以下を維持

### Fixed
- readonly struct のフィールド初期化エラー（NotificationLayoutMetrics）
- ImGuiChildFlags の互換性問題（Dalamud Bindings ImGui）
- localStorage フォールバック処理（Playwright テスト安定化）

### Technical Details
- **.NET**: 9.0-windows
- **Dalamud API Level**: 13
- **テストカバレッジ**: Playwright 17ケース、.NET ユニットテスト9ケース
- **CI ワークフロー**: Verify（全ブランチ）、UI Tests（main/PR）

### Documentation
- README.md: DEV タブの使い方、Playwright タグ実行、CI 手順
- Local.props.example: ローカル開発環境セットアップガイド
- vendor/Dalamud/README.md: DLL 配置とフォールバック手順
- plans/: Phase7-9 実行計画（詳細チェックリスト付き）

### Known Limitations
- 再構築中のため UI や文面は予告なく変わる可能性があります
- 航路名は内部 ID を簡易整形した表示であり正式名称ではありません
- ForceNotifyUnderway は開発用機能のため本番環境での使用は推奨しません

### Migration Notes
- 初回インストール: `tools/DalamudRestore/restore.ps1` を実行してDLL取得
- CI 環境: `actions/cache@v4` により2回目以降のビルドが高速化されます
- 既存設定: UiPreferences に DevHistory が追加されますが後方互換性を保持

---

## Development History

### Phase Completion Status
- ✅ Phase7-A: UiTheme パレットと配色検証
- ✅ Phase7-B: Notification フォーム安定化
- ✅ Phase7-C: Overview 体験向上
- ✅ Phase7-D: CI 強化と Playwright 統合
- ✅ Phase8-A: DalamudRestore 自動化
- ✅ Phase8-B: DEV タブ UX 仕上げ
- ✅ Phase8-C: MainWindowRenderer 分割
- ✅ Phase8-D: CI ハードニング仕上げ
- ✅ Phase9-A: リリースノート & ドキュメント整備
- ✅ Phase9-B: 配布パッケージ検証と QA
- ✅ Phase10-A: タイトルバー UI リファイン
- ✅ Phase10-B: キャラクタ保存条件の絞り込み

### Contributors
- **MonaT**: 設計・実装・テスト・ドキュメント

### Acknowledgments
- Dalamud Team: API と開発環境の提供
- goatcorp: Dalamud ディストリビューション
- XIV Submarines コミュニティ: フィードバックと支援

---

[Unreleased]: https://github.com/mona-ty/XIVSubmarinesRewrite/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/mona-ty/XIVSubmarinesRewrite/releases/tag/v1.0.0

