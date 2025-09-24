<!-- apps/XIVSubmarinesRewrite/docs/notifications/underway_notification_validation.md -->
<!-- 出航通知のみを検証するための手順書です -->
<!-- Discord/Notion で次回帰港を確認するための確認項目を共有する目的で存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/NotificationSettings.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs -->

# 出航通知検証チェックリスト (Phase 1)

## テスト設定
- `pluginConfigs/XIVSubmarinesRewrite.NotificationSettings.json` を開き、下記を確認/変更します。
  - `"NotifyVoyageCompleted": false`
  - `"NotifyVoyageUnderway": true`
  - 他の既存キーはそのまま。
- Dalamud UI から通知設定を開き、「航海完了の通知を送信」を OFF、「出航直後の通知を送信」を ON にした状態で保存します。
- Discord Webhook はテスト用チャンネルを設定しておきます。

```jsonc
{
  "NotifyVoyageCompleted": false,
  "NotifyVoyageUnderway": true,
  "ForceNotifyUnderway": false,
  "DiscordBatchWindowSeconds": 1.5,
  "EnableDiscord": true,
  "DiscordWebhookUrl": "https://discord.com/api/webhooks/..."
}
```

## 手順
1. Dalamud 上で対象キャラクターを選択し、潜水艦 UI を開いて最新スナップショットを取得します。
2. 工房から 1 隻だけ出航させるか、`NotificationMonitor` の「選択キャラクターの通知を即時送信」を使用して Underway 通知を発火させます。
3. Discord チャンネルで下記を確認します。
   - タイトルに「出航」が含まれていること。
   - 本文に「次の帰港: …」「残り時間: …」が表示されていること。
   - 4 隻以上同時出航時はバッチ通知が生成され、各行に同様の情報が含まれること。
4. `dalamud.log`（`%APPDATA%/XIVLauncher/logs/dalamud.log`）で以下のログが出力されているか確認します。
   - `[Notifications] Discord batch flush character=... status=Underway ...`
   - 完了通知が抑止された場合は `[Notifications] Completed voyage ... suppressed` が出ること。
5. Notion Webhook を有効にしている場合は、ペイロードの `Remaining` フィールドが更新されているか Zap で確認します。

## 検査結果の記録テンプレート
- キャラクター名 / テスト日時
- 出航通知スクリーンショット URL
- Discord ログ抜粋 (`ageMs`, `overshootMs`)
- dalamud.log 抜粋 (Underway suppressed / flush)
- Notion Zap 実行結果 (成功/失敗、`Remaining` の値)
- フィードバック / 次のアクション

以上を QA ノートへ貼り付け、Phase 1 の検証完了とします。

## 2025-09-24 検証準備メモ
- Discord と Notion のステージング Webhook をそれぞれ QA チャンネルに差し替え、テスト完了後は元に戻す。
- キャラクターごとに Underway 通知を 1 回ずつ送出し、スクリーンショットとログを即時保存する手順を決めておく。
- `dalamud.log` の保存先を `logs/2025-09-24/underway` にまとめるフォルダ構成を作成し、必要キャプチャを想定ファイル名で事前作成する。
- Notion Zap の実行履歴を CSV としてダウンロードする方法をリマインドし、`Remaining` 取得確認のスクリーンショットを撮るタイミングを決める。
