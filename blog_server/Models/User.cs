using System.ComponentModel.DataAnnotations.Schema;
using blog_server.Constants;

namespace blog_server.Models;

[Table("users")]
public class User : BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    [Column("refresh_token")]
    public string? RefreshToken { get; set; }

    [Column("refresh_token_expiry_time")]
    public DateTime? RefreshTokenExpiryTime { get; set; }

    [Column("status")]
    public AppStatus Status { get; set; } = AppStatus.Active;

    [Column("is_email_verified")]
    public bool IsEmailVerified { get; set; } = false;
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
