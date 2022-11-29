using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Options;

namespace ElderlyCare.Workers.RobotHandler
{
    public interface IRobotStateServerSentEventsService : IServerSentEventsService
    { }

    public class RobotStateServerSentEventsService : ServerSentEventsService, IRobotStateServerSentEventsService
    {
        public RobotStateServerSentEventsService(IOptions<ServerSentEventsServiceOptions<RobotStateServerSentEventsService>> options)
            : base(options.ToBaseServerSentEventsServiceOptions())
        { }
    }
}
