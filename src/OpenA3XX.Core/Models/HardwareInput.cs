using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class HardwareInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual HardwareInputType HardwareInputType { get; set; }

        public IList<HardwareInputSelector> HardwareInputSelectorList { get; set; }

        public int HardwarePanelId { get; set; }
        public virtual HardwarePanel HardwarePanel { get; set; }
    }
}