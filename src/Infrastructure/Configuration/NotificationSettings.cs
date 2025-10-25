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

    /// <summary>
    /// [DEPRECATED] Completed notifications are no longer sent as of Phase 13.
    /// This property is kept for backward compatibility but is no longer used.
    /// Users now receive only Underway notifications with arrival time.
    /// </summary>
    [Obsolete("Completed notifications are no longer sent. Only Underway notifications are supported.")]
    public bool NotifyVoyageCompleted { get; set; } = true;

    public bool NotifyVoyageUnderway { get; set; } = true;

    public NotificationSettings Clone() => (NotificationSettings)this.MemberwiseClone();
}
