using System;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// DTO representing a preset from the HubHop FS2020 presets API
    /// Maps to: https://hubhop-api-mgtm.azure-api.net/api/v1/fs2020/presets
    /// </summary>
    public class HubHopPresetDto
    {
        /// <summary>
        /// Full path identifier (e.g., "Black Square.Starship.Avionics.Input.BS Starship CDU 1 - 4 JKL Button")
        /// </summary>
        public string Path { get; set; } = string.Empty;
        
        /// <summary>
        /// Aircraft vendor/manufacturer (e.g., "Black Square", "Asobo", "IniBuilds")
        /// </summary>
        public string Vendor { get; set; } = string.Empty;
        
        /// <summary>
        /// Aircraft model (e.g., "Starship", "737 Max 2024", "A350 (2020)")
        /// </summary>
        public string Aircraft { get; set; } = string.Empty;
        
        /// <summary>
        /// System category (e.g., "Avionics", "Autopilot", "Engines", "Lights")
        /// </summary>
        public string System { get; set; } = string.Empty;
        
        /// <summary>
        /// SimConnect/MSFS code for the action (e.g., "(>H:CDU_4Button_1)", "(>K:LANDING_LIGHTS_ON)")
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Human-readable label for the control
        /// </summary>
        public string Label { get; set; } = string.Empty;
        
        /// <summary>
        /// Type of preset - "Input" for controls, "Output" for indicators
        /// </summary>
        public string PresetType { get; set; } = string.Empty;
        
        /// <summary>
        /// Current status of the preset ("Submitted", "Updated", etc.)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// When this preset was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Author who created this preset
        /// </summary>
        public string Author { get; set; } = string.Empty;
        
        /// <summary>
        /// Who last updated this preset (optional)
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Description or additional notes
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Version number of this preset
        /// </summary>
        public int Version { get; set; }
        
        /// <summary>
        /// Unique identifier from HubHop API
        /// </summary>
        public string Id { get; set; } = string.Empty;
    }
} 