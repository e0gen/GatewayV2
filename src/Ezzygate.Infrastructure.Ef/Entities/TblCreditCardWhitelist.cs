using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("CcwlMerchant", "CcwlCardNumber256")]
[Table("tblCreditCardWhitelist")]
[Index("Id", Name = "IX_tblCreditCardWhitelist_ID", IsUnique = true)]
[Index("CcwlInsertDate", Name = "IX_tblCreditCardWhitelist_InsertDate", AllDescending = true)]
public partial class TblCreditCardWhitelist
{
    [Column("ID")]
    public int Id { get; set; }

    [Key]
    [Column("ccwl_Merchant")]
    public int CcwlMerchant { get; set; }

    [Key]
    [Column("ccwl_CardNumber256")]
    [MaxLength(200)]
    public byte[] CcwlCardNumber256 { get; set; } = null!;

    [Column("ccwl_PaymentMethod")]
    public short? CcwlPaymentMethod { get; set; }

    [Column("ccwl_Last4")]
    public short? CcwlLast4 { get; set; }

    [Column("ccwl_Bin")]
    public int? CcwlBin { get; set; }

    [Column("ccwl_BinCountry")]
    [StringLength(2)]
    [Unicode(false)]
    public string? CcwlBinCountry { get; set; }

    [Column("ccwl_CardHolder")]
    [StringLength(100)]
    public string? CcwlCardHolder { get; set; }

    [Column("ccwl_ExpMonth")]
    public byte? CcwlExpMonth { get; set; }

    [Column("ccwl_ExpYear")]
    public short? CcwlExpYear { get; set; }

    [Column("ccwl_BurnDate", TypeName = "datetime")]
    public DateTime? CcwlBurnDate { get; set; }

    [Column("ccwl_Level")]
    public byte CcwlLevel { get; set; }

    [Column("ccwl_Comment")]
    [StringLength(200)]
    public string? CcwlComment { get; set; }

    [Column("ccwl_InsertDate", TypeName = "datetime")]
    public DateTime CcwlInsertDate { get; set; }

    [Column("ccwl_Username")]
    [StringLength(50)]
    [Unicode(false)]
    public string CcwlUsername { get; set; } = null!;

    [Column("ccwl_IP")]
    [StringLength(50)]
    [Unicode(false)]
    public string CcwlIp { get; set; } = null!;

    [Column("ccwl_IsBurnt")]
    public bool? CcwlIsBurnt { get; set; }
}
