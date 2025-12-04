namespace Ezzygate.Application.Transactions;

public record TransactionSearchResult(
    int Id,
    string Status,
    decimal Amount,
    string? ApprovalCode,
    string? PaymentMethodDisplay,
    DateTime InsertDate,
    string? CurrencyIso,
    int Installments,
    string? Comment,
    string? ResponseCode,
    string? ResponseMessage,
    string? DebitReferenceCode,
    string? OrderNumber,
    string? PayerFirstName,
    string? PayerLastName,
    string? PayerPersonalNumber,
    string? PayerEmail,
    string? PayerPhone);