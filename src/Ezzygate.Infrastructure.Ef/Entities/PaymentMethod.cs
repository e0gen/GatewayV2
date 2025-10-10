using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PaymentMethod", Schema = "List")]
[Index("Abbreviation", Name = "UNC_PaymentMethod_Abbreviation", IsUnique = true)]
public partial class PaymentMethod
{
    [Key]
    [Column("PaymentMethod_id")]
    public short PaymentMethodId { get; set; }

    [Column("PaymentMethodGroup_id")]
    public byte? PaymentMethodGroupId { get; set; }

    [Column("pm_Type")]
    public int? PmType { get; set; }

    [StringLength(80)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Abbreviation { get; set; }

    public bool IsBillingAddressMandatory { get; set; }

    public bool IsPopular { get; set; }

    public bool IsPull { get; set; }

    [Column("IsPMInfoMandatory")]
    public bool IsPminfoMandatory { get; set; }

    public bool IsTerminalRequired { get; set; }

    public bool IsExpirationDateMandatory { get; set; }

    [StringLength(30)]
    public string? Value1EncryptedCaption { get; set; }

    [StringLength(30)]
    public string? Value2EncryptedCaption { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Value1EncryptedValidationRegex { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Value2EncryptedValidationRegex { get; set; }

    [Column("PaymentMethodType_id")]
    public byte? PaymentMethodTypeId { get; set; }

    [Column("IsPersonalIDRequired")]
    public bool IsPersonalIdrequired { get; set; }

    public int? PendingKeepAliveMinutes { get; set; }

    public short? PendingCleanupDays { get; set; }

    [Column("BaseBIN")]
    [StringLength(10)]
    [Unicode(false)]
    public string? BaseBin { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<ApplicationIdentityToPaymentMethod> ApplicationIdentityToPaymentMethods { get; set; } = new List<ApplicationIdentityToPaymentMethod>();

    [ForeignKey("PaymentMethodGroupId")]
    [InverseProperty("PaymentMethods")]
    public virtual PaymentMethodGroup? PaymentMethodGroup { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<PreCreatedPaymentMethod> PreCreatedPaymentMethods { get; set; } = new List<PreCreatedPaymentMethod>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<ProcessApproved> ProcessApproveds { get; set; } = new List<ProcessApproved>();

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<TblCcstorage> TblCcstorages { get; set; } = new List<TblCcstorage>();

    [InverseProperty("CcfPaymentMethodNavigation")]
    public virtual ICollection<TblCompanyCreditFee> TblCompanyCreditFees { get; set; } = new List<TblCompanyCreditFee>();

    [InverseProperty("CpmPaymentMethodNavigation")]
    public virtual ICollection<TblCompanyPaymentMethod> TblCompanyPaymentMethods { get; set; } = new List<TblCompanyPaymentMethod>();

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("PaymentMethodNavigation")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("CcrmPaymentMethodNavigation")]
    public virtual ICollection<TblCreditCardRiskManagement> TblCreditCardRiskManagements { get; set; } = new List<TblCreditCardRiskManagement>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<TblFraudCcBlackList> TblFraudCcBlackLists { get; set; } = new List<TblFraudCcBlackList>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<TransPaymentMethod> TransPaymentMethods { get; set; } = new List<TransPaymentMethod>();
}
