namespace XIVSubmarinesRewrite.Acquisition;

using System;

/// <summary>Sampling and retry configuration for memory/UI acquisition.</summary>
public sealed class AcquisitionOptions
{
    public TimeSpan InitialSamplingInterval { get; init; } = TimeSpan.FromSeconds(2);
    public TimeSpan StableSamplingInterval { get; init; } = TimeSpan.FromSeconds(10);
    public int MaxSequentialFailures { get; init; } = 3;
}
