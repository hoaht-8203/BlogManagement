using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Role;

public class CreateRoleRequest
{
    [Required]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "Role name must be between 2 and 50 characters"
    )]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(
        200,
        MinimumLength = 2,
        ErrorMessage = "Description must be between 2 and 200 characters"
    )]
    public string Description { get; set; } = string.Empty;
}
