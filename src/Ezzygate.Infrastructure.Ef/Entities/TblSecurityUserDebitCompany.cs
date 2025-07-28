using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityUserDebitCompany")]
public partial class TblSecurityUserDebitCompany
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sudc_User")]
    public int SudcUser { get; set; }

    [Column("sudc_DebitCompany")]
    public int SudcDebitCompany { get; set; }

    [ForeignKey("SudcDebitCompany")]
    [InverseProperty("TblSecurityUserDebitCompanies")]
    public virtual TblDebitCompany SudcDebitCompanyNavigation { get; set; } = null!;

    [ForeignKey("SudcUser")]
    [InverseProperty("TblSecurityUserDebitCompanies")]
    public virtual TblSecurityUser SudcUserNavigation { get; set; } = null!;
}
