namespace XIVSubmarinesRewrite.Presentation.ViewModels;

using System.Collections.Generic;
using XIVSubmarinesRewrite.Domain.Models;

/// <summary>Alarm screen reactive model.</summary>
public interface IAlarmViewModel
{
    IReadOnlyList<Alarm> PendingAlarms { get; }
}
