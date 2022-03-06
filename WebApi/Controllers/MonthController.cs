using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;

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
    }
}
