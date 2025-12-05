using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;

namespace Ezzygate.Integrations.Paysafe.Api;

public interface IPaysafeApiClient
{
    Task<ApiResult<CreatePaymentHandlerResponse>> CreatePaymentHandlerAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<PaymentResponse>> ProcessPaymentAsync(TransactionContext ctx, bool onlyAuthorize, CancellationToken cancellationToken = default);
    Task<ApiResult<SettlementResponse>> ProcessSettlementAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<VoidAuthorizationResponse>> VoidAuthorizationAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<SettlementResponse>> GetSettlementAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<EmptyResponse>> CancelSettlementAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<RefundResponse>> ProcessRefundAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
}

