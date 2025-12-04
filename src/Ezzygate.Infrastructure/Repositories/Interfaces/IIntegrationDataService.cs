using Ezzygate.Infrastructure.Services;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IIntegrationDataService
{
    IMerchantRepository Merchants { get; }
    ICompanyChargeAdminRepository CompanyChargeAdmins { get; }
    IPaymentMethodRepository PaymentMethods { get; }
    ITransactionRepository Transactions { get; }
    ITransactionService TransactionService { get; }
    ITerminalRepository Terminals { get; }
    IChargeAttemptRepository ChargeAttempts { get; }
    IHistoryRepository History { get; }
    IEventPendingRepository EventPendings { get; }
}