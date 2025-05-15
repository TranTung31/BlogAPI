using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<List<RolePermission>> GetLstRolePermissionAsync(int roleId);
        Task CreateManyRolePermissionAsync(List<RolePermission> lstRolePermission);
        Task DeleteManyRolePermissionAsync(List<RolePermission> lstRolePermission);
    }
}
