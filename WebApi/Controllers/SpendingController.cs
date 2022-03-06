using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;

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
    }
}
