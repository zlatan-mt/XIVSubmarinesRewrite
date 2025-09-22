// apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/DalamudJsonSettingsProvider.cs
// Dalamud の ConfigDirectory を利用して設定を永続化するプロバイダを提供します
// プラグインの再起動後もユーザー設定が保持されるようにするために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Composition/PluginBootstrapper.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/ISettingsProvider.cs

namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Plugin;

/// <summary>Persists settings objects to Dalamud's configuration directory as JSON.</summary>
public sealed class DalamudJsonSettingsProvider : ISettingsProvider
{
    private readonly IDalamudPluginInterface pluginInterface;
    private readonly ConcurrentDictionary<string, object> cache = new (StringComparer.Ordinal);
    private readonly JsonSerializerOptions serializerOptions = new ()
    {
        WriteIndented = true,
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
    };

    public DalamudJsonSettingsProvider(IDalamudPluginInterface pluginInterface)
    {
        this.pluginInterface = pluginInterface ?? throw new ArgumentNullException(nameof(pluginInterface));
    }

    public TSettings Get<TSettings>() where TSettings : class, new()
    {
        var key = GetCacheKey<TSettings>();
        if (this.cache.TryGetValue(key, out var existing) && existing is TSettings cached)
        {
            return cached;
        }

        var path = this.ResolvePath<TSettings>();
        if (File.Exists(path))
        {
            try
            {
                using var stream = File.OpenRead(path);
                var loaded = JsonSerializer.Deserialize<TSettings>(stream, this.serializerOptions);
                if (loaded is not null)
                {
                    this.cache[key] = loaded;
                    return loaded;
                }
            }
            catch
            {
                // 破損した設定は握り潰し、既定値で再生成する
            }
        }

        var created = new TSettings();
        this.cache[key] = created;
        return created;
    }

    public async ValueTask SaveAsync<TSettings>(TSettings settings, CancellationToken cancellationToken = default) where TSettings : class, new()
    {
        var key = GetCacheKey<TSettings>();
        this.cache[key] = settings;
        var path = this.ResolvePath<TSettings>();
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        await JsonSerializer.SerializeAsync(stream, settings, this.serializerOptions, cancellationToken).ConfigureAwait(false);
    }

    private static string GetCacheKey<TSettings>() => typeof(TSettings).FullName ?? typeof(TSettings).Name;

    private string ResolvePath<TSettings>()
    {
        var directory = this.pluginInterface.ConfigDirectory?.FullName;
        if (string.IsNullOrEmpty(directory))
        {
            directory = Path.Combine(AppContext.BaseDirectory, "config");
        }

        var fileName = (typeof(TSettings).FullName ?? typeof(TSettings).Name) + ".json";
        return Path.Combine(directory, fileName);
    }
}
