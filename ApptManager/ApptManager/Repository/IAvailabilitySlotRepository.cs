using ApptManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public interface IAvailabilitySlotRepository
    {
        Task<bool> AddAvailabilitySlotAsync(AvailabilitySlot slot);
        Task AddSlotAsync(AvailabilitySlot slot);

        Task UpdateSlotStatusAsync(int slotId, string status);
        Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId);
        Task DeleteSlotAsync(int slotId);
        Task GenerateSlotsAsync(int professionalId, DateTime start, DateTime end);

        Task<IEnumerable<SlotWithProfessional>> GetAllSlotDetailsAsync();

    }
}
