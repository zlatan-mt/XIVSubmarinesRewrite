// apps/XIVSubmarinesRewrite/src/Application/Services/CharacterRegistry.cs
// Dalamud から得たキャラクター情報を集約し、名前と選択状態を永続化します
// UI で即座にキャラクター名を表示し直近の選択状態を引き継ぐために存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/CharacterRegistryPreferences.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/ISettingsProvider.cs

namespace XIVSubmarinesRewrite.Application.Services;

using System;
using System.Collections.Generic;
using System.Globalization;
using Dalamud.Plugin.Services;
using XIVSubmarinesRewrite.Acquisition;
using XIVSubmarinesRewrite.Infrastructure.Configuration;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Maintains character descriptors and active selection.</summary>
public sealed class CharacterRegistry : ICharacterRegistry, IDisposable
{
    private readonly IClientState clientState;
    private readonly IPlayerState playerState;
    private readonly IObjectTable objectTable;
    private readonly ILogSink log;
    private readonly ISettingsProvider settings;
    private readonly CharacterRegistryPreferences preferences;
    private readonly object sync = new ();
    private readonly Dictionary<ulong, CharacterDescriptorInternal> descriptors = new ();
    private readonly Dictionary<ulong, DateTime> lastUpdatedUtc = new ();
    private readonly Dictionary<ulong, DateTime> lastSubmarineOperationUtc = new ();
    private ulong activeCharacterId;

    public CharacterRegistry(IClientState clientState, IPlayerState playerState, IObjectTable objectTable, ILogSink log, ISettingsProvider settings)
    {
        this.clientState = clientState;
        this.playerState = playerState;
        this.objectTable = objectTable;
        this.log = log;
        this.settings = settings;
        this.preferences = this.settings.Get<CharacterRegistryPreferences>();

        foreach (var record in this.preferences.Characters.Values)
        {
            if (record.CharacterId == 0)
            {
                continue;
            }

            var descriptor = new CharacterDescriptorInternal(record.CharacterId);
            descriptor.Update(record.Name, record.World);
            this.descriptors[record.CharacterId] = descriptor;

            // Restore submarine operation history from preferences
            if (record.HasSubmarineOperations && record.LastSubmarineOperationUtc.HasValue)
            {
                this.lastSubmarineOperationUtc[record.CharacterId] = record.LastSubmarineOperationUtc.Value;
            }
        }

        if (this.preferences.LastActiveCharacterId.HasValue)
        {
            this.activeCharacterId = this.preferences.LastActiveCharacterId.Value;
        }

        // Clean up characters without submarine operations on startup
        this.CleanupCharactersWithoutSubmarineOperations();

        this.clientState.Login += this.OnLogin;
        this.clientState.Logout += this.OnLogout;
        if (this.playerState.ContentId != 0)
        {
            this.activeCharacterId = this.playerState.ContentId;
        }
    }

    public event EventHandler<CharacterChangedEventArgs>? ActiveCharacterChanged;
    public event EventHandler? CharacterListChanged;

    public ulong ActiveCharacterId
    {
        get
        {
            lock (this.sync)
            {
                return this.activeCharacterId;
            }
        }
    }

    public IReadOnlyList<CharacterDescriptor> Characters
    {
        get
        {
            lock (this.sync)
            {
                var list = new List<CharacterDescriptor>(this.descriptors.Count);
                foreach (var descriptor in this.descriptors.Values)
                {
                    list.Add(descriptor.ToDescriptor());
                }

                list.Sort((a, b) => string.Compare(a.DisplayName, b.DisplayName, StringComparison.Ordinal));
                return list;
            }
        }
    }

    public void RegisterSnapshot(AcquisitionSnapshot snapshot)
    {
        var descriptorChanged = false;
        var needsPersist = false;
        var hasSubmarineOperations = snapshot.Submarines.Count > 0;
        
        lock (this.sync)
        {
            // Track submarine operation timestamp if operations exist
            if (hasSubmarineOperations)
            {
                this.lastSubmarineOperationUtc[snapshot.CharacterId] = DateTime.UtcNow;
            }

            if (!this.descriptors.TryGetValue(snapshot.CharacterId, out var descriptor))
            {
                descriptor = new CharacterDescriptorInternal(snapshot.CharacterId);
                this.descriptors[snapshot.CharacterId] = descriptor;
                needsPersist = true;
            }

            var (name, world) = this.ResolveIdentity(snapshot, descriptor);
            descriptorChanged = descriptor.Update(name, world);
            needsPersist |= descriptorChanged;
            needsPersist |= !this.preferences.Characters.ContainsKey(snapshot.CharacterId);
            this.lastUpdatedUtc[snapshot.CharacterId] = DateTime.UtcNow;
        }

        // Only persist if character has submarine operations
        if (needsPersist && hasSubmarineOperations)
        {
            this.PersistDescriptor(snapshot.CharacterId);
        }
        else if (!hasSubmarineOperations)
        {
            this.log.Log(LogLevel.Debug, $"[CharacterRegistry] Character {snapshot.CharacterId} registered but not persisted - no submarine operations");
        }

        this.CharacterListChanged?.Invoke(this, EventArgs.Empty);

        if (this.activeCharacterId == 0 && snapshot.CharacterId != 0)
        {
            this.SelectCharacter(snapshot.CharacterId);
        }
    }

    public void SelectCharacter(ulong characterId)
    {
        var changed = false;
        lock (this.sync)
        {
            if (characterId == 0)
            {
                return;
            }

            if (this.activeCharacterId == characterId)
            {
                return;
            }

            this.activeCharacterId = characterId;
            this.preferences.LastActiveCharacterId = characterId;
            changed = true;
        }

        var descriptorChanged = this.UpdateDescriptorFromClientState(characterId);
        if (descriptorChanged)
        {
            this.PersistDescriptor(characterId);
        }
        else
        {
            this.SavePreferences();
        }

        if (changed)
        {
            this.ActiveCharacterChanged?.Invoke(this, new CharacterChangedEventArgs(characterId));
        }
    }

    public CharacterIdentity? GetIdentity(ulong characterId)
    {
        lock (this.sync)
        {
            if (this.descriptors.TryGetValue(characterId, out var descriptor))
            {
                return new CharacterIdentity(characterId, descriptor.CurrentName, descriptor.CurrentWorld);
            }
        }

        if (this.preferences.Characters.TryGetValue(characterId, out var record))
        {
            return new CharacterIdentity(characterId, record.Name, record.World);
        }

        return null;
    }

    public DateTime? GetLastUpdatedUtc(ulong characterId)
    {
        lock (this.sync)
        {
            return this.lastUpdatedUtc.TryGetValue(characterId, out var value) ? value : null;
        }
    }

    public void CleanupCharactersWithoutSubmarineOperations()
    {
        var charactersToRemove = new List<ulong>();
        lock (this.sync)
        {
            foreach (var kvp in this.descriptors)
            {
                var characterId = kvp.Key;
                
                // Check both memory and persisted flags
                var hasSubmarineOperationsInMemory = this.lastSubmarineOperationUtc.ContainsKey(characterId);
                var hasSubmarineOperationsInPreferences = this.preferences.Characters.TryGetValue(characterId, out var record) && record.HasSubmarineOperations;
                var hasSubmarineOperations = hasSubmarineOperationsInMemory || hasSubmarineOperationsInPreferences;
                
                if (!hasSubmarineOperations)
                {
                    charactersToRemove.Add(characterId);
                }
            }

            // Remove characters without submarine operations
            foreach (var characterId in charactersToRemove)
            {
                this.descriptors.Remove(characterId);
                this.lastUpdatedUtc.Remove(characterId);
                this.lastSubmarineOperationUtc.Remove(characterId);
                this.preferences.Characters.Remove(characterId);
            }
        }

        if (charactersToRemove.Count > 0)
        {
            this.log.Log(LogLevel.Information, $"[CharacterRegistry] Cleaned up {charactersToRemove.Count} characters without submarine operations");
            this.SavePreferences();
            this.CharacterListChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public IReadOnlyList<CharacterDescriptor> GetCharactersWithSubmarineOperations()
    {
        lock (this.sync)
        {
            var list = new List<CharacterDescriptor>();
            foreach (var kvp in this.descriptors)
            {
                var characterId = kvp.Key;
                if (this.lastSubmarineOperationUtc.ContainsKey(characterId))
                {
                    list.Add(kvp.Value.ToDescriptor());
                }
            }

            list.Sort((a, b) => string.Compare(a.DisplayName, b.DisplayName, StringComparison.Ordinal));
            return list;
        }
    }

    public void Dispose()
    {
        this.clientState.Login -= this.OnLogin;
        this.clientState.Logout -= this.OnLogout;
    }

    private void OnLogin()
    {
        var cid = this.playerState.ContentId;
        if (cid == 0)
        {
            this.log.Log(LogLevel.Debug, "[CharacterRegistry] Login detected but ContentId is zero.");
            return;
        }

        this.UpdateDescriptorFromClientState(cid);
        this.SelectCharacter(cid);
    }

    private void OnLogout(int type, int code)
    {
        _ = type;
        _ = code;
        lock (this.sync)
        {
            this.activeCharacterId = 0;
            this.preferences.LastActiveCharacterId = null;
        }
        this.ActiveCharacterChanged?.Invoke(this, new CharacterChangedEventArgs(0));
        this.SavePreferences();
    }

    private bool UpdateDescriptorFromClientState(ulong characterId)
    {
        if (characterId == 0 || characterId != this.playerState.ContentId || this.objectTable.LocalPlayer is null)
        {
            return false;
        }

        var updated = false;
        var requiresPersist = false;
        lock (this.sync)
        {
            if (!this.descriptors.TryGetValue(characterId, out var descriptor))
            {
                descriptor = new CharacterDescriptorInternal(characterId);
                this.descriptors[characterId] = descriptor;
                requiresPersist = true;
            }

            var player = this.objectTable.LocalPlayer!;
            var name = player.Name?.TextValue;
            var world = player.HomeWorld.ValueNullable?.Name.ToString();
            updated = descriptor.Update(name, world);
            requiresPersist |= updated;
            requiresPersist |= !this.preferences.Characters.ContainsKey(characterId);
        }

        this.CharacterListChanged?.Invoke(this, EventArgs.Empty);
        return updated || requiresPersist;
    }

    // UI フォールバック由来で欠けた名称を既存レコードや LocalPlayer から補完する。
    private (string? Name, string? World) ResolveIdentity(AcquisitionSnapshot snapshot, CharacterDescriptorInternal descriptor)
    {
        var name = snapshot.CharacterName;
        var world = snapshot.WorldName;

        if (string.IsNullOrWhiteSpace(name))
        {
            name = descriptor.CurrentName;
        }

        if (string.IsNullOrWhiteSpace(world))
        {
            world = descriptor.CurrentWorld;
        }

        if ((string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(world))
            && snapshot.CharacterId == this.playerState.ContentId)
        {
            try
            {
                var player = this.objectTable.LocalPlayer;
                if (player is not null)
                {
                    name ??= player.Name?.TextValue;
                    world ??= player.HomeWorld.ValueNullable?.Name.ToString();
                }
            }
            catch (InvalidOperationException ex)
            {
                this.log.Log(LogLevel.Trace, "[CharacterRegistry] LocalPlayer lookup skipped (" + ex.Message + ").");
            }
        }

        return (name, world);
    }

    private void PersistDescriptor(ulong characterId)
    {
        CharacterIdentityRecord record;
        lock (this.sync)
        {
            if (!this.descriptors.TryGetValue(characterId, out var descriptor))
            {
                return;
            }

            var hasSubmarineOperations = this.lastSubmarineOperationUtc.ContainsKey(characterId);
            var lastOperationUtc = hasSubmarineOperations ? this.lastSubmarineOperationUtc[characterId] : (DateTime?)null;

            record = new CharacterIdentityRecord
            {
                CharacterId = descriptor.CharacterId,
                Name = descriptor.CurrentName,
                World = descriptor.CurrentWorld,
                HasSubmarineOperations = hasSubmarineOperations,
                LastSubmarineOperationUtc = lastOperationUtc,
            };

            this.preferences.Characters[characterId] = record;
        }

        this.SavePreferences();
    }

    private void SavePreferences()
    {
        this.settings.SaveAsync(this.preferences).GetAwaiter().GetResult();
    }

    private sealed class CharacterDescriptorInternal
    {
        private readonly ulong characterId;
        private string? name;
        private string? world;

        public CharacterDescriptorInternal(ulong characterId)
        {
            this.characterId = characterId;
        }

        public ulong CharacterId => this.characterId;

        public bool Update(string? name, string? world)
        {
            var changed = false;
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (!string.Equals(this.name, name, StringComparison.Ordinal))
                {
                    this.name = name;
                    changed = true;
                }
            }

            if (!string.IsNullOrWhiteSpace(world))
            {
                if (!string.Equals(this.world, world, StringComparison.Ordinal))
                {
                    this.world = world;
                    changed = true;
                }
            }

            return changed;
        }

        public string? CurrentName => this.name;

        public string? CurrentWorld => this.world;

        public CharacterDescriptor ToDescriptor()
        {
            var cidDisplay = this.characterId.ToString("X", CultureInfo.InvariantCulture);
            var displayName = this.name is not null
                ? this.world is not null
                    ? $"{this.name}@{this.world} (0x{cidDisplay})"
                    : $"{this.name} (0x{cidDisplay})"
                : $"0x{cidDisplay}";

            return new CharacterDescriptor(this.characterId, displayName);
        }
    }
}
