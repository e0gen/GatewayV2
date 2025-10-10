using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PaymentMethodType", Schema = "List")]
public partial class PaymentMethodType
{
    [Key]
    [Column("PaymentMethodType_id")]
    public byte PaymentMethodTypeId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;
}
