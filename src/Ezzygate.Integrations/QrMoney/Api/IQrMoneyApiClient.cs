using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;

namespace Ezzygate.Integrations.QrMoney.Api;

public interface IQrMoneyApiClient
{
    Task<ApiResult<QrMoneyPaymentResponse>> PaymentRequest(TransactionContext ctx);
    Task<ApiResult<QrMoneyStatusResponse>> StatusRequest(TransactionContext ctx);
    Task<ApiResult<EmptyResponse>> RefundRequest(TransactionContext ctx);
}