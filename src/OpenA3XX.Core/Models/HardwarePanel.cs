using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class HardwarePanel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual IList<HardwareInput> HardwareInput { get; set; }
        public virtual IList<HardwareOutput> HardwareOutput { get; set; }

        public int AircraftModelId { get; set; }
        public virtual AircraftModel AircraftModel { get; set; }

        public virtual CockpitArea CockpitArea { get; set; }

        public virtual HardwarePanelOwner HardwarePanelOwner { get; set; }
    }
}
