using System;
using Humanizer;

namespace OpenA3XX.Core.Dtos
{
    public class HardwarePanelTokenDto
    {
        public string Name { get; set; }
        
        public string DeviceToken { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastSeen { get; set; }

        public string LastSeenText => $"{(DateTime.Now - LastSeen).Humanize()} ago";
    }
}