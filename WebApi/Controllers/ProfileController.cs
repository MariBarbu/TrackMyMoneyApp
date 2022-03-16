using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Dtos.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/profile-service")]
    [ApiController]
    public class ProfileController : WebApiController
    {
        private readonly IAccountService _accountService;

        public ProfileController(IHostEnvironment hostEnvironment, IAccountService accountService) : base(hostEnvironment)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Login([FromBody] EditProfileDto profile)
        {
            var login = await _accountService.EditProfile(MoneyUser, profile);
            return Ok(login);
        }

        [HttpGet]
        [Route("")]
        public ActionResult<GetProfileDto> GetProfileDto()
        {
            var result =  _accountService.GetProfile(MoneyUser);
            return Ok(result);
        }

    }
}
