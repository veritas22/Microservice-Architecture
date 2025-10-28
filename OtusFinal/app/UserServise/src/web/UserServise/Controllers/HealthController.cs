using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace UserServise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Health")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
