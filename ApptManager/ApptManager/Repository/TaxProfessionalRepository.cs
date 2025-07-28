using ApptManager.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public class TaxProfessionalRepository : ITaxProfessionalRepository
    {
        private readonly IGenericRepository<TaxProfessional> _genericRepo;
        private readonly IDbConnection _connection;

        public TaxProfessionalRepository(IDbConnection connection)
        {
            _connection = connection;
            _genericRepo = new GenericRepository<TaxProfessional>(connection);
        }

        public async Task<bool> AddTaxProfessionalAsync(TaxProfessional professional)
        {
            var sql = @"INSERT INTO tax_professional_tbl 
                        (name, email, phone, income_tax_filing_specialist, corporate_tax_consultant, investment_tax_planning_advisor)
                        VALUES (@Name, @Email, @Phone, @IncomeTaxFilingSpecialist, @CorporateTaxConsultant, @InvestmentTaxPlanningAdvisor)";
            return await _genericRepo.ExecuteAsync(sql, professional);
        }

        public async Task<IEnumerable<TaxProfessional>> GetAllProfessionalsAsync()
        {
            var sql = "SELECT * FROM tax_professional_tbl";
            return await _genericRepo.GetAllAsync(sql);
        }

        public async Task<IEnumerable<TaxProfessional>> GetAllAsync()
        {
            var sql = "SELECT * FROM tax_professional_tbl";
            return await _genericRepo.GetAllAsync(sql);
        }

        public async Task AddProfessionalAsync(TaxProfessional professional)
        {
            await AddTaxProfessionalAsync(professional);
        }

        // Slot-related methods remain unchanged

        public async Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> slots)
        {
            var sql = "INSERT INTO availability_slots_tbl (professional_id, slot_date, slot_start, slot_end, status) VALUES (@ProfessionalId, @SlotDate, @SlotStart, @SlotEnd, @Status)";
            await _connection.ExecuteAsync(sql, slots);
        }

        public async Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId)
        {
            var sql = "SELECT * FROM availability_slots_tbl WHERE professional_id = @ProfessionalId";
            return await _connection.QueryAsync<AvailabilitySlot>(sql, new { ProfessionalId = professionalId });
        }

        public async Task UpdateSlotStatusAsync(int slotId, string status)
        {
            var sql = "UPDATE availability_slots_tbl SET status = @Status WHERE slot_id = @SlotId";
            await _connection.ExecuteAsync(sql, new { SlotId = slotId, Status = status });
        }

        public async Task DeleteSlotAsync(int slotId)
        {
            var sql = "DELETE FROM availability_slots_tbl WHERE slot_id = @SlotId";
            await _connection.ExecuteAsync(sql, new { SlotId = slotId });
        }
    }
}
