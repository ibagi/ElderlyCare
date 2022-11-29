using ElderlyCare.Workers.RobotHandler;
using ElderyCare.Data.Extensions;
using Lib.AspNetCore.ServerSentEvents;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddElderlyCareContextFactory(builder.Configuration);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<RobotStateChangedHandler>();
builder.Services.AddServerSentEvents<IRobotStateServerSentEventsService, RobotStateServerSentEventsService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();
app.MapServerSentEvents<RobotStateServerSentEventsService>("/robot-states/live");

app.Run();
