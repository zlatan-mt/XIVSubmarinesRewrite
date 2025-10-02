// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/DevNotificationPanelTests.cs
// 開発向け履歴モデルの初期値とシリアライズ品質を検証するテストです
// UiPreferences.DevPanelHistory のサイズを抑えつつ状態が保持されることを保証するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/UiPreferences.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/DevNotificationPanel.cs

using System;
using System.Text.Json;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using Xunit;

public sealed class DevNotificationPanelTests
{
    [Fact]
    public void DevHistory_DefaultsToEmpty()
    {
        var prefs = new UiPreferences();
        Assert.NotNull(prefs.DevHistory);
        Assert.Null(prefs.DevHistory.LastDeveloperTabToggleUtc);
        Assert.Null(prefs.DevHistory.LastForceNotifyToggleUtc);
        Assert.Null(prefs.DevHistory.LastManualTriggerUtc);
    }

    [Fact]
    public void DevHistory_SerializesCompactly()
    {
        var prefs = new UiPreferences
        {
            DevHistory = new UiPreferences.DevPanelHistory
            {
                LastDeveloperTabToggleUtc = DateTime.UnixEpoch,
                DeveloperToolsVisible = true,
                LastForceNotifyToggleUtc = DateTime.UnixEpoch.AddMinutes(1),
                ForceNotifyEnabled = true,
                LastManualTriggerUtc = DateTime.UnixEpoch.AddMinutes(2),
                LastManualTriggerSummary = "2 件の通知を再送しました。",
            },
        };

        var json = JsonSerializer.Serialize(prefs);
        Assert.True(json.Length < 320, "Dev history serialization should remain compact.");
        Assert.Contains("\"LastManualTriggerSummary\"", json);
    }
}
