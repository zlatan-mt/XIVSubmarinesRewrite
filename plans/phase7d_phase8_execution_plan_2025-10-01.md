<!-- apps/XIVSubmarinesRewrite/plans/phase7d_phase8_execution_plan_2025-10-01.md -->
<!-- Phase7-D と Phase8 の立ち上げをまとめ、CI/ドキュメント整備から DEV タブ強化へ繋げる実行計画 -->
<!-- Phase7 作業の仕上げを行い、Phase8・Phase9 を滞りなく進めるため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/plans/phase7_followup_execution_plan_2025-10-01.md, apps/XIVSubmarinesRewrite/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-01.md, apps/XIVSubmarinesRewrite/.github/workflows/ui-tests.yml -->

# Phase7-D / Phase8 実行計画 (2025-10-01)

## 進捗サマリ
- [x] Phase7-A カラーパレット同期と @theme テストを完了
- [x] Phase7-B 通知フォーム安定化 (URL バリデーション、レイアウト計測、Playwright @notification)
- [x] Phase7-C Overview 体験向上 (列幅プリセット、コンパクト表示、Playwright @overview)
- [ ] Phase7-D CI / ドキュメント強化
- [ ] Phase8 DEV タブ操作ログ強化 (着手前)

---

## Phase7-D — CI とドキュメント強化
### 実装タスク
- [ ] `.github/workflows/ui-tests.yml` に `workflow_dispatch` トリガーと `dotnet test` ステップを追加し、Playwright と並列実行させる
- [ ] 新規 `verify.yml` ワークフローを作成し、手動実行および `push` 時に `dotnet build` → `dotnet test` → `npm run test:ui -- --grep "@theme|@notification|@overview"` を実行する
- [ ] `docs/ui/theme-final.jsonc` をリポジトリ管理に含められるよう `.gitignore` を調整し、必要なファイルのみ除外
- [ ] README の UI セクションへ配色表、@notification/@overview の動作スクリーンショット、Playwright 実行手順を追記
- [ ] Phase7 完了報告として `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-02.md` (予定) を作成する下書きを準備

### テスト / 検証タスク
- [ ] `gh workflow run ui-tests.yml` で手動起動し、アーティファクト内容を確認
- [ ] `gh workflow run verify.yml` で Verify ワークフローを実行し、CI が成功することを確認
- [ ] ローカルで `dotnet build` → `dotnet test` → `npm run test:ui -- --grep "@theme|@notification|@overview"` を再現実行

---

## Phase8 — DEV タブ操作ログと開発支援強化
### 実装タスク
- [ ] `NotificationMonitorWindowRenderer` の開発オプション部を分割し、DEV 専用トグル・操作ログを `DevNotificationPanel` (仮称) に集約する
- [ ] `UiPreferences` に DEV 操作履歴 (最終トグル時刻、直前操作) を追加し、序列化コストを測定した上で保存処理を最適化する
- [ ] `IForceNotifyDiagnostics` を拡張し、ForceNotify の手動発火時にミニログを生成できるようにする
- [ ] Playwright フィクスチャを流用し `tests/Playwright/dev-tools.spec.ts` (新規) を作成、DEV タブのトグルと ForceNotify シミュレーションを検証する
- [ ] DEV モード有効時のみ表示するトーストと色分け (黒/白/ディープブルー基調) を適用し、一般利用者との混同を防ぐ

### テスト / 検証タスク
- [ ] `dotnet test --filter DevNotificationPanelTests` (新設予定) を追加し、DEV 操作ログの序列化と復元を検証
- [ ] `npm run test:ui -- --grep "@dev"` を追加し、DEV タブ Playwright シナリオを CI に組み込む
- [ ] Dalamud 実機で `/xsr dev` → ForceNotify 操作を実行し、操作ログとトースト表示を確認

---

## Phase9 プレリリース準備 (先行タスク)
### 実装タスク
- [ ] `CHANGELOG.md` に v1.0.0 の概要、完了した Phase7 内容、テスト結果を追記
- [ ] `tools/RendererPreview` と Playwright アーティファクトのディレクトリ整備 (古いログのクリーンアップ手段をスクリプト化)
- [ ] リリースパッケージ生成手順 (`npm run build`, `dotnet build -c Release /p:DevPluginsDir=`) を Verify ワークフローへ組み込むための原案をドキュメント化

### テスト / 検証タスク
- [ ] `dotnet build -c Release` と `npm run build` をローカルで実行し、成果物を確認
- [ ] Dalamud devPlugins へベータパッケージを投入して `/xsr`, `/xsr notify`, `/xsr dev` を動作確認

---

## リファクタリング・品質ガードライン
- [ ] 新規ファイルは 250 行以下を目標にし、既存クラスが 300 行を超えそうな場合は部分クラス化を検討
- [ ] バリデーションやテレメトリなど UI ロジックはユーティリティ化し、重複コードを排除
- [ ] `.gitignore` 整理時は CI で必要なアーティファクトのみ除外し、将来のテスト拡張に備える
- [ ] Playwright シナリオごとに `@theme`, `@notification`, `@overview`, `@dev` のタグ運用を徹底し、CI コマンドが明確になるよう維持

---

## 既存タスクの引き継ぎメモ
- ForceNotifyUnderway のログ出力ポリシー調整は Phase8 で扱うため、Phase7 完了時点では現行動作を維持。  
- `plans/phase6_to_phase9_execution_plan_2025-09-27.md` の Phase8/Phase9 項目を本計画と整合させ、完了後にチェック更新を行う。  
- セッションノートは Phase7-D 完了後および Phase8 主要リリース後に必ず更新する。  

