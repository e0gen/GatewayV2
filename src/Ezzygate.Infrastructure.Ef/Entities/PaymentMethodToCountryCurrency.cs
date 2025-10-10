using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
[Table("PaymentMethodToCountryCurrency", Schema = "List")]
[Index("CountryId", "PaymentMethodId", Name = "IX_PaymentMethodToCountryCurrency_CountryID_PaymentMethodID")]
[Index("CurrencyId", "PaymentMethodId", Name = "IX_PaymentMethodToCountryCurrency_CurrencyID_PaymentMethodID")]
[Index("PaymentMethodId", "CountryId", "CurrencyId", Name = "IX_PaymentMethodToCountryCurrency_PaymentMethodID", IsUnique = true)]
public partial class PaymentMethodToCountryCurrency
{
    [Column("PaymentMethod_id")]
    public short PaymentMethodId { get; set; }

    [Column("Country_id")]
    public short? CountryId { get; set; }

    [Column("Currency_id")]
    public short? CurrencyId { get; set; }

    [ForeignKey("PaymentMethodId")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
