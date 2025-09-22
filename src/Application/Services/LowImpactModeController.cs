namespace XIVSubmarinesRewrite.Application.Services;

using System;
using XIVSubmarinesRewrite.Acquisition;

/// <summary>Evaluates telemetry and decides whether the plugin should switch sampling strategies.</summary>
public sealed class LowImpactModeController
{
    private readonly AcquisitionOptions options;
    private readonly IAcquisitionTelemetry telemetry;

    public LowImpactModeController(AcquisitionOptions options, IAcquisitionTelemetry telemetry)
    {
        this.options = options;
        this.telemetry = telemetry;
    }

    public LowImpactModeState Evaluate()
    {
        var snapshot = this.telemetry.Snapshot;
        if (snapshot.FailureCount >= this.options.MaxSequentialFailures)
        {
            return LowImpactModeState.LowImpact;
        }

        var average = snapshot.AverageDuration;
        return average > this.options.InitialSamplingInterval
            ? LowImpactModeState.LowImpact
            : LowImpactModeState.Normal;
    }
}

public enum LowImpactModeState
{
    Normal = 0,
    LowImpact = 1
}
