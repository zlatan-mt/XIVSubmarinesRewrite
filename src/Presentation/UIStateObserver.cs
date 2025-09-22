namespace XIVSubmarinesRewrite.Presentation;

using System;
using System.Threading.Tasks;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Application.Services;
using XIVSubmarinesRewrite.Presentation.ViewModels;

/// <summary>Keeps presentation layer in sync with acquisition snapshots.</summary>
public sealed class UIStateObserver : IDisposable
{
    private readonly SnapshotCache cache;
    private readonly IOverviewViewModel overview;
    private readonly IAlarmViewModel alarm;
    private readonly ICharacterRegistry characterRegistry;

    public UIStateObserver(SnapshotCache cache, IOverviewViewModel overview, IAlarmViewModel alarm, ICharacterRegistry characterRegistry)
    {
        this.cache = cache;
        this.overview = overview;
        this.alarm = alarm;
        this.characterRegistry = characterRegistry;
        this.cache.SnapshotUpdated += this.OnSnapshotUpdated;
        this.characterRegistry.ActiveCharacterChanged += this.OnActiveCharacterChanged;
    }

    public void Dispose()
    {
        this.cache.SnapshotUpdated -= this.OnSnapshotUpdated;
        this.characterRegistry.ActiveCharacterChanged -= this.OnActiveCharacterChanged;
    }

    private void OnSnapshotUpdated(object? sender, SnapshotUpdatedEventArgs args)
    {
        _ = sender;
        var active = this.characterRegistry.ActiveCharacterId;
        if (active == 0 || args.CharacterId != active)
        {
            return;
        }

        _ = Task.Run(async () => await this.overview.RefreshAsync(active).ConfigureAwait(false));
        // Alarm view model updates will be wired when alarm logic is implemented.
    }

    private void OnActiveCharacterChanged(object? sender, CharacterChangedEventArgs args)
    {
        _ = sender;
        var characterId = args.CharacterId;
        if (characterId == 0)
        {
            return;
        }

        _ = Task.Run(async () => await this.overview.RefreshAsync(characterId).ConfigureAwait(false));
    }
}
