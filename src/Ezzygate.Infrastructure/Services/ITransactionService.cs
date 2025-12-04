using Ezzygate.Application.Transactions;

namespace Ezzygate.Infrastructure.Services;

public interface ITransactionService
{
    Task<MoveTransactionResult> MoveTrxAsync(int pendingId, string replyCode, string message, string? binCountryIso);
}