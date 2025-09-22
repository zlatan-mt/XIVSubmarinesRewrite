// apps/XIVSubmarinesRewrite/src/Integrations/Notifications/IDiscordClient.cs
// Discord 配信実装とのインターフェースを定義します
// 異なる送信形態でも統一的に通知を呼び出せるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Integrations/Notifications/DiscordWebhookClient.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs

namespace XIVSubmarinesRewrite.Integrations.Notifications;

using System;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Abstraction over Discord delivery (webhook or bot).</summary>
public interface IDiscordClient
{
    ValueTask SendAlarmAsync(Alarm alarm, CancellationToken cancellationToken = default);

    ValueTask SendVoyageCompletionAsync(VoyageNotification notification, DiscordNotificationPayload payload, CancellationToken cancellationToken = default);

    ValueTask SendVoyageBatchAsync(string username, DiscordNotificationPayload payload, DateTime timestampUtc, CancellationToken cancellationToken = default);
}
