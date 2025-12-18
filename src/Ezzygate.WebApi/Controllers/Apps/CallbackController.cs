using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Integrations.Paysafe;
using Ezzygate.Integrations.QrMoney;
using Ezzygate.Integrations.Rapyd;

namespace Ezzygate.WebApi.Controllers.Apps;

[ApiController]
[Route("api/apps/[controller]")]
[ApiVersionNeutral]
public class CallbackController : ControllerBase
{
    private readonly ILogger<CallbackController> _logger;
    private readonly RapydEventHandler _rapydEventHandler;
    private readonly PaysafeEventHandler _paysafeEventHandler;
    private readonly QrMoneyEventHandler _qrMoneyEventHandler;

    public CallbackController(
        ILogger<CallbackController> logger,
        RapydEventHandler rapydEventHandler,
        PaysafeEventHandler paysafeEventHandler,
        QrMoneyEventHandler qrMoneyEventHandler)
    {
        _logger = logger;
        _rapydEventHandler = rapydEventHandler;
        _paysafeEventHandler = paysafeEventHandler;
        _qrMoneyEventHandler = qrMoneyEventHandler;
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

    [HttpPost("paysafe")]
    public async Task<IActionResult> Paysafe(CancellationToken cancellationToken)
    {
        try
        {
            await _paysafeEventHandler.HandleAsync(Request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Integration, ex, "Error handling {Integration} event: {Message}",
                _paysafeEventHandler.Tag, ex.Message);

            return Ok();
        }
    }

    [HttpPost("qr-money")]
    public async Task<IActionResult> QrMoney(CancellationToken cancellationToken)
    {
        try
        {
            await _qrMoneyEventHandler.HandleAsync(Request, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Integration, ex, "Error handling {Integration} event: {Message}",
                _qrMoneyEventHandler.Tag, ex.Message);

            return Ok();
        }
    }
}