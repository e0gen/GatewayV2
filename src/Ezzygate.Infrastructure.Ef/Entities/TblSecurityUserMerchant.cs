using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityUserMerchant")]
[Index("SumUser", "SumMerchant", Name = "IX_tblSecurityUserMerchant_User_Merchant", IsUnique = true)]
public partial class TblSecurityUserMerchant
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sum_User")]
    public int SumUser { get; set; }

    [Column("sum_Merchant")]
    public int SumMerchant { get; set; }

    [ForeignKey("SumMerchant")]
    [InverseProperty("TblSecurityUserMerchants")]
    public virtual TblCompany SumMerchantNavigation { get; set; } = null!;

    [ForeignKey("SumUser")]
    [InverseProperty("TblSecurityUserMerchants")]
    public virtual TblSecurityUser SumUserNavigation { get; set; } = null!;
}
