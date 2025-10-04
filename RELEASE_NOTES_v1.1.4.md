# XIV Submarines Rewrite v1.1.4 Release Notes

## リリース概要
ForceNotifyUnderway の通知バッチング機能を改善し、より安定した通知体験を提供します。

## 主な変更点

### 🔧 修正 (Fixed)

#### ForceNotifyUnderway の通知集約改善
- **ForceImmediate 通知の最適化**: 4隻すべてが揃うまで Discord 送信を待機し、単体通知が発生しないよう集約を強制 priming
- **サイクルリセット調整**: ForceImmediate 経路のサイクルリセットを調整し、強制集約後に次の ForceNotify が正しく新しいバッチを開始

### 🧪 テスト (Testing)
- **ユニットテスト追加**: NotificationCoordinatorForceImmediateTests に ForceImmediate 単独サイクルを検証するケースを追加
- **回帰防止**: ユニットテストで機能の回帰を防止

## 技術的詳細

### アセンブリ情報
- **バージョン**: 1.1.4.0
- **ファイルバージョン**: 1.1.4.0
- **情報バージョン**: 1.1.4+build.{timestamp}

### 互換性
- **Dalamud API Level**: 13
- **.NET バージョン**: 9.0
- **対象ゲームバージョン**: any

## インストール方法

1. Dalamud プラグインインストーラから「XIV Submarines Rewrite」を検索
2. インストールボタンをクリック
3. ゲーム内で `/submarines` コマンドでプラグインを起動

## 既知の問題

- ForceNotifyUnderway は開発用機能のため、本番環境での使用は推奨しません
- 再構築中のため、UI や文面は予告なく変わる可能性があります

## サポート

- **リポジトリ**: https://github.com/mona-ty/XIVSubmarinesRewrite
- **問題報告**: GitHub Issues をご利用ください

---

**リリース日**: 2025-10-04  
**コミット**: ffbe55f  
**タグ**: v1.1.4