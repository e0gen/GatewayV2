using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class CustomerShippingDetailTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.CustomerShippingDetails.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var shippingDetail = await Context.CustomerShippingDetails
            .FirstOrDefaultAsync();

        if (shippingDetail != null)
        {
            Assert.DoesNotThrow(() => _ = shippingDetail.CustomerShippingDetailId);
            Assert.DoesNotThrow(() => _ = shippingDetail.CustomerId);
            Assert.DoesNotThrow(() => _ = shippingDetail.AccountAddressId);
            Assert.DoesNotThrow(() => _ = shippingDetail.IsDefault);
            Assert.DoesNotThrow(() => _ = shippingDetail.Title);
            Assert.DoesNotThrow(() => _ = shippingDetail.Comment);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var shippingDetail = await Context.CustomerShippingDetails
            .Include(csd => csd.Customer)
            .Include(csd => csd.AccountAddress)
            .FirstOrDefaultAsync();

        if (shippingDetail != null)
        {
            Assert.DoesNotThrow(() => _ = shippingDetail.Customer);
            Assert.DoesNotThrow(() => _ = shippingDetail.AccountAddress);
        }
    }
}