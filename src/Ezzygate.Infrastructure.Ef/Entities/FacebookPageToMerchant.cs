using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("FacebookPageToMerchant")]
public partial class FacebookPageToMerchant
{
    [Key]
    [Column("FacebookPageToMerchant_id")]
    public int FacebookPageToMerchantId { get; set; }

    [Column("FBPageID")]
    public long FbpageId { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string Merchant { get; set; } = null!;
}
