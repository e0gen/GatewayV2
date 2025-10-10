using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogImportEPA")]
public partial class TblLogImportEpa
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TransID")]
    public int TransId { get; set; }

    public byte Installment { get; set; }

    public bool IsPaid { get; set; }

    public bool IsRefunded { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PaidInsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RefundedInsertDate { get; set; }

    public bool? IsManual { get; set; }

    [StringLength(21)]
    [Unicode(false)]
    public string? TransInstallment { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AdminUser { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AcquirerReferenceNum { get; set; }

    [ForeignKey("TransId")]
    [InverseProperty("TblLogImportEpas")]
    public virtual TblCompanyTransPass Trans { get; set; } = null!;
}
