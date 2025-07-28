using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ApplicationIdentityToMerchantGroup", Schema = "Data")]
public partial class ApplicationIdentityToMerchantGroup
{
    [Key]
    [Column("ApplicationIdentityToMerchantGroup_id")]
    public int ApplicationIdentityToMerchantGroupId { get; set; }

    [Column("ApplicationIdentity_id")]
    public int ApplicationIdentityId { get; set; }

    [Column("MerchantGroup_id")]
    public int MerchantGroupId { get; set; }

    [ForeignKey("ApplicationIdentityId")]
    [InverseProperty("ApplicationIdentityToMerchantGroups")]
    public virtual ApplicationIdentity ApplicationIdentity { get; set; } = null!;

    [ForeignKey("MerchantGroupId")]
    [InverseProperty("ApplicationIdentityToMerchantGroups")]
    public virtual TblMerchantGroup MerchantGroup { get; set; } = null!;
}
