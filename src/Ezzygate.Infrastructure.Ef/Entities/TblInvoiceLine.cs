using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblInvoiceLine")]
public partial class TblInvoiceLine
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("il_DocumentID")]
    public int? IlDocumentId { get; set; }

    [Column("il_Date", TypeName = "datetime")]
    public DateTime IlDate { get; set; }

    [Column("il_TransactionPayID")]
    public int IlTransactionPayId { get; set; }

    [Column("il_Username")]
    [StringLength(50)]
    public string IlUsername { get; set; } = null!;

    [Column("il_BillToName")]
    [StringLength(100)]
    public string IlBillToName { get; set; } = null!;

    [Column("il_MerchantID")]
    public int IlMerchantId { get; set; }

    [Column("il_Text")]
    [StringLength(200)]
    public string IlText { get; set; } = null!;

    [Column("il_Quantity")]
    public float IlQuantity { get; set; }

    [Column("il_Price", TypeName = "money")]
    public decimal IlPrice { get; set; }

    [Column("il_Currency")]
    public int IlCurrency { get; set; }

    [Column("il_CurrencyRate", TypeName = "money")]
    public decimal IlCurrencyRate { get; set; }

    [Column("il_Amount", TypeName = "money")]
    public decimal? IlAmount { get; set; }

    [ForeignKey("IlCurrency")]
    [InverseProperty("TblInvoiceLines")]
    public virtual TblSystemCurrency IlCurrencyNavigation { get; set; } = null!;

    [ForeignKey("IlDocumentId")]
    [InverseProperty("TblInvoiceLines")]
    public virtual TblInvoiceDocument? IlDocument { get; set; }
}
