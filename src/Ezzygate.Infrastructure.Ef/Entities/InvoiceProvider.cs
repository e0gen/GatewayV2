using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("InvoiceProvider", Schema = "List")]
public partial class InvoiceProvider
{
    [Key]
    [Column("InvoiceProvider_id")]
    public byte InvoiceProviderId { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }
}
