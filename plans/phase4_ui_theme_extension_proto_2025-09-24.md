<!-- apps/XIVSubmarinesRewrite/plans/phase4_ui_theme_extension_proto_2025-09-24.md -->
<!-- Phase4 で UiTheme 適用拡張のプロトタイプを実装するための段取りをまとめます -->
<!-- レンダラの色統一を短時間で検証する目的で存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/UiTheme.cs, apps/XIVSubmarinesRewrite/plans/ui_theme_extension_plan_2025-09-24.md -->

# Phase4 UiTheme プロトタイプ計画 (2025-09-24)

## 0. ゴール
- `UiTheme` に追加カラー (`ErrorText`, `PanelBg`) を導入し、`NotificationMonitorWindowRenderer` にポイント適用する。
- 影響範囲を限定したクイック実装で視覚確認を完了し、次フェーズへの足がかりを得る。

## 1. 対象コンポーネント
1. `NotificationMonitorWindowRenderer` の設定セクション見出しと情報テキスト。
2. `NotificationMonitorWindowRenderer.Diagnostics` の残り時間テキスト色。
3. `UiTheme` 本体への定数追加と最小限のコメント更新。

## 2. 手順概要
1. `UiTheme` に `ErrorText` (鮮やかな赤) と `PanelBg` (ニュートラルグレー) を追加する。
2. `NotificationMonitorWindowRenderer` で Discord/Notion 有効状態のラベルを `ImGui.TextColored` に差し替える。必要に応じて小型ヘルパーを追加する。
3. `NotificationMonitorWindowRenderer.Diagnostics` で残り時間が 1 時間未満の場合 `WarningText`、0 に近い場合 `ErrorText` を表示する。
4. Dalamud 上でレンダリング確認し、スクリーンショットを `logs/2025-09-24/ui-theme` に保存する。

## 3. 検証とフォロー
- スクリーンショットと共に `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-24.md` に観察結果を残す。
- 問題があれば `ui_theme_extension_plan_2025-09-24.md` のタスクを更新し、リスクを明記する。
- 問題なしなら次フェーズで Overview テーブルやキュー表示への展開を検討する。

## 4. 完了条件
- 追加カラー定数が他ファイルで参照されている。
- レンダリング確認のスクリーンショットとコメントが保存されている。
- 元に戻す必要のある設定変更が無い状態に戻している。
