using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AuthorizationBatch", Schema = "Trans")]
public partial class AuthorizationBatch
{
    [Key]
    [Column("AuthorizationBatch_id")]
    public int AuthorizationBatchId { get; set; }

    [Precision(2)]
    public DateTime BatchDate { get; set; }

    public int BatchInternalKey { get; set; }

    [StringLength(20)]
    public string? TransTerminalNumber { get; set; }

    [Column("TransDebitCompanyID")]
    public int? TransDebitCompanyId { get; set; }

    public int? TransCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TransTotalDebit { get; set; }

    [Column(TypeName = "money")]
    public decimal? TransTotalCredit { get; set; }

    [StringLength(50)]
    public string? ResultDescription { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BatchFileName { get; set; }

    [Column("ActionStatus_id")]
    public byte ActionStatusId { get; set; }

    [ForeignKey("ActionStatusId")]
    [InverseProperty("AuthorizationBatches")]
    public virtual ActionStatus ActionStatus { get; set; } = null!;

    [InverseProperty("AuthorizationBatch")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();
}
