namespace Ezzygate.Application.Transactions;

public record MoveTransactionResult(int TrxId, PendingTransaction PendingTrx);