using Application.Interfaces;
using Application.Interfaces.Broker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Order.Idempotent;

namespace Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderLogic _order;
        IDistributedCache _distributedCache;
        public OrderController(ILogger<OrderController> logger, IOrderLogic order, IDistributedCache distributedCache)
        {
            _logger = logger;
            _order = order;
            _distributedCache = distributedCache;
        }
        [Idempotency]
        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(float amount)
        {
            var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;
            if (userIdClaims == null)
            {
                return Unauthorized("Данного пользователя нет доступа");
            }

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var result = await _order.PlaceOrderAsync(int.Parse(userIdClaims), amount, cancel);
            return Ok(result);
        }
    }
}
