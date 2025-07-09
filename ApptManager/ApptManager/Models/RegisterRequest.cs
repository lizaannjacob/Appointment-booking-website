namespace ApptManager.Models
{
    public class RegisterRequest
    {
        public UserRegistrationInfo User { get; set; }
        public string Password { get; set; }
    }
}