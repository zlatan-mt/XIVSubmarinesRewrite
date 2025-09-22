namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System.Threading;
using System.Threading.Tasks;

/// <summary>Provides strongly typed access to plugin configuration.</summary>
public interface ISettingsProvider
{
    TSettings Get<TSettings>() where TSettings : class, new();
    ValueTask SaveAsync<TSettings>(TSettings settings, CancellationToken cancellationToken = default) where TSettings : class, new();
}
