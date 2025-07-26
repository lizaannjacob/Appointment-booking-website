using System.ComponentModel.DataAnnotations;

namespace ApptManager.Models
{
    public class TaxProfessional
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

        public string ContactNumber => Phone;

        public bool IncomeTaxFilingSpecialist { get; set; }
        public bool CorporateTaxConsultant { get; set; }
        public bool InvestmentTaxPlanningAdvisor { get; set; }
    }
}
