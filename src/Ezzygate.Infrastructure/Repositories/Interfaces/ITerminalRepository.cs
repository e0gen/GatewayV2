using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITerminalRepository
{
    Task<Terminal?> GetByTerminalNumberAsync(string terminalNumber);
    Task<Terminal?> GetByIdAsync(int terminalId);
    Task<DebitCompany?> GetDebitCompanyByIdAsync(byte debitCompanyId);
}