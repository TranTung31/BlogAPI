using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Core.Entities;
using BlogAPI.Infrastructure.Data;
using BlogAPI.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationRepository(UserManager<AspNetUser> userManager, IConfiguration configuration,
            AppDbContext dbContext, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AspNetUser?> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<AspNetUser?> FindUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CheckUserPasswordAsync(AspNetUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetUserRolesAsync(AspNetUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> CreateUserAsync(AspNetUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> FindAndRevokeRefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(refreshToken.Trim()));

            if (existingToken == null || existingToken.IsRevoked || existingToken.ExpiresAt < DateTime.UtcNow)
            {
                return false;
            }

            existingToken.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<RefreshToken?> FindRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(refreshToken.Trim()));
        }

        public async Task<AspNetUser?> FindUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
