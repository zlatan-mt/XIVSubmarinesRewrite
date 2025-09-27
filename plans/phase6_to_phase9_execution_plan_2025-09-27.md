<!-- apps/XIVSubmarinesRewrite/plans/phase6_to_phase9_execution_plan_2025-09-27.md -->
<!-- Phase6〜9 の残タスクを整理し、順序立てて実行するための計画 -->
<!-- 自動テストとUI磨き込みを完了し、1.0.0配布を仕上げるロードマップとして存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/plans/phase6_main_window_followup_plan_2025-09-26.md, apps/XIVSubmarinesRewrite/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-27.md, apps/XIVSubmarinesRewrite/tests/Playwright/ui-theme.spec.ts -->

# Phase6-9 実行計画 (2025-09-27)

## Phase6 — メインウィンドウ Playwright 仕上げ
- [x] `tests/Playwright/fixtures/main-window-fixture.ts` を作成し、単一ウィンドウ向けモックIPCと初期状態を共通化する
- [x] `main-window.spec.ts` を追加し、以下を自動検証する
  - [x] `/xsr` 実行で Overview タブが開く
  - [x] `/xsr notify` 実行で Notification タブが選択される
  - [x] DEV トグルの ON/OFF で DEV タブが出現/非表示になる
  - [x] ウィンドウをリサイズ→再起動で直近サイズが復元される
- [ ] RendererPreview の CLI を Playwright 用フィクスチャから呼び出すヘルパーにまとめ、実行時間と生成物をログ出力する
- [ ] `npm test` が `main-window.spec.ts` を含めて完走するよう `package.json` と Playwright 設定を更新する
- [ ] GitHub Actions (または既存CI) に `npm test` 実行ステップを追加し、成果物レポートをアーティファクト化する
- [ ] README に main-window シナリオの追加実行手順とトラブルシュートを追記する

### Phase6 テスト
- [ ] `npm run playwright:install`
- [ ] `npm test`
- [ ] `npm run test:ui:headed`

## Phase7 — Overview / Notification UI 磨き込み
- [ ] メインヘッダ配色とアイコンを決定し、ライト/ダーク双方でコントラスト比を測定する
- [ ] Notification フォームから未使用コンポーネントと `Dummy` 参照を削除する
- [ ] カード高さ計算を単一関数へ集約し、二列/一列レイアウトの条件分岐を最小化する
- [ ] Overview テーブルの折り返しとコピー操作にユニットテストを追加する（必要なら `Presentation` レイヤに薄いテスト機構を用意）
- [ ] UiTheme の実測カラーをレビューし、RendererPreview のサンプルを更新する
- [ ] UI 変更点をスクリーンショット化し、次回セッションノートへ添付する

### Phase7 テスト
- [ ] `dotnet test /p:DevPluginsDir=`
- [ ] `npm test -- --grep "@main-window"`
- [ ] 手動で `/xsr` → `/xsr notify` → DEV トグルを切り替え、視認性を確認する

## Phase8 — DEV タブ操作ログと開発支援
- [ ] `UiPreferences` に DEV 操作ログ領域を追加し、最終トグル時刻と直前操作内容を保持する
- [ ] DEV タブへ通知シミュレーション・キュー強制操作ボタンを復元し、既存 IPC を再利用する
- [ ] 操作時にトースト通知を表示し、一般ユーザー操作と誤認しない配色を適用する
- [ ] Playwright の DEV タグ向けシナリオを `dev-tools.spec.ts` として追加し、主要フローをカバーする
- [ ] ドキュメント (`README` / `docs/notifications/discord_batch_window_plan.md`) に DEV タブの運用と注意事項を追記する

### Phase8 テスト
- [ ] `dotnet test`
- [ ] `npm test -- --grep "@dev"`
- [ ] DEV タブシミュレーションをローカル Dalamud 環境で手動検証し、ログ出力を確認する

## Phase9 — 1.0.0 配布最終化
- [ ] CHANGELOG を作成し、1.0.0 の主要変更点とテスト結果を記載する
- [ ] `dotnet build -c Release /p:DevPluginsDir=` を再実行し、成果物を `bin/Release/net9.0-windows` へ揃える
- [ ] Dalamud devPlugins への配布パッケージを zip 化し、ハッシュ値を記録する
- [ ] GitHub Releases で `v1.0.0` のドラフトを作成し、スクリーンショットとリリースノートを添付する
- [ ] 公開後にセッションノートへ配布完了ログを残し、次バージョンの下書きを開始する

### Phase9 テスト
- [ ] `dotnet test /p:DevPluginsDir=`
- [ ] `npm test`
- [ ] Dalamud 実機で `/xsr`, `/xsr notify`, `/xsr dev` を一巡し、ログと通知結果を確認する

## 参考メモ
- すべての新規ファイルは 300 行以内を維持する。
- RendererPreview と Playwright で重複する色情報は `UiTheme` の単一ソースを参照し、別定義を作らない。
- 変更ごとにセッションノートを更新し、手動検証の証跡を残す。
