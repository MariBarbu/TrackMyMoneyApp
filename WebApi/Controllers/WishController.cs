using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;

namespace WebApi.Controllers
{
    [Route("api/wish-service")]
    [ApiController]
    public class WishController : WebApiController
    {
        private readonly IWishService _wishService;

        public WishController(IHostEnvironment hostEnvironment, IWishService wishService) : base(hostEnvironment)
        {
            _wishService = wishService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
