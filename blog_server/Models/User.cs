using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
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

    [Column("fullname")]
    public string? FullName { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    [Column("status")]
    public AppStatus Status { get; set; } = AppStatus.Active;

    [Column("is_email_verified")]
    public bool IsEmailVerified { get; set; } = false;

    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<UserToken> UserTokens { get; set; } = [];
}
