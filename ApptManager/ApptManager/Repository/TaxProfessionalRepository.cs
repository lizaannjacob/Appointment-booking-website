using ApptManager.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public class TaxProfessionalRepository : ITaxProfessionalRepository
    {
        private readonly string _connectionString;

        public TaxProfessionalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("mydb");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task AddProfessionalAsync(TaxProfessional professional)
        {
            using var db = CreateConnection();

            string sql = @"INSERT INTO tax_professional_tbl (name, email, phone, income_tax_filing_specialist, corporate_tax_consultant, investment_tax_planning_advisor)
                         VALUES (@Name, @Email, @Phone, @IncomeTaxFilingSpecialist, @CorporateTaxConsultant, @InvestmentTaxPlanningAdvisor)";

            await db.ExecuteAsync(sql, new
            {
                professional.Name,
                professional.Email,
                professional.Phone,
                professional.IncomeTaxFilingSpecialist,
                professional.CorporateTaxConsultant,
                professional.InvestmentTaxPlanningAdvisor
            });
        }

        public async Task<bool> AddTaxProfessionalAsync(TaxProfessional professional)
        {
            var sql = @"INSERT INTO tax_professional_tbl (name, email, phone, income_tax_filing_specialist, corporate_tax_consultant, investment_tax_planning_advisor)
                        VALUES (@Name, @Email, @Phone, @IncomeTaxFilingSpecialist, @CorporateTaxConsultant, @InvestmentTaxPlanningAdvisor);";

            using var connection = CreateConnection();
            var result = await connection.ExecuteAsync(sql, professional);
            return result > 0;
        }


        public async Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> slots)
        {
            var sql = "INSERT INTO availability_slots_tbl (professional_id, slot_date, slot_start, slot_end, status) VALUES (@ProfessionalId, @SlotDate, @SlotStart, @SlotEnd, @Status)";
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, slots);
        }

        public async Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId)
        {
            var sql = "SELECT * FROM availability_slots_tbl WHERE professional_id = @ProfessionalId";
            using var connection = CreateConnection();
            return await connection.QueryAsync<AvailabilitySlot>(sql, new { ProfessionalId = professionalId });
        }

        public async Task UpdateSlotStatusAsync(int slotId, string status)
        {
            var sql = "UPDATE availability_slots_tbl SET status = @Status WHERE slot_id = @SlotId";
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, new { SlotId = slotId, Status = status });
        }

        public async Task DeleteSlotAsync(int slotId)
        {
            var sql = "DELETE FROM availability_slots_tbl WHERE slot_id = @SlotId";
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, new { SlotId = slotId });
        }

        public async Task<IEnumerable<TaxProfessional>> GetAllAsync()
        {
            var sql = "SELECT * FROM tax_professional_tbl";
            using var connection = CreateConnection();
            return await connection.QueryAsync<TaxProfessional>(sql);
        }

        public async Task<IEnumerable<TaxProfessional>> GetAllProfessionalsAsync()
        {
            var sql = "SELECT * FROM tax_professional_tbl";
            using var connection = CreateConnection();
            return await connection.QueryAsync<TaxProfessional>(sql);
        }

    }
}
