using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Updates of fraud waiting to be matched to a transaction, records here are deleted after moved to trans history
/// </summary>
[Table("TransMatchPending", Schema = "Data")]
public partial class TransMatchPending
{
    [Key]
    [Column("TransMatchPending_id")]
    public int TransMatchPendingId { get; set; }

    [Column("ExternalServiceType_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? ExternalServiceTypeId { get; set; }

    [Column("DebitCompany_id")]
    public int? DebitCompanyId { get; set; }

    [Precision(0)]
    public DateTime? InsertDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? FileName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ReasonCode { get; set; }

    [Column(TypeName = "decimal(19, 2)")]
    public decimal? Amount { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? CardNumber { get; set; }

    [Precision(0)]
    public DateTime? TransDate { get; set; }

    [Column("ARN")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Arn { get; set; }

    [StringLength(50)]
    public string? DebitReferenceCode { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AcquirerReferenceNum { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ApprovalNumber { get; set; }

    [Precision(0)]
    public DateTime? MatchDate { get; set; }

    public byte? MatchCount { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("TransMatchPendings")]
    public virtual CurrencyList? CurrencyIsocodeNavigation { get; set; }

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("TransMatchPendings")]
    public virtual TblDebitCompany? DebitCompany { get; set; }

    [ForeignKey("ExternalServiceTypeId")]
    [InverseProperty("TransMatchPendings")]
    public virtual ExternalServiceType? ExternalServiceType { get; set; }
}
