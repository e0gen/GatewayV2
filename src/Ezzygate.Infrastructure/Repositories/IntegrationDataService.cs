using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class IntegrationDataService : IIntegrationDataService
{
    public IntegrationDataService(
        IMerchantRepository merchantRepository,
        ICompanyChargeAdminRepository companyChargeAdminRepository,
        IPaymentMethodRepository paymentMethodRepository,
        ITransactionRepository transactionRepository,
        IChargeAttemptRepository chargeAttemptRepository,
        IHistoryRepository historyRepository,
        IEventPendingRepository eventPendingRepository)
    {
        Merchants = merchantRepository;
        CompanyChargeAdmins = companyChargeAdminRepository;
        PaymentMethods = paymentMethodRepository;
        Transactions = transactionRepository;
        ChargeAttempts = chargeAttemptRepository;
        History = historyRepository;
        EventPendings = eventPendingRepository;
    }

    public IMerchantRepository Merchants { get; }

    public ICompanyChargeAdminRepository CompanyChargeAdmins { get; }

    public IPaymentMethodRepository PaymentMethods { get; }

    public ITransactionRepository Transactions { get; }

    public IChargeAttemptRepository ChargeAttempts { get; }

    public IHistoryRepository History { get; }

    public IEventPendingRepository EventPendings { get; }
}