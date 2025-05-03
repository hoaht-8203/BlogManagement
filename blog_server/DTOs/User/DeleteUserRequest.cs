using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.User;

public class DeleteUserRequest
{
    [Required]
    public Guid Id { get; set; }
}
