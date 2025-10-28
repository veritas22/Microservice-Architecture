using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<WarehouseController> _logger;

        public HealthController(ILogger<WarehouseController> logger)
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
