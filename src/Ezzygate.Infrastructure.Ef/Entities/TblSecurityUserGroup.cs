using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityUserGroup")]
public partial class TblSecurityUserGroup
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sug_User")]
    public int SugUser { get; set; }

    [Column("sug_Group")]
    public int SugGroup { get; set; }

    [Column("sug_IsMember")]
    public bool SugIsMember { get; set; }

    [ForeignKey("SugGroup")]
    [InverseProperty("TblSecurityUserGroups")]
    public virtual TblSecurityGroup SugGroupNavigation { get; set; } = null!;

    [ForeignKey("SugUser")]
    [InverseProperty("TblSecurityUserGroups")]
    public virtual TblSecurityUser SugUserNavigation { get; set; } = null!;
}
