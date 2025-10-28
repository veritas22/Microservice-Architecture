using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Notification.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {

        private readonly ILogger<MailController> _logger;
        private readonly IEFStore _eFStore;
        public MailController(ILogger<MailController> logger, IEFStore eFStore)
        {
            _logger = logger;
            _eFStore = eFStore;
        }
        [Authorize]
        [HttpGet("GetMail")]
        public async  Task<IActionResult> GetMail()
        {
            var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;
            if (userIdClaims == null)
            {
                return Unauthorized("Данного пользователя нет доступа");
            }
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var result = await _eFStore.GetMail(int.Parse(userIdClaims), cancel);
            return Ok(result);
        }
    }
}
