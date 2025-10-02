<!-- apps/XIVSubmarinesRewrite/plans/phase5_notification_cycle_validation_2025-09-24.md -->
<!-- Discord 集約ロジックと Notion 連携の統合検証を行うための実装計画 -->
<!-- 4 隻巡回シナリオで確実に 1 通へ集約し、副作用を防ぐため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-24.md, apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordCycleNotificationAggregator.cs, apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs -->

# Phase5 通知サイクル統合計画 (2025-09-24)

## フェーズ目的
- 4 隻固定で運用する艦隊の帰港→出航サイクルが Discord で 1 通にまとまることを実機で検証し、必要なら実装を微調整する。
- Notion Webhook が新しい `payload` + `metadata` 形式で問題なく更新されるか確認する。
- ログ収集とメトリクス化を行い、後続のリファクタリングや監視に使えるデータを残す。

## フェーズ構成
1. **Preparation** — テスト環境とログ採取基盤の整備。  
2. **Cycle Validation** — 4 隻サイクルを 2 回実施し、期待通りの Discord/Notion 出力を確認。  
3. **Adjustment & Refactor** — 必要に応じて集約条件やログメッセージを改善。  
4. **Documentation & Follow-up** — 成果と残課題をセッションノート・ドキュメントへ反映。

---

## 1. Preparation
- `NotificationSettings` の Discord/Notion Webhook をステージング用に差し替え、既存の差し替えメモを `logs/2025-09-24/notification-cycle/webhooks.txt` へ追記する。
- `logs/2025-09-24/notification-cycle` と配下に `discord`, `notion`, `dalamud`, `screenshots` フォルダを作成する。
- Dalamud のログレベルを `Debug` に temporarily 引き上げ、`DiscordCycleNotificationAggregator` ログが記録されるようにする。  
  終了後は `Information` へ戻す。
- `docs/notifications/discord_batch_window_plan.md` を開き、Cycle 測定用タブを追加しておく (列: `cycle-id`, `dispatchCount`, `expectedMessageCount`, `actualMessageCount`, `notes`)。

## 2. Cycle Validation
- **Cycle 1 (Baseline)**
  1. 4 隻が帰港済み状態でログを採取し、`Discord aggregator recorded completion` の件数を記録。  
  2. 順番に 4 隻を出航させる。各出航直後に `Discord aggregator recorded underway` と `flushing cycle` ログが出るか確認する。  
  3. Discord チャンネルで 1 通のみ投稿されたことをスクリーンショット取得し、`discord/cycle1.png` に保存する。  
  4. Notion Zap の履歴で同タイミングのイベントが 4 件分記録されていることを確認し、`notion/cycle1.json` として保存する。

- **Cycle 2 (Stress)**
  1. 4 隻の帰港→出航を 5 分以内に連続で実施し、集約が崩れないかを検証する。  
  2. ForceNotifyUnderway がオンの場合もログと通知が意図どおりかチェックし、必要なら設定を切り替えて再試行する。  
  3. Discord 側の投稿数とログの `flushing cycle` 件数が一致するか記録する。

- **結果整理**
  - 各サイクルごとに `dispatchCount`, `expectedMessageCount`, `actualMessageCount`, `overshootMsAvg`, `notes` を `discord_batch_window_plan.md` に追記する。  
  - Notion 側は `Remaining` フィールドが存在するかを最優先で確認し、差異があればスクリーンショットとともに `sessions` ノートへ残す。

## 3. Adjustment & Refactor
- 集約条件が厳しすぎて通知が出ない場合、`DiscordCycleNotificationAggregator` の `CycleSize` 変更や `CycleReady` 判定ロジックを調整する。  
  調整は `DiscordCycleNotificationAggregator.cs` へ限定し、`NotificationCoordinator` は極力再修正しない。
- ForceNotifyUnderway と集約の兼ね合いでログが過剰になっている場合、`log.Log(LogLevel.Trace, ...)` の粒度を見直し `Information` と `Debug` を切り替える。  
  必要なら `ILogSink` のカテゴリを細分化するリファクタリングを計画 (別フェーズへ切り出し可)。
- Notion 側でカラムマッピングの追加が必要になった場合、`docs/notifications/notion_payload_plan.md` を更新し、`payload` のキー追加や変換を検討する。

## 4. Documentation & Follow-up
- `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-24.md` に Phase5 の結果を追記し、Discord と Notion の挙動、ログ、課題をまとめる。
- `docs/notifications/discord_batch_window_plan.md` と `docs/notifications/notion_payload_plan.md` に測定結果・サンプルを追記する。  
  特に Discord は 4 隻サイクル専用セクションを設け、将来の監視指標を定義する。
- 残課題があれば `plans/phase6_*` (仮) を作成し、ForceNotify の再設計や UI 側の通知設定拡張などをリストアップする。
- Webhook を元に戻し、セッションノートにタイムスタンプ付きで記録する。

## 成果物・完了条件
- Discord で 2 サイクルとも集約が 1 通になったスクリーンショットが `logs/2025-09-24/notification-cycle/discord` に存在する。
- Notion Zap の履歴 JSON に `Remaining` が正しく入っており、`docs/notifications/notion_payload_plan.md` に反映済み。  
- `discord_batch_window_plan.md` へ Cycle 計測結果が追記され、サイクル単位の `overshootMs` 平均が確認できる。
- セッションノートに実施ログと結論が追記され、次フェーズのタスクが明記されている。

