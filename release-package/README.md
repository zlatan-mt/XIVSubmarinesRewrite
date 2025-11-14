# PATH: apps/XIVSubmarinesRewrite/release-package/README.md
# DESCRIPTION: Outline release-specific install notes for the packaged plugin build.
# REASON: Provide users with the same install guidance as the main repository release package.
# RELEVANT FILES: README.md, release-package/manifest.json, repo.json

# XIV Submarines Rewrite

## このプラグインについて
Vectal の潜水艦支援プラグインを Dalamud 向けに再構築したプラグインです。

潜水艦スケジュールを自動記録し、帰港予定を一覧できます。

Discord や Notion の Webhook に完了通知を送ることもできます。

## 主な機能
- 工房メニューを開くだけで潜水艦情報を自動取得します
- Overview ウィンドウで全艦の状態と帰港予定を確認できます
- Notification タブで通知設定とキュー状況を監視できます
- チャットコマンド `/xsr` と `/xsr notify` でウィンドウを切り替えます

## 必要環境
- Windows 版 Final Fantasy XIV
- Dalamud API Level 13 が利用できる XIVLauncher 環境
- Dalamud の .NET 9 ランタイムが導入済みであること

## インストール手順

Dalamud Plugin Installer から直接インストールできます。

1. Dalamud の設定でカスタムリポジトリを追加：
   ```
   https://raw.githubusercontent.com/zlatan-mt/XIVSubmarinesRewrite/main/repo.json
   ```

2. プラグインインストーラーで「XIV Submarines Rewrite」を検索してインストール

## 初回セットアップ
Dalamud の Plugin Installer から設定画面を開きます。

通知が不要な場合は各チェックボックスを無効のままにしてください。

Discord や Notion で通知したい場合は Webhook URL を貼り付けて保存します。

## 日常の使い方
工房の「潜水艦探索」メニューを開くと最新情報を自動で取得します。

Overview ウィンドウでキャラクターを選ぶと潜水艦の状態が一覧化されます。

通知タブでは保留中とデッドレターの件数を確認し、必要なら再送できます。

## 通知設定
Discord 通知を使う場合は「Discord 通知を有効化」をオンにし、Webhook URL を入力します。

同様に Notion 通知もチェックボックスと URL を設定します。

「通知設定を保存」ボタンを押すとすぐに設定が反映されます。

## トラブルシュート
一覧が空のままの場合は工房で潜水艦一覧を開き直してください。

通知が届かない場合は Notification タブのデッドレター欄と `dalamud.log` のエラーメッセージを確認します。

チャットコマンドが動作しない場合は Dalamud でプラグインが有効化されているか確認します。

## 既知の制限
ルート名は内部 ID を簡易的に整形した表示であり正式名称ではありません。

自動更新は 2 秒周期で開始し、安定後は 10 秒周期に落ち着きます。

## ライセンス
このプロジェクトは MIT ライセンスの下で公開されています。
