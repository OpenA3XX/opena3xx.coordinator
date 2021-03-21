using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    public class HardwareBoardDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public IList<IOExtenderBusDto> IOExtenderBuses { get; set; }
        
    }
}