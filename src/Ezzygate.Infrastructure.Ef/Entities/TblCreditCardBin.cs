using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCreditCardBIN")]
public partial class TblCreditCardBin
{
    [Key]
    [Column("BIN")]
    [StringLength(19)]
    public string Bin { get; set; } = null!;

    [Column("isoCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string IsoCode { get; set; } = null!;

    public short PaymentMethod { get; set; }

    [Column("CCName")]
    [StringLength(20)]
    public string Ccname { get; set; } = null!;

    [Column("CCType")]
    public byte Cctype { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ImportDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("BINID")]
    public int Binid { get; set; }

    [Column("BINLen")]
    public int? Binlen { get; set; }

    [Column("BINNumber")]
    public int? Binnumber { get; set; }

    public bool? IsPrepaid { get; set; }
}
