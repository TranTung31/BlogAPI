using AutoMapper;
using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Entities;

namespace BlogAPI.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _authenticationRepository = authenticationRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            // Validate
            if (string.IsNullOrEmpty(loginRequestDto.Email))
            {
                return "Trường email không được để trống!".ToErrorResponse<LoginResponseDto>();
            }

            if (string.IsNullOrEmpty(loginRequestDto.Password))
            {
                return "Trường mật khẩu không được để trống!".ToErrorResponse<LoginResponseDto>();
            }

            // Find user
            var user = await _authenticationRepository.FindUserByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return "Người dùng không tồn tại!".ToErrorResponse<LoginResponseDto>();
            }

            if (!user.IsActive)
            {
                return "Tài khoản đang tạm khóa, vui lòng liên hệ quản trị viên!".ToErrorResponse<LoginResponseDto>();
            }

            // Check password
            var isCheckPass = await _authenticationRepository.CheckUserPasswordAsync(user, loginRequestDto.Password);

            if (!isCheckPass)
            {
                return "Mật khẩu không chính xác!".ToErrorResponse<LoginResponseDto>();
            }

            var token = await _jwtTokenService.GenerateTokenAsync(user, 0, "");

            var result = new LoginResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AccessToken = token?.AccessToken,
                RefreshToken = token?.RefreshToken,
            };

            return result.ToResponse("Đăng nhập thành công!");
        }

        public async Task<BaseResponse<bool>> LogoutAsync(string refreshToken)
        {
            var responses = await _authenticationRepository.FindAndRevokeRefreshTokenAsync(refreshToken);

            if (!responses)
            {
                return "Chuỗi refresh token không hợp lệ".ToErrorResponse<bool>();
            }

            return true.ToResponse("Đăng xuất thành công!");
        }

        public async Task<BaseResponse<RefreshTokenResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var existingRefreshToken = await _authenticationRepository.FindRefreshTokenAsync(refreshToken);

            if (existingRefreshToken == null)
            {
                return "Chuỗi refresh token không tồn tại!".ToErrorResponse<RefreshTokenResponseDto>();
            }

            if (existingRefreshToken.IsRevoked)
            {
                return "Chuỗi refresh token đã bị thu hồi!".ToErrorResponse<RefreshTokenResponseDto>();
            }

            if (existingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                await _authenticationRepository.FindAndRevokeRefreshTokenAsync(refreshToken);
                return "Chuỗi refresh token đã hết hạn!".ToErrorResponse<RefreshTokenResponseDto>();
            }

            var user = await _authenticationRepository.FindUserByIdAsync(existingRefreshToken.UserId);

            if (user == null)
            {
                return "Người dùng không tồn tại!".ToErrorResponse<RefreshTokenResponseDto>();
            }

            var responses = await _jwtTokenService.GenerateTokenAsync(user, 1, refreshToken);

            if (responses == null)
            {
                return "Có lỗi trong quá trình làm mới!".ToErrorResponse<RefreshTokenResponseDto>();
            }

            var result = new RefreshTokenResponseDto()
            {
                AccessToken = responses.AccessToken,
                RefreshToken = responses.RefreshToken,
            };

            return result.ToResponse("Làm mới token thành công!");
        }

        public async Task<BaseResponse<bool>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            // Find user
            var userByEmail = await _authenticationRepository.FindUserByEmailAsync(registerRequestDto.Email);

            if (userByEmail != null)
            {
                return "Email đã tồn tại!".ToErrorResponse<bool>();
            }

            var userPayload = new AspNetUser()
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email,
            };

            var result = await _authenticationRepository.CreateUserAsync(userPayload, registerRequestDto.Password);

            if (!result.Succeeded) return "Đăng ký tài khoản thất bại".ToErrorResponse<bool>();

            return true.ToResponse("Đăng ký tài khoản thành công");
        }
    }
}
