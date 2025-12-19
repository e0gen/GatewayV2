using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Application.Interfaces;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Ef.Entities;
using Ezzygate.Infrastructure.Ef.Models;
using Ezzygate.Infrastructure.Extensions;
using Serilog.Core;
using Serilog.Events;

namespace Ezzygate.Infrastructure.Logging;

public class BllLogDatabaseSink : ILogEventSink
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string _fallbackLogPath;

    public BllLogDatabaseSink(IServiceProvider serviceProvider, string fallbackLogPath = @"c:\temp\")
    {
        _serviceProvider = serviceProvider;
        _fallbackLogPath = fallbackLogPath;
    }

    public void Emit(LogEvent logEvent)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ErrorsDbContext>();

            var bllLog = CreateBllLogEntry(logEvent);
            context.BllLogs.Add(bllLog);

            if ((logEvent.Level == LogEventLevel.Error
                 || logEvent is { Level: LogEventLevel.Warning, Exception: not null })
                && logEvent.Exception != null)
            {
                var errorNet = CreateErrorNetEntry(logEvent);
                context.ErrorNets.Add(errorNet);
            }

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            WriteToFallbackFile(logEvent, ex);
        }
    }

    private BllLog CreateBllLogEntry(LogEvent logEvent)
    {
        var logTag = logEvent.GetLogTag();
        var severity = MapLogLevel(logEvent.Level);
        var message = logEvent.RenderMessage();
        var longMessage = BuildLongMessage(logEvent);

        return new BllLog
        {
            InsertDate = logEvent.Timestamp.DateTime,
            Severity = severity,
            Tag = logTag.ToString(),
            Source = GetApplicationName(),
            Message = message.Truncate(400),
            LongMessage = longMessage.Truncate(8192),
            AppVersion = GetAppVersion(),
            LogDomain = GetCurrentDomain(),
            HttpCode = logEvent.GetHttpCode()
        };
    }

    private string BuildLongMessage(LogEvent logEvent)
    {
        var longMessage = new StringBuilder();

        longMessage.AddMoreInfo(logEvent);
        longMessage.AddExceptions(logEvent);
        longMessage.AddWebContextInfo(_serviceProvider);
        longMessage.AddHttpCode(logEvent);
        longMessage.AddAssembliesInfo();

        return longMessage.ToString();
    }

    private ErrorNet CreateErrorNetEntry(LogEvent logEvent)
    {
        var exception = logEvent.Exception;

        using var scope = _serviceProvider.CreateScope();
        var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
        var context = httpContextAccessor?.HttpContext;

        return new ErrorNet
        {
            ErrorTime = logEvent.Timestamp.DateTime,
            ProjectName = GetApplicationName().Truncate(25),
            RemoteIP = context.GetRemoteIP().Truncate(25),
            LocalIP = context.GetLocalIP().Truncate(25),
            RemoteUser = context.GetRemoteUser().Truncate(50),
            ServerName = Environment.MachineName.Truncate(50),
            ServerPort = context.GetServerPort().Truncate(5),
            ScriptName = context.GetScriptName().Truncate(50),
            VirtualPath = context.GetVirtualPath().Truncate(50),
            PhysicalPath = context.GetPhysicalPath().Truncate(100),
            RequestQueryString = context.GetRequestQueryString().Truncate(500),
            ExceptionSource = exception?.Source.Truncate(100) ?? string.Empty,
            ExceptionMessage = exception?.Message.Truncate(500) ?? logEvent.RenderMessage().Truncate(500),
            ExceptionTargetSite = exception?.TargetSite?.Name.Truncate(100) ?? string.Empty,
            ExceptionStackTrace = exception?.StackTrace.Truncate(1000) ?? string.Empty,
            ExceptionHelpLink = exception?.HelpLink.Truncate(100) ?? string.Empty,
            ExceptionLineNumber = 0,
            InnerExceptionSource = exception?.InnerException?.Source.Truncate(100) ?? string.Empty,
            InnerExceptionMessage = exception?.InnerException?.Message.Truncate(500) ?? string.Empty,
            InnerExceptionTargetSite = exception?.InnerException?.TargetSite?.Name.Truncate(1000) ?? string.Empty,
            InnerExceptionHelpLink = exception?.InnerException?.HelpLink.Truncate(100) ?? string.Empty,
            InnerExceptionLineNumber = 0,
            IsFailedSQL = false,
            IsArchive = false,
            InnerExceptionStackTrace = exception?.InnerException?.StackTrace ?? string.Empty,
            RequestForm = context.GetRequestForm().Truncate(500),
            IsHighlighted = false,
            AppVersion = GetAppVersion(),
            Domain = GetCurrentDomain(),
            HttpCode = logEvent.GetHttpCode()
        };
    }

    private static decimal? GetAppVersion()
    {
        var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        var appVersion = assemblyVersion != null
            ? decimal.Parse($"{assemblyVersion.Major}.{assemblyVersion.Minor}", CultureInfo.InvariantCulture)
            : (decimal?)null;
        return appVersion;
    }

    private LogSeverity MapLogLevel(LogEventLevel level)
    {
        return level switch
        {
            LogEventLevel.Information => LogSeverity.Info,
            LogEventLevel.Debug => LogSeverity.Info,
            LogEventLevel.Verbose => LogSeverity.Info,
            LogEventLevel.Warning => LogSeverity.Warning,
            LogEventLevel.Error => LogSeverity.Error,
            LogEventLevel.Fatal => LogSeverity.Error,
            _ => LogSeverity.Info
        };
    }

    private string GetApplicationName()
    {
        var fullName = Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown";

        var firstDotIndex = fullName.IndexOf('.');
        return firstDotIndex >= 0 ? fullName[(firstDotIndex + 1)..] : fullName;
    }

    private string GetCurrentDomain()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var domainConfigProvider = scope.ServiceProvider.GetRequiredService<IDomainConfigurationProvider>();
            var domainConfig = domainConfigProvider.GetCurrentDomainConfiguration();
            return domainConfig.Host;
        }
        catch
        {
            return string.Empty;
        }
    }

    private void WriteToFallbackFile(LogEvent logEvent, Exception dbException)
    {
        try
        {
            var currentLogFile = Path.Combine(_fallbackLogPath, DateTime.UtcNow.ToString("dd-MM-yyyy") + ".log");
            var logContent = $"""

                              =================== log string =========================
                              {DateTime.UtcNow}
                              {GetApplicationName()}
                              {MapLogLevel(logEvent.Level)}
                              {logEvent.GetLogTag()}
                              {logEvent.RenderMessage()}
                              {BuildLongMessage(logEvent)}
                              ========================================================
                              Could not save to BllLog: {dbException.Message}
                              ========================================================

                              """;

            if (!Directory.Exists(_fallbackLogPath))
                Directory.CreateDirectory(_fallbackLogPath);

            File.AppendAllText(currentLogFile, logContent);
        }
        catch
        {
            // If even file logging fails, there's nothing more we can do
        }
    }
}