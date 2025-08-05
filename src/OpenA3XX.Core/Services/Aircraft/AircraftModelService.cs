using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Aircraft;

namespace OpenA3XX.Core.Services.Aircraft
{
    public class AircraftModelService : IAircraftModelService
    {
        private readonly IAircraftModelRepository _aircraftModelRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AircraftModelService> _logger;

        public AircraftModelService(
            IAircraftModelRepository aircraftModelRepository,
            IManufacturerRepository manufacturerRepository,
            IMapper mapper,
            ILogger<AircraftModelService> logger)
        {
            _aircraftModelRepository = aircraftModelRepository;
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public IList<AircraftModelDto> GetAllAircraftModels()
        {
            _logger.LogInformation("Getting all aircraft models");
            
            var aircraftModels = _aircraftModelRepository.GetAll();
            var aircraftModelDtos = _mapper.Map<IList<AircraftModel>, IList<AircraftModelDto>>(aircraftModels);
            
            _logger.LogInformation("Retrieved {Count} aircraft models", aircraftModelDtos.Count);
            
            return aircraftModelDtos;
        }

        public IList<AircraftModelDto> GetActiveAircraftModels()
        {
            _logger.LogInformation("Getting active aircraft models");
            
            var aircraftModels = _aircraftModelRepository.GetActive();
            var aircraftModelDtos = _mapper.Map<IList<AircraftModel>, IList<AircraftModelDto>>(aircraftModels);
            
            _logger.LogInformation("Retrieved {Count} active aircraft models", aircraftModelDtos.Count);
            
            return aircraftModelDtos;
        }

        public AircraftModelDto GetAircraftModelById(int id)
        {
            _logger.LogInformation("Getting aircraft model by ID: {AircraftModelId}", id);
            
            var aircraftModel = _aircraftModelRepository.GetById(id);
            if (aircraftModel == null)
            {
                _logger.LogWarning("Aircraft model with ID {AircraftModelId} not found", id);
                return null;
            }
            
            var aircraftModelDto = _mapper.Map<AircraftModel, AircraftModelDto>(aircraftModel);
            
            _logger.LogInformation("Successfully retrieved aircraft model with ID {AircraftModelId}", id);
            
            return aircraftModelDto;
        }

        public AircraftModelDto AddAircraftModel(AddAircraftModelDto aircraftModelDto)
        {
            _logger.LogInformation("Adding new aircraft model: {ModelName}", aircraftModelDto.Name);
            
            // Find or create manufacturer
            var manufacturer = FindOrCreateManufacturer(aircraftModelDto.Manufacturer);
            
            var aircraftModel = _mapper.Map<AddAircraftModelDto, AircraftModel>(aircraftModelDto);
            aircraftModel.ManufacturerId = manufacturer.Id;
            
            var addedAircraftModel = _aircraftModelRepository.Add(aircraftModel);
            var result = _mapper.Map<AircraftModel, AircraftModelDto>(addedAircraftModel);
            
            _logger.LogInformation("Successfully added aircraft model with ID {AircraftModelId}", result.Id);
            
            return result;
        }

        public AircraftModelDto UpdateAircraftModel(int id, UpdateAircraftModelDto aircraftModelDto)
        {
            _logger.LogInformation("Updating aircraft model with ID: {AircraftModelId}", id);
            
            var existingAircraftModel = _aircraftModelRepository.GetById(id);
            if (existingAircraftModel == null)
            {
                _logger.LogWarning("Aircraft model with ID {AircraftModelId} not found for update", id);
                return null;
            }
            
            // Find or create manufacturer
            var manufacturer = FindOrCreateManufacturer(aircraftModelDto.Manufacturer);
            
            var aircraftModel = _mapper.Map<UpdateAircraftModelDto, AircraftModel>(aircraftModelDto);
            aircraftModel.Id = id;
            aircraftModel.ManufacturerId = manufacturer.Id;
            aircraftModel.CreatedAt = existingAircraftModel.CreatedAt; // Preserve original creation date
            
            var updatedAircraftModel = _aircraftModelRepository.Update(aircraftModel);
            var result = _mapper.Map<AircraftModel, AircraftModelDto>(updatedAircraftModel);
            
            _logger.LogInformation("Successfully updated aircraft model with ID {AircraftModelId}", result.Id);
            
            return result;
        }

        public void DeleteAircraftModel(int id)
        {
            _logger.LogInformation("Deleting aircraft model with ID: {AircraftModelId}", id);
            
            _aircraftModelRepository.Delete(id);
            
            _logger.LogInformation("Successfully deleted aircraft model with ID {AircraftModelId}", id);
        }

        private Manufacturer FindOrCreateManufacturer(string manufacturerName)
        {
            var manufacturers = _manufacturerRepository.GetAllManufacturers();
            var manufacturer = manufacturers.FirstOrDefault(m => m.Name.Equals(manufacturerName, StringComparison.OrdinalIgnoreCase));
            
            if (manufacturer == null)
            {
                _logger.LogInformation("Creating new manufacturer: {ManufacturerName}", manufacturerName);
                manufacturer = new Manufacturer { Name = manufacturerName };
                manufacturer = _manufacturerRepository.Add(manufacturer);
            }
            
            return manufacturer;
        }
    }
} 