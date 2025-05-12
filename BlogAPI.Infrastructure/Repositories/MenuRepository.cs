using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Core.Common.Pagination;
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
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _dbContext;

        public MenuRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateMenuAsync(Menu menu)
        {
            await _dbContext.Menus.AddAsync(menu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(Menu menu)
        {
            _dbContext.Menus.Remove(menu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Menu>> GetLstMenuAsync()
        {
            return await _dbContext.Menus.Include(x => x.Permissions).OrderBy(x => x.SortOrder).ToListAsync();
        }

        public async Task<Menu?> GetMenuByIdAsync(int menuId)
        {
            return await _dbContext.Menus.FirstOrDefaultAsync(x => x.Id == menuId);
        }

        public async Task<PaginationResponse<Menu>> GetPagingMenuAsync(PaginationRequest paginationRequest)
        {
            var query = _dbContext.Menus.AsQueryable();

            if (!string.IsNullOrEmpty(paginationRequest.FilterText))
            {
                query = query.Where(x => x.Name.ToLower().Contains(paginationRequest.FilterText.ToLower()));
            }

            var total = await query.CountAsync();

            var lstMenu = await query
                .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginationResponse<Menu>(lstMenu, paginationRequest.Page, paginationRequest.PageSize, total);
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _dbContext.Menus.Update(menu);
            await _dbContext.SaveChangesAsync();
        }
    }
}
