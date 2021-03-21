using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareBoardProfile : Profile
    {
        public HardwareBoardProfile()
        {
            CreateMap<HardwareBoard, HardwareBoardDto>()
                .ForMember(c => c.HardwareBusExtendersCount,
                    m => m.MapFrom(c => c.Buses.Count));


            CreateMap<HardwareBoard, HardwareBoardDetailsDto>()
                .ForMember(c => c.Id, m => m.MapFrom(c => c.Id))
                .ForMember(c => c.Name, m => m.MapFrom(c => c.Name))
                .ForMember(c => c.IOExtenderBuses, m => m.MapFrom(c => c.Buses));

            CreateMap<IOExtenderBit, IOExtenderBitDto>()
                .ForMember(c => c.Id, m => m.MapFrom(c => c.Id))
                .ForMember(c => c.Name, m => m.MapFrom(c => c.HardwareBit.ToString())).ForMember(
                    c => c.HardwareInputSelectorFullName,
                    m => m.MapFrom(c => $"{c.HardwareInputSelector.HardwareInput.HardwarePanel.Name} → {c.HardwareInputSelector.HardwareInput.Name} → {c.HardwareInputSelector.Name}"))
                .ForMember(c => c.HardwareOutputSelectorFullName,
                    m => m.MapFrom(c => $"{c.HardwareOutputSelector.HardwareOutput.HardwarePanel.Name} → {c.HardwareOutputSelector.HardwareOutput.Name} → {c.HardwareOutputSelector.Name}"));

            CreateMap<IOExtenderBus, IOExtenderBusDto>()
                .ForMember(c => c.Id, m => m.MapFrom(c => c.Id))
                .ForMember(c => c.Name, m => m.MapFrom(c => c.HardwareBus.ToString()))
                .ForMember(c => c.IOExtenderBusBits, m => m.MapFrom(c => c.Bits));

        }
    }
}