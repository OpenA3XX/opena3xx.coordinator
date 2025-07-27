namespace OpenA3XX.Core.Dtos
{
    public class AddAircraftModelDto
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
} 