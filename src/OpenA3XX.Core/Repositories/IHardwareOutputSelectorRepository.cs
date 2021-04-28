using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwareOutputSelectorRepository
    {
        IList<HardwareOutputSelector> GetAllOutputSelectors();
        HardwareOutputSelector GetHardwareOutputSelectorBy(int hardwareOutputSelectorId);

        HardwareOutputSelector UpdateHardwareOutputSelector(HardwareOutputSelector hardwareOutputSelector);
    }
}