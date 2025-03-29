using System.Net.Mail;
using System.Text;
using blog_server.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace blog_server.Services.Impl;

public class EmailServiceImpl : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailServiceImpl(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();

        // Set From address with proper name and email
        message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));

        // Set Reply-To header
        message.ReplyTo.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));

        // Set To address
        message.To.Add(new MailboxAddress("", to));

        // Set subject
        message.Subject = subject;

        // Add important headers to avoid spam
        message.Headers.Add("List-Unsubscribe", $"<mailto:{_emailSettings.FromEmail}>");
        message.Headers.Add("Precedence", "bulk");
        message.Headers.Add("X-Auto-Response-Suppress", "OOF, AutoReply");

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody =
                $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>{subject}</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #f8f9fa; padding: 20px; text-align: center; }}
                        .content {{ padding: 20px; }}
                        .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
                        .button {{ display: inline-block; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1 style='color: #007bff; margin: 0;'>{_emailSettings.FromName}</h1>
                        </div>
                        <div class='content'>
                            {body}
                        </div>
                        <div class='footer'>
                            <p>Email này được gửi tự động, vui lòng không trả lời.</p>
                            <p>© {DateTime.Now.Year} {_emailSettings.FromName}. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>",
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync(
            _emailSettings.SmtpServer,
            _emailSettings.SmtpPort,
            SecureSocketOptions.StartTls
        );
        await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task SendWelcomeEmailAsync(string to, string username)
    {
        var subject = "Chào mừng bạn đến với OurBlog!";
        var body =
            $@"
            <h2>Xin chào {username}!</h2>
            <p>Cảm ơn bạn đã đăng ký tài khoản tại OurBlog. Chúng tôi rất vui mừng được chào đón bạn!</p>
            <p>Với tài khoản của bạn, bạn có thể:</p>
            <ul>
                <li>Đọc các bài viết thú vị</li>
                <li>Viết và chia sẻ bài viết của riêng bạn</li>
                <li>Tương tác với cộng đồng</li>
            </ul>
            <p>Nếu bạn có bất kỳ câu hỏi nào, đừng ngần ngại liên hệ với chúng tôi.</p>
            <p>Trân trọng,<br>Đội ngũ OurBlog</p>
        ";

        await SendEmailAsync(to, subject, body);
    }

    public async Task SendWelcomeEmailWithPasswordAsync(string to, string username, string password)
    {
        var subject = "Chào mừng bạn đến với OurBlog!";
        var body =
            $@"
            <h2>Xin chào {username}!</h2>
            <p>Cảm ơn bạn đã đăng ký tài khoản tại OurBlog. Chúng tôi rất vui mừng được chào đón bạn!</p>
            <p>Với tài khoản của bạn, bạn có thể:</p>
            <ul>
                <li>Đọc các bài viết thú vị</li>
                <li>Viết và chia sẻ bài viết của riêng bạn</li>
                <li>Tương tác với cộng đồng</li>
            </ul>
            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                <p style='margin: 0;'><strong>Thông tin đăng nhập của bạn:</strong></p>
                <p style='margin: 10px 0;'><strong>Email:</strong> {to}</p>
                <p style='margin: 10px 0;'><strong>Mật khẩu:</strong> {password}</p>
            </div>
            <p style='color: #dc3545;'><strong>Vui lòng đổi mật khẩu sau khi đăng nhập để bảo mật tài khoản của bạn.</strong></p>
            <p>Nếu bạn có bất kỳ câu hỏi nào, đừng ngần ngại liên hệ với chúng tôi.</p>
            <p>Trân trọng,<br>Đội ngũ OurBlog</p>
        ";

        await SendEmailAsync(to, subject, body);
    }

    public async Task SendPasswordResetEmailAsync(string to, string resetToken)
    {
        var subject = "Đặt lại mật khẩu - OurBlog";
        var resetLink = $"{_emailSettings.FromEmail}/reset-password?token={resetToken}";
        var body =
            $@"
            <h2>Đặt lại mật khẩu</h2>
            <p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình.</p>
            <p>Vui lòng nhấp vào nút bên dưới để đặt lại mật khẩu:</p>
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{resetLink}' class='button'>Đặt lại mật khẩu</a>
            </p>
            <p style='color: #dc3545;'><strong>Liên kết này sẽ hết hạn sau 1 giờ.</strong></p>
            <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
            <p>Trân trọng,<br>Đội ngũ OurBlog</p>
        ";

        await SendEmailAsync(to, subject, body);
    }
}
