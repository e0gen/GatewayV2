using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransSource", Schema = "List")]
public partial class TransSource
{
    [Key]
    [Column("TransSource_id")]
    public byte TransSourceId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? NameHeb { get; set; }

    [StringLength(50)]
    public string? NameEng { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? TransSourceCode { get; set; }

    [InverseProperty("TransSource")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("TransSource")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("TransSource")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("TransSource")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();
}
