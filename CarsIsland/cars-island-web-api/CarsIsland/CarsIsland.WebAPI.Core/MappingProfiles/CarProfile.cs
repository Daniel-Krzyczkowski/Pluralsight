using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WebAPI.Core.MappingProfiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<DTOs.CarDto, Data.Models.Car>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.Brand, opt => opt.MapFrom(src => src.Brand))
                 .ForMember(s => s.Model, opt => opt.MapFrom(src => src.Model))
                 .ForMember(s => s.Cost, opt => opt.MapFrom(src => src.Cost))
                 .ForMember(s => s.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                 .ForMember(s => s.ContactPerson, opt => opt.MapFrom(src => src.ContactPerson))
                 .ForMember(s => s.Location, opt => opt.MapFrom(src => src.Location));

            CreateMap<Data.Models.Car, DTOs.CarDto>()
                 .ForMember(s => s.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(s => s.Brand, opt => opt.MapFrom(src => src.Brand))
                 .ForMember(s => s.Model, opt => opt.MapFrom(src => src.Model))
                 .ForMember(s => s.Cost, opt => opt.MapFrom(src => src.Cost))
                 .ForMember(s => s.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                 .ForMember(s => s.ContactPerson, opt => opt.MapFrom(src => src.ContactPerson))
                 .ForMember(s => s.Location, opt => opt.MapFrom(src => src.Location));
        }
    }
}
