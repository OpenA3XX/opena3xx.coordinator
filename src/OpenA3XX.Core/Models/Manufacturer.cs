using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual IList<AircraftModel> AircraftModels { get; set; }
    }
}