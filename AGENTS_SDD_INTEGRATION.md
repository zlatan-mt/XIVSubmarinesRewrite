# XIV Submarines Rewrite - Spec-Driven Development 統合ガイド

## 概要

本プロジェクトは **cc-sdd** による仕様駆動開発（Spec-Driven Development）を採用しています。

Cursor IDE と Codex CLI で統一されたワークフローを実現します。

---

## 🎯 開発ワークフロー

### Phase 開発フロー（SDD統合版）

```
Phase企画
  ↓
1. 要件定義: /kiro:spec-requirements <phase-name>
  ↓
2. 設計: /kiro:spec-design <phase-name>
  ↓
3. タスク分解: /kiro:spec-tasks <phase-name>
  ↓
4. 実装: /kiro:spec-impl <phase-name> <task-ids>
  ↓
Phase完了レポート
```

---

## 📝 主要コマンド

### プロジェクトメモリ管理

```bash
# プロジェクト全体の文脈を学習・更新
/kiro:steering

# 内容: アーキテクチャ、技術スタック、開発原則、ブランチ戦略
```

### 仕様駆動開発コマンド

```bash
# 1. 新機能の初期化
/kiro:spec-init <機能概要>

# 2. 要件定義
/kiro:spec-requirements <spec-name>
# → plans/specs/requirements/<spec-name>.md

# 3. 設計
/kiro:spec-design <spec-name> [-y]
# → plans/specs/design/<spec-name>.md

# 4. タスク分解
/kiro:spec-tasks <spec-name> [-y]
# → plans/specs/tasks/<spec-name>.md

# 5. 実装
/kiro:spec-impl <spec-name> <task-ids>
# 例: /kiro:spec-impl notification-perf 1.1,1.2,1.3
```

### 検証コマンド

```bash
# 要件と既存実装のギャップ分析
/kiro:validate-gap <spec-name>

# 設計の整合性検証
/kiro:validate-design <spec-name>
```

---

## 🏗️ プロジェクト固有ルール

### 技術スタック
- **プラットフォーム**: Dalamud API Level 13
- **言語**: .NET 9.0-windows, C#
- **UI**: ImGui (Dalamud.Bindings.ImGui)
- **テスト**: xUnit (.NET), Playwright (E2E)

### アーキテクチャレイヤー
```
Presentation (UI)
  ↓
Application (Services, Commands, Queries)
  ↓
Domain (Models, Repositories)
  ↓
Infrastructure (DB, API, External Services)
```

### コーディング規約
- **命名**:
  - モジュール: スネークケース
  - クラス: PascalCase
  - 定数: SCREAMING_SNAKE_CASE
- **ファイルサイズ**: 300行以下を維持
- **型ヒント**: 徹底（警告ゼロ）
- **テストカバレッジ**: 90%以上

### ブランチ戦略
- `develop`: 開発用（全ファイル、specs含む）
- `release`: 公開用（クリーン構成、specs除外）
- `feature/*`: 機能ブランチ

---

## 📂 ディレクトリ構成

```
XIVSubmarinesRewrite/
├── src/                       # ソースコード
│   ├── Presentation/
│   ├── Application/
│   ├── Domain/
│   └── Infrastructure/
├── tests/                     # テスト
│   ├── XIVSubmarinesRewrite.Tests/
│   └── Playwright/
├── plans/
│   ├── phase01_*.md           # 既存Phase計画
│   └── specs/                 # cc-sdd仕様書（新規）
│       ├── requirements/
│       ├── design/
│       ├── tasks/
│       └── .kiro/
│           ├── settings/
│           └── steering/      # プロジェクトメモリ
├── .cursor/commands/kiro/     # Cursor用コマンド
├── .codex/prompts/            # Codex用プロンプト
└── AGENTS.md                  # 本ファイル
```

---

## 🎯 開発原則（cc-sdd統合）

### 仕様駆動開発の原則
1. **要件の明確化**: 実装前に必ず要件文書を作成
2. **設計レビュー**: 設計文書で事前レビュー実施
3. **タスク分解**: 実装可能な粒度に分解
4. **トレーサビリティ**: 要件→設計→タスク→実装の追跡可能性

### 品質ゲート
- 各フェーズは人間の承認後に進行（`-y`で自動承認可）
- 設計変更は要件への影響を評価
- タスク完了後は必ずテスト実施

---

## 💡 使用例

### 例1: 新機能開発（Phase 13想定）

```bash
# Step 1: プロジェクト文脈の更新
/kiro:steering

# Step 2: 新機能の初期化
/kiro:spec-init Improve notification retry logic with exponential backoff

# Step 3: 要件定義
/kiro:spec-requirements notification-retry-enhancement

# Step 4: 設計（承認付き）
/kiro:spec-design notification-retry-enhancement
# → 設計内容を確認後、続行

# Step 5: タスク分解（自動承認）
/kiro:spec-tasks notification-retry-enhancement -y

# Step 6: 実装（タスク1.1, 1.2を実施）
/kiro:spec-impl notification-retry-enhancement 1.1,1.2
```

### 例2: 既存機能の拡張

```bash
# Step 1: 既存実装とのギャップ分析
/kiro:validate-gap discord-notification-batching

# Step 2: 要件定義
/kiro:spec-requirements discord-batching-v2

# Step 3: 設計の整合性検証
/kiro:validate-design discord-batching-v2

# Step 4以降: 通常フロー
```

---

## 🔄 Phase開発との統合

### 従来のPhase計画
- `plans/phase13_plan.md` - Phase全体の計画

### cc-sdd統合後
- `plans/phase13_plan.md` - Phase全体の計画（高レベル）
- `plans/specs/requirements/phase13-*.md` - 機能別要件
- `plans/specs/design/phase13-*.md` - 機能別設計
- `plans/specs/tasks/phase13-*.md` - 機能別タスク

---

## 📖 関連ドキュメント

- **CLAUDE.md**: Claude Code専用ガイド
- **README.md**: プロジェクト概要
- **CHANGELOG.md**: バージョン履歴
- **plans/specs/.kiro/steering/**: プロジェクトメモリ

---

## 🚀 開発開始チェックリスト

新しいPhase/機能開発を始める前に：

- [ ] `/kiro:steering` でプロジェクト文脈を更新
- [ ] 既存の `plans/` ドキュメントを確認
- [ ] ブランチを確認（developから機能ブランチ作成）
- [ ] `/kiro:spec-init` で機能概要を定義
- [ ] 要件→設計→タスクの順で文書化
- [ ] 各フェーズで品質ゲートを通過

---

**最終更新**: 2025-10-25  
**cc-sdd バージョン**: v1.1.5 / v2.0.0-alpha.3  
**対応AI**: Cursor IDE, Codex CLI

