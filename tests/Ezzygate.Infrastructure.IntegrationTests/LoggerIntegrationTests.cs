using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Infrastructure.IntegrationTests;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class LoggerIntegrationTests : IDisposable
{
    private readonly ILogger _logger;
    private readonly ServiceProvider _serviceProvider;
    private readonly ErrorsDbContext _dbContext;

    public LoggerIntegrationTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<ErrorsDbContext>(options =>
            options.UseMySql(
                GetConnectionString(),
                new MySqlServerVersion(new Version(9, 4, 00))));

        _serviceProvider = services.BuildServiceProvider();
        _dbContext = _serviceProvider.GetRequiredService<ErrorsDbContext>();
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.BllLogDatabase(_serviceProvider)
            .CreateLogger();

        var loggerFactory = LoggerFactory.Create(builder => { builder.AddSerilog(Log.Logger); });

        _logger = loggerFactory.CreateLogger<LoggerIntegrationTests>();
    }

    private static string GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();
        return configuration.GetConnectionString("ErrorConnection") ?? string.Empty;
    }

    [Test]
    public void Bll_Trace()
    {
        var testMessage = "Test trace log {value}";

        _logger.Trace(testMessage, 123);
    }

    [Test]
    public void Bll_Debug()
    {
        var testMessage = "Test debug log {value}";

        _logger.Debug(testMessage, 123);
    }

    [Test]
    public void Bll_Info()
    {
        var testMessage = "Test info log {value}";

        _logger.Info(LogTag.WebApi, testMessage, 123);
    }

    [Test]
    public void Bll_InfoExtra()
    {
        var testMessage = "Test info log {value}";

        _logger.InfoExtra(LogTag.WebApi, "Additional info", testMessage, 123);
    }

    [Test]
    public void Bll_Warning()
    {
        var testMessage = "Test warning log {value}";

        _logger.Warn(LogTag.Security, testMessage, 123);
    }

    [Test]
    public void Bll_WarningExtra()
    {
        var testMessage = "Test warning log {value}";

        _logger.WarnExtra(LogTag.Security, "Additional warning info", testMessage, 123);
    }

    [Test]
    public void Bll_Error_WithMessage()
    {
        var testMessage = "Test error log {value}";

        _logger.Error(LogTag.Exception, testMessage, 123);
    }

    [Test]
    public void Bll_ErrorExtra_WithMessage()
    {
        var testMessage = "Test error log {value}";

        _logger.ErrorExtra(LogTag.Exception, "Additional error info", testMessage, 123);
    }

    [Test]
    public void Bll_Error_WithException()
    {
        var exception = new InvalidOperationException("Test exception message");

        try
        {
            throw exception;
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Exception, ex);
        }
    }

    [Test]
    public void Bll_ErrorExtra_WithException()
    {
        var exception = new InvalidOperationException("Test exception message");

        try
        {
            throw exception;
        }
        catch (Exception ex)
        {
            _logger.ErrorExtra(LogTag.Exception, "Additional info for exception", ex);
        }
    }

    [Test]
    public void Bll_Error_WithHttpException()
    {
        var exception = new HttpRequestException("Http exception message", null,
            HttpStatusCode.NotFound);

        try
        {
            throw exception;
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Exception, ex);
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _serviceProvider.Dispose();
        Log.CloseAndFlush();
    }
}