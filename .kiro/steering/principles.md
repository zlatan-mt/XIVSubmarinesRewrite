# 開発原則と規約 - XIV Submarines Rewrite

## 開発の基本理念

### 品質重視
- 動くコードを書くだけでなく、品質・保守性・安全性を常に意識する
- プロトタイプフェーズは完了し、本番品質を維持する
- 問題を見つけたら放置せず、必ず対処または明示的に記録する
- **ボーイスカウトルール**: コードを見つけた時よりも良い状態で残す

### 仕様駆動開発（SDD）
- Phase 12 から cc-sdd を導入
- 要件 → 設計 → タスク → 実装の構造化ワークフロー
- 各フェーズで品質ゲートを設ける
- ドキュメントとコードの同期を維持

## コーディング規約

### C# スタイル

#### 命名規則
```csharp
// クラス: PascalCase
public class NotificationCoordinator { }

// インターフェース: I + PascalCase
public interface IDataSource { }

// メソッド: PascalCase
public void RegisterSnapshot() { }

// パブリックプロパティ: PascalCase
public string SubmarineName { get; set; }

// プライベートフィールド: _camelCase
private readonly ILogger _logger;

// ローカル変数: camelCase
var notificationCount = 0;

// 定数: SCREAMING_SNAKE_CASE
private const int MAX_RETRY_COUNT = 3;
```

#### 型ヒント
- **必須**: すべてのメソッド、プロパティに型を明示
- **Nullable Reference Types**: 有効化必須
- **var**: 型が明白な場合のみ使用

```csharp
// Good
public async Task<List<Submarine>> GetSubmarinesAsync(string characterId)

// Bad
public async Task GetSubmarinesAsync(string characterId)  // 戻り値の型なし
```

#### ファイルサイズ
- **1ファイル300行以下**を維持
- 超過する場合は部分クラス (`partial class`) で分割
- 責務ごとにファイルを分離

```csharp
// MainWindowRenderer.cs
public partial class MainWindowRenderer { }

// MainWindowRenderer.Toolbar.cs
public partial class MainWindowRenderer
{
    private void DrawToolbar() { }
}

// MainWindowRenderer.Tabs.cs
public partial class MainWindowRenderer
{
    private void DrawTabs() { }
}
```

### DRY原則
- 重複を避け、単一の信頼できる情報源を維持
- 3回以上の繰り返しは抽出してメソッド化
- 共通ロジックはユーティリティクラスへ

### SOLID原則
1. **単一責任原則（SRP）**: 1クラス1責務
2. **開放閉鎖原則（OCP）**: 拡張に開き、修正に閉じる
3. **リスコフの置換原則（LSP）**: 派生型は基底型と置換可能
4. **インターフェース分離原則（ISP）**: 小さく特化したインターフェース
5. **依存性逆転原則（DIP）**: 抽象に依存、具象に依存しない

## エラーハンドリング

### 原則
- エラーの抑制（`@ts-ignore`、`try-catch` で握りつぶす等）ではなく、根本原因を修正
- 早期にエラーを検出し、明確なエラーメッセージを提供
- エラーケースも必ずテストでカバー
- 外部API（Discord, Notion）は必ず失敗する可能性を考慮

### 例外処理
```csharp
// Good
try
{
    await SendDiscordNotificationAsync(message);
}
catch (HttpRequestException ex)
{
    _logger.LogError(ex, "Failed to send Discord notification: {Message}", ex.Message);
    await _deadLetterQueue.EnqueueAsync(message);
    throw; // 呼び出し元で判断させる
}

// Bad
try
{
    await SendDiscordNotificationAsync(message);
}
catch (Exception)
{
    // 握りつぶし - 問題が隠蔽される
}
```

### リトライ戦略
- 指数バックオフ（1秒、2秒、4秒...）
- 最大リトライ回数: 3回
- タイムアウト: 10秒
- 失敗時はデッドレターキューへ

## テスト規律

### カバレッジ目標
- **単体テスト**: 90%以上
- **E2Eテスト**: 主要フロー100%
- **回帰テスト**: 過去のバグは必ずテストケース化

### テスト命名
```csharp
[Fact]
public void RegisterSnapshot_WithValidData_StoresSnapshot()
{
    // Arrange
    var snapshot = CreateValidSnapshot();
    
    // Act
    _orchestrator.RegisterSnapshot(snapshot);
    
    // Assert
    Assert.True(_storage.Contains(snapshot.CharacterId));
}
```

### テスト原則
- **AAA パターン**: Arrange, Act, Assert
- **テスト独立性**: テスト間の依存を避ける
- **高速実行**: 単体テストは1秒以内
- **決定性**: 常に同じ結果を返す

### モック戦略
- 外部依存はモック化（Dalamud API, HTTP通信等）
- ビジネスロジックは実装をテスト
- モックの過度な使用は避ける

## パフォーマンス

### 測定に基づく最適化
- 推測ではなく計測に基づいて最適化
- Dalamud の Profiler を活用
- 60fps維持が最優先

### UI パフォーマンス
- **描画時間**: < 16.7ms / frame
- **メモリアロケーション**: 毎フレームの GC を避ける
- **キャッシュ活用**: 再計算を最小化

### データ取得
- **バッチ取得**: 個別取得を避ける
- **差分更新**: 変更検出で無駄な更新を削減
- **遅延ロード**: 必要になるまでロードを遅延

## セキュリティ

### 認証情報
- **Webhook URL**: ローカル設定ファイルに平文保存
- **環境変数**: 非対応（プラグイン特性上）
- **Git除外**: 認証情報を含むファイルは `.gitignore`

### 入力検証
- すべての外部入力を検証
- Webhook URL: 形式検証（HTTP/HTTPS）
- ユーザー入力: ImGui側で制限

## Git運用

### コミットメッセージ
- **Conventional Commits**: 形式を使用
  - `feat:` 新機能
  - `fix:` バグ修正
  - `docs:` ドキュメント
  - `test:` テスト
  - `refactor:` リファクタリング
  - `chore:` その他

```
feat: add tooltip to notification retry button

- Show retry count and last attempt time
- Implement ImGui tooltip with hover delay
- Add tests for tooltip formatting

Related: Phase 13-A
```

### コミット粒度
- **原子的**: 単一の変更に焦点
- **論理的**: 意味のある単位でコミット
- **ビルド可能**: 各コミットでビルドが通る

### ブランチ運用
```
feature/* → develop
develop → release (定期)
release → tag → GitHub Release
```

## コードレビュー

### レビュー観点
1. **機能性**: 要件を満たしているか
2. **品質**: コーディング規約に準拠しているか
3. **テスト**: 適切なテストがあるか
4. **パフォーマンス**: 60fps を維持できるか
5. **セキュリティ**: 脆弱性はないか

### レビューコメント
- 建設的な改善提案として記述
- 個人ではなくコードに焦点
- 変更の理由と影響を明確に説明

## ドキュメント

### README
- プロジェクト概要
- インストール手順
- 使用方法
- トラブルシューティング

### コード内コメント
- **「なぜ」を説明**: コードで「何を」は表現
- **複雑なロジック**: 意図を明記
- **回避策**: 理由とチケット番号を記載

```csharp
// Good: なぜ必要かを説明
// Dalamud API制約: UI操作はメインスレッドで実行必須
_mainThreadDispatcher.Invoke(() => UpdateUI());

// Bad: 何をしているかだけ
// UIを更新
_mainThreadDispatcher.Invoke(() => UpdateUI());
```

### 設計判断の記録
- 重要な設計判断は `plans/specs/design/` に記録
- トレードオフの理由を明記
- 代替案との比較を残す

## 依存関係管理

### NuGet パッケージ
- 本当に必要な依存関係のみを追加
- セキュリティパッチは定期的に更新
- ライセンス、サイズ、メンテナンス状況を確認

### ローカル DLL
- `vendor/Dalamud/` に配置
- `DalamudRestore` スクリプトで自動取得
- バージョン固定（API Level 13）

## 継続的改善

### 振り返り
- 各Phase完了時に振り返りを実施
- 学んだことを次のPhaseに活かす
- プロセス改善を記録

### 技術的負債
- 明示的にコメントやドキュメントに記録
- Phase計画で解消の優先順位を決定
- 定期的に返済

### 新技術の評価
- 新しいツールや手法を適切に評価
- パイロットプロジェクトで検証
- チームで合意してから導入

## Phase開発ワークフロー

### Phase計画
1. **Phase企画**: 目標とスコープを定義
2. **要件定義**: `/kiro:spec-requirements phase<N>-<feature>`
3. **設計**: `/kiro:spec-design phase<N>-<feature>`
4. **タスク分解**: `/kiro:spec-tasks phase<N>-<feature>`
5. **実装**: `/kiro:spec-impl phase<N>-<feature> <task-ids>`
6. **Phase完了レポート**: 成果と学びを記録

### Phase間の連携
- 前Phaseの完了レポートを確認
- 技術的負債の引き継ぎ
- アーキテクチャ変更の共有

## 開発環境

### 必須ツール
- Visual Studio 2022 または Rider
- .NET 9.0 SDK
- Git
- Cursor IDE / Claude Code / Codex CLI（いずれか）

### 推奨ツール
- Dalamud Dev Plugin（デバッグ用）
- Playwright（E2Eテスト用）
- DalamudRestore（DLL取得用）

### 環境変数
```bash
DALAMUD_LIB_PATH=C:\Users\<user>\AppData\Roaming\XIVLauncher\addon\Hooks\dev
```

## チーム開発

### コミュニケーション
- 重要な変更は事前にレビュー依頼
- ブロッカーは早めに共有
- 週次で進捗を報告

### ナレッジ共有
- `plans/specs/` で設計判断を共有
- `CHANGELOG.md` で変更履歴を記録
- Phase完了レポートで学びを共有

