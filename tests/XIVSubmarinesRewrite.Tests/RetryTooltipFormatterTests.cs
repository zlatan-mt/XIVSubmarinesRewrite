namespace XIVSubmarinesRewrite.Tests;

using System;
using System.Linq;
using Xunit;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Presentation.Rendering;

/// <summary>
/// RetryTooltipFormatter の単体テスト
/// </summary>
public class RetryTooltipFormatterTests
{
    private static NotificationEnvelope CreateTestEnvelope(string submarineName = "Sub-1")
    {
        var submarineId = new SubmarineId(0x1UL, 0);
        var voyageId = VoyageId.Create(submarineId, Guid.NewGuid());
        return NotificationEnvelope.Create(
            characterId: 0x1UL,
            characterName: "Character",
            worldName: "World",
            submarineId: submarineId,
            submarineName: submarineName,
            routeId: "Route-1",
            voyageId: voyageId,
            departure: DateTime.UtcNow.AddHours(-6),
            arrival: DateTime.UtcNow,
            status: VoyageStatus.Underway,
            confidence: SnapshotConfidence.Merged,
            forceImmediate: false);
    }

    [Fact]
    public void Format_WithRetryHistory_ReturnsCorrectContent()
    {
        // Arrange
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.Pending,
            AttemptCount: 2,
            NextAttemptAtUtc: new DateTime(2025, 10, 26, 14, 23, 53, DateTimeKind.Utc),
            LastAttemptAtUtc: new DateTime(2025, 10, 26, 14, 23, 45, DateTimeKind.Utc),
            LastError: null
        );
        var now = new DateTime(2025, 10, 26, 14, 23, 45, DateTimeKind.Utc);

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.NotEmpty(content.Lines);

        // ヘッダー
        Assert.Contains(content.Lines, l => l.Text.Contains("リトライ情報") && !l.Text.Contains("デッドレター"));

        // 試行回数
        Assert.Contains(content.Lines, l => l.Text.Contains("試行回数: 2回"));

        // 最終試行時刻
        Assert.Contains(content.Lines, l => l.Text.Contains("最終試行: 2025-10-26 14:23:45"));

        // 次回試行
        Assert.Contains(content.Lines, l => l.Text.Contains("次回試行:") && l.Text.Contains("秒後"));
    }

    [Fact]
    public void Format_WithDeadLetter_ShowsErrorInfo()
    {
        // Arrange
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.DeadLetter,
            AttemptCount: 3,
            NextAttemptAtUtc: DateTime.MaxValue,
            LastAttemptAtUtc: new DateTime(2025, 10, 26, 14, 20, 0, DateTimeKind.Utc),
            LastError: "HTTP 500 Internal Server Error"
        );
        var now = DateTime.UtcNow;

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);

        // デッドレターヘッダー
        Assert.Contains(content.Lines, l => l.Text.Contains("デッドレター"));

        // 上限到達
        Assert.Contains(content.Lines, l => l.Text.Contains("上限到達"));

        // エラーメッセージ
        Assert.Contains(content.Lines, l => l.Text.Contains("HTTP 500"));

        // エラー色が適用されている
        Assert.Contains(content.Lines, l => l.Color.HasValue && l.Color.Value == UiTheme.ErrorText);
    }

    [Fact]
    public void Format_WithNoHistory_ShowsNoHistoryMessage()
    {
        // Arrange
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.Pending,
            AttemptCount: 0,
            NextAttemptAtUtc: DateTime.UtcNow.AddSeconds(5),
            LastAttemptAtUtc: null,
            LastError: null
        );
        var now = DateTime.UtcNow;

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.Contains(content.Lines, l => l.Text.Contains("試行履歴なし"));
    }

    [Fact]
    public void Format_WithNextAttemptScheduled_ShowsCountdown()
    {
        // Arrange
        var now = new DateTime(2025, 10, 26, 14, 23, 45, DateTimeKind.Utc);
        var nextAttempt = now.AddSeconds(8);
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.Pending,
            AttemptCount: 1,
            NextAttemptAtUtc: nextAttempt,
            LastAttemptAtUtc: now.AddSeconds(-5),
            LastError: null
        );

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.Contains(content.Lines, l => l.Text.Contains("(8秒後)"));
    }

    [Fact]
    public void Format_WithoutLastAttemptTime_DoesNotShowLastAttempt()
    {
        // Arrange
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.Pending,
            AttemptCount: 0,
            NextAttemptAtUtc: DateTime.UtcNow.AddSeconds(5),
            LastAttemptAtUtc: null,
            LastError: null
        );
        var now = DateTime.UtcNow;

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.DoesNotContain(content.Lines, l => l.Text.Contains("最終試行:"));
    }

    [Fact]
    public void Format_DeadLetterWithoutError_DoesNotShowErrorSection()
    {
        // Arrange
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.DeadLetter,
            AttemptCount: 3,
            NextAttemptAtUtc: DateTime.MaxValue,
            LastAttemptAtUtc: DateTime.UtcNow,
            LastError: null
        );
        var now = DateTime.UtcNow;

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.DoesNotContain(content.Lines, l => l.Text.Contains("最終エラー:"));
    }

    [Fact]
    public void Format_WithLongErrorMessage_TruncatesError()
    {
        // Arrange
        var longError = new string('X', 250) + " error message";
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.DeadLetter,
            AttemptCount: 3,
            NextAttemptAtUtc: DateTime.MaxValue,
            LastAttemptAtUtc: DateTime.UtcNow,
            LastError: longError
        );
        var now = DateTime.UtcNow;

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        var errorLine = content.Lines.FirstOrDefault(l => l.Text.Contains("最終エラー:"));
        Assert.NotNull(errorLine);
        Assert.True(errorLine.Text.Length < longError.Length + 20); // "最終エラー: " + "..."
        Assert.Contains("...", errorLine.Text);
    }

    [Fact]
    public void Format_WithPastNextAttempt_ShowsZeroSeconds()
    {
        // Arrange
        var now = new DateTime(2025, 10, 26, 14, 23, 50, DateTimeKind.Utc);
        var nextAttempt = new DateTime(2025, 10, 26, 14, 23, 45, DateTimeKind.Utc); // 過去
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.Pending,
            AttemptCount: 1,
            NextAttemptAtUtc: nextAttempt,
            LastAttemptAtUtc: now.AddSeconds(-10),
            LastError: null
        );

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.Contains(content.Lines, l => l.Text.Contains("(0秒後)"));
    }

    [Fact]
    public void TooltipLine_Separator_IsSeparatorTrue()
    {
        // Arrange & Act
        var separator = TooltipLine.Separator;

        // Assert
        Assert.True(separator.IsSeparator);
        Assert.Empty(separator.Text);
        Assert.Null(separator.Color);
    }

    [Fact]
    public void Format_ContainsSeparators()
    {
        // Arrange
        var envelope = CreateTestEnvelope();
        var workItem = new NotificationWorkItemSnapshot(
            envelope,
            NotificationDeliveryState.Pending,
            AttemptCount: 1,
            NextAttemptAtUtc: DateTime.UtcNow.AddSeconds(5),
            LastAttemptAtUtc: DateTime.UtcNow.AddSeconds(-5),
            LastError: null
        );
        var now = DateTime.UtcNow;

        // Act
        var content = RetryTooltipFormatter.Format(workItem, now);

        // Assert
        Assert.NotNull(content);
        Assert.Contains(content.Lines, l => l.IsSeparator);
    }
}

