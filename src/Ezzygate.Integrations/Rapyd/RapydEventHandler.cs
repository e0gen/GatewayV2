using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Integrations.Core.Events;
using Ezzygate.Integrations.Extensions;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd;

public sealed class RapydEventHandler : IntegrationEventHandler
{
    private static readonly HashSet<string> SupportedEvents = new(StringComparer.OrdinalIgnoreCase)
    {
        "PAYMENT_FAILED",
        "PAYMENT_COMPLETED",
        "PAYMENT_UPDATED",
        "PAYMENT_EXPIRED",
        "PAYMENT_CANCELED",
        "PAYMENT_CAPTURED"
    };

    public RapydEventHandler(
        ILogger<IntegrationEventHandler> logger,
        IIntegrationDataService dataService,
        IDelayedTaskScheduler taskScheduler)
        : base(logger, dataService, taskScheduler)
    {
    }

    public override string Tag => "Rapyd";

    protected override async Task<IntegrationEventContext> ParseRequestAsync(HttpRequest request,
        CancellationToken cancellationToken)
    {
        var payload = await request.ReadBodyAsStringAsync();

        var signature = request.GetHeaderValue("signature");
        var salt = request.GetHeaderValue("salt");
        var timestamp = request.GetHeaderValue("timestamp");

        var paymentEvent = payload.Deserialize<RapydEvent>();
        if (paymentEvent?.Data == null)
            throw new InvalidOperationException("Failed to parse Rapyd event payload");

        var approvalNumber = paymentEvent.Data.Id;
        var eventName = paymentEvent.Type;

        return new IntegrationEventContext
        {
            UrlPath = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
            Payload = payload,
            Signature = signature,
            ApprovalNumber = approvalNumber,
            EventName = eventName,
            Metadata = new Dictionary<string, string>
            {
                ["salt"] = salt ?? string.Empty,
                ["timestamp"] = timestamp ?? string.Empty
            }
        };
    }

    protected override bool IsEventSupported(string eventName)
    {
        return SupportedEvents.Contains(eventName);
    }

    protected override bool ValidateSignature(IntegrationEventContext context, Terminal terminal)
    {
        var secretKey = terminal.AuthenticationCode1.NotNull();
        var accessKey = terminal.AccountId;
        var salt = context.Metadata.GetValueOrDefault("salt", string.Empty);
        var timestamp = context.Metadata.GetValueOrDefault("timestamp", string.Empty);

        return RapydServices.ValidateSignature(
            context.UrlPath,
            salt,
            timestamp,
            context.Payload,
            accessKey,
            secretKey,
            context.Signature ?? string.Empty);
    }
}