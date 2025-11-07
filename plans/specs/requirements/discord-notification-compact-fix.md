# 要件定義: Discord通知のコンパクト化と重複修正

## 概要

Discord通知をよりコンパクトにし、3隻目と4隻目が同じになる問題を修正する。

## 背景

### 現状の課題

1. **冗長すぎる通知**:
   - バッチ通知で各潜水艦が個別のフィールド（inline=true）になっている
   - リマインダーコマンドが別フィールドとして追加されている
   - もっとコンパクトにできる余地がある

2. **3隻目と4隻目が同じになる問題**:
   - バッチ通知で3隻目と4隻目が全く同じ内容で表示される
   - `DiscordCycleNotificationAggregator.TryCreateAggregate`で`GroupBy(n => n.SubmarineId)`を使用しているが、重複チェックが不十分

## 機能要件

### FR-1: バッチ通知のコンパクト化
**優先度**: 高

**説明**: バッチ通知をよりコンパクトな形式に変更する。

**現状の問題**:
- 各潜水艦が個別のフィールド（inline=true）で横並び表示
- リマインダーコマンドが別フィールドとして追加
- フィールド数が多すぎて冗長

**新フォーマット**:
```
タイトル: [キャラクター名] - 4隻出航
説明: 
Sub-1: 10/26 14:30 (12h)
Sub-2: 10/26 15:00 (12.5h)
Sub-3: 10/26 16:30 (14h)
Sub-4: 10/26 18:00 (15.5h)

リマインダー: `/remind #submarine 10/26 14:30 4隻帰還開始`
```

**変更内容**:
- 説明フィールドに全潜水艦の情報を1つのテキストブロックとして配置
- フィールド数を削減（4個 → 0-1個）
- リマインダーコマンドは説明の最後に追加（別フィールドにしない）

**受け入れ基準**:
- [ ] 説明フィールドに全潜水艦の情報が含まれる
- [ ] フィールド数が0-1個（リマインダー有効時のみ1個）
- [ ] スクロールなしで全体が見える
- [ ] 各潜水艦が1行で表示される

---

### FR-2: 重複通知の修正
**優先度**: 高

**説明**: 3隻目と4隻目が同じになる問題を修正する。

**原因分析**:
- `DiscordCycleNotificationAggregator.TryCreateAggregate`で`GroupBy(n => n.SubmarineId)`を使用
- 同じSubmarineIdの通知が複数ある場合、最新のものだけを取っている
- しかし、4隻の潜水艦は異なるSubmarineId（Slot 0, 1, 2, 3）を持つはず
- 問題は、同じSubmarineIdの通知が複数ある場合に、重複が発生している可能性

**修正方針**:
1. `TryCreateAggregate`で重複チェックを強化
2. SubmarineIdだけでなく、HashKeyも確認して重複を排除
3. 4隻すべてが異なるSubmarineIdを持つことを保証

**受け入れ基準**:
- [ ] 4隻すべてが異なるSubmarineIdを持つ
- [ ] 3隻目と4隻目が異なる内容で表示される
- [ ] 重複通知が発生しない
- [ ] ログで重複が検出された場合に警告を出力

---

## 非機能要件

### NFR-1: 後方互換性
**優先度**: 中

**説明**: 既存の通知フォーマットとの互換性を維持。

**考慮事項**:
- 通知フォーマット変更は破壊的変更ではない（受信側に影響なし）
- 既存の設定はそのまま動作する

---

### NFR-2: パフォーマンス
**優先度**: 低

**説明**: 通知生成のパフォーマンスを維持。

**要件**:
- 通知生成時間: < 10ms
- メモリ使用: 既存と同等

---

### NFR-3: 保守性
**優先度**: 高

**説明**: コードの可読性と保守性を維持。

**要件**:
- フォーマットロジックを独立したメソッドに分離
- 単体テストでフォーマット結果を検証
- マジックナンバーや文字列を定数化

---

## 影響範囲

### 変更対象ファイル

1. **VoyageNotificationFormatter.cs**
   - `CreateDiscordBatchPayload()`: 説明フィールドに全情報を集約
   - フィールド数を削減

2. **DiscordCycleNotificationAggregator.cs**
   - `TryCreateAggregate()`: 重複チェックを強化
   - SubmarineIdとHashKeyの両方で重複を確認

3. **テストファイル**
   - `VoyageNotificationFormatterTests.cs`: バッチ通知フォーマットのテスト追加
   - `DiscordCycleNotificationAggregatorTests.cs`: 重複チェックのテスト追加

---

## 受け入れ基準

### AC-1: バッチ通知のコンパクト化
- [ ] 説明フィールドに全潜水艦の情報が含まれる
- [ ] フィールド数が0-1個（リマインダー有効時のみ1個）
- [ ] スクロールなしで全体が見える
- [ ] 各潜水艦が1行で表示される
- [ ] リマインダーコマンドが説明の最後に含まれる（有効時）

### AC-2: 重複通知の修正
- [ ] 4隻すべてが異なるSubmarineIdを持つ
- [ ] 3隻目と4隻目が異なる内容で表示される
- [ ] 重複通知が発生しない
- [ ] ログで重複が検出された場合に警告を出力

### AC-3: テスト
- [ ] 単体テストがある（バッチ通知フォーマット）
- [ ] 単体テストがある（重複チェック）
- [ ] E2Eテストがある（Playwright: 通知表示確認）
- [ ] カバレッジが90%以上

---

## テストシナリオ

### TS-1: バッチ通知のコンパクト化テスト
```csharp
[Fact]
public void CreateDiscordBatchPayload_WithFourSubmarines_ReturnsCompactFormat()
{
    // Arrange
    var notifications = new[]
    {
        CreateNotification("Sub-1", new DateTime(2025, 10, 26, 14, 30, 0)),
        CreateNotification("Sub-2", new DateTime(2025, 10, 26, 15, 0, 0)),
        CreateNotification("Sub-3", new DateTime(2025, 10, 26, 16, 30, 0)),
        CreateNotification("Sub-4", new DateTime(2025, 10, 26, 18, 0, 0))
    };

    // Act
    var payload = formatter.CreateDiscordBatchPayload(VoyageStatus.Underway, "Character", notifications);

    // Assert
    Assert.Empty(payload.Fields); // またはリマインダー有効時のみ1個
    Assert.Contains("Sub-1: 10/26 14:30", payload.Description);
    Assert.Contains("Sub-2: 10/26 15:00", payload.Description);
    Assert.Contains("Sub-3: 10/26 16:30", payload.Description);
    Assert.Contains("Sub-4: 10/26 18:00", payload.Description);
}
```

### TS-2: 重複通知の修正テスト
```csharp
[Fact]
public void TryCreateAggregate_WithDuplicateSubmarineIds_ReturnsUniqueSubmarines()
{
    // Arrange
    var state = new CycleState();
    state.Underway[new SubmarineId(1, 0)] = CreateNotification("Sub-1", slot: 0);
    state.Underway[new SubmarineId(1, 1)] = CreateNotification("Sub-2", slot: 1);
    state.Underway[new SubmarineId(1, 2)] = CreateNotification("Sub-3", slot: 2);
    state.Underway[new SubmarineId(1, 3)] = CreateNotification("Sub-4", slot: 3);
    state.CycleReady = true;

    // Act
    var result = aggregator.TryCreateAggregate(state, out var aggregate);

    // Assert
    Assert.True(result);
    Assert.Equal(4, aggregate.Length);
    Assert.Equal(4, aggregate.Select(n => n.SubmarineId.Slot).Distinct().Count());
    Assert.NotEqual(aggregate[2].SubmarineId, aggregate[3].SubmarineId);
}
```

---

## リスク分析

### 技術的リスク
| リスク | 影響 | 確率 | 軽減策 |
|--------|------|------|--------|
| 説明フィールドが長すぎる | 中 | 低 | 文字数制限を確認（Discord: 4096文字） |
| 重複チェックのロジックエラー | 高 | 中 | 単体テストで網羅的に検証 |
| 既存の通知フォーマットとの不整合 | 低 | 低 | 既存テストを確認 |

---

## 参考資料

### 既存実装
- `VoyageNotificationFormatter.cs`: 109-173行（CreateDiscordBatchPayload）
- `DiscordCycleNotificationAggregator.cs`: 146-155行（TryCreateAggregate）

### Discord Webhook仕様
- Embed description: 最大4096文字
- Field value: 最大1024文字
- Fields: 最大25個

---

**作成日**: 2025-01-27  
**関連Phase**: Phase 13以降  
**優先度**: 高  
**次のステップ**: `/kiro:spec-design discord-notification-compact-fix`

