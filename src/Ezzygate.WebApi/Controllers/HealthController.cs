using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Ezzygate.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("3.0")]
[ApiVersion("4.0")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [MapToApiVersion("3.0")]
    [MapToApiVersion("4.0")]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check requested");
        
        return Ok(new
        {
            Status = "Healthy",
            Version = "3.0/4.0",
            Timestamp = DateTime.UtcNow,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            MachineName = Environment.MachineName
        });
    }

    [HttpGet("detailed")]
    [MapToApiVersion("3.0")]
    [MapToApiVersion("4.0")]
    public IActionResult GetDetailed()
    {
        _logger.LogInformation("Detailed health check requested");
        
        return Ok(new
        {
            Status = "Healthy",
            Version = "3.0/4.0",
            Timestamp = DateTime.UtcNow,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            MachineName = Environment.MachineName,
            ProcessId = Environment.ProcessId,
            WorkingSet = GC.GetTotalMemory(false),
            SystemInfo = new
            {
                OSVersion = Environment.OSVersion.ToString(),
                ProcessorCount = Environment.ProcessorCount,
                Is64BitProcess = Environment.Is64BitProcess
            }
        });
    }
} 