using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwareOutputTypesRepository
    {
        IList<HardwareOutputType> GetAllHardwareOutputTypes();

        HardwareOutputType GetHardwareOutputTypeBy(int id);

        HardwareOutputType AddHardwareOutputType(HardwareOutputType hardwareOutputType);

        HardwareOutputType UpdateHardwareOutputType(HardwareOutputType hardwareOutputType);
    }
}