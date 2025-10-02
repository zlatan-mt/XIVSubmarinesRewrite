<!-- apps/XIVSubmarinesRewrite/plans/phase3_notion_remaining_validation_plan_2025-09-24.md -->
<!-- Phase3 で Notion Remaining フィールドの互換性確認を行う手順書です -->
<!-- Zap での記録方法と差分整理を明確にするため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/docs/notifications/notion_payload_plan.md, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs -->

# Phase3 Notion Remaining 互換性確認 (2025-09-24)

## 0. 前提チェック
- Phase1, Phase2 が同日に実施され、Notion Webhook が QA ステージングへ向いていること。
- `docs/notifications/notion_payload_plan.md` の 2025-09-24 残タスクメモを参照し、確認項目を再確認する。
- Zapier の履歴をダウンロードする権限があることを確認する。

## 1. データ取得
1. Zapier ダッシュボードから対象 Zap の履歴を開き、Underway 通知を受信した最新イベントを選択する。
2. `Hook A` ステップの raw JSON を `logs/2025-09-24/notion/run-<index>.json` として保存する。
3. Notion ページに出力されたプロパティをエクスポートし、`Remaining` カラムを一時表示に設定したスクリーンショットを `logs/2025-09-24/notion/screenshots/<timestamp>.png` として保存する。

## 2. 検証項目
- `ArrivalLocal` が ISO8601 ローカルタイム (例: `2025-09-24T12:34:56+09:00`) で出力されているか。
- `Remaining` が `PT#H#M` 形式、もしくは `PT#M` 形式で表記されているか。
- 既存 Zap が `Remaining` 未対応の場合にフィールドが無視されてもエラーになっていないか。

## 3. ドキュメント反映
- 問題が無い場合は `docs/notifications/notion_payload.json` にサンプルエントリを追加し、`Remaining` 値を含める。
- 差異が見つかった場合は `docs/notifications/notion_payload_plan.md` に課題メモを追記し、修正案を TODO として記録する。
- `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-24.md` に検証結果とスクリーンショットパスをまとめる。

## 4. 後処理
- Zap の Webhook URL を元環境へ戻し、戻したことをセッションノートに記載する。
- 保存したスクリーンショットと JSON を共有ドライブへアップロードし、ファイル名をランブックに追記する。
- Notion 側の一時表示設定を元に戻す。
