using System;
using System.Linq;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwarePanelProfile : Profile
    {
        public HardwarePanelProfile()
        {
            CreateMap<HardwarePanel, HardwarePanelOverviewDto>()
                .ForMember(c => c.Manufacturer, m => m.MapFrom(c => c.AircraftModel.Manufacturer.Name))
                .ForMember(c => c.Owner, m => m.MapFrom(c => c.HardwarePanelOwner.ToString()))
                .ForMember(c => c.AircraftModel, m => m.MapFrom(c => c.AircraftModel.Model))
                .ForMember(c => c.CockpitArea, m => m.MapFrom(c => c.CockpitArea.ToString()));

            CreateMap<HardwarePanel, HardwarePanelDto>()
                .ForMember(c => c.Manufacturer, m => m.MapFrom(c => c.AircraftModel.Manufacturer.Name))
                .ForMember(c => c.Owner, m => m.MapFrom(c => c.HardwarePanelOwner.ToString()))
                .ForMember(c => c.AircraftModel, m => m.MapFrom(c => c.AircraftModel.Model))
                .ForMember(c => c.CockpitArea, m => m.MapFrom(c => c.CockpitArea.ToString()))
                .ForMember(c => c.TotalInputs, m => m.MapFrom(c => c.HardwareInput.Count))
                .ForMember(c => c.TotalOutputs, m => m.MapFrom(c => c.HardwareOutput.Count))
                .ForMember(c => c.HardwareInputs, m => m.MapFrom(c => c.HardwareInput))
                .ForMember(c => c.HardwareOutputs, m => m.MapFrom(c => c.HardwareOutput))
                .ForMember(c => c.TotalOutputsDiscrete,
                    m => m.MapFrom(c => c.HardwareOutput.Sum(c => c.HardwareOutputSelectorList.Count)))
                .ForMember(c => c.TotalInputsDiscrete,
                    m => m.MapFrom(c => c.HardwareInput.Sum(c => c.HardwareInputSelectorList.Count)));

            /*
            CreateMap<HardwarePanel, AddHardwarePanelDto>()
                .ForMember(c => c.AircraftModel, m => m.MapFrom(c => c.AircraftModel.Id))
                .ForMember(c => c.AircraftManufacturer, m => m.MapFrom(c => c.AircraftModel.Manufacturer.Id))
                .ForMember(c => c.CockpitArea, m => m.MapFrom(c => c.CockpitArea))
                .ForMember(c => c.HardwarePanelName, m => m.MapFrom(c => c.Name))
                .ForMember(c => c.HardwarePanelOwner, m => m.MapFrom(c => c.HardwarePanelOwner));*/

            CreateMap<AddHardwarePanelDto, HardwarePanel>()
                .ForPath(c => c.AircraftModel.Id, m => m.MapFrom(c => c.AircraftModel))
                .ForMember(c => c.AircraftModelId, m => m.MapFrom(c => c.AircraftModel))
                .ForMember(c => c.CockpitArea, m => m.MapFrom(c => c.CockpitArea))
                .ForMember(c => c.Name, m => m.MapFrom(c => c.HardwarePanelName))
                .ForMember(c => c.HardwarePanelOwner, m => m.MapFrom(c => c.HardwarePanelOwner));

            CreateMap<UpdateHardwarePanelDto, HardwarePanel>()
                .ForMember(c => c.AircraftModelId, m => m.MapFrom(c => c.AircraftModel))
                .ForMember(c => c.CockpitArea, m => m.MapFrom(c => c.CockpitArea))
                .ForMember(c => c.Name, m => m.MapFrom(c => c.Name))
                .ForMember(c => c.HardwarePanelOwner, m => m.MapFrom(c => c.Owner));



        }
    }
}