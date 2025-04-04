using blog_server.CustomAttributes;
using blog_server.DTOs.Auth;
using blog_server.Models;
using blog_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace blog_server.Controllers
{
    public class DeviceInfo
    {
        public string DeviceType { get; set; }
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
        public string CPUArchitecture { get; set; }
        public string Engine { get; set; }
        public string EngineVersion { get; set; }

        public override string ToString()
        {
            return $"DeviceType: {DeviceType}, OS: {OS}, OSVersion: {OSVersion}, Browser: {Browser}, BrowserVersion: {BrowserVersion}, DeviceBrand: {DeviceBrand}, DeviceModel: {DeviceModel}, CPUArchitecture: {CPUArchitecture}, Engine: {Engine}, EngineVersion: {EngineVersion}";
        }
    }

    public class UserAgentParser
    {
        public static DeviceInfo ParseUserAgent(string userAgent)
        {
            var parser = Parser.GetDefault();
            ClientInfo client = parser.Parse(userAgent);

            return new DeviceInfo
            {
                DeviceType = string.IsNullOrEmpty(client.Device.Family)
                    ? "Desktop"
                    : client.Device.Family,
                OS = client.OS.Family ?? "Unknown",
                OSVersion = $"{client.OS.Major}.{client.OS.Minor}" ?? "Unknown",
                Browser = client.UA.Family ?? "Unknown",
                BrowserVersion = $"{client.UA.Major}.{client.UA.Minor}" ?? "Unknown",
                DeviceBrand = "Unknown", // UAParser không cung cấp thông tin này
                DeviceModel = "Unknown", // UAParser không cung cấp thông tin này
                CPUArchitecture = "Unknown", // UAParser không cung cấp thông tin này
                Engine = "Unknown", // UAParser không cung cấp thông tin này
                EngineVersion = "Unknown", // UAParser không cung cấp thông tin này
            };
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger)
        : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("login")]
        [Transaction]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginRequest request)
        {
            DeviceInfo deviceInfo = UserAgentParser.ParseUserAgent(
                Request.Headers.UserAgent.ToString()
            );

            _logger.LogInformation("DeviceInfo: {DeviceInfo}", deviceInfo.ToString());

            var response = await _authService.Login(request);
            return Ok(ApiResponse<LoginResponse>.SuccessResponse(response, "Login successful"));
        }

        [HttpPost("google-login")]
        [Transaction]
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
        [Transaction]
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
        [Transaction]
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
        [HttpPut("update-info")]
        [Transaction]
        public async Task<ActionResult<ApiResponse<object?>>> UpdateInfo(UpdateInfoRequest request)
        {
            await _authService.UpdateInfo(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Update info successfully"));
        }

        [Authorize]
        [HttpPost("revoke")]
        [Transaction]
        public async Task<ActionResult<ApiResponse<object?>>> RevokeToken()
        {
            await _authService.RevokeToken();
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Token revoked successfully"));
        }

        [HttpPost("forgot-password")]
        [Transaction]
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
        [Transaction]
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
        [Transaction]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            await _authService.VerifyEmail(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Email đã được xác thực"));
        }

        [Authorize]
        [HttpPost("reset-password")]
        [Transaction]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            await _authService.ResetPassword(request);
            return Ok(ApiResponse<object?>.SuccessResponse(null, "Mật khẩu đã được đổi"));
        }
    }
}
