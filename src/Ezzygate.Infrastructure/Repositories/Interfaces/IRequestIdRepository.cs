namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IRequestIdRepository
{
    Task<bool> IsRequestIdUsedAsync(string requestId);
    Task MarkRequestIdAsUsedAsync(string requestId);
    Task CleanupExpiredRequestIdsAsync();
} 