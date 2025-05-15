using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Core.Common.Pagination;
using BlogAPI.Core.Entities;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRoleAsync(AspNetRole role)
        {
            await _dbContext.AspNetRoles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(AspNetRole role)
        {
            _dbContext.AspNetRoles.Remove(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginationResponse<AspNetRole>> GetPagingMenuAsync(PaginationRequest paginationRequest)
        {
            var query = _dbContext.AspNetRoles.AsQueryable();

            if (!string.IsNullOrEmpty(paginationRequest.FilterText))
            {
                query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(paginationRequest.FilterText.ToLower()));
            }

            var total = await query.CountAsync();

            var lstRole = await query
                .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginationResponse<AspNetRole>(lstRole, paginationRequest.Page, paginationRequest.PageSize, total);
        }

        public async Task<AspNetRole?> GetRoleByIdAsync(int roleId)
        {
            return await _dbContext.AspNetRoles.FirstOrDefaultAsync(x => x.Id == roleId);
        }

        public async Task UpdateRoleAsync(AspNetRole role)
        {
            _dbContext.AspNetRoles.Update(role);
            await _dbContext.SaveChangesAsync();
        }
    }
}
