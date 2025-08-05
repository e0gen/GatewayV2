using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Ef.Mapping;

public static class DomainMapping
{
    public static Terminal ToDomain(this TblDebitTerminal entity)
    {
        return new Terminal
        {
            Id = entity.Id,
            TerminalNumber = entity.TerminalNumber,
            AccountId = entity.AccountId,
            AccountSubId = entity.AccountSubId,
            TerminalNumber3D = entity.TerminalNumber3D,
            AccountId3D = entity.AccountId3D,
            AccountSubId3D = entity.AccountSubId3D,
            IsTestTerminal = entity.DtIsTestTerminal,
            Enable3DSecure = entity.DtEnable3dsecure,
            DebitCompanyId = entity.DebitCompany,
            AuthenticationCode1 = entity.AuthenticationCode1,
            AuthenticationCode3D = entity.AuthenticationCode3D,
            AccountPassword256 = entity.AccountPassword256,
        };
    }

    public static DebitCompany ToDomain(this TblDebitCompany entity)
    {
        return new DebitCompany
        {
            Id = entity.DebitCompanyId,
            CompanyName = entity.DcName,
            IsActive = entity.DcIsActive
        };
    }
    
    public static Domain.Models.PaymentMethod ToDomain(this Entities.PaymentMethod entity)
    {
        return new Domain.Models.PaymentMethod
        {
            Id = entity.PaymentMethodId,
            GroupId = entity.PaymentMethodGroupId,
            Type = entity.PmType,
            Name = entity.Name,
            Abbreviation = entity.Abbreviation,
            IsBillingAddressMandatory = entity.IsBillingAddressMandatory,
            IsPopular = entity.IsPopular,
            IsPull = entity.IsPull,
            IsInfoMandatory = entity.IsPminfoMandatory,
            IsTerminalRequired = entity.IsTerminalRequired,
            IsExpirationDateMandatory = entity.IsExpirationDateMandatory,
            Value1Caption = entity.Value1EncryptedCaption,
            Value2Caption = entity.Value2EncryptedCaption,
            Value1ValidationRegex = entity.Value1EncryptedValidationRegex,
            Value2ValidationRegex = entity.Value2EncryptedValidationRegex,
            PaymentMethodTypeId = entity.PaymentMethodTypeId,
            IsPersonalIdRequired = entity.IsPersonalIdrequired,
            PendingKeepAliveMinutes = entity.PendingKeepAliveMinutes,
            PendingCleanupDays = entity.PendingCleanupDays,
            BaseBin = entity.BaseBin
        };
    }
}