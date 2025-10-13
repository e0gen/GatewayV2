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
    public void BllInfo()
    {
        var testMessage = "Test info log}";

        _logger.Info(LogTag.WebApi, testMessage, "Additional info for info log");
    }

    [Test]
    public void BllWarning()
    {
        var testMessage = "Test warning log}";

        _logger.Warn(LogTag.Security, testMessage, "Additional info for warning log");
    }

    [Test]
    public void BllError_WithMessage()
    {
        var testMessage = "Test error log";

        _logger.Error(LogTag.Exception, testMessage, "Additional info for error log");
    }

    [Test]
    public void BllError_WithException()
    {
        var exception = new InvalidOperationException("Test exception message");

        try
        {
            throw exception;
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Exception, ex, "Additional info for exception");
        }
    }

    [Test]
    public void BllError_WithHttpException()
    {
        var exception = new HttpRequestException("Http exception message", null,
            HttpStatusCode.NotFound);

        try
        {
            throw exception;
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Exception, ex, "Additional info for http exception");
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _serviceProvider.Dispose();
        Log.CloseAndFlush();
    }
}