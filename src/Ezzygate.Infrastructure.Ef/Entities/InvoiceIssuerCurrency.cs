using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("InvoiceIssuerCurrency", Schema = "Finance")]
public partial class InvoiceIssuerCurrency
{
    [Key]
    [Column("InvoiceIssuerCurrency_id")]
    public short InvoiceIssuerCurrencyId { get; set; }

    [Column("InvoiceIssuer_id")]
    public byte? InvoiceIssuerId { get; set; }

    public short? CurrencySettlement { get; set; }

    public short? CurrencyInvoice { get; set; }
}
