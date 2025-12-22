<!-- apps/XIVSubmarinesRewrite/CHANGELOG.md -->
<!-- XIV Submarines Rewrite のバージョン履歴を記録します -->
<!-- リリースノートと変更内容を時系列で追跡するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/manifest.json, apps/XIVSubmarinesRewrite/README.md, apps/XIVSubmarinesRewrite/plugin.json -->

# Changelog

All notable changes to XIV Submarines Rewrite will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

- （未記載）

## [1.4.0] - 2025-12-22

### Changed

- **Dalamud API 14 への移行**:
  - TargetFramework を `net10.0-windows` に更新
  - DalamudPackager を 14.0.1 に更新
  - `IClientState.LocalContentId` → `IPlayerState.ContentId` に移行
  - `IClientState.LocalPlayer` → `IObjectTable.LocalPlayer` に移行
  - Plugin.cs, CharacterRegistry, DalamudUiSubmarineSnapshotSource, DalamudMemorySubmarineSnapshotSource を更新
  - PluginBootstrapper に IPlayerState, IObjectTable を追加

### Technical Details

- 調査ノート: `plans/api14-investigation-notes.md`
- 移行計画: `plans/api14-migration-plan.md`
- 安全タグ: `v1.3.0-pre-api14-migration`

## [1.3.0] - 2025-12-08

### Removed

- **Notion 通知機能の削除**: 利用頻度が低く正常に動作していなかったため、Notion 通知機能を完全に削除しました。
  - `NotionWebhookClient`, `INotionClient` を削除
  - UI から Notion 設定カードを削除
  - 関連テスト・ドキュメントを整理

## [1.2.5] - 2025-11-18

### Changed

- **本番ログの静穏化（継続）**: v1.2.3 の方針に沿って、7箇所の `Information` ログを `Debug` レベルに変更し、通常運用時のログノイズを大幅に削減しました。
  - Discord 集約完了ログ (`DiscordCycleNotificationAggregator.cs:212`) → Debug（重複検出は既存の Warning ログで担保）
  - Discord バッチ処理ログ (`DiscordNotificationBatcher.cs:43, 176`) → Debug（性能計測ログは開発時のみ必要）
  - Notion 送信成功ログ (`NotionWebhookClient.cs:82`) → Debug（正常系の高頻度ログ）
  - プラグイン初期化ログ (`Plugin.cs:52`) → Debug（開発者向けデバッグ情報）
  - UI リスナー登録ログ (`DalamudUiSubmarineSnapshotSource.Lifecycle.cs:68, 139`) → Debug（起動時の開発者向け情報）

### Technical Details

- 整合性監視（重複検出）は `DiscordCycleNotificationAggregator.cs:171` の `LogLevel.Warning` で引き続き維持されます。
- 関連ユニットテストのログレベル検証を `Debug` に更新（`NotionWebhookContractTests`, `DiscordCycleNotificationAggregatorTests`）。
- 詳細な分析レポート: `log_analysis_report_v1.2.4.md`

## [1.2.4] - 2025-11-16

### Fixed

- **帰港状態の潜水艦が UI フィルタで除外される問題の修正**：英語クライアントで「Return from voyage」ステータスを持つ潜水艦がフィルタスコアリングで除外される問題を修正しました。
  - `NegativeRowKeywords` から "return" キーワードを削除（正当なステータス語として認識）
  - ペナルティロジックを `if (!hasRoute && !hasStatus)` に簡素化し、コード意図を明確化
  - ステータスのみの行（「帰港」「探索完了」「Return from voyage」）が正しくスコア3以上を獲得することを保証

### Added

- **UI フィルタリングのユニットテスト追加**：`RowConfidenceTests.cs` を新設し、ステータスのみの行が正しく受理されることを検証
  - 日本語・英語クライアントのステータス行をカバー
  - リテイナー行の誤検出防止テストも追加

### Technical Details

- `DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers.cs` でキーワード定義とスコアリング条件を修正
- `ComputeConfidenceScoreForTest` ヘルパーでテストからスコア計算ロジックにアクセス可能に
- テストプロジェクトのビルドエラー（重複 Compile アイテム、数値リテラルサフィックス）を修正

## [1.2.3] - 2025-11-16

### Changed

- **本番ログの静穏化**: ゴーストデータ除去の WARN を INFO へ引き下げ、起動毎の不要なアラートを防ぎました。
- **UI 名前解析ログの削除**: `ExtractName` 系の診断 DEBUG を完全に除去し、UI 操作時のログスパムを抑制しました。
- **航海完了デバッグログの削除**: 完了通知を無効化済みである旨の DEBUG 出力を取り除き、ForceNotify 以外の航海関連ログを最小化しました。

### Technical Details

- Dalamud 配布用 metadata (`manifest.json`, `plugin.json`, `repo.json`, release-package/*) と DLL バージョンを 1.2.3 へ更新。

## [1.2.2] - 2025-11-16

### Fixed

- **ゴーストデータによる潜水艦名重複の修正**：永続化キャッシュに残留していた無効なスロット（4番以降）を持つ「ゴーストデータ」が原因で、Discord通知で潜水艦名が重複する問題を修正しました。
  - プラグイン起動時にスロット0-3の範囲外の潜水艦データをクレンジング
  - メモリデータソースの名前揺らぎ抑制を強化（デバウンス機能）
  - ForceNotifyUnderway の遅延実行により、データ安定化を向上

### Technical Details

- `PluginBootstrapper` にゴーストデータフィルタリング処理を追加（slot>3を除外）
- `DalamudMemorySubmarineSnapshotSource` に名前変更デバウンス機能を実装
- 診断ログを追加し、ゴーストデータ除外とメモリ読み取りの可視性を向上
- 根本原因分析レポートを `docs/analysis/` に追加

## [1.2.1] - 2025-11-14

### Fixed

- **Voyage GUID の一意性強化**: キャラクター ID とスロット番号を含めた MD5 を使い、同じ登録/帰港時刻でも GUID が衝突しません。
- **Discord 通知安定化**: slot-aware GUID により ForceImmediate 送信や集約サイクルで誤ったデータが送出される可能性を減らしました。

### Technical Details

- 1.2.1 は 1.2.0 のソースに対する再ビルドで、最新版の manifest/plugin/repo 配布メタを反映しています。

## [1.2.0] - 2025-11-14

### Fixed

- **探索完了状態の潜水艦名抽出の修正**：探索完了状態の潜水艦で「探索完了」テキストが名前から除去されず、Discord通知などで不正な名前が表示される問題を修正しました。
  - `StatusCompletedKeywords`に「探索完了」複合語を追加
  - 単語境界を考慮したステータステキスト除去ロジックの実装
  - "MyReturn"や"Theready"のような有効な潜水艦名が誤って除外されないよう改善
  - 診断用デバッグログの追加により問題の特定を容易化

### Technical Details

- `StripStatusSuffix`ヘルパーで単語境界を考慮してステータスキーワードを除去
- `IsOnlyStatusText`ヘルパーで純粋なステータスキーワードのみを除外
- `ExtractNameCandidate`で探索完了・航海中・待機中など全ステータスに対応
- 包括的なユニットテスト（`RowParsingTests`）を追加

## [1.1.6] - 2025-01-27

### Changed

- **Discord通知レイアウトの改善**：最も遅い帰還時刻を大きく表示し、全潜水艦の帰還時刻を整列表示するように改善しました。
- **通知表示の最適化**：コードブロックを使用して等幅フォントで表示し、潜水艦名の長さに関わらず帰還時刻が縦に揃うようにしました。

### Fixed

- **重複潜水艦問題の修正**：3隻目と4隻目が同じ内容で表示される問題を修正しました。
  - Slot番号の正規化（0-3の範囲に制限）
  - 重複検出ロジックの追加と警告ログの出力
  - 詳細なデバッグログの追加（SubmarineId、Slot、HashKeyを記録）

### Added

- **デバッグ機能の強化**：Discord通知集約処理に詳細なログを追加し、問題の原因特定を容易にしました。

## [1.1.5] - 2025-10-25

### Changed

- **Discord通知の最適化**：航海完了通知を廃止し、出航時の通知のみに簡素化しました。
- **通知フォーマットの改善**：単体通知は1フィールド、バッチ通知は1行/隻の簡潔な形式に変更しました。
- **時刻表示の簡略化**：残り時間を `12h`, `30m`, `12.5h` などの読みやすい形式で表示します。

### Added

- **Discord Reminder Bot連携**：通知にリマインダーコマンドを含めることができます（オプション）。
  - 設定で「リマインダーコマンドを含める」を有効化
  - チャンネル名を設定（例: `#submarine`）
  - 生成されたコマンドをDiscordで実行すると、帰還時刻に自動リマインダー

### Deprecated

- **航海完了通知**：`NotifyVoyageCompleted` 設定は廃止されました。出航時の通知で帰還予定が分かります。

### Fixed

- 通知量を50%削減し、Discordチャンネルの可読性が向上しました。

### Testing

- Phase 13向けに16個のテストケースを追加（単体テスト8、統合テスト3、E2Eテスト5）。

## [1.1.4] - 2025-10-04

### Fixed

- ForceNotifyUnderway の ForceImmediate 通知で 4 隻がそろうまで Discord 送信を待機し、単体通知が発生しないよう集約を強制 priming。
- ForceImmediate 経路のサイクルリセットを調整し、強制集約後に次の ForceNotify が正しく新しいバッチを開始するように。

### Testing

- NotificationCoordinatorForceImmediateTests に ForceImmediate 単独サイクルを検証するケースを追加し、ユニットテストで回帰を防止。

## [1.1.3] - 2025-10-03

### Fixed

- ForceNotifyUnderway 実行時に Discord 集約状態がクリアされず、通常の 4 隻まとめ通知が維持されるようロジックを調整。
- ForceImmediate 経路でも通知キューへの即時送信を保証し、Suppressed 判定時に Forward へフォールバックするよう修正。

### Changed

- ForceNotify 経路のデバッグログを拡充し、CycleReady や Underway 集計の状態を追跡しやすくしました。

## [1.1.2] - 2025-10-02

### Fixed

- ForceNotify 連打時に DeliveryRecord の重複判定が誤検出され通知がブロックされる問題を解消。
- DiscordCycleNotificationAggregator を ForceImmediate 後にリセットし、毎回新しい Discord バッチを生成できるように調整。
- NotificationCoordinator が強制送信後にアグリゲーターを初期化して、後続の ForceNotify が正しく起動するように。

### Changed

- ForceImmediate 経路のデバッグ/トレースログを拡充し、Discord バッチの開始・リセットを追跡しやすくしました。

### Testing

- 2025-10-03: Dalamud DEV 環境で ForceNotifyUnderway を用いた即時送信を確認し、Discord へのバッチ flush と cycle reset ログを取得。

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
- **Zlatan.I**: 設計・実装・テスト・ドキュメント

### Acknowledgments
- Dalamud Team: API と開発環境の提供
- goatcorp: Dalamud ディストリビューション
- XIV Submarines コミュニティ: フィードバックと支援

---

[Unreleased]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/compare/v1.4.0...HEAD
[1.4.0]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.4.0
[1.3.0]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.3.0
[1.2.5]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.2.5
[1.2.4]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.2.4
[1.2.3]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.2.3
[1.2.2]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.2.2
[1.2.1]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.2.1
[1.2.0]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.2.0
[1.1.6]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.1.6
[1.1.5]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.1.5
[1.0.0]: https://github.com/zlatan-mt/XIVSubmarinesRewrite/releases/tag/v1.0.0
