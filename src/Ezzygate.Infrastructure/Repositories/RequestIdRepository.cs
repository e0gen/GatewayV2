using Microsoft.Extensions.Caching.Memory;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class RequestIdRepository : IRequestIdRepository
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _expirationTime;

    public RequestIdRepository(IMemoryCache cache)
    {
        _cache = cache;
        _expirationTime = TimeSpan.FromMinutes(30);
    }

    public Task<bool> IsRequestIdUsedAsync(string requestId)
    {
        var isUsed = _cache.TryGetValue($"request_id_{requestId}", out _);
        return Task.FromResult(isUsed);
    }

    public Task MarkRequestIdAsUsedAsync(string requestId)
    {
        _cache.Set($"request_id_{requestId}", true, _expirationTime);
        return Task.CompletedTask;
    }

    public Task CleanupExpiredRequestIdsAsync()
    {
        return Task.CompletedTask;
    }
} 