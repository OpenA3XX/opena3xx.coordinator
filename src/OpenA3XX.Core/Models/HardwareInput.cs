using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class HardwareInput
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public int? HardwareInputTypeId { get; set; }
        public virtual HardwareInputType HardwareInputType { get; set; }

        public IList<HardwareInputSelector> HardwareInputSelectorList { get; set; }

        public int HardwarePanelId { get; set; }
        public virtual HardwarePanel HardwarePanel { get; set; }
    }
}
