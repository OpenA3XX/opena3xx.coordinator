using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class IOExtenderBus
    {
        public int Id { get; set; }

        public int HardwareBoardId { get; set; }

        public HardwareBus HardwareBus { get; set; }

        public virtual HardwareBoard HardwareBoard { get; set; }

        public virtual IList<IOExtenderBit> Bits { get; set; }
    }
}