using System;

namespace blog_server.DTOs.Auth;

public class RegisterResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
