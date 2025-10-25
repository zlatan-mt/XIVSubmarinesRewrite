# 要件定義: 通知リトライボタンのツールチップ追加

## 概要

通知タブの「リトライ」ボタンにマウスホバー時のツールチップを追加し、リトライ回数と最終試行時刻を表示することで、ユーザーが通知の再送状況を直感的に把握できるようにする。

## 背景

### 現状の課題
- リトライボタンをクリックしても、何回リトライしたかが分からない
- 最後にいつリトライを試みたかの情報がない
- ユーザーが「本当にリトライされたのか」を確認できない

### 解決したい問題
- 通知の再送状況の可視性向上
- ユーザーの不安解消（「ちゃんと送られているか？」）
- デバッグ時の情報提供

## 機能要件

### FR-1: ツールチップ表示
**優先度**: 高

**説明**: リトライボタンにマウスをホバーすると、以下の情報を含むツールチップを表示する。

**表示内容**:
```
リトライ情報
─────────────
リトライ回数: 2回
最終試行: 2025-10-26 14:23:45
次回試行: 2025-10-26 14:23:53 (8秒後)
```

**条件**:
- リトライ履歴がある場合のみ「最終試行」を表示
- 次回試行がスケジュールされている場合のみ「次回試行」を表示
- リトライ回数が0回の場合は「リトライ履歴なし」と表示

### FR-2: リアルタイム更新
**優先度**: 中

**説明**: ツールチップの情報は、ホバー中もリアルタイムで更新される。

**更新内容**:
- 「次回試行」のカウントダウン（1秒ごと）
- リトライ実行後の「リトライ回数」と「最終試行」の即時反映

### FR-3: デッドレター状態の表示
**優先度**: 中

**説明**: デッドレターキューに入っている通知の場合、その旨を表示する。

**表示例**:
```
リトライ情報 (デッドレター)
─────────────────────────
リトライ回数: 3回 (上限到達)
最終試行: 2025-10-26 14:20:00
最終エラー: HTTP 500 Internal Server Error
```

## 非機能要件

### NFR-1: パフォーマンス
- **ツールチップ表示遅延**: < 500ms
- **描画オーバーヘッド**: < 0.5ms / frame（60fps維持）
- **メモリ使用**: ツールチップ用データは既存の通知データを参照（新規アロケーション不可）

### NFR-2: UI/UX
- **ホバー遅延**: 300ms（ImGuiデフォルト）
- **フォント**: システムデフォルト（既存UIと統一）
- **色**: UiTheme パレットを使用（WCAG 2.1準拠）
- **サイズ**: 自動調整（内容に応じて）

### NFR-3: 互換性
- **Dalamud API**: Level 13
- **ImGui**: Dalamud.Bindings.ImGui（既存バージョン）
- **既存機能**: リトライボタンの既存動作に影響なし

### NFR-4: 保守性
- **コード追加**: < 100行
- **ファイル分離**: NotificationMonitorWindowRenderer の部分クラスとして追加
- **テストカバレッジ**: 90%以上

## 制約

### 技術的制約
- **ImGuiツールチップAPI**: `ImGui.BeginTooltip()` / `ImGui.EndTooltip()` を使用
- **データソース**: `NotificationDeliveryRecord` から取得
- **更新頻度**: UI更新周期（10秒）に依存

### Dalamud制約
- **メインスレッド**: ツールチップ描画はメインスレッドで実行
- **リソース管理**: 追加のDispose不要（ImGuiのライフサイクルに従う）

## ユースケース

### UC-1: 通常のリトライ状況確認
**アクター**: プレイヤー

**前提条件**:
- 通知タブが開いている
- リトライ履歴がある通知が存在する

**主フロー**:
1. ユーザーがリトライボタンにマウスをホバー
2. システムが通知履歴を取得
3. システムがツールチップを表示
4. ユーザーがリトライ回数と最終試行時刻を確認
5. ユーザーがマウスを離す
6. システムがツールチップを非表示

**期待結果**: リトライ状況が一目で分かる

### UC-2: デッドレター通知の確認
**アクター**: プレイヤー

**前提条件**:
- デッドレターキューに通知がある

**主フロー**:
1. ユーザーがデッドレター通知のリトライボタンにホバー
2. システムがデッドレター状態を含むツールチップを表示
3. ユーザーがエラー内容を確認
4. ユーザーが問題を把握し、Webhook URLを修正

**期待結果**: エラー原因が明確になる

### UC-3: リアルタイム更新の確認
**アクター**: プレイヤー

**前提条件**:
- 次回リトライがスケジュールされている

**主フロー**:
1. ユーザーがリトライボタンにホバー
2. システムが「次回試行: 8秒後」を表示
3. 1秒経過
4. システムが「次回試行: 7秒後」に更新
5. ユーザーがカウントダウンを確認

**期待結果**: リアルタイムな情報更新を体験

## 受け入れ基準

### AC-1: ツールチップ表示
- [ ] リトライボタンにホバーするとツールチップが表示される
- [ ] リトライ回数が正確に表示される
- [ ] 最終試行時刻が `yyyy-MM-dd HH:mm:ss` 形式で表示される
- [ ] 履歴がない場合は「リトライ履歴なし」と表示される

### AC-2: リアルタイム更新
- [ ] 次回試行までのカウントダウンが1秒ごとに更新される
- [ ] リトライ実行後、情報が即座に反映される

### AC-3: デッドレター表示
- [ ] デッドレター状態が明示される
- [ ] 最終エラーメッセージが表示される
- [ ] リトライ上限到達が分かる

### AC-4: パフォーマンス
- [ ] ツールチップ表示が500ms以内に完了する
- [ ] 60fpsが維持される（描画オーバーヘッド < 0.5ms）
- [ ] メモリリークが発生しない

### AC-5: UI/UX
- [ ] ツールチップのフォントと色が既存UIと統一されている
- [ ] ツールチップが画面外にはみ出さない（ImGui自動調整）
- [ ] ホバー遅延が適切（300ms）

### AC-6: テスト
- [ ] 単体テストがある（ツールチップ内容の生成ロジック）
- [ ] E2Eテストがある（Playwright: ツールチップ表示確認）
- [ ] カバレッジが90%以上

## テストシナリオ

### TS-1: 基本表示テスト
```csharp
[Fact]
public void FormatRetryTooltip_WithRetryHistory_ReturnsFormattedText()
{
    // Arrange
    var record = new NotificationDeliveryRecord
    {
        RetryCount = 2,
        LastAttemptTime = new DateTime(2025, 10, 26, 14, 23, 45),
        NextAttemptTime = new DateTime(2025, 10, 26, 14, 23, 53)
    };

    // Act
    var tooltip = RetryTooltipFormatter.Format(record, DateTime.Now);

    // Assert
    Assert.Contains("リトライ回数: 2回", tooltip);
    Assert.Contains("最終試行: 2025-10-26 14:23:45", tooltip);
    Assert.Contains("次回試行:", tooltip);
}
```

### TS-2: デッドレターテスト
```csharp
[Fact]
public void FormatRetryTooltip_ForDeadLetter_ShowsErrorInfo()
{
    // Arrange
    var record = new NotificationDeliveryRecord
    {
        State = DeliveryState.DeadLetter,
        RetryCount = 3,
        LastError = "HTTP 500 Internal Server Error"
    };

    // Act
    var tooltip = RetryTooltipFormatter.Format(record, DateTime.Now);

    // Assert
    Assert.Contains("デッドレター", tooltip);
    Assert.Contains("3回 (上限到達)", tooltip);
    Assert.Contains("HTTP 500", tooltip);
}
```

### TS-3: E2Eテスト（Playwright）
```typescript
test('@notification Retry button tooltip shows retry information', async ({ page }) => {
  // Navigate to notification tab
  await page.click('text=Notification');
  
  // Hover over retry button
  await page.hover('[data-testid="retry-button"]');
  
  // Verify tooltip appears
  const tooltip = await page.locator('.imgui-tooltip');
  await expect(tooltip).toBeVisible();
  
  // Verify content
  await expect(tooltip).toContainText('リトライ回数:');
  await expect(tooltip).toContainText('最終試行:');
});
```

## データモデル

### 既存モデル拡張
```csharp
// NotificationDeliveryRecord (既存)
public class NotificationDeliveryRecord
{
    public int RetryCount { get; set; }                  // 既存
    public DateTime? LastAttemptTime { get; set; }       // 既存
    public DateTime? NextAttemptTime { get; set; }       // 既存
    public DeliveryState State { get; set; }             // 既存
    public string? LastError { get; set; }               // 既存
}
```

### 新規ユーティリティ
```csharp
// RetryTooltipFormatter (新規)
public static class RetryTooltipFormatter
{
    public static string Format(NotificationDeliveryRecord record, DateTime now);
    public static string FormatTimeSpan(TimeSpan span);
}
```

## UI仕様

### ツールチップレイアウト
```
┌─────────────────────────────────┐
│ リトライ情報                      │
│ ─────────────                   │
│ リトライ回数: 2回                │
│ 最終試行: 2025-10-26 14:23:45   │
│ 次回試行: 2025-10-26 14:23:53   │
│          (8秒後)                 │
└─────────────────────────────────┘
```

### カラー仕様
- **背景**: `UiTheme.TooltipBg`
- **テキスト**: `UiTheme.Text`
- **区切り線**: `UiTheme.Border`
- **デッドレター**: `UiTheme.ErrorText`

## 影響範囲

### 変更対象ファイル
1. `src/Presentation/Rendering/NotificationMonitorWindowRenderer.cs`
   - `DrawRetryButton()` にツールチップロジック追加
2. `src/Presentation/Rendering/RetryTooltipFormatter.cs` (新規)
   - ツールチップ内容の生成ロジック
3. `tests/XIVSubmarinesRewrite.Tests/RetryTooltipFormatterTests.cs` (新規)
   - 単体テスト
4. `tests/Playwright/notification-layout.spec.ts`
   - E2Eテスト追加

### 影響を受ける機能
- **通知タブ**: リトライボタンの描画ロジック
- **なし**: 既存の通知送信ロジックには影響なし

## リスク

### 技術的リスク
| リスク | 影響 | 軽減策 |
|--------|------|--------|
| 描画パフォーマンス低下 | 中 | プロファイラで測定、キャッシュ活用 |
| メモリリーク | 低 | ImGuiのライフサイクルに従う |
| ツールチップの文字化け | 低 | UTF-8エンコーディング確認 |

### UX リスク
| リスク | 影響 | 軽減策 |
|--------|------|--------|
| ツールチップが邪魔 | 低 | ホバー遅延300msで調整 |
| 情報過多 | 低 | 必要最小限の情報に絞る |

## 参考資料

### ImGui ツールチップAPI
- `ImGui.BeginTooltip()`: ツールチップ開始
- `ImGui.EndTooltip()`: ツールチップ終了
- `ImGui.IsItemHovered()`: アイテムがホバーされているか

### 既存実装
- `MainWindowRenderer.Toolbar.cs`: UiThemeの使用例
- `OverviewRowFormatter.cs`: 時刻フォーマットの参考

---

**作成日**: 2025-10-26  
**作成者**: cc-sdd workflow  
**バージョン**: 1.0  
**関連Phase**: Phase 12-C (試験運用)

