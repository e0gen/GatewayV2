using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd.Api;

public interface IRapydApiClient
{
    Task<ApiResult<RapydResponse<PaymentData>>> ProcessRequestAsync(TransactionContext ctx,
        string midCountry, bool capture, PaymentMethodEnum method, CancellationToken cancellationToken = default);

    Task<ApiResult<RapydResponse<RefundData>>> RefundRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<RapydResponse<PaymentData>>> CaptureRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    Task<ApiResult<RapydResponse<PaymentData>>> StatusRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default);
    RapydResponse<PaymentData>? RestorePaymentDataResponse(string responseBody);
}