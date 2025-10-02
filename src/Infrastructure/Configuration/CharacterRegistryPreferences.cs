// apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/CharacterRegistryPreferences.cs
// キャラクター ID と名称の対応表を設定として保持します
// 起動直後からキャラクター名を表示できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Application/Services/CharacterRegistry.cs, apps/XIVSubmarinesRewrite/src/Infrastructure/Configuration/DalamudJsonSettingsProvider.cs

namespace XIVSubmarinesRewrite.Infrastructure.Configuration;

using System.Collections.Generic;

/// <summary>Stores known character identities for quick lookups.</summary>
public sealed class CharacterRegistryPreferences
{
    public ulong? LastActiveCharacterId { get; set; }

    public Dictionary<ulong, CharacterIdentityRecord> Characters { get; set; } = new();
}

/// <summary>Represents a persisted mapping between a content ID and its display metadata.</summary>
public sealed class CharacterIdentityRecord
{
    public ulong CharacterId { get; set; }

    public string? Name { get; set; }

    public string? World { get; set; }

    public bool HasSubmarineOperations { get; set; }

    public System.DateTime? LastSubmarineOperationUtc { get; set; }
}
