using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBlackListBIN")]
public partial class TblBlackListBin
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("BIN")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bin { get; set; } = null!;

    [Column("PrimaryID")]
    public int? PrimaryId { get; set; }

    [Column("TransFailCountCC")]
    public int TransFailCountCc { get; set; }

    [InverseProperty("Primary")]
    public virtual ICollection<TblBlackListBin> InversePrimary { get; set; } = new List<TblBlackListBin>();

    [ForeignKey("PrimaryId")]
    [InverseProperty("InversePrimary")]
    public virtual TblBlackListBin? Primary { get; set; }
}
