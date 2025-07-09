using ApptManager.Models;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<int> InsertUserAsync(UserRegistrationInfo user);
    Task<bool> InsertLoginAsync(string email, string password, string role);
    Task<string> GetRoleByCredentialsAsync(string email, string password);
    Task<IEnumerable<TaxProfessionalWithSlots>> GetProfessionalsWithSlotsAsync();
    Task<int> GetProfessionalIdByEmailAsync(string email);
    Task<List<AvailabilitySlot>> GetBookedSlotsByProfessionalIdAsync(int professionalId);

    Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByProfessionalAsync(int professionalId);

    Task<bool> UpdateSlotStatusAsync(int slotId, string status, string email);

    Task<IEnumerable<AvailabilitySlot>> GetBookedSlotsByUserEmailAsync(string email);



}
