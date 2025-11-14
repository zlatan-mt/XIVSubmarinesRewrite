# PATH: apps/XIVSubmarinesRewrite/README.md
# DESCRIPTION: Provide users with project overview, installation guidance, and contributor notes.
# REASON: Keep onboarding and branching guidance in a single reference document.
# RELEVANT FILES: release-package/README.md, repo.json, docs/ai-development/AGENTS.md

# XIV Submarines Rewrite

## このプラグインについて
Vectal の潜水艦支援プラグインを Dalamud 向けに再構築したプラグインです。

潜水艦スケジュールを自動記録し、帰港予定を一覧できます。

Discord や Notion の Webhook に出航通知を送ることができます。

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

### 基本設定
Discord 通知を使う場合は「Discord 通知を有効化」をオンにし、Webhook URL を入力します。

同様に Notion 通知もチェックボックスと URL を設定します。

「通知設定を保存」ボタンを押すとすぐに設定が反映されます。

### 通知タイミング
- **出港直後を通知**: 潜水艦が出航したタイミングで通知を送信します（帰還予定時刻を含む）
- ~~航海完了を通知~~: この機能は v1.1.5 で廃止されました。出航時の帰還予定通知で代替できます。

### Discord Reminder Bot 連携（オプション）
「リマインダーコマンドを含める」を有効にすると、Discord通知に [Reminder Bot](https://reminder-bot.com/) のコマンドが自動生成されます。

**使い方**:
1. 通知設定で「リマインダーコマンドを含める」をオン
2. チャンネル名を設定（例: `#submarine`）
3. Discord通知を受け取ったら、「リマインダー設定」フィールドのコマンドをコピー
4. Discordでコマンドを貼り付けて実行

これにより、帰還予定時刻にReminder Botが自動でリマインダーを送信します。

**通知フォーマット**:
- **単体通知**: シンプルな1行形式（潜水艦名、航路、帰還予定）
- **バッチ通知**: 同時出航した複数艦をまとめて通知（1.5秒以内の出航を自動グループ化）

## トラブルシュート
一覧が空のままの場合は工房で潜水艦一覧を開き直してください。

通知が届かない場合は Notification タブのデッドレター欄と `dalamud.log` のエラーメッセージを確認します。

チャットコマンドが動作しない場合は Dalamud でプラグインが有効化されているか確認します。

## 既知の制限
ルート名は内部 ID を簡易的に整形した表示であり正式名称ではありません。

自動更新は 2 秒周期で開始し、安定後は 10 秒周期に落ち着きます。

## 開発者向け情報

### ブランチ戦略

- **`main`** - 開発と公開を兼ねる単一ブランチ。リリースはタグ（例: `v1.2.0`）で管理します。
- **`feature/*`** - 必要なときだけ切る短命ブランチ。小さな修正は `main` に直接コミットしてください。+
### 開発参加

クローンするなら `main`：

```bash
git clone -b main https://github.com/zlatan-mt/XIVSubmarinesRewrite.git
```+
すべての開発資料は `main` に含まれます：
- `docs/ai-development/` - AI開発支援ドキュメント
- `docs/release/` - リリース手順
- `plans/` - フェーズ計画と cc-sdd 仕様書+
### repo.json 配信

Dalamud 用の `repo.json` は `main` から公開しています。

### ビルドとテスト

詳細は `docs/ai-development/` の手順を参照してください。

## ライセンス
このプロジェクトは MIT ライセンスの下で公開されています。

