using System;
using System.Collections.Generic;
using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Core.Services
{
    public class HardwareBoardService : IHardwareBoardService
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

        public HardwareBoardDto SaveHardwareBoard(HardwareBoardDto hardwareBoardDto)
        {
            var hardwareBoard = new HardwareBoard
            {
                Name = hardwareBoardDto.Name,
                Buses = new List<IOExtenderBus>()
            };
            for (var i = 0; i < hardwareBoardDto.HardwareBusExtendersCount; i++)
            {
                var extenderBus = new IOExtenderBus
                {
                    HardwareBus = (HardwareBus) i, Bits = new List<IOExtenderBit>(15)
                };

                extenderBus.Bits = new List<IOExtenderBit>();
                for (var j = 0; j <= 15; j++)
                {
                    extenderBus.Bits.Add(new IOExtenderBit()
                    {
                        HardwareBit = (HardwareBit) j
                    });
                }
                
                hardwareBoard.Buses.Add(extenderBus);
            }

            var savedHardwareBoard = _hardwareBoardRepository.SaveHardwareBoard(hardwareBoard);
            return _mapper.Map<HardwareBoard, HardwareBoardDto>(savedHardwareBoard);
        }
    }
}