using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class AccountPaymentMethodTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountPaymentMethods.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var paymentMethod = await Context.AccountPaymentMethods
            .FirstOrDefaultAsync();

        if (paymentMethod != null)
        {
            Assert.DoesNotThrow(() => _ = paymentMethod.AccountPaymentMethodId);
            Assert.DoesNotThrow(() => _ = paymentMethod.AccountId);
            Assert.DoesNotThrow(() => _ = paymentMethod.AccountAddressId);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodId);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsDefault);
            Assert.DoesNotThrow(() => _ = paymentMethod.Title);
            Assert.DoesNotThrow(() => _ = paymentMethod.OwnerName);
            Assert.DoesNotThrow(() => _ = paymentMethod.OwnerPersonalId);
            Assert.DoesNotThrow(() => _ = paymentMethod.OwnerDateOfBirth);
            Assert.DoesNotThrow(() => _ = paymentMethod.ExpirationDate);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value1Encrypted);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value2Encrypted);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodText);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value1Last4Text);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value1First6Text);
            Assert.DoesNotThrow(() => _ = paymentMethod.IssuerCountryIsoCode);
            Assert.DoesNotThrow(() => _ = paymentMethod.EncryptionKey);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodProviderId);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodStatusId);
            Assert.DoesNotThrow(() => _ = paymentMethod.ProviderReference1);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var paymentMethod = await Context.AccountPaymentMethods
            .Include(apm => apm.Account)
            .Include(apm => apm.AccountAddress)
            .Include(apm => apm.PaymentMethod)
            .Include(apm => apm.IssuerCountry)
            .Include(apm => apm.PaymentMethodProvider)
            .FirstOrDefaultAsync();

        if (paymentMethod != null)
        {
            Assert.DoesNotThrow(() => _ = paymentMethod.Account);
            Assert.DoesNotThrow(() => _ = paymentMethod.AccountAddress);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethod);
            Assert.DoesNotThrow(() => _ = paymentMethod.IssuerCountry);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodProvider);
        }
    }
}