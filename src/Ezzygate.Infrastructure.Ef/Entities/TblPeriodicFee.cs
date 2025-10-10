using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("MerchantId", "TypeId")]
[Table("tblPeriodicFee")]
public partial class TblPeriodicFee
{
    [Key]
    [Column("MerchantID")]
    public int MerchantId { get; set; }

    [Key]
    [Column("TypeID")]
    public int TypeId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NextDate { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("TblPeriodicFees")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("TblPeriodicFees")]
    public virtual TblPeriodicFeeType Type { get; set; } = null!;
}
