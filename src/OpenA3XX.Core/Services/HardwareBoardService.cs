using System.Collections.Generic;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareBoardService: IHardwareBoardService
    {
        private readonly IHardwareBoardRepository _hardwareBoardRepository;
        private readonly IMapper _mapper;

        public HardwareBoardService(IHardwareBoardRepository hardwareBoardRepository, IMapper mapper)
        {
            _hardwareBoardRepository = hardwareBoardRepository;
            _mapper = mapper;
        }

        public IList<HardwareBoardDto> GetAllHardwareBoards()
        {
            var hardwareBoards = _hardwareBoardRepository.GetAllHardwareBoards();
            var hardwareBoardDtoList = _mapper.Map<IList<HardwareBoard>, IList<HardwareBoardDto>>(hardwareBoards);
            return hardwareBoardDtoList;
        }
    }
}