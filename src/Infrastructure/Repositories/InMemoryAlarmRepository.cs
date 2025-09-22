namespace XIVSubmarinesRewrite.Infrastructure.Repositories;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Domain.Models;
using XIVSubmarinesRewrite.Domain.Repositories;

/// <summary>Transient persistence for alarm schedules.</summary>
public sealed class InMemoryAlarmRepository : IAlarmRepository
{
    private readonly ConcurrentDictionary<string, Alarm> store = new();

    public ValueTask<Alarm?> GetAsync(string alarmId, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        this.store.TryGetValue(alarmId, out var alarm);
        return ValueTask.FromResult(alarm);
    }

    public async IAsyncEnumerable<Alarm> ListPendingAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var alarm in this.store.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (alarm.NextTriggerAt.HasValue)
            {
                yield return alarm;
            }
        }

        await Task.CompletedTask;
    }

    public ValueTask SaveAsync(Alarm alarm, CancellationToken cancellationToken = default)
    {
        _ = cancellationToken;
        this.store[alarm.AlarmId] = alarm;
        return ValueTask.CompletedTask;
    }
}
