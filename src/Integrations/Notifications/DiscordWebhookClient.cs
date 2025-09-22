// apps/XIVSubmarinesRewrite/src/Integrations/Notifications/DiscordWebhookClient.cs
// Discord Webhook を通じて航海通知を送信する実装です
// Webhook 配信を共通化し、単体・バッチ通知の双方に対応するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Integrations/Notifications/IDiscordClient.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs

namespace XIVSubmarinesRewrite.Integrations.Notifications;

using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Sends voyage notifications to a Discord webhook.</summary>
public sealed class DiscordWebhookClient : IDiscordClient
{
    private readonly HttpClient httpClient;
    private readonly NotificationSettings settings;
    private readonly ILogSink log;
    private readonly JsonSerializerOptions serializerOptions = new (JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
    };

    public DiscordWebhookClient(HttpClient httpClient, NotificationSettings settings, ILogSink log)
    {
        this.httpClient = httpClient;
        this.settings = settings;
        this.log = log;
    }

    public ValueTask SendAlarmAsync(Alarm alarm, CancellationToken cancellationToken = default)
    {
        _ = alarm;
        return ValueTask.CompletedTask;
    }

    public async ValueTask SendVoyageCompletionAsync(VoyageNotification notification, DiscordNotificationPayload payload, CancellationToken cancellationToken = default)
    {
        if (!this.settings.EnableDiscord || string.IsNullOrWhiteSpace(this.settings.DiscordWebhookUrl))
        {
            return;
        }

        await this.SendPayloadAsync(notification.SubmarineLabel, payload, notification.ArrivalUtc, cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask SendVoyageBatchAsync(string username, DiscordNotificationPayload payload, DateTime timestampUtc, CancellationToken cancellationToken = default)
    {
        if (!this.settings.EnableDiscord || string.IsNullOrWhiteSpace(this.settings.DiscordWebhookUrl))
        {
            return;
        }

        await this.SendPayloadAsync(username, payload, timestampUtc, cancellationToken).ConfigureAwait(false);
    }

    private async ValueTask SendPayloadAsync(string username, DiscordNotificationPayload payload, DateTime timestampUtc, CancellationToken cancellationToken)
    {
        var request = BuildRequestBody(username, payload, timestampUtc);
        var json = JsonSerializer.Serialize(request, this.serializerOptions);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await this.httpClient.PostAsync(this.settings.DiscordWebhookUrl, content, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var status = (int)response.StatusCode;
            var reason = response.ReasonPhrase ?? "Unknown";
            this.log.Log(LogLevel.Warning, $"[Notifications] Discord webhook failed with {status} {reason}: {message}");
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                var retryAfter = NotificationRetryHelper.ParseRetryAfter(response);
                throw new NotificationRateLimitException(retryAfter, $"Discord rate limited ({status})");
            }

            throw new InvalidOperationException($"Discord webhook returned {status} {reason}");
        }
    }

    private static object BuildRequestBody(string username, DiscordNotificationPayload payload, DateTime timestampUtc)
    {
        var footer = string.IsNullOrWhiteSpace(payload.Footer) ? null : new { text = payload.Footer };

        var embeds = new[]
        {
            new
            {
                title = payload.Title,
                description = payload.Description,
                color = ParseColor(payload.Color),
                fields = payload.Fields.Select(f => new { name = f.Name, value = f.Value, inline = f.Inline }).ToArray(),
                footer,
                timestamp = timestampUtc.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture),
            },
        };

        return new
        {
            username,
            content = (string?)null,
            embeds,
        };
    }

    private static int ParseColor(string color)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return 0;
        }

        var hex = color.StartsWith("#", StringComparison.Ordinal) ? color[1..] : color;
        if (int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var value))
        {
            return value;
        }

        return 0;
    }

}
