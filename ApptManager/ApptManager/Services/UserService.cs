using ApptManager.Models;
using ApptManager.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApptManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> RegisterUserAsync(UserRegistrationInfo user)
        {
            int userId = await _userRepo.InsertUserAsync(user);
            return await _userRepo.InsertLoginAsync(user.Email, user.Password, user.Role);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            return await _userRepo.GetRoleByCredentialsAsync(email, password);
        }

        public async Task<IEnumerable<TaxProfessionalWithSlots>> GetAvailableSlotsAsync()
        {
            var all = await _userRepo.GetProfessionalsWithSlotsAsync();
            return all.Select(p => new TaxProfessionalWithSlots
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                IncomeTaxFilingSpecialist = p.IncomeTaxFilingSpecialist,
                CorporateTaxConsultant = p.CorporateTaxConsultant,
                InvestmentTaxPlanningAdvisor = p.InvestmentTaxPlanningAdvisor,
                Slots = p.Slots.Where(s => s.Status == "available").ToList()
            });
        }

        public async Task<IEnumerable<AvailabilitySlot>> GetBookedSlotsByEmailAsync(string email)
        {
            return await _userRepo.GetBookedSlotsByUserEmailAsync(email);
        }

        public async Task<IEnumerable<TaxProfessionalWithSlots>> GetProfessionalsWithSlotsAsync()
        {
            return await _userRepo.GetProfessionalsWithSlotsAsync();
        }

        public async Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByProfessionalAsync(int professionalId)
        {
            return await _userRepo.GetAvailableSlotsByProfessionalAsync(professionalId);
        }

        public async Task<bool> UpdateSlotStatusAsync(int slotId, string status, string email)
        {
            return await _userRepo.UpdateSlotStatusAsync(slotId, status, email);
        }
    }
}
