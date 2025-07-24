using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class PaymentMethod
{
    [Key]
    public short PaymentMethodId { get; set; }

    public byte? PaymentMethodGroupId { get; set; }

    public int? PmType { get; set; }

    [StringLength(80)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Abbreviation { get; set; }

    [Required]
    public bool IsBillingAddressMandatory { get; set; }

    [Required]
    public bool IsPopular { get; set; }

    [Required]
    public bool IsPull { get; set; }

    [Required]
    public bool IsPMInfoMandatory { get; set; }

    [Required]
    public bool IsTerminalRequired { get; set; }

    [Required]
    public bool IsExpirationDateMandatory { get; set; }

    [StringLength(30)]
    public string? Value1EncryptedCaption { get; set; }

    [StringLength(30)]
    public string? Value2EncryptedCaption { get; set; }

    [StringLength(30)]
    public string? Value1EncryptedValidationRegex { get; set; }

    [StringLength(30)]
    public string? Value2EncryptedValidationRegex { get; set; }

    public byte? PaymentMethodTypeId { get; set; }

    [Required]
    public bool IsPersonalIDRequired { get; set; }

    public int? PendingKeepAliveMinutes { get; set; }

    public short? PendingCleanupDays { get; set; }

    [StringLength(10)]
    public string? BaseBIN { get; set; }

    public ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();
}