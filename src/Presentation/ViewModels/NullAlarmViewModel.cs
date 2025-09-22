namespace XIVSubmarinesRewrite.Presentation.ViewModels;

using System.Collections.Generic;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Placeholder alarm view model until alarm logic is implemented.</summary>
public sealed class NullAlarmViewModel : IAlarmViewModel
{
    public IReadOnlyList<Alarm> PendingAlarms { get; } = new List<Alarm>();
}
