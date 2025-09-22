namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System;

/// <summary>Stores user-configurable notification endpoints and preferences.</summary>
public sealed class NotificationSettings
{
    public bool EnableDiscord { get; set; } = false;

    public string? DiscordWebhookUrl { get; set; }
        = string.Empty;

    public bool EnableNotion { get; set; } = false;

    public string? NotionWebhookUrl { get; set; }
        = string.Empty;

    public double DiscordBatchWindowSeconds { get; set; } = 2.0;

    public int DeadLetterRetentionLimit { get; set; } = 64;

    public bool ForceNotifyUnderway { get; set; } = false;

    public NotificationSettings Clone() => (NotificationSettings)this.MemberwiseClone();
}
