namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ICompanyChargeAdminRepository
{
    Task<string?> GetNotifyProcessUrlAsync(int companyId);
}

