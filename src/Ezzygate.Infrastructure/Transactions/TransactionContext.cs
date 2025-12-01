using Microsoft.AspNetCore.WebUtilities;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Extensions;

namespace Ezzygate.Infrastructure.Transactions;

public class TransactionContext
{
    private readonly DomainConfiguration _domainConfiguration;
    private const string SignatureKey = "cf396375-5eaa-494f-a783-1ba1e05be7af";

    public TransactionContext(DomainConfiguration domainConfiguration)
    {
        _domainConfiguration = domainConfiguration;
    }

    public OperationType OpType { get; set; }

    public bool Is3ds => OpType == OperationType.Authorization3DS ||
                         OpType == OperationType.Sale3DS ||
                         OpType == OperationType.RecurringInit3DS;

    public TransactionContext? LocatedTrx { get; init; }
    public Terminal? Terminal { get; init; }
    public DebitCompany? DebitCompany { get; init; }
    public PaymentMethod? PaymentMethod { get; init; }
    public int ChargeAttemptLogId { get; set; }
    public string DebitRefCode { get; set; } = string.Empty;
    public string? SentDebitRefCode { get; set; } = string.Empty;
    public string? ApprovalNumber { get; set; } = string.Empty;
    public string? DebitRefNum { get; set; } = string.Empty;
    public bool IsTestTerminal => Terminal?.IsTestTerminal ?? false;
    public string TerminalNumber => Terminal?.TerminalNumber ?? string.Empty;
    public string TerminalAccount => Terminal?.AccountId ?? string.Empty;
    public string TerminalAccount3D => Terminal?.AccountId3D ?? string.Empty;
    public string TerminalSubAccount => Terminal?.AccountSubId ?? string.Empty;
    public string TerminalSubAccount3D => Terminal?.AccountSubId3D ?? string.Empty;
    public string TerminalAuthCode => Terminal?.AuthenticationCode1 ?? string.Empty;
    public string TerminalAuthCode3D => Terminal?.AuthenticationCode3D ?? string.Empty;
    public bool IsAutomatedRequest { get; set; }
    public string? AutomatedStatus { get; set; } = string.Empty;
    public string? AutomatedCode { get; set; } = string.Empty;
    public string? AutomatedMessage { get; set; } = string.Empty;
    public object? AutomatedPayload { get; set; }
    public int TrxId { get; set; }
    public DateTime TrxDate { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
    public string Level3DataArrivalDate { get; set; } = string.Empty;
    public string PendingParams { get; set; } = string.Empty;
    public bool IsFinalized { get; set; }
    public string ReplyCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string BinCountry { get; set; } = string.Empty;
    public string PayFor { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    
    public uint AmountInteger => GetAmountInteger(Amount);

    public uint GetAmountInteger(decimal amount)
    {
        return (uint)Math.Round(amount * 100);
    }
    public string CurrencyIso { get; set; } = string.Empty;
    public byte Payments { get; set; }
    public string? PayerName { get; set; } = string.Empty;

    public string PayerFirstName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PayerName))
                return string.Empty;

            var parts = PayerName.Trim().Split([' '], StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0] : string.Empty;
        }
    }

    public string PayerLastName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PayerName))
                return string.Empty;

            var parts = PayerName.Trim().Split([' '], StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 1 ? parts[^1] : string.Empty;
        }
    }

    public string? CardNumber { get; set; } = string.Empty;
    public string? Cvv { get; set; } = string.Empty;
    public string? Track2 { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public Address BillingAddress { get; set; }
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public string? PersonalIdNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? ClientIp { get; set; } = string.Empty;
    public string? MerchantNumber { get; set; } = string.Empty;
    public string? OrderId { get; set; } = string.Empty;
    public string? CartId { get; set; } = string.Empty;
    public string? CustomerId { get; set; }
    public string? DateOfBirth { get; set; }
    public string? RequestContent { get; set; }
    public string? FormData { get; set; } = string.Empty;
    public string? QueryString { get; set; } = string.Empty;

    private Dictionary<string, string?>? _queryStringParsed;
    private Dictionary<string, string?>? _formDataParsed;

    public Dictionary<string, string?> QueryStringParsed
    {
        get
        {
            if (_queryStringParsed != null) return _queryStringParsed;
            _queryStringParsed = new Dictionary<string, string?>();
            if (string.IsNullOrEmpty(QueryString)) return _queryStringParsed;

            var parsed = QueryHelpers.ParseQuery(QueryString);
            foreach (var kvp in parsed)
                _queryStringParsed[kvp.Key] = kvp.Value.ToString();

            return _queryStringParsed;
        }
    }

    public Dictionary<string, string?> FormDataParsed
    {
        get
        {
            if (_formDataParsed != null) return _formDataParsed;
            _formDataParsed = new Dictionary<string, string?>();
            if (string.IsNullOrEmpty(FormData)) return _formDataParsed;

            var parsed = QueryHelpers.ParseQuery(FormData);
            foreach (var kvp in parsed)
                _formDataParsed[kvp.Key] = kvp.Value.ToString();

            return _formDataParsed;
        }
    }
    public decimal OriginalAmount { get; set; }
    public uint OriginalAmountInteger => GetAmountInteger(OriginalAmount);
    public TransactionSource? RequestSource { get; set; }
    public int TransType { get; set; }
    public int CreditType { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public string? RoutingNumber { get; set; } = string.Empty;
    public string? AccountNumber { get; set; } = string.Empty;
    public string? AccountName { get; set; } = string.Empty;
    public bool IsMobileMoto { get; set; }
    private ExpirationFormatter? _expiration;
    public ExpirationFormatter Expiration => _expiration ??= new ExpirationFormatter(this);

    public class ExpirationFormatter
    {
        private readonly TransactionContext _card;

        internal ExpirationFormatter(TransactionContext card) => _card = card;

        public string MM => _card.ExpirationMonth.ToString("D2");
        public string M => _card.ExpirationMonth.ToString();

        public string YY => (_card.ExpirationYear % 100).ToString("D2");
        public string YYYY => _card.ExpirationYear.ToString("D4");

        public string MMYY => $"{MM}{YY}";
        public string MMYYYY => $"{MM}{YYYY}";
        public string YYMM => $"{YY}{MM}";
        public string SlashMMYY => $"{MM}/{YY}";
    }

    public string GetMerchantUrl() => _domainConfiguration.MerchantUrl;

    public string GetCollectUrl(string actionUrl, string? integrationTag = null)
    {
        var baseUrl = _domainConfiguration.ProcessUrl;
        const string fileName = "remoteCharge_ccDebitGenericCollect.asp";
        integrationTag ??= "direct";
        var finalizeUrl = $"{baseUrl}{fileName}?action={actionUrl.ToEncodedUrl()}&integration={integrationTag}";
        return finalizeUrl;
    }

    public string GetFinalizeUrl(FinalizeUrlType urlType, string? baseUrl = null, string? fileName = null)
    {
        baseUrl ??= _domainConfiguration.ProcessUrl;
        fileName ??= "remoteCharge_ccDebitGenericFinalize.asp";
        var signature = (urlType + DebitRefCode + ChargeAttemptLogId + SignatureKey).ToSha256();
        var finalizeUrl =
            $"{baseUrl}{fileName}?transactionReferenceCode={DebitRefCode}&type={urlType.ToString()}&transactionLogId={ChargeAttemptLogId}&signature={signature.ToEncodedUrl()}";
        return finalizeUrl;
    }

    public bool CheckFinalizeUrl()
    {
        var referenceCode = QueryStringParsed["transactionReferenceCode"];
        var logId = QueryStringParsed["transactionLogId"];
        var type = QueryStringParsed["type"];
        var signature = QueryStringParsed["signature"];

        var calculatedSignature = string.Concat(type, referenceCode, logId, SignatureKey).ToSha256();
        return string.Equals(calculatedSignature, signature, StringComparison.OrdinalIgnoreCase);
    }

    public FinalizeUrlType FinalizeType
    {
        get
        {
            var type = QueryStringParsed["type"];
            return type != null ? Enum.Parse<FinalizeUrlType>(type) : FinalizeUrlType.Unknown;
        }
    }

    public IntegrationResult GetIntegrationResult()
    {
        return new IntegrationResult
        {
            Code = ReplyCode,
            Message = ErrorMessage,
            ApprovalNumber = ApprovalNumber,
            DebitRefCode = DebitRefCode,
            DebitRefNum = DebitRefNum,
            TerminalNumber = TerminalNumber,
            TrxId = TrxId,
            DebitCompanyId = DebitCompany.Id,
            IsFinalized = IsFinalized,
            TrxType = TransType,
            RedirectUrl = RedirectUrl
        };
    }
}