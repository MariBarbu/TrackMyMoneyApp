using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Services.Dtos;
using Services;

namespace WebApi.Controllers.Auth
{
    [Route("api/auth-service")]
    [ApiController]
    public class AccountController : WebApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IHostEnvironment hostEnvironment, IAccountService accountService) : base(hostEnvironment)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("account/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var login = await _accountService.LoginAsync(loginRequest);
            return Ok(login);
        }

        [HttpPost]
        [Route("account/register")]
        public async Task<IActionResult> RegisterMoneyUser([FromBody] RegisterRequestDto registerRequest)
        {
            _ = await _accountService.RegisterUserMoneyAsync(registerRequest);
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [Route("account/register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequestDto registerRequest)
        {
            _ = await _accountService.RegisterAdmin(registerRequest);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("account/who-am-i")]
        public ActionResult<UserInformationDto> WhoAmI()
        {
            var user = _accountService.GetUserInformation(UserId);
            return user;
        }
    }
}