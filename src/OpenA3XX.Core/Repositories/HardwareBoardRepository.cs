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
    }

    public interface IHardwareBoardRepository
    {
        HardwareBoard AddHardwareBoard(HardwareBoard hardwareBoard);
    }
}