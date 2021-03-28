using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class HardwarePanel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<HardwareInput> HardwareInput { get; set; }
        public virtual IList<HardwareOutput> HardwareOutput { get; set; }

        public int AircraftModelId { get; set; }
        public virtual AircraftModel AircraftModel { get; set; }

        public virtual CockpitArea CockpitArea { get; set; }

        public virtual HardwarePanelOwner HardwarePanelOwner { get; set; }
    }
}