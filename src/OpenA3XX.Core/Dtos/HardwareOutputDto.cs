using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    public class HardwareOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HardwareOutputType { get; set; }

        public int? HardwareOutputTypeId { get; set; }

        public int HardwarePanelId { get; set; }

        public IList<HardwareOutputSelectorDto> HardwareOutputSelectors { get; set; }
    }
}