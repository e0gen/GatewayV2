using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewRandNumber
{
    public double? RandNumber { get; set; }
}
