using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class CurrencyListTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.Currencies.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var currency = await Context.Currencies
            .FirstOrDefaultAsync();

        if (currency != null)
        {
            Assert.DoesNotThrow(() => _ = currency.CurrencyISOCode);
            Assert.DoesNotThrow(() => _ = currency.Name);
            Assert.DoesNotThrow(() => _ = currency.Symbol);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var currency = await Context.Currencies
            .Include(c => c.Accounts.Take(3))
            .Include(c => c.AccountBalances.Take(3))
            .FirstOrDefaultAsync();

        if (currency != null)
        {
            Assert.DoesNotThrow(() => _ = currency.Accounts);
            Assert.DoesNotThrow(() => _ = currency.AccountBalances);
        }
    }
}