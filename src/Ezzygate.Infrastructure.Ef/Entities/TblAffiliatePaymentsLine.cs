using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblAffiliatePaymentsLines")]
public partial class TblAffiliatePaymentsLine
{
    [Key]
    [Column("AFPL_ID")]
    public int AfplId { get; set; }

    [Column("AFPL_AFP_ID")]
    public int? AfplAfpId { get; set; }

    [Column("AFPL_Text")]
    [StringLength(50)]
    public string? AfplText { get; set; }

    [Column("AFPL_Quantity")]
    public int? AfplQuantity { get; set; }

    [Column("AFPL_Amount", TypeName = "smallmoney")]
    public decimal? AfplAmount { get; set; }

    [Column("AFPL_Total", TypeName = "money")]
    public decimal? AfplTotal { get; set; }

    [ForeignKey("AfplAfpId")]
    [InverseProperty("TblAffiliatePaymentsLines")]
    public virtual TblAffiliatePayment? AfplAfp { get; set; }
}
