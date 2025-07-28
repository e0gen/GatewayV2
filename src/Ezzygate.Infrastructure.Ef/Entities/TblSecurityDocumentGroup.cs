using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityDocumentGroup")]
public partial class TblSecurityDocumentGroup
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sdg_Document")]
    public int SdgDocument { get; set; }

    [Column("sdg_Group")]
    public int SdgGroup { get; set; }

    [Column("sdg_IsVisible")]
    public bool SdgIsVisible { get; set; }

    [Column("sdg_IsActive")]
    public bool SdgIsActive { get; set; }

    [Column("sdg_GroupID")]
    public int? SdgGroupId { get; set; }

    [Column("sdg_UserID")]
    public int? SdgUserId { get; set; }

    [ForeignKey("SdgDocument")]
    [InverseProperty("TblSecurityDocumentGroups")]
    public virtual TblSecurityDocument SdgDocumentNavigation { get; set; } = null!;

    [ForeignKey("SdgGroupId")]
    [InverseProperty("TblSecurityDocumentGroups")]
    public virtual TblSecurityGroup? SdgGroupNavigation { get; set; }

    [ForeignKey("SdgUserId")]
    [InverseProperty("TblSecurityDocumentGroups")]
    public virtual TblSecurityUser? SdgUser { get; set; }
}
