// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/NotificationWebhookValidatorTests.cs
// 通知チャネル URL バリデーションの挙動を検証するテストです
// 保存前に誤った URL を弾けるか確認し、UI バリデーションの信頼性を担保するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.ChannelCards.cs, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/NotificationMonitorWindowRenderer.SavePanel.cs

using XIVSubmarinesRewrite.Presentation.Rendering;
using Xunit;

public sealed class NotificationWebhookValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DiscordValidator_RejectsMissingUrl(string? value)
    {
        var result = NotificationWebhookValidator.ValidateDiscord(true, value ?? string.Empty);
        Assert.False(result.IsValid);
        Assert.Equal("URL を入力してください。", result.ErrorMessage);
    }

    [Theory]
    [InlineData("http://discord.com/webhook")]
    [InlineData("ftp://discord.com/webhook")]
    public void DiscordValidator_RequiresHttps(string url)
    {
        var result = NotificationWebhookValidator.ValidateDiscord(true, url);
        Assert.False(result.IsValid);
        Assert.Equal("https:// から始まる URL を入力してください。", result.ErrorMessage);
    }

    [Fact]
    public void DiscordValidator_RequiresDiscordDomain()
    {
        var result = NotificationWebhookValidator.ValidateDiscord(true, "https://example.com/hook");
        Assert.False(result.IsValid);
        Assert.Equal("Discord Webhook の URL を入力してください。", result.ErrorMessage);
    }

    [Fact]
    public void DiscordValidator_AllowsValidWebhook()
    {
        var result = NotificationWebhookValidator.ValidateDiscord(true, "https://discord.com/api/webhooks/123");
        Assert.True(result.IsValid);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void DiscordValidator_SkipsWhenDisabled()
    {
        var result = NotificationWebhookValidator.ValidateDiscord(false, string.Empty);
        Assert.True(result.IsValid);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void NotionValidator_AllowsHttpsAnyDomain()
    {
        var result = NotificationWebhookValidator.ValidateNotion(true, "https://hooks.zapier.com/abc");
        Assert.True(result.IsValid);
        Assert.Null(result.ErrorMessage);
    }
}
