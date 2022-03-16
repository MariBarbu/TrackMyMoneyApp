using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Dtos.Category;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("")]
        public ActionResult<GetCategoriesDto> GetAll()
        {
            var result = _categoryService.GetAllCategories(MoneyUser);
            return Ok(result);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<ActionResult<GetCategoryDto>> GetById([FromRoute] Guid categoryId)
        {
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(result);
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddCategory([FromBody] AddCategoryDto request)
        {
            var result = await _categoryService.AddCategoryAsync(request, MoneyUser);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            return Ok(result);
        }
    }
}
