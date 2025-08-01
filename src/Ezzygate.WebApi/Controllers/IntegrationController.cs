using Microsoft.AspNetCore.Mvc;
using Ezzygate.WebApi.Extensions;

namespace Ezzygate.WebApi.Controllers
{
    [ApiController]
    [Route("api/apps/Integration")]
    public class IntegrationController : ControllerBase
    {
        private readonly ILogger<IntegrationController> _logger;

        public IntegrationController(ILogger<IntegrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet, HttpPost]
        public IActionResult NotificationLoopback()
        {
            var query = Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty;
            var post = Request.GetRequestContent();
            
            _logger.LogInformation($"Notification loopback - Query: {query}, Post: {post}");
            
            return Ok("NotificationLoopback OK");
        }
    }
}
