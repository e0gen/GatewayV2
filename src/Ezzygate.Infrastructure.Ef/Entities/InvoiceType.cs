using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("InvoiceType", Schema = "List")]
public partial class InvoiceType
{
    [Key]
    [Column("InvoiceType_id")]
    public byte InvoiceTypeId { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }
}
