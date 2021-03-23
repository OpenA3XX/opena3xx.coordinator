namespace OpenA3XX.Core.Models
{
    public class HardwareInputSelector
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HardwareInputId { get; set; }

        public virtual HardwareInput HardwareInput { get; set; }

        public SimulatorEvent SimulatorEvent { get; set; }
        
    }
}