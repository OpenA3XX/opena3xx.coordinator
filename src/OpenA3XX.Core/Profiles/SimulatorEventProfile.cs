using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class SimulatorEventProfile : Profile
    {
        public SimulatorEventProfile()
        {
            CreateMap<SimulatorEvent, SimulatorEventDto>()
                .ForMember(c => c.Id, m => m.MapFrom(c => c.Id))
                .ForMember(c => c.EventCode, m => m.MapFrom(c => c.EventCode))
                .ForMember(c => c.EventName, m => m.MapFrom(c => c.EventName))
                .ForMember(c => c.FriendlyName, m => m.MapFrom(c => c.FriendlyName))
                .ForMember(c => c.SimulatorSoftware, m => m.MapFrom(c => c.SimulatorSoftware))
                .ForMember(c => c.SimulatorSoftwareName, m => m.MapFrom(c => c.SimulatorSoftware.ToString()))
                .ForMember(c => c.SimulatorEventType, m => m.MapFrom(c => c.SimulatorEventType))
                .ForMember(c => c.SimulatorEventTypeName, m => m.MapFrom(c => c.SimulatorEventType.ToString()))
                .ForMember(c => c.SimulatorEventSdkType, m => m.MapFrom(c => c.SimulatorEventSdkType))
                .ForMember(c => c.SimulatorEventSdkTypeName, m => m.MapFrom(c => c.SimulatorEventSdkType.ToString()));
        }
    }
}