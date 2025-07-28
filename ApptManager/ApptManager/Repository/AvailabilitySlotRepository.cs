using ApptManager.Models;
using ApptManager.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class AvailabilitySlotRepository : IAvailabilitySlotRepository
{
    private readonly IGenericRepository<AvailabilitySlot> _genericRepo;
    private readonly IDbConnection _connection;

    public AvailabilitySlotRepository(IDbConnection connection)
    {
        _connection = connection;
        _genericRepo = new GenericRepository<AvailabilitySlot>(connection);
    }

    public async Task<bool> AddAvailabilitySlotAsync(AvailabilitySlot slot)
    {
        var sql = @"INSERT INTO availability_slots_tbl 
                    (professional_id, slot_date, slot_start, slot_end, status)
                    VALUES (@ProfessionalId, @SlotDate, @SlotStart, @SlotEnd, @Status)";
        return await _genericRepo.ExecuteAsync(sql, slot);
    }

    public async Task AddSlotAsync(AvailabilitySlot slot)
    {
        await AddAvailabilitySlotAsync(slot);
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId)
    {
        var sql = @"SELECT * FROM availability_slots_tbl WHERE professional_id = @ProfessionalId";
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

    public async Task GenerateSlotsAsync(int professionalId, DateTime start, DateTime end)
    {
        while (start < end)
        {
            var slot = new AvailabilitySlot
            {
                ProfessionalId = professionalId,
                SlotDate = start.Date,
                SlotStart = start,
                SlotEnd = start.AddHours(1),
                Status = "available"
            };
            await AddAvailabilitySlotAsync(slot);
            start = start.AddHours(1);
        }
    }

    public async Task<IEnumerable<SlotWithProfessional>> GetAllSlotDetailsAsync()
    {
        var sql = @"
            SELECT 
                s.slot_id AS SlotId,
                s.slot_start AS SlotStart,
                s.slot_end AS SlotEnd,
                s.status AS Status,
                p.name AS ProfessionalName,
                p.email AS Email,
                p.phone AS Phone
            FROM availability_slots_tbl s
            JOIN tax_professional_tbl p ON s.professional_id = p.id";

        return await _connection.QueryAsync<SlotWithProfessional>(sql);
    }
}
