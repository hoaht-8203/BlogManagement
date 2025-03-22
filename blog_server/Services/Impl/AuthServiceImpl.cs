using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using blog_server.Constants;
using blog_server.Data;
using blog_server.DTOs.Auth;
using blog_server.Exceptions;
using blog_server.Helpers;
using blog_server.Models;
using blog_server.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace blog_server.Services.Impl;

public class AuthServiceImpl(
    ApplicationDbContext context,
    IMapper mapper,
    IOptions<JwtSettings> jwtSettings,
    ICurrentUser currentUser
) : IAuthService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly ICurrentUser _currentUser = currentUser;

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
                "Invalid username or password",
                StatusCodes.Status400BadRequest
            );

        if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new ApiException("Invalid username or password", StatusCodes.Status400BadRequest);
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
                u.Username == _currentUser.Username && u.Status == AppStatus.Active
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
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
        {
            throw new ApiException("Username already exists", StatusCodes.Status400BadRequest);
        }

        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new ApiException("Email already exists", StatusCodes.Status400BadRequest);
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = PasswordHelper.HashPassword(request.Password),
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var defaultRole =
            await _context.Roles.FirstOrDefaultAsync(r => r.Name == AppRole.USER)
            ?? throw new ApiException(
                "Default role not found",
                StatusCodes.Status500InternalServerError
            );

        return _mapper.Map<RegisterResponse>(user);
    }

    public async Task RevokeToken(string username)
    {
        var user =
            await _context.Users.SingleOrDefaultAsync(u =>
                u.Username == username && u.Status == AppStatus.Active
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
}
