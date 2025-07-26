// TaxProfessionalWithSlotsDto.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApptManager.Dtos
{
    public record class TaxProfessionalWithSlotsDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public bool IncomeTaxFilingSpecialist { get; set; }
        public bool CorporateTaxConsultant { get; set; }
        public bool InvestmentTaxPlanningAdvisor { get; set; }

        public List<AvailabilitySlotDto> Slots { get; set; } = new();
        public List<string> AvailableSlotTimes { get; set; } = new();
        public List<string> BookedSlotTimes { get; set; } = new();
    }
}
