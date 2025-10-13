using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly EzzygateDbContext _context;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "Currencies_Cache";
    private static readonly SemaphoreSlim CacheLock = new(1, 1);

    public CurrencyRepository(
        EzzygateDbContext context,
        IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public Currency Get(string isoCode)
    {
        var currencies = GetAllCached();
        return currencies.Single(c =>
            string.Equals(c.IsoCode, isoCode, StringComparison.OrdinalIgnoreCase));
    }

    public Currency Get(int currencyId)
    {
        var currencies = GetAllCached();
        return currencies.Single(c => c.Id == currencyId);
    }

    public Currency Get(CurrencyEnum currencyEnum)
    {
        return Get((int)currencyEnum);
    }

    public IEnumerable<Currency> GetAll() => GetAllCached();

    public string GetIsoCode(CurrencyEnum currencyEnum)
    {
        var currency = Get(currencyEnum);
        return currency.IsoCode;
    }

    public Currency GetByIsoNumber(string isoNumber)
    {
        var currencies = GetAllCached();
        return currencies.Single(c =>
            string.Equals(c.IsoNumber, isoNumber, StringComparison.OrdinalIgnoreCase));
    }

    public decimal ConvertAmount(decimal amount, string fromCurrencyIso, string toCurrencyIso)
    {
        var fromCurrency = Get(fromCurrencyIso);
        var toCurrency = Get(toCurrencyIso);
        return amount * (fromCurrency.BaseRate / toCurrency.BaseRate);
    }

    public decimal ConvertAmount(decimal amount, int fromCurrencyId, int toCurrencyId)
    {
        var fromCurrency = Get(fromCurrencyId);
        var toCurrency = Get(toCurrencyId);
        return amount * (fromCurrency.BaseRate / toCurrency.BaseRate);
    }

    public decimal ConvertRate(string fromCurrencyIso, string toCurrencyIso)
    {
        var fromCurrency = Get(fromCurrencyIso);
        var toCurrency = Get(toCurrencyIso);
        return fromCurrency.BaseRate / toCurrency.BaseRate;
    }

    public decimal ConvertRateWithFee(string fromCurrencyIso, string toCurrencyIso)
    {
        var fromCurrency = Get(fromCurrencyIso);
        var toCurrency = Get(toCurrencyIso);
        return fromCurrency.BaseRate * ((1 - toCurrency.ExchangeFee) / toCurrency.BaseRate);
    }

    private IReadOnlyList<Currency> GetAllCached()
    {
        if (!_cache.TryGetValue(CacheKey, out IReadOnlyList<Currency>? cachedCurrencies))
        {
            CacheLock.Wait();
            try
            {
                if (!_cache.TryGetValue(CacheKey, out cachedCurrencies))
                {
                    var entities = _context.CurrencyLists
                        .AsNoTracking()
                        .ToList();

                    cachedCurrencies = entities
                        .Select(e => e.ToDomain())
                        .ToList();

                    _cache.Set(CacheKey, cachedCurrencies, TimeSpan.FromHours(1));
                }
            }
            finally
            {
                CacheLock.Release();
            }
        }

        return cachedCurrencies!;
    }
}