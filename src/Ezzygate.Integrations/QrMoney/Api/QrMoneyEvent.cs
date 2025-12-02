using Microsoft.Extensions.Primitives;

namespace Ezzygate.Integrations.QrMoney.Api;

public class QrMoneyEvent
{
    public QrMoneyEvent(IEnumerable<KeyValuePair<string, StringValues>> formData)
    {
        var dict = formData.ToDictionary(x => x.Key, x => x.Value.ToString());

        TransactionId = dict.GetValueOrDefault("transactionId");
        StatusId = dict.GetValueOrDefault("transactionStatusId");
        PaymentRequestStatus = dict.GetValueOrDefault("paymentRequestStatus");
        MerchantId = dict.GetValueOrDefault("merchantId");
        Amount = dict.GetValueOrDefault("grossAmount");
        Fee = dict.GetValueOrDefault("fee");
        NetAmount = dict.GetValueOrDefault("netAmount");
        ReferenceId = dict.GetValueOrDefault("referenceId");
        Notes = dict.GetValueOrDefault("notes");
        ClientId = dict.GetValueOrDefault("clientId");
        ClientName = dict.GetValueOrDefault("clientName");
        ClientEmail = dict.GetValueOrDefault("clientEmail");
        ClientPhone = dict.GetValueOrDefault("clientPhone");
        ClientMemberId = dict.GetValueOrDefault("clientMemberId");
        Message = dict.GetValueOrDefault("message");
    }

    public string? TransactionId { get; set; }
    public string? StatusId { get; set; }
    public string? PaymentRequestStatus { get; set; }
    public string? MerchantId { get; set; }
    public string? Amount { get; set; }
    public string? Fee { get; set; }
    public string? NetAmount { get; set; }
    public string? ReferenceId { get; set; }
    public string? Notes { get; set; }
    public string? ClientId { get; set; }
    public string? ClientName { get; set; }
    public string? ClientEmail { get; set; }
    public string? ClientPhone { get; set; }
    public string? ClientMemberId { get; set; }
    public string? Message { get; set; }
}