using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.Core;

[TestFixture]
public class MobileDeviceTests : DatabaseTestBase
{
    [Test]
    public void ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.MobileDevices.CountAsync(); });
    }

    [Test]
    public async Task ShouldBeMappedCorrectly()
    {
        var device = await Context.MobileDevices
            .Include(md => md.Account)
            .FirstOrDefaultAsync();

        if (device != null)
        {
            Assert.DoesNotThrow(() => _ = device.MobileDeviceId);
            Assert.DoesNotThrow(() => _ = device.AccountId);
            Assert.DoesNotThrow(() => _ = device.InsertDate);
            Assert.DoesNotThrow(() => _ = device.DeviceIdentity);
            Assert.DoesNotThrow(() => _ = device.FriendlyName);
            Assert.DoesNotThrow(() => _ = device.IsActive);
        }
    }

    [Test]
    public async Task ShouldLoadAllRelationships()
    {
        var device = await Context.MobileDevices
            .Include(md => md.Account)
            .FirstOrDefaultAsync();

        if (device != null) Assert.DoesNotThrow(() => _ = device.Account);
    }
}