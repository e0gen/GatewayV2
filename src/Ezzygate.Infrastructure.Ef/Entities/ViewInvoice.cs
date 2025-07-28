using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewInvoice
{
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

    [StringLength(200)]
    public string MerchantName { get; set; } = null!;

    [StringLength(1)]
    public string? BillingCompanyFirstLetter { get; set; }

    [Column("GD_Text")]
    [StringLength(80)]
    public string? GdText { get; set; }

    [StringLength(25)]
    public string? Symbol { get; set; }

    public long? InvoiceOrderNumber { get; set; }
}
