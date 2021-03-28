namespace OpenA3XX.Core.Dtos
{
    public class AddHardwarePanelDto
    {
        public string HardwarePanelName { get; set; }

        public int AircraftModel { get; set; }

        public int CockpitArea { get; set; }

        public int HardwarePanelOwner { get; set; }
    }
}