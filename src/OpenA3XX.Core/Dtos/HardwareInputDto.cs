using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    public class HardwareInputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HardwareInputType { get; set; }

        public int HardwarePanelId { get; set; }

        public IList<HardwareInputSelectorDto> HardwareInputSelectors { get; set; }
        
    }
}