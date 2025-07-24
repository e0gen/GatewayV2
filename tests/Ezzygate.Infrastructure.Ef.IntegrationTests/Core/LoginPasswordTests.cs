using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class LoginPasswordTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.LoginPasswords.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var loginPassword = await Context.LoginPasswords
            .FirstOrDefaultAsync();

        if (loginPassword != null)
        {
            Assert.DoesNotThrow(() => _ = loginPassword.LoginPasswordId);
            Assert.DoesNotThrow(() => _ = loginPassword.LoginAccountId);
            Assert.DoesNotThrow(() => _ = loginPassword.InsertDate);
            Assert.DoesNotThrow(() => _ = loginPassword.PasswordEncrypted);
            Assert.DoesNotThrow(() => _ = loginPassword.EncryptionKey);
            Assert.DoesNotThrow(() => _ = loginPassword.IsTemporary);
            Assert.DoesNotThrow(() => _ = loginPassword.IsExpired);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var loginPassword = await Context.LoginPasswords
            .Include(lp => lp.LoginAccount)
            .FirstOrDefaultAsync();

        if (loginPassword != null) Assert.DoesNotThrow(() => _ = loginPassword.LoginAccount);
    }
}