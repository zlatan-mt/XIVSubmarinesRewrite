namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Non-persistent secret store suitable for local testing.</summary>
public sealed class InMemorySecretStore : ISecretStore
{
    private readonly ConcurrentDictionary<string, string> store = new ();

    public ValueTask SetAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        this.store[key] = value;
        return ValueTask.CompletedTask;
    }

    public ValueTask<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        this.store.TryGetValue(key, out var value);
        return ValueTask.FromResult<string?>(value);
    }
}
