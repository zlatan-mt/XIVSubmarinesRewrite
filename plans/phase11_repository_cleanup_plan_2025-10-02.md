<!-- apps/XIVSubmarinesRewrite/plans/phase11_repository_cleanup_plan_2025-10-02.md -->
<!-- 公開リポジトリから開発者向けディレクトリを整理する計画です -->
<!-- ユーザー向けにシンプルな構成を提供し、リリース管理を容易にするため存在します -->
<!-- RELEVANT FILES: apps/XIVSubmarinesRewrite/.gitattributes, apps/XIVSubmarinesRewrite/.github/workflows/verify.yml, apps/XIVSubmarinesRewrite/README.md -->

# Phase11 リポジトリ公開向けクリーンアップ計画 (2025-10-02)

## ゴール
- GitHub 上で利用者に不要なディレクトリ (.serena/ docs/ plans/ など) を見せないよう整理する。
- 開発者向け資料は維持しつつ、リリース ZIP と公開ブランチを軽量化する。
- リリース後も CI とドキュメント運用が破綻しない仕組みを整える。

---

## Phase11-A — ディレクトリ棚卸しと運用方針決定

### 実装タスク
- [ ] `.serena/`, `docs/`, `plans/`, `sessions/`, `tools/DalamudRestore/` などの用途を分類し、ユーザー公開が不要なものをリストアップ。
- [ ] 開発者向け資料を移行する場所を決定 (例: 別リポジトリ `XIVSubmarinesRewrite-internal`, GitHub Wiki, Notion 等)。
- [ ] 必須ファイルの残留を防ぐため、各ディレクトリ内の README で用途を再確認し、移行可能か判断。
- [ ] 変更後のリポジトリ構造を図示した `docs/repo-structure.md` (移行先) をドラフト作成。

### テスト / 検証
- [ ] 棚卸しリストと移行先をメンバーにレビューしてもらい、抜け漏れがないことを確認。

---

## Phase11-B — 公開ブランチと開発ブランチの分離

### 実装タスク
- [ ] 現行 `master` (または `main`) をリリース専用ブランチ (`release`) に再編成し、不要ディレクトリを削除。
- [ ] 開発用ブランチ (`develop`/`dev`) を継続運用し、ドキュメント・計画ファイルはそちらに保持。
- [ ] GitHub Actions のトリガーを調整し、`release` ブランチに対する Verify 実行が通るようワークフロー条件を更新。
- [ ] PR テンプレートや README の開発フロー節を更新し、新しいブランチモデルを明記。

### テスト / 検証
- [ ] `release` ブランチで `dotnet build`, `npm test` を実行し、不要ディレクトリ削除後も問題ないことを確認。
- [ ] GitHub Actions のテスト実行 (`verify.yml`) が `release` ブランチでも成功することを確認。

---

## Phase11-C — リリースアーカイブと公開ページの整理

### 実装タスク
- [ ] `.gitattributes` に `export-ignore` を設定し、GitHub Release の Source Code (zip/tar) から不要フォルダを除外。
- [ ] `verify.yml` のリリース候補生成ステップを修正し、`release` ブランチのクリーンな構成のみを ZIP 化。
- [ ] README の「開発者向け資料」リンクを移行先に差し替え、公開リポジトリから開発資料へ誘導。
- [ ] GitHub Release v1.0.0 の本文を再編集し、移行後の構成説明を追加 (文字化け対策を含む)。

### テスト / 検証
- [ ] `.gitattributes` 適用後、`git archive HEAD` で出力した ZIP に不要フォルダが含まれていないことを確認。
- [ ] `verify.yml` が生成する `XIVSubmarinesRewrite.zip` に開発ディレクトリが入っていないことを確認。

---

## Phase11-D — ドキュメント & 自動化の最終調整

### 実装タスク
- [ ] 開発向け資料を移設したリポジトリ/ Wiki に README を整備し、引き継ぎ方法を明記。
- [ ] CI の失敗時に開発資料側へリンクするよう Issue テンプレートやワークフロー通知を調整。
- [ ] セッションノートと計画書の管理方法を再定義し、`release` ブランチでは履歴が見えない旨を記載。
- [ ] `sessions/SESSION_XIVSubmarinesRewrite_notes_2025-10-XX.md` に Phase11 の作業記録を残す (開発ブランチ側)。

### テスト / 検証
- [ ] リリース版 README を未経験者にレビューしてもらい、迷わずインストールできるか確認。
- [ ] `release` ブランチ → GitHub Release → ZIP の導線で、不要フォルダが露出しないことを最終チェック。

---

## リスクとフォールバック
- `release` ブランチ切り離し後に両方のブランチで差分管理が難しくなるため、`develop -> release` のマージ運用手順を決めておく。
- 開発資料を別リポジトリに移す場合はアクセス権やバックアップポリシーを確認。
- `.gitattributes` 設定を誤ると必要ファイルまで除外される可能性があるため、アーカイブ検証を必須とする。

---

## 品質ガードライン
- どのブランチにどのファイルが存在するか README / CONTRIBUTING に明示。
- CI は `develop` と `release` 両方で成功することを定期確認。
- リリース生成 ZIP には `src/`, `manifest.json`, `icon.png` など必要最低限のファイルのみを含める。

---

## 参照
- 現在のリポジトリ構造: `tree -L 2` 出力 (develop ブランチ)
- リリース v1.0.0 ページ: https://github.com/mona-ty/XIVSubmarinesRewrite/releases/tag/v1.0.0
- `.gitattributes` export-ignore の参考: https://git-scm.com/docs/gitattributes#_export_ignore

