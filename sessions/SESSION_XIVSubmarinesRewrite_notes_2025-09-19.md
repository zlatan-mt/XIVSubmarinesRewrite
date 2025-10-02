<!-- docs/sessions/SESSION_XIVSubmarinesRewrite_notes_2025-09-19.md -->
<!-- XIV Submarines Rewrite の 2025-09-19 セッションノート -->
<!-- 今日までの実装内容と観測結果を時系列で整理し、次回セッションへ引き継ぐ目的で存在します -->
<!-- RELEVANT FILES: docs/plans/XIVSubmarinesRewrite_bugfix_plan_2025-09-18.md, apps/XIVSubmarinesRewrite/src/** -->

# XIV Submarines Rewrite セッションノート (2025-09-19)

## 進捗サマリ

- Overview ウィンドウに通知タブを統合。潜水艦と通知を同一ウィンドウで切り替え可能。通知タブから設定の閲覧・編集・キュー監視が行える。
- NotificationMonitor へ「選択キャラクターの通知を即時送信」ボタンを追加。ForceNotifyUnderway を ON にすれば航行中の艦も手動で通知可能。
- 通知設定は保存ボタン押下時のみ JSON (`pluginConfigs/XIVSubmarinesRewrite.NotificationSettings.json`) に書き込む。自動保存は削除済み。
- DalamudJsonSettingsProvider を導入し、設定は Dalamud の `ConfigDirectory` に永続化。Discord Webhook などが再起動後も保持される。
- UI フォールバックのスコアリングを再設計。`Submarine-1 [Rank131] [探索中:残り時間 …]` のような日本語行も確実にスコア閾値を超える。否定語フィルタや構造チェックも整備済み。
- `dalamud.log` で `Row => 'Submarine-n'` と出力されており、航行中の艦が UI 経路で正常に拾えていることを確認。

## 未解決／継続課題

1. ForceNotifyUnderway を ON にしても自動通知が「already seen underway status」で抑止される。再起動前から航行中だったケースで通知が飛ばない。手動トリガーは成功するが、自動の条件調整が必要。
2. キャラクター名が再び表示されない事象が再発。UI フォールバックのみでスナップショットが更新された場合、`CharacterRegistry` が補完しきれていない可能性あり。
3. ForceNotifyUnderway の自動再送タイミング（リトライ間隔）をどう制御するか、仕様を確定する必要あり。

## 実装詳細メモ

### UI / 設定周り
- OverviewWindowRenderer: タブ構成 (潜水艦/通知)。通知タブ内で NotificationMonitorWindowRenderer.RenderInline() を使用。
- NotificationMonitorWindowRenderer:
  - 手動送信ボタン `ImGui.Button("選択キャラクターの通知を即時送信")` を追加。SnapshotCache から最新データを取得し、Completed/Underway (ForceNotify時) をキューへ投入。
  - 開発オプションに `ForceNotifyUnderway` のチェックボックス。
  - 自動保存は削除済み。保存ボタン押下時にのみ settingsProvider.SaveAsync。

### UI フォールバック
- DalamudUiSubmarineSnapshotSource.RowExtraction.Helpers:
  - 角括弧を全て展開し、`残り時間` テキストも評価対象に追加。
  - スコア: Route(+2), ETA(+2), Status(+2), Rank(+1), Name(+1), Negative(-3)。Route or ETA 無しでも Status+Remaining で閾値 >=3 に到達可能。
  - `Row score below threshold` の Trace ログでスコア状況を確認可能。

### 通知
- VoyageCompletionProjection:
  - ForceNotifyUnderway の Trace ログを追加 (`enqueuing`/`skipping` の理由を出力)。
  - 現状は `prior.Status == Underway` の場合スキップ。航行中でも一度通知したい場合は再設計が必要。

### テスト
- UiFallbackFilterTests: `Submarine-1 / Rank131 / 探索中:残り時間 …` ケースを追加し、閾値を満たすことを検証。
- `XIVSubmarinesRewrite.Tests` のテスト DLL 出力に vendor DLL をコピーするよう csproj を調整済み。

## 次回対応候補
1. ForceNotifyUnderway の自動通知仕様を再検討。例: 初回検出時に通知し、その後は一定間隔のクールダウンを設ける等。
2. キャラクター名が UI フォールバックのみで Null になるケースを調査。CharacterRegistry と Snapshot 統合の見直し。
3. 通知メッセージ（所要時間や出航時刻が古すぎる場合のフォーマット）を調整。手動トリガーのサンプルでは 2022→2025 のような差があるため、見栄え改善を検討。

## 参考ログ抜粋
```
[dalamud.log]
…
[Notifications] ForceNotifyUnderway skipping voyage <CID:Slot:Guid>; already seen underway status.
[UI Inspector] Row => 'Submarine-1'
```

## 備考
- ForceNotifyUnderway をテストする際は、通知タブで設定変更後「通知設定を保存」を必ず押す。
- 手動トリガーは Discord 通知の確認に有効。設定が正しければ4件まとめて送信される。
- CI ワークフロー `.github/workflows/ci.yml` は XIVSubmarinesRewrite の Restore/Build/Test のみを実行するよう修正済み。

