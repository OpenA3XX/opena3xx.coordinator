using System;
using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class HardwareComponent
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string InternalId { get; set; }
        
        public virtual IList<HardwareInput> HardwareInput { get; set; }
        
        public virtual IList<HardwareOutput> HardwareOutput { get; set; }
    }
}