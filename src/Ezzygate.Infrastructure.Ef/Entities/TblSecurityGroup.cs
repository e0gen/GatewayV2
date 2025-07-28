using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityGroup")]
public partial class TblSecurityGroup
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sg_Name")]
    [StringLength(50)]
    public string SgName { get; set; } = null!;

    [Column("sg_Description")]
    [StringLength(50)]
    public string SgDescription { get; set; } = null!;

    [Column("sg_IsActive")]
    public bool SgIsActive { get; set; }

    [Column("sg_SeeUnmanaged")]
    public bool SgSeeUnmanaged { get; set; }

    [Column("sg_IsLogged")]
    public bool SgIsLogged { get; set; }

    [InverseProperty("SdgGroupNavigation")]
    public virtual ICollection<TblSecurityDocumentGroup> TblSecurityDocumentGroups { get; set; } = new List<TblSecurityDocumentGroup>();

    [InverseProperty("SugGroupNavigation")]
    public virtual ICollection<TblSecurityUserGroup> TblSecurityUserGroups { get; set; } = new List<TblSecurityUserGroup>();
}
