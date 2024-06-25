using BlogAPI.Helpers;
using BlogAPI.Models;
using BlogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor _author;

        public AuthorController(IAuthor author)
        {
            _author = author;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _author.GetAllAuthors();
                return Ok(authors);
            }
            catch (AppException ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            try
            {
                var author = await _author.GetAuthor(id);
                return Ok(author);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorModel model)
        {
            try
            {
                var authorId = await _author.CreateAuthor(model);
                var author = await _author.GetAuthor(authorId);
                return Ok(author);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorModel model)
        {
            try
            {
                await _author.UpdateAuthor(id, model);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _author.DeleteAuthor(id);
                return Ok();
            }
            catch (AppException ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
