namespace OpenA3XX.Core.Models
{
    public class HardwareInput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual HardwareInputType HardwareInputType { get; set; }

        public int? State { get; set; }
    }
}