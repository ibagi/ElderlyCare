using ElderyCare.Data;
using ElderyCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Linq;

namespace ElderlyCare.Workers.RobotHandler;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDbContextFactory<ElderlyCareContext> _dbContextFactory;
    private readonly RobotStateChangedHandler _changedHandler;

    private readonly List<RobotAgent> _agents = new();

    public Worker(ILogger<Worker> logger, IDbContextFactory<ElderlyCareContext> dbContextFactory, RobotStateChangedHandler changedHandler)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
        _changedHandler = changedHandler;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Workers.RobotHandler...");

        var robots = new List<Robot>();

        using (var dbContext = _dbContextFactory.CreateDbContext())
        {
            robots.AddRange(dbContext.Robots);
        }

        foreach (var robot in robots)
        {
            if (robot.HasOpcConnection())
            {
                var agent = new RobotAgent(robot.OpcUrl, robot.Id, robot.OpcId);
                await agent.ConnectAsync();
                agent.StateChanges.Subscribe(HandleStateChangeEvent);
            }
        }

        _logger.LogInformation("Workers.RobotHandler started...");
        await base.StartAsync(cancellationToken);
    }

    private void HandleStateChangeEvent(Domain.Robot.RobotStateChangedEvent @event)
    {
        _changedHandler.Handle(@event);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Workers.RobotHandler...");

        foreach (var agent in _agents)
        {
            await agent.DisconnectAsync();
        }

        _logger.LogInformation("Workers.RobotHandler stopped...");

        await base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
