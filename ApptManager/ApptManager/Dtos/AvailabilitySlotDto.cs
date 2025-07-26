using System;
using System.ComponentModel.DataAnnotations;

namespace ApptManager.Dtos
{
    public record class AvailabilitySlotDto
    {
        public int SlotId { get; set; }

        [Required]
        public int ProfessionalId { get; set; }

        [Required]
        public string ProfessionalName { get; set; }

        [Required]
        public DateTime SlotStart { get; set; }

        [Required]
        public DateTime SlotEnd { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
