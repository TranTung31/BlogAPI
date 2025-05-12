using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Application.DTOs.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<BaseResponse<bool>> CreatePermission(PermissionRequestDto permissionRequestDto);
        Task<BaseResponse<bool>> UpdatePermission(int permissionId, PermissionRequestDto permissionRequestDto);
        Task<BaseResponse<bool>> DeletePermission(int permissionId);
    }
}
