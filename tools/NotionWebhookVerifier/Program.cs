// apps/XIVSubmarinesRewrite/tools/NotionWebhookVerifier/Program.cs
// NotionWebhookClient を CLI から実行して実機 Webhook へ送信します
// ペイロード構造と HTTP 応答を自動検証し、ログとサマリを保存するため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Integrations/Notifications/NotionWebhookClient.cs, apps/XIVSubmarinesRewrite/tools/NotionWebhookVerifier/VerifierOptions.cs

namespace XIVSubmarinesRewrite.Tools.NotionWebhookVerifier;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Notifications;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;
using XIVSubmarinesRewrite.Integrations.Notifications;

internal static class Program
{
    private static readonly JsonSerializerOptions JsonOptions = new ()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

    public static async Task<int> Main(string[] args)
    {
        if (!VerifierOptions.TryParse(args, out var options, out var error))
        {
            Console.Error.WriteLine(error);
            return 64;
        }

        Directory.CreateDirectory(options.RunRoot);

        await using var log = new VerifierLog(options.LogFilePath);
        log.Log(LogLevel.Information, $"[NotionVerifier] Run start webhook={options.WebhookUrl}");

        try
        {
            var handler = new RecordingHandler(new HttpClientHandler());
            using var httpClient = new HttpClient(handler)
            {
                Timeout = options.Timeout,
            };

            var settings = new NotificationSettings
            {
                EnableNotion = true,
                NotionWebhookUrl = options.WebhookUrl,
            };

            var client = new NotionWebhookClient(httpClient, settings, log);
            var formatter = new VoyageNotificationFormatter();
            var notification = NotificationFactory.Create(options.CharacterId);
            var payload = formatter.CreateNotionPayload(notification);

            using var cts = new CancellationTokenSource(options.Timeout);
            await client.RecordVoyageCompletionAsync(notification, payload, cts.Token);

            var requestJson = handler.LastRequestBody ?? string.Empty;
            File.WriteAllText(options.RequestPath, requestJson, Utf8NoBom);

            var responseBody = new
            {
                statusCode = handler.LastStatusCode,
                reasonPhrase = handler.LastReasonPhrase,
                body = handler.LastResponseBody,
            };
            var responseJson = JsonSerializer.Serialize(responseBody, JsonOptions);
            File.WriteAllText(options.ResponsePath, responseJson, Utf8NoBom);

            var summary = BuildSummary(options, requestJson, handler.LastStatusCode, handler.LastResponseBody);
            var summaryJson = JsonSerializer.Serialize(summary, JsonOptions);
            File.WriteAllText(options.SummaryPath, summaryJson, Utf8NoBom);

            if (!summary.Success)
            {
                log.Log(LogLevel.Warning, "[NotionVerifier] Validation failed. See summary.json for details.");
                return 2;
            }

            log.Log(LogLevel.Information, "[NotionVerifier] Validation succeeded.");
            return 0;
        }
        catch (Exception ex)
        {
            log.Log(LogLevel.Error, "[NotionVerifier] Unhandled exception", ex);
            return 1;
        }
    }

    private static VerifierSummary BuildSummary(VerifierOptions options, string requestJson, int? statusCode, string? responseBody)
    {
        var issues = new List<string>();
        ValidateRequestJson(requestJson, issues);

        if (statusCode is null)
        {
            issues.Add("No HTTP status code recorded.");
        }
        else if (statusCode is < 200 or >= 300)
        {
            issues.Add($"Webhook returned non-success status {statusCode}.");
        }

        var success = issues.Count == 0;
        return new VerifierSummary(
            options.RunName,
            DateTime.UtcNow.ToString("O"),
            options.WebhookUrl,
            statusCode,
            responseBody,
            success,
            issues);
    }

    private static void ValidateRequestJson(string requestJson, List<string> issues)
    {
        if (string.IsNullOrWhiteSpace(requestJson))
        {
            issues.Add("Request body was empty.");
            return;
        }

        try
        {
            using var document = JsonDocument.Parse(requestJson);
            var root = document.RootElement;
            if (!root.TryGetProperty("payload", out var payload))
            {
                issues.Add("Missing payload object.");
            }
            else
            {
                foreach (var property in payload.EnumerateObject())
                {
                    var kind = property.Value.ValueKind;
                    if (kind != JsonValueKind.String && kind != JsonValueKind.Null)
                    {
                        issues.Add($"payload.{property.Name} contains non-string value ({kind}).");
                    }
                }

                if (payload.TryGetProperty("Remaining", out var remaining))
                {
                    if (remaining.ValueKind != JsonValueKind.String || string.IsNullOrWhiteSpace(remaining.GetString()))
                    {
                        issues.Add("payload.Remaining must be a non-empty string.");
                    }
                }
                else
                {
                    issues.Add("payload.Remaining missing.");
                }
            }

            if (!root.TryGetProperty("metadata", out var metadata))
            {
                issues.Add("Missing metadata object.");
            }
            else
            {
                foreach (var property in metadata.EnumerateObject())
                {
                    if (property.Value.ValueKind != JsonValueKind.String)
                    {
                        issues.Add($"metadata.{property.Name} contains non-string value.");
                    }
                }
            }
        }
        catch (JsonException jsonEx)
        {
            issues.Add($"Failed to parse request JSON: {jsonEx.Message}");
        }
    }
}

internal static class NotificationFactory
{
    public static VoyageNotification Create(ulong characterId)
    {
        var submarineId = new SubmarineId(characterId, 0);
        var nowUtc = DateTime.UtcNow;
        var arrivalUtc = nowUtc.AddHours(6);
        var departureUtc = nowUtc.AddHours(-1);
        return new VoyageNotification(
            CharacterId: characterId,
            CharacterLabel: $"Verifier-{characterId:X}",
            CharacterName: $"Verifier-{characterId:X}",
            WorldName: "Verifier",
            SubmarineId: submarineId,
            SubmarineLabel: "Verifier-1",
            SubmarineName: "Verifier-1",
            RouteId: "R-VERIFY",
            RouteDisplay: "R-VERIFY",
            VoyageId: VoyageId.Create(submarineId, Guid.NewGuid()),
            DepartureUtc: departureUtc,
            ArrivalUtc: arrivalUtc,
            ArrivalLocal: arrivalUtc.ToLocalTime(),
            Duration: arrivalUtc - departureUtc,
            Status: VoyageStatus.Underway,
            Confidence: SnapshotConfidence.Merged,
            HashKey: $"verify-{arrivalUtc:yyyyMMddHHmmss}",
            HashKeyShort: "verify-1");
    }
}

internal sealed class RecordingHandler : DelegatingHandler
{
    public RecordingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    public string? LastRequestBody { get; private set; }

    public int? LastStatusCode { get; private set; }

    public string? LastReasonPhrase { get; private set; }

    public string? LastResponseBody { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Content is not null)
        {
            this.LastRequestBody = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }

        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        this.LastStatusCode = (int)response.StatusCode;
        this.LastReasonPhrase = response.ReasonPhrase;
        this.LastResponseBody = response.Content is null
            ? null
            : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        return response;
    }
}

internal sealed class VerifierLog : ILogSink, IAsyncDisposable
{
    private readonly StreamWriter writer;
    private readonly object gate = new ();

    public VerifierLog(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        this.writer = new StreamWriter(File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read))
        {
            AutoFlush = true,
        };
    }

    public void Log(LogLevel level, string message, Exception? exception = null)
    {
        var line = $"{DateTime.UtcNow:O}\t{level}\t{message}";
        if (exception is not null)
        {
            line += "\t" + exception;
        }

        lock (this.gate)
        {
            this.writer.WriteLine(line);
        }

        Console.WriteLine(line);
    }

    public ValueTask DisposeAsync()
    {
        this.writer.Dispose();
        return ValueTask.CompletedTask;
    }
}

internal sealed record VerifierSummary(
    string Run,
    string GeneratedAtUtc,
    string Webhook,
    int? StatusCode,
    string? ResponseBody,
    bool Success,
    IReadOnlyList<string> Issues);
