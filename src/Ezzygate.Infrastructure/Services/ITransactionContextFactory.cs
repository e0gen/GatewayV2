namespace Ezzygate.Infrastructure.Services;

public interface ITransactionContextFactory
{
    Task<TransactionContext> CreateAsync(string referenceCode, int logChargeAttemptId);
    
    Task<TransactionContext> CreateAsync(int terminalId);
}