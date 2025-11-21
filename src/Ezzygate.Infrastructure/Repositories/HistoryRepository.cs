using System.Xml.Linq;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly EzzygateDbContext _context;

    public HistoryRepository(EzzygateDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> CreateAsync(
        byte historyTypeId,
        int? merchantId,
        int? sourceIdentity,
        XElement variableXml,
        CancellationToken cancellationToken = default)
    {
        var history = new Ef.Entities.History
        {
            HistoryTypeId = historyTypeId,
            MerchantId = merchantId,
            SourceIdentity = sourceIdentity,
            InsertDate = DateTime.UtcNow,
            VariableXml = variableXml.ToString(SaveOptions.DisableFormatting)
        };

        _context.Histories.Add(history);
        await _context.SaveChangesAsync(cancellationToken);

        return history.HistoryId;
    }
}