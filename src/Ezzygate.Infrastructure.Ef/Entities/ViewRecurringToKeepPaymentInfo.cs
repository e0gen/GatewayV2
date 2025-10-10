using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewRecurringToKeepPaymentInfo
{
    [Column("rc_CreditCard")]
    public int? RcCreditCard { get; set; }
}
