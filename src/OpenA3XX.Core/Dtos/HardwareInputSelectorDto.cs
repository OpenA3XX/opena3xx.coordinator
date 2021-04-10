namespace OpenA3XX.Core.Dtos
{
    public class HardwareInputSelectorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsHardwareInputSelectorMappedWithHardware { get; set; }
        public SimulatorEventDto SimulatorEventDto { get; set; }
    }
}