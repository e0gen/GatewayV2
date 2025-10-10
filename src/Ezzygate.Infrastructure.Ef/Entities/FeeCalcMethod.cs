using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("FeeCalcMethod", Schema = "List")]
public partial class FeeCalcMethod
{
    [Key]
    [Column("FeeCalcMethod_id")]
    public byte FeeCalcMethodId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("FeeCalcMethod")]
    public virtual ICollection<SetTransactionFee> SetTransactionFees { get; set; } = new List<SetTransactionFee>();
}
