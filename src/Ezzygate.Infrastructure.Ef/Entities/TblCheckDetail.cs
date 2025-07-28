using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCheckDetails")]
[Index("AccountName", Name = "IX_tblCheckDetails_accountName")]
public partial class TblCheckDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("insertDate", TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    public int CompanyId { get; set; }

    public int? CustomerId { get; set; }

    public int? BillingAddressId { get; set; }

    [StringLength(50)]
    public string AccountName { get; set; } = null!;

    [MaxLength(200)]
    public byte[]? AccountNumber256 { get; set; }

    [MaxLength(200)]
    public byte[]? RoutingNumber256 { get; set; }

    [StringLength(20)]
    public string PersonalNumber { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(500)]
    public string Comment { get; set; } = null!;

    [StringLength(10)]
    public string BirthDate { get; set; } = null!;

    public byte BankAccountTypeId { get; set; }

    [StringLength(50)]
    public string BankName { get; set; } = null!;

    [StringLength(15)]
    public string BankCity { get; set; } = null!;

    [StringLength(15)]
    public string BankPhone { get; set; } = null!;

    [StringLength(2)]
    public string BankState { get; set; } = null!;

    [StringLength(2)]
    public string BankCountry { get; set; } = null!;

    [StringLength(50)]
    public string? InvoiceName { get; set; }

    [ForeignKey("BillingAddressId")]
    [InverseProperty("TblCheckDetails")]
    public virtual TblBillingAddress? BillingAddress { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCheckDetails")]
    public virtual TblCompany Company { get; set; } = null!;

    [InverseProperty("CheckDetails")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("CheckDetails")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("CheckDetails")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("CheckDetails")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("RaEcheckNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [InverseProperty("RcEcheckNavigation")]
    public virtual ICollection<TblRecurringCharge> TblRecurringCharges { get; set; } = new List<TblRecurringCharge>();

    [InverseProperty("RsEcheckNavigation")]
    public virtual ICollection<TblRecurringSeries> TblRecurringSeries { get; set; } = new List<TblRecurringSeries>();
}
