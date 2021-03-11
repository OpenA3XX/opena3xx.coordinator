using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class SimulatorEventService : ISimulatorEventService
    {
        private readonly IHardwareInputSelectorRepository _hardwareInputSelectorRepository;
        private readonly IMapper _mapper;
        private readonly ISimulatorEventRepository _simulatorEventRepository;

        public SimulatorEventService(ISimulatorEventRepository simulatorEventRepository,
            IHardwareInputSelectorRepository hardwareInputSelectorRepository,
            IMapper mapper)
        {
            _simulatorEventRepository = simulatorEventRepository;
            _hardwareInputSelectorRepository = hardwareInputSelectorRepository;
            _mapper = mapper;
        }

        public IList<SimulatorEventDto> GetAllSimulatorEvents()
        {
            var data = _simulatorEventRepository.GetAllSimulatorEvents();
            var simulatorEventDtoList = _mapper.Map<IList<SimulatorEvent>, IList<SimulatorEventDto>>(data);
            return simulatorEventDtoList;
        }

        public IList<SimulatorEventDto> GetByIntegrationType(int integrationTypeId)
        {
            var data = _simulatorEventRepository.GetAllSimulatorEventsByIntegrationType(integrationTypeId);
            var simulatorEventDtoList = _mapper.Map<IList<SimulatorEvent>, IList<SimulatorEventDto>>(data);
            return simulatorEventDtoList;
        }

        public SimulatorEventDto GetSimulatorEventDetails(int simulatorEventId)
        {
            var data = _simulatorEventRepository.GetSimulatorEventBy(simulatorEventId);
            var simulatorEventDto = _mapper.Map<SimulatorEvent, SimulatorEventDto>(data);
            return simulatorEventDto;
        }

        public IList<KeyValuePair<int, string>> GetAllIntegrationTypes()
        {
            var dict = Enum.GetNames(typeof(SimulatorEventSdkType))
                .ToDictionary(name => (int) Enum.Parse(typeof(SimulatorEventSdkType), name));
            var data = dict.Select(item => new KeyValuePair<int, string>(item.Key, item.Value)).ToList();
            return data;
        }

        public void SaveSimulatorEventLinking(int simulatorEventId, int hardwareInputId, int hardwareInputSelectorId)
        {
            var simulatorEvent = _simulatorEventRepository.GetSimulatorEventBy(simulatorEventId);
            var hardwareInputSelector =
                _hardwareInputSelectorRepository.GetHardwareInputSelectorBy(hardwareInputSelectorId);
            hardwareInputSelector.SimulatorEvent = simulatorEvent;
            _hardwareInputSelectorRepository.UpdateHardwareInputSelector(hardwareInputSelector);
        }
    }
}