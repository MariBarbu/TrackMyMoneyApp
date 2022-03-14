using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Dtos.Spending;
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
            var result = _spendingService.AddSpendingAsync(request, MoneyUser);
            return Ok(result);
        }
    }
}
