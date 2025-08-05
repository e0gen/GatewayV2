using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Services;
using Ezzygate.WebApi.Extensions;
using Ezzygate.WebApi.Filters;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.WebApi.Controllers
{
    [ApiController]
    [Route("api/apps/Integration")]
    public class IntegrationController : ControllerBase
    {
        private readonly ILogger<IntegrationController> _logger;
        private readonly ITransactionContextFactory _transactionContextFactory;

        public IntegrationController(ILogger<IntegrationController> logger, ITransactionContextFactory transactionContextFactory)
        {
            _logger = logger;
            _transactionContextFactory = transactionContextFactory;
        }

        [HttpGet, HttpPost]
        public IActionResult NotificationLoopback()
        {
            var query = Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty;
            var post = Request.GetRequestContent();
            
            _logger.LogInformation($"Notification loopback - Query: {query}, Post: {post}");
            
            return Ok("NotificationLoopback OK");
        }

        [HttpPost]
        [Route("Process")]
        //[ServiceFilter(typeof(IntegrationSecurityFilter))]
        public async Task<IActionResult> Process([FromBody] IntegrationProcessRequest request)
        {
            try
            {
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
                {
                    ctx.Level3DataArrivalDate = l3dArrivalDate.ToString();
                }
                ctx.IsMobileMoto = !string.IsNullOrEmpty(request.Comment) && 
                                 request.Comment.StartsWith("fcm") && 
                                 ctx.RequestSource == TransactionSource.WebApi;


                _logger.LogInformation("Processing integration request for DebitRefCode: {DebitRefCode}", ctx.DebitRefCode);

                // TODO: Replace with actual service call
                // var result = await _creditCardIntegration.ProcessAsync(ctx);
                var result = new IntegrationResult
                {
                    Code = "000",
                    Message = "Success",
                    DebitRefCode = ctx.DebitRefCode
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing integration request");
                return StatusCode(520, new IntegrationResult
                {
                    Code = "520",
                    Message = "[002] Internal server error, see logs",
                    DebitRefCode = request.DebitRefCode
                });
            }
        }
    }
}
