using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class CountryListTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.Countries.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var country = await Context.Countries
            .FirstOrDefaultAsync();

        if (country != null)
        {
            Assert.DoesNotThrow(() => _ = country.CountryISOCode);
            Assert.DoesNotThrow(() => _ = country.Name);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var country = await Context.Countries
            .Include(c => c.AccountAddresses.Take(2))
            .Include(c => c.States.Take(2))
            .Include(c => c.AccountPaymentMethodsAsIssuerCountry.Take(2))
            .FirstOrDefaultAsync();

        if (country != null)
        {
            Assert.DoesNotThrow(() => _ = country.AccountAddresses);
            Assert.DoesNotThrow(() => _ = country.States);
            Assert.DoesNotThrow(() => _ = country.AccountPaymentMethodsAsIssuerCountry);
        }
    }
}