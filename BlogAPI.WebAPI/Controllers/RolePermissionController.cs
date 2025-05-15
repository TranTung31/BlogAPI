using BlogAPI.Application.DTOs.Role;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.WebAPI.Controllers
{
    [Route("api/v1/role-permission")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> AssignRolePermission(int roleId, [FromBody] List<int> lstPermissionId)
        {
            var response = await _rolePermissionService.AssignRolePermission(roleId, lstPermissionId);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
