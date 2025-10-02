<!-- apps/XIVSubmarinesRewrite/plans/phase10_titlebar_character_scope_plan_2025-10-02.md -->
<!-- タイトルバーのレイアウト崩れとキャラクタ保存条件の調整をまとめた計画です -->
<!-- v1.0.0 リリース直前に残った UI/永続化の課題を解消するため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/MainWindowRenderer.Layout.cs, apps/XIVSubmarinesRewrite/src/Application/Services/CharacterRegistry.cs, apps/XIVSubmarinesRewrite/tests/Playwright/main-window.spec.ts -->

# Phase10 タイトルバー整形 & キャラクタ永続化調整 計画 (2025-10-02)

## ゴール
- タイトルバーのアイコン/テキスト/バージョン表示がつぶれず、下端が切れないようにする。
- 潜水艦を操作したキャラクターのみ永続化されるようにし、既存データもクリーンアップする。

---

## Phase10-A — タイトルバー UI リファイン

### 実装タスク
- [ ] `MainWindowRenderer.Layout.cs` の `DrawToolbar()` を棚卸しし、アイコン・製品名・バージョン文字列のレイアウト計測を追加。
- [ ] タイトル領域をフレックス化し、アイコン／テキスト／バージョンの行間・ベースラインを `ImGui.AlignTextToFramePadding()` と個別 `PushStyleVar` で揃える。
- [ ] バージョンラベルが長い場合に省略記号または折返しを行うヘルパー `TruncateForWidth()`（新規）を実装し、ツールチップで完全表示を提供。
- [ ] 文字欠けを防ぐため、ツールバー高さと `FramePadding` を見直し、1.25×スケーリング時の文字クリップを再現して調整。
- [ ] カラーコントラスト情報とスウォッチ群を 2 行目に配置し、DEV トグルと干渉しないよう `ImGui.TableSetColumnIndex` のウェイトを調整。

### テスト / 検証
- [ ] `npm run test:ui -- --grep "@main-window"` を実行し、スクリーンショット差分が無いことを確認。
- [ ] RendererPreview で出力した HTML を手動確認し、タイトルバーエクスポート結果と配色情報が整合していることを確認。
- [ ] 解像度 1920×1080 / UI スケール 100%・125% で Dalamud 実機を確認し、文字切れが解消されていることをスクリーンショット取得で証明。

---

## Phase10-B — キャラクタ保存条件の絞り込み

### 実装タスク
- [ ] `CharacterRegistry.RegisterSnapshot()` で `snapshot.Submarines.Count == 0` のキャラクターは即時保存しないようガード条件を追加。
- [ ] 潜水艦操作履歴を判定するため、`CharacterRegistry` 内に最終操作時刻または艦数を追跡するフィールドを追加し、操作のあるキャラクターのみ `PersistDescriptor()` を実行。
- [ ] 起動時に読み込む `CharacterRegistryPreferences.Characters` から、保存済みだが潜水艦操作履歴を持たないレコードをクリーンアップするメソッドを新設。
- [ ] `ICharacterRegistry` に「潜水艦操作キャラクター一覧」取得 API を追加し、UI 側（Overview/Notification）で不要なキャラが表示されないことを確認。

### テスト / 検証
- [ ] `tests/XIVSubmarinesRewrite.Tests` に `CharacterRegistryTests` を新設し、潜水艦無しスナップショットで永続化されないこと、潜水艦ありの場合のみ保存されることを検証。
- [ ] `npm run test:ui -- --grep "@overview"` を実行し、キャラクター切り替え UI が期待通り動作することを確認。
- [ ] ダミー設定ファイルを使った統合テスト（Playwright でキャラクター選択）を行い、保存対象が限定されていることを確認。

---

## Phase10-C — QA / ドキュメントアップデート

### 実装タスク
- [ ] `CHANGELOG.md` に Phase10 の修正概要（タイトルバー整形・キャラクタ永続化フィルタ）を追記。
- [ ] `README.md` の開発者向けセクションにタイトルバー UI 調整の注意点（UI スケール対応）を追記。
- [ ] `docs/notifications/force-notify-guide.md` へ「潜水艦操作キャラクターのみ保存される」仕様を追記し、既知の制限を更新。
- [ ] `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-03.md`（新規）で Phase10 作業とテスト結果を記録。

### テスト / 検証
- [ ] `dotnet build --configuration Release` と `dotnet test --configuration Release --no-build` を実行し、全体ビルドの健全性を確認。
- [ ] `npm run test:ui:release` を実行し、リリーススモークに後退が無いことを確認。

---

## リスクとフォールバック
- UI 調整でテーブル幅が不足する場合は、カラーメトリクスを省略できるよう Feature Flag (`UiPreferences.ShowToolbarMetrics`) を用意する。
- キャラクタクリーンアップで既存ユーザーの設定が消える恐れがあるため、初回起動時にバックアップ (`character-registry.backup.json`) を生成。

---

## 品質ガードライン
- 新規コードはコメントヘッダーと 250 行以下を維持。複雑なロジックは補助メソッド化し、重複を避ける。
- Dalamud / ImGui 呼び出しでマジックナンバーを使う場合は定数化し、UI スケール対応の計算式を添える。
- 永続化の変更は必ず同期メソッド内でロックを保持し、データ競合を避ける。
- Playwright 新規シナリオには `@main-window` または `@release` タグを付与し、CI コマンドで抽出可能にする。

---

## 参照
- スクリーンショット: `logs/スクリーンショット 2025-10-02 135913.png`
- 関連セッション: `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-02.md`

