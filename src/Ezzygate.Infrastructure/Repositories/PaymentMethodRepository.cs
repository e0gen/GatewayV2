using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly EzzygateDbContext _context;

    public PaymentMethodRepository(EzzygateDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<PaymentMethod?> GetByIdAsync(short paymentMethodId)
    {
        var entity = await _context.PaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(pm => pm.PaymentMethodId == paymentMethodId);

        return entity?.ToDomain();
    }

    public async Task<PaymentMethod?> GetByAbbreviationAsync(string abbreviation)
    {
        if (string.IsNullOrWhiteSpace(abbreviation))
            throw new ArgumentException("Abbreviation cannot be null or empty", nameof(abbreviation));

        var entity = await _context.PaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(pm => pm.Abbreviation == abbreviation.Trim());

        return entity?.ToDomain();
    }
}