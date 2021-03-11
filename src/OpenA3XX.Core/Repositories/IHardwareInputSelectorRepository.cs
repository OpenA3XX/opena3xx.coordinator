using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwareInputSelectorRepository
    {
        IList<HardwareInputSelector> GetAllInputSelectors();
        HardwareInputSelector GetHardwareInputSelectorBy(int hardwareInputSelectorId);

        HardwareInputSelector UpdateHardwareInputSelector(HardwareInputSelector hardwareInputSelector);
    }
}