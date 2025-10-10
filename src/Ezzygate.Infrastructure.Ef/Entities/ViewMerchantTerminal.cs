using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewMerchantTerminal
{
    [Column("MerchantID")]
    public int? MerchantId { get; set; }

    [Column("DebitCompanyID")]
    public byte DebitCompanyId { get; set; }
}
