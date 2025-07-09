using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApptManager.Models
{
    public class UserRegistrationInfo
    {
        [JsonIgnore] // ✅ This hides it from Swagger and model binding
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public bool IsEmailVerified { get; set; }
    }
}
