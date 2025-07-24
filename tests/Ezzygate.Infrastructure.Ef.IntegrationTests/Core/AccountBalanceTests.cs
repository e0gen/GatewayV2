using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class AccountBalanceTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountBalances.CountAsync(); });
    }

    [Test]
    public async Task Properties_ShouldBeMappedCorrectly()
    {
        var balance = await Context.AccountBalances
            .FirstOrDefaultAsync();

        if (balance != null)
        {
            Assert.DoesNotThrow(() => _ = balance.AccountBalanceId);
            Assert.DoesNotThrow(() => _ = balance.AccountId);
            Assert.DoesNotThrow(() => _ = balance.BalanceSourceTypeId);
            Assert.DoesNotThrow(() => _ = balance.SourceID);
            Assert.DoesNotThrow(() => _ = balance.CurrencyISOCode);
            Assert.DoesNotThrow(() => _ = balance.Amount);
            Assert.DoesNotThrow(() => _ = balance.TotalBalance);
            Assert.DoesNotThrow(() => _ = balance.InsertDate);
            Assert.DoesNotThrow(() => _ = balance.SystemText);
            Assert.DoesNotThrow(() => _ = balance.IsPending);
            Assert.DoesNotThrow(() => _ = balance.IsCredit);
            Assert.DoesNotThrow(() => _ = balance.IsDebit);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var balance = await Context.AccountBalances
            .Include(ab => ab.Account)
            .Include(ab => ab.BalanceSourceType)
            .Include(ab => ab.Currency)
            .FirstOrDefaultAsync();

        if (balance != null)
        {
            Assert.DoesNotThrow(() => _ = balance.Account);
            Assert.DoesNotThrow(() => _ = balance.BalanceSourceType);
            Assert.DoesNotThrow(() => _ = balance.Currency);
        }
    }
}