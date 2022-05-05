using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Dtos.Spending;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/spending-service")]
    [ApiController]
    public class SpendingController : WebApiController
    {
        private readonly ISpendingService _spendingService;

        public SpendingController(IHostEnvironment hostEnvironment, ISpendingService spendingService) : base(hostEnvironment)
        {
            _spendingService = spendingService;
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddSpending(AddSpendingDto request)
        {
            var result = await _spendingService.AddSpendingAsync(request, MoneyUser);
            return Ok(result);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public ActionResult<GetSpendingsDto> GetAll([FromRoute] Guid categoryId)
        {
            var result = _spendingService.GetSpendingsByCategoryAndUser(categoryId, MoneyUser);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{spendingId}")]
        public async Task<ActionResult> DeleteSpending([FromRoute] Guid spendingId)
        {
            var result = await _spendingService.DeleteSpending(spendingId);
            return Ok(result);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<AddSpendingDto>> Upload(PictureDto picture)
        {
            var result = await _spendingService.GetPictureInfo(picture.Image, MoneyUser);
            return Ok(result);
        }
    }
}
