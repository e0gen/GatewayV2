namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IRecurringRepository
{
    Task<int?> RecurringPreCreateSeries(
        bool isPassTransaction,
        int transactionId,
        string recurringParams,
        string comments,
        int terminalCode,
        string clientIp,
        int recurringApprovalType = -1,
        string identifier = "",
        CancellationToken cancellationToken = default);
}