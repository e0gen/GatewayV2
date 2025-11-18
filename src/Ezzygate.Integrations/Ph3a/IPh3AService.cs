using System.Threading.Tasks;
using Ezzygate.Integrations.Ph3a.Api;

namespace Ezzygate.Integrations.Ph3a;

public interface IPh3AService
{
    Task<bool> ValidateScore(Ph3ARequest request, int merchantId);
}