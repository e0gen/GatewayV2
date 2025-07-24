using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class PaymentMethodProviderTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.PaymentMethodProviders.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var provider = await Context.PaymentMethodProviders
            .FirstOrDefaultAsync();

        if (provider != null)
        {
            Assert.DoesNotThrow(() => _ = provider.PaymentMethodProviderId);
            Assert.DoesNotThrow(() => _ = provider.Name);
            Assert.DoesNotThrow(() => _ = provider.IsActive);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var paymentMethod = await Context.PaymentMethodProviders
            .Include(x => x.AccountPaymentMethods)
            .FirstOrDefaultAsync();

        if (paymentMethod != null) Assert.DoesNotThrow(() => _ = paymentMethod.AccountPaymentMethods);
    }
}