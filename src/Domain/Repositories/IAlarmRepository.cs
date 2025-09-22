namespace XIVSubmarinesRewrite.Domain.Repositories;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Persists alarm schedules and next trigger times.</summary>
public interface IAlarmRepository
{
    ValueTask<Alarm?> GetAsync(string alarmId, CancellationToken cancellationToken = default);
    ValueTask SaveAsync(Alarm alarm, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Alarm> ListPendingAsync(CancellationToken cancellationToken = default);
}
