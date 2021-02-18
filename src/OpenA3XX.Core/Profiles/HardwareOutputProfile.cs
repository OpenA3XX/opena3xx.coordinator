using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareOutputProfile : Profile
    {
        public HardwareOutputProfile()
        {
            CreateMap<HardwareOutput, HardwareOutputDto>()
                .ForMember(c => c.HardwareOutputType, m => m.MapFrom(c => c.HardwareOutputType.Name))
                .ForMember(c => c.HardwareOutputSelectors, m => m.MapFrom(c => c.HardwareOutputSelectorList));
        }
    }
}