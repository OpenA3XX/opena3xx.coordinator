using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class HardwareOutputType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
