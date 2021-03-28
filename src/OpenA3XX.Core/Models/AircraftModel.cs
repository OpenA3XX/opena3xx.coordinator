using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OpenA3XX.Core.Models
{
    public class AircraftModel
    {
        public int Id { get; set; }

        public string Model { get; set; }
        
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual IList<HardwarePanel> HardwarePanels { get; set; }
    }
}