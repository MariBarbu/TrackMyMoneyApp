using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Dtos.Wish;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public ActionResult<List<GetWishDto>> GetAll()
        {
            var result = _wishService.GetUserWishes(MoneyUser);
            return Ok(result);
        }
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<GetWishDto>>> GetAllWishes()
        {
            var result = await _wishService.GetAllWishes();
            return Ok(result);
        }
        [HttpGet]
        [Route("{wishId}")]
        public async Task<ActionResult<GetWishDto>> GetById([FromRoute] Guid wishId)
        {
            var result = await _wishService.GetWishAsync(wishId);
            return Ok(result);
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddWish([FromBody] AddWishDto request)
        {
            var result = await _wishService.AddWishAsync(request, MoneyUser);
            return Ok(result);
        }

        [HttpPost]
        [Route("check/{wishId}")]
        public async Task<ActionResult> CheckWish([FromRoute] Guid wishId)
        {
            var result = await _wishService.CheckWishAsync(wishId);
            return Ok(result);
        }

        [HttpPost]
        [Route("uncheck/{wishId}")]
        public async Task<ActionResult> UncheckWish([FromRoute] Guid wishId)
        {
            var result = await _wishService.UncheckWishAsync(wishId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{wishId}")]
        public async Task<ActionResult> DeleteWish([FromRoute] Guid wishId)
        {
            var result = await _wishService.DeleteWishAsync(wishId);
            return Ok(result);
        }

    }
}
