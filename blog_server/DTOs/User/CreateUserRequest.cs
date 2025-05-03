using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.User;

public class CreateUserRequest
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
