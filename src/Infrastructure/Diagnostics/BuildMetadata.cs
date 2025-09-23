// apps/XIVSubmarinesRewrite/src/Infrastructure/Diagnostics/BuildMetadata.cs
// アセンブリからビルド時のバージョン情報を取り出すヘルパーです
// バージョン表記にビルド日時を含め、UI やログで参照できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/XIVSubmarinesRewrite.csproj, apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs

namespace XIVSubmarinesRewrite.Infrastructure.Diagnostics;

using System;
using System.Globalization;
using System.Reflection;

/// <summary>Provides access to plugin build metadata derived from assembly attributes.</summary>
public static class BuildMetadata
{
    private const string BuildToken = "+build.";
    private const string StampFormat = "yyyyMMdd'T'HHmmss'Z'";

    private static readonly Lazy<BuildMetadataSnapshot> Snapshot = new(GetSnapshot, true);

    /// <summary>Gets the raw informational version string emitted at build time.</summary>
    public static string InformationalVersion => Snapshot.Value.InformationalVersion;

    /// <summary>Gets a user-facing version label with localized build timestamp if available.</summary>
    public static string DisplayVersion
    {
        get
        {
            var snapshot = Snapshot.Value;
            if (snapshot.LocalBuildTime.HasValue)
            {
                var local = snapshot.LocalBuildTime.Value;
                return $"v{snapshot.BaseVersion} – build {local:yyyy-MM-dd HH:mm:ss zzz}";
            }

            if (!string.IsNullOrWhiteSpace(snapshot.BuildStamp))
            {
                return $"v{snapshot.BaseVersion} – build {snapshot.BuildStamp}";
            }

            return snapshot.InformationalVersion;
        }
    }

    private static BuildMetadataSnapshot GetSnapshot()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        var informational = attribute?.InformationalVersion ?? "unknown";
        var baseVersion = informational;
        string? stamp = null;
        string? normalizedStamp = null;

        var plusIndex = informational.IndexOf('+');
        if (plusIndex >= 0)
        {
            baseVersion = informational[..plusIndex];
        }

        var tokenIndex = informational.IndexOf(BuildToken, StringComparison.OrdinalIgnoreCase);
        if (tokenIndex >= 0)
        {
            stamp = informational[(tokenIndex + BuildToken.Length)..];
            normalizedStamp = NormalizeStamp(stamp);
        }

        DateTimeOffset? local = null;
        if (!string.IsNullOrWhiteSpace(normalizedStamp) && DateTimeOffset.TryParseExact(normalizedStamp, StampFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var utc))
        {
            local = utc.ToLocalTime();
        }

        return new BuildMetadataSnapshot(informational, baseVersion, normalizedStamp ?? stamp, local);
    }

    private static string? NormalizeStamp(string? stamp)
    {
        if (string.IsNullOrWhiteSpace(stamp))
        {
            return null;
        }

        var trimmed = stamp.Trim();
        if (trimmed.Length <= StampFormat.Length)
        {
            return trimmed;
        }

        var candidate = trimmed[..StampFormat.Length];
        return candidate;
    }

    private readonly record struct BuildMetadataSnapshot(
        string InformationalVersion,
        string BaseVersion,
        string? BuildStamp,
        DateTimeOffset? LocalBuildTime);
}
