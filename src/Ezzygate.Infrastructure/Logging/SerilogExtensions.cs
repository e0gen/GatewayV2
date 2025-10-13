using Ezzygate.Domain.Enums;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Ezzygate.Infrastructure.Logging;

public static class SerilogExtensions
{
    public static LoggerConfiguration BllLogDatabase(
        this LoggerSinkConfiguration sinkConfiguration,
        IServiceProvider serviceProvider,
        string fallbackLogPath = @"c:\temp\")
    {
        return sinkConfiguration.Sink(new BllLogDatabaseSink(serviceProvider, fallbackLogPath));
    }

    public static string? GetHttpCode(this LogEvent logEvent)
    {
        if (logEvent.Exception is HttpRequestException { StatusCode: not null } httpEx)
            return ((int)httpEx.StatusCode).ToString();

        return null;
    }

    public static string? GetMoreInfo(this LogEvent logEvent)
    {
        if (logEvent.Properties.TryGetValue("MoreInfo", out var moreInfoProperty) &&
            moreInfoProperty is ScalarValue { Value: string moreInfo })
            return moreInfo;

        return null;
    }

    public static LogTag GetLogTag(this LogEvent logEvent)
    {
        if (logEvent.Properties.TryGetValue("LogTag", out var logTagProperty) &&
            logTagProperty is ScalarValue { Value: LogTag logTag })
        {
            return logTag;
        }

        if (logEvent.Properties.TryGetValue("LogTag", out var logTagStringProperty) &&
            logTagStringProperty is ScalarValue { Value: string logTagString } &&
            Enum.TryParse<LogTag>(logTagString, out var parsedLogTag))
        {
            return parsedLogTag;
        }

        return LogTag.None;
    }
}