namespace OpenA3XX.Core.Models
{
    public class SimulatorEvent
    {
        public int Id { get; set; }
        
        public string EventName { get; set; }
        
        public SimulatorEventType SimulatorEventType { get; set; }
        
        public SimulatorSoftware SimulatorSoftware { get; set; }
        
        public SimulatorEventSdkType SimulatorEventSdkType { get; set; }
        
        public string EventCode { get; set; }

    }
}