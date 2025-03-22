using blog_server.DTOs.Auth;
using blog_server.Models;
using blog_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger)
        : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginRequest request)
        {
            var response = await _authService.Login(request);
            return Ok(ApiResponse<LoginResponse>.SuccessResponse(response, "Login successful"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> Register(
            RegisterRequest request
        )
        {
            var response = await _authService.Register(request);
            return Ok(
                ApiResponse<RegisterResponse>.SuccessResponse(response, "Registration successful")
            );
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<RefreshTokenResponse>>> RefreshToken(
            RefreshTokenRequest request
        )
        {
            var response = await _authService.RefreshToken(request);
            return Ok(
                ApiResponse<RefreshTokenResponse>.SuccessResponse(
                    response,
                    "Token refreshed successfully"
                )
            );
        }

        [Authorize]
        [HttpGet("my-info")]
        public async Task<ActionResult<ApiResponse<MyInfoResponse>>> MyInfo()
        {
            var response = await _authService.MyInfo();
            return Ok(
                ApiResponse<MyInfoResponse>.SuccessResponse(response, "Get my info successfully")
            );
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<ActionResult<ApiResponse<object?>>> RevokeToken()
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return Unauthorized(ApiResponse<object?>.ErrorResponse("Unauthorized"));
            }

            await _authService.RevokeToken(username);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Token revoked successfully"));
        }
    }
}
