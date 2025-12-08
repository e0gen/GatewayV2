using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class RecurringRepository : IRecurringRepository
{
    private readonly EzzygateDbContext _context;

    public RecurringRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<int?> RecurringPreCreateSeries(
        bool isPassTransaction,
        int transactionId,
        string recurringParams,
        string comments,
        int terminalCode,
        string clientIp,
        int recurringApprovalType = -1,
        string identifier = "",
        CancellationToken cancellationToken = default)
    {
        object[] parameters =
        [
            new SqlParameter("@bPass", isPassTransaction),
            new SqlParameter("@nTransaction", transactionId),
            new SqlParameter("@sRecurring", recurringParams),
            new SqlParameter("@sComments", comments),
            new SqlParameter("@nTrmCode", terminalCode),
            new SqlParameter("@sIP", clientIp),
            new SqlParameter("@nIsRecurringApproval", recurringApprovalType),
            new SqlParameter("@sIdentifier", identifier)
        ];

        var result = await _context.Database
            .SqlQueryRaw<int?>(
                // ReSharper disable once StringLiteralTypo
                "EXEC RecurringPrecreateSeries @bPass, @nTransaction, @sRecurring, @sComments, @nTrmCode, @sIP, @nIsRecurringApproval, @sIdentifier",
                // ReSharper disable once FormatStringProblem
                parameters)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}
