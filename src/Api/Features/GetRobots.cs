using ElderlyCare.Domain.Robot;
using ElderyCare.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCare.Api.Features.Robot
{
    public record GetRobotsRequest : IRequest<GetRobotsResponse>;
    public record GetRobotsResponse(List<Robot> Robots);

    public class GetRobotsHandler : IRequestHandler<GetRobotsRequest, GetRobotsResponse>
    {
        private readonly ElderlyCareContext _dbContext;

        public GetRobotsHandler(ElderlyCareContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetRobotsResponse> Handle(GetRobotsRequest request, CancellationToken cancellationToken)
        {
            var query =
                from robot in _dbContext.Robots
                from state in _dbContext.RobotStates.Where(s => s.RobotId == robot.Id).DefaultIfEmpty()
                select new Robot
                {
                    RobotId = robot.Id,
                    RobotName = robot.Name,
                    State = state != null
                        ? RawRobotState.FromJson(state.State)
                        : new RawRobotState()
                };

            var robots = await query.ToListAsync();
            return new GetRobotsResponse(robots);
        }
    }
}
