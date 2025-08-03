using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareInputSelectorProfile : Profile
    {
        public HardwareInputSelectorProfile()
        {
            CreateMap<HardwareInputSelector, HardwareInputSelectorDto>()
                .ForMember(c => c.SimulatorEventDto, m => m.MapFrom(c => c.SimulatorEvent))
                .ForMember(c => c.HardwareInputId, m => m.MapFrom(c => c.HardwareInputId));
                
            CreateMap<AddHardwareInputSelectorDto, HardwareInputSelector>()
                .ForMember(c => c.HardwareInputId, m => m.MapFrom(c => c.HardwareInputId));
        }
    }
}