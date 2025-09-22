namespace XIVSubmarinesRewrite.Domain.Models;

using System;
using System.Globalization;

/// <summary>Value object identifying a voyage within a submarine context.</summary>
public readonly record struct VoyageId(SubmarineId SubmarineId, Guid Value)
{
    public static VoyageId Create(SubmarineId submarineId, Guid value) => new (submarineId, value);

    public override string ToString() => $"{this.SubmarineId}:{this.Value}";
}
