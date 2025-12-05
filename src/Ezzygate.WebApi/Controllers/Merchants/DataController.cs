using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Ezzygate.Application.Transactions;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Services;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Dtos.Merchants.Data;
using Ezzygate.WebApi.Extensions;
using Ezzygate.WebApi.Filters;

namespace Ezzygate.WebApi.Controllers.Merchants;

[ApiController]
[Route("api/merchants/[controller]")]
[ApiVersion("3.0")]
[ApiVersion("4.0")]
public class DataController : ControllerBase
{
    private readonly ILogger<DataController> _logger;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionService _transactionService;

    public DataController(
        ILogger<DataController> logger,
        ITransactionRepository transactionRepository,
        ITransactionService transactionService)
    {
        _logger = logger;
        _transactionRepository = transactionRepository;
        _transactionService = transactionService;
    }

    [HttpPost("Version")]
    [MerchantSecurityFilter(false)]
    [MapToApiVersion("3.0")]
    public Response Version3() => new(ResultEnum.Success, "3.00");

    [HttpPost("Version")]
    [MerchantSecurityFilter(false)]
    [MapToApiVersion("4.0")]
    public Response Version4() => new(ResultEnum.Success, "4.00");

    [HttpPost("PendingData")]
    [MerchantSecurityFilter]
    public async Task<Response> PendingData([FromBody] PendingStatusRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        var lookupResult = await _transactionService.LocatePendingAsync(request.TransactionId, merchant.Id);
        if (lookupResult == null)
            return new Response("TransactionNotFound");

        var results = await _transactionRepository.SearchTransactionsAsync(
            merchant.Id,
            lookupResult.Status,
            lookupResult.TransactionId);

        var trx = results.SingleOrDefault();
        if (trx == null)
            return new Response("TransactionNotFound");

        var response = trx.ToTransactionInfoResponseDto();
        return new Response(ResultEnum.Success, response);
    }

    [HttpPost("PendingStatus")]
    [MerchantSecurityFilter]
    public async Task<Response> PendingStatus([FromBody] PendingStatusRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        var lookupResult = await _transactionService.LocatePendingAsync(request.TransactionId, merchant.Id);
        if (lookupResult == null)
            return new Response("TransactionNotFound");

        var response = new PendingStatusResponseDto
        {
            TransactionId = lookupResult.TransactionId,
            Status = lookupResult.Status.ToString()
        };

        return new Response(ResultEnum.Success, response);
    }

    [HttpPost("Transactions")]
    [MerchantSecurityFilter]
    public async Task<Response> Transactions([FromBody] TransactionInfoRequestDto request, CancellationToken cancellationToken)
    {
        var merchant = HttpContext.GetMerchant();
        if (merchant == null)
            return new Response(ResultEnum.MerchantNotFound);

        List<TransactionSearchResult> results;
        try
        {
            results = await _transactionRepository.SearchTransactionsAsync(
                merchant.Id,
                request.Status,
                request.TransactionId,
                request.From,
                request.To);
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, "Error searching transactions for merchant {MerchantId}", merchant.Id);
            return new Response(ResultEnum.GeneralError);
        }

        var responseResults = results.Select(x => x.ToTransactionInfoResponseDto()).ToList();

        _logger.LogInformation(
            "Transactions search: merchant id:{MerchantId}, type:{Status}, from:{From}, to:{To}, count:{Count}",
            merchant.Id, request.Status, request.From, request.To, responseResults.Count);

        return new Response(ResultEnum.Success, responseResults);
    }
}
