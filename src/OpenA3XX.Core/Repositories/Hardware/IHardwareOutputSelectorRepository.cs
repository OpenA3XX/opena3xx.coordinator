using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public interface IHardwareOutputSelectorRepository
    {
        IList<HardwareOutputSelector> GetAllOutputSelectors();
        HardwareOutputSelector GetHardwareOutputSelectorBy(int hardwareOutputSelectorId);

        HardwareOutputSelector AddHardwareOutputSelector(HardwareOutputSelector hardwareOutputSelector);

        HardwareOutputSelector UpdateHardwareOutputSelector(HardwareOutputSelector hardwareOutputSelector);

        void DeleteHardwareOutputSelector(int id);
    }
}