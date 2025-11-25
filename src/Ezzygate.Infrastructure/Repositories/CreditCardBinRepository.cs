using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class CreditCardBinRepository : ICreditCardBinRepository
{
    private readonly EzzygateDbContext _context;

    public CreditCardBinRepository(EzzygateDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreditCardBinInfo?> FindByCardNumberAsync(string cardNumber,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(cardNumber))
            return null;

        var entity = await _context.TblCreditCardBins
            .Where(b => EF.Functions.Like(cardNumber, b.Bin + "%"))
            .OrderByDescending(b => b.BinLen) // Prioritize longer BINs for better matches
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
            return null;

        return new CreditCardBinInfo
        {
            BinId = entity.BinId,
            Bin = entity.Bin,
            CountryIsoCode = entity.IsoCode,
            PaymentMethodId = (PaymentMethodEnum)entity.PaymentMethod,
            BrandName = entity.CcName,
            BinLength = entity.BinLen,
            BinNumber = entity.BinNumber,
            IsPrepaid = entity.IsPrepaid ?? false
        };
    }
}

