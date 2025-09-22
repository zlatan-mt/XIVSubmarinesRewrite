namespace XIVSubmarinesRewrite.Infrastructure.Diagnostics;

using System;

/// <summary>Measures plugin runtime performance to feed adaptive logic.</summary>
public interface IPerformanceMonitor
{
    PerformanceSample Capture();
}

public sealed record PerformanceSample(DateTime Timestamp, double CpuUsage, double GpuUsage, long ManagedMemoryBytes);
