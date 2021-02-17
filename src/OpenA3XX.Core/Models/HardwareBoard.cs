using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class HardwareBoard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<IOExtenderBus> Buses { get; set; }
    }
}