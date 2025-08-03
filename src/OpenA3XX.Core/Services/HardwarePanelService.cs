using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
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
            if (hardwarePanelList == null || !hardwarePanelList.Any())
            {
                return new List<HardwarePanelOverviewDto>();
            }
            
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
            if (hardwarePanel == null)
            {
                return null;
            }
            
            var hardwarePanelDto = _mapper.Map<HardwarePanel, HardwarePanelDto>(hardwarePanel);

            // Handle null collections
            if (hardwarePanelDto.HardwareInputs == null)
            {
                hardwarePanelDto.HardwareInputs = new List<HardwareInputDto>();
            }
            
            if (hardwarePanelDto.HardwareOutputs == null)
            {
                hardwarePanelDto.HardwareOutputs = new List<HardwareOutputDto>();
            }
            
            var hardwareBoards = _hardwareBoardRepository.GetAllHardwareBoards();
            foreach (var hardwareInput in hardwarePanelDto.HardwareInputs)
            {
                if (hardwareInput.HardwareInputSelectors != null)
                {
                    foreach (var hardwareInputSelector in hardwareInput.HardwareInputSelectors)
                    {
                        foreach (var hardwareBoard in hardwareBoards)
                        {
                            if (hardwareBoard.Buses != null)
                            {
                                foreach (var bus in hardwareBoard.Buses)
                                {
                                    if (bus.Bits != null)
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
                    }
                }
            }

            foreach (var hardwareInput in hardwarePanelDto.HardwareOutputs)
            {
                if (hardwareInput.HardwareOutputSelectors != null)
                {
                    foreach (var hardwareOutputSelector in hardwareInput.HardwareOutputSelectors)
                    {
                        foreach (var hardwareBoard in hardwareBoards)
                        {
                            if (hardwareBoard.Buses != null)
                            {
                                foreach (var bus in hardwareBoard.Buses)
                                {
                                    if (bus.Bits != null)
                                    {
                                        foreach (var bit in bus.Bits)
                                        {
                                            if (bit.HardwareOutputSelector != null && bit.HardwareOutputSelector.Id == hardwareOutputSelector.Id)
                                            {
                                                hardwareOutputSelector.IsHardwareOutputSelectorMappedWithHardware = true;
                                            }
                                        }
                                    }
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

        /// <summary>
        /// Deletes a hardware panel and all its associated entities (cascade delete)
        /// </summary>
        /// <param name="id">The hardware panel ID to delete</param>
        public void Delete(int id)
        {
            // Note: With the cascade delete configuration in CoreDataContext,
            // all related entities will be automatically deleted:
            // - HardwareInput records
            // - HardwareOutput records  
            // - HardwareInputSelector records (via HardwareInput)
            // - HardwareOutputSelector records (via HardwareOutput)
            _hardwarePanelRepository.DeleteHardwarePanel(id);
        }

        /// <summary>
        /// Updates an existing hardware panel
        /// </summary>
        /// <param name="id">The hardware panel ID to update</param>
        /// <param name="updateHardwarePanelDto">The hardware panel data to update</param>
        /// <returns>The updated hardware panel</returns>
        public HardwarePanelDto Update(int id, UpdateHardwarePanelDto updateHardwarePanelDto)
        {
            // Get the existing hardware panel
            var existingHardwarePanel = _hardwarePanelRepository.GetHardwarePanelDetails(id);
            if (existingHardwarePanel == null)
            {
                throw new EntityNotFoundException("HardwarePanel", id);
            }

            // Update the properties
            existingHardwarePanel.Name = updateHardwarePanelDto.Name;
            existingHardwarePanel.AircraftModel = _aircraftModelRepository.GetById(updateHardwarePanelDto.AircraftModel);
            existingHardwarePanel.CockpitArea = (CockpitArea)updateHardwarePanelDto.CockpitArea;
            existingHardwarePanel.HardwarePanelOwner = (HardwarePanelOwner)updateHardwarePanelDto.Owner;

            // Save the updated hardware panel
            var updatedHardwarePanel = _hardwarePanelRepository.UpdateHardwarePanel(existingHardwarePanel);
            return _mapper.Map<HardwarePanel, HardwarePanelDto>(updatedHardwarePanel);
        }
    }
}