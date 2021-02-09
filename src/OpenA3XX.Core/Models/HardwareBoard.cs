using System;
using System.Collections.Generic;

namespace OpenA3XX.Core.Models
{
    public class HardwareBoard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<IOExtenderBus> Buses { get; set; }
    }

    public class IOExtenderBus
    {
        public int Id { get; set; }

        public int HardwareBoardId { get; set; }
        
        public HardwareBus HardwareBus { get; set; }

        public virtual HardwareBoard HardwareBoard { get; set; }

        public virtual IList<IOExtenderBit> Bits { get; set; }
    }

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

    public enum ExtenderBusBitType
    {
        Input,
        Output,
        NotSet
    }

    public enum HardwareBit
    {
        Bit0,
        Bit1,
        Bit2,
        Bit3,
        Bit4,
        Bit5,
        Bit6,
        Bit7,
        Bit8,
        Bit9,
        Bit10,
        Bit11,
        Bit12,
        Bit13,
        Bit14,
        Bit15
    }
    public enum HardwareBus
    {
        Bus0,
        Bus1,
        Bus2,
        Bus3,
        Bus4,
        Bus5,
        Bus6,
        Bus7,
    }
}