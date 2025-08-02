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
                .ForMember(c => c.HardwareInputSelectors, m => m.MapFrom(c => c.HardwareInputSelectorList))
                .ForMember(c => c.HardwarePanelId, m => m.MapFrom(c => c.HardwarePanelId));
                
            CreateMap<HardwareInputDto, HardwareInput>()
                .ForMember(c => c.HardwarePanelId, m => m.MapFrom(c => c.HardwarePanelId))
                .ForMember(c => c.HardwareInputTypeId, m => m.MapFrom(c => c.HardwareInputTypeId))
                .ForMember(c => c.HardwareInputType, m => m.Ignore()) // Ignore the string field when mapping to model
                .ForMember(c => c.HardwareInputSelectorList, m => m.Ignore()); // Ignore the selectors array when mapping to model
        }
    }
}