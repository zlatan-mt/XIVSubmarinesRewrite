// apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs
// 通知チャネルへの配信制御と Discord 集約を司るサービスです
// 各チャンネルへ一貫したメッセージを送り、無駄な送信を避けるため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/DiscordNotificationBatcher.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Routes/RouteCatalog.cs

namespace XIVSubmarinesRewrite.Application.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Infrastructure.Routes;
using XIVSubmarinesRewrite.Integrations.Notifications;

/// <summary>Routes alarm events to the configured notification channels.</summary>
public sealed class NotificationCoordinator
{
    private readonly IDiscordClient discordClient;
    private readonly INotionClient notionClient;
    private readonly VoyageNotificationFormatter formatter;
    private readonly RouteCatalog routeCatalog;
    private readonly DiscordNotificationBatcher discordBatcher;
    private readonly DiscordCycleNotificationAggregator discordCycleAggregator;
    private readonly ILogSink log;

    public NotificationCoordinator(
        IDiscordClient discordClient,
        INotionClient notionClient,
        VoyageNotificationFormatter formatter,
        RouteCatalog routeCatalog,
        DiscordNotificationBatcher discordBatcher,
        ILogSink log)
    {
        this.discordClient = discordClient;
        this.notionClient = notionClient;
        this.formatter = formatter;
        this.routeCatalog = routeCatalog;
        this.discordBatcher = discordBatcher;
        this.log = log;
        this.discordCycleAggregator = new DiscordCycleNotificationAggregator(formatter, log);
    }

    public async ValueTask PublishAlarmAsync(Alarm alarm, CancellationToken cancellationToken = default)
    {
        await this.discordClient.SendAlarmAsync(alarm, cancellationToken).ConfigureAwait(false);
        await this.notionClient.RecordAlarmAsync(alarm, cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask PublishVoyageCompletionAsync(NotificationEnvelope envelope, CancellationToken cancellationToken = default)
    {
        var arrivalUtc = envelope.Arrival;
        var arrivalLocal = arrivalUtc.Kind == DateTimeKind.Utc
            ? TimeZoneInfo.ConvertTimeFromUtc(arrivalUtc, TimeZoneInfo.Local)
            : arrivalUtc.ToLocalTime();

        DateTime? departureUtc = envelope.Departure;
        if (departureUtc.HasValue && departureUtc.Value.Kind == DateTimeKind.Local)
        {
            departureUtc = departureUtc.Value.ToUniversalTime();
        }

        TimeSpan? duration = null;
        if (departureUtc.HasValue)
        {
            var delta = arrivalUtc - departureUtc.Value;
            if (delta > TimeSpan.Zero && delta <= TimeSpan.FromHours(72))
            {
                duration = delta;
            }
        }

        var characterLabel = BuildCharacterLabel(envelope);
        var submarineLabel = BuildSubmarineLabel(envelope);
        var hashShort = envelope.HashKey.Length >= 8 ? envelope.HashKey[..8] : envelope.HashKey;

        var routeRaw = envelope.RouteId;
        var routeDisplay = this.routeCatalog.FormatRoute(routeRaw);

        var notification = new VoyageNotification(
            envelope.CharacterId,
            characterLabel,
            envelope.CharacterName,
            envelope.WorldName,
            envelope.SubmarineId,
            submarineLabel,
            envelope.SubmarineName,
            routeRaw,
            string.IsNullOrWhiteSpace(routeDisplay) ? routeRaw : routeDisplay,
            envelope.VoyageId,
            departureUtc,
            arrivalUtc,
            arrivalLocal,
            duration,
            envelope.Status,
            envelope.Confidence,
            envelope.HashKey,
            hashShort);

        var notionPayload = this.formatter.CreateNotionPayload(notification);
        await this.notionClient.RecordVoyageCompletionAsync(notification, notionPayload, cancellationToken).ConfigureAwait(false);

        // ForceImmediate でも通常の Process を使用（状態更新は維持、リセットのみ制御）
        var decision = this.discordCycleAggregator.Process(notification, envelope.ForceImmediate);
        
        // ForceImmediate の場合は Suppress を Forward にフォールバック（即時送信を保証）
        if (envelope.ForceImmediate && decision.IsSuppressed)
        {
            decision = DiscordCycleNotificationAggregator.Decision.Forward();
            this.log.Log(LogLevel.Debug, "[Notifications] ForceImmediate bypassed cycle suppression for immediate delivery. Route: Forward");
        }
        
        // ForceImmediate の経路を明示的にログ出力
        if (envelope.ForceImmediate)
        {
            var route = decision.Aggregate is not null ? "Direct" : decision.IsSuppressed ? "Suppressed" : "Batched";
            this.log.Log(LogLevel.Debug, $"[Notifications] ForceImmediate notification route: {route} for character={notification.CharacterLabel} status={notification.Status}");
        }
        if (decision.Aggregate is { } aggregate)
        {
            await this.discordClient.SendVoyageBatchAsync(aggregate.CharacterLabel, aggregate.Payload, aggregate.TimestampUtc, cancellationToken).ConfigureAwait(false);
            
            // 通常の集約フローでは送信後に状態をリセット（ForceImmediate は除く）
            if (!envelope.ForceImmediate)
            {
                this.discordCycleAggregator.ResetCycle(notification.CharacterId);
            }
            return;
        }

        if (decision.IsSuppressed)
        {
            return;
        }

        var discordPayload = this.formatter.CreateDiscordPayload(notification);
        await this.discordBatcher.EnqueueAsync(notification, discordPayload, cancellationToken).ConfigureAwait(false);
        
        // ForceImmediate の場合は集約状態を一切変更しない
        // 通常の集約フローでは送信後に状態をリセット
        if (!envelope.ForceImmediate)
        {
            this.discordCycleAggregator.ResetCycle(notification.CharacterId);
        }
    }

    private static string BuildCharacterLabel(NotificationEnvelope envelope)
    {
        if (!string.IsNullOrWhiteSpace(envelope.CharacterName))
        {
            return string.IsNullOrWhiteSpace(envelope.WorldName)
                ? envelope.CharacterName
                : envelope.CharacterName + "@" + envelope.WorldName;
        }

        return "Unknown";
}

    private static string BuildSubmarineLabel(NotificationEnvelope envelope)
    {
        if (!string.IsNullOrWhiteSpace(envelope.SubmarineName))
        {
            return envelope.SubmarineName;
        }

        var slot = envelope.SubmarineId.Slot;
        return envelope.SubmarineId.IsPending ? "Pending Slot" : $"Slot {slot}";
    }
}
