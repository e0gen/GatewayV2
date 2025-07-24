using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class LoginAccountTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.LoginAccounts.CountAsync(); });
    }

    [Test]
    public async Task Properties_ShouldBeMappedCorrectly()
    {
        var loginAccount = await Context.LoginAccounts
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            Assert.DoesNotThrow(() => _ = loginAccount.LoginAccountId);
            Assert.DoesNotThrow(() => _ = loginAccount.LoginRoleId);
            Assert.DoesNotThrow(() => _ = loginAccount.LoginUser);
            Assert.DoesNotThrow(() => _ = loginAccount.LoginEmail);
            Assert.DoesNotThrow(() => _ = loginAccount.IsActive);
            Assert.DoesNotThrow(() => _ = loginAccount.FailCount);
            Assert.DoesNotThrow(() => _ = loginAccount.LastFailTime);
            Assert.DoesNotThrow(() => _ = loginAccount.LastSuccessTime);
            Assert.DoesNotThrow(() => _ = loginAccount.BlockEndTime);
            Assert.DoesNotThrow(() => _ = loginAccount.IsBlocked);
            Assert.DoesNotThrow(() => _ = loginAccount.IsLocked);
        }
    }

    [Test]
    public async Task WithAllRelationships_ShouldLoadWithoutErrors()
    {
        var loginAccount = await Context.LoginAccounts
            .Include(la => la.LoginRole)
            .Include(la => la.Accounts.Take(2))
            .Include(la => la.AccountSubUsers.Take(2))
            .Include(la => la.LoginPasswords.Take(2))
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            Assert.DoesNotThrow(() => _ = loginAccount.LoginRole);
            Assert.DoesNotThrow(() => _ = loginAccount.Accounts);
            Assert.DoesNotThrow(() => _ = loginAccount.AccountSubUsers);
            Assert.DoesNotThrow(() => _ = loginAccount.LoginPasswords);
        }
    }
}