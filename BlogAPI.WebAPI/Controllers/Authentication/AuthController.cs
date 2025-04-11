using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.WebAPI.Controllers.Authentication
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthenticationService authenticationService, ILogger<AuthController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var response = await _authenticationService.LoginAsync(loginRequestDto);

            if (response.Status) return Ok(response);
            return Unauthorized(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var response = await _authenticationService.RegisterAsync(registerRequestDto);

            if (response.Status) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto logoutRequestDto)
        {
            var response = await _authenticationService.LogoutAsync(logoutRequestDto.RefreshToken);

            if (response.Status) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var response = await _authenticationService.RefreshTokenAsync(refreshTokenRequestDto.RefreshToken);

            if (response.Status) return Ok(response);
            return BadRequest(response);
        }
    }
}
