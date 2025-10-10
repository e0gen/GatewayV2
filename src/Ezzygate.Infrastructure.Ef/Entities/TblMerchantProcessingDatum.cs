using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblMerchantProcessingData")]
public partial class TblMerchantProcessingDatum
{
    [Key]
    [Column("MerchantID")]
    public int MerchantId { get; set; }

    [Column("MPD_CffCurAmount", TypeName = "money")]
    public decimal MpdCffCurAmount { get; set; }

    [Column("MPD_CffResetDate")]
    public DateOnly MpdCffResetDate { get; set; }

    [Column("MPD_CffCurAmount2", TypeName = "money")]
    public decimal MpdCffCurAmount2 { get; set; }

    [Column("MPD_CffResetDate2")]
    public DateOnly MpdCffResetDate2 { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("TblMerchantProcessingDatum")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
