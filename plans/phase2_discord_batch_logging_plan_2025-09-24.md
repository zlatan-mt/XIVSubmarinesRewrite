<!-- apps/XIVSubmarinesRewrite/plans/phase2_discord_batch_logging_plan_2025-09-24.md -->
<!-- Phase2 で Discord バッチ窓を計測するためのログ取得計画 -->
<!-- ageMs と overshootMs の採取と評価基準を明確にするため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/docs/notifications/discord_batch_window_plan.md, apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs -->

# Phase2 Discord バッチログ計画 (2025-09-24)

## 0. 事前設定
- Dalamud クライアントを起動し、通知プラグインが最新ビルドか確認する。
- Phase1 で差し替えたステージング Webhook を保持し、テストチャンネルが静かな時間帯を選ぶ。
- `logs/2025-09-24/discord-batch` を作成し、`run-01` 〜 `run-05` のサブフォルダを先に用意する。
- `docs/notifications/discord_batch_window_plan.md` の計測表を開き、ローカルコピーを `discord_batch_window_plan_2025-09-24.xlsx` として保存する。
- Dalamud のログレベルが `Information` であることを `Dalamud.log` 冒頭で再確認する。

## 1. ログ採取手順
1. テストキャラクターで潜水艦管理コンソールを開き、単艦の出航通知を発火させる。
2. Discord に送信されたメッセージ時刻と Dalamud ログの `DiscordNotificationBatcher` 行を同時計測する。
3. `Dalamud.log` から対象時刻の 30 行を `rg "DiscordNotificationBatcher" -n Dalamud.log` で見つけ、`run-0X/dalamud.log` にコピーする。
4. 同手順を 5 回繰り返す。3 回目と 4 回目は複数艦を同時出航させる。
5. 各回で `ageMs` と `overshootMs` をログから抜き出し、Excel 計測表へ追記する。

## 2. データ整理
- 各 `run-0X` フォルダに `dalamud.log` と `discord_message_timestamp.txt` を必ず保存する。
- Excel には `run-id`、Discord Timestamp、`ageMs`、`overshootMs`、同時艦数、備考を記載する。
- 1 回の測定ごとに平均値セルを更新し、9 回目以降はローリング平均を別列で保持する。

## 3. 評価指標
- 平均 `ageMs` が 1500 ± 300 ms に収まるか確認する。


- `overshootMs` が 400 ms を超えた回が 2 回以上ならバッチ窓拡大を検討する。


- 全回の `overshootMs` が 100 ms 未満ならバッチ窓短縮候補として記録する。
- 同時艦出航時の `overshootMs` が単艦より 200 ms 以上大きい場合は通知分割の必要性をメモする。

## 4. 調整と再計測
1. 調整が必要な場合、`pluginConfigs/XIVSubmarinesRewrite.NotificationSettings.json` の `DiscordBatchWindowSeconds` を暫定変更する。
2. Dalamud を再読み込みし、同じフローで 3 回の追試を実施する。
3. 新しい `run-06` 〜 `run-08` フォルダを追加し、同様にログと Timestamp を保存する。
4. Excel のローリング平均を更新し、変更前後の比較表を作成する。
5. 安定値が決まったら差分と根拠を `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-24.md` の Phase2 節へ追記する。

## 5. 完了チェック
- `run-01` 〜 `run-05` (必要なら `run-08`) のフォルダにログと Timestamp が揃っている。
- Excel 計測表と sessions ノートへ数値と判断が反映されている。
- 仮変更した `DiscordBatchWindowSeconds` を元値へ戻したか、または更新根拠を明記した。
- Discord とコンソールログで異常が出ていない。

