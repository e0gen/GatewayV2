using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyFeesFloor")]
public partial class TblCompanyFeesFloor
{
    [Key]
    [Column("CFF_ID")]
    public int CffId { get; set; }

    [Column("CFF_CompanyID")]
    public int CffCompanyId { get; set; }

    [Column("CFF_TotalTo", TypeName = "money")]
    public decimal CffTotalTo { get; set; }

    [Column("CFF_Precent", TypeName = "money")]
    public decimal CffPrecent { get; set; }

    [ForeignKey("CffCompanyId")]
    [InverseProperty("TblCompanyFeesFloors")]
    public virtual TblCompany CffCompany { get; set; } = null!;
}
