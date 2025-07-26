using System.ComponentModel.DataAnnotations;

public class AppointmentSlotDto
{
    [Required]
    public string SlotStart { get; set; }

    [Required]
    public string SlotEnd { get; set; }

    [Required]
    public string ProfessionalName { get; set; }

    [Required]
    public string Status { get; set; }
}
