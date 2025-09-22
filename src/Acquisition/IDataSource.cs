namespace XIVSubmarinesRewrite.Acquisition;

using System.Threading;
using System.Threading.Tasks;

/// <summary>Generalized acquisition source (memory, UI fallback, cached data, etc.).</summary>
public interface IDataSource
{
    ValueTask<AcquisitionSnapshot?> TryAcquireAsync(CancellationToken cancellationToken = default);
}
