using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewBin
{
    [Column("BIN")]
    [StringLength(19)]
    public string Bin { get; set; } = null!;

    [Column("isoCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string IsoCode { get; set; } = null!;

    [Column("CCName")]
    [StringLength(20)]
    public string Ccname { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? ImportDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    public byte PaymentMethod { get; set; }

    [StringLength(123)]
    public string DeleteButton { get; set; } = null!;
}
