using Ezzygate.Domain.Enums;

namespace Ezzygate.Application.Transactions;

public record PendingLookupResult(TransactionStatusType Status, int TransactionId);