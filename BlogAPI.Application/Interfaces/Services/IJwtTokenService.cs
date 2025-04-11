using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        // Type = 1 là Revoke, Type = 2 là InRevoke
        Task<JwtResponseDto?> GenerateTokenAsync(AspNetUser user, int? type, string? refreshTokenString);
    }
}
