using ApptManager.Models;
using ApptManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace ApptManager.Services
{
    public class AdminService : IAdminService
    {
        private readonly ITaxProfessionalRepository _taxRepo;
        private readonly IAvailabilitySlotRepository _slotRepo;

        public AdminService(ITaxProfessionalRepository taxRepo, IAvailabilitySlotRepository slotRepo)
        {
            _taxRepo = taxRepo;
            _slotRepo = slotRepo;
        }

        public async Task<bool> AddProfessionalAsync(TaxProfessional professional)
        {
            return await _taxRepo.AddTaxProfessionalAsync(professional);
        }

        public async Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> slots)
        {
            await _taxRepo.AddAvailabilitySlotsAsync(slots);
        }

        public async Task<bool> GenerateSlotsAsync(int professionalId, DateTime startTime, DateTime endTime)
        {
            try
            {
                DateTime current = startTime;

                while (current < endTime)
                {
                    DateTime next = current.AddHours(1);
                    var slot = new AvailabilitySlot
                    {
                        ProfessionalId = professionalId,
                        SlotDate = current.Date,
                        SlotStart = current,
                        SlotEnd = next,
                        Status = "available"
                    };

                    await _slotRepo.AddAvailabilitySlotAsync(slot);
                    current = next;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task UpdateSlotStatusAsync(int slotId, string status)
        {
            await _taxRepo.UpdateSlotStatusAsync(slotId, status);
        }

        public async Task DeleteSlotAsync(int slotId)
        {
            await _taxRepo.DeleteSlotAsync(slotId);
        }

        public async Task<IEnumerable<AvailabilitySlot>> GetSlotsByProfessionalIdAsync(int professionalId)
        {
            return await _taxRepo.GetSlotsByProfessionalIdAsync(professionalId);
        }

        public async Task<IEnumerable<TaxProfessional>> GetAllProfessionalsAsync()
        {
            return await _taxRepo.GetAllProfessionalsAsync();
        }

        public async Task<IEnumerable<TaxProfessionalWithSlots>> GetAllSlotDetailsAsync()
        {
            var professionals = await _taxRepo.GetAllProfessionalsAsync();
            var result = new List<TaxProfessionalWithSlots>();

            foreach (var pro in professionals)
            {
                var slots = await _slotRepo.GetSlotsByProfessionalIdAsync(pro.Id);

                var available = slots
                    .Where(s => s.Status == "available")
                    .Select(s => $"{s.SlotStart:dd-MM-yyyy hh:mm tt} - {s.SlotEnd:hh:mm tt}")
                    .ToList();

                var booked = slots
                    .Where(s => s.Status == "booked")
                    .Select(s => $"{s.SlotStart:dd-MM-yyyy hh:mm tt} - {s.SlotEnd:hh:mm tt}")
                    .ToList();


                result.Add(new TaxProfessionalWithSlots
                {
                    Id = pro.Id,
                    Name = pro.Name,
                    Email = pro.Email,
                    Phone = pro.Phone,
                    IncomeTaxFilingSpecialist = pro.IncomeTaxFilingSpecialist,
                    CorporateTaxConsultant = pro.CorporateTaxConsultant,
                    InvestmentTaxPlanningAdvisor = pro.InvestmentTaxPlanningAdvisor,
                    AvailableSlotTimes = available,
                    BookedSlotTimes = booked
                });
            }

            return result;
        }
    }
}
