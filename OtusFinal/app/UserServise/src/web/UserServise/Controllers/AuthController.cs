using Application.Interfaces;
using Entity.Dto;
using Entity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServise.Models;

namespace AuthorizationServise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAccountServiceLogic _accountServiceLogic;

        public AuthController(IAccountServiceLogic accountServiceLogic)
        {
            _accountServiceLogic = accountServiceLogic;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountDto user) 
        {
            var isSave = await _accountServiceLogic.Register(user.UserName, user.Password, user.Telephone);
            if (isSave != -1)
            {
                return  Ok(isSave);
            }
            else 
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Проблема в Mongo не которые методы падают из за асинхроности слделал синхроными. нужен доп пакет
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccountDto user)
        {
            var result = await _accountServiceLogic.GetAccountPass(user.Password,user.Telephone);
            return Ok(result);

        }

        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int userid, [FromBody] AccountDto user)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;
            if (userIdClaims != userid.ToString())
            {
                return Unauthorized("Данного пользователя нет доступа");

            }

            await _accountServiceLogic.UpdateUser(userid, user, cancel);
            return Ok();

        }

        [Authorize]
        [HttpPost("GetUserIdToken")]
        public async Task<IActionResult> GetUserIdToken()
        {

            var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault()?.Value;
            return Ok($"Ваш user id токена. Примините при изменение пользователя userid: {userIdClaims}");

        }

        [Authorize]
        [HttpPost("GetAccount")]
        public async Task<IActionResult> GetAccount()
        {
            var telephone = User.Claims.Where(p => p.Type == "Telephone").FirstOrDefault()?.Value;

            var result = await _accountServiceLogic.GetAccount(telephone);
            return Ok(result);

        }
    }
}
