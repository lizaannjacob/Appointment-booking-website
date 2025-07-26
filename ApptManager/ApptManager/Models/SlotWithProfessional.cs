using System.ComponentModel.DataAnnotations;

namespace ApptManager.Models
{
    public class SlotWithProfessional
    {
        public int SlotId { get; set; }

        [Required]
        public string SlotStart { get; set; }

        [Required]
        public string SlotEnd { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string ProfessionalName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
