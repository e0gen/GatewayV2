using Ezzygate.Integrations.Ph3a.Api;

namespace Ezzygate.WebApi.Dtos.Apps.Integration;

public record Ph3ARequestDto : Ph3ARequest
{
    public int MerchantId { get; set; }
}