using blog_server.DTOs.Auth;
using blog_server.Models;

namespace blog_server.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request, DeviceInfo deviceInfo);
    public Task<LoginResponse> GoogleLogin(GoogleLoginRequest request, DeviceInfo deviceInfo);
    public Task<RegisterResponse> Register(RegisterRequest request);
    public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
    public Task RevokeToken();
    public Task<MyInfoResponse> MyInfo();
    public Task UpdateInfo(UpdateInfoRequest request);
    public Task ForgotPassword(ForgotPasswordRequest request);
    public Task VerifyResetToken(VerifyResetTokenRequest request);
    public Task VerifyEmail(VerifyEmailRequest request);
    public Task ResetPassword(ResetPasswordRequest request);
}
