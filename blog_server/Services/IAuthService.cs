using blog_server.DTOs.Auth;

namespace blog_server.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request);
    public Task<LoginResponse> GoogleLogin(GoogleLoginRequest request);
    public Task<RegisterResponse> Register(RegisterRequest request);
    public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
    public Task RevokeToken();
    public Task<MyInfoResponse> MyInfo();
    public Task ForgotPassword(ForgotPasswordRequest request);
    public Task VerifyResetToken(VerifyResetTokenRequest request);
    public Task VerifyEmail(VerifyEmailRequest request);
}
