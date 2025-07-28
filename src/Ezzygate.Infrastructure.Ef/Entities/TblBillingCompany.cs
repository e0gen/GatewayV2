using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBillingCompanys")]
public partial class TblBillingCompany
{
    [Key]
    [Column("BillingCompanys_id")]
    public int BillingCompanysId { get; set; }

    [StringLength(3)]
    public string LanguageShow { get; set; } = null!;

    [Column("currencyShow")]
    public int CurrencyShow { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("address")]
    [StringLength(500)]
    public string Address { get; set; } = null!;

    [Column("number")]
    [StringLength(100)]
    public string Number { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("VATamount")]
    public double Vatamount { get; set; }

    public bool? IsDefault { get; set; }

    [Column("isShow")]
    public bool IsShow { get; set; }

    [StringLength(50)]
    public string? BrandName { get; set; }

    [ForeignKey("CurrencyShow")]
    [InverseProperty("TblBillingCompanies")]
    public virtual TblSystemCurrency CurrencyShowNavigation { get; set; } = null!;

    [InverseProperty("BillingCompanys")]
    public virtual ICollection<TblCompany> TblCompanies { get; set; } = new List<TblCompany>();

    [InverseProperty("IdBillingCompany")]
    public virtual ICollection<TblInvoiceDocument> TblInvoiceDocuments { get; set; } = new List<TblInvoiceDocument>();
}
