using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface IHardwareComponentRepository
    {
        public IList<HardwareComponent> GetAllHardwareComponents();

        HardwareComponent AddHardwareComponent(HardwareComponent hardwareComponent);
    }
}