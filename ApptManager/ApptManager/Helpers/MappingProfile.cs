using AutoMapper;
using ApptManager.Models;
using ApptManager.Dtos;

namespace ApptManager.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TaxProfessional ↔ TaxProfessionalDto
            CreateMap<TaxProfessional, TaxProfessionalDto>();
            CreateMap<TaxProfessionalDto, TaxProfessional>();

            // UserRegistrationInfo ↔ UserRegistrationDto
            CreateMap<UserRegistrationInfo, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, UserRegistrationInfo>();

            // AvailabilitySlot ↔ AvailabilitySlotDto
            CreateMap<AvailabilitySlot, AvailabilitySlotDto>();
            CreateMap<AvailabilitySlotDto, AvailabilitySlot>();

            // TaxProfessionalWithSlots → TaxProfessionalWithSlotsDto
            CreateMap<TaxProfessionalWithSlots, TaxProfessionalWithSlotsDto>();

            // AvailabilitySlot → SlotWithProfessionalDto using navigation property
            CreateMap<AvailabilitySlot, SlotWithProfessionalDto>()
                .ForMember(dest => dest.ProfessionalName, opt => opt.MapFrom(src => src.Professional.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Professional.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Professional.Phone))
                .ForMember(dest => dest.SlotStart, opt => opt.MapFrom(src => src.SlotStart.ToString("g")))
                .ForMember(dest => dest.SlotEnd, opt => opt.MapFrom(src => src.SlotEnd.ToString("g")))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
