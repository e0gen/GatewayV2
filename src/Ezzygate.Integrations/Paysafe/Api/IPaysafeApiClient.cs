using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;

namespace Ezzygate.Integrations.Paysafe.Api;

public interface IPaysafeApiClient
{
    Task<ApiResult<CreatePaymentHandlerResponse>> CreatePaymentHandlerAsync(TransactionContext ctx);
    Task<ApiResult<PaymentResponse>> ProcessPaymentAsync(TransactionContext ctx, bool onlyAuthorize);
    Task<ApiResult<SettlementResponse>> ProcessSettlementAsync(TransactionContext ctx);
    Task<ApiResult<VoidAuthorizationResponse>> VoidAuthorizationAsync(TransactionContext ctx);
    Task<ApiResult<SettlementResponse>> GetSettlementAsync(TransactionContext ctx);
    Task<ApiResult<EmptyResponse>> CancelSettlementAsync(TransactionContext ctx);
    Task<ApiResult<RefundResponse>> ProcessRefundAsync(TransactionContext ctx);
}

