using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Hardware;

namespace OpenA3XX.Core.Services.Hardware
{
    public class HardwareInputTypeService : IHardwareInputTypeService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareInputTypesRepository _hardwareInputTypesRepository;
        private readonly IMapper _mapper;

        public HardwareInputTypeService(IHttpContextAccessor accessor,
            IHardwareInputTypesRepository hardwareInputTypesRepository, IMapper mapper)
        {
            _accessor = accessor;
            _hardwareInputTypesRepository = hardwareInputTypesRepository;
            _mapper = mapper;
        }

        public IList<HardwareInputTypeDto> GetAll()
        {
            var hardwareInputTypes = _hardwareInputTypesRepository.GetAllHardwareInputTypes();
            var hardwareInputTypesDtoList =
                _mapper.Map<IList<HardwareInputType>, IList<HardwareInputTypeDto>>(hardwareInputTypes);

            return hardwareInputTypesDtoList;
        }

        public HardwareInputTypeDto GetBy(int id)
        {
            var hardwareInputType = _hardwareInputTypesRepository.GetHardwareInputTypeBy(id);
            var hardwareInputTypeDto = _mapper.Map<HardwareInputType, HardwareInputTypeDto>(hardwareInputType);
            return hardwareInputTypeDto;
        }

        public HardwareInputTypeDto Add(HardwareInputTypeDto hardwareInputTypeDto)
        {
            var hardwareInputType = _mapper.Map<HardwareInputTypeDto, HardwareInputType>(hardwareInputTypeDto);
            hardwareInputType = _hardwareInputTypesRepository.AddHardwareInputType(hardwareInputType);
            hardwareInputTypeDto = _mapper.Map<HardwareInputType, HardwareInputTypeDto>(hardwareInputType);
            return hardwareInputTypeDto;
        }

        public HardwareInputTypeDto Update(HardwareInputTypeDto hardwareInputTypeDto)
        {
            var hardwareInputType = _mapper.Map<HardwareInputTypeDto, HardwareInputType>(hardwareInputTypeDto);
            hardwareInputType = _hardwareInputTypesRepository.UpdateHardwareInputType(hardwareInputType);
            hardwareInputTypeDto = _mapper.Map<HardwareInputType, HardwareInputTypeDto>(hardwareInputType);
            return hardwareInputTypeDto;
        }

        /// <summary>
        /// Deletes a hardware input type by its ID
        /// </summary>
        /// <param name="id">The hardware input type ID to delete</param>
        public void Delete(int id)
        {
            _hardwareInputTypesRepository.DeleteHardwareInputType(id);
        }
    }
}