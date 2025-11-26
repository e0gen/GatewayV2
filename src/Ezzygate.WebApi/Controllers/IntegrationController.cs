using Microsoft.AspNetCore.Mvc;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Integrations.Ph3a;
using Ezzygate.Integrations.Services.Processing;
using Ezzygate.WebApi.Extensions;
using Ezzygate.WebApi.Filters;
using Ezzygate.WebApi.Models;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.WebApi.Controllers;

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
        var post = await Request.GetRequestContentAsync();

        _logger.Info(LogTag.Integration, "Notification loopback - Query: {query}, Post: {post}", query, post);
        return Ok("NotificationLoopback OK");
    }

    [HttpPost]
    [Route("Process")]
    [IntegrationSecurityFilter]
    public async Task<IActionResult> Process([FromBody] IntegrationProcessRequest request,
        CancellationToken cancellationToken)
    {
        var processRequest = new ProcessRequest
        {
            OperationType = request.OperationType,
            TerminalId = request.TerminalId,
            PaymentMethodId = request.PaymentMethodId,
            DebitRefCode = request.DebitRefCode,
            DebitRefNum = request.DebitRefNum,
            ApprovalNumber = request.ApprovalNumber,
            Amount = request.Amount,
            OriginalAmount = request.OriginalAmount,
            CurrencyIso = request.CurrencyIso,
            Payments = request.Payments,
            TransType = request.TransType,
            CreditType = request.CreditType,
            CardHolderName = request.CreditCard?.HolderName,
            CardNumber = request.CreditCard?.Number,
            Cvv = request.CreditCard?.Cvv,
            Track2 = request.CreditCard?.Track2,
            ExpirationMonth = request.CreditCard?.ExpirationMonth ?? 0,
            ExpirationYear = request.CreditCard?.ExpirationYear ?? 0,
            Email = request.Customer?.Email,
            PersonalIdNumber = request.Customer?.PersonalIdNumber,
            PhoneNumber = request.Customer?.PhoneNumber,
            MerchantNumber = request.MerchantNumber,
            OrderId = request.OrderId,
            CartId = request.CartId,
            CustomerId = request.CustomerId,
            Comment = request.Comment,
            RoutingNumber = request.RoutingNumber,
            AccountNumber = request.AccountNumber,
            AccountName = request.AccountName,
            ClientIp = request.ClientIp,
            RequestContent = request.RequestContent,
            FormData = request.FormData,
            QueryString = request.QueryString,
            RequestSource = request.RequestSource,
            IsAutomatedRequest = request.IsAutomatedRequest,
            AutomatedStatus = request.AutomatedStatus,
            AutomatedErrorMessage = request.AutomatedErrorMessage,
            AutomatedPayload = request.AutomatedPayload
        };

        var result = await _integrationProcessor.ProcessAsync(processRequest, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("Finalize")]
    [IntegrationSecurityFilter]
    public async Task<IActionResult> Finalize([FromBody] IntegrationFinalizeRequest request,
        CancellationToken cancellationToken)
    {
        var finalizeRequest = new FinalizeRequest
        {
            DebitReferenceCode = request.DebitRefCode ?? string.Empty,
            DebitReferenceNum = request.DebitRefNum,
            ChargeAttemptLogId = request.ChargeAttemptLogId,
            OperationType = request.OperationType,
            RequestContent = request.RequestContent,
            FormData = request.FormData,
            QueryString = request.QueryString,
            IsAutomatedRequest = request.IsAutomatedRequest,
            AutomatedStatus = request.AutomatedStatus,
            AutomatedErrorMessage = request.AutomatedErrorMessage,
            AutomatedPayload = request.AutomatedPayload
        };

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