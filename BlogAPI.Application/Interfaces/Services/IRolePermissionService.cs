using BlogAPI.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Services
{
    public interface IRolePermissionService
    {
        Task<BaseResponse<bool>> AssignRolePermission(int roleId, List<int> permissionIds);
    }
}
