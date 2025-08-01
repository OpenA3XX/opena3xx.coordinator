using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class SimulatorEvent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string FriendlyName { get; set; }

        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }

        public SimulatorEventType SimulatorEventType { get; set; }

        public SimulatorSoftware SimulatorSoftware { get; set; }

        public SimulatorEventSdkType SimulatorEventSdkType { get; set; }

        [MaxLength(50)]
        public string EventCode { get; set; }
    }
}
