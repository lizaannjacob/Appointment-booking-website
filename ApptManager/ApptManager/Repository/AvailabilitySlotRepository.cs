using ApptManager.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public class AvailabilitySlotRepository : IAvailabilitySlotRepository
    {
        private readonly string _connectionString;

        public AvailabilitySlotRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("mydb");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task AddSlotAsync(AvailabilitySlot slot)
        {
            var query = @"INSERT INTO availability_slots_tbl 
                (professional_id, slot_date, slot_start, slot_end, status)
                VALUES (@ProfessionalId, @SlotDate, @SlotStart, @SlotEnd, @Status)";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, slot);
        }

        public async Task<bool> AddAvailabilitySlotAsync(AvailabilitySlot slot)
        {
            var sql = @"INSERT INTO availability_slots_tbl 
                        (professional_id, slot_date, slot_start, slot_end, status)
                        VALUES (@ProfessionalId, @SlotDate, @SlotStart, @SlotEnd, @Status)";

            using var connection = CreateConnection();
            var result = await connection.ExecuteAsync(sql, slot);
            return result > 0;
        }

        public async Task UpdateSlotStatusAsync(int slotId, string status)
        {
            using var connection = CreateConnection();
            var query = "UPDATE availability_slots_tbl SET status = @Status WHERE slot_id = @SlotId";
            await connection.ExecuteAsync(query, new { SlotId = slotId, Status = status });
        }

        public async Task DeleteSlotAsync(int slotId)
        {
            using var connection = CreateConnection();
            var query = "DELETE FROM availability_slots_tbl WHERE slot_id = @SlotId";
            await connection.ExecuteAsync(query, new { SlotId = slotId });
        }

        public async Task GenerateSlotsAsync(int professionalId, DateTime start, DateTime end)
        {
            using var connection = CreateConnection();

            while (start < end)
            {
                var nextHour = start.AddHours(1);
                var slotDate = start.Date;

                var query = @"INSERT INTO availability_slots_tbl 
                              (professional_id, slot_date, slot_start, slot_end, status)
                              VALUES (@ProfessionalId, @SlotDate, @SlotStart, @SlotEnd, 'available')";

                await connection.ExecuteAsync(query, new
                {
                    ProfessionalId = professionalId,
                    SlotDate = slotDate,
                    SlotStart = start,
                    SlotEnd = nextHour
                });

                start = nextHour;
            }
        }

        public async Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId)
        {
            using var connection = CreateConnection();

            var query = @"
                SELECT 
                    slot_id AS SlotId, 
                    professional_id AS ProfessionalId,
                    slot_date AS SlotDate,
                    CAST(slot_start AS DATETIME) AS SlotStart, 
                    CAST(slot_end AS DATETIME) AS SlotEnd, 
                    status AS Status,
                    booked_by_email AS BookedByEmail
                FROM availability_slots_tbl 
                WHERE professional_id = @ProfessionalId";

            return await connection.QueryAsync<AvailabilitySlot>(query, new { ProfessionalId = professionalId });
        }

        public async Task<IEnumerable<SlotWithProfessional>> GetAllSlotDetailsAsync()
        {
            var sql = @"
                SELECT 
                    s.slot_id AS SlotId,
                    CAST(s.slot_start AS DATETIME) AS SlotStart,
                    CAST(s.slot_end AS DATETIME) AS SlotEnd,
                    s.status AS Status,
                    p.name AS ProfessionalName,
                    p.email AS Email,
                    p.phone AS Phone
                FROM availability_slots_tbl s
                JOIN tax_professional_tbl p ON s.professional_id = p.id";

            using var connection = CreateConnection();
            return await connection.QueryAsync<SlotWithProfessional>(sql);
        }
    }
}
