using System.ComponentModel.DataAnnotations;

namespace ApptManager.Models
{
    public class LoginInfo
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
