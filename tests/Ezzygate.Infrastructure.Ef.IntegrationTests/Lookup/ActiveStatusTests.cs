using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class ActiveStatusTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.ActiveStatuses.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var activeStatus = await Context.ActiveStatuses
            .FirstOrDefaultAsync();

        if (activeStatus != null)
        {
            Assert.DoesNotThrow(() => _ = activeStatus.ActiveStatusId);
            Assert.DoesNotThrow(() => _ = activeStatus.Name);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var activeStatus = await Context.ActiveStatuses
            .Include(x => x.Customers.Take(3))
            .FirstOrDefaultAsync();

        if (activeStatus != null) Assert.DoesNotThrow(() => _ = activeStatus.Customers);
    }
}