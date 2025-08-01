using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual IList<AircraftModel> AircraftModels { get; set; }
    }
}
