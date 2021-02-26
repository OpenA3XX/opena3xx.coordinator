namespace OpenA3XX.Core.Models
{
    public class SimulatorEventLink
    {
        public int Id { get; set; }
        
        public int? SimulatorEventId { get; set; }
        
        public virtual SimulatorEvent SimulatorEvent { get; set; }
        
        public int? HardwareInputSelectorId { get; set; }
        
        public virtual HardwareInputSelector HardwareInputSelector { get; set; }
        
        public int? HardwareOutputSelectorId { get; set; }
        
        public virtual HardwareOutputSelector HardwareOutputSelector { get; set; }
    }
}