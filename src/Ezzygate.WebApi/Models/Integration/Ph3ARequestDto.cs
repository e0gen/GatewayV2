using Ezzygate.Integrations.Ph3a.Api;

namespace Ezzygate.WebApi.Models.Integration;

public class Ph3ARequestDto : Ph3ARequest
{
    public int MerchantId { get; set; }
}