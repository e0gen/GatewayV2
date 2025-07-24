using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class AccountTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.Accounts.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var account = await Context.Accounts
            .FirstOrDefaultAsync();

        if (account != null)
        {
            Assert.DoesNotThrow(() => _ = account.AccountId);
            Assert.DoesNotThrow(() => _ = account.AccountTypeId);
            Assert.DoesNotThrow(() => _ = account.MerchantId);
            Assert.DoesNotThrow(() => _ = account.CustomerId);
            Assert.DoesNotThrow(() => _ = account.AffiliateId);
            Assert.DoesNotThrow(() => _ = account.DebitCompanyId);
            Assert.DoesNotThrow(() => _ = account.PersonalAddressId);
            Assert.DoesNotThrow(() => _ = account.BusinessAddressId);
            Assert.DoesNotThrow(() => _ = account.PreferredWireProviderId);
            Assert.DoesNotThrow(() => _ = account.AccountNumber);
            Assert.DoesNotThrow(() => _ = account.LoginAccountId);
            Assert.DoesNotThrow(() => _ = account.PincodeSHA256);
            Assert.DoesNotThrow(() => _ = account.Name);
            Assert.DoesNotThrow(() => _ = account.HashKey);
            Assert.DoesNotThrow(() => _ = account.TimeZoneOffsetUI);
            Assert.DoesNotThrow(() => _ = account.DefaultCurrencyISOCode);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var account = await Context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.Customer)
            .Include(a => a.LoginAccount)
            .Include(a => a.PersonalAddress)
            .Include(a => a.BusinessAddress)
            .Include(a => a.DefaultCurrency)
            .Include(a => a.AccountBalances.Take(3))
            .Include(a => a.MobileDevices.Take(3))
            .Include(a => a.AccountSubUsers.Take(3))
            .Include(a => a.AccountPaymentMethods.Take(3))
            .FirstOrDefaultAsync();

        if (account != null)
        {
            Assert.DoesNotThrow(() => _ = account.AccountType);
            Assert.DoesNotThrow(() => _ = account.Customer);
            Assert.DoesNotThrow(() => _ = account.LoginAccount);
            Assert.DoesNotThrow(() => _ = account.PersonalAddress);
            Assert.DoesNotThrow(() => _ = account.BusinessAddress);
            Assert.DoesNotThrow(() => _ = account.DefaultCurrency);
            Assert.DoesNotThrow(() => _ = account.AccountBalances);
            Assert.DoesNotThrow(() => _ = account.MobileDevices);
            Assert.DoesNotThrow(() => _ = account.AccountSubUsers);
            Assert.DoesNotThrow(() => _ = account.AccountPaymentMethods);
        }
    }
}