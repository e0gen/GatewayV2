using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class LoginRoleTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.LoginRoles.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var loginRole = await Context.LoginRoles
            .FirstOrDefaultAsync();

        if (loginRole != null)
        {
            Assert.DoesNotThrow(() => _ = loginRole.LoginRoleId);
            Assert.DoesNotThrow(() => _ = loginRole.Name);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var loginRole = await Context.LoginRoles
            .Include(lr => lr.LoginAccounts.Take(3))
            .FirstOrDefaultAsync();

        if (loginRole != null) Assert.DoesNotThrow(() => _ = loginRole.LoginAccounts);
    }
}