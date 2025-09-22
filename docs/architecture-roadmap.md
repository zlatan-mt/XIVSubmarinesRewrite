# XIV Submarines Rewrite — アーキテクチャロードマップ

## 目的
- 工房入室時のメモリスキャン負荷を抑えつつ、信頼性の高いデータ取得を維持する。
- モジュール境界を明確化し、リファクタリングしやすく保守性の高い構造に再編する。
- テスト・テレメトリ・自動化パイプラインを整備し、バグ検知と修正を継続的に回せるようにする。

## 全体構成 (改訂案)
- **Domain**: Submarine / Voyage / Route / Alarm / Profile などのドメインモデルと、集約リポジトリ・ファクトリ。
- **Acquisition**: Dalamud Hook を束ねる Data Acquisition Gateway、`IDataSource` 実装 (Memory, UI fallback, Cache)。
- **Application Services**: スケジューラ、通知、ルート解析、低負荷モード制御、EventBus / Mediator。
- **Presentation**: ImGui UI、コマンドハンドラ、ViewModel 層。差分更新と状態同期を担う。
- **Infrastructure**: 設定管理、永続化、ロギング、テレメトリ、DI コンテナ、Secrets ストア。
- **Integrations**: Discord / Notion / Webhook クライアント、レートリミット制御、将来の外部連携ポイント。

## データフロー概要
1. Dalamud イベント → Data Acquisition Gateway が軽量チェックを実施。
2. Gateway は状態に応じて Acquisition Pipeline を起動し、SnapshotCache に最新情報を保存。
3. Snapshot 更新イベントが EventBus に publish され、Application Services が差分判定・アラーム計算を実行。
4. UI / 通知 / 外部連携はサブスクライブして非同期に反映。負荷閾値超過時は低負荷モードへ移行。

## 工房入室時負荷削減の要点
- **Adaptive Sampling**: 入室直後は短周期、安定後は長周期へ自動移行。CPU/GC 圧測定に応じて閾値制御。
- **Deferred Heavy Scan**: 重いメモリスキャンは遅延キューで段階的に実行し、優先度は UI 要求・未取得データに応じて決定。
- **SnapshotCache & Delta Delivery**: Snapshot 差分のみを各層へ配信し、同一データの再計算を避ける。
- **Fail-Safe**: 連続失敗時はバックオフ + UI 通知。低負荷モードでは UI 表示と通知は継続、スキャン頻度を最小化。

## リファクタリング指針
- **Bounded Context**: Domain / Acquisition / Scheduler / Notifications / Telemetry / Presentation に namespace (もしくはマルチプロジェクト) を分割。
- **CQRS/Mediator**: コマンド (設定変更等) とクエリ (表示用データ取得) を分離し、テスト容易性を向上。
- **Config & Secrets**: `ISettingsProvider` (JSON) と `ISecretStore` (暗号化) を抽象化し、テストでは InMemory 実装を利用。
- **共通ユーティリティ**: ログ/例外/テレメトリを Infrastructure に集約、DI で交差関心事を注入する。

## 自動化・品質保証
- **テレメトリ**: `PerformanceMonitor` で CPU/GPU/GC 指標、`AcquisitionMetrics` でスキャン成功率を収集。閾値逸脱は通知。
- **テスト階層**: Domain (純粋ロジック)、Infrastructure (メモリスタブ)、Application (シナリオ再現)、Integration (Discord/Notion モック)。
- **Regression Harness**: バグ報告テンプレート → 再現スナップショットを CI が読み込み、専用テストを自動生成/実行。
- **Static Analysis**: Roslyn Analyzer + StyleCop を導入し、Mediator ハンドラや設定クラスの規約違反を検出。

## マイルストーン (更新)
1. **M1: Core + Acquisition 基盤**
   - Data Acquisition Gateway、SnapshotCache、基本テレメトリの実装。
   - Domain モデル/Repository、ユニットテスト基盤、DI 構成。
2. **M2: アプリサービス + UI リファクタ**
   - Mediator/EventBus、低負荷モード制御、差分更新対応 UI。
   - Config/Secret 抽象、主要 ViewModel の分離。
3. **M3: 通知・外部連携 + 自動化**
   - Discord/Notion クライアント刷新、レート制御、Regression Harness の導入。
   - GitHub Actions でテレメトリ閾値監視・バグ再現テストを実行。
4. **M4: 最適化と拡張**
   - 追加連携 (Webhook 等)、パフォーマンスチューニング、ドキュメント整備。
   - 取得負荷の継続監視としきい値チューニングの自動化。
