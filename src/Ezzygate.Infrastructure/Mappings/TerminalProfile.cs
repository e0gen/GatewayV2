using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Mappings;

public static class TerminalProfile
{
    public static Terminal ToDomain(this TblDebitTerminal entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        return new Terminal
        {
            Id = entity.Id,
            TerminalNumber = entity.TerminalNumber,
            AccountId = entity.AccountId,
            AccountSubId = entity.AccountSubId,
            TerminalNumber3D = entity.TerminalNumber3D,
            AccountId3D = entity.AccountId3D,
            AccountSubId3D = entity.AccountSubId3D,
            IsTestTerminal = entity.DtIsTestTerminal,
            IsActive = entity.IsActive,
            Enable3DSecure = entity.DtEnable3dsecure,
            DebitCompanyId = entity.DebitCompany,
            AuthenticationCode1 = entity.AuthenticationCode1,
            AuthenticationCode3D = entity.AuthenticationCode3D,
            AccountPassword256 = entity.AccountPassword256,
        };
    }
}