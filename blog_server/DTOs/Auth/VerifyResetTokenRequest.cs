namespace blog_server.DTOs.Auth;

public class VerifyResetTokenRequest
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
