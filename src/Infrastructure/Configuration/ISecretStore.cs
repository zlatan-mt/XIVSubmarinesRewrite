namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System.Threading;
using System.Threading.Tasks;

/// <summary>Handles persistence of sensitive tokens and webhooks.</summary>
public interface ISecretStore
{
    ValueTask SetAsync(string key, string value, CancellationToken cancellationToken = default);
    ValueTask<string?> GetAsync(string key, CancellationToken cancellationToken = default);
}
