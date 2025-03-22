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

    [Column("refresh_token")]
    public string? RefreshToken { get; set; }

    [Column("refresh_token_expiry_time")]
    public DateTime? RefreshTokenExpiryTime { get; set; }

    [Column("status")]
    public AppStatus Status { get; set; } = AppStatus.Active;

    public ICollection<UserRole> UserRoles { get; set; } = [];
}
