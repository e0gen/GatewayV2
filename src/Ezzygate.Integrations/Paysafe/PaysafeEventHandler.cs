using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Integrations.Core.Events;
using Ezzygate.Integrations.Extensions;
using Ezzygate.Integrations.Paysafe.Api;
using Ezzygate.Integrations.Paysafe.Models;

namespace Ezzygate.Integrations.Paysafe;

public sealed class PaysafeEventHandler : IntegrationEventHandler
{
    private const string SecretKey = "Paysafe-Webhook-Secret";

    private static readonly HashSet<string> SupportedEvents = new(StringComparer.OrdinalIgnoreCase)
    {
        "PAYMENT_HANDLE_PAYABLE",
        "PAYMENT_HANDLE_EXPIRED",
        "PAYMENT_HANDLE_FAILED",
        "PAYMENT_HANDLE_ERRORED",
        "PAYMENT_HANDLE_DELETED",
        "PAYMENT_COMPLETED",
        "PAYMENT_CANCELLED",
        "PAYMENT_EXPIRED",
        "PAYMENT_FAILED",
        "PAYMENT_ERRORED"
    };

    public PaysafeEventHandler(
        ILogger<IntegrationEventHandler> logger,
        IIntegrationDataService dataService,
        IDelayedTaskScheduler taskScheduler)
        : base(logger, dataService, taskScheduler)
    {
    }

    public override string Tag => "Paysafe";

    protected override async Task<IntegrationEventContext> ParseRequestAsync(HttpRequest request,
        CancellationToken cancellationToken)
    {
        var payload = await request.ReadBodyAsStringAsync();
        var signature = request.GetHeaderValue("signature");

        string? approvalNumber = null;
        string? automatedStatus = null;
        string? automatedCode = null;
        string? automatedMessage = null;
        object? automatedPayload = null;

        var generalEvent = payload.Deserialize<PaysafeEvent>();

        automatedStatus = generalEvent?.EventName ?? throw new InvalidOperationException("Failed to parse Paysafe event payload");
        switch (generalEvent.EventName)
        {
            case "PAYMENT_HANDLE_PAYABLE":
            case "PAYMENT_HANDLE_EXPIRED":
            case "PAYMENT_HANDLE_FAILED":
            case "PAYMENT_HANDLE_ERRORED":
            case "PAYMENT_HANDLE_DELETED":
            {
                var handlerEvent = payload.Deserialize<PaysafeEvent<CreatePaymentHandlerResponse>>();
                if (handlerEvent?.Payload != null)
                {
                    approvalNumber = handlerEvent.Payload.Id;
                    switch (handlerEvent.Payload.Status)
                    {
                        case "EXPIRED":
                        case "FAILED":
                        case "ERRORED":
                        case "DELETED":
                            automatedCode = "99";
                            automatedMessage = "Payment Failed, " + handlerEvent.Payload.Error?.ToMessage();
                            break;
                    }
                }
                break;
            }
            case "PAYMENT_COMPLETED":
            case "PAYMENT_CANCELLED":
            case "PAYMENT_EXPIRED":
            case "PAYMENT_FAILED":
            case "PAYMENT_ERRORED":
            {
                var paymentEvent = payload.Deserialize<PaysafeEvent<PaymentResponse>>();
                if (paymentEvent?.Payload != null)
                {
                    approvalNumber = paymentEvent.Payload.Id;
                    switch (paymentEvent.Payload.Status)
                    {
                        case "COMPLETED":
                            automatedPayload = paymentEvent.Payload;
                            break;
                        case "CANCELLED":
                        case "EXPIRED":
                        case "FAILED":
                        case "ERRORED":
                            automatedCode = "99";
                            automatedMessage = "Payment Failed, " + paymentEvent.Payload.Error?.ToMessage();
                            break;
                    }
                }
                break;
            }
        }

        return new IntegrationEventContext
        {
            UrlPath = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
            Payload = payload,
            Signature = signature,
            ApprovalNumber = approvalNumber ?? string.Empty,
            EventName = generalEvent.EventName,
            AutomatedStatus = automatedStatus,
            AutomatedCode = automatedCode,
            AutomatedMessage = automatedMessage,
            AutomatedPayload = automatedPayload
        };
    }

    protected override bool IsEventSupported(string eventName)
    {
        return SupportedEvents.Contains(eventName);
    }

    protected override bool ValidateSignature(IntegrationEventContext context, Terminal terminal)
    {
        return PaysafeServices.VerifySignature(context.Payload, SecretKey, context.Signature);
    }
}
