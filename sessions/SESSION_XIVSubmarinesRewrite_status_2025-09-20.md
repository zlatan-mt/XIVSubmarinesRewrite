<!-- docs/sessions/SESSION_XIVSubmarinesRewrite_status_2025-09-20.md -->
<!-- XIV Submarines Rewrite の実装状況を 2025-09-20 時点でまとめた記録 -->
<!-- 次セッションへ変更点と未対応事項を引き継ぐため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/**, docs/prompts/commands.md, docs/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-20.md -->

# XIV Submarines Rewrite 実装状況 (2025-09-20)

## 本日の実装サマリ
- Overview UI: 起動時の強制表示を抑止し、ユーザーが開いた状態のみ記憶するよう `UiPreferences.OverviewWindowVisible` を利用。
- キャラクター管理: `CharacterRegistryPreferences` を導入し、ログイン前でもキャラクター名を復元。`NotificationEnvelope` へ確実に名前をセットできるよう `ICharacterRegistry.GetIdentity` を追加。
- Discord 通知: `DiscordNotificationBatcher` によるキャラクター単位の集約を実装。0.5〜15 秒の可変バッチ窓を設定から編集可能にし、保存時に即反映。
- Notion 連携: 既存のフィールド (`Character`, `Submarine`, `Route`, `Duration`, `Hash` 等) を維持し、Zaps など既存フローへの互換性を確保。
- Webhook 機能: 使用されていない Generic Webhook を削除し、設定項目・テストも整理。

## 技術的詳細
- `VoyageNotificationFormatter` は Discord 向けに残り時間を表示しつつ、Notion には従来の航海時間を提供。
- `NotificationMonitorWindowRenderer` の「通知設定」セクションで `DiscordBatchWindowSeconds` を編集。保存時に `DiscordNotificationBatcher.UpdateWindow()` を呼び、ウィンドウ更新前後の挙動を統一。
- `NotificationCoordinator` から Webhook 依存を排除し、Discord/Notion の二系統へ集中。
- テスト: `dotnet build`, `dotnet test` (9 件) を実施し、既存ケースがすべて成功。

## 未対応・課題
- Notion には従来通り航海時間 (Duration) のみを提供しているため、残り時間が必要な場合は追加キーで拡張する必要あり。
- キャラクター名の書き戻しは設定ストア依存。別環境へ移した場合は初回取得まで Unknown 表示が続く。ログの補完や UI でのリフレッシュ導線が課題。
- Discord 集約はキャラクター単位で同時帰港をまとめている。時間差が大きい場合などの境界条件は要確認。

## 次の実装計画
1. **Notion 併記情報**: 互換フィールドは維持しつつ、`RouteDisplay` や `ArrivalLocal` など追加レコードを検討。既存 Zap を壊さない形式で付加情報を提供する。
2. **Discord 集約チューニング**: バッチ窓の実測データをログに出し、適切なデフォルト (例: 1.5 秒) を決める。通知が多いユーザー向けに最適化。
3. **ForceNotifyUnderway のテスト**: クールダウンや差分通知を xUnit で自動検証できるようにし、将来のリグレッションを防止。
4. **キャラクター名の復元 UX**: 設定ファイルが欠損した場合に UI で再取得できるボタンやトースト通知を検討する。

## 参考コマンド
- `bash scripts/fetch_dalamud_libs.sh` : Dalamud 依存 DLL の取得
- `dotnet build apps/XIVSubmarinesRewrite/XIVSubmarinesRewrite.csproj`
- `dotnet test apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/XIVSubmarinesRewrite.Tests.csproj`

