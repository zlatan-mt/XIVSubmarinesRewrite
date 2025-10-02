# XIV Submarines Rewrite 実装状況

最終更新日: 2025-09-18

## 概要

Dalamud 用サブマリン支援プラグインの全面書き直し版。主要機能はスナップショット取得、通知配信、ImGui ベースの UI、永続化の試験実装で構成される。構造はクリーンアーキテクチャを意識しており、ドメイン・アプリケーション・インフラ・プレゼンテーションで明確に分離されている。

## 取得基盤 (Acquisition)

メモリと UI の両面から潜水艦データを収集する。`DalamudMemorySubmarineSnapshotSource` が Workshop 構造体を読み取り、`DalamudUiSubmarineSnapshotSource` は SelectString 監視によるフォールバックを提供する。両者は `DataAcquisitionGateway` 経由で `CharacterSnapshotAggregator` に統合され、`SnapshotPersister` がインメモリリポジトリへ同期する。フォールバックは partial クラスで分割済みだが、UI 依存部は未テストであり、AddonLifecycle が利用不可の場合は輪番ポーリングで動作する。

## アプリケーション層

通知関連が最も実装が進んでいる。`NotificationQueue`、`NotificationWorker`、`VoyageCompletionProjection` がスナップショット更新から Discord / Notion / Webhook への配信を仲介する。配信レート制御や再試行スケジューラが既に組み込まれている。`NotificationCoordinator` は複数チャネルをまとめて呼び出し、`LowImpactModeController` が取得頻度を調整する。コマンドやクエリはマーカーインターフェースのみ定義され、Mediator 実体は未導入。

## ドメイン層

`Submarine`, `Voyage`, `Alarm` などの値オブジェクトと、汎用的なリポジトリインターフェースを提供。識別子は構造体で厳密化されており、Guid と ContentId の組み合わせで一意性を担保している。永続化はまだ全てインメモリ実装で、実データベースへの接続は未実装。

## インフラ層

`PluginBootstrapper` が軽量 DI コンテナを構築し、Dalamud サービスと各実装を束ねる。通知クライアントは HTTP Webhook ベースで構築され、Discord / Notion / Generic Webhook が最小限のシリアライザで実装済み。`SnapshotStorageService` は設定ストアを流用した疑似永続化を行い、起動時に過去スナップショットをリポジトリへ再ロードする。現状は InMemory 系実装のみで、外部サービスや DB 接続は未着手。

## プレゼンテーション層

`OverviewWindowRenderer` と `NotificationMonitorWindowRenderer` の二種類の ImGui ウィンドウが用意されている。概要ウィンドウはキャラクター切り替えと潜水艦一覧、通知モニタは Webhook 設定とキューの状態表示を提供する。ビューは ViewModel を介してデータを取得し、`UIStateObserver` がスナップショット更新時にリフレッシュをトリガーする。ビューの外観は最小限で、カラーリングやスタイルの細部は未調整。

## テストとビルド

`tests` ディレクトリに `XIVSubmarinesRewrite.Tests` プロジェクトが存在するが、中身はまだ整備されていない。`dotnet build` は Dalamud 依存 DLL をローカル解決できない環境では失敗するため、CI を想定する場合はスタブ DLL の配置か条件付き参照が必要。通知ワーカーなど長時間動作するコンポーネントの動作試験は未整備。

## 既知の課題

- Dalamud / FFXIVClientStructs 依存 DLL が手動配置前提であり、開発環境に左右される。
- 永続化が設定ストアベースの疑似実装で止まっており、実データベース統合が未着手。
- UI 設定やテーマの最適化が未実装で、Apple / ChatGPT 風のビジュアル要件を満たしていない。
- テストカバレッジがゼロに近く、取得・通知・UI の動作確認は手動依存となっている。
- ファイル先頭ヘッダコメントのルールが未適用なファイルが多く、今後の整備が必要。

## 推奨アクション

1. Dalamud SDK を自動ダウンロードするスクリプト、またはビルド用スタブ DLL を整備し、`dotnet build` を安定化する。
2. スナップショット取得と通知キューのユニットテストを追加し、`XIVSubmarinesRewrite.Tests` を有効化する。
3. 永続化層をファイルベースもしくは軽量 DB に差し替え、再起動時のデータ保持を保証する。
4. UI のスタイルポリシーとローカライズ方針を決定し、ImGui コンポーネントへ反映する。
5. ファイルヘッダコメントと 300 行制限を順次適用し、リーダビリティを維持する。
