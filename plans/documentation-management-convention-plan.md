# ドキュメント管理規約の再修正計画

## 計画概要

XIV Submarines Rewrite プロジェクトのドキュメント管理規約を現状に合わせて最適化し、明確な分類体系と配置ルールを確立する。

**作成日**: 2025-01-27  
**ステータス**: 計画完了（要件定義・設計完了）

## 現状の問題点

### 1. ルートディレクトリのドキュメントが散在
- AI開発支援: `AGENTS.md`, `AGENTS_SDD_INTEGRATION.md`, `AGENTS.md.backup`
- Claude用: `CLAUDE.md`, `CLAUDE_SDD_INTEGRATION.md`
- クイックスタート: `CURSOR_QUICKSTART.md`
- リリース関連: `GITHUB_RELEASE_INSTRUCTIONS.md`, `RELEASE_NOTES_v*.md`
- Phase完了レポート: `PHASE11_COMPLETION_SUMMARY.md`, `PHASE12_CC_SDD_INSTALLATION_PLAN.md`

### 2. 重複・矛盾の存在
- `AGENTS.md`と`AGENTS_SDD_INTEGRATION.md`の関係が不明確
- `CLAUDE.md`と`CLAUDE_SDD_INTEGRATION.md`の関係が不明確
- バックアップファイル（`AGENTS.md.backup`）が残存

### 3. README.mdとの不整合
- `docs/`ディレクトリが記載されているが存在しない
- `.serena/`ディレクトリが記載されているが存在しない

### 4. 未追跡ファイル
- `GITHUB_RELEASE_INSTRUCTIONS.md`
- `plans/specs/requirements/discord-notification-compact-fix-*.md`（3ファイル）

## 解決策

### 新しいディレクトリ構造

```
XIVSubmarinesRewrite/
├── [ユーザー向け] - releaseブランチに含める
│   ├── README.md
│   ├── CHANGELOG.md
│   └── RELEASE_NOTES_v*.md
│
├── docs/                            # [開発者向け] - developブランチのみ
│   ├── ai-development/              # AI開発支援ドキュメント
│   │   ├── AGENTS.md                # 統合版
│   │   ├── CLAUDE.md                # 統合版
│   │   └── QUICKSTART.md
│   └── release/                     # リリース作業ドキュメント
│       ├── github-release.md
│       └── release-checklist.md    # 新規作成
│
└── plans/                           # [開発計画] - developブランチのみ
    ├── phase*.md
    ├── *completion*.md
    └── specs/                       # cc-sdd仕様書（既存構造維持）
```

### 実施内容

1. **ディレクトリ作成**
   - `docs/ai-development/`の作成
   - `docs/release/`の作成

2. **ドキュメント統合・移動**
   - `AGENTS.md` + `AGENTS_SDD_INTEGRATION.md` → `docs/ai-development/AGENTS.md`（統合版）
   - `CLAUDE.md` + `CLAUDE_SDD_INTEGRATION.md` → `docs/ai-development/CLAUDE.md`（統合版）
   - `CURSOR_QUICKSTART.md` → `docs/ai-development/QUICKSTART.md`
   - `GITHUB_RELEASE_INSTRUCTIONS.md` → `docs/release/github-release.md`

3. **不要ファイルの削除**
   - `AGENTS.md.backup` → 削除
   - 統合後の元ファイル → 削除（git履歴で参照可能）

4. **既存ファイルの更新**
   - `README.md`: 実際の構造に合わせて更新
   - `plans/specs/steering/structure.md`: 「ドキュメント構成」セクションを更新

5. **新規ドキュメント作成**
   - `docs/release/release-checklist.md`: リリースチェックリスト

6. **未追跡ファイルの整理**
   - `discord-notification-compact-fix-*.md`: 必要に応じて追跡または削除

## 実施ステップ

### Phase 1: 準備（影響なし）
- [ ] `docs/`ディレクトリの作成
- [ ] 統合版ドキュメントの作成（新規ファイルとして）

### Phase 2: 移動・統合（git履歴保持）
- [ ] 統合版ドキュメントを`docs/ai-development/`に配置
- [ ] リリース作業ドキュメントを`docs/release/`に移動
- [ ] 元ファイルを削除（git履歴で参照可能）

### Phase 3: 更新（既存ファイルの修正）
- [ ] `README.md`の更新
- [ ] `plans/specs/steering/structure.md`の更新

### Phase 4: クリーンアップ
- [ ] バックアップファイルの削除
- [ ] 未追跡ファイルの整理

## ドキュメント分類ルール

| 分類 | 配置場所 | develop | release | 例 |
|------|---------|---------|---------|-----|
| ユーザー向け | ルート | ✅ | ✅ | README.md, CHANGELOG.md |
| AI開発支援 | docs/ai-development/ | ✅ | ❌ | AGENTS.md, CLAUDE.md |
| リリース作業 | docs/release/ | ✅ | ❌ | github-release.md |
| Phase計画 | plans/ | ✅ | ❌ | phase11_*.md |
| cc-sdd仕様書 | plans/specs/ | ✅ | ❌ | requirements/*.md |

## 関連ドキュメント

- **要件定義**: `plans/specs/requirements/documentation-management-convention.md`
- **設計書**: `plans/specs/design/documentation-management-convention.md`
- **プロジェクト構造**: `plans/specs/steering/structure.md`

## 次のステップ

1. 計画のレビュー・承認
2. 実装の実施（`/kiro:spec-impl documentation-management-convention`）
3. 検証・確認
4. `develop`ブランチへのマージ
5. `release`ブランチへの反映確認

---

**注意**: この計画は cc-sdd の仕様駆動開発プロセスに従って作成されています。実装前に要件定義と設計書を確認してください。

