namespace Ezzygate.Application.Transactions;

public interface ITransactionService
{
    Task<MoveTransactionResult> MoveTrxAsync(int pendingId, string replyCode, string message, string? binCountryIso);
}