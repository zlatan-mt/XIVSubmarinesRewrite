<!-- apps/XIVSubmarinesRewrite/plans/phase9_release_execution_plan_2025-10-02.md -->
<!-- Phase9 リリース準備から配布までの実行計画をまとめます -->
<!-- Phase8 完了後の成果をリリース品質へ仕上げるため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/.github/workflows/verify.yml, apps/XIVSubmarinesRewrite/README.md, apps/XIVSubmarinesRewrite/tools/DalamudRestore/restore.sh -->
<!-- STATUS: ✅ COMPLETED (Phase9-A/B/C done) -->

# Phase9 リリース実行計画 (2025-10-02)

## フェーズ構成
- Phase8-D CI ハードニング仕上げ
- Phase9-A リリースノート & ドキュメント整備
- Phase9-B 配布パッケージ検証と QA
- Phase9-C 公開準備とフォローアップ

---

## Phase8-D — CI ハードニング仕上げ

### 実装タスク
- [ ] `.github/workflows/ui-tests.yml` と `verify.yml` に `actions/cache` を追加し Dalamud パッケージと `node_modules` をキャッシュする。
- [ ] DalamudRestore のダウンロード結果をアーティファクトへ保存し、最悪時に手動で差し替えられるようにする。
- [ ] `tools/DalamudRestore/restore.sh` / `.ps1` にハッシュ検証を追加し、不正な ZIP を拒否する。
- [ ] CI 成果物から重複する Playwright レポートの保存先を整理し、Verify と UI Tests で共通構造にそろえる。

### テスト / 検証
- [ ] GitHub Actions の `UI Tests` を手動実行し、キャッシュヒットで所要時間が短縮されることを確認。
- [ ] `verify.yml` を `workflow_dispatch` で実行し、ダウンロードアーティファクトが取得できることを確認。
- [ ] ローカルで `npm test` と `dotnet test` を再実行し、ハッシュ検証追加で副作用がないことを確認。

---

## Phase9-A — リリースノート & ドキュメント整備

### 実装タスク
- [ ] `CHANGELOG.md` に Phase7/Phase8 の主要変更とテスト結果を追記し、v1.0.0 の草案を作成。
- [ ] `README.md` のセットアップ手順を更新し、DalamudRestore と lockfile の利用方法を明示。
- [ ] `docs/notifications/` 配下に ForceNotify 手動ログと DEV タブ運用の手順書を追加。
- [ ] `plans` フォルダを整理し、完了済み計画へ完了印を追記し履歴を残す。

### テスト / 検証
- [ ] `markdownlint` 相当のスタイル検査をローカルで実行 (必要なら `npm run lint:docs` を追加)。
- [ ] ドキュメント更新後に `README.md` のリンクチェックを `npm run docs:check` (新設) で自動化。

---

## Phase9-B — 配布パッケージ検証と QA

### 実装タスク
- [ ] `verify.yml` に `npm run build` と `dotnet build -c Release` の成果物収集を追加し、リリース候補を自動生成。
- [ ] `tools/RendererPreview` に配色サマリー出力 (JSON + PNG) のエクスポートを追加し CI でアーティファクト化。
- [ ] `tests/Playwright` に `@release` タグのエンドツーエンドシナリオを新設し、主要タブのスクリーンショット比較を実施。
- [ ] `tests/XIVSubmarinesRewrite.Tests` に MainWindowRenderer のタブ遷移を検証するユニットテストを追加。

### テスト / 検証
- [ ] `npm run test:ui -- --grep "@release"` を CI に組み込み、スクリーンショット差分がゼロであることを確認。
- [ ] `dotnet test --filter MainWindowRendererTests` をローカルと CI で実行し、レイアウト変更に回帰がないことを確かめる。
- [ ] Dalamud devPlugins へ生成成果物を配置し `/xsr`, `/xsr notify`, `/xsr dev` を手動確認。

---

## Phase9-C — 公開準備とフォローアップ

### 実装タスク
- [ ] GitHub Release 下書きを作成し、アセットに Verify 生成済み ZIP を添付。
- [ ] `repo.json` と Dalamud マニフェスト (`manifest.json`) のバージョンを v1.0.0 に更新。
- [ ] サポートチャンネル向けのリリースノート (Discord/Notion) をテンプレ化した Markdown で用意。
- [ ] `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-XX.md` を発行し、リリース実施内容と残課題を記録。

### テスト / 検証
- [ ] Dalamud にアップロードした beta 版で通知送信/DEV ログ/Overview の主要機能を手動確認し、不具合を記録。
- [ ] `gh release view` で公開前のアセット構成を確認し、不足がないかチェック。

---

## リファクタリング / 品質ガードライン
- [ ] 各フェーズで追加するコードは 250 行以内の新規ファイルに分割し、既存ファイルは 300 行を超えないよう部分クラス化を検討。
- [ ] DalamudRestore の処理にロギングを追加する際は軽量な `echo` / `Write-Host` のみを使用し、不要な依存を持ち込まない。
- [ ] Playwright テストのタグ運用 (@theme/@notification/@overview/@dev/@release) を維持し、CI コマンドで対象が明確になるようにする。
- [ ] ドキュメントを更新する際は必ず RELEVANT FILES ヘッダーコメントを整備し、参照関係を明示する。

---

## 参考リンク / トレース
- Phase7 〜 Phase8 の計画: `plans/phase7d_phase8_execution_plan_2025-10-01.md`
- セッションログ: `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-02.md`
- CI ワークフロー: `.github/workflows/ui-tests.yml`, `.github/workflows/verify.yml`

