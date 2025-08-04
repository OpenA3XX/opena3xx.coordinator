using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Hardware;

namespace OpenA3XX.Core.Services.Hardware
{
    public class HardwareOutputTypeService : IHardwareOutputTypeService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHardwareOutputTypesRepository _hardwareOutputTypesRepository;
        private readonly IMapper _mapper;

        public HardwareOutputTypeService(IHttpContextAccessor accessor,
            IHardwareOutputTypesRepository hardwareOutputTypesRepository, IMapper mapper)
        {
            _accessor = accessor;
            _hardwareOutputTypesRepository = hardwareOutputTypesRepository;
            _mapper = mapper;
        }

        public IList<HardwareOutputTypeDto> GetAll()
        {
            var hardwareOutputTypes = _hardwareOutputTypesRepository.GetAllHardwareOutputTypes();
            var hardwareOutputTypesDtoList =
                _mapper.Map<IList<HardwareOutputType>, IList<HardwareOutputTypeDto>>(hardwareOutputTypes);

            return hardwareOutputTypesDtoList;
        }

        public HardwareOutputTypeDto GetBy(int id)
        {
            var hardwareOutputType = _hardwareOutputTypesRepository.GetHardwareOutputTypeBy(id);
            var hardwareOutputTypeDto = _mapper.Map<HardwareOutputType, HardwareOutputTypeDto>(hardwareOutputType);
            return hardwareOutputTypeDto;
        }

        public HardwareOutputTypeDto Add(HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            var hardwareOutputType = _mapper.Map<HardwareOutputTypeDto, HardwareOutputType>(hardwareOutputTypeDto);
            hardwareOutputType = _hardwareOutputTypesRepository.AddHardwareOutputType(hardwareOutputType);
            hardwareOutputTypeDto = _mapper.Map<HardwareOutputType, HardwareOutputTypeDto>(hardwareOutputType);
            return hardwareOutputTypeDto;
        }

        public HardwareOutputTypeDto Update(HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            var hardwareOutputType = _mapper.Map<HardwareOutputTypeDto, HardwareOutputType>(hardwareOutputTypeDto);
            hardwareOutputType = _hardwareOutputTypesRepository.UpdateHardwareOutputType(hardwareOutputType);
            hardwareOutputTypeDto = _mapper.Map<HardwareOutputType, HardwareOutputTypeDto>(hardwareOutputType);
            return hardwareOutputTypeDto;
        }

        /// <summary>
        /// Deletes a hardware output type by its ID
        /// </summary>
        /// <param name="id">The hardware output type ID to delete</param>
        public void Delete(int id)
        {
            _hardwareOutputTypesRepository.DeleteHardwareOutputType(id);
        }
    }
}