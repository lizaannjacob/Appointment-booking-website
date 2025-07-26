using System.ComponentModel.DataAnnotations;

namespace ApptManager.Dtos
{
    public record class TaxProfessionalDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public bool IncomeTaxFilingSpecialist { get; set; }
        public bool CorporateTaxConsultant { get; set; }
        public bool InvestmentTaxPlanningAdvisor { get; set; }
    }
}
