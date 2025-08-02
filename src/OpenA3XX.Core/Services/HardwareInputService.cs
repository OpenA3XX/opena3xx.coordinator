using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    /// <summary>
    /// Service for managing hardware input operations
    /// </summary>
    public class HardwareInputService : IHardwareInputService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareInputRepository _hardwareInputRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the HardwareInputService
        /// </summary>
        /// <param name="accessor">HTTP context accessor</param>
        /// <param name="hardwareInputRepository">Hardware input repository</param>
        /// <param name="mapper">AutoMapper instance</param>
        public HardwareInputService(IHttpContextAccessor accessor,
            IHardwareInputRepository hardwareInputRepository, IMapper mapper)
        {
            _accessor = accessor;
            _hardwareInputRepository = hardwareInputRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all hardware inputs
        /// </summary>
        /// <returns>List of all hardware input DTOs</returns>
        public IList<HardwareInputDto> GetAll()
        {
            var hardwareInputs = _hardwareInputRepository.GetAllHardwareInputs();
            var hardwareInputsDtoList =
                _mapper.Map<IList<HardwareInput>, IList<HardwareInputDto>>(hardwareInputs);

            return hardwareInputsDtoList;
        }

        /// <summary>
        /// Gets hardware inputs by panel ID
        /// </summary>
        /// <param name="panelId">The hardware panel ID</param>
        /// <returns>List of hardware input DTOs for the specified panel</returns>
        public IList<HardwareInputDto> GetByPanelId(int panelId)
        {
            var hardwareInputs = _hardwareInputRepository.GetHardwareInputsByPanelId(panelId);
            var hardwareInputsDtoList =
                _mapper.Map<IList<HardwareInput>, IList<HardwareInputDto>>(hardwareInputs);

            return hardwareInputsDtoList;
        }

        /// <summary>
        /// Gets a hardware input by its ID
        /// </summary>
        /// <param name="id">The hardware input ID</param>
        /// <returns>The hardware input DTO</returns>
        public HardwareInputDto GetBy(int id)
        {
            var hardwareInput = _hardwareInputRepository.GetHardwareInputBy(id);
            
            if (hardwareInput == null)
            {
                throw new EntityNotFoundException("HardwareInput", id);
            }
            
            var hardwareInputDto = _mapper.Map<HardwareInput, HardwareInputDto>(hardwareInput);
            return hardwareInputDto;
        }

        /// <summary>
        /// Adds a new hardware input
        /// </summary>
        /// <param name="hardwareInputDto">The hardware input DTO to add</param>
        /// <returns>The created hardware input DTO</returns>
        public HardwareInputDto Add(HardwareInputDto hardwareInputDto)
        {
            var hardwareInput = _mapper.Map<HardwareInputDto, HardwareInput>(hardwareInputDto);
            hardwareInput = _hardwareInputRepository.AddHardwareInput(hardwareInput);
            hardwareInputDto = _mapper.Map<HardwareInput, HardwareInputDto>(hardwareInput);
            return hardwareInputDto;
        }

        /// <summary>
        /// Updates an existing hardware input
        /// </summary>
        /// <param name="hardwareInputDto">The hardware input DTO to update</param>
        /// <returns>The updated hardware input DTO</returns>
        public HardwareInputDto Update(HardwareInputDto hardwareInputDto)
        {
            // Verify the hardware input exists
            var existingInput = _hardwareInputRepository.GetHardwareInputBy(hardwareInputDto.Id);
            if (existingInput == null)
            {
                throw new EntityNotFoundException("HardwareInput", hardwareInputDto.Id);
            }

            var hardwareInput = _mapper.Map<HardwareInputDto, HardwareInput>(hardwareInputDto);
            hardwareInput = _hardwareInputRepository.UpdateHardwareInput(hardwareInput);
            hardwareInputDto = _mapper.Map<HardwareInput, HardwareInputDto>(hardwareInput);
            return hardwareInputDto;
        }

        /// <summary>
        /// Deletes a hardware input by its ID
        /// </summary>
        /// <param name="id">The hardware input ID to delete</param>
        public void Delete(int id)
        {
            _hardwareInputRepository.DeleteHardwareInput(id);
        }
    }
} 