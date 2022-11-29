using System.Text.Json;

namespace ElderyCare.Data.Models
{
    public class RobotState : BaseModel
    {
        public Guid RobotId { get; set; }
        public string RobotName { get; set; }
        public string State { get; set; }

        public RobotState()
        {

        }

        public RobotState(Guid robotId, string robotName)
        {
            RobotId = robotId;
            RobotName = robotName;
        }

        public void UpdateState(object state)
        {
            if (State != null)
            {
                Updated = DateTime.Now;
            }

            State = JsonSerializer.Serialize(state);
        }
    }
}
