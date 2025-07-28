using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("EmailAddress", "CreditCardNumber256")]
[Table("CcMailUsage", Schema = "Risk")]
public partial class CcMailUsage
{
    [Key]
    [MaxLength(40)]
    public byte[] CreditCardNumber256 { get; set; } = null!;

    [Key]
    [StringLength(100)]
    public string EmailAddress { get; set; } = null!;
}
