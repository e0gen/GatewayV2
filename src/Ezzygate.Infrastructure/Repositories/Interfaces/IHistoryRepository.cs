using System.Xml.Linq;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IHistoryRepository
{
    Task<int> CreateAsync(
        byte historyTypeId,
        int? merchantId,
        int? sourceIdentity,
        XElement variableXml,
        CancellationToken cancellationToken = default);
}