using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    public class HardwarePanelOverviewDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string AircraftModel { get; set; }
        
        public string Manufacturer { get; set; }

        public string CockpitArea { get; set; }
        
        public string Owner { get; set; }

    }

    public class HardwarePanelDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string AircraftModel { get; set; }
        
        public string Manufacturer { get; set; }

        public string CockpitArea { get; set; }
        
        public string Owner { get; set; }
        
        public int TotalInputs { get; set; }
        
        public int TotalOutputs { get; set; }
        
        public IList<HardwareInputDto> HardwareInputs { get; set; }
        
        public IList<HardwareOutputDto> HardwareOutputs { get; set; }
        
        
    }
}