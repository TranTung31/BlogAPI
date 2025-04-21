using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AspNetUser?> FindUserByEmailAsync(string email);
        Task<AspNetUser?> FindUserByNameAsync(string userName);
        Task<AspNetUser?> FindUserByIdAsync(int userId);
        Task<bool> CheckUserPasswordAsync(AspNetUser user, string password);
        Task<IList<string>> GetUserRolesAsync(AspNetUser user);
        Task<IdentityResult> CreateUserAsync(AspNetUser user, string password);
        Task CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> FindAndRevokeRefreshTokenAsync(string refreshToken);
        Task<RefreshToken?> FindRefreshTokenAsync(string refreshToken);
    }
}
