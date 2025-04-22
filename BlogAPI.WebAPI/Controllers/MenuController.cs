using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Common.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.WebAPI.Controllers
{
    [Route("api/v1/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagingMenu([FromQuery] PaginationRequest paginationRequest)
        {
            var response = await _menuService.GetPagingMenu(paginationRequest);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("tree")]
        public async Task<IActionResult> GetLstMenu()
        {
            var response = await _menuService.GetLstMenu();
            var lstMenu = response.Result != null ? ConvertTreeMenu(response.Result, 0) : new List<MenuResponseDto>();
            response.Result = lstMenu;
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var response = await _menuService.GetMenuById(id);
            return response.Status ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] MenuRequestDto menuRequestDto)
        {
            var response = await _menuService.CreateMenu(menuRequestDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        private static List<MenuResponseDto> ConvertTreeMenu(List<MenuResponseDto> lstMenu, int parentId)
        {
            var result = new List<MenuResponseDto>();

            if (parentId == 0)
            {
                result = lstMenu.Where(x => x.ParentId == 0).ToList();
            }
            else
            {
                result = lstMenu.Where(x => x.ParentId == parentId).ToList();
            }

            foreach (var item in result)
            {
                var response = ConvertTreeMenu(lstMenu, item.Id);
                item.Childrens = response;
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuRequestDto menuRequestDto)
        {
            var response = await _menuService.UpdateMenu(id, menuRequestDto);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var response = await _menuService.DeleteMenu(id);
            return response.Status ? Ok(response) : NotFound(response);
        }
    }
}
