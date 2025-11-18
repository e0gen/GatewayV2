namespace Ezzygate.Domain.Models;

public class RiskSettings
{
    public int MerchantId { get; set; }
    public bool IsPh3AEnabled { get; set; }
    public byte Ph3AMinScore { get; set; }
    public byte LimitCcForEmailAllowedCount { get; set; }
    public bool IsLimitCcForEmailBlockNewCc { get; set; }
    public bool IsLimitCcForEmailBlockAllCc { get; set; }
    public bool IsLimitCcForEmailBlockEmail { get; set; }
    public bool IsLimitCcForEmailDeclineTrans { get; set; }
    public bool IsLimitCcForEmailNotifyByEmail { get; set; }
    public string? LimitCcForEmailNotifyByEmailList { get; set; }
    public byte LimitEmailForCcAllowedCount { get; set; }
    public bool IsLimitEmailForCcBlockNewEmail { get; set; }
    public bool IsLimitEmailForCcBlockAllEmails { get; set; }
    public bool IsLimitEmailForCcBlockCc { get; set; }
    public bool IsLimitEmailForCcDeclineTrans { get; set; }
    public bool IsLimitEmailForCcNotifyByEmail { get; set; }
    public string? LimitEmailForCcNotifyByEmailList { get; set; }
    public string? WhitelistCountry { get; set; }
    public string? WhitelistState { get; set; }
    public string? BlacklistCountry { get; set; }
    public string? BlacklistState { get; set; }
    public string? BlacklistIpCountry { get; set; }
    public bool IsEnableCcWhiteList { get; set; }
    public string? AmountAllowedList { get; set; }
}