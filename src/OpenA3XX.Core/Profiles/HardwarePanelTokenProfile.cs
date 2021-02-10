using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwarePanelTokenProfile : Profile
    {
        public HardwarePanelTokenProfile()
        {
            CreateMap<HardwarePanelToken, HardwarePanelTokenDto>()
                .ForMember(c => c.Name, m => m.MapFrom(c => c.HardwarePanel.Name));
        }
    }
}