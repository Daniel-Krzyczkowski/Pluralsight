using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WebAPI.Core.MappingProfiles
{
    public class LocationsProfile : Profile
    {
        public LocationsProfile()
        {
            CreateMap<DTOs.LocationDto, Data.Models.Location>()
             .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(s => s.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(s => s.City, opt => opt.MapFrom(src => src.City))
             .ForMember(s => s.Country, opt => opt.MapFrom(src => src.Country));

            CreateMap<Data.Models.Location, DTOs.LocationDto>()
             .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(s => s.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(s => s.City, opt => opt.MapFrom(src => src.City))
             .ForMember(s => s.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
