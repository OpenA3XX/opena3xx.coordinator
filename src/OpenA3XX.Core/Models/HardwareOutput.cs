using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class HardwareOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual HardwareOutputType HardwareOutputType { get; set; }

        public IList<HardwareOutputSelector> HardwareOutputSelectorList { get; set; }
    }
}