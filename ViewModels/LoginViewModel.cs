using System.ComponentModel.DataAnnotations;

namespace ASS.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email đăng nhập")]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }
    }
}
