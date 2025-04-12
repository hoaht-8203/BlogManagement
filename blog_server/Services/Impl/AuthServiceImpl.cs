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
    IRedisCacheService redisCache,
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
    private readonly IRedisCacheService _redisCache = redisCache;
    private readonly ILogger<AuthServiceImpl> _logger = logger;

    private const string USER_CACHE_KEY_PREFIX = "user:";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(30);

    public async Task<LoginResponse> Login(LoginRequest request, DeviceInfo deviceInfo)
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

        if (!user.IsEmailVerified)
        {
            throw new ApiException(
                "Email not verified",
                StatusCodes.Status400BadRequest,
                ["email:Email chưa được xác thực"]
            );
        }

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var userToken = new UserToken
        {
            Email = user.Email,
            UserId = user.Id,
            Token = refreshToken,
            TokenType = TokenTypes.RefreshToken,
            ExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays),
            DeviceType = deviceInfo.DeviceType,
            OS = deviceInfo.OS,
            Browser = deviceInfo.Browser,
        };

        _context.UserTokens.Add(userToken);
        await _context.SaveChangesAsync();

        var listRole = user.UserRoles.Select(ur => ur.Role.Name.ToString()).ToList();

        await _redisCache.RemoveAsync($"{USER_CACHE_KEY_PREFIX}{_currentUser.UserId}");

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
        var cacheKey = $"{USER_CACHE_KEY_PREFIX}{_currentUser.UserId}";

        var cachedUser = await _redisCache.GetAsync<UserCacheDto>(cacheKey);
        if (cachedUser != null)
        {
            return _mapper.Map<MyInfoResponse>(cachedUser);
        }

        var user =
            await _context.Users.SingleOrDefaultAsync(u =>
                u.Id == _currentUser.UserId && u.Status == AppStatus.Active
            ) ?? throw new ApiException("User not found", StatusCodes.Status401Unauthorized);

        var response = _mapper.Map<MyInfoResponse>(user);

        var userCacheDto = _mapper.Map<UserCacheDto>(user);
        await _redisCache.SetAsync(cacheKey, userCacheDto, CACHE_DURATION);

        return response;
    }

    public async Task UpdateInfo(UpdateInfoRequest request)
    {
        var user =
            await _context.Users.SingleOrDefaultAsync(u =>
                u.Id == _currentUser.UserId && u.Status == AppStatus.Active
            ) ?? throw new ApiException("User not found", StatusCodes.Status401Unauthorized);

        await _redisCache.RemoveAsync($"{USER_CACHE_KEY_PREFIX}{_currentUser.UserId}");

        _mapper.Map(request, user);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        var userId =
            (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            ?? throw new ApiException("Invalid token", StatusCodes.Status401Unauthorized);

        var userToken =
            await _context
                .UserTokens.Include(ut => ut.User)
                .ThenInclude(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .OrderByDescending(ut => ut.CreatedAt)
                .FirstOrDefaultAsync(ut =>
                    ut.UserId.ToString() == userId
                    && ut.Token == request.RefreshToken
                    && ut.TokenType == TokenTypes.RefreshToken
                )
            ?? throw new ApiException(
                "Invalid or expired refresh token",
                StatusCodes.Status401Unauthorized
            );

        var newAccessToken = GenerateAccessToken(userToken.User);
        var newRefreshToken = GenerateRefreshToken();

        userToken.Token = newRefreshToken;
        userToken.ExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays);

        _context.UserTokens.Update(userToken);
        await _context.SaveChangesAsync();

        var userCacheDto = _mapper.Map<UserCacheDto>(userToken.User);
        await _redisCache.SetAsync(
            $"{USER_CACHE_KEY_PREFIX}{userToken.UserId}",
            userCacheDto,
            CACHE_DURATION
        );

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

        var random = new Random();
        var token = random.Next(100000, 999999).ToString();
        var emailVerificationToken = new UserToken
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token,
            TokenType = TokenTypes.EmailVerification,
            ExpiryTime = DateTime.UtcNow.AddDays(1),
        };

        _context.UserTokens.Add(emailVerificationToken);
        await _context.SaveChangesAsync();

        _emailService.SendEmailFireAndForget(
            () => _emailService.SendEmailVerificationEmailAsync(user.Email, token),
            _logger,
            user.Email
        );

        return _mapper.Map<RegisterResponse>(user);
    }

    public async Task RevokeToken()
    {
        var userToken =
            await _context.UserTokens.FirstOrDefaultAsync(ut =>
                ut.UserId == _currentUser.UserId
                && ut.TokenType == TokenTypes.RefreshToken
                && ut.ExpiryTime > DateTime.UtcNow
            ) ?? throw new ApiException("User not found", StatusCodes.Status404NotFound);

        _context.UserTokens.Remove(userToken);
        await _context.SaveChangesAsync();

        await _redisCache.RemoveAsync($"{USER_CACHE_KEY_PREFIX}{userToken.UserId}");
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
        var expires = DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenDurationInDays);

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

    public async Task<LoginResponse> GoogleLogin(GoogleLoginRequest request, DeviceInfo deviceInfo)
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
                IsEmailVerified = true,
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

        var userToken = new UserToken
        {
            UserId = user.Id,
            Token = refreshToken,
            TokenType = TokenTypes.RefreshToken,
            ExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays),
            DeviceType = deviceInfo.DeviceType,
            OS = deviceInfo.OS,
            Browser = deviceInfo.Browser,
        };

        _context.UserTokens.Add(userToken);

        await _context.SaveChangesAsync();

        var listRole = user.UserRoles.Select(ur => ur.Role.Name.ToString()).ToList();

        var userCacheDto = _mapper.Map<UserCacheDto>(user);
        await _redisCache.SetAsync(
            $"{USER_CACHE_KEY_PREFIX}{user.Id}",
            userCacheDto,
            CACHE_DURATION
        );

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

        var existingPasswordResetTokens = await _context
            .UserTokens.Where(pr =>
                pr.Email == request.Email && pr.TokenType == TokenTypes.PasswordReset
            )
            .ToListAsync();

        if (existingPasswordResetTokens.Count != 0)
        {
            _context.UserTokens.RemoveRange(existingPasswordResetTokens);
        }

        // Generate 6-digit token
        var random = new Random();
        var token = random.Next(100000, 999999).ToString();

        // Create new password reset record
        var newToken = new UserToken
        {
            Email = request.Email,
            Token = token,
            TokenType = TokenTypes.PasswordReset,
            ExpiryTime = DateTime.UtcNow.AddMinutes(15),
        };

        _context.UserTokens.Add(newToken);
        await _context.SaveChangesAsync();

        _emailService.SendEmailFireAndForget(
            () => _emailService.SendPasswordResetEmailAsync(request.Email, token),
            _logger,
            request.Email
        );
    }

    public async Task VerifyResetToken(VerifyResetTokenRequest request)
    {
        var passwordResetToken =
            await _context.UserTokens.FirstOrDefaultAsync(pr =>
                pr.Email == request.Email
                && pr.Token == request.Token
                && pr.TokenType == TokenTypes.PasswordReset
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
        _context.UserTokens.Remove(passwordResetToken);

        await _context.SaveChangesAsync();

        _emailService.SendEmailFireAndForget(
            () => _emailService.SendNewPasswordEmailAsync(user.Email, user.Username, newPassword),
            _logger,
            user.Email
        );
    }

    public async Task VerifyEmail(VerifyEmailRequest request)
    {
        var existingEmailVerificationTokens = await _context
            .UserTokens.Where(pr =>
                pr.Email == request.Email && pr.TokenType == TokenTypes.EmailVerification
            )
            .ToListAsync();

        if (existingEmailVerificationTokens.Count != 0)
        {
            _context.UserTokens.RemoveRange(existingEmailVerificationTokens);
        }

        var emailVerificationToken =
            await _context.UserTokens.FirstOrDefaultAsync(pr =>
                pr.Email == request.Email
                && pr.Token == request.Token
                && pr.TokenType == TokenTypes.EmailVerification
                && pr.ExpiryTime > DateTime.UtcNow
            )
            ?? throw new ApiException(
                "Verify email failed",
                StatusCodes.Status400BadRequest,
                ["token:Mã xác thực không hợp lệ hoặc đã hết hạn"]
            );

        var user =
            await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email)
            ?? throw new ApiException(
                "Verify email failed",
                StatusCodes.Status400BadRequest,
                ["email:Email không tồn tại"]
            );

        if (user.IsEmailVerified)
        {
            throw new ApiException(
                "Email already verified",
                StatusCodes.Status400BadRequest,
                ["email:Email đã được xác thực"]
            );
        }

        user.IsEmailVerified = true;
        _context.UserTokens.Remove(emailVerificationToken);

        await _context.SaveChangesAsync();
    }

    public async Task ResetPassword(ResetPasswordRequest request)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(u =>
                u.Id == _currentUser.UserId && u.Status == AppStatus.Active
            ) ?? throw new ApiException("Reset password failed", StatusCodes.Status401Unauthorized);

        if (!PasswordHelper.VerifyPassword(request.OldPassword, user.PasswordHash))
        {
            throw new ApiException(
                "Reset password failed",
                StatusCodes.Status400BadRequest,
                ["old_password:Mật khẩu cũ không chính xác"]
            );
        }

        user.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();
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
