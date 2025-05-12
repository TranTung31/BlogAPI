using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task CreatePermissionAsync(Permission permission);
        Task UpdatePermissionAsync(Permission permission);
        Task DeletePermissionAsync(int permissionId);
        Task<List<Permission>> GetLstPermissionsByMenuIdAsync(int menuId);
        Task<Permission?> GetPermissionByIdAsync(int permissionId);
    }
}
