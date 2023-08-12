namespace ElderyCare.Data.Entities
{
    public class FloorPlan : EntityBase
    {
        public string Name { get; set; }
        public string SvgUrl { get; set; }
        public bool IsDefault { get; set; }
    }
}
