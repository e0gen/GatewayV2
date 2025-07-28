using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ActiveStatus", Schema = "List")]
public partial class ActiveStatus
{
    [Key]
    [Column("ActiveStatus_id")]
    public byte ActiveStatusId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("ActiveStatus")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
