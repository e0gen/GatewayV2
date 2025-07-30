using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class MobileDeviceRepository : IMobileDeviceRepository
{
    private readonly EzzygateDbContext _context;

    public MobileDeviceRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<MobileDevice?> GetByDeviceIdentityAsync(string deviceIdentity)
    {
        var entity = await _context.MobileDevices
            .FirstOrDefaultAsync(d => d.DeviceIdentity == deviceIdentity);

        if (entity == null) return null;

        return MapToModel(entity);
    }

    public async Task UpdateAsync(MobileDevice device)
    {
        var entity = await _context.MobileDevices
            .FirstOrDefaultAsync(d => d.MobileDeviceId == device.MobileDeviceId);

        if (entity == null) return;

        entity.LastLogin = device.LastLogin;
        entity.SignatureFailCount = device.SignatureFailCount;
        entity.IsActive = device.IsActive;
        entity.AppVersion = device.AppVersion;
        entity.AppPushToken = device.AppPushToken;

        await _context.SaveChangesAsync();
    }

    private static MobileDevice MapToModel(Ezzygate.Infrastructure.Ef.Entities.MobileDevice entity)
    {
        return new MobileDevice
        {
            MobileDeviceId = entity.MobileDeviceId,
            InsertDate = entity.InsertDate,
            DeviceIdentity = entity.DeviceIdentity,
            DeviceUserAgent = entity.DeviceUserAgent,
            DevicePhoneNumber = entity.DevicePhoneNumber,
            PassCode = entity.PassCode,
            LastLogin = entity.LastLogin,
            IsActivated = entity.IsActivated,
            IsActive = entity.IsActive,
            AppVersion = entity.AppVersion,
            SignatureFailCount = entity.SignatureFailCount,
            FriendlyName = entity.FriendlyName,
            AccountSubUserId = entity.AccountSubUserId,
            AccountId = entity.AccountId,
            AppPushToken = entity.AppPushToken
        };
    }
} 