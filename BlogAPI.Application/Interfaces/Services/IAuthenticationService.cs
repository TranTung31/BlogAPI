using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<BaseResponse<bool>> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<BaseResponse<bool>> LogoutAsync(string refreshToken);
        Task<BaseResponse<RefreshTokenResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}
