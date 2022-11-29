using System.Text.Json;

namespace ElderlyCare.Domain.Robot
{
    public sealed class RawRobotState : Dictionary<string, object>
    {
        public static RawRobotState FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new InvalidOperationException("Invalid robot state!");
            }

            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            if (dict is null)
            {
                throw new InvalidOperationException("Unable to deserialize the robot state!");
            }

            var state = new RawRobotState();

            foreach (var key in dict.Keys)
            {
                state[key] = dict[key];
            }

            return state;
        }

        public RawRobotState Clone()
        {
            var clone = new RawRobotState();

            foreach (var key in Keys)
            {
                clone[key] = this[key];
            }

            return clone;
        }
    }
}
