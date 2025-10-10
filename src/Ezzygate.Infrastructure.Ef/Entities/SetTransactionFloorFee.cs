using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetTransactionFloorFee", Schema = "Setting")]
public partial class SetTransactionFloorFee
{
    [Key]
    [Column("SetTransactionFloorFee_id")]
    public int SetTransactionFloorFeeId { get; set; }

    [Column("SetTransactionFloor_id")]
    public int SetTransactionFloorId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal AmountTop { get; set; }

    [Column(TypeName = "decimal(7, 4)")]
    public decimal? PercentValue { get; set; }

    [Column(TypeName = "decimal(7, 4)")]
    public decimal? FixedAmount { get; set; }

    [ForeignKey("SetTransactionFloorId")]
    [InverseProperty("SetTransactionFloorFees")]
    public virtual SetTransactionFloor SetTransactionFloor { get; set; } = null!;
}
