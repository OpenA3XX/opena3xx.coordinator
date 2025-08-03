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
                .ForMember(c => c.HardwareOutputSelectors, m => m.MapFrom(c => c.HardwareOutputSelectorList))
                .ForMember(c => c.HardwarePanelId, m => m.MapFrom(c => c.HardwarePanelId));
                
            CreateMap<HardwareOutputDto, HardwareOutput>()
                .ForMember(c => c.HardwarePanelId, m => m.MapFrom(c => c.HardwarePanelId))
                .ForMember(c => c.HardwareOutputTypeId, m => m.MapFrom(c => c.HardwareOutputTypeId))
                .ForMember(c => c.HardwareOutputType, m => m.Ignore()) // Ignore the string field when mapping to model
                .ForMember(c => c.HardwareOutputSelectorList, m => m.Ignore()); // Ignore the selectors array when mapping to model
        }
    }
}