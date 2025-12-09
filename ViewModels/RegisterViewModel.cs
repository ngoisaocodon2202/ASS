using System.ComponentModel.DataAnnotations;

namespace ASS.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [Display(Name = "Nhập lại mật khẩu")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Họ và Tên")]
        public string? FullName { get; set; }
    }
}
