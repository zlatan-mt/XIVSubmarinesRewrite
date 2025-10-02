<!-- apps/XIVSubmarinesRewrite/docs/notifications/force-notify-guide.md -->
<!-- ForceNotifyUnderway 機能の使い方と注意事項を説明します -->
<!-- 開発者が DEV タブで安全に通知テストを行うため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/DevNotificationPanel.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs -->

# ForceNotifyUnderway 開発者ガイド

## 概要

`ForceNotifyUnderway` は出航中の潜水艦でも通知を送信できる開発・テスト用機能です。

通常、通知は帰港済み（Completed）の潜水艦のみを対象としますが、この機能を有効にすると出航中（Underway）の潜水艦も通知対象に含まれます。


## 用途

- 通知フォーマットの確認
- Discord/Notion Webhook の接続テスト
- 通知キューとバッチ処理の動作確認
- DEV タブ UI のスクリーンショット取得


## 使い方

### 1. DEV タブを開く

メインウィンドウのツールバーで「DEV • OFF」ボタンをクリックします。

開発タブが表示され、ボタンが「DEV • ON」に変わります。


### 2. ForceNotifyUnderway を有効化

開発タブで「出航中でも通知を送信 (開発用)」チェックボックスをオンにします。

オレンジ色の警告バナーが表示されます：

```
⚠ 開発者ツール
テスト目的の設定です。通常運用では無効のままにしてください。
```


### 3. 通知を手動送信

「選択キャラクターの通知を即時送信」ボタンをクリックします。

- 通知設定が有効でフォームが正しい場合のみ実行可能
- 送信結果がトーストで3秒間表示されます
- 手動送信ログの ImGui Table に最新10件が記録されます


### 4. DEV サマリーで確認

メインウィンドウ上部の DEV サマリーバナーに最終操作が表示されます：

```
最終送信 HH:mm:ss
✓ N 件の通知をキューに追加しました
ForceNotify: ON / HH:mm:ss
```


### 5. 使用後は必ず無効化

テスト完了後、チェックボックスをオフにして ForceNotifyUnderway を無効化してください。


## 保存されるログ

### 1. UiPreferences.DevHistory

`%AppData%\XIVLauncher\pluginConfigs\XIVSubmarinesRewrite\UiPreferences.json` に以下が記録されます：

```json
{
  "DevHistory": {
    "LastForceNotifyToggleUtc": "2025-10-02T12:34:56Z",
    "ForceNotifyEnabled": true,
    "LastManualTriggerUtc": "2025-10-02T12:35:10Z",
    "LastManualTriggerSummary": "3 件の通知を再送しました。",
    "LastDeveloperTabToggleUtc": "2025-10-02T12:34:00Z",
    "DeveloperToolsVisible": true
  }
}
```


### 2. 手動送信ログ JSON

`logs/<date>/dev-panel/manual-trigger-<timestamp>.json` に最大10件のJSON が保存されます：

```json
{
  "TriggeredAtUtc": "2025-10-02T12:35:10.123Z",
  "CharacterId": 123456789,
  "CharacterName": "Mona Ty",
  "World": "Tonberry",
  "IncludeUnderway": true,
  "NotificationsEnqueued": 3
}
```


### 3. dalamud.log トレース

Dalamud ログに以下のトレースが出力されます：

```
[Trace] [VoyageCompletionProjection] ForceNotifyUnderway=True includeUnderway=True candidates=5 filtered=3
```


## 注意事項

### ⚠️ 本番環境での使用は推奨しません

- ForceNotifyUnderway は開発・テスト専用機能です
- 出航中の潜水艦で通知を送信すると、同じ潜水艦が帰港時に再度通知されます
- 重複通知を避けるため、通常運用では必ず無効にしてください


### ⚠️ 通知レート制限に注意

- 手動送信は NotificationQueue のバッチ処理を経由します
- Discord: デフォルト30秒間隔でバッチ送信
- 短時間に大量送信すると Webhook のレート制限に引っかかる可能性があります


### ⚠️ デッドレター確認

手動送信後、Notification タブの「デッドレター」欄を確認してください：

- `0 件` が正常
- 件数が増えている場合は Webhook URL や接続を確認

### ⚠️ キャラクター永続化の仕様

- **潜水艦操作履歴があるキャラクターのみが永続化対象**です
- 工房メニューを開いても潜水艦データがないキャラクターは保存されません
- 手動送信は現在選択中のキャラクターのみが対象となります
- 起動時に `CleanupCharactersWithoutSubmarineOperations()` で不要なキャラクターが自動削除されます


## トラブルシュート

### 「選択キャラクターの通知を即時送信」がグレーアウトしている

以下を確認してください：

1. Discord または Notion の通知設定が有効になっているか
2. Webhook URL が正しく入力されているか（https:// で始まる）
3. 「通知設定を保存」ボタンをクリックして設定を保存したか


### トーストが「再送対象はありませんでした」と表示される

現在選択中のキャラクターに送信可能な通知がありません。

- ForceNotifyUnderway がオフの場合: 帰港済みの潜水艦がない
- ForceNotifyUnderway がオンの場合: 出航中・帰港済み共に通知対象がない


### 手動送信したのに Discord/Notion に届かない

1. Notification タブの「保留」欄を確認
   - 件数が増えている場合、バッチ待機中です（Discord: 30秒）
2. 「デッドレター」欄を確認
   - 件数が増えている場合、送信失敗しています
   - 「全て再送」ボタンで再試行できます
3. `dalamud.log` でエラーを確認
   - `[Error] [NotificationWorker]` で検索


## CI での利用

GitHub Actions では ForceNotifyUnderway を使用しません。

Playwright テストは RendererPreview を使った UI 検証のみ行い、実際の通知送信はテストしません。


## 関連ファイル

- `src/Presentation/Rendering/DevNotificationPanel.cs` - DEV タブ UI
- `src/Application/Notifications/VoyageCompletionProjection.ForceNotify.cs` - フィルタリングロジック
- `src/Application/Notifications/IForceNotifyDiagnostics.cs` - 手動送信ログ管理
- `src/Infrastructure/Configuration/UiPreferences.cs` - DevHistory 永続化
- `tests/Playwright/dev-tools.spec.ts` - DEV タブの Playwright テスト


## フィードバック

ForceNotifyUnderway の動作に問題がある場合は、以下を添えて報告してください：

1. `dalamud.log` の該当部分（ForceNotifyUnderway で検索）
2. 手動送信ログ JSON（`logs/<date>/dev-panel/`）
3. DEV サマリーバナーのスクリーンショット
4. 再現手順

