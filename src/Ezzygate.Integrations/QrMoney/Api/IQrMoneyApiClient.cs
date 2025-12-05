using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;

namespace Ezzygate.Integrations.QrMoney.Api;

public interface IQrMoneyApiClient
{
    Task<ApiResult<QrMoneyPaymentResponse>> PaymentRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<QrMoneyStatusResponse>> StatusRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<EmptyResponse>> RefundRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
}