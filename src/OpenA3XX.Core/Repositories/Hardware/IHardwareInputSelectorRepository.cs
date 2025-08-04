using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories.Hardware
{
    public interface IHardwareInputSelectorRepository
    {
        IList<HardwareInputSelector> GetAllInputSelectors();
        HardwareInputSelector GetHardwareInputSelectorBy(int hardwareInputSelectorId);

        HardwareInputSelector AddHardwareInputSelector(HardwareInputSelector hardwareInputSelector);
        HardwareInputSelector UpdateHardwareInputSelector(HardwareInputSelector hardwareInputSelector);
        void DeleteHardwareInputSelector(int id);
    }
}