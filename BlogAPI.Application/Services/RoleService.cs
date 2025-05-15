using AutoMapper;
using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Role;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Common.Pagination;
using BlogAPI.Core.Entities;

namespace BlogAPI.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse<bool>> CreateRole(RoleRequestDto roleRequestDto)
        {
            try
            {
                var payload = _mapper.Map<AspNetRole>(roleRequestDto);

                await _roleRepository.CreateRoleAsync(payload);

                return true.ToResponse("Tạo role mới thành công!");
            }
            catch (Exception ex)
            {
                return ($"Tạo role mới thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }

        public async Task<BaseResponse<bool>> DeleteRole(int roleId)
        {
            try
            {
                var existingRole = await _roleRepository.GetRoleByIdAsync(roleId);

                if (existingRole == null) return ("Role không tồn tại!").ToErrorResponse<bool>();

                await _roleRepository.DeleteRoleAsync(existingRole);

                return true.ToResponse("Xóa role thành công!");
            }
            catch (Exception ex)
            {
                return ($"Xóa role thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }

        public async Task<BaseResponse<PaginationResponse<RoleResponseDto>>> GetPagingRole(PaginationRequest paginationRequest)
        {
            try
            {
                var lstRole = new List<RoleResponseDto>();
                var response = await _roleRepository.GetPagingMenuAsync(paginationRequest);

                foreach (var item in response.Data)
                {
                    lstRole.Add(_mapper.Map<RoleResponseDto>(item));
                }

                var result = new PaginationResponse<RoleResponseDto>(lstRole, response.Page, response.PageSize, response.Total);

                return result.ToResponse("Lấy danh sách role thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lấy danh sách role thất bại! (Lỗi {ex.Message})").ToErrorResponse<PaginationResponse<RoleResponseDto>>();
            }
        }

        public async Task<BaseResponse<RoleResponseDto>> GetRoleById(int roleId)
        {
            try
            {
                var existingRole = await _roleRepository.GetRoleByIdAsync(roleId);

                if (existingRole == null) return ("Role không tồn tại!").ToErrorResponse<RoleResponseDto>();

                var result = _mapper.Map<RoleResponseDto>(existingRole);

                return result.ToResponse("Lấy role theo id thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lấy role thất bại! (Lỗi {ex.Message})").ToErrorResponse<RoleResponseDto>();
            }
        }

        public async Task<BaseResponse<bool>> UpdateRole(int roleId, RoleRequestDto roleRequestDto)
        {
            try
            {
                var existingRole = await _roleRepository.GetRoleByIdAsync(roleId);

                if (existingRole == null) return ("Role không tồn tại!").ToErrorResponse<bool>();

                _mapper.Map(roleRequestDto, existingRole);

                await _roleRepository.UpdateRoleAsync(existingRole);

                return true.ToResponse("Cập nhật role thành công!");
            }
            catch (Exception ex)
            {
                return ($"Cập nhật role thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }
    }
}
