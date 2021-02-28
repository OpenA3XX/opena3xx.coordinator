using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Dtos
{
    public class SimulatorEventDto
    {
        public int Id { get; set; }
        
        public string FriendlyName { get; set; }
        
        public string EventName { get; set; }
        
        public SimulatorEventType SimulatorEventType { get; set; }

        public string SimulatorEventTypeName { get; set; }
    
        public SimulatorSoftware SimulatorSoftware { get; set; }
        
        public string SimulatorSoftwareName { get; set; }
        
        public SimulatorEventSdkType SimulatorEventSdkType { get; set; }
        
        public string SimulatorEventSdkTypeName { get; set; }
        
        public string EventCode { get; set; }
    }
}