namespace OpenA3XX.Core.Models
{
    public class HardwareOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual HardwareOutputType HardwareOutputType { get; set; }

        public int? State { get; set; }
    }
}