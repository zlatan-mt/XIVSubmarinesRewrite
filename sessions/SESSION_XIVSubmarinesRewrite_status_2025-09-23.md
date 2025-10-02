<!-- docs/sessions/SESSION_XIVSubmarinesRewrite_status_2025-09-23.md -->
<!-- XIV Submarines Rewrite の実装状況を 2025-09-23 時点でまとめた記録 -->
<!-- 進捗共有と残課題の俯瞰を目的として存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs, sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-23.md -->

# XIV Submarines Rewrite 実装状況 (2025-09-23)

## 本日の実装サマリ
- Notion 連携: `RouteDisplay`/`ArrivalLocal` を辞書に追加し、ルート未整形時は `Route` へフォールバック。互換性を崩さず追加キーを提供。
- Discord 通知: バッチ窓をデフォルト 1.5 秒に変更し、ウィンドウ更新時とフラッシュ時のログに `ageMs`/`overshootMs` を出力。
- ForceNotify: クールダウン残時間と完了遷移のユニットテストを追加し、カバレッジを補強。
- UI/UX: 通知設定タブへキャラクター名の復元ボタンとトーストを導入。共通パレット `UiTheme` でアクセント色を統一。
- 通知設定に航海完了/出航の送信フラグを追加し、出航時は Discord/Notion で次の帰港情報と残り時間を標準表示。
- 出航通知検証チェックリストを `docs/notifications/underway_notification_validation.md` に整備し、QA 手順と記録フォーマットを共有。

## 技術的詳細
- `VoyageNotificationFormatter` 内で `NormalizeRouteDisplay` を追加し、Notion 側の空欄を解消。
- `DiscordNotificationBatcher.UpdateWindow` に情報ログを追加し、`FlushAsync` でウィンドウ滞留の超過量を計測。
- `ForceNotifyUnderwayTests` へ 2 ケースを加え、手動バッチトリガー時のリグレッションを検知可能にした。
- `NotificationMonitorWindowRenderer` の復元ボタンはスナップショットキャッシュを再登録し、欠損状態からの復旧件数をトースト表示。
- `UiTheme` がアクセント (ディープブルー) とミュートグレーを提供し、概要ウィンドウのメトリクスを強調。

## 未対応・課題
- Dalamud 依存 DLL を配置しない限り `dotnet build XIVSubmarinesRewrite.csproj` は `FFXIVClientStructs` 不明で失敗。
- キャラクター名復元は最新スナップショットがない環境では無効果。必要なら Dalamud API からの直接取得が必要。
- Discord バッチの最適値 1.5 秒は仮置き。実運用ログをもとに再評価が必要。
- Notion Zap への実運用検証が未完で、互換性確認後にドキュメント更新が必要。
- 出航通知の文言チューニング（多言語対応含む）と追加ログ整備は継続検討。

## 次の実装計画
1. 出航通知チェックリストに沿って Discord/Notion ステージングで検証し、結果を記録する。
2. Discord バッチ窓 (1.5 秒) の実測ログを収集し、デフォルト値見直し案を作成する。
3. Notion Zap で `Remaining` フィールドを確認し、`docs/notifications/notion_payload_plan.md` を更新する。
4. UI カラー適用範囲を診断表示やその他ビューにも広げる案を検討する。

## 参考コマンド
- `dotnet test --no-build tests/XIVSubmarinesRewrite.Tests/XIVSubmarinesRewrite.Tests.csproj`
- `dotnet build XIVSubmarinesRewrite.csproj` (依存 DLL 未配置時は失敗)
