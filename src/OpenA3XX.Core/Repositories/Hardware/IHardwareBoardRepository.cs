using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public interface IHardwareBoardRepository
    {
        HardwareBoard AddHardwareBoard(HardwareBoard hardwareBoard);

        IList<HardwareBoard> GetAllHardwareBoards();

        HardwareBoard GetByHardwareBoard(int id);


        HardwareBoard SaveHardwareBoard(HardwareBoard hardwareBoard);

        HardwareBoard UpdateHardwareBoard(HardwareBoard hardwareBoard);
        void DeleteHardwareBoard(int id);
    }
}