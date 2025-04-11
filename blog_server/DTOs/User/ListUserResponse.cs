using System;
using blog_server.Constants;

namespace blog_server.DTOs.User;

public class ListUserResponse
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? AvatarUrl { get; set; }

    public AppStatus Status { get; set; }

    public bool IsEmailVerified { get; set; } = false;
    public List<string> Roles { get; set; } = [];
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
