namespace ApptManager.Models
{
    public class AvailabilitySlot
    {
        public int SlotId { get; set; }
        public int ProfessionalId { get; set; }
        public string ProfessionalName { get; set; } 

        public DateTime SlotDate { get; set; }
        public DateTime SlotStart { get; set; }
        public DateTime SlotEnd { get; set; }
        public string Status { get; set; }

        public string? BookedByEmail { get; set; }
    }
}
