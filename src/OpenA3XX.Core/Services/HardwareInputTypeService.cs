using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareInputTypeService: IHardwareInputTypeService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IHardwareInputTypesRepository _hardwareInputTypesRepository;

        public HardwareInputTypeService(IHttpContextAccessor accessor, IHardwareInputTypesRepository hardwareInputTypesRepository, IMapper mapper)
        {
            _accessor = accessor;
            _hardwareInputTypesRepository = hardwareInputTypesRepository;
            _mapper = mapper;
        }

        public IList<HardwareInputTypeDto> GetAll()
        {
            var hardwareInputTypes = _hardwareInputTypesRepository.GetAllHardwareInputTypes();
            var hardwareInputTypesDtoList = _mapper.Map<IList<HardwareInputType>, IList<HardwareInputTypeDto>>(hardwareInputTypes);

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
            try
            {
                var hardwareInputType = _mapper.Map<HardwareInputTypeDto, HardwareInputType>(hardwareInputTypeDto);
                hardwareInputType = _hardwareInputTypesRepository.AddHardwareInputType(hardwareInputType);
                hardwareInputTypeDto = _mapper.Map<HardwareInputType, HardwareInputTypeDto>(hardwareInputType);
                return hardwareInputTypeDto;
            }
            catch (HardwareInputTypeExistsException e)
            {
                throw;
            }
            
        }

        public HardwareInputTypeDto Update(HardwareInputTypeDto hardwareInputTypeDto)
        {
            var hardwareInputType = _mapper.Map<HardwareInputTypeDto, HardwareInputType>(hardwareInputTypeDto);
            hardwareInputType = _hardwareInputTypesRepository.UpdateHardwareInputType(hardwareInputType);
            hardwareInputTypeDto = _mapper.Map<HardwareInputType, HardwareInputTypeDto>(hardwareInputType);
            return hardwareInputTypeDto;
        }
    }
}