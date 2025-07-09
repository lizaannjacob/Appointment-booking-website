namespace ApptManager.Models
{
    public class TaxProfessional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string ContactNumber => Phone;

        public bool IncomeTaxFilingSpecialist { get; set; }
        public bool CorporateTaxConsultant { get; set; }
        public bool InvestmentTaxPlanningAdvisor { get; set; }
    }

}
