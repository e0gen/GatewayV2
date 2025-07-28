using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblAffiliatesCount")]
public partial class TblAffiliatesCount
{
    [Key]
    [Column("AFCID")]
    public int Afcid { get; set; }

    [Column("AFCAFFID")]
    public int? Afcaffid { get; set; }

    [Column("AFCType")]
    public byte? Afctype { get; set; }

    [Column("AFCIPAddress")]
    [StringLength(15)]
    public string? Afcipaddress { get; set; }

    [Column("AFCDate")]
    public DateOnly? Afcdate { get; set; }

    [Column("AFCReferal")]
    [StringLength(255)]
    public string? Afcreferal { get; set; }

    [ForeignKey("Afcaffid")]
    [InverseProperty("TblAffiliatesCounts")]
    public virtual TblAffiliate? Afcaff { get; set; }
}
