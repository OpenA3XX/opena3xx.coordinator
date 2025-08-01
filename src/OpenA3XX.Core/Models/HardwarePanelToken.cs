using System;
using System.ComponentModel.DataAnnotations;

namespace OpenA3XX.Core.Models
{
    public class HardwarePanelToken
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeviceToken { get; set; }

        public DateTime CreatedDateTime { get; set; }

        [MaxLength(45)]
        public string DeviceIpAddress { get; set; }

        public int HardwarePanelId { get; set; }

        public HardwarePanel HardwarePanel { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
