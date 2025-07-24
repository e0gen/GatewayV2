using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Lookup;

[TestFixture]
public class PaymentMethodTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.PaymentMethods.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var paymentMethod = await Context.PaymentMethods
            .FirstOrDefaultAsync();

        if (paymentMethod != null)
        {
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodId);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodGroupId);
            Assert.DoesNotThrow(() => _ = paymentMethod.PmType);
            Assert.DoesNotThrow(() => _ = paymentMethod.Name);
            Assert.DoesNotThrow(() => _ = paymentMethod.Abbreviation);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsBillingAddressMandatory);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsPopular);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsPull);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsPMInfoMandatory);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsTerminalRequired);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsExpirationDateMandatory);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value1EncryptedCaption);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value2EncryptedCaption);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value1EncryptedValidationRegex);
            Assert.DoesNotThrow(() => _ = paymentMethod.Value2EncryptedValidationRegex);
            Assert.DoesNotThrow(() => _ = paymentMethod.PaymentMethodTypeId);
            Assert.DoesNotThrow(() => _ = paymentMethod.IsPersonalIDRequired);
            Assert.DoesNotThrow(() => _ = paymentMethod.PendingKeepAliveMinutes);
            Assert.DoesNotThrow(() => _ = paymentMethod.PendingCleanupDays);
            Assert.DoesNotThrow(() => _ = paymentMethod.BaseBIN);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var paymentMethod = await Context.PaymentMethods
            .Include(x => x.AccountPaymentMethods)
            .FirstOrDefaultAsync();

        if (paymentMethod != null) Assert.DoesNotThrow(() => _ = paymentMethod.AccountPaymentMethods);
    }
}