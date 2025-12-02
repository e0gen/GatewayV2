using Ezzygate.Infrastructure.Processing.Models;

namespace Ezzygate.Infrastructure.Processing;

public interface ILegacyPaymentService
{
    Task<LegacyPaymentResult> ProcessAsync(LegacyProcessRequest request, CancellationToken cancellationToken = default);
    Task<LegacyPaymentResult> RefundAsync(LegacyRefundRequest request, CancellationToken cancellationToken = default);
    Task<LegacyPaymentResult> VoidAsync(LegacyVoidRequest request, CancellationToken cancellationToken = default);
    Task<LegacyPaymentResult> CaptureAsync(LegacyCaptureRequest request, CancellationToken cancellationToken = default);
}