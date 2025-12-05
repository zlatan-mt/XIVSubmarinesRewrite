# 設計書: Discord通知メッセージの最適化

## 設計概要

**目的**: Discord通知を出航時のみに限定し、必要最小限の情報（帰還時刻）のみを表示する。

**設計方針**:
1. **最小変更**: 既存の通知システムのロジックは維持、フォーマット層のみ変更
2. **段階的実装**: まずCompleted通知停止、次にフォーマット簡素化、最後にReminder Bot統合
3. **設定互換性**: 既存の設定ファイルは読み込むが、Completed通知フラグは無視

## アーキテクチャ設計

### レイヤー配置

```
Application Layer
  └── NotificationCoordinator (変更)
      └── VoyageCompletionProjection (変更)
          ├── Completed通知のフィルタリング追加
          └── Underway通知のみ送信

  └── VoyageNotificationFormatter (変更)
      ├── CreateDiscordPayload() → 簡素化
      ├── CreateDiscordBatchPayload() → 簡素化
      ├── BuildDiscordFields() → 削除または1フィールドのみ
      ├── BuildBatchLines() → 1行フォーマットに変更
      └── FormatReminderCommand() → 新規追加

Presentation Layer
  └── NotificationMonitorWindowRenderer (変更)
      └── NotifyVoyageCompleted チェックボックスを非表示または削除
```

### データフロー

#### Before（現状）
```
VoyageCompletionProjection
  ↓
  [Underway] → NotificationCoordinator → Discord
  [Completed] → NotificationCoordinator → Discord ← 削除
```

#### After（改善後）
```
VoyageCompletionProjection
  ↓
  [Underway のみ] → NotificationCoordinator → Discord
  [Completed] → フィルタで停止 ✓
```

---

## クラス設計

### 1. VoyageCompletionProjection の変更

**責務**: Completed通知を送信しないようフィルタリング

```csharp
namespace XIVSubmarinesRewrite.Application.Notifications;

public sealed partial class VoyageCompletionProjection
{
    // 既存メソッドを修正
    private void ProcessVoyageStateChange(/* ... */)
    {
        // ... 既存のロジック ...

        // Completed通知をスキップ
        if (currentStatus == VoyageStatus.Completed)
        {
            // Log only, do not send notification
            _log.Log(LogLevel.Debug, 
                $"[Notifications] Skipping Completed notification for {submarine.Name} (arrival notifications disabled)");
            return;
        }

        // Underway通知のみ送信
        if (currentStatus == VoyageStatus.Underway)
        {
            var notification = CreateNotification(/* ... */);
            SendToCoordinator(notification);
        }
    }
}
```

**変更点**:
- Completed時に通知を送信せず、ログのみ出力
- Underway時のみ通知送信

---

### 2. VoyageNotificationFormatter の変更

**責務**: 簡素化されたDiscordペイロードを生成

#### 2.1 単体通知フォーマット

```csharp
public DiscordNotificationPayload CreateDiscordPayload(VoyageNotification notification)
{
    // Underway のみサポート（Completed は呼び出されない想定）
    if (notification.Status != VoyageStatus.Underway)
    {
        throw new ArgumentException(
            $"Only Underway notifications are supported. Got: {notification.Status}",
            nameof(notification));
    }

    var title = $"{notification.SubmarineLabel} 出航";
    var description = notification.RouteDisplay ?? notification.RouteId ?? "航路不明";
    var color = UnderwayColor; // #3498DB

    // フィールドは1つのみ: 帰還予定
    var arrivalTime = FormatArrivalTime(notification.ArrivalLocal);
    var remaining = FormatRemainingConcise(notification.Duration);
    var arrivalField = new DiscordNotificationField(
        "帰還予定",
        $"{arrivalTime} ({remaining})",
        false
    );

    var fields = new List<DiscordNotificationField> { arrivalField };

    // オプション: リマインダーコマンド
    if (_settings.EnableReminderCommand)
    {
        var reminderCommand = FormatReminderCommand(
            _settings.ReminderChannelName ?? "#submarine",
            notification.ArrivalLocal,
            notification.SubmarineLabel
        );
        var reminderField = new DiscordNotificationField(
            "リマインダー設定",
            $"`{reminderCommand}`",
            false
        );
        fields.Add(reminderField);
    }

    return new DiscordNotificationPayload(
        Title: title,
        Description: description,
        Color: color,
        Fields: fields,
        Footer: null);
}
```

**削減効果**:
- フィールド: 6個 → 1-2個（リマインダー含む場合2個）
- コード: 20行 → 35行（エラーハンドリング含む）

---

#### 2.2 バッチ通知フォーマット

```csharp
public DiscordNotificationPayload CreateDiscordBatchPayload(
    VoyageStatus status, 
    string characterLabel, 
    IReadOnlyList<VoyageNotification> notifications)
{
    // Underway のみサポート
    if (status != VoyageStatus.Underway)
    {
        throw new ArgumentException(
            $"Only Underway batch notifications are supported. Got: {status}",
            nameof(status));
    }

    var title = $"{characterLabel} - {notifications.Count}隻出航";
    var description = string.Empty; // シンプルに

    // 各潜水艦を1行で表示
    var fields = new List<DiscordNotificationField>(notifications.Count);
    foreach (var notification in notifications)
    {
        var arrivalTime = FormatArrivalTimeConcise(notification.ArrivalLocal);
        var remaining = FormatRemainingConcise(notification.Duration);
        var value = $"{arrivalTime} ({remaining})";
        
        fields.Add(new DiscordNotificationField(
            notification.SubmarineLabel,
            value,
            true // inline = true で横並び可能に
        ));
    }

    // オプション: バッチリマインダー
    if (_settings.EnableReminderCommand && notifications.Count > 0)
    {
        var firstArrival = notifications.Min(n => n.ArrivalLocal);
        var reminderCommand = FormatReminderCommand(
            _settings.ReminderChannelName ?? "#submarine",
            firstArrival,
            $"{notifications.Count}隻帰還開始"
        );
        fields.Add(new DiscordNotificationField(
            "リマインダー一括設定",
            $"`{reminderCommand}`",
            false
        ));
    }

    return new DiscordNotificationPayload(
        Title: title,
        Description: description,
        Color: UnderwayColor,
        Fields: fields,
        Footer: null);
}
```

**削減効果**:
- 各潜水艦: 3行 → 1行（67%削減）
- 合計: 12行 → 4-5行（リマインダー含む）

---

#### 2.3 ユーティリティメソッド（新規）

```csharp
/// <summary>
/// 帰還時刻を簡潔にフォーマット（月/日 時:分）
/// </summary>
private static string FormatArrivalTimeConcise(DateTime arrivalLocal)
{
    return arrivalLocal.ToString("M/d HH:mm", CultureInfo.CurrentCulture);
}

/// <summary>
/// 残り時間を簡潔にフォーマット（12h, 30m, 12.5h）
/// </summary>
private static string FormatRemainingConcise(TimeSpan? duration)
{
    if (duration is null || duration.Value <= TimeSpan.Zero)
    {
        return "0m";
    }

    var span = duration.Value;

    // 1時間未満: 分単位
    if (span.TotalHours < 1)
    {
        return $"{(int)Math.Ceiling(span.TotalMinutes)}m";
    }

    // 30分単位で小数点表示
    var hours = span.TotalHours;
    var roundedHours = Math.Round(hours * 2) / 2; // 0.5刻み
    
    if (roundedHours == Math.Floor(roundedHours))
    {
        return $"{(int)roundedHours}h";
    }
    else
    {
        return $"{roundedHours:F1}h".Replace(".0", "");
    }
}

/// <summary>
/// Discord Reminder Bot用のコマンドを生成
/// </summary>
/// <param name="channelName">リマインダーを送信するチャンネル名（#付き）</param>
/// <param name="arrivalTime">帰還時刻</param>
/// <param name="message">リマインダーメッセージ</param>
/// <returns>Reminder Botのコマンド文字列</returns>
private static string FormatReminderCommand(
    string channelName, 
    DateTime arrivalTime, 
    string message)
{
    // Reminder Bot のコマンド形式: /remind <channel> <time> <message>
    // 時刻フォーマット: MM/dd HH:mm または yyyy-MM-dd HH:mm
    var timeStr = arrivalTime.ToString("M/d HH:mm", CultureInfo.InvariantCulture);
    
    return $"/remind {channelName} {timeStr} {message}";
}
```

---

### 3. NotificationSettings の変更

**責務**: リマインダーコマンド機能の設定を追加

```csharp
public sealed class NotificationSettings
{
    // 既存プロパティ
    public bool EnableDiscord { get; set; }
    public string? DiscordWebhookUrl { get; set; }
    public bool EnableNotion { get; set; }
    public string? NotionWebhookUrl { get; set; }

    // Deprecated: 読み込むが使用しない
    [Obsolete("Completed notifications are no longer sent")]
    public bool NotifyVoyageCompleted { get; set; }

    // 既存（維持）
    public bool NotifyVoyageUnderway { get; set; } = true;

    // 新規追加
    public bool EnableReminderCommand { get; set; } = false;
    public string? ReminderChannelName { get; set; } = "#submarine";

    // ... 既存プロパティ続く ...
}
```

---

### 4. UI の変更

**責務**: 設定フォームからCompleted通知設定を削除、リマインダー設定を追加

```csharp
// NotificationMonitorWindowRenderer.SettingsLayout.cs

private void DrawNotificationTypeSettings()
{
    // 出航通知（維持）
    ImGui.Checkbox("出航時に通知", ref this.editingSettings.NotifyVoyageUnderway);
    
    // 帰航通知（削除または非表示）
    // ImGui.Checkbox("帰港時に通知", ref this.editingSettings.NotifyVoyageCompleted);
    // ↑ このチェックボックスを削除
    
    ImGui.Spacing();
    
    // リマインダー設定（新規）
    if (ImGui.CollapsingHeader("リマインダー連携（オプション）"))
    {
        ImGui.Checkbox("リマインダーコマンドを含める", 
            ref this.editingSettings.EnableReminderCommand);
        
        if (this.editingSettings.EnableReminderCommand)
        {
            ImGui.Indent();
            ImGui.TextWrapped(
                "通知にDiscord Reminder Botのコマンドを追加します。" +
                "コマンドをコピペしてDiscordで実行すると、帰還時刻にリマインダーが送信されます。");
            
            ImGui.Text("チャンネル名:");
            ImGui.SameLine();
            ImGui.SetNextItemWidth(200);
            ImGui.InputText("##reminderChannel", 
                this.reminderChannelBuffer, 
                (uint)this.reminderChannelBuffer.Length);
            
            ImGui.TextDisabled("例: #submarine");
            ImGui.Unindent();
        }
    }
}
```

---

## データフロー設計

### 通知送信フロー（変更後）

```
潜水艦状態変更イベント
  ↓
VoyageCompletionProjection
  ↓
  [Status判定]
    ├─ Completed → スキップ（ログのみ）✓
    ├─ Underway → 通知作成 ✓
    └─ その他 → スキップ
  ↓
NotificationCoordinator
  ↓
VoyageNotificationFormatter
  ├─ CreateDiscordPayload()（簡素化）
  └─ FormatReminderCommand()（新規）
  ↓
DiscordNotificationBatcher
  ↓
Discord Webhook
```

---

## UI/UX 設計

### Discord通知のレイアウト

#### 単体通知（最小版）

```
┌──────────────────────────────────┐
│ 🚢 Sub-1 出航                    │
│ Sea of Ash 1                     │
│                                  │
│ 📅 帰還予定                      │
│ 10/26(土) 14:30 (12h)           │
└──────────────────────────────────┘
```

#### 単体通知（リマインダー付き）

```
┌──────────────────────────────────┐
│ 🚢 Sub-1 出航                    │
│ Sea of Ash 1                     │
│                                  │
│ 📅 帰還予定                      │
│ 10/26(土) 14:30 (12h)           │
│                                  │
│ ⏰ リマインダー設定              │
│ /remind #submarine 10/26 14:30   │
│ Sub-1が帰還                      │
└──────────────────────────────────┘
```

---

#### バッチ通知（4隻、最小版）

```
┌──────────────────────────────────┐
│ 🚢 Mona - 4隻出航                │
│                                  │
│ Sub-1  10/26 14:30 (12h)         │
│ Sub-2  10/26 15:00 (12.5h)       │
│ Sub-3  10/26 16:30 (14h)         │
│ Sub-4  10/26 18:00 (15.5h)       │
└──────────────────────────────────┘
```

#### バッチ通知（リマインダー付き）

```
┌──────────────────────────────────┐
│ 🚢 Mona - 4隻出航                │
│                                  │
│ Sub-1  10/26 14:30 (12h)         │
│ Sub-2  10/26 15:00 (12.5h)       │
│ Sub-3  10/26 16:30 (14h)         │
│ Sub-4  10/26 18:00 (15.5h)       │
│                                  │
│ ⏰ リマインダー一括設定          │
│ /remind #submarine 10/26 14:30   │
│ 4隻帰還開始                      │
└──────────────────────────────────┘
```

**注**: 絵文字（🚢📅⏰）はオプション（設定で有効化）

---

## 詳細設計

### FormatReminderCommand() の実装

```csharp
/// <summary>
/// Discord Reminder Bot用のコマンド文字列を生成
/// </summary>
/// <param name="settings">通知設定</param>
/// <param name="arrivalTime">帰還時刻（ローカル時刻）</param>
/// <param name="message">リマインダーメッセージ</param>
/// <returns>Reminder Botコマンド</returns>
private static string FormatReminderCommand(
    NotificationSettings settings,
    DateTime arrivalTime,
    string message)
{
    var channelName = settings.ReminderChannelName ?? "#submarine";
    
    // チャンネル名が#で始まらない場合は追加
    if (!channelName.StartsWith("#"))
    {
        channelName = "#" + channelName;
    }

    // Reminder Bot の時刻フォーマット
    // サポート形式: "MM/dd HH:mm", "yyyy-MM-dd HH:mm", "in 12 hours"
    var timeStr = arrivalTime.ToString("M/d HH:mm", CultureInfo.InvariantCulture);

    // メッセージをサニタイズ（改行、特殊文字を削除）
    var sanitizedMessage = SanitizeReminderMessage(message);

    return $"/remind {channelName} {timeStr} {sanitizedMessage}";
}

/// <summary>
/// リマインダーメッセージをサニタイズ
/// </summary>
private static string SanitizeReminderMessage(string message)
{
    // 改行を空白に変換
    var sanitized = message.Replace("\n", " ").Replace("\r", " ");
    
    // 連続する空白を1つに
    while (sanitized.Contains("  "))
    {
        sanitized = sanitized.Replace("  ", " ");
    }
    
    // 最大100文字に制限
    if (sanitized.Length > 100)
    {
        sanitized = sanitized.Substring(0, 97) + "...";
    }
    
    return sanitized.Trim();
}
```

---

### FormatRemainingConcise() の実装

```csharp
/// <summary>
/// 残り時間を簡潔にフォーマット（12h, 30m, 12.5h）
/// </summary>
private static string FormatRemainingConcise(TimeSpan? duration)
{
    if (duration is null || duration.Value <= TimeSpan.Zero)
    {
        return "0m";
    }

    var span = duration.Value;

    // 異常値チェック
    if (span > TimeSpan.FromDays(14))
    {
        return "14d+";
    }

    // 1時間未満: 分単位
    if (span.TotalHours < 1)
    {
        var minutes = (int)Math.Ceiling(span.TotalMinutes);
        return $"{minutes}m";
    }

    // 1時間以上: 時間単位（0.5刻み）
    var hours = span.TotalHours;
    var roundedHours = Math.Round(hours * 2) / 2; // 0.5刻み

    // 整数時間
    if (Math.Abs(roundedHours - Math.Floor(roundedHours)) < 0.01)
    {
        return $"{(int)roundedHours}h";
    }
    
    // 小数点（.5のみ）
    return $"{roundedHours:F1}h";
}
```

**テストケース**:
- 25分 → "25m"
- 45分 → "45m"
- 1時間 → "1h"
- 1時間15分 → "1h" (切り捨て)
- 1時間30分 → "1.5h"
- 12時間 → "12h"
- 12時間40分 → "12.5h" (四捨五入)

---

## 設定マイグレーション

### 既存設定の扱い

```csharp
// 設定読み込み時
public async Task<NotificationSettings> LoadAsync()
{
    var settings = await ReadFromFileAsync();
    
    // マイグレーション: NotifyVoyageCompleted を無視
    if (settings.NotifyVoyageCompleted)
    {
        _log.Log(LogLevel.Info, 
            "[Settings] NotifyVoyageCompleted is deprecated and will be ignored. " +
            "Completed notifications are no longer sent.");
    }
    
    return settings;
}
```

**後方互換性**:
- 既存の設定ファイルは正常に読み込める
- `NotifyVoyageCompleted` は読み込むが無視
- 次回保存時に自動的に削除（またはdeprecatedフラグ付き）

---

## パフォーマンス設計

### メモリ使用量

**Before**:
```
単体通知: ~400 bytes (6 fields)
バッチ通知: ~1600 bytes (12 lines)
```

**After**:
```
単体通知: ~150 bytes (1-2 fields) - 62%削減
バッチ通知: ~600 bytes (4-5 lines) - 62%削減
```

### 処理時間

**変更なし**: フォーマット処理は軽量（< 1ms）

---

## テスト設計

### 単体テスト

#### VoyageNotificationFormatterTests.cs（新規/拡張）

```csharp
public class VoyageNotificationFormatterTests
{
    [Fact]
    public void CreateDiscordPayload_Underway_ReturnsOptimizedFormat()
    {
        // Arrange
        var notification = CreateUnderwayNotification();
        var formatter = new VoyageNotificationFormatter();

        // Act
        var payload = formatter.CreateDiscordPayload(notification);

        // Assert
        Assert.Equal("Sub-1 出航", payload.Title);
        Assert.Equal("Sea of Ash 1", payload.Description);
        Assert.Single(payload.Fields); // フィールドは1つのみ
        Assert.Equal("帰還予定", payload.Fields[0].Name);
        Assert.Contains("(", payload.Fields[0].Value); // 残り時間を含む
    }

    [Fact]
    public void CreateDiscordPayload_Completed_ThrowsException()
    {
        // Arrange
        var notification = CreateCompletedNotification();
        var formatter = new VoyageNotificationFormatter();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            formatter.CreateDiscordPayload(notification));
    }

    [Fact]
    public void FormatRemainingConcise_Various_ReturnsCorrectFormat()
    {
        // 25分
        Assert.Equal("25m", FormatRemainingConcise(TimeSpan.FromMinutes(25)));
        
        // 1時間
        Assert.Equal("1h", FormatRemainingConcise(TimeSpan.FromHours(1)));
        
        // 1.5時間
        Assert.Equal("1.5h", FormatRemainingConcise(TimeSpan.FromMinutes(90)));
        
        // 12時間
        Assert.Equal("12h", FormatRemainingConcise(TimeSpan.FromHours(12)));
    }

    [Fact]
    public void FormatReminderCommand_ValidInput_ReturnsCorrectCommand()
    {
        // Arrange
        var settings = new NotificationSettings 
        { 
            ReminderChannelName = "#submarine" 
        };
        var arrivalTime = new DateTime(2025, 10, 26, 14, 30, 0);
        var message = "Sub-1が帰還";

        // Act
        var command = FormatReminderCommand(settings, arrivalTime, message);

        // Assert
        Assert.Equal("/remind #submarine 10/26 14:30 Sub-1が帰還", command);
    }
}
```

---

### E2Eテスト

#### notification-layout.spec.ts（拡張）

```typescript
test.describe('@notification Discord message optimization', () => {
  test('Underway notification has only 1 field', async ({ page }) => {
    // Arrange: Trigger Underway notification
    
    // Act: Check Discord webhook payload mock
    
    // Assert
    const fields = await getWebhookPayloadFields();
    expect(fields).toHaveLength(1);
    expect(fields[0].name).toBe('帰還予定');
  });

  test('Completed notification is not sent', async ({ page }) => {
    // Arrange: Trigger Completed event
    
    // Act: Wait for notification
    
    // Assert: No Discord webhook call
    const webhookCalls = await getWebhookCalls();
    const completedCalls = webhookCalls.filter(c => 
      c.payload.title.includes('帰港'));
    expect(completedCalls).toHaveLength(0);
  });

  test('Reminder command is included when enabled', async ({ page }) => {
    // Arrange: Enable reminder command
    await page.check('[data-testid="enable-reminder-command"]');
    await page.fill('[data-testid="reminder-channel"]', '#submarine');
    
    // Act: Trigger Underway notification
    
    // Assert
    const payload = await getLastWebhookPayload();
    expect(payload.fields).toContainEqual(
      expect.objectContaining({
        name: 'リマインダー設定',
        value: expect.stringContaining('/remind')
      })
    );
  });
});
```

---

## エラーハンドリング

### 異常系対応

```csharp
public DiscordNotificationPayload CreateDiscordPayload(VoyageNotification notification)
{
    // Underway以外は例外
    if (notification.Status != VoyageStatus.Underway)
    {
        throw new ArgumentException(
            $"Only Underway notifications are supported. Got: {notification.Status}",
            nameof(notification));
    }

    // 帰還時刻が不正
    if (notification.ArrivalLocal < DateTime.Now.AddMinutes(-5))
    {
        _log.Log(LogLevel.Warning, 
            $"[Notifications] Invalid arrival time for {notification.SubmarineLabel}: {notification.ArrivalLocal}");
        // 続行（エラーにしない）
    }

    // ... フォーマット処理 ...
}
```

---

## セキュリティ考慮事項

### 入力検証

```csharp
private static string FormatReminderCommand(/* ... */)
{
    // チャンネル名のバリデーション
    if (!string.IsNullOrEmpty(channelName))
    {
        // 危険な文字を除去
        channelName = channelName.Trim();
        if (!channelName.StartsWith("#"))
        {
            channelName = "#" + channelName;
        }
        
        // 英数字とハイフン、アンダースコアのみ許可
        var sanitized = new string(channelName
            .Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '#')
            .ToArray());
        
        channelName = sanitized;
    }

    // メッセージのサニタイズ（既出）
    var sanitizedMessage = SanitizeReminderMessage(message);

    return $"/remind {channelName} {timeStr} {sanitizedMessage}";
}
```

---

## トレードオフ分析

### 設計選択肢の比較

| 観点 | 案A: フォーマット変更のみ | 案B: 通知ロジック変更 | 案C: 採用（両方） |
|------|------------------------|-------------------|-----------------|
| Completed通知停止 | ✗ | ✓ | ✓ |
| 簡素化 | ✓ | ✗ | ✓ |
| 実装複雑度 | 低 | 低 | 中 |
| テスト範囲 | 狭い | 狭い | 中 |
| ユーザー影響 | 小 | 大 | 大 |

**選択理由**: 案C（両方実装）が最も要件を満たす。

---

## 移行計画

### Phase 1: Completed通知停止
1. `VoyageCompletionProjection` でCompleted通知をフィルタ
2. ログに記録（デバッグ用）
3. テスト更新

### Phase 2: フォーマット簡素化
1. `VoyageNotificationFormatter` を修正
2. 新しいユーティリティメソッド追加
3. 単体テスト追加

### Phase 3: リマインダーコマンド（オプション）
1. `FormatReminderCommand()` 実装
2. 設定UI追加
3. E2Eテスト追加

---

## 設定UI設計

### リマインダー設定セクション

```
┌────────────────────────────────────────┐
│ リマインダー連携（オプション）          │
├────────────────────────────────────────┤
│ ☑ リマインダーコマンドを含める         │
│                                        │
│ 通知にDiscord Reminder Botのコマンド   │
│ を追加します。コマンドをコピペして     │
│ Discordで実行すると、帰還時刻に        │
│ リマインダーが送信されます。           │
│                                        │
│ チャンネル名: [#submarine        ]    │
│               例: #submarine           │
└────────────────────────────────────────┘
```

---

## ロールバック戦略

### フィーチャーフラグ

```csharp
public sealed class NotificationSettings
{
    // フィーチャーフラグ（緊急時のロールバック用）
    public bool UseOptimizedDiscordFormat { get; set; } = true;

    // 旧フォーマットを使用する場合
    public bool UseLegacyCompletedNotifications { get; set; } = false;
}

// VoyageCompletionProjection
private void ProcessVoyageStateChange(/* ... */)
{
    if (currentStatus == VoyageStatus.Completed)
    {
        // レガシーフラグでロールバック可能
        if (_settings.UseLegacyCompletedNotifications)
        {
            SendCompletedNotification(/* ... */);
        }
        return;
    }
    // ...
}
```

---

## パフォーマンス影響分析

### Before vs After

| 指標 | Before | After | 変化 |
|------|--------|-------|------|
| 単体通知サイズ | ~400 bytes | ~150 bytes | -62% |
| バッチ通知サイズ | ~1600 bytes | ~600 bytes | -62% |
| 通知頻度 | 2回/voyage | 1回/voyage | -50% |
| Discord API呼び出し | 2回 | 1回 | -50% |
| ネットワーク帯域 | 2KB/voyage | 0.15KB/voyage | -92% |

**結論**: パフォーマンスが大幅に向上（負荷削減）

---

## リスク軽減策

### 主要リスク

#### R-1: ユーザーがCompleted通知を期待
**軽減策**:
- リリースノートで明確に説明
- フィーチャーフラグで緊急復活可能に
- フィードバック収集期間を設ける

#### R-2: Reminder Botが利用できない
**軽減策**:
- リマインダー機能はオプション（デフォルトOFF）
- 通知の本質機能に影響なし
- 代替案（他のBot）も使用可能

#### R-3: テスト更新漏れ
**軽減策**:
- 既存テストを全件レビュー
- Completed通知の期待を削除
- 新しいフォーマットでテスト追加

---

**作成日**: 2025-10-26  
**レビュアー**: AI Assistant  
**承認状態**: 設計レビュー待ち  
**関連Phase**: Phase 13候補  
**次のステップ**: `/kiro:spec-tasks discord-message-optimization`

