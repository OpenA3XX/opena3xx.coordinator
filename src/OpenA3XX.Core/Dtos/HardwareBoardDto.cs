namespace OpenA3XX.Core.Dtos
{
    public class HardwareBoardDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int HardwareBusExtendersCount { get; set; }

        public int TotalInputOutputs => HardwareBusExtendersCount * 16;
    }
}