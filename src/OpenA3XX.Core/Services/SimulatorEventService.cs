using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class SimulatorEventService : ISimulatorEventService
    {
        private readonly IMapper _mapper;
        private readonly ISimulatorEventRepository _simulatorEventRepository;

        public SimulatorEventService(IHttpContextAccessor accessor,
            ISimulatorEventRepository simulatorEventRepository, IMapper mapper)
        {
            _simulatorEventRepository = simulatorEventRepository;
            _mapper = mapper;
        }

        public IList<SimulatorEventDto> GetAllSimulatorEvents()
        {
            var data = _simulatorEventRepository.GetAllSimulatorEvents();
            var simulatorEventDtoList = _mapper.Map<IList<SimulatorEvent>, IList<SimulatorEventDto>>(data);
            return simulatorEventDtoList;
        }
    }
}