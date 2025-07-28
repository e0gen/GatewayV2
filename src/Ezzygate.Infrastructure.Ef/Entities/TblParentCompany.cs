using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblParentCompany")]
public partial class TblParentCompany
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("pc_Code")]
    [StringLength(20)]
    public string PcCode { get; set; } = null!;

    [Column("pc_RecurringURL")]
    [StringLength(100)]
    public string PcRecurringUrl { get; set; } = null!;

    [Column("pc_IsDefault")]
    public bool PcIsDefault { get; set; }

    [Column("pc_RecurringEcheckURL")]
    [StringLength(100)]
    public string PcRecurringEcheckUrl { get; set; } = null!;

    [Column("pc_SMS_URL")]
    [StringLength(100)]
    public string PcSmsUrl { get; set; } = null!;

    [Column("pc_TemplateName")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PcTemplateName { get; set; }

    [InverseProperty("ParentCompanyNavigation")]
    public virtual ICollection<TblCompany> TblCompanies { get; set; } = new List<TblCompany>();
}
