using Microsoft.AspNetCore.Mvc;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Integrations.Core.Processing;
using Ezzygate.Integrations.Ph3a;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Extensions;
using Ezzygate.WebApi.Filters;
using Ezzygate.WebApi.Models;

namespace Ezzygate.WebApi.Controllers.Apps;

[ApiController]
[Route("api/apps/Integration")]
public class IntegrationController : ControllerBase
{
    private readonly ILogger<IntegrationController> _logger;
    private readonly IIntegrationProcessor _integrationProcessor;
    private readonly IIntegrationFinalizer _integrationFinalizer;
    private readonly IPh3AService _ph3AService;

    public IntegrationController(
        ILogger<IntegrationController> logger,
        IIntegrationProcessor integrationProcessor,
        IIntegrationFinalizer integrationFinalizer,
        IPh3AService ph3AService)
    {
        _logger = logger;
        _integrationProcessor = integrationProcessor;
        _integrationFinalizer = integrationFinalizer;
        _ph3AService = ph3AService;
    }

    [HttpGet, HttpPost]
    public async Task<IActionResult> NotificationLoopback()
    {
        var query = Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty;
        var post = await Request.ReadBodyAsStringAsync();

        _logger.Info(LogTag.Integration, "Notification loopback - Query: {query}, Post: {post}", query, post);
        return Ok("NotificationLoopback OK");
    }

    [HttpPost]
    [Route("Process")]
    [IntegrationSecurityFilter]
    public async Task<IActionResult> Process([FromBody] IntegrationProcessRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var processRequest = requestDto.ToProcessRequest();
        var result = await _integrationProcessor.ProcessAsync(processRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("Finalize")]
    [IntegrationSecurityFilter]
    public async Task<IActionResult> Finalize([FromBody] IntegrationFinalizeRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var finalizeRequest = requestDto.ToFinalizeRequest();
        var result = await _integrationFinalizer.FinalizeAsync(finalizeRequest, cancellationToken);
        return result.Code == "520" ? StatusCode(520, result) : Ok(result);
    }

    [HttpPost]
    [IntegrationSecurityFilter]
    public async Task<Response> Ph3ARequest(Ph3ARequestDto request)
    {
        try
        {
            var isValidScore = await _ph3AService.ValidateScore(request, request.MerchantId);
            var result = isValidScore
                ? new Response(ResultEnum.Success, new { code = "000" })
                : new Response(ResultEnum.Success, new { code = "580" });
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, $"{nameof(Ph3ARequest)} exception");
            return new Response(ResultEnum.Success, new { code = "000" });
        }
    }
}