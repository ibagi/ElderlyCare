namespace ElderyCare.Data.Models
{
    public class FloorPlan : BaseModel
    {
        public string Name { get; set; }
        public string SvgUrl { get; set; }
        public bool IsDefault { get; set; }
    }
}
