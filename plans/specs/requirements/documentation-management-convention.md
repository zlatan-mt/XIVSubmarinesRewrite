# 要件定義: ドキュメント管理規約の再修正

## 概要

XIV Submarines Rewrite プロジェクトのドキュメント管理規約を現状に合わせて最適化し、明確な分類・配置・管理ルールを確立する。

## 背景

### 現状の問題点

1. **ルートディレクトリのドキュメントが散在**
   - AI開発支援ドキュメント: `AGENTS.md`, `AGENTS_SDD_INTEGRATION.md`, `AGENTS.md.backup`
   - Claude用ドキュメント: `CLAUDE.md`, `CLAUDE_SDD_INTEGRATION.md`
   - クイックスタート: `CURSOR_QUICKSTART.md`
   - リリース関連: `GITHUB_RELEASE_INSTRUCTIONS.md`, `RELEASE_NOTES_v*.md`（複数バージョン）
   - Phase完了レポート: `PHASE11_COMPLETION_SUMMARY.md`, `PHASE12_CC_SDD_INSTALLATION_PLAN.md`
   - その他: `README.md`, `CHANGELOG.md`

2. **重複・矛盾の存在**
   - `AGENTS.md`と`AGENTS_SDD_INTEGRATION.md`の関係が不明確
   - `CLAUDE.md`と`CLAUDE_SDD_INTEGRATION.md`の関係が不明確
   - バックアップファイル（`AGENTS.md.backup`）が残存

3. **README.mdとの不整合**
   - `docs/`ディレクトリが記載されているが存在しない
   - `.serena/`ディレクトリが記載されているが存在しない

4. **ブランチ戦略との整合性不足**
   - `develop`ブランチ: 全ドキュメントを含む
   - `release`ブランチ: `plans/`を除外
   - しかし、ルートの開発ドキュメントの扱いが不明確

5. **未追跡ファイル**
   - `GITHUB_RELEASE_INSTRUCTIONS.md`（git statusで未追跡）
   - `plans/specs/requirements/discord-notification-compact-fix-*.md`（3ファイル、未追跡）

6. **ドキュメント分類の不明確さ**
   - ユーザー向け vs 開発者向けの境界が不明確
   - 一時的なドキュメント（Phase完了レポート等）の扱いが不明確
   - リリース関連ドキュメントの配置が不明確

## 目的

1. **明確な分類体系の確立**
   - ユーザー向け / 開発者向け / リリース向けの明確な分類
   - 一時的ドキュメント（Phase完了レポート等）の適切な配置

2. **重複の解消**
   - 重複ドキュメントの統合または明確な役割分担
   - バックアップファイルの整理

3. **ブランチ戦略との整合**
   - `develop`ブランチ: 全開発ドキュメントを含む
   - `release`ブランチ: ユーザー向けドキュメントのみ

4. **保守性の向上**
   - ドキュメントの配置ルールを明確化
   - 新規ドキュメント作成時の判断基準を提供

## 機能要件

### FR-1: ドキュメント分類体系

**ユーザー向けドキュメント**（`release`ブランチに含める）
- `README.md`: プロジェクト概要、インストール手順、使用方法
- `CHANGELOG.md`: バージョン履歴（Keep a Changelog形式）
- `RELEASE_NOTES_v*.md`: リリースノート（最新版のみ）

**開発者向けドキュメント**（`develop`ブランチのみ）
- AI開発支援: `docs/ai-development/`配下に統合
- Phase計画・完了レポート: `plans/`配下
- cc-sdd仕様書: `plans/specs/`配下（既存）

**リリース作業ドキュメント**（`develop`ブランチのみ）
- リリース準備手順: `docs/release/`配下
- GitHub Release手順: `docs/release/`配下

### FR-2: ディレクトリ構造の再編

```
XIVSubmarinesRewrite/
├── README.md                    # ユーザー向け（release含む）
├── CHANGELOG.md                 # ユーザー向け（release含む）
├── RELEASE_NOTES_v*.md          # ユーザー向け（最新版のみrelease含む）
├── docs/                        # 開発者向け（developのみ）
│   ├── ai-development/          # AI開発支援ドキュメント
│   │   ├── AGENTS.md            # Cursor/Codex統合ガイド（統合版）
│   │   ├── CLAUDE.md            # Claude Code統合ガイド（統合版）
│   │   └── QUICKSTART.md        # クイックスタートガイド
│   └── release/                # リリース作業ドキュメント
│       ├── github-release.md    # GitHub Release作成手順
│       └── release-checklist.md # リリースチェックリスト
├── plans/                       # 開発計画（developのみ）
│   ├── phase*.md               # Phase計画
│   ├── *completion*.md         # Phase完了レポート
│   └── specs/                   # cc-sdd仕様書
└── release-package/            # リリースパッケージ（developのみ）
```

### FR-3: 重複ドキュメントの統合

- `AGENTS.md` + `AGENTS_SDD_INTEGRATION.md` → `docs/ai-development/AGENTS.md`（統合版）
- `CLAUDE.md` + `CLAUDE_SDD_INTEGRATION.md` → `docs/ai-development/CLAUDE.md`（統合版）
- `CURSOR_QUICKSTART.md` → `docs/ai-development/QUICKSTART.md`（リネーム）

### FR-4: バックアップファイルの整理

- `AGENTS.md.backup` → 削除（必要に応じてgit履歴で参照）

### FR-5: README.mdの更新

- `docs/`ディレクトリの記載を削除または修正
- `.serena/`ディレクトリの記載を削除
- 実際のディレクトリ構造に合わせて更新

### FR-6: 未追跡ファイルの整理

- `GITHUB_RELEASE_INSTRUCTIONS.md` → `docs/release/github-release.md`に移動・追跡
- `plans/specs/requirements/discord-notification-compact-fix-*.md` → 必要に応じて追跡または削除

### FR-7: ドキュメント管理規約の文書化

- `plans/specs/steering/structure.md`の「ドキュメント構成」セクションを更新
- 新規ドキュメント作成時の判断基準を明確化

## 非機能要件

### NFR-1: 後方互換性

- 既存のリンクや参照が壊れないよう、段階的な移行を検討
- または、適切なリダイレクト/参照を提供

### NFR-2: 保守性

- ドキュメントの配置ルールが明確で、新規作成時に迷わない
- 分類が直感的で、検索しやすい

### NFR-3: ブランチ戦略との整合

- `develop`ブランチ: 全開発ドキュメントを含む
- `release`ブランチ: ユーザー向けドキュメントのみ（`docs/`, `plans/`を除外）

## 受け入れ基準

1. ✅ ルートディレクトリのドキュメントが整理され、分類が明確
2. ✅ 重複ドキュメントが統合または削除され、役割が明確
3. ✅ `docs/`ディレクトリが作成され、開発者向けドキュメントが配置
4. ✅ `README.md`が実際の構造と一致
5. ✅ バックアップファイルが削除
6. ✅ 未追跡ファイルが適切に配置・追跡
7. ✅ `plans/specs/steering/structure.md`の「ドキュメント構成」が更新
8. ✅ 新規ドキュメント作成時の判断基準が文書化

## 制約

- 既存のgit履歴は保持（削除ファイルは履歴で参照可能）
- cc-sddの仕様書構造（`plans/specs/`）は変更しない
- `release-package/`の構造は変更しない（リリースパッケージ用）

## 関連ドキュメント

- `plans/specs/steering/structure.md`: プロジェクト構造定義
- `plans/specs/steering/principles.md`: 開発原則
- `plans/specs/settings/rules/steering-principles.md`: Steering管理原則

