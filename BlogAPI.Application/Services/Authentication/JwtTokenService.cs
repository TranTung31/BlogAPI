using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Enums.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BlogAPI.Application.Services.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenService(IConfiguration configuration, IAuthenticationRepository authenticationRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _authenticationRepository = authenticationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JwtResponseDto?> GenerateTokenAsync(AspNetUser user, int? type, string? refreshTokenString)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"] ?? "");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("uid", user.Id),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["JwtSettings:TokenLifetime"] ?? "")),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = Guid.NewGuid().ToString();

            var ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString();

            // Trường hợp có proxy (như Nginx, Cloudflare)
            if (httpContext != null && httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                ipAddress = forwardedFor.FirstOrDefault()?.Split(':')[0];
            }

            var payload = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IpAddress = ipAddress ?? "",
                DeviceInfo = httpContext?.Request.Headers["User-Agent"].ToString()
            };

            // Lưu RefreshToken vào DB
            await _authenticationRepository.CreateRefreshTokenAsync(payload);

            // Revoke refresh token
            if (type != null && !string.IsNullOrEmpty(refreshTokenString) && type == (int)RevokeTokenType.Revoke)
            {
                var responses = await _authenticationRepository.FindAndRevokeRefreshTokenAsync(refreshTokenString);

                if (!responses)
                {
                    return null;
                }
            }

            var result = new JwtResponseDto()
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
            };

            return result;
        }
    }
}
