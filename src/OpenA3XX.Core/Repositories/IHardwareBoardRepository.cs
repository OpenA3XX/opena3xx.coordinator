using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwareBoardRepository
    {
        HardwareBoard AddHardwareBoard(HardwareBoard hardwareBoard);

        IList<HardwareBoard> GetAllHardwareBoards();

        HardwareBoard GetByHardwarePanel(int id);
    }
}