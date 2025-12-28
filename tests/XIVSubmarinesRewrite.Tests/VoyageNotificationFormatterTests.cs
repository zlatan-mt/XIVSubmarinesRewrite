namespace XIVSubmarinesRewrite.Tests;

using System;
using System.Linq;
using Xunit;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>
/// VoyageNotificationFormatter の単体テスト（Phase 13対応）
/// </summary>
public class VoyageNotificationFormatterTests
{
    private readonly NotificationSettings settings = new NotificationSettings
    {
        EnableReminderCommand = false,
        ReminderChannelName = "#submarine"
    };

    [Fact]
    public void CreateDiscordPayload_Underway_ReturnsOptimizedFormat()
    {
        // Arrange
        var formatter = new VoyageNotificationFormatter(settings);
        var notification = CreateUnderwayNotification("Sub-1", "Sea of Ash 1");

        // Act
        var payload = formatter.CreateDiscordPayload(notification);

        // Assert
        Assert.Equal("Sub-1 出航", payload.Title);
        Assert.Equal("Sea of Ash 1", payload.Description);
        Assert.Single(payload.Fields); // 1フィールドのみ
        Assert.Equal("帰還予定", payload.Fields[0].Name);
        Assert.Contains("(", payload.Fields[0].Value); // 残り時間を含む
        Assert.Contains("h)", payload.Fields[0].Value); // 時間単位
    }

    [Fact]
    public void CreateDiscordPayload_Completed_ThrowsException()
    {
        // Arrange
        var formatter = new VoyageNotificationFormatter(settings);
        var notification = CreateCompletedNotification("Sub-1");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            formatter.CreateDiscordPayload(notification));
        Assert.Contains("Only Underway notifications are supported", exception.Message);
        Assert.Contains("Phase 13", exception.Message);
    }

    [Fact]
    public void CreateDiscordPayload_WithReminderEnabled_IncludesReminderField()
    {
        // Arrange
        var settingsWithReminder = new NotificationSettings
        {
            EnableReminderCommand = true,
            ReminderChannelName = "#submarine"
        };
        var formatter = new VoyageNotificationFormatter(settingsWithReminder);
        var notification = CreateUnderwayNotification("Sub-1", "Route-A");

        // Act
        var payload = formatter.CreateDiscordPayload(notification);

        // Assert
        Assert.Equal(2, payload.Fields.Count); // 帰還予定 + リマインダー
        
        var reminderField = payload.Fields.FirstOrDefault(f => f.Name == "リマインダー設定");
        Assert.NotNull(reminderField);
        Assert.Contains("/remind", reminderField.Value);
        Assert.Contains("#submarine", reminderField.Value);
        Assert.Contains("Sub-1が帰還", reminderField.Value);
    }

    [Fact]
    public void CreateDiscordPayload_WithReminderDisabled_NoReminderField()
    {
        // Arrange
        var settingsNoReminder = new NotificationSettings
        {
            EnableReminderCommand = false
        };
        var formatter = new VoyageNotificationFormatter(settingsNoReminder);
        var notification = CreateUnderwayNotification("Sub-1", "Route-A");

        // Act
        var payload = formatter.CreateDiscordPayload(notification);

        // Assert
        Assert.Single(payload.Fields); // 帰還予定のみ
        Assert.DoesNotContain(payload.Fields, f => f.Name == "リマインダー設定");
    }

    [Fact]
    public void CreateDiscordBatchPayload_Underway_ReturnsOptimizedFormat()
    {
        // Arrange
        var formatter = new VoyageNotificationFormatter(settings);
        var notifications = new[]
        {
            CreateUnderwayNotification("Sub-1", "Route-A"),
            CreateUnderwayNotification("Sub-2", "Route-B"),
            CreateUnderwayNotification("Sub-3", "Route-C"),
            CreateUnderwayNotification("Sub-4", "Route-D")
        };

        // Act
        var payload = formatter.CreateDiscordBatchPayload(
            VoyageStatus.Underway,
            "Mona",
            notifications
        );

        // Assert
        Assert.Equal("Mona - 4隻出航", payload.Title);
        // Phase 13: Descriptionにコードブロック形式でバッチ一覧が入る
        Assert.NotEmpty(payload.Description);
        Assert.Contains("帰還時間:", payload.Description);
        Assert.Contains("```", payload.Description); // コードブロック
        Assert.Contains("Sub-1", payload.Description);
        Assert.Contains("Sub-4", payload.Description);
        // リマインダー無効時はFieldsは空
        Assert.Empty(payload.Fields);
    }

    [Fact]
    public void CreateDiscordBatchPayload_Completed_ThrowsException()
    {
        // Arrange
        var formatter = new VoyageNotificationFormatter(settings);
        var notifications = new[] { CreateCompletedNotification("Sub-1") };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            formatter.CreateDiscordBatchPayload(VoyageStatus.Completed, "Mona", notifications));
        Assert.Contains("Only Underway batch notifications are supported", exception.Message);
    }

    [Fact]
    public void CreateDiscordBatchPayload_WithReminder_IncludesBatchReminder()
    {
        // Arrange
        var settingsWithReminder = new NotificationSettings
        {
            EnableReminderCommand = true,
            ReminderChannelName = "#submarine"
        };
        var formatter = new VoyageNotificationFormatter(settingsWithReminder);
        var notifications = new[]
        {
            CreateUnderwayNotification("Sub-1", "Route-A", DateTime.UtcNow.AddHours(12)),
            CreateUnderwayNotification("Sub-2", "Route-B", DateTime.UtcNow.AddHours(13)),
            CreateUnderwayNotification("Sub-3", "Route-C", DateTime.UtcNow.AddHours(14)),
            CreateUnderwayNotification("Sub-4", "Route-D", DateTime.UtcNow.AddHours(15))
        };

        // Act
        var payload = formatter.CreateDiscordBatchPayload(
            VoyageStatus.Underway,
            "Mona",
            notifications
        );

        // Assert
        // Phase 13: Descriptionにバッチ一覧、Fieldsはリマインダーのみ
        Assert.NotEmpty(payload.Description);
        Assert.Contains("Sub-1", payload.Description);
        Assert.Single(payload.Fields); // リマインダーフィールドのみ

        var reminderField = payload.Fields[0];
        Assert.Equal("リマインダー一括設定", reminderField.Name);
        Assert.Contains("/remind", reminderField.Value);
        Assert.Contains("4隻帰還開始", reminderField.Value);
        Assert.False(reminderField.Inline); // リマインダーは全幅表示
    }

    // ヘルパーメソッド
    private static VoyageNotification CreateUnderwayNotification(
        string submarineName,
        string routeDisplay,
        DateTime? arrivalUtc = null)
    {
        var arrival = arrivalUtc ?? DateTime.UtcNow.AddHours(12);
        var departure = arrival.AddHours(-12);
        var submarineId = new SubmarineId(0x1UL, 0);
        var voyageId = VoyageId.Create(submarineId, Guid.NewGuid());
        var hash = $"hash-{submarineName}-underway";

        return new VoyageNotification(
            CharacterId: 0x1UL,
            CharacterLabel: "Character",
            CharacterName: "Character",
            WorldName: "World",
            SubmarineId: submarineId,
            SubmarineLabel: submarineName,
            SubmarineName: submarineName,
            RouteId: routeDisplay,
            RouteDisplay: routeDisplay,
            VoyageId: voyageId,
            DepartureUtc: departure,
            ArrivalUtc: arrival,
            ArrivalLocal: arrival.ToLocalTime(),
            Duration: null,
            Status: VoyageStatus.Underway,
            Confidence: SnapshotConfidence.Merged,
            HashKey: hash,
            HashKeyShort: hash.Length > 8 ? hash[..8] : hash);
    }

    private static VoyageNotification CreateCompletedNotification(string submarineName)
    {
        var arrival = DateTime.UtcNow.AddHours(-1);
        var departure = arrival.AddHours(-12);
        var submarineId = new SubmarineId(0x1UL, 0);
        var voyageId = VoyageId.Create(submarineId, Guid.NewGuid());
        var hash = $"hash-{submarineName}-completed";

        return new VoyageNotification(
            CharacterId: 0x1UL,
            CharacterLabel: "Character",
            CharacterName: "Character",
            WorldName: "World",
            SubmarineId: submarineId,
            SubmarineLabel: submarineName,
            SubmarineName: submarineName,
            RouteId: "Route",
            RouteDisplay: "Route Display",
            VoyageId: voyageId,
            DepartureUtc: departure,
            ArrivalUtc: arrival,
            ArrivalLocal: arrival.ToLocalTime(),
            Duration: arrival - departure,
            Status: VoyageStatus.Completed,
            Confidence: SnapshotConfidence.Merged,
            HashKey: hash,
            HashKeyShort: hash.Length > 8 ? hash[..8] : hash);
    }
}

