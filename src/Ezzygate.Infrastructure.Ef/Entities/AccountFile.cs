using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountFile", Schema = "Data")]
public partial class AccountFile
{
    [Key]
    [Column("AccountFile_id")]
    public int AccountFileId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [StringLength(250)]
    public string? FileTitle { get; set; }

    [StringLength(250)]
    public string? FileName { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? FileExt { get; set; }

    [Column("FileItemType_id")]
    public byte? FileItemTypeId { get; set; }

    [StringLength(500)]
    public string? AdminComment { get; set; }

    [Precision(0)]
    public DateTime? AdminApprovalDate { get; set; }

    [StringLength(50)]
    public string? AdminApprovalUser { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountFiles")]
    public virtual Account? Account { get; set; }

    [ForeignKey("FileItemTypeId")]
    [InverseProperty("AccountFiles")]
    public virtual FileItemType? FileItemType { get; set; }
}
