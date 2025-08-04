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
    /// Service for managing HardwareOutput business logic
    /// </summary>
    public class HardwareOutputService : IHardwareOutputService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareOutputRepository _hardwareOutputRepository;
        private readonly IHardwareOutputTypesRepository _hardwareOutputTypesRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the HardwareOutputService
        /// </summary>
        /// <param name="accessor">HTTP context accessor</param>
        /// <param name="hardwareOutputRepository">Hardware output repository</param>
        /// <param name="hardwareOutputTypesRepository">Hardware output types repository</param>
        /// <param name="mapper">AutoMapper instance</param>
        public HardwareOutputService(IHttpContextAccessor accessor,
            IHardwareOutputRepository hardwareOutputRepository,
            IHardwareOutputTypesRepository hardwareOutputTypesRepository,
            IMapper mapper)
        {
            _accessor = accessor;
            _hardwareOutputRepository = hardwareOutputRepository;
            _hardwareOutputTypesRepository = hardwareOutputTypesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all hardware outputs
        /// </summary>
        /// <returns>List of all hardware outputs</returns>
        public IList<HardwareOutputDto> GetAll()
        {
            var hardwareOutputs = _hardwareOutputRepository.GetAllHardwareOutputs();
            var hardwareOutputDtoList = _mapper.Map<IList<HardwareOutput>, IList<HardwareOutputDto>>(hardwareOutputs);
            return hardwareOutputDtoList;
        }

        /// <summary>
        /// Gets hardware outputs by panel ID
        /// </summary>
        /// <param name="panelId">The panel ID to filter by</param>
        /// <returns>List of hardware outputs for the specified panel</returns>
        public IList<HardwareOutputDto> GetByPanelId(int panelId)
        {
            var hardwareOutputs = _hardwareOutputRepository.GetHardwareOutputsByPanelId(panelId);
            var hardwareOutputDtoList = _mapper.Map<IList<HardwareOutput>, IList<HardwareOutputDto>>(hardwareOutputs);
            return hardwareOutputDtoList;
        }

        /// <summary>
        /// Gets a specific hardware output by ID
        /// </summary>
        /// <param name="id">The hardware output ID</param>
        /// <returns>The hardware output or null if not found</returns>
        public HardwareOutputDto GetBy(int id)
        {
            var hardwareOutput = _hardwareOutputRepository.GetHardwareOutputBy(id);
            if (hardwareOutput == null)
            {
                return null;
            }
            
            var hardwareOutputDto = _mapper.Map<HardwareOutput, HardwareOutputDto>(hardwareOutput);
            return hardwareOutputDto;
        }

        /// <summary>
        /// Adds a new hardware output
        /// </summary>
        /// <param name="hardwareOutputDto">The hardware output data to add</param>
        /// <returns>The added hardware output</returns>
        public HardwareOutputDto Add(HardwareOutputDto hardwareOutputDto)
        {
            // Find or create hardware output type if specified
            if (!string.IsNullOrEmpty(hardwareOutputDto.HardwareOutputType))
            {
                var hardwareOutputType = FindOrCreateHardwareOutputType(hardwareOutputDto.HardwareOutputType);
                hardwareOutputDto.HardwareOutputTypeId = hardwareOutputType.Id;
            }
            
            var hardwareOutput = _mapper.Map<HardwareOutputDto, HardwareOutput>(hardwareOutputDto);
            hardwareOutput = _hardwareOutputRepository.AddHardwareOutput(hardwareOutput);
            hardwareOutputDto = _mapper.Map<HardwareOutput, HardwareOutputDto>(hardwareOutput);
            return hardwareOutputDto;
        }

        /// <summary>
        /// Updates an existing hardware output
        /// </summary>
        /// <param name="hardwareOutputDto">The hardware output data to update</param>
        /// <returns>The updated hardware output</returns>
        public HardwareOutputDto Update(HardwareOutputDto hardwareOutputDto)
        {
            var existingHardwareOutput = _hardwareOutputRepository.GetHardwareOutputBy(hardwareOutputDto.Id);
            if (existingHardwareOutput == null)
            {
                throw new EntityNotFoundException("HardwareOutput", hardwareOutputDto.Id);
            }

            // Find or create hardware output type if specified
            if (!string.IsNullOrEmpty(hardwareOutputDto.HardwareOutputType))
            {
                var hardwareOutputType = FindOrCreateHardwareOutputType(hardwareOutputDto.HardwareOutputType);
                hardwareOutputDto.HardwareOutputTypeId = hardwareOutputType.Id;
            }

            var hardwareOutput = _mapper.Map<HardwareOutputDto, HardwareOutput>(hardwareOutputDto);
            hardwareOutput = _hardwareOutputRepository.UpdateHardwareOutput(hardwareOutput);
            hardwareOutputDto = _mapper.Map<HardwareOutput, HardwareOutputDto>(hardwareOutput);
            return hardwareOutputDto;
        }

        /// <summary>
        /// Deletes a hardware output by its ID
        /// </summary>
        /// <param name="id">The hardware output ID to delete</param>
        public void Delete(int id)
        {
            // Note: With the cascade delete configuration in CoreDataContext,
            // related HardwareOutputSelector records will be automatically deleted
            _hardwareOutputRepository.DeleteHardwareOutput(id);
        }

        /// <summary>
        /// Finds or creates a hardware output type by name
        /// </summary>
        /// <param name="hardwareOutputTypeName">The name of the hardware output type</param>
        /// <returns>The hardware output type</returns>
        private HardwareOutputType FindOrCreateHardwareOutputType(string hardwareOutputTypeName)
        {
            var hardwareOutputTypes = _hardwareOutputTypesRepository.GetAllHardwareOutputTypes();
            var hardwareOutputType = hardwareOutputTypes.FirstOrDefault(t => t.Name.Equals(hardwareOutputTypeName, global::System.StringComparison.OrdinalIgnoreCase));
            
            if (hardwareOutputType == null)
            {
                // Create new hardware output type
                var newHardwareOutputType = new HardwareOutputType { Name = hardwareOutputTypeName };
                hardwareOutputType = _hardwareOutputTypesRepository.AddHardwareOutputType(newHardwareOutputType);
            }
            
            return hardwareOutputType;
        }
    }
} 