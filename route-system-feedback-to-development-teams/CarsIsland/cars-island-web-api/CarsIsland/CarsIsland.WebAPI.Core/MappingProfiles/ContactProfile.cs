using AutoMapper;

namespace CarsIsland.WebAPI.Core.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<DTOs.ContactDto, Data.Models.Contact>()
              .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(s => s.FirstName, opt => opt.MapFrom(src => src.FirstName))
              .ForMember(s => s.LastName, opt => opt.MapFrom(src => src.LastName))
              .ForMember(s => s.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
              .ForMember(s => s.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(s => s.Location, opt => opt.MapFrom(src => src.Location));

            CreateMap<Data.Models.Contact, DTOs.ContactDto>()
             .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(s => s.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForMember(s => s.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForMember(s => s.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForMember(s => s.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(s => s.Location, opt => opt.MapFrom(src => src.Location));
        }
    }
}
