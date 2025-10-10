using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSystemBankList")]
public partial class TblSystemBankList
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("bankCode")]
    public int BankCode { get; set; }

    [Column("bankName")]
    [StringLength(255)]
    public string BankName { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Column("zip")]
    [StringLength(255)]
    public string Zip { get; set; } = null!;

    [Column("phone")]
    [StringLength(255)]
    public string Phone { get; set; } = null!;

    [Column("fax")]
    [StringLength(255)]
    public string Fax { get; set; } = null!;

    [InverseProperty("PaymentBankNavigation")]
    public virtual ICollection<TblCompany> TblCompanies { get; set; } = new List<TblCompany>();
}
