using System;
using OpenA3XX.Core.Enums;

namespace OpenA3XX.Core.Models
{
    public class HardwareEvent
    {
        public string Origin { get; set; }
        
        public DateTime DateOccured { get; set; }
        
        public CockpitComponent CockpitComponent { get; set; }
    }
}