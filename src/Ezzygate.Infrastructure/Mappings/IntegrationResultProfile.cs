using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Transactions;

namespace Ezzygate.Infrastructure.Mappings;

public static class IntegrationResultProfile
{
    public static IntegrationResult ToIntegrationResult(this TransactionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return new IntegrationResult
        {
            Code = context.ReplyCode,
            Message = context.ErrorMessage,
            ApprovalNumber = context.ApprovalNumber,
            DebitRefCode = context.DebitRefCode,
            DebitRefNum = context.DebitRefNum,
            TerminalNumber = context.TerminalNumber,
            TrxId = context.TrxId,
            DebitCompanyId = context.DebitCompany?.Id,
            IsFinalized = context.IsFinalized,
            TrxType = context.TransType,
            RedirectUrl = context.RedirectUrl,
        };
    }
}