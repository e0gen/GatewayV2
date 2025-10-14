using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Entities;
using Ezzygate.Infrastructure.Extensions;

namespace Ezzygate.Infrastructure.Mappings;

public static class CurrencyProfile
{
    public static Currency ToDomain(this CurrencyList entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new Currency(
            entity.CurrencyId,
            entity.CurrencyIsocode,
            entity.Isonumber,
            entity.Name.NotNullOrEmpty(),
            entity.Symbol.NotNullOrEmpty(),
            entity.BaseRate.NotNull(),
            entity.ExchangeFeeInd.NotNull(),
            entity.RateRequestDate,
            entity.RateValueDate,
            entity.MaxTransactionAmount,
            entity.IsSymbolBeforeAmount);
    }

    public static CurrencyList ToEntity(this Currency domainModel)
    {
        ArgumentNullException.ThrowIfNull(domainModel);

        return new CurrencyList
        {
            CurrencyId = domainModel.Id,
            CurrencyIsocode = domainModel.IsoCode,
            Isonumber = domainModel.IsoNumber,
            Name = domainModel.Name,
            Symbol = domainModel.Symbol,
            BaseRate = domainModel.BaseRate,
            ExchangeFeeInd = domainModel.ExchangeFee,
            RateRequestDate = domainModel.RateRequestDate,
            RateValueDate = domainModel.RateValueDate,
            MaxTransactionAmount = domainModel.MaxTransactionAmount,
            IsSymbolBeforeAmount = domainModel.IsSymbolBeforeAmount
        };
    }
}