namespace OpenA3XX.Core.Models
{
    public class HardwareComponent
    {
        public virtual HardwarePanel HardwarePanel { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string InternalId { get; set; }
    }
}