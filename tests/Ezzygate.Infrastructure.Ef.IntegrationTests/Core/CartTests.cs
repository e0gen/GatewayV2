using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class CartTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.Carts.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var cart = await Context.Carts
            .FirstOrDefaultAsync();

        if (cart != null)
        {
            Assert.DoesNotThrow(() => _ = cart.CartId);
            Assert.DoesNotThrow(() => _ = cart.MerchantId);
            Assert.DoesNotThrow(() => _ = cart.CustomerId);
            Assert.DoesNotThrow(() => _ = cart.StartDate);
            Assert.DoesNotThrow(() => _ = cart.CheckoutDate);
            Assert.DoesNotThrow(() => _ = cart.CurrencyISOCode);
            Assert.DoesNotThrow(() => _ = cart.TotalProducts);
            Assert.DoesNotThrow(() => _ = cart.TotalShipping);
            Assert.DoesNotThrow(() => _ = cart.Total);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var cart = await Context.Carts
            .Include(c => c.Customer)
            .FirstOrDefaultAsync();

        if (cart != null) Assert.DoesNotThrow(() => _ = cart.Customer);
    }
}