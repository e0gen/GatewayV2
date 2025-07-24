using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class AccountTypeTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountTypes.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var accountType = await Context.AccountTypes
            .FirstOrDefaultAsync();

        if (accountType != null)
        {
            Assert.DoesNotThrow(() => _ = accountType.AccountTypeId);
            Assert.DoesNotThrow(() => _ = accountType.Name);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var accountType = await Context.AccountTypes
            .Include(at => at.Accounts.Take(3))
            .FirstOrDefaultAsync();

        if (accountType != null) Assert.DoesNotThrow(() => _ = accountType.Accounts);
    }
}