using System;
using System.Collections.Generic;

namespace ApptManager.Models
{
    public class TaxProfessionalWithSlots
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IncomeTaxFilingSpecialist { get; set; }
        public bool CorporateTaxConsultant { get; set; }
        public bool InvestmentTaxPlanningAdvisor { get; set; }

        // ✅ Needed for UserRepository & UserService
        public List<AvailabilitySlot> Slots { get; set; } = new List<AvailabilitySlot>();

        // ✅ Needed for AdminService
        public List<string> AvailableSlotTimes { get; set; } = new List<string>();
        public List<string> BookedSlotTimes { get; set; } = new List<string>();
    }
}
