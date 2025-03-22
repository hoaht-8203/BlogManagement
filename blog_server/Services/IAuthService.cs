using System;
using blog_server.DTOs.Auth;

namespace blog_server.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request);
    public Task<RegisterResponse> Register(RegisterRequest request);
    public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
    public Task RevokeToken(string username);
    public Task<MyInfoResponse> MyInfo();
}
