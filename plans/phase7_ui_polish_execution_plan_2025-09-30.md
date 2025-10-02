<!-- apps/XIVSubmarinesRewrite/plans/phase7_ui_polish_execution_plan_2025-09-30.md -->
<!-- Phase7 の残タスクを分解し、UI を出荷品質まで磨き込むための計画 -->
<!-- GitHub Actions とローカル双方で自動検証を走らせ、1.0.0 リリース前の品質線を確保するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/plans/phase6_to_phase9_execution_plan_2025-09-27.md, apps/XIVSubmarinesRewrite/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-27.md, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs -->

# Phase7 UI 磨き込み 実行計画 (2025-09-30)

## Phase7-A — カラーパレット仕上げ
- [x] UiTheme に WCAG コントラスト計算を実装し、ツールバーへ可視化を追加する
- [ ] UiTheme のアクセント色・背景色を Figma サンプルと照合し、最終値を確定させる
- [ ] RendererPreview の JSON/HTML 出力に色名辞書を追加し、Playwright で期待値を比較できるようにする
- [ ] UiThemeContrastTests にライトテーマ相当の仮想背景検証を追加し、境界ケースをカバーする

### Phase7-A テスト
- [ ] `dotnet test --filter UiThemeContrastTests` を Dalamud 依存 DLL 復旧後に実行する
- [ ] `npm test -- --grep "@main-window"` でツールバー配色可視化を回帰確認する

## Phase7-B — 通知フォームレイアウト安定化
- [x] Notification カードの列数判定を共通メトリクスへ集約する
- [ ] NotificationLayoutMetrics にブレークポイント一覧とテレメトリ出力を追加し、ログから閾値を可視化する
- [ ] NotificationMonitorWindowRenderer の保存ボタン周辺をコンポーネント化し、二度押し防止を組み込む
- [ ] Discord/Notion Webhook 入力のバリデーションを軽量化し、無効値は即エラー表示する

### Phase7-B テスト
- [ ] Playwright `notification-layout.spec.ts` を追加し、二列・一列切替と保存ボタンの無効化状態を自動検証する
- [ ] `npm run test:ui:headed -- --project chromium` でスクリーンショットを更新し、`tests/Playwright/test-results` に保存する

## Phase7-C — Overview 体験向上
- [x] OverviewRowFormatter を追加して表示/コピー書式を統一する
- [ ] Overview テーブルの列幅プリセットを導入し、ウィンドウ幅 720px 以下でも折返しが崩れないようにする
- [ ] 小型ウィンドウ用に残り時間と帰港予定の省略表示を実装する
- [ ] OverviewRowFormatterTests に境界ケース (到着未設定・名称欠落) を追加する

### Phase7-C テスト
- [ ] `dotnet test --filter OverviewRowFormatterTests` を実行し、新規ケースを通過させる
- [ ] Playwright に `@overview` タグ付きシナリオを追加し、列幅プリセットとコピー操作を検証する

## Phase7-D — ドキュメントと CI 強化
- [ ] Dalamud/Lumina 依存 DLL を `.github/workflows/ui-tests.yml` でダウンロードするステップを追加する
- [ ] `dotnet build` と `dotnet test` を CI に統合し、UI テストと並列で走るよう構成する
- [ ] README の UI セクションへ配色リファレンスとスクリーンショットを追記する
- [ ] `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-30.md` を新規作成し、Phase7 の完了結果とテストログを整理する

### Phase7-D テスト
- [ ] GitHub Actions 上で新ワークフローを実行し、Playwright と .NET のアーティファクトを確認する
- [ ] ローカルで `dotnet build` → `dotnet test` → `npm test` を順番に実行し、CI と同じ手順を再現する

## Phase8 準備タスク
- [ ] UiPreferences に DEV 操作履歴フィールドを追加する前提調査を行い、直列化コストを見積もる
- [ ] DEV タブ用 Playwright フィクスチャの設計を決め、共通ヘルパーの差分を洗い出す
- [ ] ForceNotifyUnderway のログサンプルを収集し、Phase8 エントリポイントのトリガー条件を整理する
