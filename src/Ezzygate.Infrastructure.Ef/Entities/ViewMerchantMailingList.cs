using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewMerchantMailingList
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string CustomerNumber { get; set; } = null!;

    [StringLength(80)]
    public string Status { get; set; } = null!;

    [StringLength(303)]
    public string? ToName { get; set; }

    [StringLength(255)]
    public string? ToMail { get; set; }
}
