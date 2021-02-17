using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareInputSelectorProfile : Profile
    {
        public HardwareInputSelectorProfile()
        {
            CreateMap<HardwareInputSelector, HardwareInputSelectorDto>();
        }
    }
}