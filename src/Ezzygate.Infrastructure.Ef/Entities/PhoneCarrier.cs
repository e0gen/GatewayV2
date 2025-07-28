using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PhoneCarrier", Schema = "List")]
public partial class PhoneCarrier
{
    [Key]
    [Column("PhoneCarrier_id")]
    public short PhoneCarrierId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("PhoneCarrier")]
    public virtual ICollection<PhoneDetail> PhoneDetails { get; set; } = new List<PhoneDetail>();
}
