using ApptManager.Models;
using System.Threading.Tasks;
namespace ApptManager.Services;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserRegistrationInfo user);
    Task<string> LoginAsync(string email, string password);

    Task<IEnumerable<TaxProfessionalWithSlots>> GetAvailableSlotsAsync();

    Task<IEnumerable<AvailabilitySlot>> GetBookedSlotsByEmailAsync(string email);
    Task<IEnumerable<TaxProfessionalWithSlots>> GetProfessionalsWithSlotsAsync();

    Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByProfessionalAsync(int professionalId);

    Task<bool> UpdateSlotStatusAsync(int slotId, string status, string email);




}
