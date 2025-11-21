namespace Ezzygate.Application.Transactions;

public class PendingTransaction
{
    public int CompanyTransPendingId { get; set; }

    public int CompanyBatchFilesId { get; set; }

    public int TransactionSourceId { get; set; }

    public int CustomerId { get; set; }

    public int FraudDetectionLogId { get; set; }

    public DateTime InsertDate { get; set; }

    public string TerminalNumber { get; set; }

    public int PaymentMethodId { get; set; }

    public string PaymentMethodDisplay { get; set; }

    public string Ipaddress { get; set; }

    public string ReplyCode { get; set; }

    public decimal TransAmount { get; set; }

    public byte TransCreditType { get; set; }

    public byte TransPayments { get; set; }

    public int TransOriginalId { get; set; }

    public string TransOrder { get; set; }

    public string DebitReferenceCode { get; set; }

    public string DebitApprovalNumber { get; set; }

    public byte Locked { get; set; }

    public int Id { get; set; }

    public string PayerIdUsed { get; set; }

    public string OrderNumber { get; set; }

    public short? PaymentMethod { get; set; }

    public int? TransCurrency { get; set; }
    public int? Currency { get; set; }

    public int CompanyId { get; set; }

    public int? CompanyId1 { get; set; }

    public int? DebitCompanyId { get; set; }

    public int? IdNew { get; set; }

    public bool IsTestOnly { get; set; }

    public int TransType { get; set; }

    public string? DebitReferenceNum { get; set; }

    public int? OriginalTransId { get; set; }

    public byte? TransSourceId { get; set; }

    public int? MobileDeviceId { get; set; }
    public string? Comment { get; set; }

    public string? SystemText { get; set; }

    public string? PayForText { get; set; }

    public int? CreditCardId { get; set; }

    public int? CheckDetailsId { get; set; }

    public int? PhoneDetailsId { get; set; }

    public int? MerchantProductId { get; set; }

    public int? PayerInfoId { get; set; }

    public int? TransPayerInfoId { get; set; }

    public int? TransPaymentMethodId { get; set; }

    public bool? Is3DSecure { get; set; }

    public string? AcquirerReferenceNum { get; set; }

    public bool? IsCardPresent { get; set; }

    public byte? OCurrency { get; set; }

    public decimal? OAmount { get; set; }

    public int? CcStorageId { get; set; }

    public string? TextValue { get; set; }
}