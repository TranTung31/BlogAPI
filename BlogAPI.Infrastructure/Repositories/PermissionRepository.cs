using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Core.Entities;
using BlogAPI.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _dbContext;

        public PermissionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePermissionAsync(Permission permission)
        {
            await _dbContext.Permissions.AddAsync(permission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            var param = new SqlParameter("@PermissionId", permissionId);

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC sp_Permissions_DeleteOne @PermissionId", param);
        }

        public async Task<List<Permission>> GetLstPermissionsByMenuIdAsync(int menuId)
        {
            return await _dbContext.Permissions.Where(x => x.MenuId == menuId).ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int permissionId)
        {
            return await _dbContext.Permissions.FirstOrDefaultAsync(x => x.Id == permissionId);
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            _dbContext.Permissions.Update(permission);
            await _dbContext.SaveChangesAsync();
        }
    }
}
