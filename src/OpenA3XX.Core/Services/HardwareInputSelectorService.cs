using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareInputSelectorService : IHardwareInputSelectorService
    {
        private readonly IHardwareInputSelectorRepository _hardwareInputSelectorRepository;
        private readonly IMapper _mapper;

        public HardwareInputSelectorService(IHardwareInputSelectorRepository hardwareInputSelectorRepository,
            IMapper mapper)
        {
            _hardwareInputSelectorRepository = hardwareInputSelectorRepository;
            _mapper = mapper;
        }

        public HardwareInputSelectorDto GetHardwareInputSelectorDetails(int hardwareInputSelectorId)
        {
            var hardwareInputSelector =
                _hardwareInputSelectorRepository.GetHardwareInputSelectorBy(hardwareInputSelectorId);
            var hardwareInputSelectorDto =
                _mapper.Map<HardwareInputSelector, HardwareInputSelectorDto>(hardwareInputSelector);
            return hardwareInputSelectorDto;
        }
        
    }
}