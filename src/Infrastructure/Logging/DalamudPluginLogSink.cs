namespace XIVSubmarinesRewrite.Infrastructure.Logging;

using Dalamud.Plugin.Services;

/// <summary>Routes log messages to Dalamud's plugin log.</summary>
public sealed class DalamudPluginLogSink : ILogSink
{
    private readonly IPluginLog pluginLog;

    public DalamudPluginLogSink(IPluginLog pluginLog)
    {
        this.pluginLog = pluginLog;
    }

    public void Log(LogLevel level, string message, Exception? exception = null)
    {
        switch (level)
        {
            case LogLevel.Trace:
            case LogLevel.Debug:
                this.pluginLog.Debug(exception, message);
                break;
            case LogLevel.Information:
                this.pluginLog.Information(exception, message);
                break;
            case LogLevel.Warning:
                this.pluginLog.Warning(exception, message);
                break;
            case LogLevel.Error:
            case LogLevel.Critical:
                this.pluginLog.Error(exception, message);
                break;
            default:
                this.pluginLog.Information(exception, message);
                break;
        }
    }
}
