using System.Text.RegularExpressions;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.QrMoney.Models;

namespace Ezzygate.Integrations.QrMoney.Api;

public static class QrMoneyApiFactory
{
    public static QrMoneyPaymentRequest CreatePaymentRequest(TransactionContext ctx, string originDomain)
    {
        var request = new QrMoneyPaymentRequest
        {
            Name = ctx.PayerName,
            Number = ctx.CardNumber,
            Expiration = ctx.Expiration.SlashMMYY,
            Cvv = ctx.Cvv,
            Email = ctx.Email,
            PhoneNumber = Regex.Replace(ctx.PhoneNumber ?? string.Empty, "[^0-9]", ""),
            Address = ctx.BillingAddress.AddressLine1,
            City = ctx.BillingAddress.City,
            State = string.IsNullOrEmpty(ctx.BillingAddress.StateIsoCode)
                ? ctx.BillingAddress.CountryIsoCode
                : ctx.BillingAddress.StateIsoCode,
            PostalCode = ctx.BillingAddress.PostalCode,
            Country = ctx.BillingAddress.CountryIsoCode,
            BirthDate = ctx.DateOfBirth,
            Amount = ctx.Amount,
            Unit = ctx.CurrencyIso,
            OriginDomain = originDomain,
            ReferenceId = ctx.DebitRefCode,
            NotifyUrl = ctx.GetCallbackUrl("qr-money"),
            SuccessUrl = ctx.GetFinalizeUrl(FinalizeUrlType.SuccessRedirect),
            FailureUrl = ctx.GetFinalizeUrl(FinalizeUrlType.FailureRedirect),
            BrowserInfo = new BrowserInfo
            {
                BrowserAcceptHeader = "browserAcceptContent=text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7",
                BrowserColorDepth = "24",
                BrowserIp = "5.77.205.118",
                BrowserJavaEnabled = false,
                BrowserJavascriptEnabled = true,
                BrowserLanguage = "en-US",
                BrowserScreenHeight = "982",
                BrowserScreenWidth = "1512",
                BrowserTz = "-240",
                BrowserUserAgent = "Mozilla/5.0+(Macintosh;+Intel+Mac+OS+X+10_15_7)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/136.0.0.0+Safari/537.36",
            }
        };

        return request;
    }
}