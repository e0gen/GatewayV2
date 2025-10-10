using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewGetNewid
{
    public Guid? Value { get; set; }
}
