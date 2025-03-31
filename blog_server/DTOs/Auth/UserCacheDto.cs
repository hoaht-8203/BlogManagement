using blog_server.Constants;

namespace blog_server.DTOs.Auth;

public class UserCacheDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public AppStatus Status { get; set; }
    public List<string> Roles { get; set; } = new();
}
