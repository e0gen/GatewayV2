using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCreditCard")]
public partial class TblCreditCard
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ccTypeID")]
    public short CcTypeId { get; set; }

    [StringLength(50)]
    public string PersonalNumber { get; set; } = null!;

    [Column("ExpMM")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExpMm { get; set; } = null!;

    [Column("ExpYY")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExpYy { get; set; } = null!;

    [StringLength(100)]
    public string Member { get; set; } = null!;

    [Column("phoneNumber")]
    [StringLength(50)]
    public string PhoneNumber { get; set; } = null!;

    [Column("email")]
    [StringLength(80)]
    public string Email { get; set; } = null!;

    [StringLength(3000)]
    public string Comment { get; set; } = null!;

    [Column("CCard_First6")]
    public int CcardFirst6 { get; set; }

    [Column("CCard_Last4")]
    public short CcardLast4 { get; set; }

    [Column("CCard_number256")]
    [MaxLength(200)]
    public byte[]? CcardNumber256 { get; set; }

    [Column("cc_InsertDate", TypeName = "datetime")]
    public DateTime? CcInsertDate { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }

    [Column("BillingAddressID")]
    public int? BillingAddressId { get; set; }

    [Column("BINCountry")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Bincountry { get; set; }

    [Column("cc_cui")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CcCui { get; set; }

    [Column("CardholderDOB")]
    public DateOnly? CardholderDob { get; set; }

    [StringLength(50)]
    public string? InvoiceName { get; set; }

    [ForeignKey("BillingAddressId")]
    [InverseProperty("TblCreditCards")]
    public virtual TblBillingAddress? BillingAddress { get; set; }

    [ForeignKey("Bincountry")]
    [InverseProperty("TblCreditCards")]
    public virtual CountryList? BincountryNavigation { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCreditCards")]
    public virtual TblCompany? Company { get; set; }

    [InverseProperty("CreditCard")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("CreditCard")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("CreditCard")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("CreditCard")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("RaCreditCardNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [InverseProperty("RcCreditCardNavigation")]
    public virtual ICollection<TblRecurringCharge> TblRecurringCharges { get; set; } = new List<TblRecurringCharge>();

    [InverseProperty("RsCreditCardNavigation")]
    public virtual ICollection<TblRecurringSeries> TblRecurringSeries { get; set; } = new List<TblRecurringSeries>();
}
