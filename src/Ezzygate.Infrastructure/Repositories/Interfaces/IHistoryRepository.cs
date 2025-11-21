namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IHistoryRepository
{
    Task<int> CreateAsync(
        byte historyTypeId,
        int? merchantId,
        int? sourceIdentity,
        string variableXml,
        CancellationToken cancellationToken = default);
}