using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.User;

public class UpdateUserRequest
{
    [Required]
    public Guid Id { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string? Username { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }

    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
