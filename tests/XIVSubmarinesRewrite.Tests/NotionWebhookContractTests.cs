// apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/NotionWebhookContractTests.cs
// NotionWebhookClient の送信 JSON 契約を検証するユニットテストです
// payload と metadata が期待通りの文字列構造で送信されることを自動確認するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Integrations/Notifications/NotionWebhookClient.cs, apps/XIVSubmarinesRewrite/src/Application/Notifications/VoyageNotificationFormatter.cs

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Integrations.Notifications;
using Xunit;

public sealed class NotionWebhookContractTests
{
    [Fact]
    public async Task RecordVoyageCompletionAsync_SendsPayloadAndMetadataWithStringsOnly()
    {
        var handler = new RecordingHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://example.test/", UriKind.Absolute),
        };

        var settings = new NotificationSettings
        {
            EnableNotion = true,
            NotionWebhookUrl = "https://example.test/webhook",
        };
        var log = new TestLogSink();
        var client = new NotionWebhookClient(httpClient, settings, log);
        var formatter = new VoyageNotificationFormatter();
        var notification = CreateNotification();
        var payload = formatter.CreateNotionPayload(notification);

        Assert.True(settings.EnableNotion);
        Assert.Equal("https://example.test/webhook", settings.NotionWebhookUrl);

        await client.RecordVoyageCompletionAsync(notification, payload, CancellationToken.None);

        Assert.True(handler.WasInvoked);
        Assert.Equal(new Uri("https://example.test/webhook"), handler.RequestUri);
        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Contains(log.Entries, entry => entry.Level == LogLevel.Debug && entry.Message.Contains("Notion webhook dispatched", StringComparison.Ordinal));

        using var document = JsonDocument.Parse(handler.Body ?? throw new InvalidOperationException("Missing body"));
        var root = document.RootElement;
        Assert.Equal(JsonValueKind.Object, root.ValueKind);

        Assert.True(root.TryGetProperty("payload", out var payloadElement));
        Assert.True(root.TryGetProperty("metadata", out var metadataElement));

        foreach (var property in payloadElement.EnumerateObject())
        {
            Assert.True(property.Value.ValueKind == JsonValueKind.String || property.Value.ValueKind == JsonValueKind.Null, $"Property {property.Name} must be string or null");
        }

        Assert.True(payloadElement.TryGetProperty("Remaining", out var remainingElement));
        Assert.Equal(JsonValueKind.String, remainingElement.ValueKind);
        Assert.False(string.IsNullOrWhiteSpace(remainingElement.GetString()));

        Assert.Equal(JsonValueKind.Object, metadataElement.ValueKind);
        Assert.True(metadataElement.TryGetProperty("arrivalUtc", out var arrivalElement));
        var arrivalUtc = DateTime.Parse(arrivalElement.GetString()!, null, System.Globalization.DateTimeStyles.RoundtripKind);
        Assert.Equal(DateTimeKind.Utc, arrivalUtc.Kind);

        Assert.True(metadataElement.TryGetProperty("status", out var statusElement));
        Assert.Equal(JsonValueKind.String, statusElement.ValueKind);
        Assert.Equal(notification.Status.ToString(), statusElement.GetString());

        Assert.True(metadataElement.EnumerateObject().All(p => p.Value.ValueKind == JsonValueKind.String));
    }

    [Fact]
    public async Task RecordingHandler_IsInvokedByHttpClient()
    {
        var handler = new RecordingHttpMessageHandler();
        var httpClient = new HttpClient(handler);
        await httpClient.PostAsync("https://example.test/webhook", new StringContent("{}"));
        Assert.True(handler.WasInvoked);
    }

    [Fact]
    public async Task RecordVoyageCompletionAsync_SendsToLiveWebhookWhenConfigured()
    {
        var webhook = Environment.GetEnvironmentVariable("XIV_NOTION_WEBHOOK_URL");
        if (string.IsNullOrWhiteSpace(webhook))
        {
            return;
        }

        using var httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10),
        };

        var settings = new NotificationSettings
        {
            EnableNotion = true,
            NotionWebhookUrl = webhook,
        };

        var log = new TestLogSink();
        var client = new NotionWebhookClient(httpClient, settings, log);
        var formatter = new VoyageNotificationFormatter();
        var notification = CreateNotification();
        var payload = formatter.CreateNotionPayload(notification);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await client.RecordVoyageCompletionAsync(notification, payload, cts.Token);

        Assert.Contains(log.Entries, entry => entry.Level == LogLevel.Debug && entry.Message.Contains("Notion webhook dispatched", StringComparison.Ordinal));
    }

    private static VoyageNotification CreateNotification()
    {
        var characterId = 0xBEEF1234UL;
        var submarineId = new SubmarineId(characterId, 1);
        var arrivalUtc = DateTime.UtcNow.AddMinutes(45);
        var departureUtc = arrivalUtc.AddHours(-6);
        var voyageId = VoyageId.Create(submarineId, Guid.NewGuid());

        return new VoyageNotification(
            CharacterId: characterId,
            CharacterLabel: "Tester@World",
            CharacterName: "Tester",
            WorldName: "World",
            SubmarineId: submarineId,
            SubmarineLabel: "Alpha",
            SubmarineName: "Alpha",
            RouteId: "R-TEST",
            RouteDisplay: "R-TEST",
            VoyageId: voyageId,
            DepartureUtc: departureUtc,
            ArrivalUtc: arrivalUtc,
            ArrivalLocal: arrivalUtc.ToLocalTime(),
            Duration: arrivalUtc - departureUtc,
            Status: VoyageStatus.Underway,
            Confidence: SnapshotConfidence.Merged,
            HashKey: "hash-123",
            HashKeyShort: "hash-123");
    }

    private sealed class RecordingHttpMessageHandler : HttpMessageHandler
    {
        public bool WasInvoked { get; private set; }

        public Uri? RequestUri { get; private set; }

        public HttpMethod? Method { get; private set; }

        public string? Body { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.WasInvoked = true;
            this.RequestUri = request.RequestUri;
            this.Method = request.Method;
            this.Body = request.Content is null
                ? null
                : await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

    private sealed class TestLogSink : ILogSink
    {
        public List<(LogLevel Level, string Message)> Entries { get; } = new ();

        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            this.Entries.Add((level, message));
            _ = exception;
        }
    }
}
