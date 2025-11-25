using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Interfaces;
using Ezzygate.Domain.Models;
using Ezzygate.Domain.Services;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Services;

public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardBinRepository _binRepository;

    public CreditCardService(ICreditCardBinRepository binRepository)
    {
        _binRepository = binRepository ?? throw new ArgumentNullException(nameof(binRepository));
    }

    public async Task<CreditCard> FromCardNumberAsync(string cardNumber, CancellationToken cancellationToken = default)
    {
        var paymentMethodId = PaymentMethodEnum.CCUnknown;
        string? binCountryIsoCode = null;
        string? brandName = null;

        var binInfo = await _binRepository.FindByCardNumberAsync(cardNumber, cancellationToken);
        if (binInfo != null)
        {
            paymentMethodId = binInfo.PaymentMethodId;
            binCountryIsoCode = binInfo.CountryIsoCode;
            brandName = binInfo.BrandName;
        }

        return new CreditCard
        {
            CardNumber = cardNumber,
            PaymentMethodId = paymentMethodId,
            BinCountryIsoCode = binCountryIsoCode,
            BrandName = brandName
        };
    }

    public bool ValidateCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber)) return false;
        if (cardNumber.Trim() == "") return false;

        if (cardNumber.Length is 8 or 9)
            return ValidateIsracard(cardNumber);

        if (cardNumber.Length is >= 13 and <= 16)
        {
            // LiveCash special case - doesn't support Luhn
            if (cardNumber.StartsWith("629971"))
                return true;

            try
            {
                return ValidateLuhn(cardNumber);
            }
            catch
            {
                return false;
            }
        }

        return false;
    }

    public async Task<string?> GetBinCountryIsoCodeAsync(string cardNumber,
        CancellationToken cancellationToken = default)
    {
        var binInfo = await _binRepository.FindByCardNumberAsync(cardNumber, cancellationToken);
        return binInfo?.CountryIsoCode;
    }

    private static bool ValidateLuhn(string cardNumber)
    {
        var cardSize = cardNumber.Length;
        if (cardSize < 13 || cardSize > 16) return false;

        var checkDigit = GetLuhnCheckDigit(cardNumber.Substring(0, cardSize - 1));
        return checkDigit == cardNumber[cardSize - 1];
    }

    private static char GetLuhnCheckDigit(string cardNumber)
    {
        var sum = 0;
        var mul = cardNumber.Length % 2 == 1;

        for (var i = 0; i < cardNumber.Length; i++, mul = !mul)
        {
            var num = cardNumber[i] - '0';
            if (num < 0 || num > 9)
                throw new ArgumentOutOfRangeException(nameof(cardNumber), "must be numeric");

            if (mul)
            {
                num *= 2;
                if (num > 9) num = (num % 10) + 1;
            }

            sum += num;
        }

        var ret = 10 - sum % 10;
        if (ret == 10) return '0';
        return (char)('0' + ret);
    }

    private static bool ValidateIsracard(string cardNumber)
    {
        if (cardNumber.Length == 8)
            cardNumber = "0" + cardNumber;

        var total = 0;
        var index = 0;

        for (sbyte reverseIndex = 8; reverseIndex >= 0; reverseIndex--)
        {
            index++;
            var currentDigit = sbyte.Parse(cardNumber[reverseIndex].ToString());
            total += index * currentDigit;
        }

        var isValid = total > 0 && Math.Round((double)total / 11) * 11 == total;
        return isValid;
    }

    public static string GenerateCardNumber(string cardPrefix)
    {
        var num = new Random().Next(0, int.MaxValue);
        var ret = cardPrefix + num.ToString().PadLeft(15, '0');

        ret = ret.Length > 15
            ? ret[..15]
            : ret.PadLeft(15, '0');

        ret = ret[..15];
        ret += GetLuhnCheckDigit(ret);
        return ret;
    }
}