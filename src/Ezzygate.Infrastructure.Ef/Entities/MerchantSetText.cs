using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantSetText", Schema = "Setting")]
public partial class MerchantSetText
{
    [Key]
    [Column("MerchantSetText_id")]
    public int MerchantSetTextId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("Language_id")]
    public byte LanguageId { get; set; }

    [Column("SolutionList_id")]
    public byte SolutionListId { get; set; }

    [StringLength(50)]
    public string? TextKey { get; set; }

    [StringLength(2500)]
    public string? TextValue { get; set; }

    public bool IsDefault { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("MerchantSetTexts")]
    public virtual LanguageList Language { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("MerchantSetTexts")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("SolutionListId")]
    [InverseProperty("MerchantSetTexts")]
    public virtual SolutionList SolutionList { get; set; } = null!;
}
