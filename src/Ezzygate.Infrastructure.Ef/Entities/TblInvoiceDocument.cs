using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblInvoiceDocument")]
[Index("IdBillingCompanyId", "IdInvoiceNumber", "IdType", Name = "IX_tblInvoiceDocument_BillingCompany_InvoiceNumber_Type", IsUnique = true)]
public partial class TblInvoiceDocument
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("id_BillingCompanyID")]
    public int IdBillingCompanyId { get; set; }

    [Column("id_Type")]
    public int IdType { get; set; }

    [Column("id_InvoiceNumber")]
    public int IdInvoiceNumber { get; set; }

    [Column("id_TransactionPayID")]
    public int IdTransactionPayId { get; set; }

    [Column("id_BillToName")]
    [StringLength(850)]
    public string IdBillToName { get; set; } = null!;

    [Column("id_MerchantID")]
    public int IdMerchantId { get; set; }

    [Column("id_InsertDate", TypeName = "datetime")]
    public DateTime IdInsertDate { get; set; }

    [Column("id_PrintDate", TypeName = "datetime")]
    public DateTime IdPrintDate { get; set; }

    [Column("id_Username")]
    [StringLength(50)]
    public string IdUsername { get; set; } = null!;

    [Column("id_Lines")]
    public int IdLines { get; set; }

    [Column("id_TotalLines", TypeName = "money")]
    public decimal IdTotalLines { get; set; }

    [Column("id_ApplyVAT")]
    public bool IdApplyVat { get; set; }

    [Column("id_VATPercent")]
    public float IdVatpercent { get; set; }

    [Column("id_TotalVAT", TypeName = "money")]
    public decimal IdTotalVat { get; set; }

    [Column("id_TotalDocument", TypeName = "money")]
    public decimal IdTotalDocument { get; set; }

    [Column("id_Currency")]
    public int IdCurrency { get; set; }

    [Column("id_CurrencyRate", TypeName = "money")]
    public decimal IdCurrencyRate { get; set; }

    [Column("id_IsPrinted")]
    public bool? IdIsPrinted { get; set; }

    [Column("id_BillingCompanyName")]
    [StringLength(100)]
    public string IdBillingCompanyName { get; set; } = null!;

    [Column("id_BillingCompanyAddress")]
    [StringLength(500)]
    public string IdBillingCompanyAddress { get; set; } = null!;

    [Column("id_BillingCompanyNumber")]
    [StringLength(100)]
    public string IdBillingCompanyNumber { get; set; } = null!;

    [Column("id_BillingCompanyEmail")]
    [StringLength(100)]
    public string IdBillingCompanyEmail { get; set; } = null!;

    [Column("id_BillingCompanyLanguage")]
    [StringLength(3)]
    public string IdBillingCompanyLanguage { get; set; } = null!;

    [Column("id_IsManual")]
    public bool IdIsManual { get; set; }

    [ForeignKey("IdBillingCompanyId")]
    [InverseProperty("TblInvoiceDocuments")]
    public virtual TblBillingCompany IdBillingCompany { get; set; } = null!;

    [ForeignKey("IdCurrency")]
    [InverseProperty("TblInvoiceDocuments")]
    public virtual TblSystemCurrency IdCurrencyNavigation { get; set; } = null!;

    [InverseProperty("IlDocument")]
    public virtual ICollection<TblInvoiceLine> TblInvoiceLines { get; set; } = new List<TblInvoiceLine>();
}
