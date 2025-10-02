<!-- apps/XIVSubmarinesRewrite/README.md -->
<!-- プラグイン利用者が導入と操作を確認するためのガイド -->
<!-- XIV Submarines Rewrite の使い方を簡潔にまとめる目的で存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/manifest.json, apps/XIVSubmarinesRewrite/src/Plugin.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs -->
# XIV Submarines Rewrite

## このプラグインについて
Vectal の潜水艦支援プラグインを Dalamud 向けに再構築したテスト版です。

潜水艦スケジュールを自動記録し、帰港予定を一覧できます。

Discord や Notion の Webhook に完了通知を送ることもできます。

## 主な機能
- 工房メニューを開くだけで潜水艦情報を自動取得します。
- Overview ウィンドウで全艦の状態と帰港予定を確認できます。
- Notification タブで通知設定とキュー状況を監視できます。
- チャットコマンド `/xsr` と `/xsr notify` でウィンドウを切り替えます。

## 必要環境
- Windows 版 Final Fantasy XIV。
- Dalamud API Level 13 が利用できる XIVLauncher 環境。
- Dalamud の .NET 9 ランタイムが導入済みであること。

## インストール手順
1. ビルド後の `bin/Release/net9.0-windows` から `XIVSubmarinesRewrite.dll`, `manifest.json`, `icon.png` を準備します。
2. `%AppData%\XIVLauncher\devPlugins\XIVSubmarinesRewrite` にファイルを配置します。
3. Dalamud を再起動し、プラグインリストで `XIV Submarines Rewrite` を有効化します。

## 初回セットアップ
Dalamud の Plugin Installer から設定画面を開きます。

通知が不要な場合は各チェックボックスを無効のままにしてください。

Discord や Notion で通知したい場合は Webhook URL を貼り付けて保存します。

## 日常の使い方
工房の「潜水艦探索」メニューを開くと最新情報を自動で取得します。

Overview ウィンドウでキャラクターを選ぶと潜水艦の状態が一覧化されます。

通知タブでは保留中とデッドレターの件数を確認し、必要なら再送できます。

## 通知設定とテスト
Discord 通知を使う場合は「Discord 通知を有効化」をオンにし、Webhook URL を入力します。

同様に Notion 通知もチェックボックスと URL を設定します。

「通知設定を保存」ボタンを押すとすぐに設定が反映されます。

「選択キャラクターの通知を即時送信」で現行のスナップショットを手動送信できます。

出航中でも通知を送りたい場合は「出航中でも通知を送信する」を一時的にオンにします。

開発向けの挙動なので有効化時は `dalamud.log` の `ForceNotifyUnderway` Trace を必ず確認してください。

## トラブルシュート
一覧が空のままの場合は工房で潜水艦一覧を開き直してください。

通知が届かない場合は Notification タブのデッドレター欄と `dalamud.log` のエラーメッセージを確認します。

チャットコマンドが動作しない場合は Dalamud でプラグインが有効化されているか確認します。

## 開発者向けテスト
ルートの npm スクリプトで Playwright と .NET の検証を素早く実行できます。

- `npm run test:ui` で Playwright シナリオをヘッドレス実行します。
- `npm run test:ui:headed` でウィンドウを開いたままデバッグします。
- `npm run playwright:install` で Playwright ブラウザを取得します。
- `npm run test:ui:main-window` でメインウィンドウのシナリオを個別実行します。

初回は `npm install --prefix tests/Playwright` を実行して依存を揃えてください。

## UI カラーパレット
黒と白を基調とし、アクセントはディープブルーです。

- WindowBg: #0D0D0F でメイン背景を統一します。
- AccentPrimary: #1F5CD1 で操作要素を強調します。
- ErrorText: #EB4D47 で警告とエラーを示します。
- docs/ui/theme-final.jsonc に完全な一覧があります。

## CI ワークフロー
GitHub Actions の Verify で .NET と Playwright をまとめて検証します。

- `gh workflow run verify.yml` で手動起動できます。
- `gh workflow run ui-tests.yml` でタグ指定の UI テストを再実行できます。
- 成功すると artifacts に Playwright レポートが残ります。

## Playwright タグ実行
シナリオごとにタグで実行を切り替えられます。

- `npm run test:ui -- --grep "@theme"` で配色検証を実行します。
- `npm run test:ui -- --grep "@notification"` で通知フォームを確認します。
- `npm run test:ui -- --grep "@overview"` で Overview を確認します。
- `npm run test:ui -- --grep "@dev"` で DEV タブのロジックを確認します。

## DEV 操作ログ
開発タブでは履歴とミニログを確認できます。

- ForceNotify の手動トグルは時刻付きで保存されます。
- 手動送信は最大 10 件の JSON が `logs/<date>/dev-panel` に保存されます。
- メインウィンドウの DEV バナーで直近の送信要約を確認できます。

### DEV タブの使い方

ツールバーの「DEV • OFF」ボタンをクリックすると開発タブが表示されます。

DEV タブでは以下の機能を利用できます：

- **ForceNotifyUnderway トグル**: 出航中でも通知を送信するテストモード
- **即時送信ボタン**: 選択キャラクターの通知を手動でキューに追加
- **トースト通知**: 送信結果を 3 秒間視覚的にフィードバック
- **手動送信ログ**: 最新 10 件の送信履歴をテーブル表示
- **DEV サマリーバナー**: メインウィンドウ上部に最終操作の概要を表示

手動送信は通知設定が有効でフォームが正しい場合のみ実行できます。

ForceNotifyUnderway は危険操作のため、オレンジ色の警告バナーが表示されます。

開発タブを閉じるには、再度「DEV • ON」ボタンをクリックしてください。

## 既知の制限
再構築中のため UI や文面は予告なく変わります。

ルート名は内部 ID を簡易的に整形した表示であり正式名称ではありません。

自動更新は 2 秒周期で開始し、安定後は 10 秒周期に落ち着きます。

