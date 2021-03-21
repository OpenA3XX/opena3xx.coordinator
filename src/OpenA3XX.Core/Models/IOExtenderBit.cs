namespace OpenA3XX.Core.Models
{
    public class IOExtenderBit
    {
        public int Id { get; set; }
        public HardwareBit HardwareBit { get; set; }

        public int IOExtenderBusId { get; set; }

        public virtual IOExtenderBus IOExtenderBus { get; set; }
        
        public ExtenderBusBitType ExtenderBusBitType { get; set; }
        
        public int? HardwareInputSelectorId { get; set; }
        public virtual HardwareInputSelector HardwareInputSelector { get; set; }

        public int? HardwareOutputSelectorId { get; set; }
        public virtual HardwareOutputSelector HardwareOutputSelector { get; set; }
    }
}