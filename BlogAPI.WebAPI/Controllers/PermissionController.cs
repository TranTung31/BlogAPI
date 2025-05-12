using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Application.DTOs.Permission;
using BlogAPI.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.WebAPI.Controllers
{
    [Route("api/v1/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionRequestDto permissionRequestDto)
        {
            var response = await _permissionService.CreatePermission(permissionRequestDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var response = await _permissionService.DeletePermission(id);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
