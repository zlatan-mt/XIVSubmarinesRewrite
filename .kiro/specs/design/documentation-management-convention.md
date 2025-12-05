# 設計書: ドキュメント管理規約の再修正

## 概要

ドキュメント管理規約の再修正を実施し、明確な分類体系と配置ルールを確立する。

**目的**: ドキュメントの散在を解消し、ユーザー向け/開発者向け/リリース向けの明確な分類を実現する。

**影響範囲**: ルートディレクトリ、新規`docs/`ディレクトリ、`plans/`配下のドキュメント

## アーキテクチャ

### ディレクトリ構造設計

```
XIVSubmarinesRewrite/
├── [ユーザー向け] - releaseブランチに含める
│   ├── README.md                    # プロジェクト概要、インストール、使用方法
│   ├── CHANGELOG.md                 # バージョン履歴（Keep a Changelog形式）
│   └── RELEASE_NOTES_v*.md          # リリースノート（最新版のみ）
│
├── docs/                            # [開発者向け] - developブランチのみ
│   ├── ai-development/              # AI開発支援ドキュメント
│   │   ├── AGENTS.md                # Cursor/Codex統合ガイド（統合版）
│   │   ├── CLAUDE.md                # Claude Code統合ガイド（統合版）
│   │   └── QUICKSTART.md            # クイックスタートガイド
│   └── release/                     # リリース作業ドキュメント
│       ├── github-release.md        # GitHub Release作成手順
│       └── release-checklist.md    # リリースチェックリスト（新規作成）
│
└── plans/                           # [開発計画] - developブランチのみ
    ├── phase*.md                    # Phase計画
    ├── *completion*.md              # Phase完了レポート
    ├── *installation*.md            # インストール計画
    ├── v*.md                        # リリース準備サマリー
    └── specs/                       # cc-sdd仕様書（既存構造維持）
        ├── requirements/
        ├── design/
        ├── tasks/
        ├── steering/
        └── settings/
```

### ドキュメント分類マトリクス

| 分類 | 配置場所 | develop | release | 例 |
|------|---------|---------|---------|-----|
| ユーザー向け | ルート | ✅ | ✅ | README.md, CHANGELOG.md |
| AI開発支援 | docs/ai-development/ | ✅ | ❌ | AGENTS.md, CLAUDE.md |
| リリース作業 | docs/release/ | ✅ | ❌ | github-release.md |
| Phase計画 | plans/ | ✅ | ❌ | phase11_*.md |
| cc-sdd仕様書 | plans/specs/ | ✅ | ❌ | requirements/*.md |

## 実装設計

### ステップ1: ディレクトリ作成

```bash
mkdir -p docs/ai-development
mkdir -p docs/release
```

### ステップ2: ドキュメント統合・移動

#### 2.1 AI開発支援ドキュメントの統合

**AGENTS.md（統合版）の作成**
- `AGENTS.md`（基本版）と`AGENTS_SDD_INTEGRATION.md`（統合版）を統合
- 統合方針:
  - 基本情報は`AGENTS.md`から
  - cc-sdd統合情報は`AGENTS_SDD_INTEGRATION.md`から
  - 重複部分は最新版を優先
  - 構成: 基本情報 → cc-sdd統合 → ワークフロー → コマンドリファレンス

**CLAUDE.md（統合版）の作成**
- `CLAUDE.md`（基本版）と`CLAUDE_SDD_INTEGRATION.md`（統合版）を統合
- 統合方針: AGENTS.mdと同様

**QUICKSTART.mdの作成**
- `CURSOR_QUICKSTART.md`を`docs/ai-development/QUICKSTART.md`に移動・リネーム
- 内容はそのまま（Cursor IDE固有の内容を含む）

#### 2.2 リリース作業ドキュメントの移動

**github-release.mdの作成**
- `GITHUB_RELEASE_INSTRUCTIONS.md`を`docs/release/github-release.md`に移動・リネーム
- 内容はそのまま

**release-checklist.mdの作成**（新規）
- リリース作業のチェックリストを新規作成
- 内容:
  - ビルド確認
  - テスト実行
  - バージョン番号更新
  - タグ作成
  - GitHub Release作成
  - パッケージアップロード

#### 2.3 Phase完了レポートの整理

- `PHASE11_COMPLETION_SUMMARY.md` → `plans/`配下に移動（既に配置済み）
- `PHASE12_CC_SDD_INSTALLATION_PLAN.md` → `plans/`配下に移動（既に配置済み）
- 命名規則: `phase<N>_<description>.md` または `phase<N>_completion_summary.md`

### ステップ3: 不要ファイルの削除

- `AGENTS.md.backup` → 削除（git履歴で参照可能）
- 統合後の元ファイル（`AGENTS_SDD_INTEGRATION.md`, `CLAUDE_SDD_INTEGRATION.md`, `CURSOR_QUICKSTART.md`）→ 削除（git履歴で参照可能）

### ステップ4: README.mdの更新

**削除する記載**
- `docs/`ディレクトリの記載（存在しないため）
- `.serena/`ディレクトリの記載（存在しないため）

**追加する記載**
- 開発者向けドキュメントの配置場所（`docs/ai-development/`）
- リリース作業ドキュメントの配置場所（`docs/release/`）

**更新例**
```markdown
### 開発参加

開発に参加する場合は `develop` ブランチをクローンしてください：

```bash
git clone -b develop https://github.com/zlatan-mt/XIVSubmarinesRewrite.git
```

開発資料、設計ドキュメント、計画書は `develop` ブランチの以下のディレクトリに含まれています：
- `docs/ai-development/` - AI開発支援ドキュメント（Cursor/Codex/Claude Code用）
- `docs/release/` - リリース作業手順
- `plans/` - フェーズ別開発計画とcc-sdd仕様書
```

### ステップ5: steering/structure.mdの更新

**「ドキュメント構成」セクションの更新**

```markdown
## ドキュメント構成

### ユーザー向け（releaseブランチに含める）
- `README.md`: プロジェクト概要、インストール手順、使用方法
- `CHANGELOG.md`: バージョン履歴（Keep a Changelog形式）
- `RELEASE_NOTES_v*.md`: リリースノート（最新版のみ）

### 開発者向け（developブランチのみ）
- `docs/ai-development/`: AI開発支援ドキュメント
  - `AGENTS.md`: Cursor/Codex統合ガイド
  - `CLAUDE.md`: Claude Code統合ガイド
  - `QUICKSTART.md`: クイックスタートガイド
- `docs/release/`: リリース作業ドキュメント
  - `github-release.md`: GitHub Release作成手順
  - `release-checklist.md`: リリースチェックリスト
- `plans/`: Phase計画とcc-sdd仕様書
  - `phase*.md`: Phase計画
  - `*completion*.md`: Phase完了レポート
  - `specs/`: cc-sdd仕様書（requirements, design, tasks, steering）

### ドキュメント分類ルール
- **ユーザー向け**: ルートディレクトリに配置、releaseブランチに含める
- **開発者向け**: `docs/`配下に配置、developブランチのみ
- **開発計画**: `plans/`配下に配置、developブランチのみ
- **一時的ドキュメント**: Phase完了レポート等は`plans/`配下に配置
```

### ステップ6: 未追跡ファイルの整理

**discord-notification-compact-fix-*.mdの扱い**
- `plans/specs/requirements/discord-notification-compact-fix.md`（基本）
- `plans/specs/requirements/discord-notification-compact-fix-review.md`（レビュー）
- `plans/specs/requirements/discord-notification-compact-fix-implementation.md`（実装）

**判断基準**
- アクティブな仕様書: gitに追加
- 完了済み/不要: 削除または`plans/archive/`に移動

## 移行手順

### Phase 1: 準備（影響なし）

1. `docs/`ディレクトリの作成
2. 統合版ドキュメントの作成（新規ファイルとして）

### Phase 2: 移動・統合（git履歴保持）

1. 統合版ドキュメントを`docs/ai-development/`に配置
2. リリース作業ドキュメントを`docs/release/`に移動
3. 元ファイルを削除（git履歴で参照可能）

### Phase 3: 更新（既存ファイルの修正）

1. `README.md`の更新
2. `plans/specs/steering/structure.md`の更新

### Phase 4: クリーンアップ

1. バックアップファイルの削除
2. 未追跡ファイルの整理

## リスクと対策

### リスク1: 既存リンクの破損

**対策**
- 段階的な移行（新規ファイル作成 → 旧ファイル削除）
- git履歴で旧ファイルを参照可能
- 必要に応じてシンボリックリンクまたはリダイレクトを検討

### リスク2: ドキュメント内容の統合ミス

**対策**
- 統合前に両ファイルの内容を確認
- 統合版のレビューを実施
- 元ファイルはgit履歴で参照可能

### リスク3: ブランチ戦略との不整合

**対策**
- `docs/`ディレクトリは`develop`ブランチのみに存在
- `release`ブランチへのマージ時に`docs/`を除外することを確認

## 検証方法

1. ✅ `docs/`ディレクトリが作成されている
2. ✅ 統合版ドキュメントが正しく配置されている
3. ✅ 元ファイルが削除されている（git履歴で確認可能）
4. ✅ `README.md`が実際の構造と一致している
5. ✅ `plans/specs/steering/structure.md`が更新されている
6. ✅ 未追跡ファイルが適切に処理されている
7. ✅ `develop`ブランチで全ドキュメントが存在
8. ✅ `release`ブランチで`docs/`と`plans/`が除外されている

## 関連ドキュメント

- 要件定義: `plans/specs/requirements/documentation-management-convention.md`
- プロジェクト構造: `plans/specs/steering/structure.md`
- 開発原則: `plans/specs/steering/principles.md`

