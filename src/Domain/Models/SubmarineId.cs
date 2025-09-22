namespace XIVSubmarinesRewrite.Domain.Models;

using System;
using System.Globalization;

/// <summary>Value object identifying a submarine uniquely by character and slot.</summary>
public readonly record struct SubmarineId(ulong CharacterId, byte Slot)
{
    public const byte PendingSlot = byte.MaxValue;

    public bool IsPending => this.Slot == PendingSlot;

    public override string ToString()
    {
        var cid = this.CharacterId.ToString("X", CultureInfo.InvariantCulture);
        return this.IsPending
            ? $"0x{cid}:pending"
            : $"0x{cid}:{this.Slot}";
    }
}
