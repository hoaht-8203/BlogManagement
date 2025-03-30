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

    [Column("is_used")]
    public bool IsUsed { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
