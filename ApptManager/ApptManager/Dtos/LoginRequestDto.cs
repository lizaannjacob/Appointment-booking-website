using System.ComponentModel.DataAnnotations;

namespace ApptManager.Dtos
{
    public record class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
