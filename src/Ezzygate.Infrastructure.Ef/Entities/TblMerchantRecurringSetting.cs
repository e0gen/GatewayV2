using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblMerchantRecurringSettings")]
public partial class TblMerchantRecurringSetting
{
    [Key]
    [Column("MerchantID")]
    public int MerchantId { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsEnabledFromTransPass { get; set; }

    public bool IsEnabledModify { get; set; }

    [Column("ForceMD5OnModify")]
    public bool ForceMd5onModify { get; set; }

    public int MaxYears { get; set; }

    public int MaxCharges { get; set; }

    public int MaxStages { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("TblMerchantRecurringSetting")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
