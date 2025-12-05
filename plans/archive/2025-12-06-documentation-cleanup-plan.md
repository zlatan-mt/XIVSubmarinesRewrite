# ドキュメント整理・構造化計画 (2025-12 Final Revised v3)

## 1. 目的
プロジェクトのディレクトリ構造を「ドキュメント管理規約 v3.4」に準拠させ、情報の散乱と不整合を解消する。

## 2. 現状とのギャップ
- **仕様書の場所**: 規約は `.kiro/specs/` だが、実態は `plans/specs/` にある。
- **ルートのノイズ**: バックアップ用テキストファイルが存在。
- **アーカイブ未実施**: 完了した計画書が `plans/` 直下に放置されている。

## 3. 実行タスクリスト

### Phase 1: 規約の適用準備 (完了)
- [x] `docs/conventions/` 作成と規約 v3.4 の配置。

### Phase 2: 仕様書の移行 (`plans/specs` -> `.kiro/specs`)
- [ ] `ls .kiro/` で既存の内容を確認し、`specs/` が既に存在しないことを確認する。
- [ ] `.kiro/specs/` ディレクトリを作成。
- [ ] `plans/specs/` 配下のディレクトリ（`design`, `requirements` 等）を `.kiro/specs/` へ移動。
- [ ] `plans/specs/` ディレクトリを削除。

### Phase 3: ルートディレクトリの清掃
- [ ] `AGENTS.md` の状態確認（ルートに残っている場合は `docs/ai-development/` へ移動または削除）。
  - ※`docs/ai-development/` は存在を確認済み。
- [ ] `.gitignore` に以下のパターンを追加:
    - `*.backup.txt`
    - `*-backup.txt`
    - `pre-sdd-state.txt`
- [ ] ルートにある `branch-backup.txt`, `commit-graph-backup.txt`, `pre-sdd-state.txt` を削除（Git管理外へ）。

### Phase 4: 計画書のアーカイブと整理
- [ ] `plans/` 直下のファイル一覧を確認し、移動漏れがないかチェック。
  - 特に `documentation-management-convention-*.md` のワイルドカード対象を目視確認。
- [ ] 旧ファイル `plans/documentation-cleanup-plan-2025-12.md` が存在しないことを確認。
- [ ] `plans/archive/` ディレクトリを作成。
- [ ] 以下の完了済みファイルを `plans/archive/` へ移動:
    - `PHASE11_COMPLETION_SUMMARY.md`
    - `PHASE12_CC_SDD_INSTALLATION_PLAN.md`
    - `discord.plan.md`
    - `fix_plan_submarine_status_issue.md`
    - `phase11_repository_cleanup_plan_2025-10-02.md`
    - `phase11a_directory_analysis_2025-10-02.md`
    - `v1.1.6_release_preparation_summary.md`
    - `documentation-management-convention-*.md` (一括移動前にファイル確認)
- [ ] 本計画書 (`2025-12-06-documentation-cleanup-plan.md`) は、全タスク完了後に `plans/archive/` へ移動する。

## 4. 完了定義 (Checklist)

以下の順序で確認を行い、全て満たすこと。

1. **`.kiro` ディレクトリ**:
   - `specs/` が存在し、旧 `plans/specs/` の内容が正しく移行されている。

2. **`plans` ディレクトリ**:
   - `archive/` が存在し、指定された過去ログが全て格納されている。
   - 直下には本計画書（移動前）のみ、または進行中の計画のみが存在する。
   - 旧ファイル名の計画書が存在しない。

3. **ルートディレクトリ**:
   - バックアップ用 `*.txt` (規約3.1で禁止されたパターン) が存在しない。
   - `AGENTS.md` が存在しない。

4. **Git状態**:
   - 上記を確認し、本計画書をアーカイブへ移動した後、`git status` が clean な状態であること（untracked files がなく、`ignored` ファイルは `.gitignore` に含まれていること）。
