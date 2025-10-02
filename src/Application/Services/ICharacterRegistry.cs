namespace XIVSubmarinesRewrite.Application.Services;

using System;
using System.Collections.Generic;
using XIVSubmarinesRewrite.Acquisition;

/// <summary>Tracks active characters and known descriptors.</summary>
public interface ICharacterRegistry
{
    event EventHandler<CharacterChangedEventArgs>? ActiveCharacterChanged;
    event EventHandler? CharacterListChanged;

    ulong ActiveCharacterId { get; }
    IReadOnlyList<CharacterDescriptor> Characters { get; }

    void RegisterSnapshot(AcquisitionSnapshot snapshot);
    void SelectCharacter(ulong characterId);

    CharacterIdentity? GetIdentity(ulong characterId);
    System.DateTime? GetLastUpdatedUtc(ulong characterId);

    void CleanupCharactersWithoutSubmarineOperations();
    IReadOnlyList<CharacterDescriptor> GetCharactersWithSubmarineOperations();
}

public sealed record CharacterDescriptor(ulong CharacterId, string DisplayName);

public sealed record CharacterIdentity(ulong CharacterId, string? Name, string? World);

public sealed class CharacterChangedEventArgs : EventArgs
{
    public CharacterChangedEventArgs(ulong characterId)
    {
        this.CharacterId = characterId;
    }

    public ulong CharacterId { get; }
}
