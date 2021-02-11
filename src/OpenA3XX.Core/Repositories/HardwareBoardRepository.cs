using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenA3XX.Core.Models;

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
                .ThenInclude(c => c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .ToList();
        }

        public HardwareBoard GetByHardwarePanel(int id)
        {
            return FindBy(c => c.Id == id)
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareInput)
                .ThenInclude(c => c.HardwareInputType)
                .Include(c => c.Buses)
                .ThenInclude(c => c.Bits)
                .ThenInclude(c => c.HardwareOutput)
                .ThenInclude(c => c.HardwareOutputType)
                .First();
        }
    }

    public interface IHardwareBoardRepository
    {
        HardwareBoard AddHardwareBoard(HardwareBoard hardwareBoard);

        IList<HardwareBoard> GetAllHardwareBoards();

        HardwareBoard GetByHardwarePanel(int id);
    }
}