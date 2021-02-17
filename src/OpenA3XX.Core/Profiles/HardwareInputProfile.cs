using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareInputProfile : Profile
    {
        public HardwareInputProfile()
        {
            CreateMap<HardwareInput, HardwareInputDto>()
                .ForMember(c => c.HardwareInputType, m => m.MapFrom(c => c.HardwareInputType.Name))
                .ForMember(c => c.HardwareInputSelectors, m => m.MapFrom(c => c.HardwareInputSelectorList));
        }
    }
}