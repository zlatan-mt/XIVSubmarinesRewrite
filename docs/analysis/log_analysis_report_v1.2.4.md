### ログ出力分析レポート (v1.2.4) - 最終確定版

#### 1. 概要

v1.2.4のログ出力が過剰であるとの指摘を受け、ログの内容と必要性を再評価しました。本レポートは、複数回のレビューと技術的再検証を経て、当初の目的である「ログの静穏化」を最大限達成するための最終的な実装計画を提示します。

#### 2. ログの性質と最終判断

`Information`レベルで出力されている主要なログについて、その役割と過去の修正履歴（v1.2.2での根本原因修正、v1.2.3でのログ静穏化方針）を考慮し、以下の通り最終判断を下しました。

| 分類 | ファイル:行 | 内容 | 最終判断と理由 |
| :--- | :--- | :--- | :--- |
| **A. 冗長な監視ログ** | `src/Application/Notifications/DiscordCycleNotificationAggregator.cs:212` | 4隻サイクルの集約完了ログ。 | **`Debug`レベルに変更。** 整合性監視（重複検出）は、同ファイル内の162-176行目で`LogLevel.Warning`の専用ログとして既に実装済み。本ログは正常時も毎回出力される高頻度ノイズであり、`Information`レベルで維持する技術的合理性はない。 |
| **B. 性能計測** | `src/Application/Notifications/DiscordNotificationBatcher.cs:176` | バッチ処理の滞留時間 (`ageMs`) や遅延 (`overshootMs`) を含むフラッシュログ。 | **`Debug`レベルに変更。** 過去の性能問題解決に直接役立った明確な実績がないため。 |
| **C. 高頻度・運用** | `src/Integrations/Notifications/NotionWebhookClient.cs:82` | Notion Webhookの送信成功ログ。 | **`Debug`レベルに変更。** 正常系の高頻度ログであり、通常運用ではノイズとなる。 |
| **D. デバッグ専用** | `src/Plugin.cs:52` | `AddonLifecycle`の型情報を出力するログ。 | **`Debug`レベルに変更。** 開発者向けの純粋なデバッグ情報。 |
| | `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.Lifecycle.cs:68, 139` | UIイベントリスナー登録の成功ログ。 | **`Debug`レベルに変更。** プラグイン起動時の開発者向け情報。 |
| | `src/Application/Notifications/DiscordNotificationBatcher.cs:43` | バッチウィンドウ更新ログ。 | **`Debug`レベルに変更。** 設定変更時のデバッグ情報。 |

#### 3. 「開発モード」に関する調査

プラグインの `/xsr dev` コマンドは、現状では**開発者用UIタブを開く機能のみ**であり、システム全体のログレベルを変更する「開発モード」としては機能していません。

**将来的な提案:**
この `/xsr dev` コマンドを拡張し、実行時にログレベルを一時的に `Debug` に引き上げるトグル機能として実装すれば、通常はログを抑制しつつ、必要な時だけ性能計測ログなどを得ることが可能になります。

#### 4. 実装計画とチェックリスト

##### **方針**
当初の目的である「本番ログの静穏化」をv1.2.3の方針に沿って徹底するため、上記で特定した7箇所の`Information`ログをすべて`Debug`レベルに統一します。

##### **コード変更**
- [ ] `src/Application/Notifications/DiscordCycleNotificationAggregator.cs:212`
    - `LogLevel.Information` → `LogLevel.Debug`
- [ ] `src/Application/Notifications/DiscordNotificationBatcher.cs:176`
    - `LogLevel.Information` → `LogLevel.Debug`
- [ ] `src/Integrations/Notifications/NotionWebhookClient.cs:82`
    - `LogLevel.Information` → `LogLevel.Debug`
- [ ] `src/Plugin.cs:52`
    - `LogLevel.Information` → `LogLevel.Debug`
- [ ] `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.Lifecycle.cs:68`
    - `LogLevel.Information` → `LogLevel.Debug`
- [ ] `src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.Lifecycle.cs:139`
    - `LogLevel.Information` → `LogLevel.Debug`
- [ ] `src/Application/Notifications/DiscordNotificationBatcher.cs:43`
    - `LogLevel.Information` → `LogLevel.Debug`

##### **テスト修正**
- [ ] `tests/XIVSubmarinesRewrite.Tests/NotionWebhookContractTests.cs:51, 119`
    - **修正方針:** `LogLevel.Information` を期待しているアサーションを `LogLevel.Debug` に変更します。
- [ ] `tests/XIVSubmarinesRewrite.Tests/DiscordCycleNotificationAggregatorTests.cs:57, 78`
    - **修正方針:** `LogLevel.Information` を期待しているアサーションを `LogLevel.Debug` に変更、またはログ内容の検証が主目的であればレベル検証部分を削除します。

#### 5. 総括

本計画の実施により、当初の目的であったログの静穏化を、過去の修正方針との一貫性を保ちつつ達成できます。整合性監視はより適切な`Warning`レベルのログによって担保されており、本番環境のログ品質が大幅に向上します。
