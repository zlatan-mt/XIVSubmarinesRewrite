# 設計書: 通知リトライボタンのツールチップ追加

## 設計概要

**目的**: 通知リトライボタンにツールチップを追加し、リトライ状況を可視化する。

**設計方針**:
1. **最小変更**: 既存の`NotificationMonitorWindowRenderer`に小規模な拡張のみ
2. **パフォーマンス優先**: ImGuiの標準APIを使用し、追加のアロケーションを避ける
3. **保守性重視**: ツールチップロジックを独立した部分クラスに分離

## アーキテクチャ設計

### レイヤー配置

```
Presentation Layer
  └── NotificationMonitorWindowRenderer (既存)
      ├── NotificationMonitorWindowRenderer.cs (既存)
      ├── NotificationMonitorWindowRenderer.RetryTooltip.cs (新規)
      └── RetryTooltipFormatter.cs (新規ユーティリティ)

Application Layer
  └── NotificationDeliveryRecord (既存、変更なし)
```

### 設計パターン

- **部分クラス**: ツールチップロジックを独立したファイルに分離
- **Static Utility**: フォーマット処理を静的メソッドで提供
- **Composition over Inheritance**: 既存クラスの拡張ではなく、機能追加

## クラス設計

### 1. NotificationMonitorWindowRenderer.RetryTooltip.cs (新規)

**責務**: リトライボタンのツールチップ描画ロジック

```csharp
namespace XIVSubmarinesRewrite.Presentation.Rendering;

public partial class NotificationMonitorWindowRenderer
{
    /// <summary>
    /// リトライボタン描画（ツールチップ付き）
    /// </summary>
    private void DrawRetryButtonWithTooltip(NotificationDeliveryRecord record)
    {
        // ボタン描画
        var buttonLabel = "Retry";
        if (ImGui.Button($"{buttonLabel}##{record.Id}"))
        {
            OnRetryClicked(record);
        }

        // ツールチップ描画
        if (ImGui.IsItemHovered())
        {
            DrawRetryTooltip(record);
        }
    }

    /// <summary>
    /// ツールチップ内容を描画
    /// </summary>
    private void DrawRetryTooltip(NotificationDeliveryRecord record)
    {
        ImGui.BeginTooltip();
        
        try
        {
            var tooltipContent = RetryTooltipFormatter.Format(
                record,
                DateTime.UtcNow,
                _theme
            );
            
            foreach (var line in tooltipContent.Lines)
            {
                DrawTooltipLine(line);
            }
        }
        finally
        {
            ImGui.EndTooltip();
        }
    }

    /// <summary>
    /// ツールチップの1行を描画
    /// </summary>
    private void DrawTooltipLine(TooltipLine line)
    {
        if (line.IsSeparator)
        {
            ImGui.Separator();
            return;
        }

        if (line.Color.HasValue)
        {
            ImGui.TextColored(line.Color.Value, line.Text);
        }
        else
        {
            ImGui.Text(line.Text);
        }
    }
}
```

### 2. RetryTooltipFormatter.cs (新規)

**責務**: ツールチップ内容の生成とフォーマット

```csharp
namespace XIVSubmarinesRewrite.Presentation.Rendering;

/// <summary>
/// リトライツールチップのフォーマッター
/// </summary>
public static class RetryTooltipFormatter
{
    /// <summary>
    /// ツールチップ内容を生成
    /// </summary>
    public static TooltipContent Format(
        NotificationDeliveryRecord record,
        DateTime now,
        UiTheme theme)
    {
        var lines = new List<TooltipLine>();

        // ヘッダー
        lines.Add(new TooltipLine
        {
            Text = record.State == DeliveryState.DeadLetter
                ? "リトライ情報 (デッドレター)"
                : "リトライ情報",
            Color = record.State == DeliveryState.DeadLetter
                ? theme.ErrorText
                : null
        });

        // 区切り線
        lines.Add(TooltipLine.Separator);

        // リトライ回数
        var retryText = FormatRetryCount(record);
        lines.Add(new TooltipLine { Text = retryText });

        // 最終試行時刻
        if (record.LastAttemptTime.HasValue)
        {
            var lastAttemptText = $"最終試行: {record.LastAttemptTime.Value:yyyy-MM-dd HH:mm:ss}";
            lines.Add(new TooltipLine { Text = lastAttemptText });
        }

        // 次回試行
        if (record.NextAttemptTime.HasValue && record.State != DeliveryState.DeadLetter)
        {
            var nextAttemptText = FormatNextAttempt(record.NextAttemptTime.Value, now);
            lines.Add(new TooltipLine { Text = nextAttemptText });
        }

        // エラー情報（デッドレターの場合）
        if (record.State == DeliveryState.DeadLetter && !string.IsNullOrEmpty(record.LastError))
        {
            lines.Add(TooltipLine.Separator);
            lines.Add(new TooltipLine
            {
                Text = $"最終エラー: {record.LastError}",
                Color = theme.ErrorText
            });
        }

        return new TooltipContent { Lines = lines };
    }

    /// <summary>
    /// リトライ回数のフォーマット
    /// </summary>
    private static string FormatRetryCount(NotificationDeliveryRecord record)
    {
        if (record.RetryCount == 0)
        {
            return "リトライ履歴なし";
        }

        if (record.State == DeliveryState.DeadLetter)
        {
            return $"リトライ回数: {record.RetryCount}回 (上限到達)";
        }

        return $"リトライ回数: {record.RetryCount}回";
    }

    /// <summary>
    /// 次回試行時刻のフォーマット
    /// </summary>
    private static string FormatNextAttempt(DateTime nextAttempt, DateTime now)
    {
        var timeSpan = nextAttempt - now;
        var remainingSeconds = (int)Math.Max(0, timeSpan.TotalSeconds);

        return $"次回試行: {nextAttempt:yyyy-MM-dd HH:mm:ss} ({remainingSeconds}秒後)";
    }
}

/// <summary>
/// ツールチップの内容
/// </summary>
public record TooltipContent
{
    public required List<TooltipLine> Lines { get; init; }
}

/// <summary>
/// ツールチップの1行
/// </summary>
public record TooltipLine
{
    public string Text { get; init; } = string.Empty;
    public Vector4? Color { get; init; }
    public bool IsSeparator { get; init; }

    public static readonly TooltipLine Separator = new() { IsSeparator = true };
}
```

### 3. 既存クラスの変更

#### NotificationMonitorWindowRenderer.cs

```csharp
// 既存メソッドを修正
private void DrawRetryButton(NotificationDeliveryRecord record)
{
    // 旧実装をDrawRetryButtonWithTooltipに置き換え
    DrawRetryButtonWithTooltip(record);
}
```

## データフロー

### ツールチップ表示フロー

```
1. ユーザーがリトライボタンにホバー
   ↓
2. ImGui.IsItemHovered() が true を返す
   ↓
3. DrawRetryTooltip() が呼び出される
   ↓
4. RetryTooltipFormatter.Format() でツールチップ内容を生成
   ↓
5. NotificationDeliveryRecord からデータ取得
   ↓
6. 各行をループで描画（DrawTooltipLine）
   ↓
7. ImGui.EndTooltip() で完了
```

### データソース

```
NotificationCoordinator
  ↓ (通知履歴)
NotificationDeliveryRecord
  ├─ RetryCount: int
  ├─ LastAttemptTime: DateTime?
  ├─ NextAttemptTime: DateTime?
  ├─ State: DeliveryState
  └─ LastError: string?
  ↓
RetryTooltipFormatter
  ↓
TooltipContent
```

## UI/UX 設計

### ツールチップレイアウト

#### 通常状態
```
┌─────────────────────────────────┐
│ リトライ情報                      │  ← 通常色
│ ─────────────                   │
│ リトライ回数: 2回                │
│ 最終試行: 2025-10-26 14:23:45   │
│ 次回試行: 2025-10-26 14:23:53   │
│          (8秒後)                 │
└─────────────────────────────────┘
```

#### デッドレター状態
```
┌─────────────────────────────────────┐
│ リトライ情報 (デッドレター)          │  ← エラー色
│ ─────────────────────             │
│ リトライ回数: 3回 (上限到達)        │
│ 最終試行: 2025-10-26 14:20:00       │
│ ─────────────────────             │
│ 最終エラー: HTTP 500 Internal       │  ← エラー色
│            Server Error             │
└─────────────────────────────────────┘
```

#### 履歴なし状態
```
┌─────────────────────────────────┐
│ リトライ情報                      │
│ ─────────────                   │
│ リトライ履歴なし                  │
└─────────────────────────────────┘
```

### カラー定義

```csharp
// UiTheme使用
var theme = UiTheme.Instance;

// 通常テキスト
theme.Text

// エラーテキスト
theme.ErrorText

// 背景（ImGui自動）
// 区切り線（ImGui自動）
```

## パフォーマンス設計

### 最適化戦略

#### 1. メモリアロケーション削減
```csharp
// Bad: 毎フレーム新規アロケーション
var tooltip = new StringBuilder();
tooltip.AppendLine("リトライ情報");

// Good: 構造体とスタック割り当て
var lines = new List<TooltipLine>(capacity: 8);  // 事前確保
```

#### 2. 計算キャッシュ
```csharp
// ツールチップ表示中は毎フレーム同じ内容
// → 表示開始時に1回だけ計算
private TooltipContent? _cachedTooltipContent;
private DateTime? _cachedTooltipTime;

private void DrawRetryTooltip(NotificationDeliveryRecord record)
{
    var now = DateTime.UtcNow;
    
    // 1秒以内なら再利用
    if (_cachedTooltipContent != null 
        && _cachedTooltipTime.HasValue
        && (now - _cachedTooltipTime.Value).TotalSeconds < 1)
    {
        // キャッシュ使用
    }
    else
    {
        // 再計算
        _cachedTooltipContent = RetryTooltipFormatter.Format(record, now, _theme);
        _cachedTooltipTime = now;
    }
}
```

#### 3. 条件分岐最適化
```csharp
// 早期リターン
if (!ImGui.IsItemHovered())
{
    return;  // ホバーしていなければ何もしない
}
```

### パフォーマンス目標

| 指標 | 目標値 | 測定方法 |
|------|--------|----------|
| ツールチップ生成時間 | < 100μs | Stopwatch |
| 描画時間 | < 500μs | Dalamud Profiler |
| メモリアロケーション | < 1KB | Memory Profiler |
| フレームレート影響 | 0% | 60fps維持確認 |

## テスト設計

### 単体テスト

#### RetryTooltipFormatterTests.cs

```csharp
public class RetryTooltipFormatterTests
{
    private readonly UiTheme _theme = UiTheme.Instance;

    [Fact]
    public void Format_WithRetryHistory_ReturnsCorrectContent()
    {
        // Arrange
        var record = new NotificationDeliveryRecord
        {
            Id = Guid.NewGuid(),
            RetryCount = 2,
            LastAttemptTime = new DateTime(2025, 10, 26, 14, 23, 45),
            NextAttemptTime = new DateTime(2025, 10, 26, 14, 23, 53),
            State = DeliveryState.Pending
        };
        var now = new DateTime(2025, 10, 26, 14, 23, 45);

        // Act
        var content = RetryTooltipFormatter.Format(record, now, _theme);

        // Assert
        Assert.NotNull(content);
        Assert.Contains(content.Lines, l => l.Text.Contains("リトライ回数: 2回"));
        Assert.Contains(content.Lines, l => l.Text.Contains("最終試行: 2025-10-26 14:23:45"));
        Assert.Contains(content.Lines, l => l.Text.Contains("次回試行:"));
    }

    [Fact]
    public void Format_WithDeadLetter_ShowsErrorInfo()
    {
        // Arrange
        var record = new NotificationDeliveryRecord
        {
            Id = Guid.NewGuid(),
            RetryCount = 3,
            LastAttemptTime = DateTime.UtcNow,
            State = DeliveryState.DeadLetter,
            LastError = "HTTP 500 Internal Server Error"
        };

        // Act
        var content = RetryTooltipFormatter.Format(record, DateTime.UtcNow, _theme);

        // Assert
        Assert.Contains(content.Lines, l => l.Text.Contains("デッドレター"));
        Assert.Contains(content.Lines, l => l.Text.Contains("上限到達"));
        Assert.Contains(content.Lines, l => l.Text.Contains("HTTP 500"));
        Assert.Contains(content.Lines, l => l.Color == _theme.ErrorText);
    }

    [Fact]
    public void Format_WithNoHistory_ShowsNoHistoryMessage()
    {
        // Arrange
        var record = new NotificationDeliveryRecord
        {
            Id = Guid.NewGuid(),
            RetryCount = 0,
            State = DeliveryState.Pending
        };

        // Act
        var content = RetryTooltipFormatter.Format(record, DateTime.UtcNow, _theme);

        // Assert
        Assert.Contains(content.Lines, l => l.Text.Contains("リトライ履歴なし"));
    }
}
```

### E2Eテスト

#### notification-layout.spec.ts

```typescript
test.describe('@notification Retry tooltip', () => {
  test('shows tooltip on hover', async ({ page }) => {
    // Arrange: Navigate to notification tab
    await page.click('text=Notification');
    await page.waitForSelector('[data-testid="retry-button"]');

    // Act: Hover over retry button
    await page.hover('[data-testid="retry-button"]');
    await page.waitForTimeout(500);  // ツールチップ表示待ち

    // Assert: Tooltip is visible
    const tooltip = page.locator('.imgui-tooltip');
    await expect(tooltip).toBeVisible();
  });

  test('displays retry count and last attempt time', async ({ page }) => {
    // Arrange
    await page.click('text=Notification');
    
    // Act
    await page.hover('[data-testid="retry-button"]');
    await page.waitForTimeout(500);

    // Assert
    const tooltip = page.locator('.imgui-tooltip');
    await expect(tooltip).toContainText('リトライ回数:');
    await expect(tooltip).toContainText('最終試行:');
  });

  test('shows dead letter status for failed notifications', async ({ page }) => {
    // Arrange: Create dead letter notification
    // (テストデータセットアップ)

    // Act
    await page.click('text=Notification');
    await page.hover('[data-testid="dead-letter-retry-button"]');
    await page.waitForTimeout(500);

    // Assert
    const tooltip = page.locator('.imgui-tooltip');
    await expect(tooltip).toContainText('デッドレター');
    await expect(tooltip).toContainText('上限到達');
  });
});
```

## エラーハンドリング

### 異常系対応

```csharp
private void DrawRetryTooltip(NotificationDeliveryRecord record)
{
    try
    {
        ImGui.BeginTooltip();
        
        try
        {
            var content = RetryTooltipFormatter.Format(record, DateTime.UtcNow, _theme);
            
            if (content == null || content.Lines.Count == 0)
            {
                ImGui.Text("情報がありません");
                return;
            }
            
            foreach (var line in content.Lines)
            {
                DrawTooltipLine(line);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to render retry tooltip");
            ImGui.Text("ツールチップの表示に失敗しました");
        }
        finally
        {
            ImGui.EndTooltip();
        }
    }
    catch (Exception ex)
    {
        // ImGui例外は致命的なので再スロー
        _logger.LogCritical(ex, "Critical error in tooltip rendering");
        throw;
    }
}
```

## セキュリティ考慮事項

### 入力検証
- `LastError`: 最大1024文字に制限
- `DateTime`: 妥当な範囲（1900-2100年）を確認

### 情報漏洩防止
- Webhook URL は表示しない
- 内部エラーメッセージはサニタイズ

## デプロイ戦略

### 段階的リリース

#### Phase 1: 基本実装
- ツールチップ表示のみ
- リアルタイム更新なし

#### Phase 2: リアルタイム更新
- カウントダウン機能追加

#### Phase 3: デッドレター対応
- エラー情報表示

## トレードオフ分析

### 設計選択肢

| 観点 | 案A: インライン実装 | 案B: 部分クラス分離（採用） | 案C: 別コンポーネント |
|------|-------------------|---------------------------|---------------------|
| 変更規模 | 小 | 中 | 大 |
| 保守性 | 低 | 高 | 高 |
| テスタビリティ | 低 | 中 | 高 |
| パフォーマンス | 高 | 高 | 中 |
| 複雑度 | 低 | 中 | 高 |

**選択理由**: 案Bはバランスが良く、300行制限を守りつつ保守性を確保できる。

## 移行計画

### 既存コードへの影響

- **破壊的変更**: なし
- **後方互換性**: 完全に保持
- **マイグレーション**: 不要

### ロールバック戦略

```csharp
// フィーチャーフラグで制御可能に
private void DrawRetryButton(NotificationDeliveryRecord record)
{
    if (_featureFlags.EnableRetryTooltip)
    {
        DrawRetryButtonWithTooltip(record);
    }
    else
    {
        DrawRetryButtonLegacy(record);
    }
}
```

---

**作成日**: 2025-10-26  
**レビュアー**: Claude Code  
**承認状態**: 設計レビュー待ち  
**関連Phase**: Phase 12-C (試験運用)

