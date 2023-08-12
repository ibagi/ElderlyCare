namespace ElderyCare.Data.Entities
{
    public class Robot : EntityBase
    {
        public string Name { get; set; }
        public string OpcUrl { get; set; }
        public string OpcId { get; set; }

        public bool HasOpcConnection()
        {
            return !string.IsNullOrEmpty(OpcUrl) && !string.IsNullOrEmpty(OpcId);
        }
    }
}