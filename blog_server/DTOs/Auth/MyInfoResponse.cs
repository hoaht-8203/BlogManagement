namespace blog_server.DTOs.Auth;

public class MyInfoResponse
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
}
