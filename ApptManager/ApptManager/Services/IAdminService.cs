using ApptManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApptManager.Services
{
    public interface IAdminService
    {
        Task<bool> AddProfessionalAsync(TaxProfessional professional);

        Task<bool> GenerateSlotsAsync(int professionalId, DateTime startTime, DateTime endTime);

        Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> slots);
        Task UpdateSlotStatusAsync(int slotId, string status);
        Task DeleteSlotAsync(int slotId);
        Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId);
        Task<IEnumerable<TaxProfessional>> GetAllProfessionalsAsync();

        Task<IEnumerable<TaxProfessionalWithSlots>> GetAllSlotDetailsAsync();

    }
}
