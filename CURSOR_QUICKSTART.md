# Cursor IDE クイックスタートガイド - cc-sdd コマンド

## 🚀 初めての方へ

XIV Submarines Rewrite では **cc-sdd（Spec-Driven Development）** を導入しています。
Cursor IDE で簡単に仕様駆動開発ができるようになりました。

---

## 📝 コマンドの呼び出し方

### 方法1: チャットから呼び出し（最も簡単）

1. **Ctrl+L** で Cursor Chat を開く
2. チャット入力欄に `@kiro/` と入力
3. 候補リストから選択（例: `@kiro/steering`）
4. Enterで実行

### 方法2: コマンドパレット

1. **Ctrl+Shift+P** でコマンドパレットを開く
2. 「Cursor: Custom Commands」と入力
3. `kiro/` で始まるコマンドを選択

### 方法3: チャットで自然言語

```
Use the kiro/steering command to update project memory
```

Cursor AIが自動的にコマンドを認識して実行します。

---

## 🎯 最初にやるべきこと

### ステップ1: プロジェクト文脈の学習

Cursor Chatで以下を実行：

```
@kiro/steering
```

**何が起こるか**:
- AIがプロジェクト全体を分析
- `plans/specs/steering/` のドキュメントを確認・更新
- プロジェクトのアーキテクチャ、技術スタック、開発原則を学習

**これにより**: AIがプロジェクトの制約や規約を理解し、より適切な提案ができるようになります。

---

## 🔄 新機能開発の流れ

### 完全なワークフロー例

#### 1. 機能のアイデア出し

Cursor Chatで：
```
@kiro/spec-init Add keyboard shortcuts for common submarine operations
```

**実行後**: AIが機能の初期化を行い、次のステップを提案します。

---

#### 2. 要件定義

```
@kiro/spec-requirements submarine-keyboard-shortcuts
```

**実行後**:
- `plans/specs/requirements/submarine-keyboard-shortcuts.md` が作成される
- 機能要件、非機能要件、受け入れ基準が含まれる

**生成される内容例**:
```markdown
# 要件定義: 潜水艦操作のキーボードショートカット

## 機能要件
- FR-1: Ctrl+R でリフレッシュ
- FR-2: Ctrl+N で通知タブ切替
...
```

---

#### 3. 設計レビュー

```
@kiro/spec-design submarine-keyboard-shortcuts
```

**実行後**:
- `plans/specs/design/submarine-keyboard-shortcuts.md` が作成される
- アーキテクチャ、クラス設計、UI/UX設計が含まれる

**AIへの追加質問例**:
```
この設計でパフォーマンス上の問題はありますか？
```

---

#### 4. タスク分解

```
@kiro/spec-tasks submarine-keyboard-shortcuts
```

**実行後**:
- `plans/specs/tasks/submarine-keyboard-shortcuts.md` が作成される
- 実装可能な粒度のタスクリスト
- 見積もり、優先度、依存関係が含まれる

**生成される内容例**:
```markdown
## タスク1: コア実装
### 1.1 KeyboardShortcutHandler 実装
見積もり: 3時間
優先度: 高
...
```

---

#### 5. 実装

```
@kiro/spec-impl submarine-keyboard-shortcuts 1.1,1.2
```

**実行後**:
- AIがタスク1.1と1.2の実装を支援
- コードを生成し、既存コードとの統合を提案

---

## 💡 便利な使い方

### パターン1: 既存機能の改善

```
# 1. ギャップ分析
@kiro/validate-gap notification-system

# 2. AIに質問
「NotificationCoordinatorの複雑度を下げるリファクタリング案を3つ提示してください」

# 3. 設計
@kiro/spec-design notification-refactoring

# 4. 実装
@kiro/spec-impl notification-refactoring 1.1
```

---

### パターン2: バグ修正

```
# 1. 問題の説明
@kiro/spec-init Fix memory leak in submarine snapshot caching

# 2. 要件（バグ再現条件など）
@kiro/spec-requirements memory-leak-fix

# 3. 設計（修正方針）
@kiro/spec-design memory-leak-fix

# 4. 実装
@kiro/spec-impl memory-leak-fix 1.1
```

---

### パターン3: パフォーマンス最適化

```
# 1. 現状分析
@kiro/validate-gap ui-performance

# 2. AIに相談
「60fpsを維持するための最適化ポイントを教えてください」

# 3. 最適化計画
@kiro/spec-tasks ui-performance-optimization
```

---

## 🎓 よくある質問

### Q1: コマンドが見つからない

**A**: Cursor を再起動してみてください。`.cursor/commands/` の変更は再起動後に反映されます。

---

### Q2: コマンド実行後、何も起こらない

**A**: Cursor Chat の履歴を確認してください。AIが応答している可能性があります。

---

### Q3: 仕様書がどこに作られるか分からない

**A**: すべて `plans/specs/` 配下に作成されます：
```
plans/specs/
├── requirements/    # 要件定義書
├── design/          # 設計書
├── tasks/           # タスク分解
└── steering/        # プロジェクトメモリ
```

---

### Q4: エラーが出た

**A**: 以下を確認：
1. `plans/specs/` ディレクトリが存在するか
2. プロジェクトルート（`XIVSubmarinesRewrite/`）で実行しているか
3. ファイルのアクセス権限

---

## 🎬 実践チュートリアル

### チュートリアル: プロジェクトメモリの初回セットアップ

**所要時間**: 2分

1. Cursor IDE で `C:\Codex\apps\XIVSubmarinesRewrite` を開く
2. **Ctrl+L** でチャットを開く
3. 以下を入力して Enter:
```
@kiro/steering
```
4. AIが分析を開始（15-30秒）
5. 結果が表示されたら、`plans/specs/steering/` を確認
6. 完了！

---

### チュートリアル: 簡単な機能を追加

**所要時間**: 10分

**例**: 通知ボタンに確認ダイアログを追加

1. 機能の初期化:
```
@kiro/spec-init Add confirmation dialog before sending notifications
```

2. 要件定義:
```
@kiro/spec-requirements notification-confirmation-dialog
```

3. 生成された `plans/specs/requirements/notification-confirmation-dialog.md` を確認

4. 設計:
```
@kiro/spec-design notification-confirmation-dialog
```

5. タスク分解:
```
@kiro/spec-tasks notification-confirmation-dialog
```

6. 実装（タスク1.1のみ）:
```
@kiro/spec-impl notification-confirmation-dialog 1.1
```

7. AIの提案に従ってコードを編集

8. 完了！

---

## 📚 各コマンドの詳細

### `@kiro/steering` - プロジェクトメモリ管理

**用途**: プロジェクト全体の文脈を更新

**いつ使うか**:
- プロジェクトに初めて参加したとき
- 大きなアーキテクチャ変更があったとき
- 新しいPhaseを始める前

**出力先**: `plans/specs/steering/*.md`

---

### `@kiro/spec-init` - 機能初期化

**用途**: 新しい機能や改善のアイデアを記録

**引数**: 機能の簡潔な説明（1文）

**例**:
```
@kiro/spec-init Implement auto-save for notification settings
```

---

### `@kiro/spec-requirements` - 要件定義

**用途**: 機能要件・非機能要件の詳細化

**引数**: 仕様名（ケバブケース推奨）

**例**:
```
@kiro/spec-requirements auto-save-settings
```

**出力**: `plans/specs/requirements/auto-save-settings.md`

---

### `@kiro/spec-design` - 設計

**用途**: アーキテクチャ設計、クラス設計

**引数**: 仕様名

**例**:
```
@kiro/spec-design auto-save-settings
```

**AIに追加で質問できること**:
- 「この設計のトレードオフは何ですか？」
- 「パフォーマンスへの影響を評価してください」
- 「別の設計案を3つ提示してください」

---

### `@kiro/spec-tasks` - タスク分解

**用途**: 実装可能な粒度にタスクを分解

**引数**: 仕様名

**例**:
```
@kiro/spec-tasks auto-save-settings
```

**出力**: `plans/specs/tasks/auto-save-settings.md`
- タスクリスト（1.1, 1.2, 1.3...）
- 見積もり時間
- 優先度と依存関係

---

### `@kiro/spec-impl` - 実装

**用途**: 特定のタスクを実装

**引数**: `<仕様名> <タスクID1>,<タスクID2>,...`

**例**:
```
@kiro/spec-impl auto-save-settings 1.1,1.2
```

**AIがやること**:
- タスクの詳細を確認
- 必要なファイルを特定
- コード生成または変更提案
- テストコードの提案

---

## 🔧 トラブルシューティング

### コマンドが表示されない

1. Cursor を再起動
2. `.cursor/commands/kiro/` ディレクトリが存在するか確認
3. ファイルが `.md` 拡張子か確認

### コマンド実行でエラー

1. プロジェクトルートで実行しているか確認
2. `plans/specs/` ディレクトリが存在するか確認
3. エラーメッセージを確認し、不足しているファイル/ディレクトリを作成

### AIの応答が期待と異なる

1. `/kiro:steering` でプロジェクトメモリを更新
2. より具体的な指示を追加
3. 仕様書を手動で編集して調整

---

## 📖 関連ドキュメント

- **AGENTS_SDD_INTEGRATION.md**: Cursor/Codex 統合ガイド（詳細版）
- **CLAUDE_SDD_INTEGRATION.md**: Claude Code 使用ガイド
- **PHASE12_CC_SDD_INSTALLATION_PLAN.md**: 導入計画書

---

## 💬 サポート

質問がある場合は、Cursor Chat で以下のように聞いてください：

```
cc-sdd コマンドの使い方を教えてください
```

または

```
@kiro/spec-init の具体的な使用例を見せてください
```

AIがプロジェクトメモリを参照して、XIV Submarines Rewrite 固有の例を提示します。

---

**最終更新**: 2025-10-26  
**対象バージョン**: cc-sdd@next (v2.0.0-alpha.3)  
**Cursor バージョン**: 0.40+ 推奨

