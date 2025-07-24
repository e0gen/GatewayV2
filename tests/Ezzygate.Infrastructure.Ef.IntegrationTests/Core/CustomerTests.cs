using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class CustomerTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.Customers.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var customer = await Context.Customers
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            Assert.DoesNotThrow(() => _ = customer.CustomerId);
            Assert.DoesNotThrow(() => _ = customer.ActiveStatusId);
            Assert.DoesNotThrow(() => _ = customer.ApplicationIdentityId);
            Assert.DoesNotThrow(() => _ = customer.CustomerNumber);
            Assert.DoesNotThrow(() => _ = customer.RegistrationDate);
            Assert.DoesNotThrow(() => _ = customer.RulesApproveDate);
            Assert.DoesNotThrow(() => _ = customer.FirstName);
            Assert.DoesNotThrow(() => _ = customer.LastName);
            Assert.DoesNotThrow(() => _ = customer.PersonalNumber);
            Assert.DoesNotThrow(() => _ = customer.PhoneNumber);
            Assert.DoesNotThrow(() => _ = customer.CellNumber);
            Assert.DoesNotThrow(() => _ = customer.DateOfBirth);
            Assert.DoesNotThrow(() => _ = customer.EmailAddress);
            Assert.DoesNotThrow(() => _ = customer.EmailToken);
            Assert.DoesNotThrow(() => _ = customer.Pincode);
            Assert.DoesNotThrow(() => _ = customer.FacebookUserID);
            Assert.DoesNotThrow(() => _ = customer.AccountId);
            Assert.DoesNotThrow(() => _ = customer.FullName);
        }
    }

    [Test]
    public async Task ShouldLoadWithAllRelationships()
    {
        var customer = await Context.Customers
            .Include(c => c.Account)
            .Include(c => c.ActiveStatus)
            .Include(c => c.CustomerShippingDetails.Take(1))
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            Assert.DoesNotThrow(() => _ = customer.Account);
            Assert.DoesNotThrow(() => _ = customer.ActiveStatus);
            Assert.DoesNotThrow(() => _ = customer.CustomerShippingDetails);
        }
    }
}