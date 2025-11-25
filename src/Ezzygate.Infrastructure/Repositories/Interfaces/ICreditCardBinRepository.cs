using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ICreditCardBinRepository
{
    Task<CreditCardBinInfo?> FindByCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default);
}