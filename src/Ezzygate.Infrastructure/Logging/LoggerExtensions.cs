using Microsoft.Extensions.Logging;
using JetBrains.Annotations;

namespace Ezzygate.Infrastructure.Logging;

public static class LoggerExtensions
{
    public static void Trace(this ILogger logger,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        logger.LogTrace(messageTemplate, args);
    }

    public static void Debug(this ILogger logger,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        logger.LogDebug(messageTemplate, args);
    }

    public static void Info(this ILogger logger, LogTag logTag,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        logger.InfoExtra(logTag, null, messageTemplate, args);
    }

    public static void Warn(this ILogger logger, LogTag logTag,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        logger.WarnExtra(logTag, null, messageTemplate, args);
    }

    public static void Error(this ILogger logger, LogTag logTag,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        logger.ErrorExtra(logTag, null, messageTemplate, args);
    }

    public static void Error(this ILogger logger, LogTag logTag, Exception exception,
        [StructuredMessageTemplate] string? messageTemplate = null, params object?[] args)
    {
        logger.ErrorExtra(logTag, null, exception, messageTemplate, args);
    }

    public static void InfoExtra(this ILogger logger, LogTag logTag, string? moreInfo,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogInformation(messageTemplate, args);
        }
    }

    public static void WarnExtra(this ILogger logger, LogTag logTag, string? moreInfo,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogWarning(messageTemplate, args);
        }
    }

    public static void ErrorExtra(this ILogger logger, LogTag logTag, string? moreInfo,
        [StructuredMessageTemplate] string messageTemplate, params object?[] args)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogError(messageTemplate, args);
        }
    }

    public static void ErrorExtra(this ILogger logger, LogTag logTag, string? moreInfo, Exception exception,
        [StructuredMessageTemplate] string? messageTemplate = null, params object?[] args)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogError(exception, messageTemplate, args);
        }
    }

    public static ScopedLogger GetScoped(this ILogger logger, LogTag logTag, string message)
    {
        return new ScopedLogger(logger, logTag, message);
    }

    public static ScopedLogger GetScopedForIntegration(this ILogger logger, string tag, string method)
    {
        return new ScopedLogger(logger, LogTag.Integration,
            getShortMessage => $"[{tag}] {method} completed. {getShortMessage}");
    }
}