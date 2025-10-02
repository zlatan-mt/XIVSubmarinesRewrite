<!-- apps/XIVSubmarinesRewrite/plans/phase1_underway_validation_runbook_2025-09-24.md -->
<!-- Phase1 の出航通知ステージング検証を順次進めるための手順書です -->
<!-- QA 実施者が必要な準備と記録方法を即座に把握できるようにするため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/docs/notifications/underway_notification_validation.md, apps/XIVSubmarinesRewrite/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-23.md -->

# Phase1 出航通知検証ランブック (2025-09-24)

## 0. 事前準備
- `docs/notifications/underway_notification_validation.md` を読み、前提条件とチェックリストを確認する。
- `oci://staging-webhooks` 管理表で Discord/Notion の QA Webhook URL を確認し、元 URL をメモしておく。
- `logs/2025-09-24/underway` ディレクトリを作成し、`discord`, `notion`, `screenshots` の 3 サブフォルダを用意する。
- Dalamud のログレベルを `Information` に設定し、開始時刻を `sessions` ノートへ書き留める。

## 1. Discord Webhook 差し替え
- `NotificationSettings` の `DiscordWebhookUrl` を QA チャンネルに差し替え、保存後 Dalamud を再起動しないことを確認する。
- `NotificationMonitor` 画面で「Discord 通知を有効化」が ON であるかチェックする。
- 差し替えた URL を `logs/2025-09-24/underway/discord/webhook.txt` に記録する。

## 2. Notion Webhook 差し替え
- Zapier のステージングワークフローを開き、受信 Webhook を QA 用 URL に切り替える。
- 切り替え完了後にテスト Webhook を送信し、200 OK を確認する。
- 受信ログを `logs/2025-09-24/underway/notion/test-validation.json` として保存する。

## 3. Underway 通知送信
- 対象キャラクターを Dalamud で選択し、最新スナップショット取得後に潜水艦を 1 隻出航させる。
- 代替として `NotificationMonitor` の「通知を即時送信」を使用する場合、実行時間を記録しスクリーンショットを取得する。
- 実行ごとに Discord メッセージと Notion ペイロードを保存し、Discord は `.png`、Notion は `.json` 形式で保管する。

## 4. ログ収集
- Dalamud ログから `[Notifications] Discord batch flush` 行を抽出し、`logs/2025-09-24/underway/discord/run-<index>.log` に保存する。
- `ageMs`/`overshootMs` を `docs/notifications/discord_batch_window_plan.md` の表へ転記する。
- Notion Zap の履歴から raw JSON をダウンロードし、`Remaining` 値を確認する。

## 5. 記録と片付け
- `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-24.md` を作成し、検証結果を「Phase1 実施ログ」としてまとめる。
- Webhook URL を元の値へ戻し、戻したことを同ノートへ記載する。
- 残タスクがあれば `sessions/SESSION_XIVSubmarinesRewrite_status_2025-09-24.md` へ次アクションを追記する。

## 6. 完了確認
- 収集したスクリーンショットとログが所定フォルダに揃っているかをダブルチェックする。
- `discord_batch_window_plan.md` と `notion_payload_plan.md` に必要な追記が完了しているか確認する。
- チームへ共有する前に差し替え忘れが無いかチェックし、完了チェックリストへサインオフする。
