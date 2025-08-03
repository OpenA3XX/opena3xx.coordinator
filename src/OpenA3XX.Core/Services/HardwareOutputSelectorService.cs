using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareOutputSelectorService : IHardwareOutputSelectorService
    {
        private readonly IHardwareOutputSelectorRepository _hardwareOutputSelectorRepository;
        private readonly IMapper _mapper;

        public HardwareOutputSelectorService(IHardwareOutputSelectorRepository hardwareOutputSelectorRepository,
            IMapper mapper)
        {
            _hardwareOutputSelectorRepository = hardwareOutputSelectorRepository;
            _mapper = mapper;
        }

        public HardwareOutputSelectorDto GetHardwareOutputSelectorDetails(int hardwareOutputSelectorId)
        {
            var hardwareOutputSelector =
                _hardwareOutputSelectorRepository.GetHardwareOutputSelectorBy(hardwareOutputSelectorId);
            var hardwareOutputSelectorDto =
                _mapper.Map<HardwareOutputSelector, HardwareOutputSelectorDto>(hardwareOutputSelector);
            return hardwareOutputSelectorDto;
        }

        public HardwareOutputSelectorDto Add(AddHardwareOutputSelectorDto addHardwareOutputSelectorDto)
        {
            var hardwareOutputSelector = _mapper.Map<AddHardwareOutputSelectorDto, HardwareOutputSelector>(addHardwareOutputSelectorDto);
            hardwareOutputSelector = _hardwareOutputSelectorRepository.AddHardwareOutputSelector(hardwareOutputSelector);
            var hardwareOutputSelectorDto = _mapper.Map<HardwareOutputSelector, HardwareOutputSelectorDto>(hardwareOutputSelector);
            return hardwareOutputSelectorDto;
        }

        public void Delete(int id)
        {
            _hardwareOutputSelectorRepository.DeleteHardwareOutputSelector(id);
        }
    }
}