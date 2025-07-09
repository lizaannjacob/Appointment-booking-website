using ApptManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApptManager.Repository
{
    public interface ITaxProfessionalRepository
    {
        Task AddProfessionalAsync(TaxProfessional professional);
        Task<IEnumerable<TaxProfessional>> GetAllProfessionalsAsync();
        Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> slots);
        Task<bool> AddTaxProfessionalAsync(TaxProfessional professional);
        Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId);
        Task UpdateSlotStatusAsync(int slotId, string status);
        Task DeleteSlotAsync(int slotId);
        Task<IEnumerable<TaxProfessional>> GetAllAsync();
    }
}
