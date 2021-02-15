using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareOutputTypeProfile : Profile
    {
        public HardwareOutputTypeProfile()
        {
            CreateMap<HardwareOutputType, HardwareOutputTypeDto>();
            CreateMap<HardwareOutputTypeDto, HardwareOutputType>();
        }
    }
}