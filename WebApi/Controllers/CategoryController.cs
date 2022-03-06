using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;

namespace WebApi.Controllers
{

    [Route("api/category-service")]
    [ApiController]
    public class CategoryController : WebApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IHostEnvironment hostEnvironment, ICategoryService categoryService) : base(hostEnvironment)
        {
            _categoryService = categoryService;
        }
    }
}
