using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Dtos.Month;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/month-service")]
    [ApiController]
    public class MonthController : WebApiController
    {
        private readonly IMonthService _monthService;

        public MonthController(IHostEnvironment hostEnvironment, IMonthService monthService) : base(hostEnvironment)
        {
            _monthService = monthService;
        }

        [HttpPost]
        [Route("update-budget")]
        public async Task<ActionResult> UpdateBudget(UpdateBudgetDto request)
        {
            var result = await _monthService.UpdateBudget(MoneyUser, request);
            return Ok(result);
        }

        [HttpPost]
        [Route("add-economy")]
        public async Task<ActionResult> AddEconomy(AddEconomyDto request)
        {
            var result = await _monthService.AddEconomy(MoneyUser, request);
            return Ok(result);
        }

        [HttpGet]
        [Route("default-screen")]
        public ActionResult<GetDefaultScreenDto> GetDefaultScreen()
        {
            var result = _monthService.GetDefaultScreen(MoneyUser);
            return result;
        }

        [HttpGet]
        [Route("history-by-year/{year}")]
        public ActionResult<HistoryDto> GetDefaultScreen([FromRoute] int year)
        {
            var result = _monthService.GetHistoryByYear(year, MoneyUser);
            return result;
        }

        [HttpGet]
        [Route("history-by-month/{year}/{month}")]
        public ActionResult<HistoryDto> GetDefaultScreen([FromRoute] int year, [FromRoute] int month)
        {
            var result = _monthService.GetHistoryByMonth(year, month, MoneyUser);
            return result;
        }

        [HttpGet]
        [Route("budget")]
        public ActionResult<UpdateBudgetDto> GetBudget()
        {
            var result = _monthService.GetBudget(MoneyUser);
            return result;
        }

    }
}
