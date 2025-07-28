using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TableList", Schema = "List")]
public partial class TableList
{
    [Key]
    [Column("TableList_id")]
    public byte TableListId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("TableList")]
    public virtual ICollection<ShippingDetail> ShippingDetails { get; set; } = new List<ShippingDetail>();
}
