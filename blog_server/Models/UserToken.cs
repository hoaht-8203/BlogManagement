using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_server.Models;

[Table("user_tokens")]
public class UserToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("token_type")]
    public string TokenType { get; set; } = string.Empty;

    [Column("token")]
    public string Token { get; set; } = string.Empty;

    [Column("expiry_time")]
    public DateTime ExpiryTime { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("device_type")]
    public string? DeviceType { get; set; } = string.Empty;

    [Column("os")]
    public string? OS { get; set; } = string.Empty;

    [Column("browser")]
    public string? Browser { get; set; } = string.Empty;

    [Column("user_id")]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
