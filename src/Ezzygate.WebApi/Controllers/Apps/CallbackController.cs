using Microsoft.AspNetCore.Mvc;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Integrations.Rapyd;

namespace Ezzygate.WebApi.Controllers.Apps;

[ApiController]
[Route("api/apps/callback")]
public class CallbackController : ControllerBase
{
    private readonly ILogger<CallbackController> _logger;
    private readonly RapydEventHandler _rapydEventHandler;

    public CallbackController(
        ILogger<CallbackController> logger,
        RapydEventHandler rapydEventHandler)
    {
        _logger = logger;
        _rapydEventHandler = rapydEventHandler;
    }

    [HttpPost("rapyd")]
    public async Task<IActionResult> Rapyd(CancellationToken cancellationToken)
    {
        try
        {
            await _rapydEventHandler.HandleAsync(Request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Integration, ex, "Error handling {Integration} event: {Message}",
                _rapydEventHandler.Tag, ex.Message);

            return Ok();
        }
    }
}