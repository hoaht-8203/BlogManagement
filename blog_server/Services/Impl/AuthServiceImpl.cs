using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Auth;
using blog_server.Exceptions;
using blog_server.Extensions;
using blog_server.Helpers;
using blog_server.Models;
using blog_server.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace blog_server.Services.Impl;

public class AuthServiceImpl(
    ApplicationDbContext context,
    IMapper mapper,
    IOptions<JwtSettings> jwtSettings,
    IOptions<GoogleAuthSettings> googleAuthSettings,
    ICurrentUser currentUser,
    IHttpClientFactory httpClientFactory,
    IEmailService emailService,
    ILogger<AuthServiceImpl> logger
) : IAuthService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly GoogleAuthSettings _googleAuthSettings = googleAuthSettings.Value;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IEmailService _emailService = emailService;
    private readonly ILogger<AuthServiceImpl> _logger = logger;

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user =
            await _context
                .Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u =>
                    (u.Email == request.Username || u.Username == request.Username)
                    && u.Status == AppStatus.Active
                )
            ?? throw new ApiException(
                "Authentication failed",
                StatusCodes.Status400BadRequest,
                ["username:Email hoặc tên tài khoản không tồn tại"]
            );

        if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new ApiException(
                "Authentication failed",
                StatusCodes.Status400BadRequest,
                ["password:Mật khẩu không chính xác"]
            );
        }

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
            _jwtSettings.RefreshTokenDurationInDays
        );

        await _context.SaveChangesAsync();

        var listRole = user.UserRoles.Select(ur => ur.Role.Name.ToString()).ToList();

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Username = user.Username,
            Roles = listRole,
        };
    }

    public async Task<MyInfoResponse> MyInfo()
    {
        var user =
            await _context.Users.SingleOrDefaultAsync(u =>
                u.Id == _currentUser.UserId && u.Status == AppStatus.Active
            ) ?? throw new ApiException("User not found", StatusCodes.Status404NotFound);

        return _mapper.Map<MyInfoResponse>(user);
    }

    public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        var username = principal.Identity?.Name;

        var user =
            await _context.Users.SingleOrDefaultAsync(u =>
                u.Username == username && u.Status == AppStatus.Active
            ) ?? throw new ApiException("Invalid token", StatusCodes.Status401Unauthorized);

        if (
            user.RefreshToken != request.RefreshToken
            || user.RefreshTokenExpiryTime <= DateTime.UtcNow
        )
        {
            throw new ApiException(
                "Invalid or expired refresh token",
                StatusCodes.Status401Unauthorized
            );
        }

        var newAccessToken = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
            _jwtSettings.RefreshTokenDurationInDays
        );
        await _context.SaveChangesAsync();

        return new RefreshTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
        };
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        bool userEmailExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
        if (userEmailExists)
        {
            throw new ApiException(
                "Validation failed",
                StatusCodes.Status400BadRequest,
                ["email:Email đã được sử dụng"]
            );
        }

        bool userUsernameExists = await _context.Users.AnyAsync(u =>
            u.Username == request.Username
        );
        if (userUsernameExists)
        {
            throw new ApiException(
                "Validation failed",
                StatusCodes.Status400BadRequest,
                ["username:Tên tài khoản đã được sử dụng"]
            );
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = PasswordHelper.HashPassword(request.Password),
        };

        var defaultRole =
            await _context.Roles.FirstOrDefaultAsync(r => r.Name == AppRole.USER)
            ?? throw new ApiException(
                "Default role not found",
                StatusCodes.Status500InternalServerError
            );

        UserRole newUserRole = new() { RoleId = defaultRole.Id, UserId = user.Id };

        _context.Users.Add(user);
        _context.UserRoles.Add(newUserRole);
        await _context.SaveChangesAsync();

        // Send welcome email
        await _emailService.SendWelcomeEmailAsync(user.Email, user.Username);

        return _mapper.Map<RegisterResponse>(user);
    }

    public async Task RevokeToken()
    {
        var user =
            await _context.Users.SingleOrDefaultAsync(u =>
                u.Id == _currentUser.UserId && u.Status == AppStatus.Active
            ) ?? throw new ApiException("User not found", StatusCodes.Status404NotFound);

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _context.SaveChangesAsync();
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        claims.AddRange(
            user.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name.ToString()))
        );

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenDurationInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken
        );

        if (
            securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase
            )
        )
        {
            throw new ApiException("Invalid token", StatusCodes.Status401Unauthorized);
        }

        return principal;
    }

    public async Task<LoginResponse> GoogleLogin(GoogleLoginRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient();

        // Verify the ID token with Google
        var response = await httpClient.GetAsync(
            $"https://oauth2.googleapis.com/tokeninfo?id_token={request.IdToken}"
        );

        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException("Invalid Google token", StatusCodes.Status400BadRequest);
        }

        var tokenInfo = await response.Content.ReadFromJsonAsync<GoogleTokenInfo>();
        if (tokenInfo == null || tokenInfo.Aud != _googleAuthSettings.ClientId)
        {
            throw new ApiException("Invalid Google token", StatusCodes.Status400BadRequest);
        }

        // Check if user exists
        var user = await _context
            .Users.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .SingleOrDefaultAsync(u => u.Email == tokenInfo.Email);

        if (user == null)
        {
            // Create new user if doesn't exist
            var password = PasswordHelper.GenerateRandomPassword();
            user = new User
            {
                Username = tokenInfo.Email,
                Email = tokenInfo.Email,
                PasswordHash = PasswordHelper.HashPassword(password),
            };

            var defaultRole =
                await _context.Roles.FirstOrDefaultAsync(r => r.Name == AppRole.USER)
                ?? throw new ApiException(
                    "Default role not found",
                    StatusCodes.Status500InternalServerError
                );

            UserRole newUserRole = new() { RoleId = defaultRole.Id, UserId = user.Id };

            _context.Users.Add(user);
            _context.UserRoles.Add(newUserRole);
            await _context.SaveChangesAsync();

            _emailService.SendEmailFireAndForget(
                () =>
                    _emailService.SendWelcomeEmailWithPasswordAsync(
                        user.Email,
                        user.Username,
                        password
                    ),
                _logger,
                user.Email
            );
        }

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
            _jwtSettings.RefreshTokenDurationInDays
        );

        await _context.SaveChangesAsync();

        var listRole = user.UserRoles.Select(ur => ur.Role.Name.ToString()).ToList();

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Username = user.Username,
            Roles = listRole,
        };
    }

    public async Task ForgotPassword(ForgotPasswordRequest request)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email)
            ?? throw new ApiException(
                "Send password reset email failed",
                StatusCodes.Status404NotFound,
                ["email:Email không tồn tại"]
            );

        // Delete any existing unused tokens for this email
        var existingTokens = await _context
            .PasswordResets.Where(pr => pr.Email == request.Email && !pr.IsUsed)
            .ToListAsync();

        if (existingTokens.Count != 0)
        {
            _context.PasswordResets.RemoveRange(existingTokens);
        }

        // Generate 6-digit token
        var random = new Random();
        var token = random.Next(100000, 999999).ToString();

        // Create new password reset record
        var passwordReset = new PasswordReset
        {
            Email = request.Email,
            Token = token,
            ExpiryTime = DateTime.UtcNow.AddMinutes(15),
            IsUsed = false,
        };

        _context.PasswordResets.Add(passwordReset);
        await _context.SaveChangesAsync();

        _emailService.SendEmailFireAndForget(
            () => _emailService.SendPasswordResetEmailAsync(request.Email, token),
            _logger,
            request.Email
        );
    }

    public async Task VerifyResetToken(VerifyResetTokenRequest request)
    {
        var passwordReset =
            await _context.PasswordResets.FirstOrDefaultAsync(pr =>
                pr.Email == request.Email
                && pr.Token == request.Token
                && !pr.IsUsed
                && pr.ExpiryTime > DateTime.UtcNow
            )
            ?? throw new ApiException(
                "Verify reset token failed",
                StatusCodes.Status400BadRequest,
                ["token:Mã xác thực không hợp lệ hoặc đã hết hạn"]
            );

        // Generate new password
        var newPassword = PasswordHelper.GenerateRandomPassword();

        // Update user password
        var user =
            await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email)
            ?? throw new ApiException(
                "Verify reset token failed",
                StatusCodes.Status400BadRequest,
                ["email:Email không tồn tại"]
            );
        user.PasswordHash = PasswordHelper.HashPassword(newPassword);

        // Remove the used token
        _context.PasswordResets.Remove(passwordReset);

        await _context.SaveChangesAsync();

        _emailService.SendEmailFireAndForget(
            () => _emailService.SendNewPasswordEmailAsync(user.Email, user.Username, newPassword),
            _logger,
            user.Email
        );
    }
}

public class GoogleTokenInfo
{
    public string Email { get; set; } = string.Empty;
    public string Aud { get; set; } = string.Empty;
    public string Sub { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
}
