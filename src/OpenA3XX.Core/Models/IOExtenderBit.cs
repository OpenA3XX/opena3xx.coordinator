namespace OpenA3XX.Core.Models
{
    public class IOExtenderBit
    {
        public int Id { get; set; }

        public HardwareBit HardwareBit { get; set; }

        public int IOExtenderBusId { get; set; }

        public virtual IOExtenderBus IOExtenderBus { get; set; }
        public ExtenderBusBitType ExtenderBusBitType { get; set; }
        public int? HardwareInputId { get; set; }

        public virtual HardwareInput HardwareInput { get; set; }

        public int? HardwareOutputId { get; set; }

        public virtual HardwareOutput HardwareOutput { get; set; }
    }
}