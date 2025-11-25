using Ezzygate.Domain.Models;

namespace Ezzygate.Domain.Services;

public interface ICreditCardService
{
    Task<CreditCard> FromCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default);
    bool ValidateCardNumber(string cardNumber);
    Task<string?> GetBinCountryIsoCodeAsync(string cardNumber, CancellationToken cancellationToken = default);
}