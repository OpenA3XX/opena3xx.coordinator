namespace OpenA3XX.Core.Dtos
{
    public class HardwareOutputSelectorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsHardwareOutputSelectorMappedWithHardware { get; set; }

        public int HardwareOutputId { get; set; }

        public SimulatorEventDto SimulatorEventDto { get; set; }
    }
}