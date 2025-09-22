namespace XIVSubmarinesRewrite.Infrastructure.Logging;

using System;

/// <summary>No-op logger used during early development.</summary>
public sealed class NullLogSink : ILogSink
{
    public void Log(LogLevel level, string message, Exception? exception = null)
    {
        _ = level;
        _ = message;
        _ = exception;
    }
}
