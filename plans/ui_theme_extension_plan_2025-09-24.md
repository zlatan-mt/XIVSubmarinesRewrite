<!-- apps/XIVSubmarinesRewrite/plans/ui_theme_extension_plan_2025-09-24.md -->
<!-- UiTheme 適用範囲を拡張するためのタスクリスト -->
<!-- レンダラ間の配色統一を段階的に進める指針として存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.Diagnostics.cs -->

# UiTheme Extension Plan (2025-09-24)

## 現状整理
- `OverviewWindowRenderer` はハイライトと補助テキストに `UiTheme` を使用しているが、テーブル内の色指定は未統一。
- `NotificationMonitorWindowRenderer` シリーズはベース色を直接書いておらず、重要テキストの色分けが不足している。
- `NotificationMonitorWindowRenderer.Diagnostics` では警告色に `UiTheme.WarningText` を使用しているが、残り秒数やクールダウン行に強調が無い。

## 適用候補
1. 通知設定タブ: 有効/無効状態を `UiTheme.AccentPrimary` / `UiTheme.MutedText` で統一表示。
2. キュー一覧テーブル: ステータス列の色分け (`Pending` = `PrimaryText`, `DeadLetter` = `WarningText`) を `UiTheme` へリファクタ。
3. ForceNotify 診断: クールダウン残り時間が 1 時間未満の場合 `WarningText` を使用し、通常時は `MutedText`。
4. Overview サマリ: 航行中/完了カウンタ用に `MutedText`、有効率向上には `AccentPrimary` を使うヘルパーを追加。

## タスク分解 (Quick -> Iterative)
1. `UiTheme` に `ErrorText` と背景用 `PanelBg` を追加し、既存ファイルへ影響しないことを確認する。
2. `NotificationMonitorWindowRenderer` の設定セクションでチェックボックスラベルの `ImGui.TextColored` 呼び出しを導入し、状態に応じてテーマ色を適用する。
3. キュー/デッドレター表のテーブル行に対して `ImGui.PushStyleColor` を使った色付けヘルパーを追加し、`UiTheme` を参照させる。
4. ForceNotify 診断の残り時間描画に閾値条件を追加し、色分けロジックを `UiTheme` へ統一する。
5. `OverviewWindowRenderer` のテーブルヘッダや最終更新テキストに `UiTheme.MutedText` を適用し、ファイルの LOC を 300 未満に抑えるため必要箇所を部分クラスへ分割する案を検討する。

## 検証メモ
- 変更ごとに `XIV Launcher` 上のレンダリングを撮影し、暗色/明色テーマで視認性を確認する。
- 主要テキスト色が WCAG AA コントラスト (4.5:1) を満たすか簡易チェックを行い、問題があれば数値を調整する。
