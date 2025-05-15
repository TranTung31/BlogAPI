using BlogAPI.Application.DTOs.Role;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.WebAPI.Controllers
{
    [Route("api/v1/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagingRole([FromQuery] PaginationRequest paginationRequest)
        {
            var response = await _roleService.GetPagingRole(paginationRequest);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleService.GetRoleById(id);
            return response.Status ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequestDto roleRequestDto)
        {
            var response = await _roleService.CreateRole(roleRequestDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleRequestDto roleRequestDto)
        {
            var response = await _roleService.UpdateRole(id, roleRequestDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRole(id);
            return response.Status ? Ok(response) : NotFound(response);
        }
    }
}
