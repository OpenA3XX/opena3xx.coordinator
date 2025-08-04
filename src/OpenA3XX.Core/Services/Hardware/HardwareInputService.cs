using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Hardware;

namespace OpenA3XX.Core.Services.Hardware
{
    /// <summary>
    /// Service for managing hardware input operations
    /// </summary>
    public class HardwareInputService : IHardwareInputService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareInputRepository _hardwareInputRepository;
        private readonly IHardwareInputTypesRepository _hardwareInputTypesRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the HardwareInputService
        /// </summary>
        /// <param name="accessor">HTTP context accessor</param>
        /// <param name="hardwareInputRepository">Hardware input repository</param>
        /// <param name="hardwareInputTypesRepository">Hardware input types repository</param>
        /// <param name="mapper">AutoMapper instance</param>
        public HardwareInputService(IHttpContextAccessor accessor,
            IHardwareInputRepository hardwareInputRepository,
            IHardwareInputTypesRepository hardwareInputTypesRepository,
            IMapper mapper)
        {
            _accessor = accessor;
            _hardwareInputRepository = hardwareInputRepository;
            _hardwareInputTypesRepository = hardwareInputTypesRepository;
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
            // Find or create hardware input type if specified
            if (!string.IsNullOrEmpty(hardwareInputDto.HardwareInputType))
            {
                var hardwareInputType = FindOrCreateHardwareInputType(hardwareInputDto.HardwareInputType);
                hardwareInputDto.HardwareInputTypeId = hardwareInputType.Id;
            }
            
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

            // Find or create hardware input type if specified
            if (!string.IsNullOrEmpty(hardwareInputDto.HardwareInputType))
            {
                var hardwareInputType = FindOrCreateHardwareInputType(hardwareInputDto.HardwareInputType);
                hardwareInputDto.HardwareInputTypeId = hardwareInputType.Id;
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
            // Note: With the cascade delete configuration in CoreDataContext,
            // related HardwareInputSelector records will be automatically deleted
            _hardwareInputRepository.DeleteHardwareInput(id);
        }

        /// <summary>
        /// Finds or creates a hardware input type by name
        /// </summary>
        /// <param name="hardwareInputTypeName">The name of the hardware input type</param>
        /// <returns>The hardware input type</returns>
        private HardwareInputType FindOrCreateHardwareInputType(string hardwareInputTypeName)
        {
            var hardwareInputTypes = _hardwareInputTypesRepository.GetAllHardwareInputTypes();
            var hardwareInputType = hardwareInputTypes.FirstOrDefault(t => t.Name.Equals(hardwareInputTypeName, global::System.StringComparison.OrdinalIgnoreCase));
            
            if (hardwareInputType == null)
            {
                // Create new hardware input type
                var newHardwareInputType = new HardwareInputType { Name = hardwareInputTypeName };
                hardwareInputType = _hardwareInputTypesRepository.AddHardwareInputType(newHardwareInputType);
            }
            
            return hardwareInputType;
        }
    }
} 