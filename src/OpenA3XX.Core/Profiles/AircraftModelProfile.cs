using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class AircraftModelProfile : Profile
    {
        public AircraftModelProfile()
        {
            CreateMap<AircraftModel, AircraftModelDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer.Name));

            CreateMap<AddAircraftModelDto, AircraftModel>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => System.DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => System.DateTime.UtcNow))
                .ForMember(dest => dest.Manufacturer, opt => opt.Ignore()); // Ignore manufacturer mapping, handled in service

            CreateMap<UpdateAircraftModelDto, AircraftModel>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => System.DateTime.UtcNow))
                .ForMember(dest => dest.Manufacturer, opt => opt.Ignore()); // Ignore manufacturer mapping, handled in service
        }
    }
} 