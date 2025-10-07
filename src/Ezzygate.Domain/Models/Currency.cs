namespace Ezzygate.Domain.Models;

public class Currency
{
    public int Id { get; private set; }
    public string IsoCode { get; private set; }
    public string IsoNumber { get; private set; }
    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public decimal BaseRate { get; private set; }
    public decimal ExchangeFee { get; private set; }
    public DateTime? RateRequestDate { get; private set; }
    public DateTime? RateValueDate { get; private set; }
    public decimal? MaxTransactionAmount { get; private set; }
    public bool? IsSymbolBeforeAmount { get; private set; }

    public Currency(
        int? id,
        string isoCode,
        string isoNumber,
        string name,
        string symbol,
        decimal baseRate,
        decimal exchangeFee,
        DateTime? rateRequestDate,
        DateTime? rateValueDate,
        decimal? maxTransactionAmount,
        bool? isSymbolBeforeAmount)
    {
        if (string.IsNullOrWhiteSpace(isoCode))
            throw new ArgumentException("ISO code is required", nameof(isoCode));
                
        if (string.IsNullOrWhiteSpace(isoNumber))
            throw new ArgumentException("ISO number is required", nameof(isoNumber));

        Id = id ?? 0;
        IsoCode = isoCode;
        IsoNumber = isoNumber;
        Name = name;
        Symbol = symbol;
        BaseRate = baseRate;
        ExchangeFee = exchangeFee;
        RateRequestDate = rateRequestDate;
        RateValueDate = rateValueDate;
        MaxTransactionAmount = maxTransactionAmount;
        IsSymbolBeforeAmount = isSymbolBeforeAmount;
    }
}