using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblExternalCardCustomer")]
public partial class TblExternalCardCustomer
{
    [Key]
    [Column("ExternalCardCustomer_id")]
    public int ExternalCardCustomerId { get; set; }

    [Column("ExternalCardTerminal_id")]
    public int ExternalCardTerminalId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("UniqueID")]
    public Guid UniqueId { get; set; }

    [StringLength(200)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public short Status { get; set; }

    [StringLength(255)]
    public string? Comment { get; set; }

    [Column(TypeName = "xml")]
    public string? ExtraData { get; set; }

    [ForeignKey("ExternalCardTerminalId")]
    [InverseProperty("TblExternalCardCustomers")]
    public virtual TblExternalCardTerminal ExternalCardTerminal { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("TblExternalCardCustomers")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [InverseProperty("ExternalCardCustomer")]
    public virtual ICollection<TblExternalCardCustomerPayment> TblExternalCardCustomerPayments { get; set; } = new List<TblExternalCardCustomerPayment>();
}
