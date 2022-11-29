namespace ElderlyCare.Domain.Robot
{
    public record RobotStateChangedEvent(
        Guid RobotId,
        string RobotName,
        RawRobotState NewState);
}
