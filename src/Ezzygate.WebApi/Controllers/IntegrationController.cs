using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Locking;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Abstractions;
using Ezzygate.Integrations.Ph3a;
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
    private readonly ITransactionContextFactory _transactionContextFactory;
    private readonly ICreditCardIntegrationProcessor _creditCardIntegrationProcessor;
    private readonly IDistributedLockService _distributedLockService;
    private readonly IPh3AService _ph3AService;
    private readonly IOptions<IntegrationSettings> _integrationSettings;

    public IntegrationController(
        ILogger<IntegrationController> logger,
        ITransactionContextFactory transactionContextFactory,
        ICreditCardIntegrationProcessor creditCardIntegrationProcessor,
        IDistributedLockService distributedLockService,
        IPh3AService ph3AService,
        IOptions<IntegrationSettings> integrationSettings)
    {
        _logger = logger;
        _transactionContextFactory = transactionContextFactory;
        _creditCardIntegrationProcessor = creditCardIntegrationProcessor;
        _distributedLockService = distributedLockService;
        _ph3AService = ph3AService;
        _integrationSettings = integrationSettings;
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
    public async Task<IActionResult> Process([FromBody] IntegrationProcessRequest request)
    {
        try
        {
            _logger.Info(LogTag.Integration, "Processing integration request for DebitRefCode: {DebitRefCode}",
                request.DebitRefCode);

            var ctx = await _transactionContextFactory.CreateAsync(request.TerminalId, request.PaymentMethodId);

            ctx.OpType = request.OperationType;
            ctx.RequestContent = request.RequestContent;
            ctx.FormData = request.FormData;
            ctx.QueryString = request.QueryString;
            ctx.IsAutomatedRequest = request.IsAutomatedRequest;
            ctx.AutomatedStatus = request.AutomatedStatus;
            ctx.AutomatedErrorMessage = request.AutomatedErrorMessage;
            ctx.AutomatedPayload = request.AutomatedPayload;
            ctx.Amount = request.Amount;
            ctx.CurrencyIso = request.CurrencyIso;
            ctx.Payments = request.Payments;
            ctx.PayerName = request.CreditCard?.HolderName ?? string.Empty;
            ctx.CardNumber = request.CreditCard?.Number ?? string.Empty;
            ctx.Cvv = request.CreditCard?.Cvv ?? string.Empty;
            ctx.Track2 = request.CreditCard?.Track2 ?? string.Empty;
            ctx.Email = request.Customer?.Email ?? string.Empty;
            ctx.ExpirationMonth = request.CreditCard?.ExpirationMonth ?? 0;
            ctx.ExpirationYear = request.CreditCard?.ExpirationYear ?? 0;
            ctx.PersonalIdNumber = request.Customer?.PersonalIdNumber ?? string.Empty;
            ctx.PhoneNumber = request.Customer?.PhoneNumber ?? string.Empty;
            ctx.SentDebitRefCode = request.DebitRefCode;
            ctx.ApprovalNumber = request.ApprovalNumber;
            ctx.DebitRefNum = request.DebitRefNum;
            ctx.ClientIp = request.ClientIp;
            ctx.MerchantNumber = request.MerchantNumber;
            ctx.OrderId = request.OrderId;
            ctx.CartId = request.CartId;
            ctx.CustomerId = request.CustomerId;
            ctx.OriginalAmount = request.OriginalAmount;
            ctx.RequestSource = request.RequestSource;
            ctx.TransType = request.TransType;
            ctx.CreditType = request.CreditType;
            ctx.Comment = request.Comment;
            ctx.RoutingNumber = request.RoutingNumber;
            ctx.AccountNumber = request.AccountNumber;
            ctx.AccountName = request.AccountName;
            var queryParams = QueryHelpers.ParseQuery(ctx.QueryString);
            if (queryParams.TryGetValue("l3d_arrival_date", out var l3dArrivalDate))
                ctx.Level3DataArrivalDate = l3dArrivalDate.ToString();
            ctx.IsMobileMoto = !string.IsNullOrEmpty(request.Comment) &&
                               request.Comment.StartsWith("fcm") &&
                               ctx.RequestSource == TransactionSource.WebApi;

            var result = await _creditCardIntegrationProcessor.ProcessTransactionAsync(ctx);

            if (_integrationSettings.Value.DisablePostRedirectUrl)
                result.RedirectUrl = null;

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, "Process exception: OpType: {OperationType}", request.OperationType);
            return StatusCode(520, new IntegrationResult
            {
                Code = "520",
                Message = "[002] Internal server error, see logs",
                DebitRefCode = request.DebitRefCode
            });
        }
    }

    [HttpPost]
    [Route("Finalize")]
    [IntegrationSecurityFilter]
    public async Task<IActionResult> Finalize([FromBody] IntegrationFinalizeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DebitRefCode))
            return BadRequest(new IntegrationResult
            {
                Code = "520", Message = "Invalid transaction reference id", DebitRefCode = request.DebitRefCode
            });

        var lockKey = $"process_finalize_{request.DebitRefCode}";

        await using var distributedLock = await _distributedLockService.AcquireLockAsync(lockKey);

        try
        {
            _logger.Info(LogTag.Integration, "Finalize request for DebitRefCode: {DebitRefCode}", request.DebitRefCode);

            var ctx = await _transactionContextFactory.CreateAsync(request.DebitRefCode,
                request.ChargeAttemptLogId);

            ctx.OpType = request.OperationType;
            ctx.DebitRefCode = request.DebitRefCode;
            ctx.RequestContent = request.RequestContent;
            ctx.FormData = request.FormData;
            ctx.QueryString = request.QueryString;
            ctx.IsAutomatedRequest = request.IsAutomatedRequest;
            ctx.AutomatedStatus = request.AutomatedStatus;
            ctx.AutomatedErrorMessage = request.AutomatedErrorMessage;
            ctx.AutomatedPayload = request.AutomatedPayload;

            var result = await _creditCardIntegrationProcessor.ProcessTransactionAsync(ctx);

            if (_integrationSettings.Value.DisablePostRedirectUrl)
                result.RedirectUrl = null;

            return Ok(result);
        }
        catch (DistributedLockException ex)
        {
            _logger.Error(LogTag.Integration, ex, "Failed to acquire lock for Finalize: DebitRefCode: {DebitRefCode}", 
                request.DebitRefCode);
            return StatusCode(520, new IntegrationResult
            {
                Code = "520",
                Message = "[003] Transaction is being processed, please retry",
                DebitRefCode = request.DebitRefCode
            });
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Integration, ex, "Finalize exception: OpType: {OperationType}", request.OperationType);
            return StatusCode(520, new IntegrationResult
            {
                Code = "520",
                Message = "[003] Internal server error, see logs",
                DebitRefCode = request.DebitRefCode
            });
        }
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