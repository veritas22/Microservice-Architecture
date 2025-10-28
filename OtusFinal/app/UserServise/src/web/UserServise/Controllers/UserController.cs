using Entity.Dto;
using Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserServise.Models;

namespace UserServise.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;
        private readonly IEFStore _store;

        public UserController(IEFStore store , ILogger<UserController> logger)
        {
            _store = store;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public  async Task<ActionResult<User>>  Get(int userId)
        {
            var userIdClaims  = User.Claims.Where(p=>p.Type == "Id").FirstOrDefault().Value;
            if (userIdClaims != userId.ToString())
            {
                return Unauthorized("Данного пользователя нет доступа");

            }

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            var user = await _store.GetUser(userId, cancel);
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Post([FromBody]UserDto userDto)
        {
            var ttt = userDto;
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;
            await _store.AddUser(userDto, cancel);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(int userId, UserDto userDto)
        {
            try
            {
                var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault().Value;
                if (userIdClaims != userId.ToString())
                {
                    return Unauthorized("Данного пользователя нет доступа");

                }

                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                CancellationToken cancel = cancelTokenSource.Token;
                await _store.UpdateUser(userId, userDto, cancel);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int userId)
        {
            try
            {
                var userIdClaims = User.Claims.Where(p => p.Type == "Id").FirstOrDefault().Value;
                if (userIdClaims != userId.ToString())
                {
                    return Unauthorized("Данного пользователя нет доступа");

                }
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                CancellationToken cancel = cancelTokenSource.Token;
                await _store.DeleteUser(userId, cancel);
                return Ok();
            }
            catch (Exception ex) 
            {
                return Ok();
            }
        }
    }
}
