using System.ComponentModel.DataAnnotations;

namespace blog_server.DTOs.Auth;

public class ResetPasswordRequest
{
    [Required]
    public string OldPassword { get; set; } = string.Empty;

    [Required]
    [StringLength(
        maximumLength: 100,
        MinimumLength = 8,
        ErrorMessage = "password:Mật khẩu phải có ít nhất 8 ký tự"
    )]
    public string NewPassword { get; set; } = string.Empty;
}
