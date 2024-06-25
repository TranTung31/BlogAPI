using BlogAPI.Helpers;
using BlogAPI.Models;
using BlogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlog _blog;

        public BlogController(IBlog blog)
        {
            _blog = blog;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await _blog.GetAllBlogs();
                return Ok(blogs);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            try
            {
                var blog = await _blog.GetBlog(id);
                return Ok(blog);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogRequest model)
        {
            try
            {
                var blogId = await _blog.CreateBlog(model);
                var blog = await _blog.GetBlog(blogId);
                return Ok(blog);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, BlogRequest model)
        {
            try
            {
                await _blog.UpdateBlog(id, model);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                await _blog.DeleteBlog(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
