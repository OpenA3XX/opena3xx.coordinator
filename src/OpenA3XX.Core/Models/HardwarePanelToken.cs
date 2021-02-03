using System;

namespace OpenA3XX.Core.Models
{
    public class HardwarePanelToken
    {
        public int Id { get; set; }
        
        public string DeviceToken { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
        
        public string DeviceIpAddress { get; set; }
        
        public int HardwarePanelId { get; set; }
        
        public HardwarePanel HardwarePanel { get; set; }
        
        
        public DateTime LastSeen { get; set; }
    }
}