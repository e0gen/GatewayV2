using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Mappings;

public static class DebitCompanyProfile
{
    public static DebitCompany ToDomain(this TblDebitCompany entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        return new DebitCompany
        {
            Id = entity.DebitCompanyId,
            Name = entity.DcName,
            IsActive = entity.DcIsActive,
            IntegrationTag = entity.IntegrationTag,
        };
    }

    
}