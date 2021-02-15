using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareOutputTypeService: IHardwareOutputTypeService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IHardwareOutputTypesRepository _hardwareOutputTypesRepository;

        public HardwareOutputTypeService(IHttpContextAccessor accessor, IHardwareOutputTypesRepository hardwareOutputTypesRepository, IMapper mapper)
        {
            _accessor = accessor;
            _hardwareOutputTypesRepository = hardwareOutputTypesRepository;
            _mapper = mapper;
        }

        public IList<HardwareOutputTypeDto> GetAll()
        {
            var hardwareOutputTypes = _hardwareOutputTypesRepository.GetAllHardwareOutputTypes();
            var hardwareOutputTypesDtoList = _mapper.Map<IList<HardwareOutputType>, IList<HardwareOutputTypeDto>>(hardwareOutputTypes);

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
            try
            {
                var hardwareOutputType = _mapper.Map<HardwareOutputTypeDto, HardwareOutputType>(hardwareOutputTypeDto);
                hardwareOutputType = _hardwareOutputTypesRepository.AddHardwareOutputType(hardwareOutputType);
                hardwareOutputTypeDto = _mapper.Map<HardwareOutputType, HardwareOutputTypeDto>(hardwareOutputType);
                return hardwareOutputTypeDto;
            }
            catch (HardwareOutputTypeExistsException e)
            {
                throw;
            }
            
        }

        public HardwareOutputTypeDto Update(HardwareOutputTypeDto hardwareOutputTypeDto)
        {
            var hardwareOutputType = _mapper.Map<HardwareOutputTypeDto, HardwareOutputType>(hardwareOutputTypeDto);
            hardwareOutputType = _hardwareOutputTypesRepository.UpdateHardwareOutputType(hardwareOutputType);
            hardwareOutputTypeDto = _mapper.Map<HardwareOutputType, HardwareOutputTypeDto>(hardwareOutputType);
            return hardwareOutputTypeDto;
        }
    }
}