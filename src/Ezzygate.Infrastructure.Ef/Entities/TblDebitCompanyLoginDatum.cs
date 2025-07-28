using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitCompanyLoginData")]
public partial class TblDebitCompanyLoginDatum
{
    [Key]
    [Column("debitCompanyLoginData_id")]
    public int DebitCompanyLoginDataId { get; set; }

    [Column("debitCompany_id")]
    public int DebitCompanyId { get; set; }

    [Column("username")]
    [StringLength(20)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(20)]
    public string Password { get; set; } = null!;
}
