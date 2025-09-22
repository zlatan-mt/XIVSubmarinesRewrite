namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Placeholder settings provider for early development/testing.</summary>
public sealed class InMemorySettingsProvider : ISettingsProvider
{
    private readonly ConcurrentDictionary<string, object> cache = new ();

    public TSettings Get<TSettings>() where TSettings : class, new()
    {
        var key = typeof(TSettings).FullName ?? typeof(TSettings).Name;
        if (this.cache.TryGetValue(key, out var value) && value is TSettings typed)
        {
            return typed;
        }

        var created = new TSettings();
        this.cache[key] = created;
        return created;
    }

    public ValueTask SaveAsync<TSettings>(TSettings settings, CancellationToken cancellationToken = default) where TSettings : class, new()
    {
        var key = typeof(TSettings).FullName ?? typeof(TSettings).Name;
        this.cache[key] = settings;
        return ValueTask.CompletedTask;
    }
}
