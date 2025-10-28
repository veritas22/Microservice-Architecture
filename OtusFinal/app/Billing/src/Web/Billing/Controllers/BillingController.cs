using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeuroStore.Repository;

namespace Billing.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {

        private readonly ILogger<BillingController> _logger;
        private readonly IBillingLogic _billingLogic;
        private readonly IEFStore _eFStore;

        public BillingController(ILogger<BillingController> logger, IBillingLogic billingLogic, IEFStore eFStore)
        {
            _logger = logger;
            _billingLogic = billingLogic;
            _eFStore = eFStore;
        }
        [HttpPost("SetDeposit")]
        public async Task<IActionResult> SetDeposit(float amount)
        {
            var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;
            if (userIdClaims == null)
            {
                return Unauthorized("Данного пользователя нет доступа");

            }
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var result = await _billingLogic.DepositMoney(int.Parse(userIdClaims), amount, cancel);
            return Ok(result);
        }
        [HttpGet("GetAmount")]
        public async Task<IActionResult> GetAmount()
        {
            var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;
            if (userIdClaims == null)
            {
                return Unauthorized("Данного пользователя нет доступа");

            }
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;

            var result = await _eFStore.GetAccount(int.Parse(userIdClaims), cancel);
            return Ok(result);
        }

    }
}
