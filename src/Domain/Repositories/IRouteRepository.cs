namespace XIVSubmarinesRewrite.Domain.Repositories;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Manages known route templates and metadata.</summary>
public interface IRouteRepository
{
    ValueTask<Route?> GetAsync(string routeId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Route> ListAsync(CancellationToken cancellationToken = default);
}
