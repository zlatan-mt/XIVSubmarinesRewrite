# ドキュメント管理規約

**バージョン**: 3.4 (Final Refined based on critical review)
**発効日**: 2025-12-06
**適用範囲**: XIVSubmarinesRewrite プロジェクト全域

## 1. 目的
本規約は、プロジェクト内のドキュメント、計画書、および関連ファイルの配置と管理運用を定義し、情報の発見性と保守性を維持することを目的とする。

## 2. ディレクトリ構造とカテゴリ

ドキュメントは以下のカテゴリに分類し、指定のディレクトリで管理する。

| カテゴリ | 内容 | 配置ディレクトリ | 備考 |
| :--- | :--- | :--- | :--- |
| **ユーザー向け** | 利用者への説明、変更履歴 | `(Root)/` | README, CHANGELOG など |
| **プロジェクト規約** | 全体ルール、運用ガイド | `docs/conventions/` | 本書など |
| **AI開発コンテキスト** | AI-DLCの定義・仕様 | `.kiro/steering/` (知識)<br>`.kiro/specs/` (仕様) | `.kiro/` はAI-DLCツールが管理する領域。 |
| **AI設定・補助** | ツール設定、プロンプト定義 | `docs/ai-development/` | 詳細は3.1節参照。 |
| **リリース関連** | 手順書、過去のリリースノート | `docs/release/` (手順)<br>`docs/releases/` (アーカイブ) | |
| **調査・分析** | バグ調査、ログ分析レポート | `docs/analysis/` | |
| **計画** | ロードマップ、大規模改修計画 | `plans/` | 進行中の計画のみ配置。 |
| **過去の計画** | 完了した計画書のアーカイブ | `plans/archive/` | |

## 3. 管理運用ルール

### 3.1 ルートディレクトリ (`/`)
プロジェクトの顔となるファイル、およびツール仕様上必須な設定ファイルのみを配置する。
- **必須**: `README.md`, `CHANGELOG.md`, `LICENSE` (または `LICENSE.txt`)
- **ツール設定(例外)**: `GEMINI.md`, `CLAUDE.md` 等、ツールがルートパスをハードコードして要求する場合のみ配置可。
- **禁止**: 一時的なメモ、バックアップファイル（例: `*-backup.txt`, `*.backup`, `pre-sdd-state.txt`）。
  - 正規のドキュメントとしての `.txt` (例: `LICENSE.txt`, `NOTICE.txt`) は禁止しない。

### 3.2 仕様書と計画 (`.kiro/specs/` & `plans/`)
- **仕様書**: 機能開発の詳細な仕様は **`.kiro/specs/`** 配下で管理する (AI-DLC準拠)。
- **計画書**: プロジェクト全体に関わる計画やロードマップは `plans/` で管理する。
  - 完了した計画は `plans/archive/` へ移動する。

### 3.3 リリースノート運用
- **最新情報**: ルートの `CHANGELOG.md` に集約する。
- **アーカイブ**: バージョンごとの詳細な `RELEASE_NOTES_vX.X.X.md` は **`docs/releases/`** に配置する。

### 3.4 命名規則
各カテゴリのファイル命名は以下に従う。

| カテゴリ | 規則 | 例 |
| :--- | :--- | :--- |
| **計画書** | `YYYY-MM-DD-{topic}-plan.md` | `2025-12-06-documentation-cleanup-plan.md` |
| **分析レポート** | `report_{issue_id}_{topic}.md` | `report_issue-42_crash-analysis.md` |
| **規約** | `{topic}.md` | `documentation-management.md` |

## 4. バックアップと一時ファイル
- 手動バックアップファイル（例: `branch-backup.txt`）は **Git管理対象外** とする。
- 必要に応じて `.gitignore` に除外設定を追加し、リポジトリを汚染しないようにする。

## 5. 改訂プロセス
本規約の改訂は、`docs/conventions/` 内のドキュメントを更新することで行う。
