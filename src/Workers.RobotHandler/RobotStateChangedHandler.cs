using ElderyCare.Data;
using ElderyCare.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ElderlyCare.Workers.RobotHandler
{
    public class RobotStateChangedHandler
    {
        private readonly IDbContextFactory<ElderlyCareContext> _dbContextFactory;
        private readonly IRobotStateServerSentEventsService _serverSentEventsService;

        public RobotStateChangedHandler(IDbContextFactory<ElderlyCareContext> dbContextFactory, IRobotStateServerSentEventsService serverSentEventsService)
        {
            _dbContextFactory = dbContextFactory;
            _serverSentEventsService = serverSentEventsService;
        }

        public void Handle(Domain.Robot.RobotStateChangedEvent @event)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var entity = dbContext.RobotStates.FirstOrDefault(s => s.RobotId == @event.RobotId);

            if (entity != null)
            {
                entity.UpdateState(@event.NewState);
                dbContext.Update(entity);
            }
            else
            {
                entity = new RobotState(@event.RobotId, @event.RobotName);
                entity.UpdateState(@event.NewState);
                dbContext.Add(entity);
            }

            dbContext.SaveChanges();
            var payload = new
            {
                entity.RobotId,
                entity.RobotName,
                State = @event.NewState
            };

            _serverSentEventsService.SendEventAsync(JsonSerializer.Serialize(payload)).Wait();
        }
    }
}
