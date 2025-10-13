using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Enums;

namespace Ezzygate.Infrastructure.Logging;

public static class LoggerExtensions
{
    public static void Info(this ILogger logger, LogTag logTag, string message, string? moreInfo = null)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogInformation("{Message}", message);
        }
    }

    public static void Warn(this ILogger logger, LogTag logTag, string message, string? moreInfo = null)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogWarning("{Message}", message);
        }
    }

    public static void Error(this ILogger logger, LogTag logTag, string message, string? moreInfo = null)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogError("{Message}", message);
        }
    }

    public static void Error(this ILogger logger, LogTag logTag, Exception exception, string? moreInfo = null)
    {
        var message = exception.Message;
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["LogTag"] = logTag,
                   ["MoreInfo"] = moreInfo ?? string.Empty
               }))
        {
            logger.LogError(exception, "{Message}", message);
        }
    }
}