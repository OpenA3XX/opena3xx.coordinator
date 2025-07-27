using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OpenA3XX.Core.Models
{
    public class AircraftModel
    {
        public int Id { get; set; }

        public string Model { get; set; }
        
        public string Type { get; set; }
        
        public string Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual IList<HardwarePanel> HardwarePanels { get; set; }
    }
}