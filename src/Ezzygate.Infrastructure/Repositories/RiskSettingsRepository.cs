using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class RiskSettingsRepository : IRiskSettingsRepository
{
    private readonly EzzygateDbContext _context;

    public RiskSettingsRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<RiskSettings?> GetByMerchantIdAsync(int merchantId)
    {
        var entity = await _context.SetMerchantRisks
            .AsNoTracking()
            .Include(r => r.Merchant)
            .FirstOrDefaultAsync(r => r.MerchantId == merchantId);

        if (entity == null) return null;

        return new RiskSettings
        {
            MerchantId = entity.MerchantId,
            IsPh3AEnabled = entity.Ph3aIsEnabled ?? false,
            Ph3AMinScore = entity.Ph3aMinScoreAllowed ?? 0,
            LimitCcForEmailAllowedCount = entity.LimitCcForEmailAllowedCount ?? 0,
            IsLimitCcForEmailBlockNewCc = entity.IsLimitCcForEmailBlockNewCc ?? false,
            IsLimitCcForEmailBlockAllCc = entity.IsLimitCcForEmailBlockAllCc ?? false,
            IsLimitCcForEmailBlockEmail = entity.IsLimitCcForEmailBlockEmail ?? false,
            IsLimitCcForEmailDeclineTrans = entity.IsLimitCcForEmailDeclineTrans ?? false,
            IsLimitCcForEmailNotifyByEmail = entity.IsLimitCcForEmailNotifyByEmail ?? false,
            LimitCcForEmailNotifyByEmailList = entity.LimitCcForEmailNotifyByEmailList,
            LimitEmailForCcAllowedCount = entity.LimitEmailForCcAllowedCount ?? 0,
            IsLimitEmailForCcBlockNewEmail = entity.IsLimitEmailForCcBlockNewEmail ?? false,
            IsLimitEmailForCcBlockAllEmails = entity.IsLimitEmailForCcBlockAllEmails ?? false,
            IsLimitEmailForCcBlockCc = entity.IsLimitEmailForCcBlockCc ?? false,
            IsLimitEmailForCcDeclineTrans = entity.IsLimitEmailForCcDeclineTrans ?? false,
            IsLimitEmailForCcNotifyByEmail = entity.IsLimitEmailForCcNotifyByEmail ?? false,
            LimitEmailForCcNotifyByEmailList = entity.LimitEmailForCcNotifyByEmailList,
            WhitelistCountry = entity.WhitelistCountry,
            WhitelistState = entity.WhitelistState,
            BlacklistCountry = entity.BlacklistCountry,
            BlacklistState = entity.BlacklistState,
            BlacklistIpCountry = entity.BlacklistIpcountry,
            IsEnableCcWhiteList = entity.IsEnableCcWhiteList ?? false,
            AmountAllowedList = entity.AmountAllowedList
        };
    }
}