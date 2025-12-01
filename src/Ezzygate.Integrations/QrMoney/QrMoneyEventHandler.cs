using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Integrations.Core.Events;
using Ezzygate.Integrations.QrMoney.Api;

namespace Ezzygate.Integrations.QrMoney;

public sealed class QrMoneyEventHandler : IntegrationEventHandler
{
    private static readonly HashSet<string> SupportedEvents = new(StringComparer.OrdinalIgnoreCase)
    {
        "APPROVED",
        "DECLINED"
    };

    public QrMoneyEventHandler(
        ILogger<IntegrationEventHandler> logger,
        IIntegrationDataService dataService,
        IDelayedTaskScheduler taskScheduler)
        : base(logger, dataService, taskScheduler)
    {
    }

    public override string Tag => "QrMoney";

    protected override async Task<IntegrationEventContext> ParseRequestAsync(HttpRequest request,
        CancellationToken cancellationToken)
    {
        var signature = request.GetHeaderValue("X-Signature");
        var payload = request.QueryString.ToString();

        var form = await request.ReadFormAsync(cancellationToken);
        var qrMoneyEvent = new QrMoneyEvent(form);

        var approvalNumber = qrMoneyEvent.TransactionId ?? string.Empty;
        var eventName = qrMoneyEvent.StatusId switch
        {
            "1" => "APPROVED",
            "2" => "DECLINED",
            _ => "UNKNOWN"
        };

        return new IntegrationEventContext
        {
            UrlPath = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
            Payload = payload,
            Signature = signature,
            ApprovalNumber = approvalNumber,
            EventName = eventName
        };
    }

    protected override bool IsEventSupported(string eventName)
    {
        return SupportedEvents.Contains(eventName);
    }

    protected override bool ValidateSignature(IntegrationEventContext context, Terminal terminal)
    {
        var secretKey = terminal.AuthenticationCode1;
        if (string.IsNullOrEmpty(secretKey))
            return false;

        return QrMoneyServices.ValidateSignature(
            context.Payload,
            secretKey,
            context.Signature ?? string.Empty);
    }
}