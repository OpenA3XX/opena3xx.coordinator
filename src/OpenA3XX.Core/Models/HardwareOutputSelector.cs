using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class HardwareOutputSelector
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int HardwareOutputId { get; set; }

        public virtual HardwareOutput HardwareOutput { get; set; }
    }
}
