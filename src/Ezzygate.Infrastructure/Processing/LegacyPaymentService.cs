using System.Collections.Specialized;
using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using Ezzygate.Application.Configuration;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Processing.Models;

namespace Ezzygate.Infrastructure.Processing;

public class LegacyPaymentService : ILegacyPaymentService
{
    private const int TrxSource = (int)TransactionSource.WebApi;
    private readonly HttpClient _httpClient;
    private readonly IDomainConfiguration _domainConfiguration;
    private readonly ILogger<LegacyPaymentService> _logger;

    public LegacyPaymentService(
        HttpClient httpClient,
        IDomainConfiguration domainConfiguration,
        ILogger<LegacyPaymentService> logger)
    {
        _httpClient = httpClient;
        _domainConfiguration = domainConfiguration;
        _logger = logger;
    }

    public async Task<LegacyPaymentResult> ProcessAsync(LegacyProcessRequest request, CancellationToken cancellationToken = default)
    {
        var url = new StringBuilder();
        url.Append(_domainConfiguration.ProcessUrl);
        url.Append($"remote_charge_v2.asp?CompanyNum={request.MerchantNumber}");
        url.Append($"&CVV2={request.CreditCard?.Cvv}");
        url.Append($"&CardNum={request.CreditCard?.Number}");
        url.Append($"&ExpMonth={request.CreditCard?.ExpirationMonth}");
        url.Append($"&ExpYear={request.CreditCard?.ExpirationYear}");
        url.Append($"&TrmCode={request.TrmCode}");
        url.Append($"&Member={request.CreditCard?.HolderName}");
        url.Append($"&requestSource={TrxSource}");
        url.Append($"&Amount={request.Amount}");
        url.Append($"&Currency={request.CurrencyIso.ToUpper()}");
        url.Append($"&Payments={request.Installments}");
        url.Append($"&TransType={request.TransType}");
        url.Append($"&TypeCredit={request.TypeCredit}");
        url.Append($"&ClientIP={request.ClientIP}");
        url.Append($"&PersonalNum={request.Customer?.PersonalIdNumber}");
        url.Append($"&DateOfBirth={request.Customer?.DateOfBirth}");
        url.Append($"&PhoneNumber={request.Customer?.PhoneNumber}");
        url.Append($"&StoreCc={request.StoreCc}");
        url.Append($"&Email={request.Customer?.Email}");
        url.Append($"&BillingAddress1={request.CreditCard?.BillingAddress?.AddressLine1}");
        url.Append($"&BillingAddress2={request.CreditCard?.BillingAddress?.AddressLine2}");
        url.Append($"&BillingCity={request.CreditCard?.BillingAddress?.City}");
        url.Append($"&BillingZipCode={request.CreditCard?.BillingAddress?.PostalCode}");
        url.Append($"&BillingState={request.CreditCard?.BillingAddress?.StateIso}");
        url.Append($"&BillingCountry={request.CreditCard?.BillingAddress?.CountryIso}");
        url.Append($"&RetURL={request.PostRedirectUrl}");
        url.Append($"&Comment={request.Comment}");
        url.Append($"&ccStorageID={request.SavedCardId}");
        url.Append($"&Order={request.Order}");
        url.Append($"&PayFor={request.OrderDescription}");
        url.Append(BuildRecurringParams(request.Recurring));
        url.Append($"&l3d_arrival_date={request.Level3Data?.ArrivalDate}");

        if (request.TipAmount != 0)
            url.Append($"&TipAmount={request.TipAmount}");

        if (!string.IsNullOrWhiteSpace(request.Comment) && request.Comment.StartsWith("fcm"))
        {
            url.Append("&MobileApiRequest=true");
            if (request.CardPresent)
                url.Append("&MobileApiCardPresent=true");
        }

        return await ExecuteRequestAsync(url.ToString(), cancellationToken);
    }

    public async Task<LegacyPaymentResult> RefundAsync(LegacyRefundRequest request, CancellationToken cancellationToken = default)
    {
        var url = $"{_domainConfiguration.ProcessUrl}remote_charge_v2.asp" +
                  $"?CompanyNum={request.MerchantNumber}" +
                  $"&requestSource={TrxSource}" +
                  $"&Amount={request.Amount}" +
                  $"&Currency={request.CurrencyIso.ToUpper()}" +
                  $"&TransType=0&TypeCredit=0" +
                  $"&ClientIP={request.ClientIP}" +
                  $"&RefTransID={request.TransactionId}";

        return await ExecuteRequestAsync(url, cancellationToken);
    }

    public async Task<LegacyPaymentResult> VoidAsync(LegacyVoidRequest request, CancellationToken cancellationToken = default)
    {
        var url = $"{_domainConfiguration.ProcessUrl}remote_charge_v2.asp" +
                  $"?CompanyNum={request.MerchantNumber}" +
                  $"&requestSource={TrxSource}" +
                  $"&Currency={request.CurrencyIso.ToUpper()}" +
                  $"&TransType=4" +
                  $"&ClientIP={request.ClientIP}" +
                  $"&TransApprovalID={request.TransactionId}";

        return await ExecuteRequestAsync(url, cancellationToken);
    }

    public async Task<LegacyPaymentResult> CaptureAsync(LegacyCaptureRequest request, CancellationToken cancellationToken = default)
    {
        var url = $"{_domainConfiguration.ProcessUrl}remote_charge_v2.asp" +
                  $"?CompanyNum={request.MerchantNumber}" +
                  $"&requestSource={TrxSource}" +
                  $"&Currency={request.CurrencyIso.ToUpper()}" +
                  $"&TransType=2" +
                  $"&ClientIP={request.ClientIP}" +
                  $"&TransApprovalID={request.TransactionId}";

        return await ExecuteRequestAsync(url, cancellationToken);
    }

    private async Task<LegacyPaymentResult> ExecuteRequestAsync(string url, CancellationToken cancellationToken)
    {
        try
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(180);
            var response = await _httpClient.GetStringAsync(url, cancellationToken);

            var parsed = HttpUtility.ParseQueryString(response);
            return ParseResponse(parsed);
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, "Call to remote_charge_v2.asp failed");
            throw;
        }
    }

    private static LegacyPaymentResult ParseResponse(NameValueCollection parsed)
    {
        return new LegacyPaymentResult
        {
            ReplyCode = parsed.Get("Reply")?.Trim() ?? string.Empty,
            ReplyDescription = parsed.Get("ReplyDesc")?.Trim() ?? string.Empty,
            TransactionId = parsed.Get("TransID")?.Trim() ?? string.Empty,
            Currency = parsed.Get("Currency")?.Trim() ?? string.Empty,
            Amount = parsed.Get("Amount")?.Trim() ?? string.Empty,
            Last4 = parsed.Get("Last4")?.Trim(),
            CardType = parsed.Get("CCType")?.Trim(),
            SavedCardId = parsed.Get("ccStorageID")?.Trim(),
            Descriptor = parsed.Get("Descriptor")?.Trim(),
            AuthenticationRedirectUrl = parsed.Get("D3Redirect")?.Trim(),
            RecurringSeries = parsed.Get("RecurringSeries")?.Trim(),
            Order = parsed.Get("Order")?.Trim(),
            Comment = parsed.Get("Comment")?.Trim(),
            Date = parsed.Get("Date")?.Trim(),
            ConfirmationNumber = parsed.Get("ConfirmationNum")?.Trim(),
            ApprovalNumber = parsed.Get("DebitApprovalNumber")?.Trim(),
            AcquirerReferenceNumber = parsed.Get("DebitReferenceNum")?.Trim(),
            TransRefNum = parsed.Get("RefTransID")?.Trim(),
            WalletId = parsed.Get("WalletID")?.Trim(),
            Signature = parsed.Get("signature")?.Trim(),
            Payments = parsed.Get("Payments")?.Trim()
        };
    }

    private static string BuildRecurringParams(LegacyRecurring? recurring)
    {
        if (recurring == null) return string.Empty;

        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(recurring.Recurring1))
            sb.Append($"&Recurring1={recurring.Recurring1}");
        if (!string.IsNullOrWhiteSpace(recurring.Recurring2))
            sb.Append($"&Recurring2={recurring.Recurring2}");
        if (!string.IsNullOrWhiteSpace(recurring.Recurring3))
            sb.Append($"&Recurring3={recurring.Recurring3}");
        if (!string.IsNullOrWhiteSpace(recurring.Recurring4))
            sb.Append($"&Recurring4={recurring.Recurring4}");
        if (!string.IsNullOrWhiteSpace(recurring.Recurring5))
            sb.Append($"&Recurring5={recurring.Recurring5}");
        if (!string.IsNullOrWhiteSpace(recurring.Recurring6))
            sb.Append($"&Recurring6={recurring.Recurring6}");
        return sb.ToString();
    }
}