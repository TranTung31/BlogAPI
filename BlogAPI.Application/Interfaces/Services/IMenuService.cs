using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Core.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Services
{
    public interface IMenuService
    {
        Task<BaseResponse<bool>> CreateMenu(MenuRequestDto menuRequestDto);
        Task<BaseResponse<bool>> UpdateMenu(int menuId, MenuRequestDto menuRequestDto);
        Task<BaseResponse<bool>> DeleteMenu(int menuId);
        Task<BaseResponse<List<MenuResponseDto>>> GetLstMenu();
        Task<BaseResponse<List<MenuSelectResponseDto>>> GetLstMenuSelect();
        Task<BaseResponse<PaginationResponse<MenuResponseDto>>> GetPagingMenu(PaginationRequest paginationRequest);
        Task<BaseResponse<MenuResponseDto>> GetMenuById(int menuId);
    }
}
