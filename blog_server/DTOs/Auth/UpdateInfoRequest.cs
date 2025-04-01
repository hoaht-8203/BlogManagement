using System;
using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Auth;

public class UpdateInfoRequest
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
}
