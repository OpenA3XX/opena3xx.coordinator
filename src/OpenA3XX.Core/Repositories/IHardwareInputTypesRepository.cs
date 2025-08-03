using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwareInputTypesRepository
    {
        IList<HardwareInputType> GetAllHardwareInputTypes();

        HardwareInputType GetHardwareInputTypeBy(int id);

        HardwareInputType AddHardwareInputType(HardwareInputType hardwareInputType);

        HardwareInputType UpdateHardwareInputType(HardwareInputType hardwareInputType);
        void DeleteHardwareInputType(int id);
    }
}