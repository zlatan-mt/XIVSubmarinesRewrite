<!-- apps/XIVSubmarinesRewrite/docs/notifications/discord_batch_window_plan.md -->
<!-- Discord バッチ窓の計測と調整方針を記録するメモ -->
<!-- ログ取得手順と推奨アクションを共有するために存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs, docs/notifications/underway_notification_validation.md -->

# Discord バッチ窓評価メモ (2025-09-24 更新)

## CLI ログ取得手順
- `dotnet run --project tools/DiscordCycleSimulator/DiscordCycleSimulator.csproj -- --run=cycle-test --validate` を実行し、4 隻サイクルを一括再生する。
- 実行結果は `logs/<date>/notification-cycle/<run>/` に保存される。`discord/` と `notion/` に JSON、`meta/` に `manifest.json` と `summary.json` と `report.html` が出力される。
- `summary.json` の `AllCyclesValid` が `true` の場合は Discord 集約が成立している。`Issues` が列挙されたときは `report.html` で該当サイクルを確認する。
- Dalamud 実機ログを追加で採取する際は従来どおり `DiscordNotificationBatcher` の `ageMs` / `overshootMs` を比較目的で記録する。

## 計測サマリフォーマット
| 試行 | 通知件数 | ageMs 平均 | overshootMs 最大 | 所感 |
| --- | --- | --- | --- | --- |
| 1 |  |  |  |  |
| 2 |  |  |  |  |
| 3 |  |  |  |  |
| 4 |  |  |  |  |
| 5 |  |  |  |  |

CLI 実行後は必要項目のみ表へ追記し、詳細は `meta/report.html` のスクリーンショットを併置する。

## 調整判断の目安
- `summary.json` の各サイクルで `DiscordFieldCount` や `NotionPayloadCount` が 4 件と一致しない場合、`DiscordCycleNotificationAggregator` のロジックやテストデータを点検する。
- Dalamud 実測で `ageMs` が 1.5 秒 ±0.3 秒から外れるケースが連続する場合は `DiscordBatchWindowSeconds` を調整する。
- `overshootMs` が 400 ms を超える場合は 1.7 秒へ拡大、100 ms 未満なら 1.3 秒へ短縮する仮説を検証する。
- 設定変更後は CLI と Dalamud の双方で 3 回以上再計測し、`summary.json` と手動ログを比較して差分を記録する。

## 自動バリデーションの運用
- `meta/manifest.json`: サイクル別に Discord/Notion のファイルパスと件数を保持する。集約テストの証跡として保存する。
- `meta/summary.json`: `AllCyclesValid` と `Issues` を CI 判定に利用する。失敗時は `report.html` へリンクを貼りレビューできるようにする。
- `meta/report.html`: Discord/Notion 出力のファイルパスと問題点を表形式で確認できる。ドキュメントへ貼り付ける際の元ネタとして活用する。
- `simulation.log`: CLI 実行ログ。集約が走らなかった場合は `Decision suppress` の連続などをここでチェックする。

## 次のアクション (Pending)
- Phase5 実施時は必ず CLI の `--validate` モードを走らせ、`summary.json` を sessions ノートへ添付する。
- Dalamud 実測の `ageMs` / `overshootMs` を Excel または Notion に追記し、CLI 結果と乖離があれば原因を整理する。
- `report.html` のスクリーンショットを docs/notifications へ貼り付け、監視指標を明文化する。
