using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwarePanelService : IHardwarePanelService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwarePanelRepository _hardwarePanelRepository;
        private readonly IHardwareBoardRepository _hardwareBoardRepository;
        private readonly IHardwarePanelTokensRepository _hardwarePanelTokensRepository;
        private readonly IAircraftModelRepository _aircraftModelRepository;
        private readonly IMapper _mapper;

        public HardwarePanelService(IHttpContextAccessor accessor,
            IHardwarePanelTokensRepository hardwarePanelTokensRepository,
            IAircraftModelRepository aircraftModelRepository,
            IHardwarePanelRepository hardwarePanelRepository,
            IHardwareBoardRepository hardwareBoardRepository,
            IMapper mapper)
        {
            _accessor = accessor;
            _hardwarePanelTokensRepository = hardwarePanelTokensRepository;
            _aircraftModelRepository = aircraftModelRepository;
            _hardwarePanelRepository = hardwarePanelRepository;
            _hardwareBoardRepository = hardwareBoardRepository;
            _mapper = mapper;
        }

        public void UpdateLastSeenForHardwarePane(Guid token)
        {
            _hardwarePanelTokensRepository.UpdateLastSeenForHardwarePanel(token);
        }

        public HardwarePanelTokenDto GetTokenDetailsByHardwarePanelId(int id)
        {
            var hardwarePanelToken = _hardwarePanelTokensRepository.GetByHardwarePanelId(id);
            var hardwarePanelTokenDto = _mapper.Map<HardwarePanelToken, HardwarePanelTokenDto>(hardwarePanelToken);

            return hardwarePanelTokenDto;
        }

        public HardwarePanelTokenDto GetTokenDetailsByHardwarePanelToken(Guid token)
        {
            var hardwarePanelToken = _hardwarePanelTokensRepository.GetByHardwarePanelToken(token);
            var hardwarePanelTokenDto = _mapper.Map<HardwarePanelToken, HardwarePanelTokenDto>(hardwarePanelToken);

            return hardwarePanelTokenDto;
        }

        public IList<HardwarePanelTokenDto> GetAllHardwarePanelTokens()
        {
            var hardwarePanelTokens = _hardwarePanelTokensRepository.GetAllHardwarePanelTokens();
            var hardwarePanelTokenDtoList =
                _mapper.Map<IList<HardwarePanelToken>, IList<HardwarePanelTokenDto>>(hardwarePanelTokens);

            return hardwarePanelTokenDtoList;
        }

        public IList<HardwarePanelOverviewDto> GetAllHardwarePanels()
        {
            var hardwarePanelList = _hardwarePanelRepository.GetAllHardwarePanels();
            var hardwarePaneDtoList =
                _mapper.Map<IList<HardwarePanel>, IList<HardwarePanelOverviewDto>>(hardwarePanelList);
            return hardwarePaneDtoList;
        }

        public HardwarePanelTokenDto RegisterHardwarePanel(DeviceRegistrationRequestDto deviceRegistrationRequest)
        {
            var hardwarePanelToken = new HardwarePanelToken
            {
                DeviceToken = Guid.NewGuid().ToString(),
                HardwarePanelId = deviceRegistrationRequest.HardwarePanelId,
                CreatedDateTime = DateTime.Now,
                DeviceIpAddress = _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };

            hardwarePanelToken = _hardwarePanelTokensRepository.SaveHardwarePanelToken(hardwarePanelToken);
            var hardwarePanelTokenDto = _mapper.Map<HardwarePanelToken, HardwarePanelTokenDto>(hardwarePanelToken);

            return hardwarePanelTokenDto;
        }

        public HardwarePanelDto GetHardwarePanelDetails(int id)
        {
            var hardwarePanel = _hardwarePanelRepository.GetHardwarePanelDetails(id);
            var hardwarePanelDto = _mapper.Map<HardwarePanel, HardwarePanelDto>(hardwarePanel);

            
            var hardwareBoards = _hardwareBoardRepository.GetAllHardwareBoards();
            foreach (var hardwareInput in hardwarePanelDto.HardwareInputs)
            {
                foreach (var hardwareInputSelector in hardwareInput.HardwareInputSelectors)
                {
                    foreach (var hardwareBoard in hardwareBoards)
                    {
                        foreach (var bus in hardwareBoard.Buses)
                        {
                            foreach (var bit in bus.Bits)
                            {
                                if (bit.HardwareInputSelector != null && bit.HardwareInputSelector.Id == hardwareInputSelector.Id)
                                {
                                    hardwareInputSelector.IsHardwareInputSelectorMappedWithHardware = true;
                                }
                            }
                        }
                    }
                }
            }
            
            return hardwarePanelDto;
        }

        public HardwarePanelDto AddHardwarePanel(AddHardwarePanelDto hardwarePanelDto)
        {
            var hardwarePanel = new HardwarePanel
            {
                Name = hardwarePanelDto.HardwarePanelName,
                AircraftModel = _aircraftModelRepository.GetById(hardwarePanelDto.AircraftModel),
                CockpitArea = (CockpitArea) hardwarePanelDto.CockpitArea,
                HardwarePanelOwner = (HardwarePanelOwner) hardwarePanelDto.HardwarePanelOwner
            };

            hardwarePanel = _hardwarePanelRepository.AddHardwarePanel(hardwarePanel);
            return _mapper.Map<HardwarePanel, HardwarePanelDto>(hardwarePanel);
        }
    }
}