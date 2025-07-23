using BlogAPI.Core.Common.Pagination;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        Task<PaginationResponse<Menu>> GetPagingMenuAsync(PaginationRequest paginationRequest);
        Task<List<Menu>> GetLstMenuAsync();
        Task<List<Menu>> GetLstMenuSelectAsync();
        Task<Menu?> GetMenuByIdAsync(int menuId);
        Task CreateMenuAsync(Menu menu);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(Menu menu);
    }
}
