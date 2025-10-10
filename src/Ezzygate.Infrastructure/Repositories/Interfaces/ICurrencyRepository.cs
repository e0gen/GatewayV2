using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ICurrencyRepository
{
    Currency Get(string isoCode);
    Currency Get(int currencyId);
    Currency Get(CurrencyEnum currencyEnum);
    IEnumerable<Currency> GetAll();
    string GetIsoCode(CurrencyEnum currencyEnum);
    Currency GetByIsoNumber(string isoNumber);

    decimal ConvertAmount(decimal amount, string fromCurrencyIso, string toCurrencyIso);
    decimal ConvertAmount(decimal amount, int fromCurrencyId, int toCurrencyId);
    decimal ConvertRate(string fromCurrencyIso, string toCurrencyIso);
    decimal ConvertRateWithFee(string fromCurrencyIso, string toCurrencyIso);
}