using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByIdAsync(short paymentMethodId);
    Task<PaymentMethod?> GetByAbbreviationAsync(string abbreviation);
}
