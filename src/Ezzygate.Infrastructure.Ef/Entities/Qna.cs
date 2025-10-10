using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("QNA")]
public partial class Qna
{
    [Key]
    [Column("QNA_id")]
    public int QnaId { get; set; }

    [Column("QNAGroup_id")]
    public int? QnagroupId { get; set; }

    [StringLength(1000)]
    public string? Question { get; set; }

    [StringLength(4000)]
    public string? Answer { get; set; }

    public int Rating { get; set; }

    public bool IsVisible { get; set; }

    [ForeignKey("QnagroupId")]
    [InverseProperty("Qnas")]
    public virtual Qnagroup? Qnagroup { get; set; }
}
