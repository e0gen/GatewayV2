using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd.Api;

public interface IRapydApiClient
{
    Task<ApiResult<RapydResponse<PaymentData>>> ProcessRequest(TransactionContext ctx,
        string midCountry, bool capture, PaymentMethodEnum method);

    Task<ApiResult<RapydResponse<RefundData>>> RefundRequest(TransactionContext ctx);
    Task<ApiResult<RapydResponse<PaymentData>>> CaptureRequest(TransactionContext ctx);
    Task<ApiResult<RapydResponse<PaymentData>>> StatusRequest(TransactionContext ctx);
    RapydResponse<PaymentData>? RestorePaymentDataResponse(string responseBody);
}