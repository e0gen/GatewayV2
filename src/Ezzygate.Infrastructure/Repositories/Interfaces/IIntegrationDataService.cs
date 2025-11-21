namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IIntegrationDataService
{
    IMerchantRepository Merchants { get; }
    ICompanyChargeAdminRepository CompanyChargeAdmins { get; }
    IPaymentMethodRepository PaymentMethods { get; }
    ITransactionRepository Transactions { get; }
    IChargeAttemptRepository ChargeAttempts { get; }
    IHistoryRepository History { get; }
}