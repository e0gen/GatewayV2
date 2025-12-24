using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Processing;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Dtos.Merchants.CreditCard;
using Ezzygate.WebApi.Filters;

namespace Ezzygate.WebApi.Controllers.Merchants;

[ApiController]
[Route("merchants/[controller]")]
[ApiVersion("3.0")]
[ApiVersion("4.0")]
public class CreditcardController : ControllerBase
{
    private readonly ILogger<CreditcardController> _logger;
    private readonly ILegacyPaymentService _legacyPaymentService;
    private readonly IPaymentPageSettingsRepository _paymentPageSettingsRepository;
    private readonly ICurrencyRepository _currencyRepository;

    public CreditcardController(
        ILogger<CreditcardController> logger,
        ILegacyPaymentService legacyPaymentService,
        IPaymentPageSettingsRepository paymentPageSettingsRepository,
        ICurrencyRepository currencyRepository)
    {
        _logger = logger;
        _legacyPaymentService = legacyPaymentService;
        _paymentPageSettingsRepository = paymentPageSettingsRepository;
        _currencyRepository = currencyRepository;
    }

    [HttpPost("Process")]
    [MerchantSecurityFilter]
    [EnableCors]
    public async Task<Response> Process([FromBody] CreditcardProcessRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        var validationResult = await ValidateRequestAsync(request, merchant, cancellationToken);
        if (validationResult != null)
            return validationResult;

        var legacyRequest = CreditCardDtoFactory.CreateLegacyProcessRequest(request, merchant);
        var result = await _legacyPaymentService.ProcessAsync(legacyRequest, cancellationToken);
        var response = CreditCardDtoFactory.CreateBasicResponse(result, request, _currencyRepository);

        return new Response(result.GetResultStatus(), response);
    }

    [HttpPost("ProcessEncrypted")]
    [MerchantSecurityFilter]
    public async Task<Response> ProcessEncrypted([FromBody] CreditcardProcessEncryptedRequestDto encryptedRequest, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        CreditcardProcessRequestDto request;
        try
        {
            var decrypted = AesEncryption.DecryptStringAes(encryptedRequest.Data);
            request = JsonSerializer.Deserialize<CreditcardProcessRequestDto>(decrypted,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).NotNull();
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, "Failed to decrypt ProcessEncrypted request");
            return new Response(ResultEnum.InvalidRequest, "Failed to decrypt request data");
        }

        var validationResult = await ValidateRequestAsync(request, merchant, cancellationToken);
        if (validationResult != null)
            return validationResult;

        var legacyRequest = CreditCardDtoFactory.CreateLegacyProcessRequest(request, merchant);
        var result = await _legacyPaymentService.ProcessAsync(legacyRequest, cancellationToken);
        var response = CreditCardDtoFactory.CreateExtendedResponse(result, request, merchant, _currencyRepository);

        return new Response(result.GetResultStatus(), response);
    }

    [HttpPost("Refund")]
    [MerchantSecurityFilter]
    public async Task<Response> Refund([FromBody] RefundRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        var legacyRequest = CreditCardDtoFactory.CreateLegacyRefundRequest(request, merchant, HttpContext.GetClientIP());
        var result = await _legacyPaymentService.RefundAsync(legacyRequest, cancellationToken);
        var response = CreditCardDtoFactory.CreateRefundResponse(result, _currencyRepository);

        return new Response(result.GetResultStatus(), response);
    }

    [HttpPost("Void")]
    [MerchantSecurityFilter]
    public async Task<Response> Void([FromBody] VoidRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        var legacyRequest = CreditCardDtoFactory.CreateLegacyVoidRequest(request, merchant, HttpContext.GetClientIP());
        var result = await _legacyPaymentService.VoidAsync(legacyRequest, cancellationToken);
        var response = CreditCardDtoFactory.CreateVoidResponse(result, _currencyRepository);

        return new Response(result.GetResultStatus(), response);
    }

    [HttpPost("Capture")]
    [MerchantSecurityFilter]
    public async Task<Response> Capture([FromBody] CaptureRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        var legacyRequest = CreditCardDtoFactory.CreateLegacyCaptureRequest(request, merchant, HttpContext.GetClientIP());
        var result = await _legacyPaymentService.CaptureAsync(legacyRequest, cancellationToken);
        var response = CreditCardDtoFactory.CreateCaptureResponse(result, _currencyRepository);

        return new Response(result.GetResultStatus(), response);
    }

    private async Task<Response?> ValidateRequestAsync(CreditcardProcessRequestDto request, Merchant merchant, CancellationToken cancellationToken)
    {
        if (request.TipAmount != 0)
        {
            if (request.TipAmount < 0)
                return new Response(ResultEnum.InvalidRequest, "Tip amount cannot be negative");

            var isTipAllowed = await _paymentPageSettingsRepository.IsTipAllowedAsync(merchant.Id, cancellationToken);
            if (!isTipAllowed)
                return new Response(ResultEnum.InvalidRequest, "The merchant is not allowed to receive tips, Do not include tipAmount in request");

            if (request.TipAmount >= request.Amount)
                return new Response(ResultEnum.InvalidRequest, "tip Amount should be lesser than the actual amount");
        }

        if (!string.IsNullOrWhiteSpace(request.SavedCardId))
        {
            if (request.SaveCard)
                return new Response(ResultEnum.InvalidRequest, "Cannot save card when trying to use a saved card");
            if (request.AuthorizeOnly)
                return new Response(ResultEnum.InvalidRequest, "Cannot authorize with a saved card");
        }

        return null;
    }
}
