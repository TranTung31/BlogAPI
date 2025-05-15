using BlogAPI.Core.Common.Pagination;
using BlogAPI.Core.Entities;

namespace BlogAPI.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<PaginationResponse<AspNetRole>> GetPagingMenuAsync(PaginationRequest paginationRequest);
        Task CreateRoleAsync(AspNetRole role);
        Task UpdateRoleAsync(AspNetRole role);
        Task DeleteRoleAsync(AspNetRole role);
        Task<AspNetRole?> GetRoleByIdAsync(int roleId);
    }
}
