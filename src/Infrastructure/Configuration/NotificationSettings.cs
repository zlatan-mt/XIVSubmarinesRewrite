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

    public double DiscordBatchWindowSeconds { get; set; } = 1.5;

    public int DeadLetterRetentionLimit { get; set; } = 64;

    public bool ForceNotifyUnderway { get; set; } = false;

    public bool NotifyVoyageCompleted { get; set; } = true;

    public bool NotifyVoyageUnderway { get; set; } = true;

    public NotificationSettings Clone() => (NotificationSettings)this.MemberwiseClone();
}
