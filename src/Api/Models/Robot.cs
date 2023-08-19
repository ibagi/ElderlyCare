using ElderlyCare.Domain.Robot;

namespace ElderlyCare.Api.Features.Robot
{
    public class Robot
    {
        public Guid RobotId { get; set; }
        public string RobotName { get; set; }
        public RawRobotState State { get; set; }
    }
}
