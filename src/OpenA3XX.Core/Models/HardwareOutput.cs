using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class HardwareOutput
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual HardwareOutputType HardwareOutputType { get; set; }

        public IList<HardwareOutputSelector> HardwareOutputSelectorList { get; set; }
        
        public int HardwarePanelId { get; set; }

        public virtual HardwarePanel HardwarePanel { get; set; }
    }
}
