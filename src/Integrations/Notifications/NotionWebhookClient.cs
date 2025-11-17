// apps/XIVSubmarinesRewrite/src/Integrations/Notifications/NotionWebhookClient.cs
// Notion 用 Webhook クライアントの実装です
// 航海通知を JSON として送信し、Zapier などの自動化と連携させるため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs, apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/NotionWebhookContractTests.cs

namespace XIVSubmarinesRewrite.Integrations.Notifications;

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Sends voyage notifications to a Notion automation webhook (Zapier等想定)。</summary>
public sealed class NotionWebhookClient : INotionClient
{
    private readonly HttpClient httpClient;
    private readonly NotificationSettings settings;
    private readonly ILogSink log;
    private readonly JsonSerializerOptions serializerOptions = new (JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
    };

    public NotionWebhookClient(HttpClient httpClient, NotificationSettings settings, ILogSink log)
    {
        this.httpClient = httpClient;
        this.settings = settings;
        this.log = log;
    }

    public ValueTask RecordAlarmAsync(Alarm alarm, CancellationToken cancellationToken = default)
    {
        _ = alarm;
        return ValueTask.CompletedTask;
    }

    public async ValueTask RecordVoyageCompletionAsync(VoyageNotification notification, NotionNotificationPayload payload, CancellationToken cancellationToken = default)
    {
        if (!this.settings.EnableNotion || string.IsNullOrWhiteSpace(this.settings.NotionWebhookUrl))
        {
            return;
        }

        var body = new
        {
            payload = payload.Properties,
            metadata = new
            {
                notification.CharacterLabel,
                notification.SubmarineLabel,
                ArrivalUtc = notification.ArrivalUtc.ToString("O", CultureInfo.InvariantCulture),
                Status = notification.Status.ToString(),
                notification.HashKeyShort,
            },
        };

        var json = JsonSerializer.Serialize(body, this.serializerOptions);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await this.httpClient.PostAsync(this.settings.NotionWebhookUrl, content, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var status = (int)response.StatusCode;
            this.log.Log(LogLevel.Warning, $"[Notifications] Notion webhook failed with {status}: {responseBody}");
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new NotificationRateLimitException(NotificationRetryHelper.ParseRetryAfter(response), "Notion rate limited");
            }
            throw new InvalidOperationException($"Notion webhook returned {status}");
        }

        this.log.Log(
            LogLevel.Debug,
            $"[Notifications] Notion webhook dispatched character={notification.CharacterLabel} submarine={notification.SubmarineLabel} hash={notification.HashKeyShort} timestamp={DateTime.UtcNow:O}.");
    }
}
