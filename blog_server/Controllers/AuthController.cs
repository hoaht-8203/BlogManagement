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

        [HttpPost("google-login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> GoogleLogin(
            GoogleLoginRequest request
        )
        {
            var response = await _authService.GoogleLogin(request);
            return Ok(
                ApiResponse<LoginResponse>.SuccessResponse(response, "Google login successful")
            );
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
        [HttpPost("update-info")]
        public async Task<ActionResult<ApiResponse<object?>>> UpdateInfo(UpdateInfoRequest request)
        {
            await _authService.UpdateInfo(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Update info successfully"));
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<ActionResult<ApiResponse<object?>>> RevokeToken()
        {
            await _authService.RevokeToken();
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Token revoked successfully"));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _authService.ForgotPassword(request);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Mã xác thực đã được gửi đến email của bạn"
                )
            );
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken(
            [FromBody] VerifyResetTokenRequest request
        )
        {
            await _authService.VerifyResetToken(request);
            return Ok(
                ApiResponse<object?>.SuccessResponse(
                    null,
                    "Mật khẩu mới đã được gửi đến email của bạn"
                )
            );
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            await _authService.VerifyEmail(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Email đã được xác thực"));
        }
    }
}
