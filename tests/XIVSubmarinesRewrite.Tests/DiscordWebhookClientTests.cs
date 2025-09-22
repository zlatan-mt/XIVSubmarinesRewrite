using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Integrations.Notifications;
using Xunit;

public sealed class DiscordWebhookClientTests
{
    [Fact]
    public async Task SendVoyageCompletionAsync_SendsWhenEnabled()
    {
        var handler = new TestHandler();
        var client = new HttpClient(handler);
        var settings = new NotificationSettings
        {
            EnableDiscord = true,
            DiscordWebhookUrl = "https://example.com/webhook",
        };
        var log = new TestLogSink();
        var discord = new DiscordWebhookClient(client, settings, log);

        var notification = CreateNotification();
        var payload = CreatePayload();

        await discord.SendVoyageCompletionAsync(notification, payload, CancellationToken.None);

        Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, handler.Requests[0].Method);
    }

    [Fact]
    public async Task SendVoyageCompletionAsync_NoOpWhenDisabled()
    {
        var handler = new TestHandler();
        var client = new HttpClient(handler);
        var settings = new NotificationSettings
        {
            EnableDiscord = false,
            DiscordWebhookUrl = "https://example.com/webhook",
        };
        var log = new TestLogSink();
        var discord = new DiscordWebhookClient(client, settings, log);

        await discord.SendVoyageCompletionAsync(CreateNotification(), CreatePayload(), CancellationToken.None);

        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task SendVoyageCompletionAsync_ThrowsOnFailure()
    {
        var handler = new TestHandler { StatusCode = HttpStatusCode.BadRequest };
        var client = new HttpClient(handler);
        var settings = new NotificationSettings
        {
            EnableDiscord = true,
            DiscordWebhookUrl = "https://example.com/webhook",
        };
        var log = new TestLogSink();
        var discord = new DiscordWebhookClient(client, settings, log);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => discord.SendVoyageCompletionAsync(CreateNotification(), CreatePayload(), CancellationToken.None).AsTask());
    }

    private static VoyageNotification CreateNotification() => new (
        CharacterId: 1,
        CharacterLabel: "Tester@World",
        CharacterName: "Tester",
        WorldName: "World",
        SubmarineId: new SubmarineId(1, 0),
        SubmarineLabel: "Alpha",
        SubmarineName: "Alpha",
        RouteId: "ROUTE",
        VoyageId: VoyageId.Create(new SubmarineId(1, 0), Guid.NewGuid()),
        DepartureUtc: DateTime.UtcNow.AddHours(-6),
        ArrivalUtc: DateTime.UtcNow,
        ArrivalLocal: DateTime.Now,
        Duration: TimeSpan.FromHours(6),
        Status: VoyageStatus.Completed,
        Confidence: SnapshotConfidence.Merged,
        HashKey: "ABC123",
        HashKeyShort: "ABC123");

    private static DiscordNotificationPayload CreatePayload() => new (
        "Alpha 帰港",
        "完了",
        "#1ABC9C",
        Array.Empty<DiscordNotificationField>(),
        "hash");

    private sealed class TestHandler : HttpMessageHandler
    {
        public List<HttpRequestMessage> Requests { get; } = new ();

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.Requests.Add(request);
            var response = new HttpResponseMessage(this.StatusCode)
            {
                Content = new StringContent(string.Empty),
            };
            return Task.FromResult(response);
        }
    }

    private sealed class TestLogSink : ILogSink
    {
        public void Log(LogLevel level, string message, Exception? exception = null)
        {
            _ = level;
            _ = message;
            _ = exception;
        }
    }
}
