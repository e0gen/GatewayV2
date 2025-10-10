using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
[Table("RunningProcess", Schema = "Track")]
public partial class RunningProcess
{
    [MaxLength(40)]
    public byte[] CreditCardNumber256 { get; set; } = null!;

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Precision(2)]
    public DateTime InsertDate { get; set; }
}
