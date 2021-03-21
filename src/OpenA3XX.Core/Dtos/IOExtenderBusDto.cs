using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    public class IOExtenderBusDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public IList<IOExtenderBitDto> IOExtenderBusBits { get; set; }
        
    }
}