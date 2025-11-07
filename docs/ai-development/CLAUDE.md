# XIV Submarines Rewrite - Claude Code 開発ガイド

## 概要

Claude Code による **Spec-Driven Development（仕様駆動開発）** ガイドです。

高度な推論能力を活かした設計レビュー、アーキテクチャ検討、複雑なリファクタリングに最適です。

---

## 🎯 Claude Code の役割

### 推奨用途
- ✅ **設計レビュー**: アーキテクチャの妥当性検証
- ✅ **複雑な実装**: マルチレイヤーにまたがる機能
- ✅ **リファクタリング**: 大規模コード改善
- ✅ **ドキュメント生成**: 高品質な技術文書作成
- ✅ **問題分析**: バグ原因の深堀り調査

### Cursor/Codex との使い分け
- **Claude Code**: 設計・レビュー・複雑な実装
- **Cursor IDE**: 日常的なコーディング・UI作業
- **Codex CLI**: スクリプト・自動化・一括処理

---

## 📝 主要コマンド

### プロジェクトメモリ管理

```bash
/kiro:steering
```

プロジェクト全体の文脈を学習・更新します。Claude Code はこの情報を元に高精度な提案を行います。

**含まれる情報**:
- Dalamud API Level 13 の制約
- .NET 9.0 アーキテクチャパターン
- ImGui UI 設計パターン
- 通知システムのバッチング戦略
- テスト戦略（xUnit, Playwright）

---

### 仕様駆動開発ワークフロー

#### 1. 新機能の初期化
```bash
/kiro:spec-init <機能の概要説明>
```

**例**:
```bash
/kiro:spec-init Implement real-time submarine position tracking with WebSocket integration
```

#### 2. 要件定義
```bash
/kiro:spec-requirements <spec-name>
```

Claude Code は以下を含む詳細な要件文書を生成します：
- 機能要件
- 非機能要件（パフォーマンス、セキュリティ）
- Dalamud API 制約の考慮
- エッジケースの洗い出し

**出力**: `plans/specs/requirements/<spec-name>.md`

#### 3. 設計文書作成
```bash
/kiro:spec-design <spec-name> [-y]
```

Claude Code の強み：
- 複数のアーキテクチャパターン比較
- トレードオフ分析
- 既存コードとの整合性検証
- パフォーマンス影響の予測

**出力**: `plans/specs/design/<spec-name>.md`

#### 4. タスク分解
```bash
/kiro:spec-tasks <spec-name> [-y]
```

実装可能な粒度にタスクを分解し、優先順位と依存関係を明確化します。

**出力**: `plans/specs/tasks/<spec-name>.md`

#### 5. 実装
```bash
/kiro:spec-impl <spec-name> <task-ids>
```

**例**:
```bash
/kiro:spec-impl websocket-tracking 1.1,1.2,1.3
```

---

### 検証・品質保証コマンド

#### ギャップ分析
```bash
/kiro:validate-gap <spec-name>
```

要件と既存実装の差分を分析し、影響範囲を特定します。

#### 設計検証
```bash
/kiro:validate-design <spec-name>
```

設計の整合性、パフォーマンス、保守性を多角的に評価します。

---

## 🏗️ プロジェクトコンテキスト

### 技術スタック詳細

#### Dalamud プラグインアーキテクチャ
```
Plugin.cs (エントリポイント)
  ↓
Presentation Layer (ImGui UI)
  ↓
Application Layer (Services, Coordinators)
  ↓
Domain Layer (Models, Business Logic)
  ↓
Infrastructure Layer (Dalamud API, Storage)
```

#### 主要コンポーネント
- **SnapshotOrchestrator**: データ取得の調整
- **NotificationCoordinator**: 通知バッチング制御
- **DiscordCycleNotificationAggregator**: Discord通知集約
- **MainWindowRenderer**: メインUI描画（3部分クラス）

### コーディング規約

#### C# スタイル
- **Nullable Reference Types**: 有効化必須
- **Async/Await**: I/O操作は非同期化
- **LINQ**: 可読性優先、過度な連鎖は避ける
- **Dependency Injection**: コンストラクタインジェクション

#### ファイル構成
- 1ファイル300行以下を維持
- 部分クラス (`partial class`) で分割
- 責務ごとにファイル分離

#### テスト
- **単体テスト**: xUnit, Moq
- **E2Eテスト**: Playwright
- **カバレッジ**: 90%以上必須
- **テストケース命名**: `MethodName_Scenario_ExpectedBehavior`

---

## 💡 Claude Code 活用パターン

### パターン1: 設計レビュー重視

```bash
# Step 1: 要件定義
/kiro:spec-requirements new-feature

# Step 2: 複数の設計案を比較検討
/kiro:spec-design new-feature

# → Claude Codeに以下を依頼:
# "この設計案のトレードオフを3つのアプローチで比較してください"
# 1. パフォーマンス優先
# 2. 保守性優先
# 3. 拡張性優先

# Step 3: 最適案を選択後、タスク化
/kiro:spec-tasks new-feature -y
```

### パターン2: 既存コード分析と改善

```bash
# Step 1: プロジェクトメモリ更新
/kiro:steering

# Step 2: ギャップ分析
/kiro:validate-gap notification-system

# → Claude Codeに以下を依頼:
# "NotificationCoordinatorの複雑度を削減する
#  リファクタリング案を3つ提示してください"

# Step 3: 改善実装
/kiro:spec-impl notification-refactor 1.1,1.2
```

### パターン3: 複雑な機能実装

```bash
# Step 1: 詳細な要件定義（Claude Codeで深掘り）
/kiro:spec-requirements complex-feature

# → Claude Codeに追加質問:
# "Dalamud APIの制約下でWebSocket接続を
#  安定化させる方法を検討してください"

# Step 2: アーキテクチャ設計
/kiro:spec-design complex-feature

# Step 3: 段階的実装
/kiro:spec-impl complex-feature 1.1
# → 検証後、次のタスクへ
/kiro:spec-impl complex-feature 1.2
```

---

## 🎯 品質ゲート

Claude Code を活用した各フェーズの品質確認：

### 要件フェーズ
- [ ] 機能要件は SMART（具体的・測定可能・達成可能・関連性・期限）か
- [ ] 非機能要件（パフォーマンス、セキュリティ）は明記されているか
- [ ] Dalamud API 制約は考慮されているか
- [ ] エッジケースは洗い出されているか

### 設計フェーズ
- [ ] 既存アーキテクチャとの整合性は取れているか
- [ ] 複数の設計案を比較検討したか
- [ ] パフォーマンス影響は評価されているか
- [ ] テスト戦略は明確か

### タスクフェーズ
- [ ] タスクは1-3日で完了可能な粒度か
- [ ] 依存関係は明確か
- [ ] 優先順位は適切か
- [ ] テストタスクは含まれているか

### 実装フェーズ
- [ ] コーディング規約に準拠しているか
- [ ] ファイルサイズは300行以下か
- [ ] テストカバレッジは90%以上か
- [ ] ドキュメントは更新されているか

---

## 🔄 Cursor/Codex との連携

### 推奨ワークフロー

```
Claude Code: 設計レビュー、アーキテクチャ検討
  ↓
  設計文書を plans/specs/design/ に出力
  ↓
Cursor IDE: 日常的な実装作業
  ↓
  コード変更をコミット
  ↓
Codex CLI: テスト自動実行、ビルド確認
  ↓
  CI/CD パイプライン実行
  ↓
Claude Code: コードレビュー、品質チェック
```

### ファイル共有
- `plans/specs/`: 3ツール共通の仕様書置き場
- `plans/specs/steering/`: 共有プロジェクトメモリ
- `docs/ai-development/AGENTS.md`: Cursor/Codex用ガイド
- `docs/ai-development/CLAUDE.md`: 本ファイル（Claude Code専用）

---

## 📖 プロジェクト固有の考慮事項

### Dalamud プラグイン制約
- **メインスレッド**: UI操作は必ずメインスレッドで実行
- **リソース管理**: Dispose パターンの徹底
- **プラグインライフサイクル**: 初期化・有効化・無効化・破棄の適切な処理

### ImGui UI 設計
- **レスポンシブ**: ウィンドウサイズに応じたレイアウト調整
- **パフォーマンス**: 毎フレーム描画の最適化
- **アクセシビリティ**: スケーリング対応、コントラスト確保

### 通知システム
- **バッチング**: 4隻分の通知を集約
- **リトライ**: 指数バックオフ戦略
- **デッドレター**: 失敗通知の記録と再送

---

## 🚀 開発開始前チェックリスト

- [ ] `/kiro:steering` でプロジェクト文脈を最新化
- [ ] `develop` ブランチから機能ブランチ作成
- [ ] 既存の `plans/` ドキュメントを確認
- [ ] 関連する既存コンポーネントを把握
- [ ] テスト戦略を明確化

---

## 📚 参考資料

### 内部ドキュメント
- `docs/ai-development/AGENTS.md`: Cursor/Codex用ガイド
- `docs/ai-development/QUICKSTART.md`: クイックスタートガイド
- `README.md`: プロジェクト概要
- `CHANGELOG.md`: バージョン履歴
- `plans/specs/steering/`: プロジェクトメモリ

### 外部リソース
- Dalamud API Documentation
- .NET 9.0 Documentation
- ImGui Documentation

---

**最終更新**: 2025-01-27  
**cc-sdd バージョン**: v1.1.5 / v2.0.0-alpha.3  
**対応AI**: Claude Code  
**推論モデル**: Claude Sonnet 4.5

