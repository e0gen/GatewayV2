using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyCreditFeesTerminals")]
public partial class TblCompanyCreditFeesTerminal
{
    [Key]
    [Column("CCFT_ID")]
    public int CcftId { get; set; }

    [Column("CCFT_Terminal")]
    [StringLength(20)]
    public string CcftTerminal { get; set; } = null!;

    [Column("CCFT_Ratio")]
    public int CcftRatio { get; set; }

    [Column("CCFT_UseCount")]
    public int CcftUseCount { get; set; }

    [Column("CCFT_CompanyID")]
    public int? CcftCompanyId { get; set; }

    [Column("CCFT_CCF_ID")]
    public int? CcftCcfId { get; set; }

    [Column("CCFT_Amount", TypeName = "decimal(10, 2)")]
    public decimal CcftAmount { get; set; }

    [ForeignKey("CcftCcfId")]
    [InverseProperty("TblCompanyCreditFeesTerminals")]
    public virtual TblCompanyCreditFee? CcftCcf { get; set; }

    [ForeignKey("CcftCompanyId")]
    [InverseProperty("TblCompanyCreditFeesTerminals")]
    public virtual TblCompany? CcftCompany { get; set; }
}
