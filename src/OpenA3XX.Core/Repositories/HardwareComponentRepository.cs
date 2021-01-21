using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareComponentRepository : BaseRepository<HardwareComponent>, IHardwareComponentRepository
    {
        public HardwareComponentRepository(DbContext context) : base(context)
        {
        }

        public IList<HardwareComponent> GetAllHardwareComponents()
        {
            return GetAll()
                
                .Include(c => c.HardwareInput)
                .ThenInclude(c=>c.HardwareInputType)
                .Include(c => c.HardwareOutput)
                .ThenInclude(c=>c.HardwareOutputType)
                .ToList();
        }

        public HardwareComponent AddHardwareComponent(HardwareComponent hardwareComponent)
        {
            return Add(hardwareComponent);
        }
    }
}