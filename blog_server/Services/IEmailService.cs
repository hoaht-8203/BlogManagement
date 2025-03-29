namespace blog_server.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendWelcomeEmailAsync(string to, string username);
    Task SendWelcomeEmailWithPasswordAsync(string to, string username, string password);
    Task SendPasswordResetEmailAsync(string to, string resetToken);
    Task SendNewPasswordEmailAsync(string to, string username, string newPassword);
}
