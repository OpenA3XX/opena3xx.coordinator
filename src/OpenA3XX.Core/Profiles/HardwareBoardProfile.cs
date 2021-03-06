﻿using AutoMapper;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Profiles
{
    public class HardwareBoardProfile : Profile
    {
        public HardwareBoardProfile()
        {
            CreateMap<HardwareBoard, HardwareBoardDto>();
        }
    }
}