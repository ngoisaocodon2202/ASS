using System.ComponentModel.DataAnnotations;

namespace ASS.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }

        public string? FullName { get; set; }

        public bool IsActivated { get; set; }

        public string? ActivationCode { get; set; }
    }
}
