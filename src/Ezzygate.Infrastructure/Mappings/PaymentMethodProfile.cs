namespace Ezzygate.Infrastructure.Mappings;

public static class PaymentMethodProfile
{
    public static Domain.Models.PaymentMethod ToDomain(this Ef.Entities.PaymentMethod entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
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