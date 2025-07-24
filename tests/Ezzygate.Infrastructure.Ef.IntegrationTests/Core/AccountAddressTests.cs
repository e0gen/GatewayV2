using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class AccountAddressTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountAddresses.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var address = await Context.AccountAddresses
            .FirstOrDefaultAsync();

        if (address != null)
        {
            Assert.DoesNotThrow(() => _ = address.AccountAddressId);
            Assert.DoesNotThrow(() => _ = address.Street1);
            Assert.DoesNotThrow(() => _ = address.Street2);
            Assert.DoesNotThrow(() => _ = address.City);
            Assert.DoesNotThrow(() => _ = address.PostalCode);
            Assert.DoesNotThrow(() => _ = address.StateISOCode);
            Assert.DoesNotThrow(() => _ = address.CountryISOCode);
            Assert.DoesNotThrow(() => _ = address.FullAddress);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var address = await Context.AccountAddresses
            .Include(aa => aa.Country)
            .Include(aa => aa.State)
            .FirstOrDefaultAsync();

        if (address != null)
        {
            Assert.DoesNotThrow(() => _ = address.Country);
            Assert.DoesNotThrow(() => _ = address.State);
            Assert.DoesNotThrow(() => _ = address.FullAddress);
        }
    }
}