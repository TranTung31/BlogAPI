using BlogAPI.Helpers;
using BlogAPI.Models;
using BlogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _category.GetAllCategories());
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _category.GetCategory(id);
                return StatusCode(StatusCodes.Status200OK, category);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel model)
        {
            try
            {
                var categoryId = await _category.CreateCategory(model);
                var category = await _category.GetCategory(categoryId);
                return StatusCode(StatusCodes.Status200OK, category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel model)
        {
            try
            {
                await _category.UpdateCategory(id, model);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _category.DeleteCategory(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
