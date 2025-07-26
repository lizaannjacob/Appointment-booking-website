using System;
using System.ComponentModel.DataAnnotations;

namespace ApptManager.Models
{
    public class AvailabilitySlot
    {
        public int SlotId { get; set; }

        [Required]
        public int ProfessionalId { get; set; }

        [Required]
        public DateTime SlotDate { get; set; }

        [Required]
        public DateTime SlotStart { get; set; }

        [Required]
        public DateTime SlotEnd { get; set; }

        [Required]
        public string Status { get; set; }

        public TaxProfessional Professional { get; set; }
    }
}
