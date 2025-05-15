using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Core.Entities;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Infrastructure.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly AppDbContext _dbContext;

        public RolePermissionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateManyRolePermissionAsync(List<RolePermission> lstRolePermission)
        {
            await _dbContext.RolePermissions.AddRangeAsync(lstRolePermission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteManyRolePermissionAsync(List<RolePermission> lstRolePermission)
        {
            _dbContext.RolePermissions.RemoveRange(lstRolePermission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<RolePermission>> GetLstRolePermissionAsync(int roleId)
        {
            return await _dbContext.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync();
        }
    }
}
