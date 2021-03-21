using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories.Base;

namespace OpenA3XX.Core.Repositories
{
    public class HardwareBoardRepository : BaseRepository<HardwareBoard>, IHardwareBoardRepository
    {
        public HardwareBoardRepository(DbContext context) : base(context)
        {
        }

        public HardwareBoard AddHardwareBoard(HardwareBoard hardwareBoard)
        {
            return Add(hardwareBoard);
        }

        public IList<HardwareBoard> GetAllHardwareBoards()
        {
            return GetAll()
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareInputSelector)
                .ThenInclude(c=>c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareOutputSelector)
                .ThenInclude(c=>c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .Include(c=>c.Buses)
                .ThenInclude(c=>c.Bits)
                .ThenInclude(c=>c.HardwareInputSelector)
                .ThenInclude(c=>c.HardwareInput)
                .ThenInclude(c=>c.HardwarePanel)
                .Include(c=>c.Buses)
                .ThenInclude(c=>c.Bits)
                .ThenInclude(c=>c.HardwareOutputSelector)
                .ThenInclude(c=>c.HardwareOutput)
                .ThenInclude(c=>c.HardwarePanel)
                .ToList();
        }

        public HardwareBoard GetByHardwareBoard(int id)
        {
            return FindBy(c => c.Id == id)
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareInputSelector)
                .ThenInclude(c=>c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareOutputSelector)
                .ThenInclude(c=>c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .Include(c=>c.Buses)
                .ThenInclude(c=>c.Bits)
                .ThenInclude(c=>c.HardwareInputSelector)
                .ThenInclude(c=>c.HardwareInput)
                .ThenInclude(c=>c.HardwarePanel)
                .Include(c=>c.Buses)
                .ThenInclude(c=>c.Bits)
                .ThenInclude(c=>c.HardwareOutputSelector)
                .ThenInclude(c=>c.HardwareOutput)
                .ThenInclude(c=>c.HardwarePanel)
                .First();
        }

        public HardwareBoard SaveHardwareBoard(HardwareBoard hardwareBoard)
        {
            return Add(hardwareBoard);
        }
    }
}