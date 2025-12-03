namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IPaymentPageSettingsRepository
{
    Task<bool> IsTipAllowedAsync(int companyId, CancellationToken cancellationToken = default);
}