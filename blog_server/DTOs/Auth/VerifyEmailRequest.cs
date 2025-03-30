using System;

namespace blog_server.DTOs.Auth;

public class VerifyEmailRequest
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
