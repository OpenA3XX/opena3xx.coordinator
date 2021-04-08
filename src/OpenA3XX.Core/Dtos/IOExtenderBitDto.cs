namespace OpenA3XX.Core.Dtos
{
    public class IOExtenderBitDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int HardwareInputSelectorId { get; set; }
        public string HardwareInputSelectorFullName { get; set; }
        
        public int HardwareOutputSelectorId { get; set; }
        public string HardwareOutputSelectorFullName { get; set; }
    }
}