// apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/UiPreferences.cs
// UI 表示状態とユーザー選択を永続化する設定クラスを定義します
// 起動時のウィンドウ表示やキャラクター選択を復元できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/DalamudJsonSettingsProvider.cs

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

/// <summary>User interface preferences persisted across sessions.</summary>
public sealed class UiPreferences
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ulong? LastSelectedCharacterId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? OverviewWindowVisible { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MainWindowVisible { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? MainWindowWidth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? MainWindowHeight { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowDeveloperTools { get; set; }

    public DevPanelHistory DevHistory { get; set; } = new DevPanelHistory();

    public sealed class DevPanelHistory
    {
        [JsonConverter(typeof(CompactUtcDateTimeSecondsConverter))]
        public DateTime? LastDeveloperTabToggleUtc { get; set; }

        public bool DeveloperToolsVisible { get; set; }

        [JsonConverter(typeof(CompactUtcDateTimeSecondsConverter))]
        public DateTime? LastForceNotifyToggleUtc { get; set; }

        public bool ForceNotifyEnabled { get; set; }

        [JsonConverter(typeof(CompactUtcDateTimeSecondsConverter))]
        public DateTime? LastManualTriggerUtc { get; set; }

        public string? LastManualTriggerSummary { get; set; }
    }

    /// <summary>
    /// DevHistory の日時をコンパクトに保存するためのコンバータ。
    /// - 書き込み: Unix epoch seconds (number)
    /// - 読み込み: number (epoch seconds) / string (ISO8601) の両対応（後方互換）
    /// </summary>
    internal sealed class CompactUtcDateTimeSecondsConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var seconds))
            {
                return DateTime.UnixEpoch.AddSeconds(seconds);
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var text = reader.GetString();
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                if (DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var parsed))
                {
                    return parsed.ToUniversalTime();
                }
            }

            throw new JsonException($"Unsupported DevHistory datetime token: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            var utc = value.Value.Kind == DateTimeKind.Utc ? value.Value : value.Value.ToUniversalTime();
            var seconds = (long)Math.Round((utc - DateTime.UnixEpoch).TotalSeconds);
            writer.WriteNumberValue(seconds);
        }
    }
}
