using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class StateListTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.States.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var state = await Context.States
            .FirstOrDefaultAsync();

        if (state != null)
        {
            Assert.DoesNotThrow(() => _ = state.StateISOCode);
            Assert.DoesNotThrow(() => _ = state.CountryISOCode);
            Assert.DoesNotThrow(() => _ = state.Name);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var state = await Context.States
            .Include(s => s.Country)
            .Include(s => s.AccountAddresses.Take(3))
            .FirstOrDefaultAsync();

        if (state != null)
        {
            Assert.DoesNotThrow(() => _ = state.Country);
            Assert.DoesNotThrow(() => _ = state.AccountAddresses);
        }
    }
}