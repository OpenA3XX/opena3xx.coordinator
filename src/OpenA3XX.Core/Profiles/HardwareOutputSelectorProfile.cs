using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareOutputSelectorProfile : Profile
    {
        public HardwareOutputSelectorProfile()
        {
            CreateMap<HardwareOutputSelector, HardwareOutputSelectorDto>()
                .ForMember(c => c.HardwareOutputId, m => m.MapFrom(c => c.HardwareOutputId))
                .ForMember(c => c.SimulatorEventDto, m => m.Ignore()); // HardwareOutputSelector doesn't have SimulatorEvent
                
            CreateMap<AddHardwareOutputSelectorDto, HardwareOutputSelector>()
                .ForMember(c => c.HardwareOutputId, m => m.MapFrom(c => c.HardwareOutputId));
        }
    }
}