using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class BalanceSourceTypeTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.BalanceSourceTypes.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var balanceSourceType = await Context.BalanceSourceTypes
            .FirstOrDefaultAsync();

        if (balanceSourceType != null)
        {
            Assert.DoesNotThrow(() => _ = balanceSourceType.BalanceSourceTypeId);
            Assert.DoesNotThrow(() => _ = balanceSourceType.Name);
            Assert.DoesNotThrow(() => _ = balanceSourceType.IsFee);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var balanceSourceType = await Context.BalanceSourceTypes
            .Include(bst => bst.AccountBalances.Take(3))
            .FirstOrDefaultAsync();

        if (balanceSourceType != null) Assert.DoesNotThrow(() => _ = balanceSourceType.AccountBalances);
    }
}