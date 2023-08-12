using System.Text.Json;

namespace ElderyCare.Data.Entities
{
    public class RobotState : EntityBase
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
