<!-- apps/XIVSubmarinesRewrite/plans/phase6_main_window_followup_plan_2025-09-26.md -->
<!-- 単一ウィンドウ統合後のUI品質向上と自動化を進める実装計画 -->
<!-- 新UXの回帰を防ぎ、レイアウトの課題を段階的に解消するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.cs, apps/XIVSubmarinesRewrite/tests/Playwright -->

# Phase6+ メインウィンドウ改善計画 (2025-09-26)

## Phase6 — タブ統合の自動テスト整備
- [ ] `tests/Playwright` にメインウィンドウ専用のシナリオ (`main-window.spec.ts`) を追加し、以下を自動検証する
  - [ ] `/xsr` → 概要タブ表示、ウィンドウ可視状態のトグル
  - [ ] `/xsr notify` → 通知タブが選択状態になる
  - [ ] DEV トグル ON/OFF で「開発」タブ表示の出現・非表示
  - [ ] リサイズ後に再オープンし、径路で直近サイズが復元される
- [ ] `tools/RendererPreview` または既存 CLI で Playwright が利用するダミー起動フローを更新（単一ウィンドウ構成に追従）
- [ ] CI/ローカルで `npm test` が通るよう README とスクリプトを更新

## Phase7 — Overview/通知レイアウトの磨き込み
- [ ] `MainWindowRenderer`／`OverviewWindowRenderer` のヘッダを整備し、バージョン・アクティブキャラクターの表示位置を整理
- [ ] 概要テーブルの長文セルに `Selectable + Copy` と折り返しを適用（通知タブと揃える）
- [ ] 通知タブのフォームコンポーネントから残余荷重（`Dummy` 等）を再確認し、カード高さを自動算出に一本化
- [ ] DEV タブに移動したスライダーへツールチップと最終更新値ラベルを追加

## Phase8 — 開発者向けUI・設定の可視化
- [ ] 設定ファイル (`UiPreferences`) に DEV タブの直近操作ログを保持する余白（例: 最終トグル時刻）を追加
- [ ] DEV タブへ通知シミュレーションやキュー強制操作のショートカットを整理（既存メソッドの再利用）
- [ ] 強制送信/診断操作時にトーストメッセージを表示し、一般ユーザー操作と誤認しないよう配色を変更
- [ ] ドキュメント (`README.md` or `docs/notifications/discord_batch_window_plan.md`) に DEV タブの使い方と注意事項を追記

## Phase9 — QA とリリース前点検
- [ ] `dotnet test` / `npm test` / Playwright シナリオをフル実行し、結果をセッションノートに記録
- [ ] `/xsr dev` を含む全 slash コマンドの回帰テストを行い、ヘルプメッセージを最終確認
- [ ] 実装差分を `CHANGELOG` またはセッションノートへ反映し、リリース準備を完了

