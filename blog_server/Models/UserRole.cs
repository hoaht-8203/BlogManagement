using System.ComponentModel.DataAnnotations.Schema;

namespace blog_server.Models;

[Table("user_roles")]
public class UserRole
{
    [Column("user_id")]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("role_id")]
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    [Column("join_date")]
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
}
