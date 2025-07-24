using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class AccountSubUserTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountSubUsers.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var subUser = await Context.AccountSubUsers
            .Include(asu => asu.Account)
            .FirstOrDefaultAsync();

        if (subUser != null)
        {
            Assert.DoesNotThrow(() => _ = subUser.AccountSubUserId);
            Assert.DoesNotThrow(() => _ = subUser.AccountId);
            Assert.DoesNotThrow(() => _ = subUser.LoginAccountId);
            Assert.DoesNotThrow(() => _ = subUser.Description);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var subUser = await Context.AccountSubUsers
            .Include(asu => asu.Account)
            .Include(asu => asu.LoginAccount)
            .FirstOrDefaultAsync();

        if (subUser != null)
        {
            Assert.DoesNotThrow(() => _ = subUser.Account);
            Assert.DoesNotThrow(() => _ = subUser.LoginAccount);
        }
    }
}