using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Creditcard&apos;s issuer CHB reason list
/// </summary>
[Table("ChbReasonCode", Schema = "List")]
public partial class ChbReasonCode
{
    [StringLength(15)]
    [Unicode(false)]
    public string Brand { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string ReasonCode { get; set; } = null!;

    [StringLength(100)]
    public string? Title { get; set; }

    [StringLength(1500)]
    public string? Description { get; set; }

    [StringLength(1500)]
    public string? MediaRequired { get; set; }

    [StringLength(500)]
    public string? RefundInfo { get; set; }

    [Column("IsPendingCHB")]
    public bool IsPendingChb { get; set; }

    [Key]
    [Column("ChbReasonCode_id")]
    public short ChbReasonCodeId { get; set; }
}
