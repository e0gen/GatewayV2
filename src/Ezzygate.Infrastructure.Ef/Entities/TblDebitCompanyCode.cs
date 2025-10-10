using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitCompanyCode")]
[Index("DebitCompanyId", "Code", Name = "IX_tblDebitCompanyCode_DebitCompanyID_Code", IsUnique = true)]
public partial class TblDebitCompanyCode
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("DebitCompanyID")]
    public short DebitCompanyId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(400)]
    public string DescriptionOriginal { get; set; } = null!;

    [StringLength(550)]
    public string DescriptionMerchantHeb { get; set; } = null!;

    [StringLength(550)]
    public string DescriptionMerchantEng { get; set; } = null!;

    [StringLength(550)]
    public string DescriptionCustomerHeb { get; set; } = null!;

    [StringLength(550)]
    public string DescriptionCustomerEng { get; set; } = null!;

    public bool ChargeFail { get; set; }

    public int RecurringAttemptsCharge { get; set; }

    public int RecurringAttemptsSeries { get; set; }

    public byte FailSource { get; set; }

    [StringLength(11)]
    public string LocalError { get; set; } = null!;

    [Column("BlockCC")]
    public bool BlockCc { get; set; }

    public bool BlockMail { get; set; }

    public bool IsCascade { get; set; }
}
