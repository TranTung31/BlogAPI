using AutoMapper;
using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Common.Pagination;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMapper mapper, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
        }

        public async Task<BaseResponse<bool>> CreateMenu(MenuRequestDto menuRequestDto)
        {
            try
            {
                var payload = _mapper.Map<Menu>(menuRequestDto);

                await _menuRepository.CreateMenuAsync(payload);

                return true.ToResponse("Tạo menu mới thành công!");
            }
            catch (Exception ex)
            {
                return ($"Tạo menu mới thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }

        public async Task<BaseResponse<bool>> DeleteMenu(int menuId)
        {
            try
            {
                var existingMenu = await _menuRepository.GetMenuByIdAsync(menuId);

                if (existingMenu == null) return ("Menu không tồn tại!").ToErrorResponse<bool>();

                await _menuRepository.DeleteMenuAsync(existingMenu);

                return true.ToResponse("Xóa menu thành công!");
            }
            catch (Exception ex)
            {
                return ($"Xóa menu thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }

        public async Task<BaseResponse<List<MenuResponseDto>>> GetLstMenu()
        {
            try
            {
                var lstMenu = new List<MenuResponseDto>();
                var lstMenuEntity = await _menuRepository.GetLstMenuAsync();

                foreach (var item in lstMenuEntity)
                {
                    lstMenu.Add(_mapper.Map<MenuResponseDto>(item));
                }

                return lstMenu.ToResponse("Lấy danh sách menu thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lấy danh sách menu thất bại! (Lỗi {ex.Message})").ToErrorResponse<List<MenuResponseDto>>();
            }
        }

        public async Task<BaseResponse<MenuResponseDto>> GetMenuById(int menuId)
        {
            try
            {
                var existingMenu = await _menuRepository.GetMenuByIdAsync(menuId);

                if (existingMenu == null) return ("Menu không tồn tại!").ToErrorResponse<MenuResponseDto>();

                var result = _mapper.Map<MenuResponseDto>(existingMenu);

                return result.ToResponse("Lấy menu theo id thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lấy menu thất bại! (Lỗi {ex.Message})").ToErrorResponse<MenuResponseDto>();
            }
        }

        public async Task<BaseResponse<PaginationResponse<MenuResponseDto>>> GetPagingMenu(PaginationRequest paginationRequest)
        {
            try
            {
                var lstMenu = new List<MenuResponseDto>();
                var response = await _menuRepository.GetPagingMenuAsync(paginationRequest);

                foreach (var item in response.Data)
                {
                    lstMenu.Add(_mapper.Map<MenuResponseDto>(item));
                }

                var result = new PaginationResponse<MenuResponseDto>(lstMenu, response.Page, response.PageSize, response.Total);

                return result.ToResponse("Lấy danh sách menu thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lấy danh sách menu thất bại! (Lỗi {ex.Message})").ToErrorResponse<PaginationResponse<MenuResponseDto>>();
            }
        }

        public async Task<BaseResponse<bool>> UpdateMenu(int menuId, MenuRequestDto menuRequestDto)
        {
            try
            {
                var existingMenu = await _menuRepository.GetMenuByIdAsync(menuId);

                if (existingMenu == null) return ("Menu không tồn tại!").ToErrorResponse<bool>();

                _mapper.Map(menuRequestDto, existingMenu);

                await _menuRepository.UpdateMenuAsync(existingMenu);

                return true.ToResponse("Cập nhật menu thành công!");
            }
            catch (Exception ex)
            {
                return ($"Cập nhật menu thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }
    }
}
