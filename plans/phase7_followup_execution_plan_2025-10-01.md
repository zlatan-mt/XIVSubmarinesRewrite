<!-- apps/XIVSubmarinesRewrite/plans/phase7_followup_execution_plan_2025-10-01.md -->
<!-- Phase7 の残タスクを段階的に完了し、UI をリリース品質へ持ち上げるための実行計画 -->
<!-- カラーパレット同期の成果を基盤に通知フォームと Overview を磨き込み、CI を整えることを目的とします -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/plans/phase7_ui_polish_execution_plan_2025-09-30.md, apps/XIVSubmarinesRewrite/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-27.md, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs -->

# Phase7 フォローアップ実行計画 (2025-10-01)

## 進捗サマリ
- [x] Phase7-A カラーパレット仕上げ (RendererPreview と Figma 参照の同期、Playwright/@theme テスト追加)
- [ ] Phase7-B 通知フォーム安定化
- [ ] Phase7-C Overview 体験向上
- [ ] Phase7-D ドキュメントと CI 強化

---

## Phase7-B — 通知フォーム安定化
### 実装タスク
- [ ] `NotificationLayoutMetrics` にブレークポイント配列と実測ログ出力ユーティリティを追加する
- [ ] Discord/Notion カードの描画を新規 `NotificationChannelCard` クラスへ切り出し、重複ロジックを除去する
- [ ] 保存ボタンとバリデーション制御を `NotificationSavePanel` (新ファイル) として分離して二度押し防止を実装する
- [ ] Discord/Notion URL の即時検証を行い、無効値の場合は保存ボタンと開発向け操作を無効化する
- [ ] ForceNotify 開発オプションのトグルと診断を整理し、一般ユーザー表示から完全に切り離す

### テストタスク
- [ ] Playwright `notification-layout.spec.ts` を追加し、1列/2列判定、バリデーション表示、保存ボタン状態を自動検証する
- [ ] `dotnet test --filter NotificationMonitorWindowRendererTests` (新規テスト) を実行し、ブレークポイント計算をユニット検証する
- [ ] `npm run test:ui -- --grep "@notification"` を CI/ローカル双方で回せるよう `package.json` を更新する

---

## Phase7-C — Overview 体験向上
### 実装タスク
- [ ] `OverviewWindowRenderer` に列幅プリセット計算ヘルパーを追加し、ウィンドウ幅に応じて列構成を調整する
- [ ] `OverviewRowFormatter` にコンパクト表示関数を追加し、720px 未満で残り時間と帰港予定を短縮表記に切り替える
- [ ] 航路コピー機能でゼロ幅スペースが含まれる場合に除去した文字列をクリップボードへ送る
- [ ] DEV タブとの連携のため、Overview から通知設定タブを開くショートカットボタンを設ける

### テストタスク
- [ ] `OverviewRowFormatterTests` を拡張し、名称欠落・日時未設定・コンパクト表示のフォーマットを検証する
- [ ] Playwright `overview-responsive.spec.ts` を新規作成し、幅 680px と 960px の表示崩れを自動比較する
- [ ] `npm run test:ui -- --grep "@overview"` を新設し、CI でスクリーンショットをアーティファクト化する

---

## Phase7-D — ドキュメントと CI 強化
### 実装タスク
- [ ] `.github/workflows/ui-tests.yml` に `workflow_dispatch` トリガーと `dotnet test` ステップを追加する
- [ ] Verify ワークフロー (新規 `verify.yml`) を作成し、Playwright + .NET テスト + アーティファクト収集を手動および push 時に実行できるようにする
- [ ] README の UI セクションへ最終配色表、`@theme` テスト手順、通知フォームのスクリーンショットを追加する
- [ ] `docs/ui/theme-final.jsonc` と `tests/.../UiThemePaletteTests.cs` をリポジトリ管理対象にするため `.gitignore` を整理する (必要に応じてサブディレクトリ単位で override)
- [ ] `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-01.md` を作成し、Phase7-B/C/D の進捗とテスト結果を記録する

### テストタスク
- [ ] `dotnet build` → `dotnet test` → `npm run test:ui -- --grep "@theme|@notification|@overview"` の連続実行をローカルで確認する
- [ ] GitHub Actions の Verify ワークフローを `gh workflow run` で手動実行し、アーティファクト内容を検証する
- [ ] Dalamud 実機で `/xsr` → `/xsr notify` → `/xsr dev` を巡回し、通知フォームの保存挙動と Overview からの遷移を確認する

---

## リファクタリングおよびコード品質ガード
- [ ] 新規ファイルは 200 行以内を目標とし、既存クラスが 300 行を超える場合は部分クラス化を検討する
- [ ] バリデーションやメトリクス計測で共通化できる関数は `NotificationLayoutMetrics` へ集約し、ロジック重複を許さない
- [ ] Playwright フィクスチャで RendererPreview 出力を共有するユーティリティを再利用し、不要な CLI 呼び出しを避ける
- [ ] Git 管理外となっているファイルは `.gitignore` 更新前に `git add -f` で暫定対応し、計画完了時に ignore ルールを整理する

---

## フェーズ横断の次アクション
- [ ] Phase8 DEV タブ操作ログ強化のため、通知フォーム計測で得た JSON 出力を解析し、必要なフィールドリストを草案化する
- [ ] ForceNotifyUnderway のログ出力ポリシーを Phase7-B の計測値と突き合わせ、Phase8 へ引き継ぐ改善案をまとめる
- [ ] CHANGELOG v1.0.0 草案 (Phase9) の下書きを開始し、完了した Phase7-A/B/C/D の成果を記録する
