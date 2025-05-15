using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Application.DTOs.Role;
using BlogAPI.Core.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<bool>> CreateRole(RoleRequestDto roleRequestDto);
        Task<BaseResponse<bool>> UpdateRole(int roleId, RoleRequestDto roleRequestDto);
        Task<BaseResponse<bool>> DeleteRole(int roleId);
        Task<BaseResponse<PaginationResponse<RoleResponseDto>>> GetPagingRole(PaginationRequest paginationRequest);
        Task<BaseResponse<RoleResponseDto>> GetRoleById(int roleId);
    }
}
