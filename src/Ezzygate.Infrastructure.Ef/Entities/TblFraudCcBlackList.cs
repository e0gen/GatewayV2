using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblFraudCcBlackList")]
public partial class TblFraudCcBlackList
{
    [Key]
    [Column("fraudCcBlackList_id")]
    public int FraudCcBlackListId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("fcbl_ccDisplay")]
    [StringLength(30)]
    public string FcblCcDisplay { get; set; } = null!;

    [Column("fcbl_comment")]
    [StringLength(500)]
    public string FcblComment { get; set; } = null!;

    [Column("fcbl_ccNumber256")]
    [MaxLength(200)]
    public byte[]? FcblCcNumber256 { get; set; }

    [Column("fcbl_InsertDate", TypeName = "datetime")]
    public DateTime FcblInsertDate { get; set; }

    [Column("fcbl_BlockLevel")]
    public byte FcblBlockLevel { get; set; }

    [Column("fcbl_BlockCount")]
    public int FcblBlockCount { get; set; }

    [Column("fcbl_ReplyCode")]
    [StringLength(11)]
    public string FcblReplyCode { get; set; } = null!;

    [Column("fcbl_UnblockDate", TypeName = "datetime")]
    public DateTime FcblUnblockDate { get; set; }

    [Column("fcbl_CCRMID")]
    public int FcblCcrmid { get; set; }

    [Column("PaymentMethod_id")]
    public short? PaymentMethodId { get; set; }

    [ForeignKey("FcblBlockLevel")]
    [InverseProperty("TblFraudCcBlackLists")]
    public virtual BlockLevel FcblBlockLevelNavigation { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("TblFraudCcBlackLists")]
    public virtual PaymentMethod? PaymentMethod { get; set; }
}
