namespace blog_server.Models;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenDurationInDays { get; set; } = 7;
    public int RefreshTokenDurationInDays { get; set; } = 30;
}
