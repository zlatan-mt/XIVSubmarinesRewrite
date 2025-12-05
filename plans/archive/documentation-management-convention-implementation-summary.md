# ドキュメント管理規約の再修正 - 実装完了サマリー

## 実装日
2025-01-27

## 実施内容

### Phase 1: 準備 ✅
- [x] `docs/ai-development/` ディレクトリの作成
- [x] `docs/release/` ディレクトリの作成
- [x] 統合版ドキュメントの作成

### Phase 2: 移動・統合 ✅
- [x] `docs/ai-development/AGENTS.md` - AGENTS.md + AGENTS_SDD_INTEGRATION.md を統合
- [x] `docs/ai-development/CLAUDE.md` - CLAUDE.md + CLAUDE_SDD_INTEGRATION.md を統合
- [x] `docs/ai-development/QUICKSTART.md` - CURSOR_QUICKSTART.md を移動・リネーム
- [x] `docs/release/github-release.md` - GITHUB_RELEASE_INSTRUCTIONS.md を移動・リネーム
- [x] `docs/release/release-checklist.md` - 新規作成
- [x] 元ファイルの削除（git履歴で参照可能）

### Phase 3: 更新 ✅
- [x] `README.md` の更新（実際の構造に合わせて修正）
- [x] `plans/specs/steering/structure.md` の「ドキュメント構成」セクションを更新

### Phase 4: クリーンアップ ✅
- [x] `AGENTS.md.backup` の削除
- [x] 統合後の元ファイルの削除

## 新しいディレクトリ構造

```
XIVSubmarinesRewrite/
├── [ユーザー向け] - releaseブランチに含める
│   ├── README.md
│   ├── CHANGELOG.md
│   └── RELEASE_NOTES_v*.md
│
├── docs/                            # [開発者向け] - developブランチのみ
│   ├── ai-development/              # AI開発支援ドキュメント
│   │   ├── AGENTS.md                # 統合版（Cursor/Codex用）
│   │   ├── CLAUDE.md                # 統合版（Claude Code用）
│   │   └── QUICKSTART.md            # クイックスタートガイド
│   └── release/                     # リリース作業ドキュメント
│       ├── github-release.md        # GitHub Release作成手順
│       └── release-checklist.md    # リリースチェックリスト
│
└── plans/                           # [開発計画] - developブランチのみ
    ├── phase*.md
    ├── *completion*.md
    └── specs/                       # cc-sdd仕様書（既存構造維持）
```

## 削除されたファイル

以下のファイルは削除されましたが、git履歴で参照可能です：

- `AGENTS.md` → `docs/ai-development/AGENTS.md`（統合版）に統合
- `AGENTS_SDD_INTEGRATION.md` → `docs/ai-development/AGENTS.md`（統合版）に統合
- `AGENTS.md.backup` → 削除（バックアップファイル）
- `CLAUDE.md` → `docs/ai-development/CLAUDE.md`（統合版）に統合
- `CLAUDE_SDD_INTEGRATION.md` → `docs/ai-development/CLAUDE.md`（統合版）に統合
- `CURSOR_QUICKSTART.md` → `docs/ai-development/QUICKSTART.md` に移動・リネーム
- `GITHUB_RELEASE_INSTRUCTIONS.md` → `docs/release/github-release.md` に移動・リネーム

## 更新されたファイル

- `README.md`: 開発者向けドキュメントの配置場所を更新
- `plans/specs/steering/structure.md`: 「ドキュメント構成」セクションを更新

## ドキュメント分類ルール

| 分類 | 配置場所 | develop | release | 例 |
|------|---------|---------|---------|-----|
| ユーザー向け | ルート | ✅ | ✅ | README.md, CHANGELOG.md |
| AI開発支援 | docs/ai-development/ | ✅ | ❌ | AGENTS.md, CLAUDE.md |
| リリース作業 | docs/release/ | ✅ | ❌ | github-release.md |
| Phase計画 | plans/ | ✅ | ❌ | phase11_*.md |
| cc-sdd仕様書 | plans/specs/ | ✅ | ❌ | requirements/*.md |

## 次のステップ

1. **gitコミット**: 変更をコミット
   ```bash
   git add docs/ plans/specs/steering/structure.md README.md
   git commit -m "docs: reorganize documentation structure

   - Create docs/ai-development/ and docs/release/ directories
   - Consolidate AGENTS.md and AGENTS_SDD_INTEGRATION.md
   - Consolidate CLAUDE.md and CLAUDE_SDD_INTEGRATION.md
   - Move CURSOR_QUICKSTART.md to docs/ai-development/QUICKSTART.md
   - Move GITHUB_RELEASE_INSTRUCTIONS.md to docs/release/github-release.md
   - Create docs/release/release-checklist.md
   - Update README.md and steering/structure.md
   - Remove backup and duplicate files

   Related: documentation-management-convention"
   ```

2. **検証**: 
   - [ ] `develop`ブランチで全ドキュメントが存在することを確認
   - [ ] `release`ブランチで`docs/`と`plans/`が除外されることを確認
   - [ ] リンクが正しく機能することを確認

3. **未追跡ファイルの整理**（オプション）:
   - `plans/specs/requirements/discord-notification-compact-fix-*.md` のステータスを確認
   - 必要に応じてgitに追加または削除

## 関連ドキュメント

- **要件定義**: `plans/specs/requirements/documentation-management-convention.md`
- **設計書**: `plans/specs/design/documentation-management-convention.md`
- **計画サマリー**: `plans/documentation-management-convention-plan.md`

---

**実装完了日**: 2025-01-27  
**ステータス**: ✅ 完了

