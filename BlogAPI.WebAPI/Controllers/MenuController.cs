using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Common.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
            var lstMenu = response.Result != null ? ConvertTreeMenu(response.Result, null) : new List<MenuResponseDto>();
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

        private static List<MenuResponseDto> ConvertTreeMenu(List<MenuResponseDto> lstMenu, int? parentId)
        {
            var result = new List<MenuResponseDto>();

            if (parentId == null)
            {
                result = lstMenu.Where(x => x.ParentId == null).ToList();
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

        [HttpGet("select")]
        public async Task<IActionResult> GetLstMenuSelect()
        {
            var response = await _menuService.GetLstMenuSelect();
            var dicMenuSelect = response.Result != null ? ConvertMenuSelect(response.Result) : new Dictionary<int, string>();
            var result = dicMenuSelect.Select(x => new MenuSelectResponseDto
            {
                Id = x.Key,
                Name = x.Value,
            }).OrderBy(x => x.Name).ToList();
            response.Result = result;

            return response.Status ? Ok(response) : BadRequest(response);
        }

        private static Dictionary<int, string> ConvertMenuSelect(List<MenuSelectResponseDto> lstMenu)
        {
            var result = new Dictionary<int, string>();
            var parentIdMap = lstMenu.ToDictionary(item => item.Id, item => item);

            foreach (var item in lstMenu)
            {
                if (item?.ParentId == null && item != null)
                {
                    result[item.Id] = item.Name;
                }
                else if (item?.ParentId != null && item != null)
                {
                    result[item.Id] = BuildMenuName(item, parentIdMap);
                }
            }

            return result;
        }

        // Hàm xây dựng tên đầy đủ của menu từ item và Dictionary parentIdMap
        private static string BuildMenuName(MenuSelectResponseDto item, Dictionary<int, MenuSelectResponseDto> parentIdMap)
        {
            var result = new StringBuilder();
            var lstName = new List<string>();
            var currentItem = item;

            // Lặp ngược từ item đến gốc để xây dựng tên đầy đủ
            while (currentItem != null)
            {
                // Kiểm tra nếu ParentId có giá trị và tồn tại trong từ điển Dictionary
                if (currentItem.ParentId.HasValue && parentIdMap.ContainsKey(currentItem.ParentId.Value))
                {
                    lstName.Add(currentItem.Name);
                    currentItem = parentIdMap[currentItem.ParentId.Value];
                }
                else
                {
                    // Dừng điều kiện lặp khi ParentId không có giá trị
                    lstName.Add(currentItem.Name);
                    currentItem = null;
                }
            }

            // Đảo ngược danh sách để có thứ tự từ gốc đến lá
            lstName.Reverse();
            result.Append(string.Join(" > ", lstName));

            return result.ToString();
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
