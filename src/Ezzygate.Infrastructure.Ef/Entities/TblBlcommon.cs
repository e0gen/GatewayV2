using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBLCommon")]
public partial class TblBlcommon
{
    [Key]
    [Column("BL_ID")]
    public int BlId { get; set; }

    [Column("BL_InsertDate", TypeName = "datetime")]
    public DateTime BlInsertDate { get; set; }

    [Column("BL_CompanyID")]
    public int? BlCompanyId { get; set; }

    [Column("BL_BlockLevel")]
    public byte BlBlockLevel { get; set; }

    [Column("BL_Value")]
    public string BlValue { get; set; } = null!;

    [Column("BL_Type")]
    public byte BlType { get; set; }

    [Column("BL_Comment")]
    [StringLength(50)]
    public string BlComment { get; set; } = null!;

    [Column("BL_User")]
    [StringLength(50)]
    public string BlUser { get; set; } = null!;

    [Column("BL_BlockSourceID")]
    public byte BlBlockSourceId { get; set; }

    [ForeignKey("BlBlockLevel")]
    [InverseProperty("TblBlcommons")]
    public virtual BlockLevel BlBlockLevelNavigation { get; set; } = null!;

    [ForeignKey("BlCompanyId")]
    [InverseProperty("TblBlcommons")]
    public virtual TblCompany? BlCompany { get; set; }
}
